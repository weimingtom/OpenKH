namespace vcBinTex4
{
    public class Reform32
    {
        private static readonly byte[] tbl32pao =
        {
            0,
            1,
            4,
            5,
            8,
            9,
            12,
            13,
            2,
            3,
            6,
            7,
            10,
            11,
            14,
            15
        };

        private static readonly byte[] tbl32bc =
        {
            0,
            1,
            4,
            5,
            16,
            17,
            20,
            21,
            2,
            3,
            6,
            7,
            18,
            19,
            22,
            23,
            8,
            9,
            12,
            13,
            24,
            25,
            28,
            29,
            10,
            11,
            14,
            15,
            26,
            27,
            30,
            31
        };

        public static byte[] Encode32(byte[] bin, int bw, int bh)
        {
            var array = new byte[bin.Length];
            for (int i = 0; i < 32*bh; i += 32)
            {
                for (int j = 0; j < 64*bw; j += 64)
                {
                    int num = 8192*(j/64 + bw*(i/32));
                    for (int k = 0; k < 32; k += 8)
                    {
                        for (int l = 0; l < 64; l += 8)
                        {
                            int num2 = 256*tbl32bc[l/8 + k/8*8];
                            for (int m = 0; m < 4; m++)
                            {
                                int num3 = 64*m;
                                for (int n = 0; n < 16; n++)
                                {
                                    int num4 = j + l + n%8;
                                    int num5 = i + k + 2*m + n/8;
                                    int num6 = 4*(num4 + 64*bw*num5);
                                    int num7 = 4*tbl32pao[n] + num3 + num2 + num;
                                    array[num7] = bin[num6];
                                    array[num7 + 1] = bin[num6 + 1];
                                    array[num7 + 2] = bin[num6 + 2];
                                    array[num7 + 3] = bin[num6 + 3];
                                }
                            }
                        }
                    }
                }
            }
            return array;
        }

        public static byte[] Decode32(byte[] bin, int bw, int bh)
        {
            var array = new byte[bin.Length];
            for (int i = 0; i < 32*bh; i += 32)
            {
                for (int j = 0; j < 64*bw; j += 64)
                {
                    int num = 8192*(j/64 + bw*(i/32));
                    for (int k = 0; k < 32; k += 8)
                    {
                        for (int l = 0; l < 64; l += 8)
                        {
                            int num2 = 256*tbl32bc[l/8 + k/8*8];
                            for (int m = 0; m < 4; m++)
                            {
                                int num3 = 64*m;
                                for (int n = 0; n < 16; n++)
                                {
                                    int num4 = j + l + n%8;
                                    int num5 = i + k + 2*m + n/8;
                                    int num6 = 4*(num4 + 64*bw*num5);
                                    int num7 = 4*tbl32pao[n] + num3 + num2 + num;
                                    array[num6] = bin[num7];
                                    array[num6 + 1] = bin[num7 + 1];
                                    array[num6 + 2] = bin[num7 + 2];
                                    array[num6 + 3] = bin[num7 + 3];
                                }
                            }
                        }
                    }
                }
            }
            return array;
        }

        public static void Encode32b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff,
            int bw64)
        {
            for (int i = 0; i < rrh; i++)
            {
                int num = dday + i;
                for (int j = 0; j < rrw; j++)
                {
                    int num2 = ddax + j;
                    int num3 = 8192*(num2/64 + bw64*(num/32));
                    int num4 = 256*tbl32bc[(num2 & 63)/8 + (num & 31)/8*8];
                    int num5 = (num & 7)/2;
                    int num6 = 64*num5;
                    int num7 = (num2 & 7) + 8*(num & 1);
                    int num8 = 4*(j + rrw*i);
                    int num9 = baseoff + 4*tbl32pao[num7] + num6 + num4 + num3;
                    gsram[num9] = src[num8];
                    gsram[num9 + 1] = src[num8 + 1];
                    gsram[num9 + 2] = src[num8 + 2];
                    gsram[num9 + 3] = src[num8 + 3];
                }
            }
        }

        public static byte[] Decode32c(byte[] gsram, int cx, int cy, int readoff, int bw64)
        {
            var array = new byte[4*cx*cy];
            for (int i = 0; i < cy; i++)
            {
                for (int j = 0; j < cx; j++)
                {
                    int num = 8192*(j/64 + bw64*(i/32));
                    int num2 = 256*tbl32bc[(j & 63)/8 + (i & 31)/8*8];
                    int num3 = (i & 7)/2;
                    int num4 = 64*num3;
                    int num5 = (j & 7) + 8*(i & 1);
                    int num6 = 4*(j + cx*i);
                    int num7 = readoff + 4*tbl32pao[num5] + num4 + num2 + num;
                    array[num6] = gsram[num7];
                    array[num6 + 1] = gsram[num7 + 1];
                    array[num6 + 2] = gsram[num7 + 2];
                    array[num6 + 3] = gsram[num7 + 3];
                }
            }
            return array;
        }
    }
}