using JetBrains.Annotations;

namespace TrueColorConsole
{
    /// <summary>
    ///     Defines DEC line drawing elements.
    /// </summary>
    [PublicAPI]
    public enum VTDecLine : byte
    {
        /// <summary>
        ///     Lower-right crossing.
        /// </summary>
        LowerRightCrossing = (byte) 'j',

        /// <summary>
        ///     Upper-right crossing.
        /// </summary>
        UpperRightCrossing = (byte) 'k',

        /// <summary>
        ///     Upper-left crossing.
        /// </summary>
        UpperLeftCrossing = (byte) 'l',

        /// <summary>
        ///     Lower-left crossing.
        /// </summary>
        LowerLeftCrossing = (byte) 'm',

        /// <summary>
        ///     Crossing lines.
        /// </summary>
        CrossingLines = (byte) 'n',

        /// <summary>
        ///     Horizontal line.
        /// </summary>
        HorizontalLine = (byte) 'q',

        /// <summary>
        ///     Left T.
        /// </summary>
        LeftT = (byte) 't',

        /// <summary>
        ///     Right T.
        /// </summary>
        RightT = (byte) 'u',

        /// <summary>
        ///     Bottom T.
        /// </summary>
        BottomT = (byte) 'v',

        /// <summary>
        ///     Top T.
        /// </summary>
        TopT = (byte) 'w',

        /// <summary>
        ///     Vertical bar.
        /// </summary>
        VerticalBar = (byte) 'x'
    }
}