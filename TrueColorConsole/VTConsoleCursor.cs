using System;
using JetBrains.Annotations;

namespace TrueColorConsole
{
    public static partial class VTConsole
    {
        /// <summary>
        ///     Cursor moves to Nth position horizontally in the current line.
        /// </summary>
        /// <param name="column">
        ///     Column to move to, one-based.
        /// </param>
        [PublicAPI]
        public static void CursorAbsoluteHorizontal(int column)
        {
            if (column < 1 || column > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(column));

            WriteConcat(ESC, "[", column, "G");
        }

        /// <summary>
        ///     Cursor moves to the Nth position vertically in the current column.
        /// </summary>
        /// <param name="row">
        ///     Row to move to, one-based.
        /// </param>
        [PublicAPI]
        public static void CursorAbsoluteVertical(int row)
        {
            if (row < 1 || row > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(row));

            WriteConcat(ESC, "[", row, "d");
        }

        /// <summary>
        ///     Performs a restore cursor option like <see cref="CursorPositionRestore" />.
        /// </summary>
        [PublicAPI]
        public static void CursorAnsiRestore()
        {
            WriteConcat(ESC, "[u");
        }

        /// <summary>
        ///     Performs a save cursor operation like <see cref="CursorPositionSave" />.
        /// </summary>
        [PublicAPI]
        public static void CursorAnsiSave()
        {
            WriteConcat(ESC, "[s");
        }

        /// <summary>
        ///     Cursor down to beginning of Nth line in the viewport.
        /// </summary>
        /// <param name="line">
        ///     Line to move to, one-based.
        /// </param>
        [PublicAPI]
        public static void CursorLineDown(int line = 1)
        {
            if (line < 1 || line > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(line));

            WriteConcat(ESC, "[", line, "E");
        }

        /// <summary>
        ///     Cursor up to beginning of Nth line in the viewport.
        /// </summary>
        /// <param name="line">
        ///     Line to move to, one-based.
        /// </param>
        [PublicAPI]
        public static void CursorLineUp(int line = 1)
        {
            if (line < 1 || line > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(line));

            WriteConcat(ESC, "[", line, "F");
        }

        /// <summary>
        ///     Cursor up by N rows.
        /// </summary>
        /// <param name="rows">
        ///     Number of rows to move by.
        /// </param>
        [PublicAPI]
        public static void CursorMoveUp(int rows = 1)
        {
            if (rows < 1 || rows > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rows));

            WriteConcat(ESC, "[", rows, "A");
        }

        /// <summary>
        ///     Cursor down by N rows.
        /// </summary>
        /// <param name="rows">
        ///     Number of rows to move by.
        /// </param>
        [PublicAPI]
        public static void CursorMoveDown(int rows = 1)
        {
            if (rows < 1 || rows > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rows));

            WriteConcat(ESC, "[", rows, "B");
        }

        /// <summary>
        ///     Cursor right by N columns.
        /// </summary>
        /// <param name="columns">
        ///     Number of columns to move by.
        /// </param>
        [PublicAPI]
        public static void CursorMoveRight(int columns = 1)
        {
            if (columns < 1 || columns > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(columns));

            WriteConcat(ESC, "[", columns, "C");
        }

        /// <summary>
        ///     Cursor left by N columns.
        /// </summary>
        /// <param name="columns">
        ///     Number of columns to move by.
        /// </param>
        [PublicAPI]
        public static void CursorMoveLeft(int columns = 1)
        {
            if (columns < 1 || columns > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(columns));

            WriteConcat(ESC, "[", columns, "D");
        }

        /// <summary>
        ///     Cursor move to coordinates within the viewport.
        /// </summary>
        /// <param name="column">
        ///     Column to move to, one-based.
        /// </param>
        /// <param name="row">
        ///     Row to move to, one-based.
        /// </param>
        [PublicAPI]
        public static void CursorPosition(int column, int row)
        {
            if (column < 1 || column > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(column));

            if (row < 1 || row > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(row));

            WriteConcat(ESC, "[", column, ";", row, "H");
        }

        /// <summary>
        ///     Restore cursor position in memory (see Remarks).
        /// </summary>
        /// <remarks>
        ///     There will be no value saved in memory until the first use of the save command. The only way to access the saved
        ///     value is with the restore command.
        /// </remarks>
        [PublicAPI]
        public static void CursorPositionRestore()
        {
            WriteConcat(ESC, 8);
        }

        /// <summary>
        ///     Save cursor position in memory (see Remarks).
        /// </summary>
        /// <remarks>
        ///     There will be no value saved in memory until the first use of the save command. The only way to access the saved
        ///     value is with the restore command.
        /// </remarks>
        [PublicAPI]
        public static void CursorPositionSave()
        {
            WriteConcat(ESC, 7);
        }

        /// <summary>
        ///     Reverse Index – Performs the reverse operation of \n, moves cursor up one line, maintains horizontal position,
        ///     scrolls buffer if necessary (see Remarks).
        /// </summary>
        /// <remarks>
        ///     If there are scroll margins set, RI inside the margins will scroll only the contents of the margins, and leave the
        ///     viewport unchanged.
        /// </remarks>
        [PublicAPI]
        public static void CursorReverseIndex()
        {
            WriteConcat(ESC, "M");
        }

        /// <summary>
        ///     Sets cursor blinking.
        /// </summary>
        /// <param name="enabled">
        ///     Enable blinking.
        /// </param>
        [PublicAPI]
        public static void CursorSetBlinking(bool enabled)
        {
            WriteConcat(ESC, "[?12", enabled ? "h" : "l");
        }

        /// <summary>
        ///     Sets cursor visibility.
        /// </summary>
        /// <param name="enabled">
        ///     Enable visibility.
        /// </param>
        [PublicAPI]
        public static void CursorSetVisibility(bool enabled)
        {
            WriteConcat(ESC, "[?25", enabled ? "h" : "l");
        }
    }
}