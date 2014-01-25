namespace vwBinTex2
{
    using System;

    internal class KHcv8pal_swap34
    {
        public static int repl(int x)
        {
            return (((x & 0xe7) | (((x & 0x10) != 0) ? 8 : 0)) | (((x & 8) != 0) ? 0x10 : 0));
        }
    }
}

