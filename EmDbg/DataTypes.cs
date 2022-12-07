namespace EmDbg.Types
{
    public enum DebuggerPlatform
    {
        XBOX_XBDM,
        WII_GECKO
    }

    public enum DebuggerProcessor
    {
        PPC_32,
        PPC_64
    }

    public enum BreakpointType
    {
        READ,
        WRITE,
        READWRITE,
        EXECUTE
    }

    public class ThreadContext
    {
        public ulong[] gpr = new ulong[32];
        public ulong[] fpr = new ulong[32];
        public ulong ctr;
        public uint msr;
        public uint pc;
        public uint cr;
        public uint xer;
        public uint lr;
        // todo: vectors on x360 debugging
    }

    public class ThreadInfo
    {
        public uint threadID;
        public string? name;
        public uint priority;
        public uint startAddr;
        public uint stackBase;
        public uint stackLimit;
        public uint stackSlack;
        public DateTime creationDate;
    }

    public class ModuleSegments
    {
        public string? name;
        public uint startAddr;
        public uint length;
        public uint flags;
    }

    public class ModuleInfo
    {
        public string? name;
        public uint baseAddr;
        public uint length;
        public uint checksum;
        public uint thread;
        public DateTime timestamp;
    }

    public class MemoryMapInfo
    {
        public uint startAddr;
        public uint length;
        public uint protect;
        public uint physicalAddr;
    }

    public class Breakpoint
    {
        public uint breakAddr;
        public uint dataAddr;
        public uint length;
        public uint thread;
        public BreakpointType type;
        public bool stopped;
    }

    public class SetBreakpoint
    {
        public uint address;
        public uint length;
        public BreakpointType type;
    }

    public class ExceptionInfo
    {
        public uint exceptAddress;
        public uint exceptType;
        public uint responsibleAddress;
        public uint thread;
        public bool write;
        public bool first;
        public bool noncont;
        public bool stopped;
    }
}
