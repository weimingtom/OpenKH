using khkh_xldMii.Mc;
using System;
using System.IO;
namespace khkh_xldMii.Mo
{
	public class Msetblk
	{
		public To to = new To();
		public int cntb1;
		public int cntb2;
		public Msetblk(Stream si)
		{
			PosTbl posTbl = new PosTbl(si);
			int num = 0;
			int tbloff = posTbl.tbloff;
			this.cntb1 = posTbl.va0;
			this.cntb2 = posTbl.va2;
			BinaryReader binaryReader = new BinaryReader(si);
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			si.Position = (long)(num + tbloff + posTbl.vc0 - num);
			for (int i = 0; i < posTbl.vc4; i++)
			{
				binaryReader.ReadByte();
				binaryReader.ReadByte();
				binaryReader.ReadByte();
				int num5 = (int)binaryReader.ReadByte();
				int num6 = (int)binaryReader.ReadUInt16();
				num2 = Math.Max(num2, num6 + num5);
			}
			si.Position = (long)(num + tbloff + posTbl.vc8 - num);
			for (int j = 0; j < posTbl.vcc; j++)
			{
				binaryReader.ReadByte();
				binaryReader.ReadByte();
				binaryReader.ReadByte();
				int num7 = (int)binaryReader.ReadByte();
				int num8 = (int)binaryReader.ReadUInt16();
				num2 = Math.Max(num2, num8 + num7);
			}
			si.Position = (long)(num + tbloff + posTbl.vd0 - num);
			for (int k = 0; k < num2; k++)
			{
				binaryReader.ReadUInt16();
				int num9 = (int)binaryReader.ReadUInt16();
				num3 = Math.Max(num3, num9 + 1);
				int num10 = (int)binaryReader.ReadUInt16();
				num4 = Math.Max(num4, num10 + 1);
				int num11 = (int)binaryReader.ReadUInt16();
				num4 = Math.Max(num4, num11 + 1);
			}
			int val = 0;
			si.Position = (long)(num + tbloff + posTbl.ve0 - num);
			for (int l = 0; l < posTbl.ve4; l++)
			{
				binaryReader.ReadUInt16();
				binaryReader.ReadUInt16();
				binaryReader.ReadUInt16();
				int num12 = (int)binaryReader.ReadInt16();
				val = Math.Max(val, num12 + 1);
				binaryReader.ReadUInt16();
				binaryReader.ReadUInt16();
			}
			int num13 = tbloff + posTbl.vb4;
			int vb = posTbl.vb8;
			si.Position = (long)num13;
			for (int m = 0; m < vb; m++)
			{
				int c = (int)binaryReader.ReadUInt16();
				int c2 = (int)binaryReader.ReadUInt16();
				float c3 = binaryReader.ReadSingle();
				this.to.al1.Add(new T1(c, c2, c3));
			}
			int num14 = tbloff + posTbl.vd8;
			si.Position = (long)num14;
			this.to.al10 = new float[num3];
			for (int n = 0; n < num3; n++)
			{
				this.to.al10[n] = binaryReader.ReadSingle();
			}
			int num15 = tbloff + posTbl.vd4;
			int vb2 = posTbl.vb0;
			si.Position = (long)num15;
			this.to.al11 = new float[vb2];
			for (int num16 = 0; num16 < vb2; num16++)
			{
				this.to.al11[num16] = binaryReader.ReadSingle();
			}
			int num17 = tbloff + posTbl.vdc;
			si.Position = (long)num17;
			this.to.al12 = new float[num4];
			for (int num18 = 0; num18 < num4; num18++)
			{
				this.to.al12[num18] = binaryReader.ReadSingle();
			}
			int num19 = tbloff + posTbl.vd0;
			si.Position = (long)num19;
			for (int num20 = 0; num20 < num2; num20++)
			{
				int c4 = (int)binaryReader.ReadUInt16();
				int c5 = (int)binaryReader.ReadUInt16();
				int c6 = (int)binaryReader.ReadUInt16();
				int c7 = (int)binaryReader.ReadUInt16();
				this.to.al9.Add(new T9(c4, c5, c6, c7));
			}
			int num21 = tbloff + posTbl.vc0;
			int vc = posTbl.vc4;
			si.Position = (long)num21;
			for (int num22 = 0; num22 < vc; num22++)
			{
				int c8 = (int)binaryReader.ReadByte();
				int c9 = (int)binaryReader.ReadByte();
				int c10 = (int)binaryReader.ReadByte();
				int c11 = (int)binaryReader.ReadByte();
				int c12 = (int)binaryReader.ReadUInt16();
				T2 item = new T2(c8, c9, c10, c11, c12);
				this.to.al2.Add(item);
			}
			for (int num23 = 0; num23 < vc; num23++)
			{
				T2 t = this.to.al2[num23];
				for (int num24 = 0; num24 < t.c03; num24++)
				{
					T9 t2 = this.to.al9[t.c04 + num24];
					int c13 = t2.c00;
					int c14 = t2.c02;
					int c15 = t2.c04;
					int c16 = t2.c06;
					t.al9f.Add(new T9f(t.c04 + num24, this.to.al11[c13 >> 2], this.to.al10[c14], this.to.al12[c15], this.to.al12[c16]));
				}
			}
			int num25 = tbloff + posTbl.vc8;
			int vcc = posTbl.vcc;
			si.Position = (long)num25;
			for (int num26 = 0; num26 < vcc; num26++)
			{
				int c17 = (int)binaryReader.ReadByte();
				int c18 = (int)binaryReader.ReadByte();
				int c19 = (int)binaryReader.ReadByte();
				int c20 = (int)binaryReader.ReadByte();
				int c21 = (int)binaryReader.ReadUInt16();
				T2 item2 = new T2(c17, c18, c19, c20, c21);
				this.to.al2x.Add(item2);
			}
			for (int num27 = 0; num27 < vcc; num27++)
			{
				T2 t3 = this.to.al2x[num27];
				for (int num28 = 0; num28 < t3.c03; num28++)
				{
					T9 t4 = this.to.al9[t3.c04 + num28];
					int c22 = t4.c00;
					int c23 = t4.c02;
					int c24 = t4.c04;
					int c25 = t4.c06;
					t3.al9f.Add(new T9f(t3.c04 + num28, this.to.al11[c22 >> 2], this.to.al10[c23], this.to.al12[c24], this.to.al12[c25]));
				}
			}
			int num29 = tbloff + posTbl.ve0;
			int ve = posTbl.ve4;
			si.Position = (long)num29;
			for (int num30 = 0; num30 < ve; num30++)
			{
				int c26 = (int)binaryReader.ReadByte();
				int c27 = (int)binaryReader.ReadByte();
				int c28 = (int)binaryReader.ReadUInt16();
				int c29 = (int)binaryReader.ReadUInt16();
				int c30 = (int)binaryReader.ReadUInt16();
				uint c31 = binaryReader.ReadUInt32();
				this.to.al3.Add(new T3(c26, c27, c28, c29, c30, c31));
			}
			int num31 = tbloff + posTbl.vac;
			int va = posTbl.va2;
			si.Position = (long)num31;
			for (int num32 = 0; num32 < va; num32++)
			{
				int c32 = (int)binaryReader.ReadUInt16();
				int c33 = (int)binaryReader.ReadUInt16();
				this.to.al4.Add(new T4(c32, c33));
			}
			this.to.off5 = tbloff + posTbl.va8;
			this.to.cnt5 = posTbl.va2 - posTbl.va0;
			si.Position = (long)this.to.off5;
			for (int num33 = 0; num33 < this.to.cnt5; num33++)
			{
				AxBone axBone = new AxBone();
				axBone.cur = (int)binaryReader.ReadUInt16();
				axBone.parent = (int)binaryReader.ReadUInt16();
				axBone.v08 = (int)binaryReader.ReadUInt16();
				axBone.v0c = (int)binaryReader.ReadUInt16();
				binaryReader.ReadUInt64();
				axBone.x1 = binaryReader.ReadSingle();
				axBone.y1 = binaryReader.ReadSingle();
				axBone.z1 = binaryReader.ReadSingle();
				axBone.w1 = binaryReader.ReadSingle();
				axBone.x2 = binaryReader.ReadSingle();
				axBone.y2 = binaryReader.ReadSingle();
				axBone.z2 = binaryReader.ReadSingle();
				axBone.w2 = binaryReader.ReadSingle();
				axBone.x3 = binaryReader.ReadSingle();
				axBone.y3 = binaryReader.ReadSingle();
				axBone.z3 = binaryReader.ReadSingle();
				axBone.w3 = binaryReader.ReadSingle();
				this.to.al5.Add(axBone);
			}
		}
	}
}
