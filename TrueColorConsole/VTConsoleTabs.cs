using System;
using JetBrains.Annotations;

namespace TrueColorConsole
{
    public static partial class VTConsole
    {
        /// <summary>
        ///     Sets a tab stop in the current column the cursor is in.
        /// </summary>
        [PublicAPI]
        public static void TabHorizontalSet()
        {
            WriteConcat(ESC, "H");
        }

        /// <summary>
        ///     Advance the cursor to the next column (in the same row) with a tab stop. If there are no more tab stops, move to
        ///     the last column in the row. If the cursor is in the last column, move to the first column of the next row.
        /// </summary>
        /// <param name="columns">
        ///     Number of columns to move by, one-based.
        /// </param>
        [PublicAPI]
        public static void TabCursorForward(int columns)
        {
            if (columns < 1 || columns > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(columns));

            WriteConcat(ESC, "[", columns, "l");
        }

        /// <summary>
        ///     Move the cursor to the previous column (in the same row) with a tab stop. If there are no more tab stops, moves the
        ///     cursor to the first column. If the cursor is in the first column, doesn’t move the cursor.
        /// </summary>
        /// <param name="columns">
        ///     Number of columns to move by, one-based.
        /// </param>
        [PublicAPI]
        public static void TabCursorBackward(int columns)
        {
            if (columns < 1 || columns > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(columns));

            WriteConcat(ESC, "[", columns, "Z");
        }

        /// <summary>
        ///     Clears the tab stop in the current column, if there is one. Otherwise does nothing.
        /// </summary>
        [PublicAPI]
        public static void TabClear()
        {
            WriteConcat(ESC, "[0g");
        }

        /// <summary>
        ///     Clears all currently set tab stops.
        /// </summary>
        [PublicAPI]
        public static void TabClearAll()
        {
            WriteConcat(ESC, "[3g");
        }
    }
}