namespace khiiMapv
{
    using System;
    using System.Drawing;

    internal class CUtil
    {
        public CUtil()
        {
            base..ctor();
            return;
        }

        public static unsafe Color Gamma(Color a, float gamma)
        {
            return Color.FromArgb(&a.A, Math.Min(0xff, (int) (Math.Pow(((double) &a.R) / 255.0, (double) gamma) * 255.0)), Math.Min(0xff, (int) (Math.Pow(((double) &a.G) / 255.0, (double) gamma) * 255.0)), Math.Min(0xff, (int) (Math.Pow(((double) &a.B) / 255.0, (double) gamma) * 255.0)));
        }
    }
}

