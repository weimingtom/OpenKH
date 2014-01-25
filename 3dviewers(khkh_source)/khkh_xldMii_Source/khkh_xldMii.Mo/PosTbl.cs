namespace khkh_xldMii.Mo
{
    using System;
    using System.IO;

    internal class PosTbl
    {
        public int tbloff = 0x90;
        public int va0;
        public int va2;
        public int va8;
        public int vac;
        public int vb0;
        public int vb4;
        public int vb8;
        public int vc0;
        public int vc4;
        public int vc8;
        public int vcc;
        public int vd0;
        public int vd4;
        public int vd8;
        public int vdc;
        public int ve0;
        public int ve4;
        public int ve8;
        public int vec;
        public int vf0;
        public int vf4;
        public int vf8;
        public int vfc;

        public PosTbl(Stream si)
        {
            BinaryReader reader = new BinaryReader(si);
            int num = this.tbloff - 0x90;
            si.Position = num + 160;
            this.va0 = reader.ReadUInt16();
            this.va2 = reader.ReadUInt16();
            si.Position = num + 0xa8;
            this.va8 = reader.ReadInt32();
            this.vac = reader.ReadInt32();
            si.Position = num + 0xb0;
            this.vb0 = reader.ReadInt32();
            this.vb4 = reader.ReadInt32();
            this.vb8 = reader.ReadInt32();
            si.Position = num + 0xc0;
            this.vc0 = reader.ReadInt32();
            this.vc4 = reader.ReadInt32();
            this.vc8 = reader.ReadInt32();
            this.vcc = reader.ReadInt32();
            si.Position = num + 0xd0;
            this.vd0 = reader.ReadInt32();
            this.vd4 = reader.ReadInt32();
            this.vd8 = reader.ReadInt32();
            this.vdc = reader.ReadInt32();
            si.Position = num + 0xe0;
            this.ve0 = reader.ReadInt32();
            this.ve4 = reader.ReadInt32();
            this.ve8 = reader.ReadInt32();
            this.vec = reader.ReadInt32();
            si.Position = num + 240;
            this.vf0 = reader.ReadInt32();
            this.vf4 = reader.ReadInt32();
            this.vf8 = reader.ReadInt32();
            this.vfc = reader.ReadInt32();
        }
    }
}

