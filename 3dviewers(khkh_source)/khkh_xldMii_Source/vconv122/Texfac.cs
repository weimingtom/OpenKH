namespace vconv122
{
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
            return string.Format("{0,3},{1,2},{2,2}|{3,4},{4,3},{5,3},{6,3}", new object[]
            {
                i0,
                i1,
                i2,
                v0,
                v2,
                v4,
                v6
            });
        }
    }
}