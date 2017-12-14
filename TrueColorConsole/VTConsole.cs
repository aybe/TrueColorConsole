using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace TrueColorConsole
{
    public static class VTConsole
    {
        // TODO https://docs.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences#span-idtabsspanspan-idtabsspanspan-idtabsspantabs
        // TODO https://docs.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences#span-iddesignatecharactersetspanspan-iddesignatecharactersetspanspan-iddesignatecharactersetspandesignate-character-set
        // TODO https://docs.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences#span-idscrollingmarginsspanspan-idscrollingmarginsspanspan-idscrollingmarginsspanscrolling-margins
        // TODO https://docs.microsoft.com/en-us/windows/console/console-virtual-terminal-sequences#span-idinputsequencesspanspan-idinputsequencesspanspan-idinputsequencesspaninput-sequences

        #region Private

        private const string BEL = "\x07";
        private const string ESC = "\x1b";

        #endregion

        #region Public

        public static bool IsSupported { get; }

        static VTConsole()
        {
            bool TryEnableInput(out ConsoleModeInput result)
            {
                result = 0;

                var handle = GetStdHandle(StdInputHandle);
                if (handle == InvalidHandleValue)
                    return false;

                if (!GetConsoleMode(handle, out result))
                    return false;

                var mode = result | ConsoleModeInput.EnableVirtualTerminalInput;

                return SetConsoleMode(handle, (uint) mode);
            }

            bool TryEnableOutput(out ConsoleModeOutput result)
            {
                result = 0;

                var handle = GetStdHandle(StdOutputHandle);
                if (handle == InvalidHandleValue)
                    return false;

                if (!GetConsoleMode(handle, out result))
                    return false;

                var mode = result |
                           ConsoleModeOutput.EnableVirtualTerminalProcessing |
                           ConsoleModeOutput.DisableNewlineAutoReturn;

                if (SetConsoleMode(handle, (uint) mode))
                    return true;

                mode = result | ConsoleModeOutput.EnableVirtualTerminalProcessing;

                return SetConsoleMode(handle, (uint) mode);
            }

            IsSupported = TryEnableInput(out _) && TryEnableOutput(out _);
        }

        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static ConsoleKeyInfo ReadKey(bool intercept = false)
        {
            return Console.ReadKey(intercept);
        }

        public static void Write(string text, Color? foreground = null, Color? background = null)
        {
            Format(foreground, background);
            Console.Write(text);
        }

        public static void Write(string text, Color? foreground = null, Color? background = null,
            params VTFormat[] formats)
        {
            Format(formats);
            Write(text, foreground, background);
        }

        public static void WriteLine()
        {
            Write(Environment.NewLine);
        }


        public static void WriteLine(string text, Color? foreground = null, Color? background = null)
        {
            Write(text, foreground, background);
            WriteLine();
        }

        public static void WriteLine(string text, Color? foreground = null, Color? background = null,
            params VTFormat[] formats)
        {
            Write(text, foreground, background, formats);
            WriteLine();
        }

        #endregion

        #region Cursor

        /// <summary>
        ///     Cursor moves to Nth position horizontally in the current line.
        /// </summary>
        /// <param name="row"></param>
        public static void CursorAbsoluteHorizontal(int row)
        {
            if (row < 0 || row > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(row));

            Write($"{ESC}[{row}G");
        }

        /// <summary>
        ///     Cursor moves to the Nth position vertically in the current column.
        /// </summary>
        /// <param name="column"></param>
        public static void CursorAbsoluteVertical(int column)
        {
            if (column < 0 || column > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(column));

            Write($"{ESC}[{column}d");
        }

        /// <summary>
        ///     Performs a restore cursor option like <see cref="CursorPositionRestore" />.
        /// </summary>
        public static void CursorAnsiRestore()
        {
            Write($"{ESC}[u");
        }

        /// <summary>
        ///     Performs a save cursor operation like <see cref="CursorPositionSave" />.
        /// </summary>
        public static void CursorAnsiSave()
        {
            Write($"{ESC}[s");
        }

        /// <summary>
        ///     Cursor down to beginning of Nth line in the viewport.
        /// </summary>
        /// <param name="line"></param>
        public static void CursorLineDown(int line)
        {
            if (line < 0 || line > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(line));

            Write($"{ESC}[{line}E");
        }

        /// <summary>
        ///     Cursor up to beginning of Nth line in the viewport.
        /// </summary>
        /// <param name="line"></param>
        public static void CursorLineUp(int line)
        {
            if (line < 0 || line > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(line));

            Write($"{ESC}[{line}F");
        }

        /// <summary>
        ///     Cursor up by N rows.
        /// </summary>
        /// <param name="rows"></param>
        public static void CursorMoveUp(int rows = 1)
        {
            if (rows < 0 || rows > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rows));

            Write($"{ESC}[{rows}A");
        }

        /// <summary>
        ///     Cursor down by N rows.
        /// </summary>
        /// <param name="rows"></param>
        public static void CursorMoveDown(int rows = 1)
        {
            if (rows < 0 || rows > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(rows));

            Write($"{ESC}[{rows}B");
        }

        /// <summary>
        ///     Cursor right by N columns.
        /// </summary>
        /// <param name="columns"></param>
        public static void CursorMoveRight(int columns = 1)
        {
            if (columns < 0 || columns > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(columns));

            Write($"{ESC}[{columns}C");
        }

        /// <summary>
        ///     Cursor left by N columns.
        /// </summary>
        /// <param name="columns"></param>
        public static void CursorMoveLeft(int columns = 1)
        {
            if (columns < 0 || columns > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(columns));

            Write($"{ESC}[{columns}D");
        }

        /// <summary>
        ///     Cursor move to coordinates within the viewport.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        public static void CursorPosition(int column, int row)
        {
            if (row < 0 || row > short.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(row));

            Write($"{ESC}[{column};{row}H");
        }

        /// <summary>
        ///     Restore cursor position in memory (see Remarks).
        /// </summary>
        /// <remarks>
        ///     There will be no value saved in memory until the first use of the save command. The only way to access the saved
        ///     value is with the restore command.
        /// </remarks>
        public static void CursorPositionRestore()
        {
            Write($"{ESC}8");
        }

        /// <summary>
        ///     Save cursor position in memory (see Remarks).
        /// </summary>
        /// <remarks>
        ///     There will be no value saved in memory until the first use of the save command. The only way to access the saved
        ///     value is with the restore command.
        /// </remarks>
        public static void CursorPositionSave()
        {
            Write($"{ESC}7");
        }

        /// <summary>
        ///     Reverse Index – Performs the reverse operation of \n, moves cursor up one line, maintains horizontal position,
        ///     scrolls buffer if necessary (see Remarks).
        /// </summary>
        /// <remarks>
        ///     If there are scroll margins set, RI inside the margins will scroll only the contents of the margins, and leave the
        ///     viewport unchanged. TODO (See Scrolling Margins)
        /// </remarks>
        public static void CursorReverseIndex()
        {
            Write($"{ESC}M");
        }

        /// <summary>
        ///     Sets cursor blinking.
        /// </summary>
        /// <param name="enabled"></param>
        public static void CursorSetBlinking(bool enabled)
        {
            Write($"{ESC}[?12{(enabled ? "h" : "l")}");
        }

        /// <summary>
        ///     Sets cursor visibility.
        /// </summary>
        /// <param name="enabled"></param>
        public static void CursorSetVisibility(bool enabled)
        {
            Write($"{ESC}[?25{(enabled ? "h" : "l")}");
        }

        #endregion

        #region Viewport

        /// <summary>
        ///     Scroll text up by N lines. Also known as pan down, new lines fill in from the bottom of the screen.
        /// </summary>
        /// <param name="lines"></param>
        public static void ScrollUp(int lines = 1)
        {
            if (lines < 0)
                throw new ArgumentOutOfRangeException(nameof(lines));

            Write($"{ESC}[{lines}S");
        }

        /// <summary>
        ///     Scroll down by N lines. Also known as pan up, new lines fill in from the top of the screen.
        /// </summary>
        /// <param name="lines"></param>
        public static void ScrollDown(int lines = 1)
        {
            if (lines < 0)
                throw new ArgumentOutOfRangeException(nameof(lines));

            Write($"{ESC}[{lines}T");
        }

        #endregion

        #region Text

        /// <summary>
        ///     Insert N spaces at the current cursor position, shifting all existing text to the right. Text exiting the screen to
        ///     the right is removed.
        /// </summary>
        /// <param name="count"></param>
        public static void CharacterInsert(int count = 1)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Write($"{ESC}{count}@");
        }

        /// <summary>
        ///     Delete N characters at the current cursor position, shifting in space characters from the right edge of the screen.
        /// </summary>
        /// <param name="count"></param>
        public static void CharacterDelete(int count = 1)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Write($"{ESC}{count}P");
        }

        /// <summary>
        ///     Erase N characters from the current cursor position by overwriting them with a space character.
        /// </summary>
        /// <param name="count"></param>
        public static void CharacterErase(int count = 1)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Write($"{ESC}{count}X");
        }


        /// <summary>
        ///     Inserts N lines into the buffer at the cursor position. The line the cursor is on, and lines below it, will be
        ///     shifted downwards.
        /// </summary>
        /// <param name="count"></param>
        public static void LineInsert(int count = 1)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Write($"{ESC}{count}L");
        }

        /// <summary>
        ///     Deletes N lines from the buffer, starting with the row the cursor is on.
        /// </summary>
        /// <param name="count"></param>
        public static void LineDelete(int count = 1)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Write($"{ESC}{count}M");
        }

        /// <summary>
        ///     Replace all text in the current viewport/screen with space characters.
        /// </summary>
        /// <param name="eraseMode"></param>
        public static void EraseInDisplay(VTEraseMode eraseMode = VTEraseMode.FromCursorToEnd)
        {
            Write($"{ESC}[{(int) eraseMode}J");
        }

        /// <summary>
        ///     Replace all text on the line with the cursor with space characters.
        /// </summary>
        /// <param name="eraseMode"></param>
        public static void EraseInLine(VTEraseMode eraseMode = VTEraseMode.FromCursorToEnd)
        {
            Write($"{ESC}[{(int) eraseMode}K");
        }

        /// <summary>
        ///     Applies general formatting.
        /// </summary>
        /// <param name="formats"></param>
        public static void Format(params VTFormat[] formats)
        {
            if (formats == null || !formats.Any())
                return;

            var text = $"{ESC}[{string.Join(";", formats.Take(16).Select(s => (int) s))}m";
            Write(text);
        }

        /// <summary>
        ///     Applies color formatting.
        /// </summary>
        /// <param name="foreground">
        ///     If <c>null</c>, foreground color will be left unchanged.
        /// </param>
        /// <param name="background">
        ///     If <c>null</c>, background color will be left unchanged.
        /// </param>
        public static void Format(Color? foreground = null, Color? background = null)
        {
            void Format(Color color, int layer)
            {
                Write($"{ESC}[{layer};2;{color.R};{color.G};{color.B}m");
            }

            if (foreground.HasValue)
                Format(foreground.Value, 38);

            if (background.HasValue)
                Format(background.Value, 48);
        }

        #endregion

        #region Mode changes

        /// <summary>
        ///     Keypad keys will emit their Application Mode sequences.
        /// </summary>
        public static void SwitchKeypadApplicationMode()
        {
            Write($"{ESC}=");
        }

        /// <summary>
        ///     Keypad keys will emit their Numeric Mode sequences.
        /// </summary>
        public static void SwitchKeypadNumericMode()
        {
            Write($"{ESC}>");
        }

        /// <summary>
        ///     Keypad keys will emit their Application Mode sequences.
        /// </summary>
        public static void SwitchCursorKeysApplicationMode()
        {
            Write($"{ESC}[?1h");
        }

        /// <summary>
        ///     Keypad keys will emit their Numeric Mode sequences.
        /// </summary>
        public static void SwitchCursorKeysNumericMode()
        {
            Write($"{ESC}[?1l");
        }

        #endregion

        #region Query state

        /// <summary>
        ///     Emit the cursor position as: ESC [ &lt;r&gt; ; &lt;c&gt; R where &lt;r&gt; = cursor row and &lt;c&gt; = cursor
        ///     column.
        /// </summary>
        public static void QueryCursorPosition()
        {
            Write($"{ESC}[6n");
        }

        /// <summary>
        ///     Report the terminal identity. Will emit “\x1b[?1;0c”, indicating "VT101 with No Options".
        /// </summary>
        public static void QueryDeviceAttributes()
        {
            Write($"{ESC}[0c");
        }

        #endregion

        #region Window title

        /// <summary>
        ///     Sets the console window’s title.
        /// </summary>
        /// <param name="text"></param>
        public static void SetWindowTitleAndIcon([NotNull] string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (text.Length > 255)
                throw new ArgumentOutOfRangeException(nameof(text));

            Write($"{ESC}]0;{text}{BEL}");
        }

        /// <summary>
        ///     Sets the console window’s title.
        /// </summary>
        /// <param name="text"></param>
        public static void SetWindowTitle(string text)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (text.Length > 255)
                throw new ArgumentOutOfRangeException(nameof(text));

            Write($"{ESC}]2;{text}{BEL}");
        }

        #endregion

        #region Alternate screen buffer

        /// <summary>
        ///     Switches to a new alternate screen buffer.
        /// </summary>
        public static void SwitchScreenBufferAlternate()
        {
            Write($"{ESC}[?1049h");
        }

        /// <summary>
        ///     Switches to the main buffer.
        /// </summary>
        public static void SwitchScreenBufferMain()
        {
            Write($"{ESC}[?1049l");
        }

        #endregion

        #region Window width, soft reset

        /// <summary>
        ///     Sets the console width to 132 columns wide.
        /// </summary>
        public static void SetConsoleWidth132()
        {
            Write($"{ESC}[?3h");
        }

        /// <summary>
        ///     Sets the console width to 80 columns wide.
        /// </summary>
        public static void SetConsoleWidth80()
        {
            Write($"{ESC}[?3l");
        }

        /// <summary>
        ///     Reset certain terminal settings to their defaults.
        /// </summary>
        public static void SoftReset()
        {
            Write($"{ESC}[!p");
        }

        #endregion

        #region Native methods

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

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(uint nStdHandle);

        private const uint StdOutputHandle = unchecked((uint) -11);
        private const uint StdInputHandle = unchecked((uint) -10);
        private static readonly IntPtr InvalidHandleValue = new IntPtr(-1);

        #endregion
    }
}