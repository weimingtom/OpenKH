namespace vwBinTex2
{
    using System;

    internal class KHcv4pal_v2
    {
        private static readonly sbyte[] tbl;

        static KHcv4pal_v2()
        {
            tbl = new sbyte[] { 0, 1, 4, 5, 8, 9, 12, 13, 2, 3, 6, 7, 10, 11, 14, 15 };
            return;
        }

        public KHcv4pal_v2()
        {
            base..ctor();
            return;
        }

        public static void repl(byte[] bSrc, int offSrc, byte[] bDst, int offDst)
        {
            int num;
            int num2;
            num = 0;
            goto Label_002C;
        Label_0004:
            num2 = 0;
            goto Label_0024;
        Label_0008:
            bDst[(offDst + (4 * num)) + num2] = bSrc[(offSrc + (4 * tbl[num])) + num2];
            num2 += 1;
        Label_0024:
            if (num2 < 4)
            {
                goto Label_0008;
            }
            num += 1;
        Label_002C:
            if (num < 0x10)
            {
                goto Label_0004;
            }
            return;
        }
    }
}

