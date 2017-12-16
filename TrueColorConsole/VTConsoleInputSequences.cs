using System;
using JetBrains.Annotations;

namespace TrueColorConsole
{
    public static partial class VTConsole
    {
        /// <summary>
        ///     Emits a key.
        /// </summary>
        /// <param name="cursorKey">
        ///     Cursor key to emit.
        /// </param>
        [PublicAPI]
        public static void Emit(VTCursorKey cursorKey)
        {
            string s;

            switch (CursorKeysMode)
            {
                case VTCursorKeysMode.Application:
                    s = "O";
                    break;
                case VTCursorKeysMode.Normal:
                    s = "[";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (cursorKey)
            {
                case VTCursorKey.UpArrow:
                    Write($"{ESC}{s}A");
                    break;
                case VTCursorKey.DownArrow:
                    Write($"{ESC}{s}B");
                    break;
                case VTCursorKey.RightArrow:
                    Write($"{ESC}{s}C");
                    break;
                case VTCursorKey.LeftArrow:
                    Write($"{ESC}{s}D");
                    break;
                case VTCursorKey.Home:
                    Write($"{ESC}{s}H");
                    break;
                case VTCursorKey.End:
                    Write($"{ESC}{s}F");
                    break;
                case VTCursorKey.CtrlUpArrow:
                    Write($"{ESC}[1;5A");
                    break;
                case VTCursorKey.CtrlDownArrow:
                    Write($"{ESC}[1;5B");
                    break;
                case VTCursorKey.CtrlRightArrow:
                    Write($"{ESC}[1;5C");
                    break;
                case VTCursorKey.CtrlLeftArrow:
                    Write($"{ESC}[1;5D");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cursorKey), cursorKey, null);
            }
        }

        /// <summary>
        ///     Emits a key.
        /// </summary>
        /// <param name="key">
        ///     Key to emit.
        /// </param>
        [PublicAPI]
        public static void Emit(VTKey key)
        {
            switch (key)
            {
                case VTKey.Backspace:
                    Write($"{DEL}");
                    break;
                case VTKey.Pause:
                    Write($"{SUB}");
                    break;
                case VTKey.Escape:
                    Write($"{ESC}");
                    break;
                case VTKey.Insert:
                    Write($"{ESC}[2~");
                    break;
                case VTKey.Delete:
                    Write($"{ESC}[3~");
                    break;
                case VTKey.PageUp:
                    Write($"{ESC}[5~");
                    break;
                case VTKey.PageDown:
                    Write($"{ESC}[6~");
                    break;
                case VTKey.F1:
                    Write($"{ESC}OP");
                    break;
                case VTKey.F2:
                    Write($"{ESC}OQ");
                    break;
                case VTKey.F3:
                    Write($"{ESC}OR");
                    break;
                case VTKey.F4:
                    Write($"{ESC}OS");
                    break;
                case VTKey.F5:
                    Write($"{ESC}[15~");
                    break;
                case VTKey.F6:
                    Write($"{ESC}[17~");
                    break;
                case VTKey.F7:
                    Write($"{ESC}[18~");
                    break;
                case VTKey.F8:
                    Write($"{ESC}[19~");
                    break;
                case VTKey.F9:
                    Write($"{ESC}[20~");
                    break;
                case VTKey.F10:
                    Write($"{ESC}[21~");
                    break;
                case VTKey.F11:
                    Write($"{ESC}[23~");
                    break;
                case VTKey.F12:
                    Write($"{ESC}[24~");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
        }
    }
}