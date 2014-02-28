using System;
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
			BinaryReader binaryReader = new BinaryReader(si);
			int num = this.tbloff - 144;
			si.Position = (long)(num + 160);
			this.va0 = (int)binaryReader.ReadUInt16();
			this.va2 = (int)binaryReader.ReadUInt16();
			si.Position = (long)(num + 168);
			this.va8 = binaryReader.ReadInt32();
			this.vac = binaryReader.ReadInt32();
			si.Position = (long)(num + 176);
			this.vb0 = binaryReader.ReadInt32();
			this.vb4 = binaryReader.ReadInt32();
			this.vb8 = binaryReader.ReadInt32();
			si.Position = (long)(num + 192);
			this.vc0 = binaryReader.ReadInt32();
			this.vc4 = binaryReader.ReadInt32();
			this.vc8 = binaryReader.ReadInt32();
			this.vcc = binaryReader.ReadInt32();
			si.Position = (long)(num + 208);
			this.vd0 = binaryReader.ReadInt32();
			this.vd4 = binaryReader.ReadInt32();
			this.vd8 = binaryReader.ReadInt32();
			this.vdc = binaryReader.ReadInt32();
			si.Position = (long)(num + 224);
			this.ve0 = binaryReader.ReadInt32();
			this.ve4 = binaryReader.ReadInt32();
			this.ve8 = binaryReader.ReadInt32();
			this.vec = binaryReader.ReadInt32();
			si.Position = (long)(num + 240);
			this.vf0 = binaryReader.ReadInt32();
			this.vf4 = binaryReader.ReadInt32();
			this.vf8 = binaryReader.ReadInt32();
			this.vfc = binaryReader.ReadInt32();
		}
	}
}
