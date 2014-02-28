using System;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace khiiMapv
{
	internal class STim
	{
		public Bitmap pic;
		public TFX tfx;
		public TCC tcc;
		public WM wms;
		public WM wmt;
		public int minu;
		public int maxu;
		public int minv;
		public int maxv;
		public int UMSK
		{
			get
			{
				return this.minu;
			}
		}
		public int VMSK
		{
			get
			{
				return this.minv;
			}
		}
		public int UFIX
		{
			get
			{
				return this.maxu;
			}
		}
		public int VFIX
		{
			get
			{
				return this.maxv;
			}
		}
		public STim(Bitmap pic)
		{
			this.pic = pic;
		}
		public Bitmap Generate()
		{
			if (this.wms == WM.RRepeat && this.wmt == WM.RRepeat)
			{
				Bitmap bitmap = new Bitmap(this.UMSK + 1, this.VMSK + 1);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.DrawImage(this.pic, new Point[]
					{
						new Point(0, 0),
						new Point(bitmap.Width, 0),
						new Point(0, bitmap.Height)
					}, new Rectangle(this.UFIX, this.VFIX, this.UMSK + 1, this.VMSK + 1), GraphicsUnit.Pixel);
				}
				return bitmap;
			}
			if (this.wms == WM.RClamp && this.wmt == WM.RClamp)
			{
				Bitmap bitmap2 = new Bitmap(this.pic);
				using (Graphics graphics2 = Graphics.FromImage(bitmap2))
				{
					int num = 0;
					int num2 = 0;
					int num3 = this.minu;
					int num4 = this.minv;
					int num5 = this.maxu;
					int num6 = this.maxv;
					int width = bitmap2.Width;
					int height = bitmap2.Height;
					graphics2.CompositingMode = CompositingMode.SourceCopy;
					graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num3, num4)), Rectangle.FromLTRB(num, num2, num3, num4));
					graphics2.DrawImage(bitmap2, new Point[]
					{
						new Point(num3, num2),
						new Point(num5, num2),
						new Point(num3, num4)
					}, Rectangle.FromLTRB(num3, num4, num5, num4 + 1), GraphicsUnit.Pixel);
					graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num5, num4)), Rectangle.FromLTRB(num5, num2, width, num4));
					graphics2.DrawImage(bitmap2, new Point[]
					{
						new Point(num, num4),
						new Point(num3, num4),
						new Point(num, num6)
					}, Rectangle.FromLTRB(num3, num4, num3 + 1, num6), GraphicsUnit.Pixel);
					graphics2.DrawImage(bitmap2, new Point[]
					{
						new Point(num5, num4),
						new Point(width, num4),
						new Point(num5, num6)
					}, Rectangle.FromLTRB(num5 - 1, num4, num5, num6), GraphicsUnit.Pixel);
					graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num3, num6)), Rectangle.FromLTRB(num, num6, num3, height));
					graphics2.DrawImage(bitmap2, new Point[]
					{
						new Point(num3, num6),
						new Point(num5, num6),
						new Point(num3, height)
					}, Rectangle.FromLTRB(num3, num6 - 1, num5, num6), GraphicsUnit.Pixel);
					graphics2.FillRectangle(new SolidBrush(bitmap2.GetPixel(num5, num6)), Rectangle.FromLTRB(num5, num6, width, height));
				}
				return bitmap2;
			}
			WM arg_358_0 = this.wms;
			WM arg_357_0 = this.wmt;
			return this.pic;
		}
	}
}
