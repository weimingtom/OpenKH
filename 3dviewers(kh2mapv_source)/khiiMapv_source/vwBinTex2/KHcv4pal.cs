namespace vwBinTex2
{
    using System;

    internal class KHcv4pal
    {
        private static readonly sbyte[] tbl;

        static KHcv4pal()
        {
            tbl = new sbyte[] { 0, 1, 4, 5, 8, 9, 12, 13, 2, 3, 6, 7, 10, 11, 14, 15 };
            return;
        }

        public KHcv4pal()
        {
            base..ctor();
            return;
        }

        public static int repl(int t)
        {
            return tbl[t];
        }
    }
}

