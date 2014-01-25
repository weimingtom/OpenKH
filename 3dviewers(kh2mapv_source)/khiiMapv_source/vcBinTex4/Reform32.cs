namespace vcBinTex4
{
    using System;

    public class Reform32
    {
        private static readonly byte[] tbl32bc;
        private static readonly byte[] tbl32pao;

        static Reform32()
        {
            tbl32pao = new byte[] { 0, 1, 4, 5, 8, 9, 12, 13, 2, 3, 6, 7, 10, 11, 14, 15 };
            tbl32bc = new byte[] { 
                0, 1, 4, 5, 0x10, 0x11, 20, 0x15, 2, 3, 6, 7, 0x12, 0x13, 0x16, 0x17, 
                8, 9, 12, 13, 0x18, 0x19, 0x1c, 0x1d, 10, 11, 14, 15, 0x1a, 0x1b, 30, 0x1f
             };
            return;
        }

        public Reform32()
        {
            base..ctor();
            return;
        }

        public static byte[] Decode32(byte[] bin, int bw, int bh)
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
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            buffer = new byte[(int) bin.Length];
            num = 0;
            goto Label_011B;
        Label_0010:
            num2 = 0;
            goto Label_010C;
        Label_0017:
            num3 = 0x2000 * ((num2 / 0x40) + (bw * (num / 0x20)));
            num4 = 0;
            goto Label_00FE;
        Label_0031:
            num5 = 0;
            goto Label_00EF;
        Label_0039:
            num6 = 0x100 * tbl32bc[(num5 / 8) + ((num4 / 8) * 8)];
            num7 = 0;
            goto Label_00E1;
        Label_005A:
            num8 = 0x40 * num7;
            num9 = 0;
            goto Label_00D5;
        Label_0066:
            num10 = (num2 + num5) + (num9 % 8);
            num11 = ((num + num4) + (2 * num7)) + (num9 / 8);
            num12 = 4 * (num10 + ((0x40 * bw) * num11));
            num13 = (((4 * tbl32pao[num9]) + num8) + num6) + num3;
            buffer[num12] = bin[num13];
            buffer[num12 + 1] = bin[num13 + 1];
            buffer[num12 + 2] = bin[num13 + 2];
            buffer[num12 + 3] = bin[num13 + 3];
            num9 += 1;
        Label_00D5:
            if (num9 < 0x10)
            {
                goto Label_0066;
            }
            num7 += 1;
        Label_00E1:
            if (num7 < 4)
            {
                goto Label_005A;
            }
            num5 += 8;
        Label_00EF:
            if (num5 < 0x40)
            {
                goto Label_0039;
            }
            num4 += 8;
        Label_00FE:
            if (num4 < 0x20)
            {
                goto Label_0031;
            }
            num2 += 0x40;
        Label_010C:
            if (num2 < (0x40 * bw))
            {
                goto Label_0017;
            }
            num += 0x20;
        Label_011B:
            if (num < (0x20 * bh))
            {
                goto Label_0010;
            }
            return buffer;
        }

        public static byte[] Decode32c(byte[] gsram, int cx, int cy, int readoff, int bw64)
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
            int num9;
            buffer = new byte[(4 * cx) * cy];
            num = 0;
            goto Label_00BC;
        Label_0012:
            num2 = 0;
            goto Label_00B1;
        Label_0019:
            num3 = 0x2000 * ((num2 / 0x40) + (bw64 * (num / 0x20)));
            num4 = 0x100 * tbl32bc[((num2 & 0x3f) / 8) + (((num & 0x1f) / 8) * 8)];
            num5 = (num & 7) / 2;
            num6 = 0x40 * num5;
            num7 = (num2 & 7) + (8 * (num & 1));
            num8 = 4 * (num2 + (cx * num));
            num9 = (((readoff + (4 * tbl32pao[num7])) + num6) + num4) + num3;
            buffer[num8] = gsram[num9];
            buffer[num8 + 1] = gsram[num9 + 1];
            buffer[num8 + 2] = gsram[num9 + 2];
            buffer[num8 + 3] = gsram[num9 + 3];
            num2 += 1;
        Label_00B1:
            if (num2 < cx)
            {
                goto Label_0019;
            }
            num += 1;
        Label_00BC:
            if (num < cy)
            {
                goto Label_0012;
            }
            return buffer;
        }

        public static byte[] Encode32(byte[] bin, int bw, int bh)
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
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            buffer = new byte[(int) bin.Length];
            num = 0;
            goto Label_011B;
        Label_0010:
            num2 = 0;
            goto Label_010C;
        Label_0017:
            num3 = 0x2000 * ((num2 / 0x40) + (bw * (num / 0x20)));
            num4 = 0;
            goto Label_00FE;
        Label_0031:
            num5 = 0;
            goto Label_00EF;
        Label_0039:
            num6 = 0x100 * tbl32bc[(num5 / 8) + ((num4 / 8) * 8)];
            num7 = 0;
            goto Label_00E1;
        Label_005A:
            num8 = 0x40 * num7;
            num9 = 0;
            goto Label_00D5;
        Label_0066:
            num10 = (num2 + num5) + (num9 % 8);
            num11 = ((num + num4) + (2 * num7)) + (num9 / 8);
            num12 = 4 * (num10 + ((0x40 * bw) * num11));
            num13 = (((4 * tbl32pao[num9]) + num8) + num6) + num3;
            buffer[num13] = bin[num12];
            buffer[num13 + 1] = bin[num12 + 1];
            buffer[num13 + 2] = bin[num12 + 2];
            buffer[num13 + 3] = bin[num12 + 3];
            num9 += 1;
        Label_00D5:
            if (num9 < 0x10)
            {
                goto Label_0066;
            }
            num7 += 1;
        Label_00E1:
            if (num7 < 4)
            {
                goto Label_005A;
            }
            num5 += 8;
        Label_00EF:
            if (num5 < 0x40)
            {
                goto Label_0039;
            }
            num4 += 8;
        Label_00FE:
            if (num4 < 0x20)
            {
                goto Label_0031;
            }
            num2 += 0x40;
        Label_010C:
            if (num2 < (0x40 * bw))
            {
                goto Label_0017;
            }
            num += 0x20;
        Label_011B:
            if (num < (0x20 * bh))
            {
                goto Label_0010;
            }
            return buffer;
        }

        public static void Encode32b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff, int bw64)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            num = 0;
            goto Label_00BE;
        Label_0007:
            num2 = dday + num;
            num3 = 0;
            goto Label_00B3;
        Label_0013:
            num4 = ddax + num3;
            num5 = 0x2000 * ((num4 / 0x40) + (bw64 * (num2 / 0x20)));
            num6 = 0x100 * tbl32bc[((num4 & 0x3f) / 8) + (((num2 & 0x1f) / 8) * 8)];
            num7 = (num2 & 7) / 2;
            num8 = 0x40 * num7;
            num9 = (num4 & 7) + (8 * (num2 & 1));
            num10 = 4 * (num3 + (rrw * num));
            num11 = (((baseoff + (4 * tbl32pao[num9])) + num8) + num6) + num5;
            gsram[num11] = src[num10];
            gsram[num11 + 1] = src[num10 + 1];
            gsram[num11 + 2] = src[num10 + 2];
            gsram[num11 + 3] = src[num10 + 3];
            num3 += 1;
        Label_00B3:
            if (num3 < rrw)
            {
                goto Label_0013;
            }
            num += 1;
        Label_00BE:
            if (num < rrh)
            {
                goto Label_0007;
            }
            return;
        }
    }
}

