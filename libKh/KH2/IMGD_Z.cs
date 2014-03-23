using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace LIBKH

{
    namespace KH2
    {
        /// <summary>
        ///     Reform functions and useful ones
        /// </summary>
        internal static class Reform
        {
            #region pals

            internal class pals
            {
                public static int repl(int x)
                {
                    return (x & 231) | (((x & 16) != 0) ? 8 : 0) | (((x & 8) != 0) ? 16 : 0);
                }
            }

            #endregion

            #region Reform32

            private static readonly byte[] tbl32bc =
            {
                0, 1, 4, 5, 16, 17, 20, 21, 2, 3, 6, 7, 18, 19, 22, 23,
                8, 9, 12, 13, 24, 25, 28, 29, 10, 11, 14, 15, 26, 27, 30, 31
            };

            private static readonly byte[] tbl32pao = {0, 1, 4, 5, 8, 9, 12, 13, 2, 3, 6, 7, 10, 11, 14, 15};

            public static byte[] Decode32(byte[] bin, int bw, int bh)
            {
                var buffer = new byte[bin.Length];
                for (int i = 0; i < (0x20*bh); i += 0x20)
                {
                    for (int j = 0; j < (0x40*bw); j += 0x40)
                    {
                        int num3 = 0x2000*((j/0x40) + (bw*(i/0x20)));
                        for (int k = 0; k < 0x20; k += 8)
                        {
                            for (int m = 0; m < 0x40; m += 8)
                            {
                                int num6 = 0x100*tbl32bc[(m/8) + ((k/8)*8)];
                                for (int n = 0; n < 4; n++)
                                {
                                    int num8 = 0x40*n;
                                    for (int num9 = 0; num9 < 0x10; num9++)
                                    {
                                        int num10 = (j + m) + (num9%8);
                                        int num11 = ((i + k) + (2*n)) + (num9/8);
                                        int index = 4*(num10 + ((0x40*bw)*num11));
                                        int num13 = (((4*tbl32pao[num9]) + num8) + num6) + num3;
                                        buffer[index] = bin[num13];
                                        buffer[index + 1] = bin[num13 + 1];
                                        buffer[index + 2] = bin[num13 + 2];
                                        buffer[index + 3] = bin[num13 + 3];
                                    }
                                }
                            }
                        }
                    }
                }
                return buffer;
            }

            public static byte[] Encode32(byte[] bin, int bw, int bh)
            {
                var buffer = new byte[bin.Length];
                for (int i = 0; i < (0x20*bh); i += 0x20)
                {
                    for (int j = 0; j < (0x40*bw); j += 0x40)
                    {
                        int num3 = 0x2000*((j/0x40) + (bw*(i/0x20)));
                        for (int k = 0; k < 0x20; k += 8)
                        {
                            for (int m = 0; m < 0x40; m += 8)
                            {
                                int num6 = 0x100*tbl32bc[(m/8) + ((k/8)*8)];
                                for (int n = 0; n < 4; n++)
                                {
                                    int num8 = 0x40*n;
                                    for (int num9 = 0; num9 < 0x10; num9++)
                                    {
                                        int num10 = (j + m) + (num9%8);
                                        int num11 = ((i + k) + (2*n)) + (num9/8);
                                        int index = 4*(num10 + ((0x40*bw)*num11));
                                        int num13 = (((4*tbl32pao[num9]) + num8) + num6) + num3;
                                        buffer[num13] = bin[index];
                                        buffer[num13 + 1] = bin[index + 1];
                                        buffer[num13 + 2] = bin[index + 2];
                                        buffer[num13 + 3] = bin[index + 3];
                                    }
                                }
                            }
                        }
                    }
                }
                return buffer;
            }

            #endregion Reform32

            #region Reform4

            private static readonly byte[] tbl4bc =
            {
                0, 2, 8, 10, 1, 3, 9, 11, 4, 6, 12, 14, 5, 7, 13, 15,
                16, 18, 24, 26, 17, 19, 25, 27, 20, 22, 28, 30, 21, 23, 29, 31
            };

            private static readonly int[] tbl4col0 =
            {
                0x00, 0x20, 0x80, 0xa0, 0x100, 0x120, 0x180, 0x1a0, 0x08, 0x28, 0x88, 0xa8, 0x108, 0x128, 0x188, 0x1a8,
                0x10, 0x30, 0x90, 0xb0, 0x110, 0x130, 0x190, 0x1b0, 0x18, 0x38, 0x98, 0xb8, 0x118, 0x138, 0x198, 0x1b8,
                0x40, 0x60, 0xc0, 0xe0, 0x140, 0x160, 0x1c0, 0x1e0, 0x48, 0x68, 0xc8, 0xe8, 0x148, 0x168, 0x1c8, 0x1e8,
                0x50, 0x70, 0xd0, 0xf0, 0x150, 0x170, 0x1d0, 0x1f0, 0x58, 0x78, 0xd8, 0xf8, 0x158, 0x178, 0x1d8, 0x1f8,
                0x104, 0x124, 0x184, 0x1a4, 0x04, 0x24, 0x84, 0xa4, 0x10c, 0x12c, 0x18c, 0x1ac, 0x0c, 0x2c, 0x8c, 0xac,
                0x114, 0x134, 0x194, 0x1b4, 0x14, 0x34, 0x94, 0xb4, 0x11c, 0x13c, 0x19c, 0x1bc, 0x1c, 0x3c, 0x9c, 0xbc,
                0x144, 0x164, 0x1c4, 0x1e4, 0x44, 0x64, 0xc4, 0xe4, 0x14c, 0x16c, 0x1cc, 0x1ec, 0x4c, 0x6c, 0xcc, 0xec,
                0x154, 0x174, 0x1d4, 0x1f4, 0x54, 0x74, 0xd4, 0xf4, 0x15c, 0x17c, 0x1dc, 0x1fc, 0x5c, 0x7c, 0xdc, 0xfc
            };

            private static readonly int[] tbl4col1 =
            {
                0x100, 0x120, 0x180, 0x1a0, 0x00, 0x20, 0x80, 0xa0, 0x108, 0x128, 0x188, 0x1a8, 0x08, 0x28, 0x88, 0xa8,
                0x110, 0x130, 0x190, 0x1b0, 0x10, 0x30, 0x90, 0xb0, 0x118, 0x138, 0x198, 0x1b8, 0x18, 0x38, 0x98, 0xb8,
                0x140, 0x160, 0x1c0, 0x1e0, 0x40, 0x60, 0xc0, 0xe0, 0x148, 0x168, 0x1c8, 0x1e8, 0x48, 0x68, 0xc8, 0xe8,
                0x150, 0x170, 0x1d0, 0x1f0, 0x50, 0x70, 0xd0, 0xf0, 0x158, 0x178, 0x1d8, 0x1f8, 0x58, 0x78, 0xd8, 0xf8,
                0x04, 0x24, 0x84, 0xa4, 0x104, 0x124, 0x184, 0x1a4, 0x0c, 0x2c, 0x8c, 0xac, 0x10c, 0x12c, 0x18c, 0x1ac,
                0x14, 0x34, 0x94, 0xb4, 0x114, 0x134, 0x194, 0x1b4, 0x1c, 0x3c, 0x9c, 0xbc, 0x11c, 0x13c, 0x19c, 0x1bc,
                0x44, 0x64, 0xc4, 0xe4, 0x144, 0x164, 0x1c4, 0x1e4, 0x4c, 0x6c, 0xcc, 0xec, 0x14c, 0x16c, 0x1cc, 0x1ec,
                0x54, 0x74, 0xd4, 0xf4, 0x154, 0x174, 0x1d4, 0x1f4, 0x5c, 0x7c, 0xdc, 0xfc, 0x15c, 0x17c, 0x1dc, 0x1fc
            };

            public static byte[] Decode4(byte[] bin, int bw, int bh)
            {
                var buffer = new byte[bin.Length];
                for (int i = 0; i < (0x80*bh); i += 0x80)
                {
                    for (int j = 0; j < (0x80*bw); j += 0x80)
                    {
                        int num3 = 0x2000*((j/0x80) + (bw*(i/0x80)));
                        for (int k = 0; k < 0x80; k += 0x10)
                        {
                            for (int m = 0; m < 0x80; m += 0x20)
                            {
                                int num6 = 0x100*tbl4bc[(m/0x20) + (4*(k/0x10))];
                                for (int n = 0; n < 4; n++)
                                {
                                    int num8 = 0x40*n;
                                    int[] numArray = ((n & 1) == 0) ? tbl4col0 : tbl4col1;
                                    for (int num9 = 0; num9 < 0x80; num9++)
                                    {
                                        int num10 = numArray[num9]/8;
                                        int num11 = numArray[num9]%8;
                                        var num12 = (byte) ((bin[((num3 + num6) + num8) + num10] >> num11) & 15);
                                        int num13 = (j + m) + (num9%0x20);
                                        int num14 = ((i + k) + (4*n)) + (num9/0x20);
                                        int num15 = num13 + ((0x80*bw)*num14);
                                        byte num16 = buffer[num15/2];
                                        if ((num15 & 1) == 0)
                                        {
                                            num16 = (byte) (num16 & 15);
                                            num16 = (byte) (num16 | ((byte) (num12 << 4)));
                                        }
                                        else
                                        {
                                            num16 = (byte) (num16 & 240);
                                            num16 = (byte) (num16 | num12);
                                        }
                                        buffer[num15/2] = num16;
                                    }
                                }
                            }
                        }
                    }
                }
                return buffer;
            }

            public static byte[] Encode4(byte[] bin, int bw, int bh)
            {
                var buffer = new byte[bin.Length];
                for (int i = 0; i < (0x80*bh); i += 0x80)
                {
                    for (int j = 0; j < (0x80*bw); j += 0x80)
                    {
                        int num3 = 0x2000*((j/0x80) + (bw*(i/0x80)));
                        for (int k = 0; k < 0x80; k += 0x10)
                        {
                            for (int m = 0; m < 0x80; m += 0x20)
                            {
                                int num6 = 0x100*tbl4bc[(m/0x20) + (4*(k/0x10))];
                                for (int n = 0; n < 4; n++)
                                {
                                    int num8 = 0x40*n;
                                    int[] numArray = ((n & 1) == 0) ? tbl4col0 : tbl4col1;
                                    for (int num9 = 0; num9 < 0x80; num9++)
                                    {
                                        int num10 = (j + m) + (num9%0x20);
                                        int num11 = ((i + k) + (4*n)) + (num9/0x20);
                                        int num12 = num10 + ((0x80*bw)*num11);
                                        byte num13 = bin[num12/2];
                                        if ((num12 & 1) != 0)
                                        {
                                            num13 = (byte) (num13 & 15);
                                        }
                                        else
                                        {
                                            num13 = (byte) (num13 >> 4);
                                        }
                                        int num14 = numArray[num9]/8;
                                        int num15 = numArray[num9]%8;
                                        int index = ((num3 + num6) + num8) + num14;
                                        byte num17 = buffer[index];
                                        switch (num15)
                                        {
                                            case 0:
                                                num17 = (byte) (num17 & 240);
                                                num17 = (byte) (num17 | num13);
                                                break;

                                            case 4:
                                                num17 = (byte) (num17 & 15);
                                                num17 = (byte) (num17 | ((byte) (num13 << 4)));
                                                break;
                                        }
                                        buffer[index] = num17;
                                    }
                                }
                            }
                        }
                    }
                }
                return buffer;
            }

            #endregion Reform4

            #region Reform8

            private static readonly byte[] tbl8bc =
            {
                0, 1, 4, 5, 16, 17, 20, 21, 2, 3, 6, 7, 18, 19, 22, 23,
                8, 9, 12, 13, 24, 25, 28, 29, 10, 11, 14, 15, 26, 27, 30, 31
            };

            private static readonly byte[] tbl8c0 =
            {
                0x00, 0x04, 0x10, 0x14, 0x20, 0x24, 0x30, 0x34, 0x02, 0x06, 0x12, 0x16, 0x22, 0x26, 0x32, 0x36,
                0x08, 0x0c, 0x18, 0x1c, 0x28, 0x2c, 0x38, 0x3c, 0x0a, 0x0e, 0x1a, 0x1e, 0x2a, 0x2e, 0x3a, 0x3e,
                0x21, 0x25, 0x31, 0x35, 0x01, 0x05, 0x11, 0x15, 0x23, 0x27, 0x33, 0x37, 0x03, 0x07, 0x13, 0x17,
                0x29, 0x2d, 0x39, 0x3d, 0x09, 0x0d, 0x19, 0x1d, 0x2b, 0x2f, 0x3b, 0x3f, 0x0b, 0x0f, 0x1b, 0x1f
            };

            private static readonly byte[] tbl8c1 =
            {
                0x20, 0x24, 0x30, 0x34, 0x00, 0x04, 0x10, 0x14, 0x22, 0x26, 0x32, 0x36, 0x02, 0x06, 0x12, 0x16,
                0x28, 0x2c, 0x38, 0x3c, 0x08, 0x0c, 0x18, 0x1c, 0x2a, 0x2e, 0x3a, 0x3e, 0x0a, 0x0e, 0x1a, 0x1e,
                0x01, 0x05, 0x11, 0x15, 0x21, 0x25, 0x31, 0x35, 0x03, 0x07, 0x13, 0x17, 0x23, 0x27, 0x33, 0x37,
                0x09, 0x0d, 0x19, 0x1d, 0x29, 0x2d, 0x39, 0x3d, 0x0b, 0x0f, 0x1b, 0x1f, 0x2b, 0x2f, 0x3b, 0x3f
            };

            public static byte[] Decode8(byte[] bin, int bw, int bh)
            {
                var buffer = new byte[bin.Length];
                for (int i = 0; i < (0x40*bh); i += 0x40)
                {
                    for (int j = 0; j < (0x80*bw); j += 0x80)
                    {
                        int num3 = 0x2000*((j/0x80) + (bw*(i/0x40)));
                        for (int k = 0; k < 0x40; k += 0x10)
                        {
                            for (int m = 0; m < 0x80; m += 0x10)
                            {
                                int num6 = 0x100*tbl8bc[(m/0x10) + (8*(k/0x10))];
                                for (int n = 0; n < 4; n++)
                                {
                                    int num8 = 0x40*n;
                                    byte[] buffer2 = ((n & 1) == 0) ? tbl8c0 : tbl8c1;
                                    for (int num9 = 0; num9 < 0x40; num9++)
                                    {
                                        int index = ((num3 + num6) + num8) + buffer2[num9];
                                        int num11 = (j + m) + (num9%0x10);
                                        int num12 = ((i + k) + (4*n)) + (num9/0x10);
                                        int num13 = num11 + ((0x80*bw)*num12);
                                        buffer[num13] = bin[index];
                                    }
                                }
                            }
                        }
                    }
                }
                return buffer;
            }

            public static byte[] Encode8(byte[] bin, int bw, int bh)
            {
                var buffer = new byte[bin.Length];
                for (int i = 0; i < (0x40*bh); i += 0x40)
                {
                    for (int j = 0; j < (0x80*bw); j += 0x80)
                    {
                        int num3 = 0x2000*((j/0x80) + (bw*(i/0x40)));
                        for (int k = 0; k < 0x40; k += 0x10)
                        {
                            for (int m = 0; m < 0x80; m += 0x10)
                            {
                                int num6 = 0x100*tbl8bc[(m/0x10) + (8*(k/0x10))];
                                for (int n = 0; n < 4; n++)
                                {
                                    int num8 = 0x40*n;
                                    byte[] buffer2 = ((n & 1) == 0) ? tbl8c0 : tbl8c1;
                                    for (int num9 = 0; num9 < 0x40; num9++)
                                    {
                                        int index = ((num3 + num6) + num8) + buffer2[num9];
                                        int num11 = (j + m) + (num9%0x10);
                                        int num12 = ((i + k) + (4*n)) + (num9/0x10);
                                        int num13 = num11 + ((0x80*bw)*num12);
                                        buffer[index] = bin[num13];
                                    }
                                }
                            }
                        }
                    }
                }
                return buffer;
            }

            #endregion Reform8
        }

        /// <summary>
        ///     Return bitmap
        ///     <param name="p">Bitmap stream</param>
        /// </summary>
        public class PicIMGD
        {
            public Bitmap pic;

            public PicIMGD(Bitmap p)
            {
                pic = p;
            }
        }

        public class IMGD
        {
            /// <summary>
            ///     Return bitmap of the IMGD file
            ///     <param name="si">MemoryStream of the IMGD file/param>
            /// </summary>
            public static PicIMGD GetIMGD(MemoryStream si)
            {
                si.Position = 0L;
                if (si.ReadByte() != 73)
                {
                    throw new NotSupportedException("!IMGD");
                }
                if (si.ReadByte() != 77)
                {
                    throw new NotSupportedException("!IMGD");
                }
                if (si.ReadByte() != 71)
                {
                    throw new NotSupportedException("!IMGD");
                }
                if (si.ReadByte() != 68)
                {
                    throw new NotSupportedException("!IMGD");
                }
                var binaryReader = new BinaryReader(si);
                binaryReader.ReadInt32();
                int num = binaryReader.ReadInt32();
                int count = binaryReader.ReadInt32();
                int num2 = binaryReader.ReadInt32();
                int count2 = binaryReader.ReadInt32();
                si.Position = 28L;
                int num3 = binaryReader.ReadUInt16();
                int num4 = binaryReader.ReadUInt16();
                si.Position = 38L;
                int num5 = binaryReader.ReadUInt16();
                si.Position = 60L;
                int num6 = binaryReader.ReadByte();
                bool flag = num6 == 7;
                si.Position = num;
                byte[] array = binaryReader.ReadBytes(count);
                si.Position = num2;
                byte[] array2 = binaryReader.ReadBytes(count2);
                int num7 = num3;
                int num8 = num4;
                if (num5 == 19)
                {
                    int num9 = num3/128;
                    int num10 = num4/64;
                    int bw = num9;
                    int bh = num10;
                    byte[] array3 = flag ? Reform.Decode8(Reform.Encode32(array, num9, num10), bw, bh) : array;
                    var bitmap = new Bitmap(num7, num8, PixelFormat.Format8bppIndexed);
                    BitmapData bitmapData = bitmap.LockBits(Rectangle.FromLTRB(0, 0, num7, num8),
                        ImageLockMode.WriteOnly,
                        PixelFormat.Format8bppIndexed);
                    try
                    {
                        Marshal.Copy(array3, 0, bitmapData.Scan0, array3.Length);
                    }
                    finally
                    {
                        bitmap.UnlockBits(bitmapData);
                    }
                    var array4 = new byte[8192];
                    Array.Copy(array2, 0, array4, 0, Math.Min(8192, array2.Length));
                    byte[] array5 = array4;
                    ColorPalette palette = bitmap.Palette;
                    for (int i = 0; i < 256; i++)
                    {
                        int num11 = i;
                        int num12 = Reform.pals.repl(i);
                        palette.Entries[num12] = Color.FromArgb(Math.Min(255, array5[4*num11 + 3]*2), array5[4*num11],
                            array5[4*num11 + 1], array5[4*num11 + 2]);
                    }
                    bitmap.Palette = palette;
                    return new PicIMGD(bitmap);
                }
                if (num5 == 20)
                {
                    int num13 = num3/128;
                    int num14 = num4/128;
                    int bw2 = num13;
                    int bh2 = num14;
                    byte[] array6 = flag
                        ? Reform.Decode4(Reform.Encode32(array, num13, num14), bw2, bh2)
                        : HLUt.Swap(array);
                    var bitmap2 = new Bitmap(num7, num8, PixelFormat.Format4bppIndexed);
                    BitmapData bitmapData2 = bitmap2.LockBits(Rectangle.FromLTRB(0, 0, num7, num8),
                        ImageLockMode.WriteOnly,
                        PixelFormat.Format4bppIndexed);
                    try
                    {
                        Marshal.Copy(array6, 0, bitmapData2.Scan0, array6.Length);
                    }
                    finally
                    {
                        bitmap2.UnlockBits(bitmapData2);
                    }
                    var array7 = new byte[8192];
                    Array.Copy(array2, 0, array7, 0, Math.Min(8192, array2.Length));
                    byte[] array8 = array7;
                    ColorPalette palette2 = bitmap2.Palette;
                    for (int j = 0; j < 16; j++)
                    {
                        int num15 = j;
                        int num16 = j;
                        palette2.Entries[num16] = Color.FromArgb(Math.Min(255, array8[4*num15 + 3]*2), array8[4*num15],
                            array8[4*num15 + 1], array8[4*num15 + 2]);
                    }
                    bitmap2.Palette = palette2;
                    return new PicIMGD(bitmap2);
                }
                throw new NotSupportedException("v26 = " + num5.ToString("X"));
            }

            /// <summary>
            ///     Return a list of bitmap of the IMGZ file
            ///     <param name="bin">array of bytes of the IMGZ file/param>
            /// </summary>
            public static PicIMGD[] GetIMGZ(byte[] bin)
            {
                var memoryStream = new MemoryStream(bin, false);
                var binaryReader = new BinaryReader(memoryStream);
                var list = new List<PicIMGD>();
                memoryStream.Position = 12L;
                int num = binaryReader.ReadInt32();
                for (int i = 0; i < num; i++)
                {
                    int index = binaryReader.ReadInt32();
                    int count = binaryReader.ReadInt32();
                    var si = new MemoryStream(bin, index, count, false);
                    PicIMGD item = GetIMGD(si);
                    list.Add(item);
                }
                return list.ToArray();
            }

            private class HLUt
            {
                public static byte[] Swap(byte[] bin)
                {
                    int num = bin.Length;
                    var array = new byte[num];
                    for (int i = 0; i < num; i++)
                    {
                        byte b = bin[i];
                        array[i] = (byte) (b >> 4 | b << 4);
                    }
                    return array;
                }
            }
        }
    }
}