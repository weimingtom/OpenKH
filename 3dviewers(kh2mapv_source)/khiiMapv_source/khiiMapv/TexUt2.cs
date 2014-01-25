namespace khiiMapv
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;
    using vcBinTex4;
    using vwBinTex2;

    internal class TexUt2
    {
        private const float γ = 1f;

        public TexUt2()
        {
            base..ctor();
            return;
        }

        public static unsafe STim Decode4(byte[] picbin, byte[] palbin, int tbw, int cx, int cy)
        {
            Bitmap bitmap;
            byte[] buffer;
            BitmapData data;
            int num;
            ColorPalette palette;
            int num2;
            int num3;
            bitmap = new Bitmap(cx, cy, 0x30402);
            tbw /= 2;
            buffer = Reform4.Decode4(picbin, tbw, Math.Max(1, (((int) picbin.Length) / 0x2000) / tbw));
            data = bitmap.LockBits(Rectangle.FromLTRB(0, 0, bitmap.Width, bitmap.Height), 2, 0x30402);
        Label_004C:
            try
            {
                num = data.Stride * data.Height;
                Marshal.Copy(buffer, 0, data.Scan0, Math.Min((int) buffer.Length, num));
                goto Label_007A;
            }
            finally
            {
            Label_0072:
                bitmap.UnlockBits(data);
            }
        Label_007A:
            palette = bitmap.Palette;
            num2 = 0;
            num3 = 0;
            goto Label_00E1;
        Label_008A:
            *(&(palette.Entries[num3])) = CUtil.Gamma(Color.FromArgb(AcUt.GetA(palbin[(num2 + (4 * num3)) + 3]), palbin[num2 + (4 * num3)], palbin[(num2 + (4 * num3)) + 1], palbin[(num2 + (4 * num3)) + 2]), 1f);
            num3 += 1;
        Label_00E1:
            if (num3 < 0x10)
            {
                goto Label_008A;
            }
            bitmap.Palette = palette;
            return new STim(bitmap);
        }

        public static unsafe STim Decode4Ps(byte[] picbin, byte[] palbin, int tbw, int cx, int cy, int csa)
        {
            Bitmap bitmap;
            byte[] buffer;
            BitmapData data;
            int num;
            ColorPalette palette;
            int num2;
            byte[] buffer2;
            int num3;
            int num4;
            int num5;
            bitmap = new Bitmap(cx, cy, 0x30402);
            tbw = Math.Max(1, tbw / 2);
            buffer = Reform4.Decode4(picbin, tbw, Math.Max(1, (((int) picbin.Length) / 0x2000) / tbw));
            data = bitmap.LockBits(Rectangle.FromLTRB(0, 0, bitmap.Width, bitmap.Height), 2, 0x30402);
        Label_0052:
            try
            {
                num = data.Stride * data.Height;
                Marshal.Copy(buffer, 0, data.Scan0, Math.Min((int) buffer.Length, num));
                goto Label_0080;
            }
            finally
            {
            Label_0078:
                bitmap.UnlockBits(data);
            }
        Label_0080:
            palette = bitmap.Palette;
            num2 = 0x40 * csa;
            buffer2 = new byte[0x400];
            num3 = 0;
            goto Label_00C0;
        Label_00A0:
            num4 = KHcv8pal.repl(num3);
            Array.Copy(palbin, 4 * num3, buffer2, 4 * num4, 4);
            num3 += 1;
        Label_00C0:
            if (num3 < 0x100)
            {
                goto Label_00A0;
            }
            num5 = 0;
            goto Label_0129;
        Label_00CE:
            *(&(palette.Entries[num5])) = CUtil.Gamma(Color.FromArgb(AcUt.GetA(buffer2[(num2 + (4 * num5)) + 3]), buffer2[num2 + (4 * num5)], buffer2[(num2 + (4 * num5)) + 1], buffer2[(num2 + (4 * num5)) + 2]), 1f);
            num5 += 1;
        Label_0129:
            if (num5 < 0x10)
            {
                goto Label_00CE;
            }
            bitmap.Palette = palette;
            return new STim(bitmap);
        }

        public static unsafe STim Decode8(byte[] picbin, byte[] palbin, int tbw, int cx, int cy)
        {
            Bitmap bitmap;
            byte[] buffer;
            BitmapData data;
            int num;
            ColorPalette palette;
            int num2;
            byte[] buffer2;
            int num3;
            int num4;
            int num5;
            bitmap = new Bitmap(cx, cy, 0x30803);
            tbw /= 2;
            buffer = Reform8.Decode8(picbin, tbw, Math.Max(1, (((int) picbin.Length) / 0x2000) / tbw));
            data = bitmap.LockBits(Rectangle.FromLTRB(0, 0, bitmap.Width, bitmap.Height), 2, 0x30803);
        Label_004C:
            try
            {
                num = data.Stride * data.Height;
                Marshal.Copy(buffer, 0, data.Scan0, Math.Min((int) buffer.Length, num));
                goto Label_007A;
            }
            finally
            {
            Label_0072:
                bitmap.UnlockBits(data);
            }
        Label_007A:
            palette = bitmap.Palette;
            num2 = 0;
            buffer2 = new byte[0x400];
            num3 = 0;
            goto Label_00B6;
        Label_0096:
            num4 = KHcv8pal.repl(num3);
            Array.Copy(palbin, 4 * num3, buffer2, 4 * num4, 4);
            num3 += 1;
        Label_00B6:
            if (num3 < 0x100)
            {
                goto Label_0096;
            }
            Array.Copy(buffer2, 0, palbin, 0, 0x400);
            num5 = 0;
            goto Label_0156;
        Label_00D6:
            *(&(palette.Entries[num5])) = CUtil.Gamma(Color.FromArgb(AcUt.GetA(palbin[(num2 + (4 * num5)) + 3]) ^ (num5 & 1), Math.Min(0xff, palbin[num2 + (4 * num5)] + 1), Math.Min(0xff, palbin[(num2 + (4 * num5)) + 1] + 1), Math.Min(0xff, palbin[(num2 + (4 * num5)) + 2] + 1)), 1f);
            num5 += 1;
        Label_0156:
            if (num5 < 0x100)
            {
                goto Label_00D6;
            }
            bitmap.Palette = palette;
            return new STim(bitmap);
        }

        private class AcUt
        {
            public AcUt()
            {
                base..ctor();
                return;
            }

            public static byte GetA(byte a)
            {
                return (byte) Math.Min((a * 0xff) / 0x80, 0xff);
            }
        }
    }
}

