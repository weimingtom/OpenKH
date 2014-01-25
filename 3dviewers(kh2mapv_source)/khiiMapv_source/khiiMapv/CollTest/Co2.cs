namespace khiiMapv.CollTest
{
    using SlimDX;
    using System;
    using System.IO;

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
            base..ctor();
            this.v00 = br.ReadInt16();
            this.v02 = br.ReadInt16();
            this.v04 = br.ReadInt16();
            this.v06 = br.ReadInt16();
            this.v08 = br.ReadInt16();
            this.v0a = br.ReadInt16();
            this.v0c = br.ReadInt16();
            this.v0e = br.ReadInt16();
            this.v10 = br.ReadInt16();
            this.v12 = br.ReadInt16();
            return;
        }

        public override string ToString()
        {
            object[] objArray;
            return string.Format("bbox-min({0,6}, {1,6}, {2,6}) bbox-max({3,6}, {4,6}, {5,6}) Co3frmTo({6,3}, {7,3}) {8:x4} {9,3}", new object[] { (short) this.v00, (short) this.v02, (short) this.v04, (short) this.v06, (short) this.v08, (short) this.v0a, (short) this.v0c, (short) this.v0e, (short) this.v10, (short) this.v12 });
        }

        public int Co3frm
        {
            get
            {
                return this.v0c;
            }
        }

        public int Co3to
        {
            get
            {
                return this.v0e;
            }
        }

        public Vector3 Max
        {
            get
            {
                return new Vector3((float) this.v06, (float) this.v08, (float) this.v0a);
            }
        }

        public Vector3 Min
        {
            get
            {
                return new Vector3((float) this.v00, (float) this.v02, (float) this.v04);
            }
        }
    }
}

