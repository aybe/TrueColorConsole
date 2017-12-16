using System;
using JetBrains.Annotations;

namespace TrueColorConsole
{
    public static partial class VTConsole
    {
        /// <summary>
        ///     Sets the active character mode.
        /// </summary>
        /// <param name="charSet">
        ///     Character set to use.
        /// </param>
        [PublicAPI]
        public static void SetCharacterMode(VTCharSet charSet)
        {
            switch (charSet)
            {
                case VTCharSet.Ascii:
                    WriteConcat(ESC, "(B");
                    break;
                case VTCharSet.DecLineDrawing:
                    WriteConcat(ESC, "(0");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(charSet), charSet, null);
            }
        }

        /// <summary>
        ///     Sets the VT scrolling margins of the viewport.
        /// </summary>
        /// <param name="top">
        ///     Top line of the scroll region, one-based, inclusive.
        /// </param>
        /// <param name="bottom">
        ///     Bottom line of the scroll region, one-based, inclusive.
        /// </param>
        [PublicAPI]
        public static void SetScrollingRegion(int top, int bottom)
        {
            if (top < 1 || top > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(top));

            if (bottom < 1 || bottom > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(bottom));

            WriteConcat(ESC, "[", top, ";", bottom, "r");
        }

        /// <summary>
        ///     Sets the console window’s title.
        /// </summary>
        /// <param name="text">
        ///     Text for the console window title, 255 characters at most.
        /// </param>
        [PublicAPI]
        public static void SetWindowTitle([CanBeNull] string text)
        {
            if (text == null)
                text = string.Empty;

            if (text.Length > 255)
                throw new ArgumentOutOfRangeException(nameof(text));

            WriteConcat(ESC.Length, "]2;", text, BEL);
        }

        /// <summary>
        ///     Sets the console window’s title.
        /// </summary>
        /// <param name="text">
        ///     Text for the console window title, 255 characters at most.
        /// </param>
        [PublicAPI]
        public static void SetWindowTitleAndIcon([CanBeNull] string text)
        {
            if (text == null)
                text = string.Empty;

            if (text.Length > 255)
                throw new ArgumentOutOfRangeException(nameof(text));

            WriteConcat(ESC.Length, "]0;", text, BEL);
        }

        /// <summary>
        ///     Switches to a new alternate screen buffer.
        /// </summary>
        [PublicAPI]
        public static void SwitchScreenBufferAlternate()
        {
            WriteConcat(ESC, "[?1049h");
        }

        /// <summary>
        ///     Switches to the main buffer.
        /// </summary>
        [PublicAPI]
        public static void SwitchScreenBufferMain()
        {
            WriteConcat(ESC, "[?1049l");
        }

        /// <summary>
        ///     Sets the console width to 80 columns wide.
        /// </summary>
        [PublicAPI]
        public static void SetConsoleWidth80()
        {
            WriteConcat(ESC, "[?3l");
        }

        /// <summary>
        ///     Sets the console width to 132 columns wide.
        /// </summary>
        [PublicAPI]
        public static void SetConsoleWidth132()
        {
            WriteConcat(ESC, "[?3h");
        }

        /// <summary>
        ///     Reset certain terminal settings to their defaults.
        /// </summary>
        [PublicAPI]
        public static void SoftReset()
        {
            WriteConcat(ESC, "[!p");
        }
    }
}