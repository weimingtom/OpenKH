namespace vwBinTex2
{
    internal class KHcv4pal_v2
    {
        private static readonly sbyte[] tbl =
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

        public static void repl(byte[] bSrc, int offSrc, byte[] bDst, int offDst)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    bDst[offDst + 4*i + j] = bSrc[offSrc + 4*tbl[i] + j];
                }
            }
        }
    }
}