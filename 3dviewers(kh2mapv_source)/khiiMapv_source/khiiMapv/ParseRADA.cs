using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
namespace khiiMapv
{
	public class ParseRADA
	{
		private class BUt
		{
			public static Bitmap Make4(int cx, int cy, byte[] bin, byte[] palb)
			{
				Bitmap bitmap = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
				BitmapData bitmapData = bitmap.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);
				for (int i = 0; i < bin.Length; i++)
				{
					byte b = bin[i];
					b = (byte)((int)b << 4 | b >> 4);
					bin[i] = b;
				}
				try
				{
					int val = bitmapData.Stride * bitmapData.Height;
					Marshal.Copy(bin, 0, bitmapData.Scan0, Math.Min(bin.Length, val));
				}
				finally
				{
					bitmap.UnlockBits(bitmapData);
				}
				ColorPalette palette = bitmap.Palette;
				for (int j = 0; j < 16; j++)
				{
					int num = 4 * j;
					palette.Entries[j] = Color.FromArgb(Math.Min(255, (int)(2 * palb[num + 3])), (int)palb[num], (int)palb[num + 1], (int)palb[num + 2]);
				}
				bitmap.Palette = palette;
				return bitmap;
			}
		}
		private Stream si;
		private BinaryReader br;
		public Bitmap pic;
		public ParseRADA(Stream si)
		{
			this.si = si;
			this.br = new BinaryReader(si);
		}
		public void Parse()
		{
			BinaryReader binaryReader = new BinaryReader(this.si);
			this.si.Position = 4L;
			int num = (int)binaryReader.ReadUInt16();
			if (num != 4)
			{
				throw new NotSupportedException("@04 != 4");
			}
			this.si.Position = 36L;
			int num2 = (int)binaryReader.ReadUInt16();
			this.si.Position = 38L;
			int num3 = (int)binaryReader.ReadUInt16();
			this.si.Position = 64L;
			byte[] bin = binaryReader.ReadBytes(num2 * num3 / 2);
			byte[] palb = binaryReader.ReadBytes(64);
			this.pic = ParseRADA.BUt.Make4(num2, num3, bin, palb);
		}
	}
}
