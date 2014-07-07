namespace vcBinTex4
{
    public class Reform4
    {
        private static byte[] tbl4bc =
        {
            0,
            2,
            8,
            10,
            1,
            3,
            9,
            11,
            4,
            6,
            12,
            14,
            5,
            7,
            13,
            15,
            16,
            18,
            24,
            26,
            17,
            19,
            25,
            27,
            20,
            22,
            28,
            30,
            21,
            23,
            29,
            31
        };

        private static readonly int[] tbl4col0 =
        {
            0,
            32,
            128,
            160,
            256,
            288,
            384,
            416,
            8,
            40,
            136,
            168,
            264,
            296,
            392,
            424,
            16,
            48,
            144,
            176,
            272,
            304,
            400,
            432,
            24,
            56,
            152,
            184,
            280,
            312,
            408,
            440,
            64,
            96,
            192,
            224,
            320,
            352,
            448,
            480,
            72,
            104,
            200,
            232,
            328,
            360,
            456,
            488,
            80,
            112,
            208,
            240,
            336,
            368,
            464,
            496,
            88,
            120,
            216,
            248,
            344,
            376,
            472,
            504,
            260,
            292,
            388,
            420,
            4,
            36,
            132,
            164,
            268,
            300,
            396,
            428,
            12,
            44,
            140,
            172,
            276,
            308,
            404,
            436,
            20,
            52,
            148,
            180,
            284,
            316,
            412,
            444,
            28,
            60,
            156,
            188,
            324,
            356,
            452,
            484,
            68,
            100,
            196,
            228,
            332,
            364,
            460,
            492,
            76,
            108,
            204,
            236,
            340,
            372,
            468,
            500,
            84,
            116,
            212,
            244,
            348,
            380,
            476,
            508,
            92,
            124,
            220,
            252
        };

        private static readonly int[] tbl4col1 =
        {
            256,
            288,
            384,
            416,
            0,
            32,
            128,
            160,
            264,
            296,
            392,
            424,
            8,
            40,
            136,
            168,
            272,
            304,
            400,
            432,
            16,
            48,
            144,
            176,
            280,
            312,
            408,
            440,
            24,
            56,
            152,
            184,
            320,
            352,
            448,
            480,
            64,
            96,
            192,
            224,
            328,
            360,
            456,
            488,
            72,
            104,
            200,
            232,
            336,
            368,
            464,
            496,
            80,
            112,
            208,
            240,
            344,
            376,
            472,
            504,
            88,
            120,
            216,
            248,
            4,
            36,
            132,
            164,
            260,
            292,
            388,
            420,
            12,
            44,
            140,
            172,
            268,
            300,
            396,
            428,
            20,
            52,
            148,
            180,
            276,
            308,
            404,
            436,
            28,
            60,
            156,
            188,
            284,
            316,
            412,
            444,
            68,
            100,
            196,
            228,
            324,
            356,
            452,
            484,
            76,
            108,
            204,
            236,
            332,
            364,
            460,
            492,
            84,
            116,
            212,
            244,
            340,
            372,
            468,
            500,
            92,
            124,
            220,
            252,
            348,
            380,
            476,
            508
        };

        public static byte[] Decode4(byte[] bin, int bw, int bh)
        {
            var array = new byte[bin.Length];
            for (int i = 0; i < 128*bh; i += 128)
            {
                for (int j = 0; j < 128*bw; j += 128)
                {
                    int num = 8192*(j/128 + bw*(i/128));
                    for (int k = 0; k < 128; k += 16)
                    {
                        for (int l = 0; l < 128; l += 32)
                        {
                            int num2 = 256*tbl4bc[l/32 + 4*(k/16)];
                            for (int m = 0; m < 4; m++)
                            {
                                int num3 = 64*m;
                                int[] array2 = ((m & 1) == 0) ? tbl4col0 : tbl4col1;
                                for (int n = 0; n < 128; n++)
                                {
                                    int num4 = array2[n]/8;
                                    int num5 = array2[n]%8;
                                    var b = (byte) (bin[num + num2 + num3 + num4] >> num5 & 15);
                                    int num6 = j + l + n%32;
                                    int num7 = i + k + 4*m + n/32;
                                    int num8 = num6 + 128*bw*num7;
                                    byte b2 = array[num8/2];
                                    if ((num8 & 1) == 0)
                                    {
                                        b2 &= 15;
                                        b2 |= (byte) (b << 4);
                                    }
                                    else
                                    {
                                        b2 &= 240;
                                        b2 |= b;
                                    }
                                    array[num8/2] = b2;
                                }
                            }
                        }
                    }
                }
            }
            return array;
        }

        public static byte[] Encode4(byte[] bin, int bw, int bh)
        {
            var array = new byte[bin.Length];
            for (int i = 0; i < 128*bh; i += 128)
            {
                for (int j = 0; j < 128*bw; j += 128)
                {
                    int num = 8192*(j/128 + bw*(i/128));
                    for (int k = 0; k < 128; k += 16)
                    {
                        for (int l = 0; l < 128; l += 32)
                        {
                            int num2 = 256*tbl4bc[l/32 + 4*(k/16)];
                            for (int m = 0; m < 4; m++)
                            {
                                int num3 = 64*m;
                                int[] array2 = ((m & 1) == 0) ? tbl4col0 : tbl4col1;
                                for (int n = 0; n < 128; n++)
                                {
                                    int num4 = j + l + n%32;
                                    int num5 = i + k + 4*m + n/32;
                                    int num6 = num4 + 128*bw*num5;
                                    byte b = bin[num6/2];
                                    if ((num6 & 1) != 0)
                                    {
                                        b &= 15;
                                    }
                                    else
                                    {
                                        b = (byte) (b >> 4);
                                    }
                                    int num7 = array2[n]/8;
                                    int num8 = array2[n]%8;
                                    int num9 = num + num2 + num3 + num7;
                                    byte b2 = array[num9];
                                    if (num8 == 0)
                                    {
                                        b2 &= 240;
                                        b2 |= b;
                                    }
                                    else
                                    {
                                        if (num8 == 4)
                                        {
                                            b2 &= 15;
                                            b2 |= (byte) (b << 4);
                                        }
                                    }
                                    array[num9] = b2;
                                }
                            }
                        }
                    }
                }
            }
            return array;
        }

        public static void Encode4b(byte[] src, byte[] gsram, int rrw, int rrh, int ddax, int dday, int baseoff,
            int bw128)
        {
            for (int i = 0; i < rrh; i++)
            {
                int num = dday + i;
                for (int j = 0; j < rrw; j++)
                {
                    int num2 = ddax + j;
                    int num3 = 8192*(num2/128 + bw128*(num/128));
                    int num4 = 256*tbl4bc[(num2 & 127)/32 + 4*((num & 127)/16)];
                    int num5 = (num & 15)/4;
                    int num6 = 64*num5;
                    int[] array = ((num5 & 1) == 0) ? tbl4col0 : tbl4col1;
                    int num7 = (num2 & 31) + 32*(num & 3);
                    int num8 = j + rrw*i;
                    byte b = src[num8/2];
                    if ((num8 & 1) != 0)
                    {
                        b &= 15;
                    }
                    else
                    {
                        b = (byte) (b >> 4);
                    }
                    int num9 = array[num7]/8;
                    int num10 = array[num7]%8;
                    int num11 = baseoff + num3 + num4 + num6 + num9;
                    byte b2 = gsram[num11];
                    if (num10 == 0)
                    {
                        b2 &= 240;
                        b2 |= b;
                    }
                    else
                    {
                        if (num10 == 4)
                        {
                            b2 &= 15;
                            b2 |= (byte) (b << 4);
                        }
                    }
                    gsram[num11] = b2;
                }
            }
        }

        public static byte[] Decode4c(byte[] gsram, int cx, int cy, int readoff, int bw128)
        {
            var array = new byte[(cx*cy + 1)/2];
            for (int i = 0; i < cy; i++)
            {
                for (int j = 0; j < cx; j++)
                {
                    int num = 8192*(j/128 + bw128*(i/128));
                    int num2 = 256*tbl4bc[(j & 127)/32 + 4*((i & 127)/16)];
                    int num3 = (i & 15)/4;
                    int num4 = 64*num3;
                    int[] array2 = ((num3 & 1) == 0) ? tbl4col0 : tbl4col1;
                    int num5 = (j & 31) + 32*(i & 3);
                    int num6 = array2[num5]/8;
                    int num7 = array2[num5]%8;
                    var b = (byte) (gsram[readoff + num + num2 + num4 + num6] >> num7 & 15);
                    int num8 = j + cx*i;
                    byte b2 = array[num8/2];
                    if ((num8 & 1) == 0)
                    {
                        b2 &= 15;
                        b2 |= (byte) (b << 4);
                    }
                    else
                    {
                        b2 &= 240;
                        b2 |= b;
                    }
                    array[num8/2] = b2;
                }
            }
            return array;
        }
    }
}