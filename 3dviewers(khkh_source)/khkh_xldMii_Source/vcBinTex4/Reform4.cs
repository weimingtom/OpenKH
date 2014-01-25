namespace vcBinTex4
{
    using System;

    public class Reform4
    {
        private static byte[] tbl4bc = new byte[] { 
            0, 2, 8, 10, 1, 3, 9, 11, 4, 6, 12, 14, 5, 7, 13, 15, 
            0x10, 0x12, 0x18, 0x1a, 0x11, 0x13, 0x19, 0x1b, 20, 0x16, 0x1c, 30, 0x15, 0x17, 0x1d, 0x1f
         };
        private static readonly int[] tbl4col0 = new int[] { 
            0, 0x20, 0x80, 160, 0x100, 0x120, 0x180, 0x1a0, 8, 40, 0x88, 0xa8, 0x108, 0x128, 0x188, 0x1a8, 
            0x10, 0x30, 0x90, 0xb0, 0x110, 0x130, 400, 0x1b0, 0x18, 0x38, 0x98, 0xb8, 280, 0x138, 0x198, 440, 
            0x40, 0x60, 0xc0, 0xe0, 320, 0x160, 0x1c0, 480, 0x48, 0x68, 200, 0xe8, 0x148, 360, 0x1c8, 0x1e8, 
            80, 0x70, 0xd0, 240, 0x150, 0x170, 0x1d0, 0x1f0, 0x58, 120, 0xd8, 0xf8, 0x158, 0x178, 0x1d8, 0x1f8, 
            260, 0x124, 0x184, 420, 4, 0x24, 0x84, 0xa4, 0x10c, 300, 0x18c, 0x1ac, 12, 0x2c, 140, 0xac, 
            0x114, 0x134, 0x194, 0x1b4, 20, 0x34, 0x94, 180, 0x11c, 0x13c, 0x19c, 0x1bc, 0x1c, 60, 0x9c, 0xbc, 
            0x144, 0x164, 0x1c4, 0x1e4, 0x44, 100, 0xc4, 0xe4, 0x14c, 0x16c, 460, 0x1ec, 0x4c, 0x6c, 0xcc, 0xec, 
            340, 0x174, 0x1d4, 500, 0x54, 0x74, 0xd4, 0xf4, 0x15c, 380, 0x1dc, 0x1fc, 0x5c, 0x7c, 220, 0xfc
         };
        private static readonly int[] tbl4col1 = new int[] { 
            0x100, 0x120, 0x180, 0x1a0, 0, 0x20, 0x80, 160, 0x108, 0x128, 0x188, 0x1a8, 8, 40, 0x88, 0xa8, 
            0x110, 0x130, 400, 0x1b0, 0x10, 0x30, 0x90, 0xb0, 280, 0x138, 0x198, 440, 0x18, 0x38, 0x98, 0xb8, 
            320, 0x160, 0x1c0, 480, 0x40, 0x60, 0xc0, 0xe0, 0x148, 360, 0x1c8, 0x1e8, 0x48, 0x68, 200, 0xe8, 
            0x150, 0x170, 0x1d0, 0x1f0, 80, 0x70, 0xd0, 240, 0x158, 0x178, 0x1d8, 0x1f8, 0x58, 120, 0xd8, 0xf8, 
            4, 0x24, 0x84, 0xa4, 260, 0x124, 0x184, 420, 12, 0x2c, 140, 0xac, 0x10c, 300, 0x18c, 0x1ac, 
            20, 0x34, 0x94, 180, 0x114, 0x134, 0x194, 0x1b4, 0x1c, 60, 0x9c, 0xbc, 0x11c, 0x13c, 0x19c, 0x1bc, 
            0x44, 100, 0xc4, 0xe4, 0x144, 0x164, 0x1c4, 0x1e4, 0x4c, 0x6c, 0xcc, 0xec, 0x14c, 0x16c, 460, 0x1ec, 
            0x54, 0x74, 0xd4, 0xf4, 340, 0x174, 0x1d4, 500, 0x5c, 0x7c, 220, 0xfc, 0x15c, 380, 0x1dc, 0x1fc
         };

        public static byte[] Decode4(byte[] bin, int bw, int bh)
        {
            byte[] buffer = new byte[bin.Length];
            for (int i = 0; i < (0x80 * bh); i += 0x80)
            {
                for (int j = 0; j < (0x80 * bw); j += 0x80)
                {
                    int num3 = 0x2000 * ((j / 0x80) + (bw * (i / 0x80)));
                    for (int k = 0; k < 0x80; k += 0x10)
                    {
                        for (int m = 0; m < 0x80; m += 0x20)
                        {
                            int num6 = 0x100 * tbl4bc[(m / 0x20) + (4 * (k / 0x10))];
                            for (int n = 0; n < 4; n++)
                            {
                                int num8 = 0x40 * n;
                                int[] numArray = ((n & 1) == 0) ? tbl4col0 : tbl4col1;
                                for (int num9 = 0; num9 < 0x80; num9++)
                                {
                                    int num10 = numArray[num9] / 8;
                                    int num11 = numArray[num9] % 8;
                                    byte num12 = (byte) ((bin[((num3 + num6) + num8) + num10] >> num11) & 15);
                                    int num13 = (j + m) + (num9 % 0x20);
                                    int num14 = ((i + k) + (4 * n)) + (num9 / 0x20);
                                    int num15 = num13 + ((0x80 * bw) * num14);
                                    byte num16 = buffer[num15 / 2];
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
                                    buffer[num15 / 2] = num16;
                                }
                            }
                        }
                    }
                }
            }
            return buffer;
        }

        public static byte[] Decode4c(byte[] gsram, int cx, int cy, int readoff, int bw128)
        {
            byte[] buffer = new byte[((cx * cy) + 1) / 2];
            for (int i = 0; i < cy; i++)
            {
                for (int j = 0; j < cx; j++)
                {
                    int num3 = 0x2000 * ((j / 0x80) + (bw128 * (i / 0x80)));
                    int num4 = 0x100 * tbl4bc[((j & 0x7f) / 0x20) + (4 * ((i & 0x7f) / 0x10))];
                    int num5 = (i & 15) / 4;
                    int num6 = 0x40 * num5;
                    int[] numArray = ((num5 & 1) == 0) ? tbl4col0 : tbl4col1;
                    int index = (j & 0x1f) + (0x20 * (i & 3));
                    int num8 = numArray[index] / 8;
                    int num9 = numArray[index] % 8;
                    byte num10 = (byte) ((gsram[(((readoff + num3) + num4) + num6) + num8] >> num9) & 15);
                    int num11 = j + (cx * i);
                    byte num12 = buffer[num11 / 2];
                    if ((num11 & 1) == 0)
                    {
                        num12 = (byte) (num12 & 15);
                        num12 = (byte) (num12 | ((byte) (num10 << 4)));
                    }
                    else
                    {
                        num12 = (byte) (num12 & 240);
                        num12 = (byte) (num12 | num10);
                    }
                    buffer[num11 / 2] = num12;
                }
            }
            return buffer;
        }

        public static byte[] Encode4(byte[] bin, int bw, int bh)
        {
            byte[] buffer = new byte[bin.Length];
            for (int i = 0; i < (0x80 * bh); i += 0x80)
            {
                for (int j = 0; j < (0x80 * bw); j += 0x80)
                {
                    int num3 = 0x2000 * ((j / 0x80) + (bw * (i / 0x80)));
                    for (int k = 0; k < 0x80; k += 0x10)
                    {
                        for (int m = 0; m < 0x80; m += 0x20)
                        {
                            int num6 = 0x100 * tbl4bc[(m / 0x20) + (4 * (k / 0x10))];
                            for (int n = 0; n < 4; n++)
                            {
                                int num8 = 0x40 * n;
                                int[] numArray = ((n & 1) == 0) ? tbl4col0 : tbl4col1;
                                for (int num9 = 0; num9 < 0x80; num9++)
                                {
                                    int num10 = (j + m) + (num9 % 0x20);
                                    int num11 = ((i + k) + (4 * n)) + (num9 / 0x20);
                                    int num12 = num10 + ((0x80 * bw) * num11);
                                    byte num13 = bin[num12 / 2];
                                    if ((num12 & 1) != 0)
                                    {
                                        num13 = (byte) (num13 & 15);
                                    }
                                    else
                                    {
                                        num13 = (byte) (num13 >> 4);
                                    }
                                    int num14 = numArray[num9] / 8;
                                    int num15 = numArray[num9] % 8;
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

        public static void Encode4b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff, int bw128)
        {
            for (int i = 0; i < rrh; i++)
            {
                int num2 = dday + i;
                for (int j = 0; j < rrw; j++)
                {
                    int num4 = ddax + j;
                    int num5 = 0x2000 * ((num4 / 0x80) + (bw128 * (num2 / 0x80)));
                    int num6 = 0x100 * tbl4bc[((num4 & 0x7f) / 0x20) + (4 * ((num2 & 0x7f) / 0x10))];
                    int num7 = (num2 & 15) / 4;
                    int num8 = 0x40 * num7;
                    int[] numArray = ((num7 & 1) == 0) ? tbl4col0 : tbl4col1;
                    int index = (num4 & 0x1f) + (0x20 * (num2 & 3));
                    int num10 = j + (rrw * i);
                    byte num11 = src[num10 / 2];
                    if ((num10 & 1) != 0)
                    {
                        num11 = (byte) (num11 & 15);
                    }
                    else
                    {
                        num11 = (byte) (num11 >> 4);
                    }
                    int num12 = numArray[index] / 8;
                    int num13 = numArray[index] % 8;
                    int num14 = (((baseoff + num5) + num6) + num8) + num12;
                    byte num15 = gsram[num14];
                    switch (num13)
                    {
                        case 0:
                            num15 = (byte) (num15 & 240);
                            num15 = (byte) (num15 | num11);
                            break;

                        case 4:
                            num15 = (byte) (num15 & 15);
                            num15 = (byte) (num15 | ((byte) (num11 << 4)));
                            break;
                    }
                    gsram[num14] = num15;
                }
            }
        }
    }
}

