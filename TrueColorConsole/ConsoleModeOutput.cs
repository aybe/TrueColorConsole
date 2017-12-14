using System;

namespace TrueColorConsole
{
    [Flags]
    internal enum ConsoleModeOutput : uint
    {
        EnableProcessedOutput = 0x1,
        EnableWrapAtEolOutput = 0x2,
        EnableVirtualTerminalProcessing = 0x4,
        DisableNewlineAutoReturn = 0x8,
        EnableLvbGridWorldwide = 0x10
    }
}