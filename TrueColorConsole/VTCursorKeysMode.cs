using JetBrains.Annotations;

namespace TrueColorConsole
{
    /// <summary>
    ///     Defines the mode for cursor keys.
    /// </summary>
    [PublicAPI]
    public enum VTCursorKeysMode
    {
        /// <summary>
        ///     Cursor keys will emit their Application Mode sequences.
        /// </summary>
        Application,

        /// <summary>
        ///     Cursor keys will emit their Numeric Mode sequences.
        /// </summary>
        Normal
    }
}