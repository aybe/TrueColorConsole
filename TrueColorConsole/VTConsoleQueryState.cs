using JetBrains.Annotations;

namespace TrueColorConsole
{
    public static partial class VTConsole
    {
        /// <summary>
        ///     Emit the cursor position as: ESC [ &lt;r&gt; ; &lt;c&gt; R where &lt;r&gt; = cursor row and &lt;c&gt; = cursor
        ///     column.
        /// </summary>
        [PublicAPI]
        public static void QueryCursorPosition()
        {
            WriteConcat(ESC, "[6n");
        }

        /// <summary>
        ///     Report the terminal identity. Will emit “\x1b[?1;0c”, indicating "VT101 with No Options".
        /// </summary>
        [PublicAPI]
        public static void QueryDeviceAttributes()
        {
            WriteConcat(ESC, "[0c");
        }
    }
}