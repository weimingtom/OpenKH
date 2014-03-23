using System;
using System.IO;
using khkh_xldMii.Mc;

namespace khkh_xldMii.Mo
{
    public class Msetblk
    {
        public int cntb1;
        public int cntb2;
        public To to = new To();

        public Msetblk(Stream si)
        {
            var posTbl = new PosTbl(si);
            int num = 0;
            int tbloff = posTbl.tbloff;
            cntb1 = posTbl.va0;
            cntb2 = posTbl.va2;
            var binaryReader = new BinaryReader(si);
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            si.Position = num + tbloff + posTbl.vc0 - num;
            for (int i = 0; i < posTbl.vc4; i++)
            {
                binaryReader.ReadByte();
                binaryReader.ReadByte();
                binaryReader.ReadByte();
                int num5 = binaryReader.ReadByte();
                int num6 = binaryReader.ReadUInt16();
                num2 = Math.Max(num2, num6 + num5);
            }
            si.Position = num + tbloff + posTbl.vc8 - num;
            for (int j = 0; j < posTbl.vcc; j++)
            {
                binaryReader.ReadByte();
                binaryReader.ReadByte();
                binaryReader.ReadByte();
                int num7 = binaryReader.ReadByte();
                int num8 = binaryReader.ReadUInt16();
                num2 = Math.Max(num2, num8 + num7);
            }
            si.Position = num + tbloff + posTbl.vd0 - num;
            for (int k = 0; k < num2; k++)
            {
                binaryReader.ReadUInt16();
                int num9 = binaryReader.ReadUInt16();
                num3 = Math.Max(num3, num9 + 1);
                int num10 = binaryReader.ReadUInt16();
                num4 = Math.Max(num4, num10 + 1);
                int num11 = binaryReader.ReadUInt16();
                num4 = Math.Max(num4, num11 + 1);
            }
            int val = 0;
            si.Position = num + tbloff + posTbl.ve0 - num;
            for (int l = 0; l < posTbl.ve4; l++)
            {
                binaryReader.ReadUInt16();
                binaryReader.ReadUInt16();
                binaryReader.ReadUInt16();
                int num12 = binaryReader.ReadInt16();
                val = Math.Max(val, num12 + 1);
                binaryReader.ReadUInt16();
                binaryReader.ReadUInt16();
            }
            int num13 = tbloff + posTbl.vb4;
            int vb = posTbl.vb8;
            si.Position = num13;
            for (int m = 0; m < vb; m++)
            {
                int c = binaryReader.ReadUInt16();
                int c2 = binaryReader.ReadUInt16();
                float c3 = binaryReader.ReadSingle();
                to.al1.Add(new T1(c, c2, c3));
            }
            int num14 = tbloff + posTbl.vd8;
            si.Position = num14;
            to.al10 = new float[num3];
            for (int n = 0; n < num3; n++)
            {
                to.al10[n] = binaryReader.ReadSingle();
            }
            int num15 = tbloff + posTbl.vd4;
            int vb2 = posTbl.vb0;
            si.Position = num15;
            to.al11 = new float[vb2];
            for (int num16 = 0; num16 < vb2; num16++)
            {
                to.al11[num16] = binaryReader.ReadSingle();
            }
            int num17 = tbloff + posTbl.vdc;
            si.Position = num17;
            to.al12 = new float[num4];
            for (int num18 = 0; num18 < num4; num18++)
            {
                to.al12[num18] = binaryReader.ReadSingle();
            }
            int num19 = tbloff + posTbl.vd0;
            si.Position = num19;
            for (int num20 = 0; num20 < num2; num20++)
            {
                int c4 = binaryReader.ReadUInt16();
                int c5 = binaryReader.ReadUInt16();
                int c6 = binaryReader.ReadUInt16();
                int c7 = binaryReader.ReadUInt16();
                to.al9.Add(new T9(c4, c5, c6, c7));
            }
            int num21 = tbloff + posTbl.vc0;
            int vc = posTbl.vc4;
            si.Position = num21;
            for (int num22 = 0; num22 < vc; num22++)
            {
                int c8 = binaryReader.ReadByte();
                int c9 = binaryReader.ReadByte();
                int c10 = binaryReader.ReadByte();
                int c11 = binaryReader.ReadByte();
                int c12 = binaryReader.ReadUInt16();
                var item = new T2(c8, c9, c10, c11, c12);
                to.al2.Add(item);
            }
            for (int num23 = 0; num23 < vc; num23++)
            {
                T2 t = to.al2[num23];
                for (int num24 = 0; num24 < t.c03; num24++)
                {
                    T9 t2 = to.al9[t.c04 + num24];
                    int c13 = t2.c00;
                    int c14 = t2.c02;
                    int c15 = t2.c04;
                    int c16 = t2.c06;
                    t.al9f.Add(new T9f(t.c04 + num24, to.al11[c13 >> 2], to.al10[c14], to.al12[c15], to.al12[c16]));
                }
            }
            int num25 = tbloff + posTbl.vc8;
            int vcc = posTbl.vcc;
            si.Position = num25;
            for (int num26 = 0; num26 < vcc; num26++)
            {
                int c17 = binaryReader.ReadByte();
                int c18 = binaryReader.ReadByte();
                int c19 = binaryReader.ReadByte();
                int c20 = binaryReader.ReadByte();
                int c21 = binaryReader.ReadUInt16();
                var item2 = new T2(c17, c18, c19, c20, c21);
                to.al2x.Add(item2);
            }
            for (int num27 = 0; num27 < vcc; num27++)
            {
                T2 t3 = to.al2x[num27];
                for (int num28 = 0; num28 < t3.c03; num28++)
                {
                    T9 t4 = to.al9[t3.c04 + num28];
                    int c22 = t4.c00;
                    int c23 = t4.c02;
                    int c24 = t4.c04;
                    int c25 = t4.c06;
                    t3.al9f.Add(new T9f(t3.c04 + num28, to.al11[c22 >> 2], to.al10[c23], to.al12[c24], to.al12[c25]));
                }
            }
            int num29 = tbloff + posTbl.ve0;
            int ve = posTbl.ve4;
            si.Position = num29;
            for (int num30 = 0; num30 < ve; num30++)
            {
                int c26 = binaryReader.ReadByte();
                int c27 = binaryReader.ReadByte();
                int c28 = binaryReader.ReadUInt16();
                int c29 = binaryReader.ReadUInt16();
                int c30 = binaryReader.ReadUInt16();
                uint c31 = binaryReader.ReadUInt32();
                to.al3.Add(new T3(c26, c27, c28, c29, c30, c31));
            }
            int num31 = tbloff + posTbl.vac;
            int va = posTbl.va2;
            si.Position = num31;
            for (int num32 = 0; num32 < va; num32++)
            {
                int c32 = binaryReader.ReadUInt16();
                int c33 = binaryReader.ReadUInt16();
                to.al4.Add(new T4(c32, c33));
            }
            to.off5 = tbloff + posTbl.va8;
            to.cnt5 = posTbl.va2 - posTbl.va0;
            si.Position = to.off5;
            for (int num33 = 0; num33 < to.cnt5; num33++)
            {
                var axBone = new AxBone();
                axBone.cur = binaryReader.ReadUInt16();
                axBone.parent = binaryReader.ReadUInt16();
                axBone.v08 = binaryReader.ReadUInt16();
                axBone.v0c = binaryReader.ReadUInt16();
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
                to.al5.Add(axBone);
            }
        }
    }
}