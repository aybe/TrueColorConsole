using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace TrueColorConsole
{
    public static partial class VTConsole
    {
        #region Interop

        private const uint StdOutputHandle = unchecked((uint) -11);
        private const uint StdInputHandle = unchecked((uint) -10);
        private static readonly IntPtr InvalidHandleValue = new IntPtr(-1);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(uint nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        private static bool GetConsoleMode(IntPtr hConsoleHandle, out ConsoleModeOutput mode)
        {
            if (!GetConsoleMode(hConsoleHandle, out uint lpMode))
            {
                mode = 0;
                return false;
            }

            mode = (ConsoleModeOutput) lpMode;
            return true;
        }

        private static bool GetConsoleMode(IntPtr hConsoleHandle, out ConsoleModeInput mode)
        {
            if (!GetConsoleMode(hConsoleHandle, out uint lpMode))
            {
                mode = 0;
                return false;
            }

            mode = (ConsoleModeInput) lpMode;
            return true;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WriteConsole(
            IntPtr hConsoleOutput,
            [MarshalAs(UnmanagedType.LPArray)] byte[] lpBuffer,
            int lpNumberOfCharsToWrite,
            out int lpNumberOfCharsToWritten,
            IntPtr lpReserved
        );

        private static bool GetStdIn(out IntPtr handle)
        {
            handle = GetStdHandle(StdInputHandle);
            return handle != InvalidHandleValue;
        }

        private static bool GetStdOut(out IntPtr handle)
        {
            handle = GetStdHandle(StdOutputHandle);
            return handle != InvalidHandleValue;
        }

        #endregion

        #region General

        private const string ESC = "\x1b";
        private const string BEL = "\x07";
        private const string SUB = "\x1a";
        private const string DEL = "\x7f";

        private static readonly string[] BytesMap =
            Enumerable.Range(0, 256).Select(s => s.ToString()).ToArray();

        private static IntPtr _inHandle;
        private static ConsoleModeInput _inLast;
        private static IntPtr _outHandle;
        private static ConsoleModeOutput _outLast;

        /// <summary>
        ///     Gets if virtual terminal features are enabled.
        /// </summary>
        [PublicAPI]
        public static bool IsEnabled { get; private set; }

        /// <summary>
        ///     Gets if virtual terminal features are supported.
        /// </summary>
        [PublicAPI]
        public static bool IsSupported { get; } = Enable() && Disable();

        [PublicAPI]
        public static IntPtr StdIn => _inHandle;

        [PublicAPI]
        public static IntPtr StdOut => _outHandle;

        /// <summary>
        ///     Enables virtual terminal features.
        /// </summary>
        /// <returns>
        ///     <c>true</c> on success.
        /// </returns>
        [PublicAPI]
        public static bool Enable()
        {
            if (IsEnabled)
                return true;

            bool EnableInput()
            {
                if (!GetStdIn(out _inHandle))
                    return false;

                if (!GetConsoleMode(StdIn, out _inLast))
                    return false;

                var mode = _inLast | ConsoleModeInput.EnableVirtualTerminalInput;

                return SetConsoleMode(StdIn, (uint) mode);
            }

            bool EnableOutput()
            {
                if (!GetStdOut(out _outHandle))
                    return false;

                if (!GetConsoleMode(StdOut, out _outLast))
                    return false;

                var mode = _outLast | ConsoleModeOutput.EnableVirtualTerminalProcessing |
                           ConsoleModeOutput.DisableNewlineAutoReturn;

                if (SetConsoleMode(StdOut, (uint) mode))
                    return true;

                mode = _outLast | ConsoleModeOutput.EnableVirtualTerminalProcessing;

                return SetConsoleMode(StdOut, (uint) mode);
            }

            IsEnabled = EnableInput() && EnableOutput();

            return IsEnabled;
        }

        /// <summary>
        ///     Disables virtual terminal features.
        /// </summary>
        /// <returns>
        ///     <c>true</c> on success.
        /// </returns>
        [PublicAPI]
        public static bool Disable()
        {
            if (!IsEnabled)
                return false;

            bool DisableInput()
            {
                return GetStdIn(out var handle) && SetConsoleMode(handle, (uint) _inLast);
            }

            bool DisableOutput()
            {
                return GetStdOut(out var handle) && SetConsoleMode(handle, (uint) _outLast);
            }

            IsEnabled = !(DisableInput() && DisableOutput());

            return !IsEnabled;
        }

        /// <summary>
        ///     Gets the virtual terminal sequence for a background color.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [PublicAPI]
        public static string GetColorBackgroundString(int r, int g, int b)
        {
            return string.Concat(ESC, "[48;2;", BytesMap[r], ";", BytesMap[g], ";", BytesMap[b], "m");
        }

        /// <summary>
        ///     Gets the virtual terminal sequence for a foreground color.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [PublicAPI]
        public static string GetColorForegroundString(int r, int g, int b)
        {
            return string.Concat(ESC, "[38;2;", BytesMap[r], ";", BytesMap[g], ";", BytesMap[b], "m");
        }

        [PublicAPI]
        public static void SetColorBackground(Color color)
        {
            Console.Write(GetColorBackgroundString(color.R, color.G, color.B));
        }

        [PublicAPI]
        public static void SetColorForeground(Color color)
        {
            Console.Write(GetColorForegroundString(color.R, color.G, color.B));
        }

        [PublicAPI]
        public static void SetFormat(params VTFormat[] formats)
        {
            if (formats == null || !formats.Any())
                return;

            Console.Write($"{ESC}[{string.Join(";", formats.Take(16).Select(s => (int) s))}m");
        }

        /// <summary>
        ///     Fast writing using WriteConsole.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>
        ///     Number of characters written.
        /// </returns>
        [PublicAPI]
        public static int WriteFast(byte[] buffer)
        {
            WriteConsole(StdOut, buffer, buffer.Length, out var written, IntPtr.Zero);
            return written;
        }

        [PublicAPI]
        public static void WriteLine()
        {
            Console.WriteLine();
        }

        [PublicAPI]
        public static void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        [PublicAPI]
        public static void WriteLine(string value, Color foreground)
        {
            SetColorForeground(foreground);
            WriteLine(value);
        }

        [PublicAPI]
        public static void WriteLine(string value, Color foreground, Color background)
        {
            SetColorForeground(foreground);
            SetColorBackground(background);
            WriteLine(value);
        }

        [PublicAPI]
        public static void Write(string value)
        {
            Console.Write(value);
        }

        [PublicAPI]
        public static void Write(string value, Color foreground)
        {
            SetColorForeground(foreground);
            Console.Write(value);
        }

        [PublicAPI]
        public static void Write(string value, Color foreground, Color background)
        {
            SetColorForeground(foreground);
            SetColorBackground(background);
            Console.Write(value);
        }

        [PublicAPI]
        public static void WriteConcat(params object[] objects)
        {
            Console.Write(string.Concat(objects));
        }

        #endregion
    }
}