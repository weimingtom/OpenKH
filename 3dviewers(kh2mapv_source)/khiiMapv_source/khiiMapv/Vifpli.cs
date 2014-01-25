namespace khiiMapv
{
    using System;

    public class Vifpli
    {
        public int texi;
        public byte[] vifpkt;

        public Vifpli(byte[] vifpkt, int texi)
        {
            base..ctor();
            this.vifpkt = vifpkt;
            this.texi = texi;
            return;
        }
    }
}

