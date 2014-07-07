namespace vcBinTex4
{
    public class Reform8
    {
        private static readonly byte[] tbl8bc =
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

        private static readonly byte[] tbl8c0 =
        {
            0,
            4,
            16,
            20,
            32,
            36,
            48,
            52,
            2,
            6,
            18,
            22,
            34,
            38,
            50,
            54,
            8,
            12,
            24,
            28,
            40,
            44,
            56,
            60,
            10,
            14,
            26,
            30,
            42,
            46,
            58,
            62,
            33,
            37,
            49,
            53,
            1,
            5,
            17,
            21,
            35,
            39,
            51,
            55,
            3,
            7,
            19,
            23,
            41,
            45,
            57,
            61,
            9,
            13,
            25,
            29,
            43,
            47,
            59,
            63,
            11,
            15,
            27,
            31
        };

        private static readonly byte[] tbl8c1 =
        {
            32,
            36,
            48,
            52,
            0,
            4,
            16,
            20,
            34,
            38,
            50,
            54,
            2,
            6,
            18,
            22,
            40,
            44,
            56,
            60,
            8,
            12,
            24,
            28,
            42,
            46,
            58,
            62,
            10,
            14,
            26,
            30,
            1,
            5,
            17,
            21,
            33,
            37,
            49,
            53,
            3,
            7,
            19,
            23,
            35,
            39,
            51,
            55,
            9,
            13,
            25,
            29,
            41,
            45,
            57,
            61,
            11,
            15,
            27,
            31,
            43,
            47,
            59,
            63
        };

        public static byte[] Decode8(byte[] bin, int bw, int bh)
        {
            var array = new byte[bin.Length];
            for (int i = 0; i < 64*bh; i += 64)
            {
                for (int j = 0; j < 128*bw; j += 128)
                {
                    int num = 8192*(j/128 + bw*(i/64));
                    for (int k = 0; k < 64; k += 16)
                    {
                        for (int l = 0; l < 128; l += 16)
                        {
                            int num2 = 256*tbl8bc[l/16 + 8*(k/16)];
                            for (int m = 0; m < 4; m++)
                            {
                                int num3 = 64*m;
                                byte[] array2 = ((m & 1) == 0) ? tbl8c0 : tbl8c1;
                                for (int n = 0; n < 64; n++)
                                {
                                    int num4 = num + num2 + num3 + array2[n];
                                    int num5 = j + l + n%16;
                                    int num6 = i + k + 4*m + n/16;
                                    int num7 = num5 + 128*bw*num6;
                                    array[num7] = bin[num4];
                                }
                            }
                        }
                    }
                }
            }
            return array;
        }

        public static byte[] Encode8(byte[] bin, int bw, int bh)
        {
            var array = new byte[bin.Length];
            for (int i = 0; i < 64*bh; i += 64)
            {
                for (int j = 0; j < 128*bw; j += 128)
                {
                    int num = 8192*(j/128 + bw*(i/64));
                    for (int k = 0; k < 64; k += 16)
                    {
                        for (int l = 0; l < 128; l += 16)
                        {
                            int num2 = 256*tbl8bc[l/16 + 8*(k/16)];
                            for (int m = 0; m < 4; m++)
                            {
                                int num3 = 64*m;
                                byte[] array2 = ((m & 1) == 0) ? tbl8c0 : tbl8c1;
                                for (int n = 0; n < 64; n++)
                                {
                                    int num4 = num + num2 + num3 + array2[n];
                                    int num5 = j + l + n%16;
                                    int num6 = i + k + 4*m + n/16;
                                    int num7 = num5 + 128*bw*num6;
                                    array[num4] = bin[num7];
                                }
                            }
                        }
                    }
                }
            }
            return array;
        }

        public static void Encode8b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff,
            int bw128)
        {
            for (int i = 0; i < rrh; i++)
            {
                int num = dday + i;
                for (int j = 0; j < rrw; j++)
                {
                    int num2 = ddax + j;
                    int num3 = 8192*(num2/128 + bw128*(num/64));
                    int num4 = 256*tbl8bc[(num2 & 127)/16 + 8*((num & 63)/16)];
                    int num5 = (num & 15)/4;
                    int num6 = 64*num5;
                    byte[] array = ((num5 & 1) == 0) ? tbl8c0 : tbl8c1;
                    int num7 = (num2 & 15) + 16*(num & 3);
                    int num8 = baseoff + num3 + num4 + num6 + array[num7];
                    int num9 = j + rrw*i;
                    gsram[num8] = src[num9];
                }
            }
        }

        public static byte[] Decode8c(byte[] gsram, int cx, int cy, int readoff, int bw128)
        {
            var array = new byte[cx*cy];
            for (int i = 0; i < cy; i++)
            {
                for (int j = 0; j < cx; j++)
                {
                    int num = 8192*(j/128 + bw128*(i/64));
                    int num2 = 256*tbl8bc[(j & 127)/16 + 8*((i & 63)/16)];
                    int num3 = (i & 15)/4;
                    int num4 = 64*num3;
                    byte[] array2 = ((num3 & 1) == 0) ? tbl8c0 : tbl8c1;
                    int num5 = (j & 15) + 16*(i & 3);
                    int num6 = readoff + num + num2 + num4 + array2[num5];
                    int num7 = j + cx*i;
                    array[num7] = gsram[num6];
                }
            }
            return array;
        }
    }
}