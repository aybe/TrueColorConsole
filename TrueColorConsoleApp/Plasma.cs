using System;

namespace ConsoleApp1
{
    public class Plasma
    {
        public readonly int[] ColB = new int[256];
        public readonly int[] ColG = new int[256];
        public readonly int[] ColR = new int[256];

        private readonly double[] Rand1 = new double[3];
        private readonly double[] Rand2 = new double[6];
        public readonly byte[] Screen;

        public readonly int SizeX;
        public readonly int SizeY;
        private readonly byte[] Table;
        private readonly int TableX;
        private readonly int TableY;

        public Plasma(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            TableX = SizeX * 2;
            TableY = SizeY * 2;
            Table = new byte[TableX * TableY];
            Screen = new byte[SizeX * SizeY];
            var random = new Random();

            for (var i = 0; i < 3; i++)
                Rand1[i] = (double) random.Next(1, 1000) / 30000 * Math.PI;

            for (var i = 0; i < 6; i++)
                Rand2[i] = (double) random.Next(1, 1000) / 5000;

            for (var y = 0; y < TableY; y++)
            for (var x = 0; x < TableX; x++)
            {
                var tmp = ((x - TableX / 2) * (x - TableX / 2)
                           +
                           (y - TableX / 2) * (y - TableX / 2))
                          *
                          (Math.PI / (TableX * TableX + TableY * TableY));

                Table[x + y * TableX] = (byte) ((1.0 + Math.Sin(12.0 * Math.Sqrt(tmp))) * 256 / 6);
            }
        }

        public void PlasmaFrame(int frame)
        {
            for (var i = 0; i < 256; i++)
            {
                var z = (double) i / 256 * 6 * Math.PI;
                ColR[i] = (int) ((1.0 + Math.Cos(z + Rand1[0] * frame)) / 2 * 4095);
                ColG[i] = (int) ((1.0 + Math.Sin(z + Rand1[1] * frame)) / 2 * 4095);
                ColB[i] = (int) ((1.0 + Math.Cos(z + Rand1[2] * frame)) / 2 * 4095);
            }

            PlasmaFrame(Screen,
                (1.0 + Math.Sin(frame * Rand2[0])) / 2,
                (1.0 + Math.Sin(frame * Rand2[1])) / 2,
                (1.0 + Math.Sin(frame * Rand2[2])) / 2,
                (1.0 + Math.Sin(frame * Rand2[3])) / 2,
                (1.0 + Math.Sin(frame * Rand2[4])) / 2,
                (1.0 + Math.Sin(frame * Rand2[5])) / 2);
        }

        private void PlasmaFrame(
            byte[] pixels, double dx1, double dy1, double dx2, double dy2, double dx3, double dy3)
        {
            var tablex = TableX;
            var tabley = TableY;
            var X1 = (int) (dx1 * (tablex / 2));
            var Y1 = (int) (dy1 * (tabley / 2));
            var X2 = (int) (dx2 * (tablex / 2));
            var Y2 = (int) (dy2 * (tabley / 2));
            var X3 = (int) (dx3 * (tablex / 2));
            var Y3 = (int) (dy3 * (tabley / 2));
            var t1 = X1 + Y1 * tablex;
            var t2 = X2 + Y2 * tablex;
            var t3 = X3 + Y3 * tablex;

            for (var y = 0; y < SizeY; y++)
            {
                var tmp = y * SizeY;
                int ty = y * tablex, tmax = ty + SizeX;
                for (var x = 0; ty < tmax; ty++, tmp++)
                {
                    var b = (byte) (Table[t1 + ty] + Table[t2 + ty] + Table[t3 + ty]);
                    pixels[tmp] = b;
                }
            }
        }
    }
}