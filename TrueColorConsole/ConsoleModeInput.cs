using System;
using System.Diagnostics.CodeAnalysis;

namespace TrueColorConsole
{
    [Flags]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal enum ConsoleModeInput : uint
    {
        EnableProcessedInput = 0x1,
        EnableLineInput = 0x2,
        EnableEchoInput = 0x4,
        EnableWindowInput = 0x8,
        EnableMouseInput = 0x10,
        EnableInsertMode = 0x20,
        EnableQuickEditMode = 0x40,
        EnableExtendedFlags = 0x80,
        EnableAutoPosition = 0x100,
        EnableVirtualTerminalInput = 0x200
    }
}