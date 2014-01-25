namespace vwBinTex2
{
    using System;

    internal class KHcv8pal_v2
    {
        private static readonly byte[] alt = new byte[] { 
            0, 1, 2, 3, 4, 5, 6, 7, 0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 
            8, 9, 10, 11, 12, 13, 14, 15, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f
         };

        public static void repl(byte[] bSrc, int offSrc, byte[] bDst, int offDst)
        {
            for (int i = 0; i < 0x100; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    bDst[(offDst + (4 * i)) + j] = bSrc[(offSrc + (4 * (alt[i & 0x1f] + (i & -32)))) + j];
                }
            }
        }
    }
}

