namespace vcBinTex4
{
    using System;

    public class Reform4
    {
        private static byte[] tbl4bc;
        private static readonly int[] tbl4col0;
        private static readonly int[] tbl4col1;

        static Reform4()
        {
            tbl4bc = new byte[] { 
                0, 2, 8, 10, 1, 3, 9, 11, 4, 6, 12, 14, 5, 7, 13, 15, 
                0x10, 0x12, 0x18, 0x1a, 0x11, 0x13, 0x19, 0x1b, 20, 0x16, 0x1c, 30, 0x15, 0x17, 0x1d, 0x1f
             };
            tbl4col0 = new int[] { 
                0, 0x20, 0x80, 160, 0x100, 0x120, 0x180, 0x1a0, 8, 40, 0x88, 0xa8, 0x108, 0x128, 0x188, 0x1a8, 
                0x10, 0x30, 0x90, 0xb0, 0x110, 0x130, 400, 0x1b0, 0x18, 0x38, 0x98, 0xb8, 280, 0x138, 0x198, 440, 
                0x40, 0x60, 0xc0, 0xe0, 320, 0x160, 0x1c0, 480, 0x48, 0x68, 200, 0xe8, 0x148, 360, 0x1c8, 0x1e8, 
                80, 0x70, 0xd0, 240, 0x150, 0x170, 0x1d0, 0x1f0, 0x58, 120, 0xd8, 0xf8, 0x158, 0x178, 0x1d8, 0x1f8, 
                260, 0x124, 0x184, 420, 4, 0x24, 0x84, 0xa4, 0x10c, 300, 0x18c, 0x1ac, 12, 0x2c, 140, 0xac, 
                0x114, 0x134, 0x194, 0x1b4, 20, 0x34, 0x94, 180, 0x11c, 0x13c, 0x19c, 0x1bc, 0x1c, 60, 0x9c, 0xbc, 
                0x144, 0x164, 0x1c4, 0x1e4, 0x44, 100, 0xc4, 0xe4, 0x14c, 0x16c, 460, 0x1ec, 0x4c, 0x6c, 0xcc, 0xec, 
                340, 0x174, 0x1d4, 500, 0x54, 0x74, 0xd4, 0xf4, 0x15c, 380, 0x1dc, 0x1fc, 0x5c, 0x7c, 220, 0xfc
             };
            tbl4col1 = new int[] { 
                0x100, 0x120, 0x180, 0x1a0, 0, 0x20, 0x80, 160, 0x108, 0x128, 0x188, 0x1a8, 8, 40, 0x88, 0xa8, 
                0x110, 0x130, 400, 0x1b0, 0x10, 0x30, 0x90, 0xb0, 280, 0x138, 0x198, 440, 0x18, 0x38, 0x98, 0xb8, 
                320, 0x160, 0x1c0, 480, 0x40, 0x60, 0xc0, 0xe0, 0x148, 360, 0x1c8, 0x1e8, 0x48, 0x68, 200, 0xe8, 
                0x150, 0x170, 0x1d0, 0x1f0, 80, 0x70, 0xd0, 240, 0x158, 0x178, 0x1d8, 0x1f8, 0x58, 120, 0xd8, 0xf8, 
                4, 0x24, 0x84, 0xa4, 260, 0x124, 0x184, 420, 12, 0x2c, 140, 0xac, 0x10c, 300, 0x18c, 0x1ac, 
                20, 0x34, 0x94, 180, 0x114, 0x134, 0x194, 0x1b4, 0x1c, 60, 0x9c, 0xbc, 0x11c, 0x13c, 0x19c, 0x1bc, 
                0x44, 100, 0xc4, 0xe4, 0x144, 0x164, 0x1c4, 0x1e4, 0x4c, 0x6c, 0xcc, 0xec, 0x14c, 0x16c, 460, 0x1ec, 
                0x54, 0x74, 0xd4, 0xf4, 340, 0x174, 0x1d4, 500, 0x5c, 0x7c, 220, 0xfc, 0x15c, 380, 0x1dc, 0x1fc
             };
            return;
        }

        public Reform4()
        {
            base..ctor();
            return;
        }

        public static byte[] Decode4(byte[] bin, int bw, int bh)
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
            int[] numArray;
            int num9;
            int num10;
            int num11;
            byte num12;
            int num13;
            int num14;
            int num15;
            byte num16;
            buffer = new byte[(int) bin.Length];
            num = 0;
            goto Label_017C;
        Label_0010:
            num2 = 0;
            goto Label_0167;
        Label_0017:
            num3 = 0x2000 * ((num2 / 0x80) + (bw * (num / 0x80)));
            num4 = 0;
            goto Label_0153;
        Label_0037:
            num5 = 0;
            goto Label_0140;
        Label_003F:
            num6 = 0x100 * tbl4bc[(num5 / 0x20) + (4 * (num4 / 0x10))];
            num7 = 0;
            goto Label_0131;
        Label_0062:
            num8 = 0x40 * num7;
            numArray = ((num7 & 1) == null) ? tbl4col0 : tbl4col1;
            num9 = 0;
            goto Label_011F;
        Label_0085:
            num10 = numArray[num9] / 8;
            num11 = numArray[num9] % 8;
            num12 = (byte) ((bin[((num3 + num6) + num8) + num10] >> (num11 & 0x1f)) & 15);
            num13 = (num2 + num5) + (num9 % 0x20);
            num14 = ((num + num4) + (4 * num7)) + (num9 / 0x20);
            num15 = num13 + ((0x80 * bw) * num14);
            num16 = buffer[num15 / 2];
            if ((num15 & 1) != null)
            {
                goto Label_00FE;
            }
            num16 = (byte) (num16 & 15);
            num16 = (byte) (num16 | ((byte) (num12 << 4)));
            goto Label_0111;
        Label_00FE:
            num16 = (byte) (num16 & 240);
            num16 = (byte) (num16 | num12);
        Label_0111:
            buffer[num15 / 2] = num16;
            num9 += 1;
        Label_011F:
            if (num9 < 0x80)
            {
                goto Label_0085;
            }
            num7 += 1;
        Label_0131:
            if (num7 < 4)
            {
                goto Label_0062;
            }
            num5 += 0x20;
        Label_0140:
            if (num5 < 0x80)
            {
                goto Label_003F;
            }
            num4 += 0x10;
        Label_0153:
            if (num4 < 0x80)
            {
                goto Label_0037;
            }
            num2 += 0x80;
        Label_0167:
            if (num2 < (0x80 * bw))
            {
                goto Label_0017;
            }
            num += 0x80;
        Label_017C:
            if (num < (0x80 * bh))
            {
                goto Label_0010;
            }
            return buffer;
        }

        public static byte[] Decode4c(byte[] gsram, int cx, int cy, int readoff, int bw128)
        {
            byte[] buffer;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int[] numArray;
            int num7;
            int num8;
            int num9;
            byte num10;
            int num11;
            byte num12;
            buffer = new byte[((cx * cy) + 1) / 2];
            num = 0;
            goto Label_0103;
        Label_0014:
            num2 = 0;
            goto Label_00F8;
        Label_001B:
            num3 = 0x2000 * ((num2 / 0x80) + (bw128 * (num / 0x80)));
            num4 = 0x100 * tbl4bc[((num2 & 0x7f) / 0x20) + (4 * ((num & 0x7f) / 0x10))];
            num5 = (num & 15) / 4;
            num6 = 0x40 * num5;
            numArray = ((num5 & 1) == null) ? tbl4col0 : tbl4col1;
            num7 = (num2 & 0x1f) + (0x20 * (num & 3));
            num8 = numArray[num7] / 8;
            num9 = numArray[num7] % 8;
            num10 = (byte) ((gsram[(((readoff + num3) + num4) + num6) + num8] >> (num9 & 0x1f)) & 15);
            num11 = num2 + (cx * num);
            num12 = buffer[num11 / 2];
            if ((num11 & 1) != null)
            {
                goto Label_00D9;
            }
            num12 = (byte) (num12 & 15);
            num12 = (byte) (num12 | ((byte) (num10 << 4)));
            goto Label_00EC;
        Label_00D9:
            num12 = (byte) (num12 & 240);
            num12 = (byte) (num12 | num10);
        Label_00EC:
            buffer[num11 / 2] = num12;
            num2 += 1;
        Label_00F8:
            if (num2 < cx)
            {
                goto Label_001B;
            }
            num += 1;
        Label_0103:
            if (num < cy)
            {
                goto Label_0014;
            }
            return buffer;
        }

        public static byte[] Encode4(byte[] bin, int bw, int bh)
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
            int[] numArray;
            int num9;
            int num10;
            int num11;
            int num12;
            byte num13;
            int num14;
            int num15;
            int num16;
            byte num17;
            buffer = new byte[(int) bin.Length];
            num = 0;
            goto Label_018E;
        Label_0010:
            num2 = 0;
            goto Label_0179;
        Label_0017:
            num3 = 0x2000 * ((num2 / 0x80) + (bw * (num / 0x80)));
            num4 = 0;
            goto Label_0165;
        Label_0037:
            num5 = 0;
            goto Label_0152;
        Label_003F:
            num6 = 0x100 * tbl4bc[(num5 / 0x20) + (4 * (num4 / 0x10))];
            num7 = 0;
            goto Label_0143;
        Label_0062:
            num8 = 0x40 * num7;
            numArray = ((num7 & 1) == null) ? tbl4col0 : tbl4col1;
            num9 = 0;
            goto Label_0131;
        Label_0085:
            num10 = (num2 + num5) + (num9 % 0x20);
            num11 = ((num + num4) + (4 * num7)) + (num9 / 0x20);
            num12 = num10 + ((0x80 * bw) * num11);
            num13 = bin[num12 / 2];
            if ((num12 & 1) == null)
            {
                goto Label_00C9;
            }
            num13 = (byte) (num13 & 15);
            goto Label_00D0;
        Label_00C9:
            num13 = (byte) (num13 >> 4);
        Label_00D0:
            num14 = numArray[num9] / 8;
            num15 = numArray[num9] % 8;
            num16 = ((num3 + num6) + num8) + num14;
            num17 = buffer[num16];
            if (num15 != null)
            {
                goto Label_010D;
            }
            num17 = (byte) (num17 & 240);
            num17 = (byte) (num17 | num13);
            goto Label_0125;
        Label_010D:
            if (num15 != 4)
            {
                goto Label_0125;
            }
            num17 = (byte) (num17 & 15);
            num17 = (byte) (num17 | ((byte) (num13 << 4)));
        Label_0125:
            buffer[num16] = num17;
            num9 += 1;
        Label_0131:
            if (num9 < 0x80)
            {
                goto Label_0085;
            }
            num7 += 1;
        Label_0143:
            if (num7 < 4)
            {
                goto Label_0062;
            }
            num5 += 0x20;
        Label_0152:
            if (num5 < 0x80)
            {
                goto Label_003F;
            }
            num4 += 0x10;
        Label_0165:
            if (num4 < 0x80)
            {
                goto Label_0037;
            }
            num2 += 0x80;
        Label_0179:
            if (num2 < (0x80 * bw))
            {
                goto Label_0017;
            }
            num += 0x80;
        Label_018E:
            if (num < (0x80 * bh))
            {
                goto Label_0010;
            }
            return buffer;
        }

        public static void Encode4b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff, int bw128)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int[] numArray;
            int num9;
            int num10;
            byte num11;
            int num12;
            int num13;
            int num14;
            byte num15;
            num = 0;
            goto Label_0115;
        Label_0007:
            num2 = dday + num;
            num3 = 0;
            goto Label_010A;
        Label_0013:
            num4 = ddax + num3;
            num5 = 0x2000 * ((num4 / 0x80) + (bw128 * (num2 / 0x80)));
            num6 = 0x100 * tbl4bc[((num4 & 0x7f) / 0x20) + (4 * ((num2 & 0x7f) / 0x10))];
            num7 = (num2 & 15) / 4;
            num8 = 0x40 * num7;
            numArray = ((num7 & 1) == null) ? tbl4col0 : tbl4col1;
            num9 = (num4 & 0x1f) + (0x20 * (num2 & 3));
            num10 = num3 + (rrw * num);
            num11 = src[num10 / 2];
            if ((num10 & 1) == null)
            {
                goto Label_00A0;
            }
            num11 = (byte) (num11 & 15);
            goto Label_00A7;
        Label_00A0:
            num11 = (byte) (num11 >> 4);
        Label_00A7:
            num12 = numArray[num9] / 8;
            num13 = numArray[num9] % 8;
            num14 = (((baseoff + num5) + num6) + num8) + num12;
            num15 = gsram[num14];
            if (num13 != null)
            {
                goto Label_00E8;
            }
            num15 = (byte) (num15 & 240);
            num15 = (byte) (num15 | num11);
            goto Label_0100;
        Label_00E8:
            if (num13 != 4)
            {
                goto Label_0100;
            }
            num15 = (byte) (num15 & 15);
            num15 = (byte) (num15 | ((byte) (num11 << 4)));
        Label_0100:
            gsram[num14] = num15;
            num3 += 1;
        Label_010A:
            if (num3 < rrw)
            {
                goto Label_0013;
            }
            num += 1;
        Label_0115:
            if (num < rrh)
            {
                goto Label_0007;
            }
            return;
        }
    }
}

