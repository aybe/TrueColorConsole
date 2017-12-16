using System;
using System.Text;
using System.Threading;
using TrueColorConsole;

namespace ConsoleApp1
{
    internal static class Program
    {
        private static void Main1()
        {
            if (!VTConsole.IsSupported)
                throw new NotSupportedException();

            VTConsole.Enable();
            PlasmaDemo();
        }

        private static void PlasmaDemo()
        {
            var plasma = new Plasma(256, 256);
            var width = 80;
            var height = 40;

            Console.SetWindowSize(width, height);
            Console.SetBufferSize(width, height);
            Console.SetWindowSize(width, height); // yes, twice
            Console.Title = "Plasma !";
            Console.CursorVisible = false;

            var builder = new StringBuilder(width * height * 22);

            for (var frame = 0; ; frame++)
            {
                plasma.PlasmaFrame(frame);
                builder.Clear();
                  Thread.Sleep((int)(1.0 / 20 * 1000));
                for (var i = 0; i < width * height; i++)
                {
                    var x1 = i % width;
                    var y1 = i / width;
                    var i1 = y1 * plasma.SizeX + x1;
                    var b = plasma.Screen[i1];

                    var cr = plasma.ColR[b] >> 4;
                    var cg = plasma.ColG[b] >> 4;
                    var cb = plasma.ColB[b] >> 4;
                    var str = VTConsole.GetColorBackgroundString(cr, cg, cb);
                    builder.Append(str);
                    builder.Append(' ');
                }
                var bytes = Encoding.ASCII.GetBytes(builder.ToString());
                VTConsole.WriteFast(bytes);
            }
        }
    }
}