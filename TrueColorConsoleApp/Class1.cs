using System;
using System.Drawing;
using TrueColorConsole;

namespace TrueColorConsoleApp
{
    internal class Class1
    {
        private static readonly int XSIZ = 64;
        private static readonly int YSIZ = 32;

        private static readonly int TABLEX = XSIZ * 2;
        private static readonly int TABLEY = YSIZ * 2;

        public static readonly byte[] screen = new byte[XSIZ * YSIZ];
        private static readonly byte[] table = new byte[TABLEX * TABLEY];

        public static int main()
        {
            var random = new Random();

            var red = new int[256];
            var green = new int[256];
            var blue = new int[256];
            var alpha = new int[256];
            var r = new double[3];
            var R = new double[6];
            int i, x, y, frame;


            /* Fill various tables */
            for (i = 0; i < 256; i++)
                red[i] = green[i] = blue[i] = alpha[i] = 0;

            for (i = 0; i < 3; i++)
                r[i] = (double) random.Next(1, 1000) / 30000 * Math.PI;

            for (i = 0; i < 6; i++)
                R[i] = (double) random.Next(1, 1000) / 5000;

            for (y = 0; y < TABLEY; y++)
            for (x = 0; x < TABLEX; x++)
            {
                var tmp = ((x - TABLEX / 2) * (x - TABLEX / 2)
                           + (y - TABLEX / 2) * (y - TABLEX / 2))
                          * (Math.PI / (TABLEX * TABLEX + TABLEY * TABLEY));

                table[x + y * TABLEX] = (byte) ((1.0 + Math.Sin(12.0 * Math.Sqrt(tmp))) * 256 / 6);
            }

            Console.WindowWidth = XSIZ;
            Console.WindowHeight = YSIZ;


            /* Main loop */
            for (frame = 0; /*!caca_get_event(CACA_EVENT_KEY_PRESS)*/; frame++)
            {
                for (i = 0; i < 256; i++)
                {
                    var z = (double) i / 256 * 6 * Math.PI;

                    red[i] = (int) ((1.0 + Math.Cos(z + r[0] * frame)) / 2 * 0xfff);
                    green[i] = (int) ((1.0 + Math.Sin(z + r[1] * frame)) / 2 * 0xfff);
                    blue[i] = (int) ((1.0 + Math.Cos(z + r[2] * frame)) / 2 * 0xfff);
                }

                do_plasma(screen,
                    (1.0 + Math.Sin(frame * R[0])) / 2,
                    (1.0 + Math.Sin(frame * R[1])) / 2,
                    (1.0 + Math.Sin(frame * R[2])) / 2,
                    (1.0 + Math.Sin(frame * R[3])) / 2,
                    (1.0 + Math.Sin(frame * R[4])) / 2,
                    (1.0 + Math.Sin(frame * R[5])) / 2);

                Console.Clear();
                VTConsole.CursorPosition(0,0);
                foreach (var b in screen)
                {
                    var color =  Color.FromArgb(red[b]/16, green[b]/16, blue[b]/16);
                    VTConsole.Write("0",background:color);
                }
            }
            

            return 0;
        }

        private static void do_plasma(byte[] pixels, double x_1, double y_1,
            double x_2, double y_2, double x_3, double y_3)
        {
            uint X1 = (uint) (x_1 * (TABLEX / 2)),
                Y1 = (uint) (y_1 * (TABLEY / 2)),
                X2 = (uint) (x_2 * (TABLEX / 2)),
                Y2 = (uint) (y_2 * (TABLEY / 2)),
                X3 = (uint) (x_3 * (TABLEX / 2)),
                Y3 = (uint) (y_3 * (TABLEY / 2));
            uint y;
            int t1 = (int) (X1 + Y1 * TABLEX),
                t2 = (int) (X2 + Y2 * TABLEX),
                t3 = (int) (X3 + Y3 * TABLEX);

            for (y = 0; y < YSIZ; y++)
            {
                uint x;
                var tmp = (int) (y * YSIZ);
                int ty = (int) (y * TABLEX), tmax = ty + XSIZ;
                for (x = 0; ty < tmax; ty++, tmp++)
                    pixels[tmp] = (byte) (table[t1 + ty] + table[t2 + ty] + table[t3 + ty]);
            }
        }
    }
}