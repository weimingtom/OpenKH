namespace vwBinTex2
{
    using System;

    internal class KHcv8pal
    {
        private static readonly sbyte[] tbl;

        static KHcv8pal()
        {
            tbl = new sbyte[] { 
                0, 0, 6, 6, -2, -2, 4, 4, -4, -4, 2, 2, -6, -6, 0, 0, 
                0x10, 0x10, 0x16, 0x16, 14, 14, 20, 20, 12, 12, 0x12, 0x12, 10, 10, 0x10, 0x10, 
                0x20, 0x20, 0x26, 0x26, 30, 30, 0x24, 0x24, 0x1c, 0x1c, 0x22, 0x22, 0x1a, 0x1a, 0x20, 0x20, 
                0x30, 0x30, 0x36, 0x36, 0x2e, 0x2e, 0x34, 0x34, 0x2c, 0x2c, 50, 50, 0x2a, 0x2a, 0x30, 0x30, 
                -48, -48, -42, -42, -50, -50, -44, -44, -52, -52, -46, -46, -54, -54, -48, -48, 
                -32, -32, -26, -26, -34, -34, -28, -28, -36, -36, -30, -30, -38, -38, -32, -32, 
                -16, -16, -10, -10, -18, -18, -12, -12, -20, -20, -14, -14, -22, -22, -16, -16, 
                0, 0, 6, 6, -2, -2, 4, 4, -4, -4, 2, 2, -6, -6, 0, 0
             };
            return;
        }

        public KHcv8pal()
        {
            base..ctor();
            return;
        }

        public static int repl(int t)
        {
            return ((t & 0x80) | ((t & 0x7f) + tbl[t & 0x7f]));
        }
    }
}

