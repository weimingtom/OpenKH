namespace hex04BinTrack
{
    using System;

    public class VU1Mem
    {
        public byte[] vumem;

        public VU1Mem()
        {
            this.vumem = new byte[0x4000];
            base..ctor();
            return;
        }
    }
}

