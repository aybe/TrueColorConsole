using System;
using JetBrains.Annotations;

namespace TrueColorConsole
{
    public static partial class VTConsole
    {
        [PublicAPI]
        public static void SetCharacterMode(VTCharSet charSet)
        {
            switch (charSet)
            {
                case VTCharSet.Ascii:
                    Write($"{ESC}(B");
                    break;
                case VTCharSet.DecLineDrawing:
                    Write($"{ESC}(0");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(charSet), charSet, null);
            }
        }

        /// <summary>
        ///     Sets the VT scrolling margins of the viewport.
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        [PublicAPI]
        public static void SetScrollingRegion(int top, int bottom)
        {
            if (top < 1 || top > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(top));

            if (bottom < 1 || bottom > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(bottom));

            Write($"{ESC}[{top};{bottom}r");
        }

        /// <summary>
        ///     Sets the console window’s title.
        /// </summary>
        /// <param name="text"></param>
        [PublicAPI]
        public static void SetWindowTitle([CanBeNull] string text)
        {
            if (text == null)
                text = string.Empty;

            if (text.Length > 255)
                throw new ArgumentOutOfRangeException(nameof(text));

            Write($"{ESC}]2;{text}{BEL}");
        }

        /// <summary>
        ///     Sets the console window’s title.
        /// </summary>
        /// <param name="text"></param>
        [PublicAPI]
        public static void SetWindowTitleAndIcon([CanBeNull] string text)
        {
            if (text == null)
                text = string.Empty;

            if (text.Length > 255)
                throw new ArgumentOutOfRangeException(nameof(text));

            Write($"{ESC}]0;{text}{BEL}");
        }

        /// <summary>
        ///     Switches to a new alternate screen buffer.
        /// </summary>
        [PublicAPI]
        public static void SwitchScreenBufferAlternate()
        {
            Write($"{ESC}[?1049h");
        }

        /// <summary>
        ///     Switches to the main buffer.
        /// </summary>
        [PublicAPI]
        public static void SwitchScreenBufferMain()
        {
            Write($"{ESC}[?1049l");
        }

        /// <summary>
        ///     Sets the console width to 80 columns wide.
        /// </summary>
        [PublicAPI]
        public static void SetConsoleWidth80()
        {
            Write($"{ESC}[?3l");
        }

        /// <summary>
        ///     Sets the console width to 132 columns wide.
        /// </summary>
        [PublicAPI]
        public static void SetConsoleWidth132()
        {
            Write($"{ESC}[?3h");
        }

        /// <summary>
        ///     Reset certain terminal settings to their defaults.
        /// </summary>
        [PublicAPI]
        public static void SoftReset()
        {
            Write($"{ESC}[!p");
        }
    }
}