using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using vcBinTex4;
using vwBinTex2;

namespace khiiMapv
{
    /// <summary>
    /// Only decode functions.
    /// </summary>
    internal class TexUt2
    {
        private const float γ = 1f;
        /// <summary>
        /// Function that will decode the picture to a convertable format
        /// </summary>
        /// <param name="picbin">Byte Array of the encoded pic</param>
        /// <param name="palbin">Byte array of the pallette</param>
        /// <param name="tbw">Unknown</param>
        /// <param name="cx">X Coordinates</param>
        /// <param name="cy">Y coordinates</param>
        /// <returns>Bitmap picture</returns>
        public static STim Decode8(byte[] picbin, byte[] palbin, int tbw, int cx, int cy)
        {
            var bitmap = new Bitmap(cx, cy, PixelFormat.Format8bppIndexed);
            tbw /= 2;
            byte[] array = Reform8.Decode8(picbin, tbw, Math.Max(1, picbin.Length/8192/tbw));
            BitmapData bitmapData = bitmap.LockBits(Rectangle.FromLTRB(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
            try
            {
                int val = bitmapData.Stride*bitmapData.Height;
                Marshal.Copy(array, 0, bitmapData.Scan0, Math.Min(array.Length, val));
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
            ColorPalette palette = bitmap.Palette;
            int num = 0;
            var array2 = new byte[1024];
            for (int i = 0; i < 256; i++)
            {
                int num2 = KHcv8pal.repl(i);
                Array.Copy(palbin, 4*i, array2, 4*num2, 4);
            }
            Array.Copy(array2, 0, palbin, 0, 1024);
            for (int j = 0; j < 256; j++)
            {
                palette.Entries[j] =
                    CUtil.Gamma(
                        Color.FromArgb(AcUt.GetA(palbin[num + 4*j + 3]) ^ (j & 1), Math.Min(255, palbin[num + 4*j] + 1),
                            Math.Min(255, palbin[num + 4*j + 1] + 1), Math.Min(255, palbin[num + 4*j + 2] + 1)), 1f);
            }
            bitmap.Palette = palette;
            return new STim(bitmap);
        }
        /// <summary>
        /// Function that will decode the picture to a convertable format
        /// </summary>
        /// <param name="picbin">Byte Array of the encoded pic</param>
        /// <param name="palbin">Byte array of the pallette</param>
        /// <param name="tbw">Unknown</param>
        /// <param name="cx">X Coordinates</param>
        /// <param name="cy">Y coordinates</param>
        /// <returns>Bitmap picture</returns>
        public static STim Decode4(byte[] picbin, byte[] palbin, int tbw, int cx, int cy)
        {
            var bitmap = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
            tbw /= 2;
            byte[] array = Reform4.Decode4(picbin, tbw, Math.Max(1, picbin.Length/8192/tbw));
            BitmapData bitmapData = bitmap.LockBits(Rectangle.FromLTRB(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);
            try
            {
                int val = bitmapData.Stride*bitmapData.Height;
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
                palette.Entries[i] =
                    CUtil.Gamma(
                        Color.FromArgb(AcUt.GetA(palbin[num + 4*i + 3]), palbin[num + 4*i], palbin[num + 4*i + 1],
                            palbin[num + 4*i + 2]), 1f);
            }
            bitmap.Palette = palette;
            return new STim(bitmap);
        }
        /// <summary>
        /// Function that will decode the picture to a convertable format
        /// </summary>
        /// <param name="picbin">Byte Array of the encoded pic</param>
        /// <param name="palbin">Byte array of the pallette</param>
        /// <param name="tbw">Unknown</param>
        /// <param name="cx">X Coordinates</param>
        /// <param name="cy">Y coordinates</param>
        /// <param name="csa">Unknown</param>
        /// <returns>Bitmap picture</returns>
        public static STim Decode4Ps(byte[] picbin, byte[] palbin, int tbw, int cx, int cy, int csa)
        {
            var bitmap = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
            tbw = Math.Max(1, tbw/2);
            byte[] array = Reform4.Decode4(picbin, tbw, Math.Max(1, picbin.Length/8192/tbw));
            BitmapData bitmapData = bitmap.LockBits(Rectangle.FromLTRB(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format4bppIndexed);
            try
            {
                int val = bitmapData.Stride*bitmapData.Height;
                Marshal.Copy(array, 0, bitmapData.Scan0, Math.Min(array.Length, val));
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
            ColorPalette palette = bitmap.Palette;
            int num = 64*csa;
            var array2 = new byte[1024];
            for (int i = 0; i < 256; i++)
            {
                int num2 = KHcv8pal.repl(i);
                Array.Copy(palbin, 4*i, array2, 4*num2, 4);
            }
            for (int j = 0; j < 16; j++)
            {
                palette.Entries[j] =
                    CUtil.Gamma(
                        Color.FromArgb(AcUt.GetA(array2[num + 4*j + 3]), array2[num + 4*j], array2[num + 4*j + 1],
                            array2[num + 4*j + 2]), 1f);
            }
            bitmap.Palette = palette;
            return new STim(bitmap);
        }

        private class AcUt
        {
            public static byte GetA(byte a)
            {
                return (byte) Math.Min(a*255/128, 255);
            }
        }
    }
}