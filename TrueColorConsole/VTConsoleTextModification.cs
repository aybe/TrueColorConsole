using System;
using JetBrains.Annotations;

namespace TrueColorConsole
{
    public static partial class VTConsole
    {
        /// <summary>
        ///     Insert N spaces at the current cursor position, shifting all existing text to the right. Text exiting the screen to
        ///     the right is removed.
        /// </summary>
        /// <param name="count">
        ///     Number of characters to insert.
        /// </param>
        [PublicAPI]
        public static void CharacterInsert(int count = 1)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            WriteConcat(ESC, "[", count, "@");
        }

        /// <summary>
        ///     Delete N characters at the current cursor position, shifting in space characters from the right edge of the screen.
        /// </summary>
        /// <param name="count">
        ///     Number of characters to delete.
        /// </param>
        [PublicAPI]
        public static void CharacterDelete(int count = 1)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            WriteConcat(ESC, "[", count, "P");
        }

        /// <summary>
        ///     Erase N characters from the current cursor position by overwriting them with a space character.
        /// </summary>
        /// <param name="count">
        ///     Number of characters to erase.
        /// </param>
        [PublicAPI]
        public static void CharacterErase(int count = 1)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            WriteConcat(ESC, "[", count, "X");
        }

        /// <summary>
        ///     Inserts N lines into the buffer at the cursor position. The line the cursor is on, and lines below it, will be
        ///     shifted downwards.
        /// </summary>
        /// <param name="count">
        ///     Number of lines to insert.
        /// </param>
        [PublicAPI]
        public static void LineInsert(int count = 1)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            WriteConcat(ESC, "[", count, "L");
        }

        /// <summary>
        ///     Deletes N lines from the buffer, starting with the row the cursor is on.
        /// </summary>
        /// <param name="count">
        ///     Number of lines to delete.
        /// </param>
        [PublicAPI]
        public static void LineDelete(int count = 1)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException(nameof(count));

            WriteConcat(ESC, "[", count, "M");
        }

        /// <summary>
        ///     Replace all text in the current viewport/screen with space characters.
        /// </summary>
        /// <param name="eraseMode">
        ///     Erase mode for the replacement.
        /// </param>
        [PublicAPI]
        public static void EraseInDisplay(VTEraseMode eraseMode = VTEraseMode.FromCursorToEnd)
        {
            WriteConcat(ESC, "[", (int) eraseMode, "J");
        }

        /// <summary>
        ///     Replace all text on the line with the cursor with space characters.
        /// </summary>
        /// <param name="eraseMode">
        ///     Erase mode for the replacement.
        /// </param>
        [PublicAPI]
        public static void EraseInLine(VTEraseMode eraseMode = VTEraseMode.FromCursorToEnd)
        {
            WriteConcat(ESC, "[", (int) eraseMode, "K");
        }
    }
}