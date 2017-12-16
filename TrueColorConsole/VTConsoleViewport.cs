using System;
using JetBrains.Annotations;

namespace TrueColorConsole
{
    public static partial class VTConsole
    {
        /// <summary>
        ///     Scroll text up by N lines. Also known as pan down, new lines fill in from the bottom of the screen.
        /// </summary>
        /// <param name="lines">
        ///     Number of lines to scroll by.
        /// </param>
        [PublicAPI]
        public static void ScrollUp(int lines = 1)
        {
            if (lines < 1)
                throw new ArgumentOutOfRangeException(nameof(lines));

            WriteConcat(ESC, "[", lines, "S");
        }

        /// <summary>
        ///     Scroll down by N lines. Also known as pan up, new lines fill in from the top of the screen.
        /// </summary>
        /// <param name="lines">
        ///     Number of lines to scroll by.
        /// </param>
        [PublicAPI]
        public static void ScrollDown(int lines = 1)
        {
            if (lines < 1)
                throw new ArgumentOutOfRangeException(nameof(lines));

            WriteConcat(ESC, "[", lines, "T");
        }
    }
}