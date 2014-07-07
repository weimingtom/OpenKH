using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using vcBinTex4;
using vwBinTex2;

namespace khiiMapv
{
    public class ParseIMGD
    {
        /// <summary>
        /// Function that will parse an IMGD file
        /// </summary>
        /// <param name="si">MemoryStream of the file</param>
        /// <returns></returns>
        public static PicIMGD TakeIMGD(MemoryStream si)
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
                byte[] array3 = flag ? Reform8.Decode8(Reform32.Encode32(array, num9, num10), bw, bh) : array;
                var bitmap = new Bitmap(num7, num8, PixelFormat.Format8bppIndexed);
                BitmapData bitmapData = bitmap.LockBits(Rectangle.FromLTRB(0, 0, num7, num8), ImageLockMode.WriteOnly,
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
                    int num12 = KHcv8pal_swap34.repl(i);
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
                    ? Reform4.Decode4(Reform32.Encode32(array, num13, num14), bw2, bh2)
                    : HLUt.Swap(array);
                var bitmap2 = new Bitmap(num7, num8, PixelFormat.Format4bppIndexed);
                BitmapData bitmapData2 = bitmap2.LockBits(Rectangle.FromLTRB(0, 0, num7, num8), ImageLockMode.WriteOnly,
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

        private class HLUt
        {
            /// <summary>
            /// Utility function for swapping the bytes
            /// </summary>
            /// <param name="bin">Byte array to swap</param>
            /// <returns></returns>
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