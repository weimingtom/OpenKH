using System.Drawing;
using System.Drawing.Drawing2D;

namespace khiiMapv
{
    internal class STim
    {
        public int maxu;
        public int maxv;
        public int minu;
        public int minv;
        public Bitmap pic;
        public TCC tcc;
        public TFX tfx;
        public WM wms;
        public WM wmt;
        /// <summary>
        /// Function that will copy the Bitmap picture onto the var pic
        /// </summary>
        /// <param name="pic">Bitmap pic</param>
        public STim(Bitmap pic)
        {
            this.pic = pic;
        }
        /// <summary>
        /// Function that will return the minu var
        /// </summary>
        public int UMSK
        {
            get { return minu; }
        }
        /// <summary>
        /// Function that will return the minv var
        /// </summary>
        public int VMSK
        {
            get { return minv; }
        }
        /// <summary>
        /// Function that will return the maxu var
        /// </summary>
        public int UFIX
        {
            get { return maxu; }
        }
        /// <summary>
        /// Function that will return the maxv var
        /// </summary>
        public int VFIX
        {
            get { return maxv; }
        }
        /// <summary>
        /// Function that will generate a Bitmap picture
        /// </summary>
        /// <returns>The picture</returns>
        public Bitmap Generate()
        {
            if (wms == WM.RRepeat && wmt == WM.RRepeat)
            {
                var bitmap = new Bitmap(UMSK + 1, VMSK + 1);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.DrawImage(pic, new[]
                    {
                        new Point(0, 0),
                        new Point(bitmap.Width, 0),
                        new Point(0, bitmap.Height)
                    }, new Rectangle(UFIX, VFIX, UMSK + 1, VMSK + 1), GraphicsUnit.Pixel);
                }
                return bitmap;
            }
            if (wms == WM.RClamp && wmt == WM.RClamp)
            {
                var bitmap2 = new Bitmap(pic);
                using (Graphics graphics2 = Graphics.FromImage(bitmap2))
                {
                    int num = 0;
                    int num2 = 0;
                    int num3 = minu;
                    int num4 = minv;
                    int num5 = maxu;
                    int num6 = maxv;
                    int width = bitmap2.Width;
                    int height = bitmap2.Height;
                    graphics2.CompositingMode = CompositingMode.SourceCopy;
                    graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num3, num4)),
                        Rectangle.FromLTRB(num, num2, num3, num4));
                    graphics2.DrawImage(bitmap2, new[]
                    {
                        new Point(num3, num2),
                        new Point(num5, num2),
                        new Point(num3, num4)
                    }, Rectangle.FromLTRB(num3, num4, num5, num4 + 1), GraphicsUnit.Pixel);
                    graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num5, num4)),
                        Rectangle.FromLTRB(num5, num2, width, num4));
                    graphics2.DrawImage(bitmap2, new[]
                    {
                        new Point(num, num4),
                        new Point(num3, num4),
                        new Point(num, num6)
                    }, Rectangle.FromLTRB(num3, num4, num3 + 1, num6), GraphicsUnit.Pixel);
                    graphics2.DrawImage(bitmap2, new[]
                    {
                        new Point(num5, num4),
                        new Point(width, num4),
                        new Point(num5, num6)
                    }, Rectangle.FromLTRB(num5 - 1, num4, num5, num6), GraphicsUnit.Pixel);
                    graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num3, num6)),
                        Rectangle.FromLTRB(num, num6, num3, height));
                    graphics2.DrawImage(bitmap2, new[]
                    {
                        new Point(num3, num6),
                        new Point(num5, num6),
                        new Point(num3, height)
                    }, Rectangle.FromLTRB(num3, num6 - 1, num5, num6), GraphicsUnit.Pixel);
                    graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num5, num6)),
                        Rectangle.FromLTRB(num5, num6, width, height));
                }
                return bitmap2;
            }
            WM arg_358_0 = wms;
            WM arg_357_0 = wmt;
            return pic;
        }
    }
}