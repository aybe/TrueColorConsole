using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    public partial class MainWindow
    {
        private WriteableBitmap _bitmap;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _bitmap = BitmapFactory.New(128, 128);
            Image.Source = _bitmap;
            CompositionTarget.Rendering += CompositionTarget_Rendering;
        }

        private void CompositionTarget_Rendering(object sender, EventArgs e)
        {
            using (var ctx = _bitmap.GetBitmapContext())
            {
                Draw(ctx);
            }
        }

        private unsafe void Draw(BitmapContext ctx)
        {
            var time = DateTime.Now.TimeOfDay.TotalSeconds;
            var pw = ctx.Width;
            var ph = ctx.Height;
            var dw = 1.0f / pw;
            var dh = 1.0f / ph;

            var length = ctx.Length;
            var pixels = ctx.Pixels;

            var funcs = new PlasmaFunc[]
            {
                Final
            };
            for (var i = 0; i < length; i++)
            {
                var x = i % pw * dw;
                var y = i / pw * dh;
                var v = funcs.Select(s => s(x, y, time)).Average();
                var b = (byte) (255 * (0.5 + 0.5 * v));
                var g = (byte) (255 * (0.5 + 0.5 * v));
                var r = (byte) (255 * (0.5 + 0.5 * v));
                var a = (byte) 255;

                var p = (a << 24) | (r << 16) | (g << 8) | b;
                *pixels = p;
                pixels++;
            }
        }

        private double Final(double x, double y, double time)
        {
            var v = 0.0d;
            v += Math.Sin(x * 10 + time);
            v += Math.Sin((y * 10 + time) / 2.0);
            v += Math.Sin((x * 10 + y * 10 + time) / 2.0);
            var cx = x + .5 * Math.Sin(time / 5.0);
            var cy = y + .5 * Math.Cos(time / 3.0);
            v += Math.Sin(Math.Sqrt(100 * (cx * cx + cy * cy) + 1) + time);
            v = v / 2.0;
            return v;
        }

        private double ZoomingRotatingBars(double x, double y, double time)
        {
            return Math.Sin(10 * (x * Math.Sin(time / 2) + y * Math.Cos(time / 3)) + time);
        }

        private double HScrollBar(double x, double y, double time)
        {
            return Math.Sin(y * 10 + time);
        }

        private double VScrollBar(double x, double y, double time)
        {
            return Math.Sin(x * 10 + time);
        }

        private delegate double PlasmaFunc(double x, double y, double time);
    }
}