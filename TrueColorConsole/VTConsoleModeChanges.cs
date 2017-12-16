using System;
using JetBrains.Annotations;

namespace TrueColorConsole
{
    public static partial class VTConsole
    {
        private static VTCursorKeysMode _cursorKeysMode = VTCursorKeysMode.Normal;
        private static VTKeypadMode _keypadMode = VTKeypadMode.Numeric;

        /// <summary>
        ///     Gets or sets the mode for cursor keys.
        /// </summary>
        [PublicAPI]
        public static VTCursorKeysMode CursorKeysMode
        {
            get => _cursorKeysMode;
            set
            {
                switch (value)
                {
                    case VTCursorKeysMode.Normal:
                        WriteConcat(ESC, "[?1l");
                        break;
                    case VTCursorKeysMode.Application:
                        WriteConcat(ESC, "[?1h");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
                _cursorKeysMode = value;
            }
        }

        /// <summary>
        ///     Gets or sets the mode for keypad keys.
        /// </summary>
        [PublicAPI]
        public static VTKeypadMode KeypadMode
        {
            get => _keypadMode;
            set
            {
                switch (value)
                {
                    case VTKeypadMode.Numeric:
                        WriteConcat(ESC, ">");
                        break;
                    case VTKeypadMode.Application:
                        WriteConcat(ESC, "=");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
                _keypadMode = value;
            }
        }
    }
}