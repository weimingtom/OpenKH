namespace khiiMapv
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using vcBinTex4;
    using vwBinTex2;

    public class ParseIMGD
    {
        public ParseIMGD()
        {
            base..ctor();
            return;
        }

        public static unsafe PicIMGD TakeIMGD(MemoryStream si)
        {
            BinaryReader reader;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            bool flag;
            byte[] buffer;
            byte[] buffer2;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            int num14;
            byte[] buffer3;
            Bitmap bitmap;
            BitmapData data;
            byte[] buffer4;
            byte[] buffer5;
            ColorPalette palette;
            int num15;
            int num16;
            int num17;
            int num18;
            int num19;
            int num20;
            int num21;
            byte[] buffer6;
            Bitmap bitmap2;
            BitmapData data2;
            byte[] buffer7;
            byte[] buffer8;
            ColorPalette palette2;
            int num22;
            int num23;
            int num24;
            si.Position = 0L;
            if (si.ReadByte() == 0x49)
            {
                goto Label_001D;
            }
            throw new NotSupportedException("!IMGD");
        Label_001D:
            if (si.ReadByte() == 0x4d)
            {
                goto Label_0032;
            }
            throw new NotSupportedException("!IMGD");
        Label_0032:
            if (si.ReadByte() == 0x47)
            {
                goto Label_0047;
            }
            throw new NotSupportedException("!IMGD");
        Label_0047:
            if (si.ReadByte() == 0x44)
            {
                goto Label_005C;
            }
            throw new NotSupportedException("!IMGD");
        Label_005C:
            reader = new BinaryReader(si);
            reader.ReadInt32();
            num = reader.ReadInt32();
            num2 = reader.ReadInt32();
            num3 = reader.ReadInt32();
            num4 = reader.ReadInt32();
            si.Position = 0x1cL;
            num5 = reader.ReadUInt16();
            num6 = reader.ReadUInt16();
            si.Position = 0x26L;
            num7 = reader.ReadUInt16();
            si.Position = 60L;
            flag = reader.ReadByte() == 7;
            si.Position = (long) num;
            buffer = reader.ReadBytes(num2);
            si.Position = (long) num3;
            buffer2 = reader.ReadBytes(num4);
            num9 = num5;
            num10 = num6;
            if (num7 != 0x13)
            {
                goto Label_0227;
            }
            num11 = num5 / 0x80;
            num12 = num6 / 0x40;
            num13 = num11;
            num14 = num12;
            buffer3 = (flag != null) ? Reform8.Decode8(Reform32.Encode32(buffer, num11, num12), num13, num14) : buffer;
            bitmap = new Bitmap(num9, num10, 0x30803);
            data = bitmap.LockBits(Rectangle.FromLTRB(0, 0, num9, num10), 2, 0x30803);
        Label_015E:
            try
            {
                Marshal.Copy(buffer3, 0, data.Scan0, (int) buffer3.Length);
                goto Label_017D;
            }
            finally
            {
            Label_0173:
                bitmap.UnlockBits(data);
            }
        Label_017D:
            buffer4 = new byte[0x2000];
            Array.Copy(buffer2, 0, buffer4, 0, Math.Min(0x2000, (int) buffer2.Length));
            buffer5 = buffer4;
            palette = bitmap.Palette;
            num15 = 0;
            goto Label_020D;
        Label_01B4:
            num16 = num15;
            num17 = KHcv8pal_swap34.repl(num15);
            *(&(palette.Entries[num17])) = Color.FromArgb(Math.Min(0xff, buffer5[(4 * num16) + 3] * 2), buffer5[4 * num16], buffer5[(4 * num16) + 1], buffer5[(4 * num16) + 2]);
            num15 += 1;
        Label_020D:
            if (num15 < 0x100)
            {
                goto Label_01B4;
            }
            bitmap.Palette = palette;
            return new PicIMGD(bitmap);
        Label_0227:
            if (num7 != 20)
            {
                goto Label_035A;
            }
            num18 = num5 / 0x80;
            num19 = num6 / 0x80;
            num20 = num18;
            num21 = num19;
            buffer6 = (flag != null) ? Reform4.Decode4(Reform32.Encode32(buffer, num18, num19), num20, num21) : HLUt.Swap(buffer);
            bitmap2 = new Bitmap(num9, num10, 0x30402);
            data2 = bitmap2.LockBits(Rectangle.FromLTRB(0, 0, num9, num10), 2, 0x30402);
        Label_0299:
            try
            {
                Marshal.Copy(buffer6, 0, data2.Scan0, (int) buffer6.Length);
                goto Label_02B8;
            }
            finally
            {
            Label_02AE:
                bitmap2.UnlockBits(data2);
            }
        Label_02B8:
            buffer7 = new byte[0x2000];
            Array.Copy(buffer2, 0, buffer7, 0, Math.Min(0x2000, (int) buffer2.Length));
            buffer8 = buffer7;
            palette2 = bitmap2.Palette;
            num22 = 0;
            goto Label_0343;
        Label_02EF:
            num23 = num22;
            num24 = num22;
            *(&(palette2.Entries[num24])) = Color.FromArgb(Math.Min(0xff, buffer8[(4 * num23) + 3] * 2), buffer8[4 * num23], buffer8[(4 * num23) + 1], buffer8[(4 * num23) + 2]);
            num22 += 1;
        Label_0343:
            if (num22 < 0x10)
            {
                goto Label_02EF;
            }
            bitmap2.Palette = palette2;
            return new PicIMGD(bitmap2);
        Label_035A:
            throw new NotSupportedException("v26 = " + &num7.ToString("X"));
        }

        private class HLUt
        {
            public HLUt()
            {
                base..ctor();
                return;
            }

            public static byte[] Swap(byte[] bin)
            {
                int num;
                byte[] buffer;
                int num2;
                byte num3;
                num = (int) bin.Length;
                buffer = new byte[num];
                num2 = 0;
                goto Label_0022;
            Label_000F:
                num3 = bin[num2];
                buffer[num2] = (byte) ((num3 >> 4) | (num3 << 4));
                num2 += 1;
            Label_0022:
                if (num2 < num)
                {
                    goto Label_000F;
                }
                return buffer;
            }
        }
    }
}

