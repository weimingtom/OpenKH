namespace khiiMapv
{
    using System;
    using System.Drawing;

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

        public STim(Bitmap pic)
        {
            base..ctor();
            this.pic = pic;
            return;
        }

        public unsafe Bitmap Generate()
        {
            Bitmap bitmap;
            Graphics graphics;
            Bitmap bitmap2;
            Graphics graphics2;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            Point[] pointArray;
            Point[] pointArray2;
            Point[] pointArray3;
            Point[] pointArray4;
            Point[] pointArray5;
            if (this.wms != 3)
            {
                goto Label_00C1;
            }
            if (this.wmt != 3)
            {
                goto Label_00C1;
            }
            bitmap = new Bitmap(this.UMSK + 1, this.VMSK + 1);
            graphics = Graphics.FromImage(bitmap);
        Label_0035:
            try
            {
                pointArray = new Point[3];
                *(&(pointArray[0])) = new Point(0, 0);
                *(&(pointArray[1])) = new Point(bitmap.Width, 0);
                *(&(pointArray[2])) = new Point(0, bitmap.Height);
                graphics.DrawImage(this.pic, pointArray, new Rectangle(this.UFIX, this.VFIX, this.UMSK + 1, this.VMSK + 1), 2);
                goto Label_00BF;
            }
            finally
            {
            Label_00B5:
                if (graphics == null)
                {
                    goto Label_00BE;
                }
                graphics.Dispose();
            Label_00BE:;
            }
        Label_00BF:
            return bitmap;
        Label_00C1:
            if (this.wms != 2)
            {
                goto Label_034B;
            }
            if (this.wmt != 2)
            {
                goto Label_034B;
            }
            bitmap2 = new Bitmap(this.pic);
            graphics2 = Graphics.FromImage(bitmap2);
        Label_00EC:
            try
            {
                num = 0;
                num2 = 0;
                num3 = this.minu;
                num4 = this.minv;
                num5 = this.maxu;
                num6 = this.maxv;
                num7 = bitmap2.Width;
                num8 = bitmap2.Height;
                graphics2.CompositingMode = 1;
                graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num3, num4)), Rectangle.FromLTRB(num, num2, num3, num4));
                pointArray2 = new Point[3];
                *(&(pointArray2[0])) = new Point(num3, num2);
                *(&(pointArray2[1])) = new Point(num5, num2);
                *(&(pointArray2[2])) = new Point(num3, num4);
                graphics2.DrawImage(bitmap2, pointArray2, Rectangle.FromLTRB(num3, num4, num5, num4 + 1), 2);
                graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num5, num4)), Rectangle.FromLTRB(num5, num2, num7, num4));
                pointArray3 = new Point[3];
                *(&(pointArray3[0])) = new Point(num, num4);
                *(&(pointArray3[1])) = new Point(num3, num4);
                *(&(pointArray3[2])) = new Point(num, num6);
                graphics2.DrawImage(bitmap2, pointArray3, Rectangle.FromLTRB(num3, num4, num3 + 1, num6), 2);
                pointArray4 = new Point[3];
                *(&(pointArray4[0])) = new Point(num5, num4);
                *(&(pointArray4[1])) = new Point(num7, num4);
                *(&(pointArray4[2])) = new Point(num5, num6);
                graphics2.DrawImage(bitmap2, pointArray4, Rectangle.FromLTRB(num5 - 1, num4, num5, num6), 2);
                graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num3, num6)), Rectangle.FromLTRB(num, num6, num3, num8));
                pointArray5 = new Point[3];
                *(&(pointArray5[0])) = new Point(num3, num6);
                *(&(pointArray5[1])) = new Point(num5, num6);
                *(&(pointArray5[2])) = new Point(num3, num8);
                graphics2.DrawImage(bitmap2, pointArray5, Rectangle.FromLTRB(num3, num6 - 1, num5, num6), 2);
                graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num5, num6)), Rectangle.FromLTRB(num5, num6, num7, num8));
                goto Label_0349;
            }
            finally
            {
            Label_033F:
                if (graphics2 == null)
                {
                    goto Label_0348;
                }
                graphics2.Dispose();
            Label_0348:;
            }
        Label_0349:
            return bitmap2;
        Label_034B:
            WM wm1 = this.wmt;
            WM wm2 = this.wms;
            return this.pic;
        }

        public int UFIX
        {
            get
            {
                return this.maxu;
            }
        }

        public int UMSK
        {
            get
            {
                return this.minu;
            }
        }

        public int VFIX
        {
            get
            {
                return this.maxv;
            }
        }

        public int VMSK
        {
            get
            {
                return this.minv;
            }
        }
    }
}

