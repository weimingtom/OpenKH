namespace khiiMapv
{
    public class Vifpli
    {
        public int texi;
        public byte[] vifpkt;

        public Vifpli(byte[] vifpkt, int texi)
        {
            this.vifpkt = vifpkt;
            this.texi = texi;
        }
    }
}