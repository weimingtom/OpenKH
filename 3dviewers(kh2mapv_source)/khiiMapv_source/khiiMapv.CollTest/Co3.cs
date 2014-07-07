using System.IO;

namespace khiiMapv.CollTest
{
    public class Co3
    {
        public short v0;
        public short v2;
        public short v4;
        public short v6;
        public short v8;
        public short va;
        public short vc;
        public short ve;

        public Co3(BinaryReader br)
        {
            v0 = br.ReadInt16();
            v2 = br.ReadInt16();
            v4 = br.ReadInt16();
            v6 = br.ReadInt16();
            v8 = br.ReadInt16();
            va = br.ReadInt16();
            vc = br.ReadInt16();
            ve = br.ReadInt16();
        }

        public int vi0
        {
            get { return v2; }
        }

        public int vi1
        {
            get { return v4; }
        }

        public int vi2
        {
            get { return v6; }
        }

        public int vi3
        {
            get { return v8; }
        }

        public int PlaneCo5
        {
            get { return va; }
        }

        public override string ToString()
        {
            return string.Format("{0,4} PolyCo4({1,4},{2,4},{3,4},{4,4}) PlaneCo5({5,3}) {6,3} {7,3}", new object[]
            {
                v0,
                v2,
                v4,
                v6,
                v8,
                va,
                vc,
                ve
            });
        }
    }
}