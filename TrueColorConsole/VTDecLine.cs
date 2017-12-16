using JetBrains.Annotations;

namespace TrueColorConsole
{
    [PublicAPI]
    public enum VTDecLine : byte
    {
        LowerRightCrossing = (byte) 'j',
        UpperRightCrossing = (byte) 'k',
        UpperLeftCrossing = (byte) 'l',
        LowerLeftCrossing = (byte) 'm',
        CrossingLines = (byte) 'n',
        HorizontalLine = (byte) 'q',
        LeftT = (byte) 't',
        RightT = (byte) 'u',
        BottomT = (byte) 'v',
        TopT = (byte) 'w',
        VerticalBar = (byte) 'x'
    }
}