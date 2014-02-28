namespace khiiMapv.Parse02
{
    using System;

    public class StrCode
    {
        public byte[] bin;

        public StrCode(byte[] bin)
        {
            this.bin = bin;
        }

        public override string ToString()
        {
            return ("<" + string.Join(" ", Array.ConvertAll<byte, string>(this.bin, b => b.ToString("x2"))) + ">\r\n");
        }
    }
}

