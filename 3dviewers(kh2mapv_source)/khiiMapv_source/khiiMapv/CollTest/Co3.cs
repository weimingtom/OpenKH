namespace khiiMapv.CollTest
{
    using System;
    using System.IO;

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
            base..ctor();
            this.v0 = br.ReadInt16();
            this.v2 = br.ReadInt16();
            this.v4 = br.ReadInt16();
            this.v6 = br.ReadInt16();
            this.v8 = br.ReadInt16();
            this.va = br.ReadInt16();
            this.vc = br.ReadInt16();
            this.ve = br.ReadInt16();
            return;
        }

        public override string ToString()
        {
            object[] objArray;
            return string.Format("{0,4} PolyCo4({1,4},{2,4},{3,4},{4,4}) PlaneCo5({5,3}) {6,3} {7,3}", new object[] { (short) this.v0, (short) this.v2, (short) this.v4, (short) this.v6, (short) this.v8, (short) this.va, (short) this.vc, (short) this.ve });
        }

        public int PlaneCo5
        {
            get
            {
                return this.va;
            }
        }

        public int vi0
        {
            get
            {
                return this.v2;
            }
        }

        public int vi1
        {
            get
            {
                return this.v4;
            }
        }

        public int vi2
        {
            get
            {
                return this.v6;
            }
        }

        public int vi3
        {
            get
            {
                return this.v8;
            }
        }
    }
}

