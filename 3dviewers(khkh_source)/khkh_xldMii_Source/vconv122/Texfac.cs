namespace vconv122
{
    using System;

    public class Texfac
    {
        public int i0;
        public int i1;
        public int i2;
        public short v0;
        public short v2;
        public short v4;
        public short v6;

        public override string ToString()
        {
            return string.Format("{0,3},{1,2},{2,2}|{3,4},{4,3},{5,3},{6,3}", new object[] { this.i0, this.i1, this.i2, this.v0, this.v2, this.v4, this.v6 });
        }
    }
}

