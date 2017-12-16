using JetBrains.Annotations;

namespace TrueColorConsole
{
    /// <summary>
    ///     Defines the modes for keypad keys.
    /// </summary>
    [PublicAPI]
    public enum VTKeypadMode
    {
        /// <summary>
        ///     Keypad keys will emit their Application Mode sequences.
        /// </summary>
        Application,

        /// <summary>
        ///     Keypad keys will emit their Numeric Mode sequences.
        /// </summary>
        Numeric
    }
}