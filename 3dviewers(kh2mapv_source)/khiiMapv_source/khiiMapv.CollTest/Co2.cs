using System.IO;
using SlimDX;

namespace khiiMapv.CollTest
{
    public class Co2
    {
        public short v00;
        public short v02;
        public short v04;
        public short v06;
        public short v08;
        public short v0a;
        public short v0c;
        public short v0e;
        public short v10;
        public short v12;

        public Co2(BinaryReader br)
        {
            v00 = br.ReadInt16();
            v02 = br.ReadInt16();
            v04 = br.ReadInt16();
            v06 = br.ReadInt16();
            v08 = br.ReadInt16();
            v0a = br.ReadInt16();
            v0c = br.ReadInt16();
            v0e = br.ReadInt16();
            v10 = br.ReadInt16();
            v12 = br.ReadInt16();
        }

        public Vector3 Min
        {
            get { return new Vector3(v00, v02, v04); }
        }

        public Vector3 Max
        {
            get { return new Vector3(v06, v08, v0a); }
        }

        public int Co3frm
        {
            get { return v0c; }
        }

        public int Co3to
        {
            get { return v0e; }
        }

        public override string ToString()
        {
            return
                string.Format(
                    "bbox-min({0,6}, {1,6}, {2,6}) bbox-max({3,6}, {4,6}, {5,6}) Co3frmTo({6,3}, {7,3}) {8:x4} {9,3}",
                    new object[]
                    {
                        v00,
                        v02,
                        v04,
                        v06,
                        v08,
                        v0a,
                        v0c,
                        v0e,
                        v10,
                        v12
                    });
        }
    }
}