using JetBrains.Annotations;

namespace TrueColorConsole
{
    /// <summary>
    ///     Defines cursor keys.
    /// </summary>
    [PublicAPI]
    public enum VTCursorKey
    {
        /// <summary>
        ///     'Up arrow' key.
        /// </summary>
        UpArrow,

        /// <summary>
        ///     'Down arrow' key.
        /// </summary>
        DownArrow,

        /// <summary>
        ///     'Right arrow' key.
        /// </summary>
        RightArrow,

        /// <summary>
        ///     'Left arrow' key.
        /// </summary>
        LeftArrow,

        /// <summary>
        ///     'Home' key.
        /// </summary>
        Home,

        /// <summary>
        ///     'End' key.
        /// </summary>
        End,

        /// <summary>
        ///     'Ctrl' + 'Up arrow' keys.
        /// </summary>
        CtrlUpArrow,

        /// <summary>
        ///     'Ctrl' + 'Down arrow' keys.
        /// </summary>
        CtrlDownArrow,

        /// <summary>
        ///     'Ctrl' + 'Right arrow' keys.
        /// </summary>
        CtrlRightArrow,

        /// <summary>
        ///     'Ctrl' + 'Left arrow' keys.
        /// </summary>
        CtrlLeftArrow
    }
}