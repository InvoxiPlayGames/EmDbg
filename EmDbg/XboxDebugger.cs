using NeighborSharp;
using NeighborSharp.Types;
using EmDbg.Types;

namespace EmDbg
{

    public class XboxDebugger : IDebugger
    {
        // interface variable definitions
        static public DebuggerPlatform Platform = DebuggerPlatform.XBOX_XBDM;
        static public DebuggerProcessor Processor = DebuggerProcessor.PPC_64;

        // local variables
        private readonly Xbox360 console;
        private readonly XBDMConnection callerConnection;
        private XBDMConnection? notifyConnection;
        private bool shouldDisconnect;
        private bool isNatelxXBDM;
        private uint reconnectPort = 1;
        private Dictionary<string, ModuleInfo> modules = new();
        private Dictionary<uint, ThreadInfo> threads = new();
        private Dictionary<uint, SetBreakpoint> breakpoints = new();

        // callbacks
        public DebugString? cbDebugString;
        public RawNotifyMessageHandler? cbRawNotifyMessageHandler;
        public BreakpointHit? cbBreakpointHit;
        public ExceptionHit? cbExceptionHit;
        public ExecutionStateChange? cbExecutionStateChange;
        public Rebooting? cbRebooting;

        // options
        public bool ReportDebugLogs = true;

        private void NotifyThread()
        {
            if (notifyConnection == null) return;
            while (!shouldDisconnect)
            {
                // wait for bytes available on our socket, sleep to avoid 100% CPU usage
                while (!notifyConnection.BytesAvailable()) Thread.Sleep(10);
                // parse the arguments
                XboxArguments args = new XboxArguments(notifyConnection.ReadLine());
                // if the program has set a handler for raw messages, send it over
                if (cbRawNotifyMessageHandler != null)
                    cbRawNotifyMessageHandler(args);
                // send a callback depending on the message recieved
                // the first command will always be the type of the message
                switch (args.commands[0])
                {
                    case "debugstr":
                        // if we actually have a string, send it to our callback
                        if (cbDebugString != null && args.stringValues.ContainsKey("string"))
                            cbDebugString(new ThreadInfo(), args.stringValues["string"], args.commands.Contains("lf"));
                        break;
                    case "execution":
                        // report execution state in full if the user wants
                        if (ReportDebugLogs && cbDebugString != null)
                            cbDebugString(null, $"[EmDbg] Execution: '{args.commands[1]}'", true);
                        // if we're rebooting, make sure we drop all connections
                        if (args.commands[1] == "rebooting")
                        {
                            shouldDisconnect = true;
                            if (cbRebooting != null)
                                cbRebooting(false);
                        }
                        // otherwise, let our callback know the state has changed
                        else if (cbExecutionStateChange != null)
                            cbExecutionStateChange(!args.commands.Contains("stopped"));
                        break;
                    case "exception":
                        if (cbExceptionHit != null)
                        {
                            // parse the exception into our struct and throw it
                            ExceptionInfo exception = new();
                            exception.exceptType = args.intValues["code"];
                            exception.thread = args.intValues["thread"];
                            exception.exceptAddress = args.intValues["address"];
                            if (args.intValues.ContainsKey("read")) // invalid read
                                exception.responsibleAddress = args.intValues["read"];
                            else if (args.intValues.ContainsKey("write")) // invalid write
                                exception.responsibleAddress = args.intValues["write"];
                            // TODO: handle nparam/params from non-data exceptions
                            exception.first = args.commands.Contains("first");
                            exception.noncont = args.commands.Contains("noncont");
                            exception.stopped = true; //args.commands.Contains("stop"); - i dont know bro
                            cbExceptionHit(exception);
                        }
                        break;
                    case "break":
                        if (cbBreakpointHit != null)
                        {
                            // parse the breakpoint into our struct and throw it
                            Breakpoint bp = new();
                            bp.type = BreakpointType.EXECUTE;
                            bp.breakAddr = args.intValues["addr"];
                            bp.thread = args.intValues["thread"];
                            bp.stopped = args.commands.Contains("stop");
                            bp.length = 0x4; // if its an instruction its always 0x4
                            cbBreakpointHit(bp);
                        }
                        break;
                    case "databreak":
                        if (cbBreakpointHit != null)
                        {
                            // parse the breakpoint into our struct and throw it
                            Breakpoint bp = new();
                            if (args.intValues.ContainsKey("read"))
                            {
                                bp.type = BreakpointType.READ;
                                bp.dataAddr = args.intValues["read"];
                            } else if (args.intValues.ContainsKey("readwrite"))
                            {
                                bp.type = BreakpointType.READWRITE;
                                bp.dataAddr = args.intValues["readwrite"];
                            }
                            else if (args.intValues.ContainsKey("execute"))
                            {
                                bp.type = BreakpointType.EXECUTE;
                                bp.dataAddr = args.intValues["execute"];
                            }
                            bp.breakAddr = args.intValues["addr"];
                            bp.thread = args.intValues["thread"];
                            bp.stopped = args.commands.Contains("stop");
                            bp.length = 0x0; // no idea??
                            cbBreakpointHit(bp);
                        }
                        break;
                    default:
                        if (ReportDebugLogs && cbDebugString != null)
                            cbDebugString(null, $"[EmDbg] Unknown XBDM notification '{args.commands[0]}'", true);
                        break;
                }
            }
            notifyConnection.Dispose();
            callerConnection.Dispose();
        }

        public MemoryMapInfo[] GetMemoryMap()
        {
            // this can take a long time, so connect again
            XBDMConnection xbdm = new(console);
            List<MemoryMapInfo> list = new();
            XboxArguments[] resp = callerConnection.CommandMultiline("walkmem");
            xbdm.Dispose();
            foreach (XboxArguments r in resp)
            {
                MemoryMapInfo mm = new();
                mm.startAddr = r.intValues["base"];
                mm.length = r.intValues["size"];
                mm.protect = r.intValues["protect"];
                mm.physicalAddr = r.intValues["phys"];
                list.Add(mm);
            }
            return list.ToArray();
        }

        private static byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public byte[] GetMemory(uint address, uint length)
        {
            // this can take a long time, so connect again
            XBDMConnection xbdm = new(console);
            XboxArguments args = new("getmem");
            args.intValues["addr"] = address;
            args.intValues["length"] = length;
            string[] memhex = xbdm.CommandMultilineStrings(args);
            byte[] mem = StringToByteArray(string.Join("", memhex));
            xbdm.Dispose();
            return mem;
        }

        public void SetMemory(uint address, byte[] data)
        {
            // this can take a long time, so connect again
            XBDMConnection xbdm = new(console);
            XboxArguments args = new("setmem");
            args.intValues["addr"] = address;
            args.stringValues["data"] = BitConverter.ToString(data).Replace("-", "");
            xbdm.Command(args);
            xbdm.Dispose();
            return;
        }

        public void RawCommand(string command)
        {
            // this can take a long time, so connect again
            XBDMConnection xbdm = new(console);
            xbdm.Command(command);
            xbdm.Dispose();
            return;
        }

        public bool IsModdedConsole()
        {
            return isNatelxXBDM;
        }

        public string Identification()
        {
            return $"{console.DebugName} ({console.EndPoint.Address}){(!isNatelxXBDM ? " (XDK)" : "")}";
        }

        public string RunningTitle()
        {
            return callerConnection.CommandMultilineArg("xbeinfo running").stringValues["name"].Split('\\').Last();
        }

        public void StopExecution()
        {
            callerConnection.Command("stop");
        }

        public void ResumeExecution()
        {
            callerConnection.Command("go");
        }
        
        public void RestartSystem()
        {
            callerConnection.Command("magicboot cold");
        }

        public void RestartTitle()
        {
            callerConnection.Command("magicboot");
        }

        public bool IsExecutionRunning()
        {
            return callerConnection.Command("getexecstate").message == "start";
        }

        public void ResumeThread(uint threadID)
        {
            XboxArguments arg = new("suspend");
            arg.intValues.Add("thread", threadID);
            callerConnection.Command(arg);
        }

        public void PauseThread(uint threadID)
        {
            XboxArguments arg = new("resume");
            arg.intValues.Add("thread", threadID);
            callerConnection.Command(arg);
        }

        public void ClearAllBreakpoints()
        {
            XboxArguments arg = new("break");
            arg.commands.Add("clearall");
            callerConnection.Command(arg);
            breakpoints.Clear();
        }

        public void DataBreakpoint(uint address, uint size, BreakpointType type)
        {
            string bptype = "read";
            if (type == BreakpointType.WRITE)
                bptype = "write";
            else if (type == BreakpointType.READWRITE)
                bptype = "readwrite";
            else if (type == BreakpointType.EXECUTE)
                bptype = "execute";
            XboxArguments arg = new("break");
            arg.intValues.Add(bptype, address);
            arg.intValues.Add("size", size);
            callerConnection.Command(arg);

            SetBreakpoint sbp = new();
            sbp.address = address;
            sbp.length = size;
            sbp.type = type;
            breakpoints.Add(address, sbp);
        }

        public void ClearDataBreakpoint(uint address, BreakpointType type)
        {
            string bptype = "read";
            if (type == BreakpointType.WRITE)
                bptype = "write";
            else if (type == BreakpointType.READWRITE)
                bptype = "readwrite";
            else if (type == BreakpointType.EXECUTE)
                bptype = "execute";
            XboxArguments arg = new("break");
            arg.commands.Add("clear");
            arg.intValues.Add(bptype, address);
            callerConnection.Command(arg);

            breakpoints.Remove(address);
        }

        public void ExecuteBreakpoint(uint address)
        {
            XboxArguments arg = new("break");
            arg.intValues.Add("addr", address);
            XboxResponse j = callerConnection.Command(arg);

            SetBreakpoint sbp = new();
            sbp.address = address;
            sbp.length = 4;
            sbp.type = BreakpointType.EXECUTE;
            breakpoints.Add(address, sbp);
        }

        public void ClearExecuteBreakpoint(uint address)
        {
            XboxArguments arg = new("break");
            arg.commands.Add("clear");
            arg.intValues.Add("addr", address);
            callerConnection.Command(arg);

            breakpoints.Remove(address);
        }

        public bool IsBreakpointed(uint address)
        {
            return breakpoints.ContainsKey(address);
        }

        public SetBreakpoint[] CurrentBreakpoints()
        {
            return breakpoints.Values.ToArray();
        }

        public ThreadContext GetThreadContext(uint threadID)
        {
            XboxArguments arg = new("getcontext");
            arg.commands.Add("control");
            arg.commands.Add("int");
            arg.intValues.Add("thread", threadID);
            XboxArguments resp = callerConnection.CommandMultilineArg(arg);
            ThreadContext ctx = new();
            ctx.cr = resp.intValues["Cr"];
            ctx.msr = resp.intValues["Msr"];
            ctx.xer = resp.intValues["Xer"];
            ctx.pc = resp.intValues["Iar"];
            ctx.lr = resp.intValues["Lr"];
            ctx.ctr = resp.longValues["Ctr"];
            for (int i = 0; i < 32; i++)
            {
                ctx.gpr[i] = resp.longValues[$"Gpr{i}"];
            }
            // todo: fpu, vectors
            return ctx;
        }

        public void SetThreadContext(uint threadID, ThreadContext ctx)
        {
            XboxArguments arg = new("setcontext");
            // send the control register values
            arg.intValues.Add("thread", threadID);
            arg.intValues.Add("Cr", ctx.cr);
            arg.intValues.Add("Msr", ctx.msr);
            arg.intValues.Add("Xer", ctx.xer);
            arg.intValues.Add("Iar", ctx.pc);
            arg.intValues.Add("Lr", ctx.lr);
            arg.longValues.Add("Ctr", ctx.ctr);
            callerConnection.Command(arg);
            // split the GPRs across multiple packets due to the size limit
            arg = new("setcontext");
            arg.intValues.Add("thread", threadID);
            for (int i = 0; i < 19; i++)
            {
                arg.longValues.Add($"Gpr{i}", ctx.gpr[i]);
            }
            callerConnection.Command(arg);
            // packet number 2!
            arg = new("setcontext");
            arg.intValues.Add("thread", threadID);
            for (int i = 19; i < 32; i++)
            {
                arg.longValues.Add($"Gpr{i}", ctx.gpr[i]);
            }
            callerConnection.Command(arg);
            // todo: fpu, vectors
        }

        public void SubscribeNotifications()
        {
            notifyConnection = new(console);
            XboxArguments arg = new("notify");
            arg.intValues.Add("reconnectport", reconnectPort);
            XboxResponse resp = notifyConnection.Command(arg);
            if (resp.statusCode != 205)
            {
                throw new Exception($"Could not start notification thread on Xbox; console returned {resp.statusCode}: {resp.message}");
            }
            Task.Run(NotifyThread);
        }

        public XboxDebugger(Xbox360 xbox) {
            console = xbox;
            callerConnection = new(console);
            // check what XBDM version we have (some features differ between devkit and rgh)
            // RIP natelx 2018 <3 thanks for everything
            isNatelxXBDM = callerConnection.Command("whomadethis").message.Contains("Natelx");
            // tell the console we're debugging
            XboxArguments arg = new("debugger");
            arg.commands.Add("connect");
            arg.commands.Add("override"); // in case there's already a debugging session
            arg.stringValues.Add("name", "EmDbg");
            arg.stringValues.Add("user", "EmDbg");
            XboxResponse resp = callerConnection.Command(arg);
            if (resp.statusCode != 200)
            {
                throw new Exception($"Could not start debugging session on Xbox; console returned {resp.statusCode}: {resp.message}");
            }
            // tell the console to stop on exceptions
            arg = new("stopon");
            arg.commands.Add("fce");
            callerConnection.Command(arg);
            // start up a notification socket on another thread
            Task.Run(NotifyThread);
        }
    }
}