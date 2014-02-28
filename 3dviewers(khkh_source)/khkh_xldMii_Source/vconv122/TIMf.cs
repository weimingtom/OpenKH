using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using vcBinTex4;
using vwBinTex2;
namespace vconv122
{
	public class TIMf
	{
		private class TexUtil
		{
			private class AcUt
			{
				public static byte GetA(byte a)
				{
					if (0 < a)
					{
						return 255;
					}
					return 0;
				}
			}
			public static STim Decode8(byte[] picbin, byte[] palbin, int tbw, int cx, int cy)
			{
				Bitmap bitmap = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
				tbw /= 2;
				byte[] array = Reform8.Decode8(picbin, tbw, Math.Max(1, picbin.Length / 8192 / tbw));
				BitmapData bitmapData = bitmap.LockBits(Rectangle.FromLTRB(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
				try
				{
					int val = bitmapData.Stride * bitmapData.Height;
					Marshal.Copy(array, 0, bitmapData.Scan0, Math.Min(array.Length, val));
				}
				finally
				{
					bitmap.UnlockBits(bitmapData);
				}
				ColorPalette palette = bitmap.Palette;
				int num = 0;
				byte[] array2 = new byte[1024];
				for (int i = 0; i < 256; i++)
				{
					int num2 = KHcv8pal.repl(i);
					Array.Copy(palbin, 4 * i, array2, 4 * num2, 4);
				}
				Array.Copy(array2, 0, palbin, 0, 1024);
				for (int j = 0; j < 256; j++)
				{
					palette.Entries[j] = TIMf.CUtil.NoGamma(Color.FromArgb((int)TIMf.TexUtil.AcUt.GetA(palbin[num + 4 * j + 3]) ^ (j & 1), Math.Min(255, (int)(palbin[num + 4 * j] + 1)), Math.Min(255, (int)(palbin[num + 4 * j + 1] + 1)), Math.Min(255, (int)(palbin[num + 4 * j + 2] + 1))), 0.5f);
				}
				bitmap.Palette = palette;
				return new STim(bitmap);
			}
			public static STim Decode4(byte[] picbin, byte[] palbin, int tbw, int cx, int cy)
			{
				Bitmap bitmap = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
				tbw /= 2;
				byte[] array = Reform4.Decode4(picbin, tbw, Math.Max(1, picbin.Length / 8192 / tbw));
				BitmapData bitmapData = bitmap.LockBits(Rectangle.FromLTRB(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);
				try
				{
					int val = bitmapData.Stride * bitmapData.Height;
					Marshal.Copy(array, 0, bitmapData.Scan0, Math.Min(array.Length, val));
				}
				finally
				{
					bitmap.UnlockBits(bitmapData);
				}
				ColorPalette palette = bitmap.Palette;
				int num = 0;
				for (int i = 0; i < 16; i++)
				{
					palette.Entries[i] = TIMf.CUtil.NoGamma(Color.FromArgb((int)TIMf.TexUtil.AcUt.GetA(palbin[num + 4 * i + 3]), (int)palbin[num + 4 * i], (int)palbin[num + 4 * i + 1], (int)palbin[num + 4 * i + 2]), 0.5f);
				}
				bitmap.Palette = palette;
				return new STim(bitmap);
			}
		}
		private class CUtil
		{
			public static Color Gamma(Color a, float gamma)
			{
				return Color.FromArgb((int)a.A, Math.Min(255, (int)(Math.Pow((double)a.R / 255.0, (double)gamma) * 255.0)), Math.Min(255, (int)(Math.Pow((double)a.G / 255.0, (double)gamma) * 255.0)), Math.Min(255, (int)(Math.Pow((double)a.B / 255.0, (double)gamma) * 255.0)));
			}
			public static Color NoGamma(Color a, float gamma)
			{
				return Color.FromArgb((int)a.A, (int)a.R, (int)a.G, (int)a.B);
			}
		}
		public const float γ = 0.5f;
		public static Texex2 Load(Stream fs)
		{
			Texex2 texex = new Texex2();
			BinaryReader binaryReader = new BinaryReader(fs);
			fs.Position = 8L;
			binaryReader.ReadInt32();
			int num = binaryReader.ReadInt32();
			int num2 = binaryReader.ReadInt32();
			int num3 = binaryReader.ReadInt32();
			int num4 = binaryReader.ReadInt32();
			fs.Position = (long)num2;
			byte[] array = binaryReader.ReadBytes(num);
			fs.Position = 28L;
			int num5 = binaryReader.ReadInt32();
			int num6 = binaryReader.ReadInt32();
			fs.Position = (long)num3;
			binaryReader.ReadBytes(num4 - num3);
			fs.Position = (long)num4;
			binaryReader.ReadBytes(num5 - num4);
			fs.Position = (long)num5;
			binaryReader.ReadBytes(num6 - num5);
			binaryReader.ReadBytes(Convert.ToInt32(fs.Length) - 4 - num6);
			byte[] array2 = new byte[4194304];
			for (int i = 0; i < num; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					int num7 = (int)((j == 0) ? 0 : (1 + array[i]));
					fs.Position = (long)(num3 + 144 * num7 + 32);
					ulong num8 = binaryReader.ReadUInt64();
					int num9 = (int)(num8 >> 32) & 16383;
					int num10 = (int)(num8 >> 48) & 63;
					int num11 = (int)(num8 >> 56) & 63;
					Trace.Assert(binaryReader.ReadUInt64() == 80uL, "Unexpected texture format");
					fs.Position = (long)(num3 + 144 * num7 + 64);
					ulong num12 = binaryReader.ReadUInt64();
					Trace.Assert(binaryReader.ReadUInt64() == 82uL, "Unexpected texture format");
					fs.Position = (long)(num3 + 144 * num7 + 96);
					ulong num13 = binaryReader.ReadUInt64();
					int num14 = (int)num13 & 16383;
					fs.Position = (long)(num3 + 144 * num7 + 112);
					ulong num15 = binaryReader.ReadUInt64();
					int num16 = (int)num15 & 16383;
					int num17 = (int)(num15 >> 32) & 2147483647;
					Trace.Assert(num14 == num16, "Unexpected texture format");
					fs.Position = (long)num17;
					byte[] array3 = new byte[16 * num16];
					fs.Read(array3, 0, 16 * num16);
					Trace.Assert(num11 == 0, "Unexpected texture format");
					int bh = Convert.ToInt32(array3.Length) / 8192 / num10;
					array3 = Reform32.Encode32(array3, num10, bh);
					Array.Copy(array3, 0, array2, 256 * num9, 16 * num16);
					Console.Write("");
				}
				fs.Position = (long)(num4 + 160 * i + 32);
				ulong num18 = binaryReader.ReadUInt64();
				Trace.Assert(num18 == 0uL, "Unexpected texture format");
				Trace.Assert(binaryReader.ReadUInt64() == 63uL, "Unexpected texture format");
				fs.Position = (long)(num4 + 160 * i + 48);
				ulong num19 = binaryReader.ReadUInt64();
				Trace.Assert(num19 == 0uL, "Unexpected texture format");
				Trace.Assert(binaryReader.ReadUInt64() == 52uL, "Unexpected texture format");
				fs.Position = (long)(num4 + 160 * i + 64);
				ulong num20 = binaryReader.ReadUInt64();
				Trace.Assert(num20 == 0uL, "Unexpected texture format");
				Trace.Assert(binaryReader.ReadUInt64() == 54uL, "Unexpected texture format");
				fs.Position = (long)(num4 + 160 * i + 80);
				ulong num21 = binaryReader.ReadUInt64();
				Trace.Assert(binaryReader.ReadUInt64() == 22uL, "Unexpected texture format");
				fs.Position = (long)(num4 + 160 * i + 112);
				ulong num22 = binaryReader.ReadUInt64();
				int num23 = (int)num22 & 16383;
				int tbw = (int)(num22 >> 14) & 63;
				int num24 = (int)(num22 >> 20) & 63;
				int num25 = (int)(num22 >> 26) & 15;
				int num26 = (int)(num22 >> 30) & 15;
				int num27 = (int)(num22 >> 37) & 16383;
				Trace.Assert(binaryReader.ReadUInt64() == 6uL, "Unexpected texture format");
				int num28 = (1 << num25) * (1 << num26);
				byte[] array4 = new byte[num28];
				Array.Copy(array2, 256 * num23, array4, 0, array4.Length);
				byte[] array5 = new byte[8192];
				Array.Copy(array2, 256 * num27, array5, 0, array5.Length);
				STim item = null;
				if (num24 == 19)
				{
					item = TIMf.TexUtil.Decode8(array4, array5, tbw, 1 << num25, 1 << num26);
				}
				if (num24 == 20)
				{
					item = TIMf.TexUtil.Decode4(array4, array5, tbw, 1 << num25, 1 << num26);
				}
				texex.alt.Add(item);
			}
			int num29 = 0;
			while ((long)num29 < fs.Length)
			{
				fs.Position = (long)num29;
				byte[] array6 = binaryReader.ReadBytes(16);
				if (array6[0] == 95 && array6[1] == 68 && array6[2] == 77 && array6[3] == 89 && array6[8] == 84 && array6[9] == 69 && array6[10] == 88 && array6[11] == 65)
				{
					fs.Position = (long)(num29 + 16 + 2);
					int texi = (int)binaryReader.ReadUInt16();
					fs.Position = (long)(num29 + 16 + 12);
					binaryReader.ReadUInt16();
					int num30 = (int)binaryReader.ReadUInt16();
					int px = (int)binaryReader.ReadUInt16();
					int py = (int)binaryReader.ReadUInt16();
					int num31 = (int)binaryReader.ReadUInt16();
					int num32 = (int)binaryReader.ReadUInt16();
					int num33 = binaryReader.ReadInt32();
					int num34 = binaryReader.ReadInt32();
					int num35 = binaryReader.ReadInt32();
					fs.Position = (long)(num29 + 16 + num35);
					byte[] bits = binaryReader.ReadBytes(num31 * num32 * num30);
					Patc patc;
					texex.alp.Add(patc = new Patc(bits, px, py, num31, num32, num30, texi));
					int num36 = num29 + 16;
					int num37 = (num34 - num33) / 2;
					List<Texfac> list = new List<Texfac>();
					for (int k = 0; k < num37; k++)
					{
						fs.Position = (long)(num36 + num33 + 2 * k);
						int num38 = (int)(binaryReader.ReadInt16() - 1);
						if (num38 >= 0)
						{
							fs.Position = (long)(num36 + num34 + 4 * num38);
							int num39 = binaryReader.ReadInt32();
							fs.Position = (long)(num36 + num39);
							int num40 = 0;
							Texfac texfac;
							do
							{
								texfac = new Texfac();
								texfac.i0 = k;
								texfac.i1 = num38;
								texfac.i2 = num40;
								texfac.v0 = binaryReader.ReadInt16();
								texfac.v2 = binaryReader.ReadInt16();
								texfac.v4 = binaryReader.ReadInt16();
								texfac.v6 = binaryReader.ReadInt16();
								list.Add(texfac);
								num40++;
							}
							while (texfac.v0 >= 0);
						}
					}
					patc.altf = list.ToArray();
				}
				num29 += 16;
			}
			return texex;
		}
	}
}
