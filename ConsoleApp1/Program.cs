using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using ClassLibrary1;

namespace ConsoleApp1
{
    internal class Program
    {
        private static readonly Dictionary<int, string> Colors = new Dictionary<int, string>();
        private static readonly StringBuilder Builder = new StringBuilder(20);

        private static readonly string[] Dictionary =
            Enumerable.Range(0, 256).Select(s => s.ToString()).ToArray();

        private static string GetColor(int r, int g, int b, int i)
        {
            return FormatBackgroundColor(r, g, b);
            if (!Colors.ContainsKey(i))
                Colors.Add(i, FormatForegroundColor(r, g, b));

            return Colors[i];
        }

        public static string FormatColor(int r, int g, int b, int layer)
        {
            return $"\x1b[{layer};2;{r};{g};{b}m";
        }

        public static string FormatBackgroundColor(int r, int g, int b)
        {
            return FormatColor(r, g, b, 48);
        }

        public static string FormatForegroundColor(int r, int g, int b)
        {
            Builder.Clear();
            Builder.Append("\x1B[38;2;");
            //            Builder.Append(r);
            Builder.Append(Dictionary[r]);

            Builder.Append(";");
            //Builder.Append(g);
            Builder.Append(Dictionary[g]);
            Builder.Append(";");
            // Builder.Append(b);
            Builder.Append(Dictionary[b]);

            Builder.Append("m");
            return Builder.ToString();
            return string.Concat("\x1b[38;2;", r, ";", g, ";", b, "m");
            return FormatColor(r, g, b, 38);
        }

        private static void Main(string[] args)
        {
            VTConsole.GetStdOutputHandle(out var handle);
            var aaa = new Plasma(64, 64);

            Console.BufferWidth = Console.WindowWidth = aaa.SizeX;
            Console.BufferHeight = Console.WindowHeight = aaa.SizeY;

            VTConsole.CursorSetVisibility(false);
            var builder = new StringBuilder(aaa.SizeX * aaa.SizeY * 22);

            for (var frame = 0;; frame++)
            {
                aaa.PlasmaFrame(frame);
                builder.Clear();
                foreach (var b in aaa.Screen)
                {
                    var cr = aaa.ColR[b] >> 4;
                    var cg = aaa.ColG[b] >> 4;
                    var cb = aaa.ColB[b] >> 4;
                    var pv = (0xFF << 24) | (cr << 16) | (cg << 08) | (cb << 00);
                    var str = GetColor(cr, cg, cb, pv);
                    builder.Append(str);
                    builder.Append(' ');
                }
                // Console.Write(builder);
                var bytes = Encoding.ASCII.GetBytes(builder.ToString());
                var writeConsole = WriteConsole(handle, bytes, (uint) bytes.Length, out var written, IntPtr.Zero);
            }
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WriteConsole(
            IntPtr hConsoleOutput,
            [MarshalAs(UnmanagedType.LPArray)] byte[] lpBuffer,
            uint lpNumberOfCharsToWrite,
            out uint lpNumberOfCharsToWritten,
            IntPtr lpReserved
        );
    }
}