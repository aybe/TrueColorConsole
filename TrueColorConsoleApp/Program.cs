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
            Class1.main();
            if (!VTConsole.IsSupported)
                throw new NotSupportedException();

            var cx = Console.WindowWidth;
            var cy = Console.WindowHeight;
            for (var y = 0; y < cy; y++)
            for (var x = 0; x < cx; x++)
            {
                var r = (int) ((float) x / cx * 255);
                var g = (int) ((float) y / cy * 255);
                var b = (int) (1.0f * 255);
                VTConsole.Write($"{(y * cx + x) % 10}", Color.Black, Color.FromArgb(255, r, g, b));
            }

            Sleep(1500);

            for (var i = 0; i < cy / 4; i++)
            {
                Sleep(50);
                VTConsole.ScrollUp();
            }

            VTConsole.Format(Color.White);

            VTConsole.WriteLine("Disabling cursor blinking", background: Color.Red);
            VTConsole.CursorSetBlinking(false);
            Sleep();

            VTConsole.WriteLine("Enabling cursor blinking", background: Color.Green);
            VTConsole.CursorSetBlinking(true);
            Sleep();

            VTConsole.Format(background: Color.White);
            VTConsole.WriteLine("Hiding cursor", Color.DeepPink);
            VTConsole.CursorSetVisibility(false);
            Sleep();

            VTConsole.WriteLine("Showing cursor", Color.DeepSkyBlue);
            VTConsole.CursorSetVisibility(true);
            Sleep();

            VTConsole.WriteLine();
            VTConsole.WriteLine("Press a key to exit !!!", Color.White, Color.Red, VTFormat.UnderlineOn);
            VTConsole.ReadKey();
        }

        private static void Sleep(int millisecondsTimeout = 2000)
        {
            Thread.Sleep(millisecondsTimeout);
        }
    }
}