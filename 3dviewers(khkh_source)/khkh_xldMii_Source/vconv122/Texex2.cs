namespace vconv122
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public class Texex2
    {
        public List<Patc> alp = new List<Patc>();
        public List<STim> alt = new List<STim>();

        public Bitmap GetPattex(int p, int pi)
        {
            if ((p < 0) || (this.alp.Count <= p))
            {
                return null;
            }
            Patc patc = this.alp[p];
            if ((pi < 0) || (patc.ycnt <= pi))
            {
                return null;
            }
            Bitmap tex = this.GetTex(patc.texi);
            if (tex == null)
            {
                return null;
            }
            Bitmap image = new Bitmap(tex.Width, tex.Height, PixelFormat.Format32bppArgb);
            using (Bitmap bitmap3 = new Bitmap(patc.pcx, patc.pcy, PixelFormat.Format8bppIndexed))
            {
                BitmapData bitmapdata = bitmap3.LockBits(new Rectangle(0, 0, patc.pcx, patc.pcy), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                try
                {
                    int num = (patc.pcx * patc.pcy) * pi;
                    for (int i = 0; i < patc.pcy; i++)
                    {
                        Marshal.Copy(patc.bits, num + (patc.pcx * i), new IntPtr(bitmapdata.Scan0.ToInt64() + (i * bitmapdata.Stride)), patc.pcx);
                    }
                }
                finally
                {
                    bitmap3.UnlockBits(bitmapdata);
                }
                bitmap3.Palette = tex.Palette;
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    graphics.Clear(Color.Empty);
                    graphics.DrawImageUnscaled(bitmap3, patc.px, patc.py);
                }
            }
            return image;
        }

        public Bitmap GetTex(int w)
        {
            if ((w >= 0) && (this.alt.Count > w))
            {
                return this.alt[w].pic;
            }
            return null;
        }

        public Bitmap GetTex2(int w, byte[] al)
        {
            Bitmap tex = this.GetTex(w);
            if (tex == null)
            {
                return tex;
            }
            Bitmap bitmap2 = (Bitmap) tex.Clone();
            for (int i = 0; (i < this.alp.Count) && (i < al.Length); i++)
            {
                Patc patc = this.alp[i];
                int num2 = al[i];
                if ((num2 < patc.ycnt) && (patc.texi == w))
                {
                    BitmapData bitmapdata = bitmap2.LockBits(new Rectangle(patc.px, patc.py, patc.pcx, patc.pcy), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                    try
                    {
                        int num3 = (patc.pcx * patc.pcy) * num2;
                        for (int j = 0; j < patc.pcy; j++)
                        {
                            Marshal.Copy(patc.bits, num3 + (patc.pcx * j), new IntPtr(bitmapdata.Scan0.ToInt64() + (j * bitmapdata.Stride)), patc.pcx);
                        }
                    }
                    finally
                    {
                        bitmap2.UnlockBits(bitmapdata);
                    }
                }
            }
            return bitmap2;
        }
    }
}

