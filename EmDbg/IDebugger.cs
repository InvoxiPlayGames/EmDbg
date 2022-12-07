using EmDbg.Types;
using NeighborSharp.Types;

namespace EmDbg
{
    public delegate void RawNotifyMessageHandler(XboxArguments message);
    public delegate void BreakpointHit(Breakpoint bp);
    public delegate void ExceptionHit(ExceptionInfo exception);
    public delegate void DebugString(ThreadInfo? thread, string text, bool lf);
    public delegate void NewThread(ThreadInfo thread);
    public delegate void ThreadStop(ThreadInfo thread);
    public delegate void ModuleLoad(ModuleInfo module);
    public delegate void ModuleUnload(ModuleInfo module);
    public delegate void ExecutionStateChange(bool running);
    public delegate void Rebooting(bool done);

    public interface IDebugger
    {
        //public bool SupportsLogging { get; set; }
        //public bool SupportsBreakpoints { get; set; }
        //public bool SupportsExceptions { get; set; }
        //public bool SupportsThreading { get; set; }
        //public bool SupportsModules { get; set; }
        static public DebuggerPlatform Platform { get; set; }
        static public DebuggerProcessor Processor { get; set; }

        // TODO: Finish interface definitions
    }
}
