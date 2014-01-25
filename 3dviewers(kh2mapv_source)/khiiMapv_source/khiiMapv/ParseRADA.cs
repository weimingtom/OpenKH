namespace khiiMapv
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;

    public class ParseRADA
    {
        private BinaryReader br;
        public Bitmap pic;
        private Stream si;

        public ParseRADA(Stream si)
        {
            Stream stream;
            base..ctor();
            this.br = new BinaryReader(this.si = si);
            return;
        }

        public void Parse()
        {
            BinaryReader reader;
            int num;
            int num2;
            int num3;
            byte[] buffer;
            byte[] buffer2;
            reader = new BinaryReader(this.si);
            this.si.Position = 4L;
            if (reader.ReadUInt16() == 4)
            {
                goto Label_002F;
            }
            throw new NotSupportedException("@04 != 4");
        Label_002F:
            this.si.Position = 0x24L;
            num2 = reader.ReadUInt16();
            this.si.Position = 0x26L;
            num3 = reader.ReadUInt16();
            this.si.Position = 0x40L;
            buffer = reader.ReadBytes((num2 * num3) / 2);
            buffer2 = reader.ReadBytes(0x40);
            this.pic = BUt.Make4(num2, num3, buffer, buffer2);
            return;
        }

        private class BUt
        {
            public BUt()
            {
                base..ctor();
                return;
            }

            public static unsafe Bitmap Make4(int cx, int cy, byte[] bin, byte[] palb)
            {
                Bitmap bitmap;
                BitmapData data;
                int num;
                byte num2;
                int num3;
                ColorPalette palette;
                int num4;
                int num5;
                bitmap = new Bitmap(cx, cy, 0x30402);
                data = bitmap.LockBits(Rectangle.FromLTRB(0, 0, cx, cy), 2, 0x30402);
                num = 0;
                goto Label_003C;
            Label_0027:
                num2 = bin[num];
                num2 = (byte) ((num2 << 4) | (num2 >> 4));
                bin[num] = num2;
                num += 1;
            Label_003C:
                if (num < ((int) bin.Length))
                {
                    goto Label_0027;
                }
            Label_0042:
                try
                {
                    num3 = data.Stride * data.Height;
                    Marshal.Copy(bin, 0, data.Scan0, Math.Min((int) bin.Length, num3));
                    goto Label_0072;
                }
                finally
                {
                Label_006A:
                    bitmap.UnlockBits(data);
                }
            Label_0072:
                palette = bitmap.Palette;
                num4 = 0;
                goto Label_00C5;
            Label_007F:
                num5 = 4 * num4;
                *(&(palette.Entries[num4])) = Color.FromArgb(Math.Min(0xff, 2 * palb[num5 + 3]), palb[num5], palb[num5 + 1], palb[num5 + 2]);
                num4 += 1;
            Label_00C5:
                if (num4 < 0x10)
                {
                    goto Label_007F;
                }
                bitmap.Palette = palette;
                return bitmap;
            }
        }
    }
}

