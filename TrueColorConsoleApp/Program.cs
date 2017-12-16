using System;
using System.Drawing;
using System.Threading;
using TrueColorConsole;

namespace TrueColorConsoleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            if (!VTConsole.IsSupported)
                throw new NotSupportedException();

            VTConsole.Enable();

            goto start;

            var width = 80;
            var height = 25;
            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.SetWindowSize(width, height);

            var cx = Console.WindowWidth;
            var cy = Console.WindowHeight;
            for (var y = 0; y < cy; y++)
            for (var x = 0; x < cx; x++)
            {
                var r = (int) ((float) x / cx * 255);
                var g = (int) ((float) y / cy * 255);
                var b = (int) (1.0f * 255);
                var value = $"{(y * cx + x) % 10}";
                VTConsole.Write(value, Color.Black, Color.FromArgb(r, g, b));
            }

            Sleep();

            for (var i = 0; i < cy / 4; i++)
            {
                Sleep(50);
                VTConsole.ScrollUp();
            }

            VTConsole.WriteLine("Disabling cursor blinking", Color.White, Color.Red);
            VTConsole.CursorSetBlinking(false);
            Sleep();

            VTConsole.WriteLine("Enabling cursor blinking", Color.White, Color.Green);
            VTConsole.CursorSetBlinking(true);
            Sleep();

            VTConsole.SetColorBackground(Color.White);
            VTConsole.WriteLine("Hiding cursor", Color.DeepPink);
            VTConsole.CursorSetVisibility(false);
            Sleep();

            VTConsole.WriteLine("Showing cursor", Color.DeepSkyBlue);
            VTConsole.CursorSetVisibility(true);
            Sleep();

            VTConsole.WriteLine();
            VTConsole.SetFormat(VTFormat.Underline, VTFormat.Negative);
            VTConsole.WriteLine("Press a key to exit !!!", Color.White, Color.Red);

            start:
            WriteLineSlow("abcd");
            VTConsole.CursorAbsoluteHorizontal(1);

            VTConsole.CharacterDelete();
            Console.ReadKey(true);
        }

        static void WriteLineSlow(string text)
        {
            WriteSlow(text + Environment.NewLine);
        }
        private static void WriteSlow(string text)
        {
            foreach (var c in text)
            {
                VTConsole.Write(c.ToString());
                Sleep(100);
            }
        }

        private static void Sleep(int millisecondsTimeout = 2000)
        {
            Thread.Sleep(millisecondsTimeout);
        }
    }
}