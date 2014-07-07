using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace khiiMapv
{
    public class ParseRADA
    {
        private BinaryReader br;
        public Bitmap pic;
        private Stream si;
        /// <summary>
        /// Makes a BinaryReader of the Stream
        /// </summary>
        /// <param name="si">Stream</param>
        public ParseRADA(Stream si)
        {
            this.si = si;
            br = new BinaryReader(si);
        }
        /// <summary>
        /// Function that will parse the RADAR file(minimap)
        /// </summary>
        public void Parse()
        {
            var binaryReader = new BinaryReader(si);
            si.Position = 4L;
            int num = binaryReader.ReadUInt16();
            if (num != 4)
            {
                throw new NotSupportedException("@04 != 4");
            }
            si.Position = 36L;
            int num2 = binaryReader.ReadUInt16();
            si.Position = 38L;
            int num3 = binaryReader.ReadUInt16();
            si.Position = 64L;
            byte[] bin = binaryReader.ReadBytes(num2*num3/2);
            byte[] palb = binaryReader.ReadBytes(64);
            pic = BUt.Make4(num2, num3, bin, palb);
        }

        private class BUt
        {
            /// <summary>
            /// Utility Function that will convert the picture into a Bitmap one
            /// </summary>
            /// <param name="cx">X Coordinates</param>
            /// <param name="cy">Y Coordinates</param>
            /// <param name="bin">Byte array of the picture</param>
            /// <param name="palb">Byte array of the palette</param>
            /// <returns></returns>
            public static Bitmap Make4(int cx, int cy, byte[] bin, byte[] palb)
            {
                var bitmap = new Bitmap(cx, cy, PixelFormat.Format4bppIndexed);
                BitmapData bitmapData = bitmap.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), ImageLockMode.WriteOnly,
                    PixelFormat.Format4bppIndexed);
                for (int i = 0; i < bin.Length; i++)
                {
                    byte b = bin[i];
                    b = (byte) (b << 4 | b >> 4);
                    bin[i] = b;
                }
                try
                {
                    int val = bitmapData.Stride*bitmapData.Height;
                    Marshal.Copy(bin, 0, bitmapData.Scan0, Math.Min(bin.Length, val));
                }
                finally
                {
                    bitmap.UnlockBits(bitmapData);
                }
                ColorPalette palette = bitmap.Palette;
                for (int j = 0; j < 16; j++)
                {
                    int num = 4*j;
                    palette.Entries[j] = Color.FromArgb(Math.Min(255, 2*palb[num + 3]), palb[num], palb[num + 1],
                        palb[num + 2]);
                }
                bitmap.Palette = palette;
                return bitmap;
            }
        }
    }
}