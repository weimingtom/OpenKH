using khiiMapv.Pax;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using vwBinTex2;
namespace khiiMapv
{
	public class ParsePAX
	{
		private class BUt
		{
			public static Bitmap Make8(byte[] pic, byte[] pal, int cx, int cy)
			{
				Bitmap bitmap = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
				try
				{
					Marshal.Copy(pic, 0, bitmapData.Scan0, Math.Min(bitmapData.Stride * bitmapData.Height, pic.Length));
				}
				finally
				{
					bitmap.UnlockBits(bitmapData);
				}
				ColorPalette palette = bitmap.Palette;
				for (int i = 0; i < 256; i++)
				{
					int num = i;
					int num2 = KHcv8pal_swap34.repl(i);
					palette.Entries[num2] = Color.FromArgb(Math.Min(255, (int)(pal[4 * num + 3] * 2)), (int)pal[4 * num], (int)pal[4 * num + 1], (int)pal[4 * num + 2]);
				}
				bitmap.Palette = palette;
				return bitmap;
			}
		}
		public static PicPAX ReadPAX(MemoryStream si)
		{
			si.Position = 0L;
			BinaryReader binaryReader = new BinaryReader(si);
			if (si.ReadByte() != 80)
			{
				throw new NotSupportedException("!PAX");
			}
			if (si.ReadByte() != 65)
			{
				throw new NotSupportedException("!PAX");
			}
			if (si.ReadByte() != 88)
			{
				throw new NotSupportedException("!PAX");
			}
			if (si.ReadByte() != 95)
			{
				throw new NotSupportedException("!PAX");
			}
			si.Position = 12L;
			int num = binaryReader.ReadInt32();
			si.Position = (long)num;
			int num2 = binaryReader.ReadInt32();
			if (num2 != 130)
			{
				throw new NotSupportedException("!82");
			}
			si.Position = (long)(num + 12);
			int num3 = binaryReader.ReadInt32();
			PicPAX picPAX = new PicPAX();
			for (int i = 0; i < num3; i++)
			{
				si.Position = (long)(num + 16 + 32 * i);
				int num4 = num + binaryReader.ReadInt32();
				si.Position = (long)num4;
				int num5 = binaryReader.ReadInt32();
				if (num5 != 150)
				{
					throw new NotSupportedException("!96");
				}
				int num6 = binaryReader.ReadInt32();
				List<int> list = new List<int>();
				for (int j = 0; j < num6; j++)
				{
					list.Add(num4 + binaryReader.ReadInt32());
				}
				int num7 = binaryReader.ReadInt32();
				List<int> list2 = new List<int>();
				for (int k = 0; k < num7; k++)
				{
					list2.Add(num4 + binaryReader.ReadInt32());
				}
				int num8 = binaryReader.ReadInt32();
				List<int> list3 = new List<int>();
				for (int l = 0; l < num8; l++)
				{
					list3.Add(num4 + binaryReader.ReadInt32());
				}
				int num9 = binaryReader.ReadInt32();
				List<int> list4 = new List<int>();
				for (int m = 0; m < num9; m++)
				{
					list4.Add(num4 + binaryReader.ReadInt32());
				}
				int num10 = binaryReader.ReadInt32();
				List<int> list5 = new List<int>();
				for (int n = 0; n < num10; n++)
				{
					list5.Add(num4 + binaryReader.ReadInt32());
				}
				R r = new R();
				picPAX.alr.Add(r);
				for (int num11 = 0; num11 < list2.Count; num11++)
				{
					int num12 = list2[num11];
					si.Position = (long)num12;
					binaryReader.ReadInt32();
					binaryReader.ReadInt16();
					int num13 = (int)binaryReader.ReadInt16();
					binaryReader.ReadInt32();
					int num14 = (int)binaryReader.ReadInt16();
					int num15 = (int)binaryReader.ReadInt16();
					if (num13 == 19)
					{
						si.Position = (long)(num12 + 32);
						byte[] pic = binaryReader.ReadBytes(num14 * num15);
						byte[] pal = binaryReader.ReadBytes(1024);
						r.pics.Add(ParsePAX.BUt.Make8(pic, pal, num14, num15));
					}
				}
				for (int num16 = 0; num16 < list3.Count; num16++)
				{
					int num17 = list3[num16];
					si.Position = (long)(num17 + 20);
					St3 st = new St3();
					r.als3.Add(st);
					st.cnt1 = (int)binaryReader.ReadInt16();
					st.cnt2 = (int)binaryReader.ReadInt16();
					for (int num18 = 0; num18 < st.cnt1; num18++)
					{
						si.Position = (long)(num17 + 32 + 8 * num18);
						int num19 = (int)binaryReader.ReadUInt16();
						St3r st3r = new St3r();
						st.al3r.Add(st3r);
						si.Position = (long)(num17 + 16 + num19);
						st3r.v0 = (int)binaryReader.ReadUInt16();
						st3r.v2 = (int)binaryReader.ReadUInt16();
						st3r.v4 = (int)binaryReader.ReadUInt16();
						si.Position = (long)(num17 + 16 + num19 + 32);
						for (int num20 = 0; num20 < st3r.v2; num20++)
						{
							STXYC item = default(STXYC);
							STXYC item2 = default(STXYC);
							item.x = (int)binaryReader.ReadInt16();
							item.y = (int)binaryReader.ReadInt16();
							item2.x = (int)binaryReader.ReadInt16();
							item2.y = (int)binaryReader.ReadInt16();
							item.s = (int)binaryReader.ReadInt16();
							item.t = (int)binaryReader.ReadInt16();
							item2.s = (int)binaryReader.ReadInt16();
							item2.t = (int)binaryReader.ReadInt16();
							st3r.alv.Add(item);
							st3r.alv.Add(item2);
						}
						for (int num21 = 0; num21 < st3r.v2; num21++)
						{
							STXYC value = st3r.alv[num21];
							int red = (int)binaryReader.ReadByte();
							int green = (int)binaryReader.ReadByte();
							int blue = (int)binaryReader.ReadByte();
							int alpha = (int)binaryReader.ReadByte();
							value.clr = Color.FromArgb(alpha, red, green, blue);
							st3r.alv[num21] = value;
							binaryReader.ReadInt32();
						}
					}
				}
			}
			return picPAX;
		}
	}
}
