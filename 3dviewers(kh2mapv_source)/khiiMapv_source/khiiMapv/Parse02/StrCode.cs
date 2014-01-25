namespace khiiMapv.Parse02
{
    using System;
    using System.Runtime.CompilerServices;

    public class StrCode
    {
        [CompilerGenerated]
        private static Converter<byte, string> <>9__CachedAnonymousMethodDelegate1;
        public byte[] bin;

        public StrCode(byte[] bin)
        {
            base..ctor();
            this.bin = bin;
            return;
        }

        [CompilerGenerated]
        private static unsafe string <ToString>b__0(byte b)
        {
            return &b.ToString("x2");
        }

        public override string ToString()
        {
            if (<>9__CachedAnonymousMethodDelegate1 != null)
            {
                goto Label_0028;
            }
            <>9__CachedAnonymousMethodDelegate1 = new Converter<byte, string>(StrCode.<ToString>b__0);
        Label_0028:
            return ("<" + string.Join(" ", Array.ConvertAll<byte, string>(this.bin, <>9__CachedAnonymousMethodDelegate1)) + ">\r\n");
        }
    }
}

