namespace vcBinTex4
{
    using System;

    public class Reform32
    {
        private static readonly byte[] tbl32bc = new byte[] { 
            0, 1, 4, 5, 0x10, 0x11, 20, 0x15, 2, 3, 6, 7, 0x12, 0x13, 0x16, 0x17, 
            8, 9, 12, 13, 0x18, 0x19, 0x1c, 0x1d, 10, 11, 14, 15, 0x1a, 0x1b, 30, 0x1f
         };
        private static readonly byte[] tbl32pao = new byte[] { 0, 1, 4, 5, 8, 9, 12, 13, 2, 3, 6, 7, 10, 11, 14, 15 };

        public static byte[] Decode32(byte[] bin, int bw, int bh)
        {
            byte[] buffer = new byte[bin.Length];
            for (int i = 0; i < (0x20 * bh); i += 0x20)
            {
                for (int j = 0; j < (0x40 * bw); j += 0x40)
                {
                    int num3 = 0x2000 * ((j / 0x40) + (bw * (i / 0x20)));
                    for (int k = 0; k < 0x20; k += 8)
                    {
                        for (int m = 0; m < 0x40; m += 8)
                        {
                            int num6 = 0x100 * tbl32bc[(m / 8) + ((k / 8) * 8)];
                            for (int n = 0; n < 4; n++)
                            {
                                int num8 = 0x40 * n;
                                for (int num9 = 0; num9 < 0x10; num9++)
                                {
                                    int num10 = (j + m) + (num9 % 8);
                                    int num11 = ((i + k) + (2 * n)) + (num9 / 8);
                                    int index = 4 * (num10 + ((0x40 * bw) * num11));
                                    int num13 = (((4 * tbl32pao[num9]) + num8) + num6) + num3;
                                    buffer[index] = bin[num13];
                                    buffer[index + 1] = bin[num13 + 1];
                                    buffer[index + 2] = bin[num13 + 2];
                                    buffer[index + 3] = bin[num13 + 3];
                                }
                            }
                        }
                    }
                }
            }
            return buffer;
        }

        public static byte[] Decode32c(byte[] gsram, int cx, int cy, int readoff, int bw64)
        {
            byte[] buffer = new byte[(4 * cx) * cy];
            for (int i = 0; i < cy; i++)
            {
                for (int j = 0; j < cx; j++)
                {
                    int num3 = 0x2000 * ((j / 0x40) + (bw64 * (i / 0x20)));
                    int num4 = 0x100 * tbl32bc[((j & 0x3f) / 8) + (((i & 0x1f) / 8) * 8)];
                    int num5 = (i & 7) / 2;
                    int num6 = 0x40 * num5;
                    int index = (j & 7) + (8 * (i & 1));
                    int num8 = 4 * (j + (cx * i));
                    int num9 = (((readoff + (4 * tbl32pao[index])) + num6) + num4) + num3;
                    buffer[num8] = gsram[num9];
                    buffer[num8 + 1] = gsram[num9 + 1];
                    buffer[num8 + 2] = gsram[num9 + 2];
                    buffer[num8 + 3] = gsram[num9 + 3];
                }
            }
            return buffer;
        }

        public static byte[] Encode32(byte[] bin, int bw, int bh)
        {
            byte[] buffer = new byte[bin.Length];
            for (int i = 0; i < (0x20 * bh); i += 0x20)
            {
                for (int j = 0; j < (0x40 * bw); j += 0x40)
                {
                    int num3 = 0x2000 * ((j / 0x40) + (bw * (i / 0x20)));
                    for (int k = 0; k < 0x20; k += 8)
                    {
                        for (int m = 0; m < 0x40; m += 8)
                        {
                            int num6 = 0x100 * tbl32bc[(m / 8) + ((k / 8) * 8)];
                            for (int n = 0; n < 4; n++)
                            {
                                int num8 = 0x40 * n;
                                for (int num9 = 0; num9 < 0x10; num9++)
                                {
                                    int num10 = (j + m) + (num9 % 8);
                                    int num11 = ((i + k) + (2 * n)) + (num9 / 8);
                                    int index = 4 * (num10 + ((0x40 * bw) * num11));
                                    int num13 = (((4 * tbl32pao[num9]) + num8) + num6) + num3;
                                    buffer[num13] = bin[index];
                                    buffer[num13 + 1] = bin[index + 1];
                                    buffer[num13 + 2] = bin[index + 2];
                                    buffer[num13 + 3] = bin[index + 3];
                                }
                            }
                        }
                    }
                }
            }
            return buffer;
        }

        public static void Encode32b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff, int bw64)
        {
            for (int i = 0; i < rrh; i++)
            {
                int num2 = dday + i;
                for (int j = 0; j < rrw; j++)
                {
                    int num4 = ddax + j;
                    int num5 = 0x2000 * ((num4 / 0x40) + (bw64 * (num2 / 0x20)));
                    int num6 = 0x100 * tbl32bc[((num4 & 0x3f) / 8) + (((num2 & 0x1f) / 8) * 8)];
                    int num7 = (num2 & 7) / 2;
                    int num8 = 0x40 * num7;
                    int index = (num4 & 7) + (8 * (num2 & 1));
                    int num10 = 4 * (j + (rrw * i));
                    int num11 = (((baseoff + (4 * tbl32pao[index])) + num8) + num6) + num5;
                    gsram[num11] = src[num10];
                    gsram[num11 + 1] = src[num10 + 1];
                    gsram[num11 + 2] = src[num10 + 2];
                    gsram[num11 + 3] = src[num10 + 3];
                }
            }
        }
    }
}

