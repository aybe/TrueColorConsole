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
                    WriteConcat(ESC, s, "A");
                    break;
                case VTCursorKey.DownArrow:
                    WriteConcat(ESC, s, "B");
                    break;
                case VTCursorKey.RightArrow:
                    WriteConcat(ESC, s, "C");
                    break;
                case VTCursorKey.LeftArrow:
                    WriteConcat(ESC, s, "D");
                    break;
                case VTCursorKey.Home:
                    WriteConcat(ESC, s, "H");
                    break;
                case VTCursorKey.End:
                    WriteConcat(ESC, s, "F");
                    break;
                case VTCursorKey.CtrlUpArrow:
                    WriteConcat(ESC, "[1;5A");
                    break;
                case VTCursorKey.CtrlDownArrow:
                    WriteConcat(ESC, "[1;5B");
                    break;
                case VTCursorKey.CtrlRightArrow:
                    WriteConcat(ESC, "[1;5C");
                    break;
                case VTCursorKey.CtrlLeftArrow:
                    WriteConcat(ESC, "[1;5D");
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
                    WriteConcat(DEL);
                    break;
                case VTKey.Pause:
                    WriteConcat(SUB);
                    break;
                case VTKey.Escape:
                    WriteConcat(ESC);
                    break;
                case VTKey.Insert:
                    WriteConcat(ESC, "[2~");
                    break;
                case VTKey.Delete:
                    WriteConcat(ESC, "[3~");
                    break;
                case VTKey.PageUp:
                    WriteConcat(ESC, "[5~");
                    break;
                case VTKey.PageDown:
                    WriteConcat(ESC, "[6~");
                    break;
                case VTKey.F1:
                    WriteConcat(ESC, "OP");
                    break;
                case VTKey.F2:
                    WriteConcat(ESC, "OQ");
                    break;
                case VTKey.F3:
                    WriteConcat(ESC, "OR");
                    break;
                case VTKey.F4:
                    WriteConcat(ESC, "OS");
                    break;
                case VTKey.F5:
                    WriteConcat(ESC, "[15~");
                    break;
                case VTKey.F6:
                    WriteConcat(ESC, "[17~");
                    break;
                case VTKey.F7:
                    WriteConcat(ESC, "[18~");
                    break;
                case VTKey.F8:
                    WriteConcat(ESC, "[19~");
                    break;
                case VTKey.F9:
                    WriteConcat(ESC, "[20~");
                    break;
                case VTKey.F10:
                    WriteConcat(ESC, "[21~");
                    break;
                case VTKey.F11:
                    WriteConcat(ESC, "[23~");
                    break;
                case VTKey.F12:
                    WriteConcat(ESC, "[24~");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
        }
    }
}