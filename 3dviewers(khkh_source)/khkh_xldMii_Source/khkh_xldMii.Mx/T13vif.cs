namespace khkh_xldMii.Mx
{
    using System;

    public class T13vif
    {
        public int[] alaxi;
        public byte[] bin;
        public int len;
        public int off;
        public int texi;

        public T13vif(int off, int len, int texi, int[] alaxi, byte[] bin)
        {
            this.off = off;
            this.len = len;
            this.texi = texi;
            this.alaxi = alaxi;
            this.bin = bin;
        }
    }
}

