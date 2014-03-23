using System.IO;

namespace khkh_xldMii.Mo
{
    internal class PosTbl
    {
        public int tbloff = 144;
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
            var binaryReader = new BinaryReader(si);
            int num = tbloff - 144;
            si.Position = num + 160;
            va0 = binaryReader.ReadUInt16();
            va2 = binaryReader.ReadUInt16();
            si.Position = num + 168;
            va8 = binaryReader.ReadInt32();
            vac = binaryReader.ReadInt32();
            si.Position = num + 176;
            vb0 = binaryReader.ReadInt32();
            vb4 = binaryReader.ReadInt32();
            vb8 = binaryReader.ReadInt32();
            si.Position = num + 192;
            vc0 = binaryReader.ReadInt32();
            vc4 = binaryReader.ReadInt32();
            vc8 = binaryReader.ReadInt32();
            vcc = binaryReader.ReadInt32();
            si.Position = num + 208;
            vd0 = binaryReader.ReadInt32();
            vd4 = binaryReader.ReadInt32();
            vd8 = binaryReader.ReadInt32();
            vdc = binaryReader.ReadInt32();
            si.Position = num + 224;
            ve0 = binaryReader.ReadInt32();
            ve4 = binaryReader.ReadInt32();
            ve8 = binaryReader.ReadInt32();
            vec = binaryReader.ReadInt32();
            si.Position = num + 240;
            vf0 = binaryReader.ReadInt32();
            vf4 = binaryReader.ReadInt32();
            vf8 = binaryReader.ReadInt32();
            vfc = binaryReader.ReadInt32();
        }
    }
}