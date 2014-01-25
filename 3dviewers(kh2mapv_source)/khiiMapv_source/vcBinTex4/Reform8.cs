namespace vcBinTex4
{
    using System;

    public class Reform8
    {
        private static readonly byte[] tbl8bc;
        private static readonly byte[] tbl8c0;
        private static readonly byte[] tbl8c1;

        static Reform8()
        {
            tbl8bc = new byte[] { 
                0, 1, 4, 5, 0x10, 0x11, 20, 0x15, 2, 3, 6, 7, 0x12, 0x13, 0x16, 0x17, 
                8, 9, 12, 13, 0x18, 0x19, 0x1c, 0x1d, 10, 11, 14, 15, 0x1a, 0x1b, 30, 0x1f
             };
            tbl8c0 = new byte[] { 
                0, 4, 0x10, 20, 0x20, 0x24, 0x30, 0x34, 2, 6, 0x12, 0x16, 0x22, 0x26, 50, 0x36, 
                8, 12, 0x18, 0x1c, 40, 0x2c, 0x38, 60, 10, 14, 0x1a, 30, 0x2a, 0x2e, 0x3a, 0x3e, 
                0x21, 0x25, 0x31, 0x35, 1, 5, 0x11, 0x15, 0x23, 0x27, 0x33, 0x37, 3, 7, 0x13, 0x17, 
                0x29, 0x2d, 0x39, 0x3d, 9, 13, 0x19, 0x1d, 0x2b, 0x2f, 0x3b, 0x3f, 11, 15, 0x1b, 0x1f
             };
            tbl8c1 = new byte[] { 
                0x20, 0x24, 0x30, 0x34, 0, 4, 0x10, 20, 0x22, 0x26, 50, 0x36, 2, 6, 0x12, 0x16, 
                40, 0x2c, 0x38, 60, 8, 12, 0x18, 0x1c, 0x2a, 0x2e, 0x3a, 0x3e, 10, 14, 0x1a, 30, 
                1, 5, 0x11, 0x15, 0x21, 0x25, 0x31, 0x35, 3, 7, 0x13, 0x17, 0x23, 0x27, 0x33, 0x37, 
                9, 13, 0x19, 0x1d, 0x29, 0x2d, 0x39, 0x3d, 11, 15, 0x1b, 0x1f, 0x2b, 0x2f, 0x3b, 0x3f
             };
            return;
        }

        public Reform8()
        {
            base..ctor();
            return;
        }

        public static byte[] Decode8(byte[] bin, int bw, int bh)
        {
            byte[] buffer;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            byte[] buffer2;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            buffer = new byte[(int) bin.Length];
            num = 0;
            goto Label_0113;
        Label_0010:
            num2 = 0;
            goto Label_0101;
        Label_0017:
            num3 = 0x2000 * ((num2 / 0x80) + (bw * (num / 0x40)));
            num4 = 0;
            goto Label_00F0;
        Label_0034:
            num5 = 0;
            goto Label_00DD;
        Label_003C:
            num6 = 0x100 * tbl8bc[(num5 / 0x10) + (8 * (num4 / 0x10))];
            num7 = 0;
            goto Label_00D1;
        Label_005C:
            num8 = 0x40 * num7;
            buffer2 = ((num7 & 1) == null) ? tbl8c0 : tbl8c1;
            num9 = 0;
            goto Label_00C5;
        Label_007C:
            num10 = ((num3 + num6) + num8) + buffer2[num9];
            num11 = (num2 + num5) + (num9 % 0x10);
            num12 = ((num + num4) + (4 * num7)) + (num9 / 0x10);
            num13 = num11 + ((0x80 * bw) * num12);
            buffer[num13] = bin[num10];
            num9 += 1;
        Label_00C5:
            if (num9 < 0x40)
            {
                goto Label_007C;
            }
            num7 += 1;
        Label_00D1:
            if (num7 < 4)
            {
                goto Label_005C;
            }
            num5 += 0x10;
        Label_00DD:
            if (num5 < 0x80)
            {
                goto Label_003C;
            }
            num4 += 0x10;
        Label_00F0:
            if (num4 < 0x40)
            {
                goto Label_0034;
            }
            num2 += 0x80;
        Label_0101:
            if (num2 < (0x80 * bw))
            {
                goto Label_0017;
            }
            num += 0x40;
        Label_0113:
            if (num < (0x40 * bh))
            {
                goto Label_0010;
            }
            return buffer;
        }

        public static byte[] Decode8c(byte[] gsram, int cx, int cy, int readoff, int bw128)
        {
            byte[] buffer;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            byte[] buffer2;
            int num7;
            int num8;
            int num9;
            buffer = new byte[cx * cy];
            num = 0;
            goto Label_00AB;
        Label_0010:
            num2 = 0;
            goto Label_00A0;
        Label_0017:
            num3 = 0x2000 * ((num2 / 0x80) + (bw128 * (num / 0x40)));
            num4 = 0x100 * tbl8bc[((num2 & 0x7f) / 0x10) + (8 * ((num & 0x3f) / 0x10))];
            num5 = (num & 15) / 4;
            num6 = 0x40 * num5;
            buffer2 = ((num5 & 1) == null) ? tbl8c0 : tbl8c1;
            num7 = (num2 & 15) + (0x10 * (num & 3));
            num8 = (((readoff + num3) + num4) + num6) + buffer2[num7];
            num9 = num2 + (cx * num);
            buffer[num9] = gsram[num8];
            num2 += 1;
        Label_00A0:
            if (num2 < cx)
            {
                goto Label_0017;
            }
            num += 1;
        Label_00AB:
            if (num < cy)
            {
                goto Label_0010;
            }
            return buffer;
        }

        public static byte[] Encode8(byte[] bin, int bw, int bh)
        {
            byte[] buffer;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            byte[] buffer2;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            buffer = new byte[(int) bin.Length];
            num = 0;
            goto Label_0113;
        Label_0010:
            num2 = 0;
            goto Label_0101;
        Label_0017:
            num3 = 0x2000 * ((num2 / 0x80) + (bw * (num / 0x40)));
            num4 = 0;
            goto Label_00F0;
        Label_0034:
            num5 = 0;
            goto Label_00DD;
        Label_003C:
            num6 = 0x100 * tbl8bc[(num5 / 0x10) + (8 * (num4 / 0x10))];
            num7 = 0;
            goto Label_00D1;
        Label_005C:
            num8 = 0x40 * num7;
            buffer2 = ((num7 & 1) == null) ? tbl8c0 : tbl8c1;
            num9 = 0;
            goto Label_00C5;
        Label_007C:
            num10 = ((num3 + num6) + num8) + buffer2[num9];
            num11 = (num2 + num5) + (num9 % 0x10);
            num12 = ((num + num4) + (4 * num7)) + (num9 / 0x10);
            num13 = num11 + ((0x80 * bw) * num12);
            buffer[num10] = bin[num13];
            num9 += 1;
        Label_00C5:
            if (num9 < 0x40)
            {
                goto Label_007C;
            }
            num7 += 1;
        Label_00D1:
            if (num7 < 4)
            {
                goto Label_005C;
            }
            num5 += 0x10;
        Label_00DD:
            if (num5 < 0x80)
            {
                goto Label_003C;
            }
            num4 += 0x10;
        Label_00F0:
            if (num4 < 0x40)
            {
                goto Label_0034;
            }
            num2 += 0x80;
        Label_0101:
            if (num2 < (0x80 * bw))
            {
                goto Label_0017;
            }
            num += 0x40;
        Label_0113:
            if (num < (0x40 * bh))
            {
                goto Label_0010;
            }
            return buffer;
        }

        public static void Encode8b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff, int bw128)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            byte[] buffer;
            int num9;
            int num10;
            int num11;
            num = 0;
            goto Label_00AF;
        Label_0007:
            num2 = dday + num;
            num3 = 0;
            goto Label_00A4;
        Label_0013:
            num4 = ddax + num3;
            num5 = 0x2000 * ((num4 / 0x80) + (bw128 * (num2 / 0x40)));
            num6 = 0x100 * tbl8bc[((num4 & 0x7f) / 0x10) + (8 * ((num2 & 0x3f) / 0x10))];
            num7 = (num2 & 15) / 4;
            num8 = 0x40 * num7;
            buffer = ((num7 & 1) == null) ? tbl8c0 : tbl8c1;
            num9 = (num4 & 15) + (0x10 * (num2 & 3));
            num10 = (((baseoff + num5) + num6) + num8) + buffer[num9];
            num11 = num3 + (rrw * num);
            gsram[num10] = src[num11];
            num3 += 1;
        Label_00A4:
            if (num3 < rrw)
            {
                goto Label_0013;
            }
            num += 1;
        Label_00AF:
            if (num < rrh)
            {
                goto Label_0007;
            }
            return;
        }
    }
}

