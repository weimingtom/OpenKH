namespace vconv122
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using vcBinTex4;
    using vwBinTex2;

    public class TIMf
    {
        public const float γ = 0.5f;

        public static Texex2 Load(Stream fs)
        {
            Texex2 texex = new Texex2();
            byte[] buffer = null;
            BinaryReader reader = new BinaryReader(fs);
            fs.Position = 8L;
            reader.ReadInt32();
            int count = reader.ReadInt32();
            int num2 = reader.ReadInt32();
            int num3 = reader.ReadInt32();
            int num4 = reader.ReadInt32();
            fs.Position = num2;
            buffer = reader.ReadBytes(count);
            fs.Position = 0x1cL;
            int num5 = reader.ReadInt32();
            int num6 = reader.ReadInt32();
            fs.Position = num3;
            reader.ReadBytes(num4 - num3);
            fs.Position = num4;
            reader.ReadBytes(num5 - num4);
            fs.Position = num5;
            reader.ReadBytes(num6 - num5);
            reader.ReadBytes((Convert.ToInt32(fs.Length) - 4) - num6);
            byte[] destinationArray = new byte[0x400000];
            for (int i = 0; i < count; i++)
            {
                for (int k = 0; k < 2; k++)
                {
                    int num9 = (k == 0) ? 0 : (1 + buffer[i]);
                    fs.Position = (num3 + (0x90 * num9)) + 0x20;
                    ulong num10 = reader.ReadUInt64();
                    int num11 = ((int) (num10 >> 0x20)) & 0x3fff;
                    int bw = ((int) (num10 >> 0x30)) & 0x3f;
                    int num13 = ((int) (num10 >> 0x38)) & 0x3f;
                    Trace.Assert(reader.ReadUInt64() == 80L, "Unexpected texture format");
                    fs.Position = (num3 + (0x90 * num9)) + 0x40;
                    ulong num14 = reader.ReadUInt64();
                    Trace.Assert(reader.ReadUInt64() == 0x52L, "Unexpected texture format");
                    fs.Position = (num3 + (0x90 * num9)) + 0x60;
                    int num16 = ((int) reader.ReadUInt64()) & 0x3fff;
                    fs.Position = (num3 + (0x90 * num9)) + 0x70;
                    ulong num17 = reader.ReadUInt64();
                    int num18 = ((int) num17) & 0x3fff;
                    int num19 = ((int) (num17 >> 0x20)) & 0x7fffffff;
                    Trace.Assert(num16 == num18, "Unexpected texture format");
                    fs.Position = num19;
                    byte[] buffer3 = new byte[0x10 * num18];
                    fs.Read(buffer3, 0, 0x10 * num18);
                    Trace.Assert(num13 == 0, "Unexpected texture format");
                    int bh = (Convert.ToInt32(buffer3.Length) / 0x2000) / bw;
                    Array.Copy(Reform32.Encode32(buffer3, bw, bh), 0, destinationArray, 0x100 * num11, 0x10 * num18);
                    Console.Write("");
                }
                fs.Position = (num4 + (160 * i)) + 0x20;
                Trace.Assert(reader.ReadUInt64() == 0L, "Unexpected texture format");
                Trace.Assert(reader.ReadUInt64() == 0x3fL, "Unexpected texture format");
                fs.Position = (num4 + (160 * i)) + 0x30;
                Trace.Assert(reader.ReadUInt64() == 0L, "Unexpected texture format");
                Trace.Assert(reader.ReadUInt64() == 0x34L, "Unexpected texture format");
                fs.Position = (num4 + (160 * i)) + 0x40;
                Trace.Assert(reader.ReadUInt64() == 0L, "Unexpected texture format");
                Trace.Assert(reader.ReadUInt64() == 0x36L, "Unexpected texture format");
                fs.Position = (num4 + (160 * i)) + 80;
                ulong num24 = reader.ReadUInt64();
                Trace.Assert(reader.ReadUInt64() == 0x16L, "Unexpected texture format");
                fs.Position = (num4 + (160 * i)) + 0x70;
                ulong num25 = reader.ReadUInt64();
                int num26 = ((int) num25) & 0x3fff;
                int tbw = ((int) (num25 >> 14)) & 0x3f;
                int num28 = ((int) (num25 >> 20)) & 0x3f;
                int num29 = ((int) (num25 >> 0x1a)) & 15;
                int num30 = ((int) (num25 >> 30)) & 15;
                int num31 = ((int) (num25 >> 0x25)) & 0x3fff;
                Trace.Assert(reader.ReadUInt64() == 6L, "Unexpected texture format");
                int num32 = (((int) 1) << num29) * (((int) 1) << num30);
                byte[] buffer4 = new byte[num32];
                Array.Copy(destinationArray, 0x100 * num26, buffer4, 0, buffer4.Length);
                byte[] buffer5 = new byte[0x2000];
                Array.Copy(destinationArray, 0x100 * num31, buffer5, 0, buffer5.Length);
                STim item = null;
                switch (num28)
                {
                    case 0x13:
                        item = TexUtil.Decode8(buffer4, buffer5, tbw, ((int) 1) << num29, ((int) 1) << num30);
                        break;

                    case 20:
                        item = TexUtil.Decode4(buffer4, buffer5, tbw, ((int) 1) << num29, ((int) 1) << num30);
                        break;
                }
                texex.alt.Add(item);
            }
            for (int j = 0; j < fs.Length; j += 0x10)
            {
                fs.Position = j;
                byte[] buffer6 = reader.ReadBytes(0x10);
                if ((((buffer6[0] == 0x5f) && (buffer6[1] == 0x44)) && ((buffer6[2] == 0x4d) && (buffer6[3] == 0x59))) && (((buffer6[8] == 0x54) && (buffer6[9] == 0x45)) && ((buffer6[10] == 0x58) && (buffer6[11] == 0x41))))
                {
                    Patc patc;
                    fs.Position = (j + 0x10) + 2;
                    int texi = reader.ReadUInt16();
                    fs.Position = (j + 0x10) + 12;
                    reader.ReadUInt16();
                    int ycnt = reader.ReadUInt16();
                    int px = reader.ReadUInt16();
                    int py = reader.ReadUInt16();
                    int pcx = reader.ReadUInt16();
                    int pcy = reader.ReadUInt16();
                    int num40 = reader.ReadInt32();
                    int num41 = reader.ReadInt32();
                    int num42 = reader.ReadInt32();
                    fs.Position = (j + 0x10) + num42;
                    byte[] bits = reader.ReadBytes((pcx * pcy) * ycnt);
                    texex.alp.Add(patc = new Patc(bits, px, py, pcx, pcy, ycnt, texi));
                    int num43 = j + 0x10;
                    int num44 = (num41 - num40) / 2;
                    List<Texfac> list = new List<Texfac>();
                    for (int m = 0; m < num44; m++)
                    {
                        fs.Position = (num43 + num40) + (2 * m);
                        int num46 = reader.ReadInt16() - 1;
                        if (num46 >= 0)
                        {
                            Texfac texfac;
                            fs.Position = (num43 + num41) + (4 * num46);
                            int num47 = reader.ReadInt32();
                            fs.Position = num43 + num47;
                            int num48 = 0;
                            do
                            {
                                texfac = new Texfac {
                                    i0 = m,
                                    i1 = num46,
                                    i2 = num48,
                                    v0 = reader.ReadInt16(),
                                    v2 = reader.ReadInt16(),
                                    v4 = reader.ReadInt16(),
                                    v6 = reader.ReadInt16()
                                };
                                list.Add(texfac);
                                num48++;
                            }
                            while (texfac.v0 >= 0);
                        }
                    }
                    patc.altf = list.ToArray();
                }
            }
            return texex;
        }

        private class CUtil
        {
            public static Color Gamma(Color a, float gamma)
            {
                return Color.FromArgb(a.A, Math.Min(0xff, (int) (Math.Pow(((double) a.R) / 255.0, (double) gamma) * 255.0)), Math.Min(0xff, (int) (Math.Pow(((double) a.G) / 255.0, (double) gamma) * 255.0)), Math.Min(0xff, (int) (Math.Pow(((double) a.B) / 255.0, (double) gamma) * 255.0)));
            }
        }

        private class TexUtil
        {
            public static STim Decode4(byte[] picbin, byte[] palbin, int tbw, int cx, int cy)
            {
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
                tbw /= 2;
                byte[] source = Reform4.Decode4(picbin, tbw, Math.Max(1, (picbin.Length / 0x2000) / tbw));
                BitmapData bitmapdata = pic.LockBits(Rectangle.FromLTRB(0, 0, pic.Width, pic.Height), ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);
                try
                {
                    int num = bitmapdata.Stride * bitmapdata.Height;
                    Marshal.Copy(source, 0, bitmapdata.Scan0, Math.Min(source.Length, num));
                }
                finally
                {
                    pic.UnlockBits(bitmapdata);
                }
                ColorPalette palette = pic.Palette;
                int num2 = 0;
                for (int i = 0; i < 0x10; i++)
                {
                    palette.Entries[i] = TIMf.CUtil.Gamma(Color.FromArgb(AcUt.GetA(palbin[(num2 + (4 * i)) + 3]), palbin[num2 + (4 * i)], palbin[(num2 + (4 * i)) + 1], palbin[(num2 + (4 * i)) + 2]), 0.5f);
                }
                pic.Palette = palette;
                return new STim(pic);
            }

            public static STim Decode8(byte[] picbin, byte[] palbin, int tbw, int cx, int cy)
            {
                Bitmap pic = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
                tbw /= 2;
                byte[] source = Reform8.Decode8(picbin, tbw, Math.Max(1, (picbin.Length / 0x2000) / tbw));
                BitmapData bitmapdata = pic.LockBits(Rectangle.FromLTRB(0, 0, pic.Width, pic.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
                try
                {
                    int num = bitmapdata.Stride * bitmapdata.Height;
                    Marshal.Copy(source, 0, bitmapdata.Scan0, Math.Min(source.Length, num));
                }
                finally
                {
                    pic.UnlockBits(bitmapdata);
                }
                ColorPalette palette = pic.Palette;
                int num2 = 0;
                byte[] destinationArray = new byte[0x400];
                for (int i = 0; i < 0x100; i++)
                {
                    int num4 = KHcv8pal.repl(i);
                    Array.Copy(palbin, 4 * i, destinationArray, 4 * num4, 4);
                }
                Array.Copy(destinationArray, 0, palbin, 0, 0x400);
                for (int j = 0; j < 0x100; j++)
                {
                    palette.Entries[j] = TIMf.CUtil.Gamma(Color.FromArgb(AcUt.GetA(palbin[(num2 + (4 * j)) + 3]) ^ (j & 1), Math.Min(0xff, palbin[num2 + (4 * j)] + 1), Math.Min(0xff, palbin[(num2 + (4 * j)) + 1] + 1), Math.Min(0xff, palbin[(num2 + (4 * j)) + 2] + 1)), 0.5f);
                }
                pic.Palette = palette;
                return new STim(pic);
            }

            private class AcUt
            {
                public static byte GetA(byte a)
                {
                    if (0 < a)
                    {
                        return 0xff;
                    }
                    return 0;
                }
            }
        }
    }
}

