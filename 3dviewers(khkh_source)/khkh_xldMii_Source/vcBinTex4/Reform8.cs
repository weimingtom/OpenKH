namespace vcBinTex4
{
    using System;

    public class Reform8
    {
        private static readonly byte[] tbl8bc = new byte[] { 
            0, 1, 4, 5, 0x10, 0x11, 20, 0x15, 2, 3, 6, 7, 0x12, 0x13, 0x16, 0x17, 
            8, 9, 12, 13, 0x18, 0x19, 0x1c, 0x1d, 10, 11, 14, 15, 0x1a, 0x1b, 30, 0x1f
         };
        private static readonly byte[] tbl8c0 = new byte[] { 
            0, 4, 0x10, 20, 0x20, 0x24, 0x30, 0x34, 2, 6, 0x12, 0x16, 0x22, 0x26, 50, 0x36, 
            8, 12, 0x18, 0x1c, 40, 0x2c, 0x38, 60, 10, 14, 0x1a, 30, 0x2a, 0x2e, 0x3a, 0x3e, 
            0x21, 0x25, 0x31, 0x35, 1, 5, 0x11, 0x15, 0x23, 0x27, 0x33, 0x37, 3, 7, 0x13, 0x17, 
            0x29, 0x2d, 0x39, 0x3d, 9, 13, 0x19, 0x1d, 0x2b, 0x2f, 0x3b, 0x3f, 11, 15, 0x1b, 0x1f
         };
        private static readonly byte[] tbl8c1 = new byte[] { 
            0x20, 0x24, 0x30, 0x34, 0, 4, 0x10, 20, 0x22, 0x26, 50, 0x36, 2, 6, 0x12, 0x16, 
            40, 0x2c, 0x38, 60, 8, 12, 0x18, 0x1c, 0x2a, 0x2e, 0x3a, 0x3e, 10, 14, 0x1a, 30, 
            1, 5, 0x11, 0x15, 0x21, 0x25, 0x31, 0x35, 3, 7, 0x13, 0x17, 0x23, 0x27, 0x33, 0x37, 
            9, 13, 0x19, 0x1d, 0x29, 0x2d, 0x39, 0x3d, 11, 15, 0x1b, 0x1f, 0x2b, 0x2f, 0x3b, 0x3f
         };

        public static byte[] Decode8(byte[] bin, int bw, int bh)
        {
            byte[] buffer = new byte[bin.Length];
            for (int i = 0; i < (0x40 * bh); i += 0x40)
            {
                for (int j = 0; j < (0x80 * bw); j += 0x80)
                {
                    int num3 = 0x2000 * ((j / 0x80) + (bw * (i / 0x40)));
                    for (int k = 0; k < 0x40; k += 0x10)
                    {
                        for (int m = 0; m < 0x80; m += 0x10)
                        {
                            int num6 = 0x100 * tbl8bc[(m / 0x10) + (8 * (k / 0x10))];
                            for (int n = 0; n < 4; n++)
                            {
                                int num8 = 0x40 * n;
                                byte[] buffer2 = ((n & 1) == 0) ? tbl8c0 : tbl8c1;
                                for (int num9 = 0; num9 < 0x40; num9++)
                                {
                                    int index = ((num3 + num6) + num8) + buffer2[num9];
                                    int num11 = (j + m) + (num9 % 0x10);
                                    int num12 = ((i + k) + (4 * n)) + (num9 / 0x10);
                                    int num13 = num11 + ((0x80 * bw) * num12);
                                    buffer[num13] = bin[index];
                                }
                            }
                        }
                    }
                }
            }
            return buffer;
        }

        public static byte[] Decode8c(byte[] gsram, int cx, int cy, int readoff, int bw128)
        {
            byte[] buffer = new byte[cx * cy];
            for (int i = 0; i < cy; i++)
            {
                for (int j = 0; j < cx; j++)
                {
                    int num3 = 0x2000 * ((j / 0x80) + (bw128 * (i / 0x40)));
                    int num4 = 0x100 * tbl8bc[((j & 0x7f) / 0x10) + (8 * ((i & 0x3f) / 0x10))];
                    int num5 = (i & 15) / 4;
                    int num6 = 0x40 * num5;
                    byte[] buffer2 = ((num5 & 1) == 0) ? tbl8c0 : tbl8c1;
                    int index = (j & 15) + (0x10 * (i & 3));
                    int num8 = (((readoff + num3) + num4) + num6) + buffer2[index];
                    int num9 = j + (cx * i);
                    buffer[num9] = gsram[num8];
                }
            }
            return buffer;
        }

        public static byte[] Encode8(byte[] bin, int bw, int bh)
        {
            byte[] buffer = new byte[bin.Length];
            for (int i = 0; i < (0x40 * bh); i += 0x40)
            {
                for (int j = 0; j < (0x80 * bw); j += 0x80)
                {
                    int num3 = 0x2000 * ((j / 0x80) + (bw * (i / 0x40)));
                    for (int k = 0; k < 0x40; k += 0x10)
                    {
                        for (int m = 0; m < 0x80; m += 0x10)
                        {
                            int num6 = 0x100 * tbl8bc[(m / 0x10) + (8 * (k / 0x10))];
                            for (int n = 0; n < 4; n++)
                            {
                                int num8 = 0x40 * n;
                                byte[] buffer2 = ((n & 1) == 0) ? tbl8c0 : tbl8c1;
                                for (int num9 = 0; num9 < 0x40; num9++)
                                {
                                    int index = ((num3 + num6) + num8) + buffer2[num9];
                                    int num11 = (j + m) + (num9 % 0x10);
                                    int num12 = ((i + k) + (4 * n)) + (num9 / 0x10);
                                    int num13 = num11 + ((0x80 * bw) * num12);
                                    buffer[index] = bin[num13];
                                }
                            }
                        }
                    }
                }
            }
            return buffer;
        }

        public static void Encode8b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff, int bw128)
        {
            for (int i = 0; i < rrh; i++)
            {
                int num2 = dday + i;
                for (int j = 0; j < rrw; j++)
                {
                    int num4 = ddax + j;
                    int num5 = 0x2000 * ((num4 / 0x80) + (bw128 * (num2 / 0x40)));
                    int num6 = 0x100 * tbl8bc[((num4 & 0x7f) / 0x10) + (8 * ((num2 & 0x3f) / 0x10))];
                    int num7 = (num2 & 15) / 4;
                    int num8 = 0x40 * num7;
                    byte[] buffer = ((num7 & 1) == 0) ? tbl8c0 : tbl8c1;
                    int index = (num4 & 15) + (0x10 * (num2 & 3));
                    int num10 = (((baseoff + num5) + num6) + num8) + buffer[index];
                    int num11 = j + (rrw * i);
                    gsram[num10] = src[num11];
                }
            }
        }
    }
}

