using JetBrains.Annotations;

namespace TrueColorConsole
{
    /// <summary>
    ///     Defines the mode for erasure.
    /// </summary>
    [PublicAPI]
    public enum VTEraseMode
    {
        /// <summary>
        ///     Erases from the beginning of the line/display up to and including the current cursor position.
        /// </summary>
        FromBeginningToCursor = 0,

        /// <summary>
        ///     Erases from the current cursor position (inclusive) to the end of the line/display.
        /// </summary>
        FromCursorToEnd = 1,

        /// <summary>
        ///     Erases the entire line/display.
        /// </summary>
        Entirely = 2
    }
}