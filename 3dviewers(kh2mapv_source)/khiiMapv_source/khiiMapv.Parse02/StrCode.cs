using System;

namespace khiiMapv.Parse02
{
    public class StrCode
    {
        public byte[] bin;

        public StrCode(byte[] bin)
        {
            this.bin = bin;
        }

        public override string ToString()
        {
            return ("<" + string.Join(" ", Array.ConvertAll(bin, b => b.ToString("x2"))) + ">\r\n");
        }
    }
}