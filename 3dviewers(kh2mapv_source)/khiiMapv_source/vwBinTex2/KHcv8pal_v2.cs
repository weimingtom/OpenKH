namespace vwBinTex2
{
    using System;

    internal class KHcv8pal_v2
    {
        private static readonly byte[] alt;

        static KHcv8pal_v2()
        {
            alt = new byte[] { 
                0, 1, 2, 3, 4, 5, 6, 7, 0x10, 0x11, 0x12, 0x13, 20, 0x15, 0x16, 0x17, 
                8, 9, 10, 11, 12, 13, 14, 15, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 30, 0x1f
             };
            return;
        }

        public KHcv8pal_v2()
        {
            base..ctor();
            return;
        }

        public static void repl(byte[] bSrc, int offSrc, byte[] bDst, int offDst)
        {
            int num;
            int num2;
            num = 0;
            goto Label_0034;
        Label_0004:
            num2 = 0;
            goto Label_002C;
        Label_0008:
            bDst[(offDst + (4 * num)) + num2] = bSrc[(offSrc + (4 * (alt[num & 0x1f] + (num & -32)))) + num2];
            num2 += 1;
        Label_002C:
            if (num2 < 4)
            {
                goto Label_0008;
            }
            num += 1;
        Label_0034:
            if (num < 0x100)
            {
                goto Label_0004;
            }
            return;
        }
    }
}

