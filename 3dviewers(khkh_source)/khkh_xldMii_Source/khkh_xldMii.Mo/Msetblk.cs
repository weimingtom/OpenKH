namespace khkh_xldMii.Mo
{
    using khkh_xldMii.Mc;
    using System;
    using System.IO;

    public class Msetblk
    {
        public int cntb1;
        public int cntb2;
        public To to = new To();

        public Msetblk(Stream si)
        {
            PosTbl tbl = new PosTbl(si);
            int num = 0;
            int tbloff = tbl.tbloff;
            this.cntb1 = tbl.va0;
            this.cntb2 = tbl.va2;
            BinaryReader reader = new BinaryReader(si);
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            si.Position = ((num + tbloff) + tbl.vc0) - num;
            for (int i = 0; i < tbl.vc4; i++)
            {
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                int num7 = reader.ReadByte();
                int num8 = reader.ReadUInt16();
                num3 = Math.Max(num3, num8 + num7);
            }
            si.Position = ((num + tbloff) + tbl.vc8) - num;
            for (int j = 0; j < tbl.vcc; j++)
            {
                reader.ReadByte();
                reader.ReadByte();
                reader.ReadByte();
                int num10 = reader.ReadByte();
                int num11 = reader.ReadUInt16();
                num3 = Math.Max(num3, num11 + num10);
            }
            si.Position = ((num + tbloff) + tbl.vd0) - num;
            for (int k = 0; k < num3; k++)
            {
                reader.ReadUInt16();
                int num13 = reader.ReadUInt16();
                num4 = Math.Max(num4, num13 + 1);
                int num14 = reader.ReadUInt16();
                num5 = Math.Max(num5, num14 + 1);
                int num15 = reader.ReadUInt16();
                num5 = Math.Max(num5, num15 + 1);
            }
            int num16 = 0;
            si.Position = ((num + tbloff) + tbl.ve0) - num;
            for (int m = 0; m < tbl.ve4; m++)
            {
                reader.ReadUInt16();
                reader.ReadUInt16();
                reader.ReadUInt16();
                int num18 = reader.ReadInt16();
                num16 = Math.Max(num16, num18 + 1);
                reader.ReadUInt16();
                reader.ReadUInt16();
            }
            int num19 = tbloff + tbl.vb4;
            int num20 = tbl.vb8;
            si.Position = num19;
            for (int n = 0; n < num20; n++)
            {
                int num22 = reader.ReadUInt16();
                int num23 = reader.ReadUInt16();
                float num24 = reader.ReadSingle();
                this.to.al1.Add(new T1(num22, num23, num24));
            }
            int num25 = tbloff + tbl.vd8;
            si.Position = num25;
            this.to.al10 = new float[num4];
            for (int num26 = 0; num26 < num4; num26++)
            {
                this.to.al10[num26] = reader.ReadSingle();
            }
            int num27 = tbloff + tbl.vd4;
            int num28 = tbl.vb0;
            si.Position = num27;
            this.to.al11 = new float[num28];
            for (int num29 = 0; num29 < num28; num29++)
            {
                this.to.al11[num29] = reader.ReadSingle();
            }
            int num30 = tbloff + tbl.vdc;
            si.Position = num30;
            this.to.al12 = new float[num5];
            for (int num31 = 0; num31 < num5; num31++)
            {
                this.to.al12[num31] = reader.ReadSingle();
            }
            int num32 = tbloff + tbl.vd0;
            si.Position = num32;
            for (int num33 = 0; num33 < num3; num33++)
            {
                int num34 = reader.ReadUInt16();
                int num35 = reader.ReadUInt16();
                int num36 = reader.ReadUInt16();
                int num37 = reader.ReadUInt16();
                this.to.al9.Add(new T9(num34, num35, num36, num37));
            }
            int num38 = tbloff + tbl.vc0;
            int num39 = tbl.vc4;
            si.Position = num38;
            for (int num40 = 0; num40 < num39; num40++)
            {
                int num41 = reader.ReadByte();
                int num42 = reader.ReadByte();
                int num43 = reader.ReadByte();
                int num44 = reader.ReadByte();
                int num45 = reader.ReadUInt16();
                T2 item = new T2(num41, num42, num43, num44, num45);
                this.to.al2.Add(item);
            }
            for (int num46 = 0; num46 < num39; num46++)
            {
                T2 t2 = this.to.al2[num46];
                for (int num47 = 0; num47 < t2.c03; num47++)
                {
                    T9 t3 = this.to.al9[t2.c04 + num47];
                    int num48 = t3.c00;
                    int index = t3.c02;
                    int num50 = t3.c04;
                    int num51 = t3.c06;
                    t2.al9f.Add(new T9f(t2.c04 + num47, this.to.al11[num48 >> 2], this.to.al10[index], this.to.al12[num50], this.to.al12[num51]));
                }
            }
            int num52 = tbloff + tbl.vc8;
            int vcc = tbl.vcc;
            si.Position = num52;
            for (int num54 = 0; num54 < vcc; num54++)
            {
                int num55 = reader.ReadByte();
                int num56 = reader.ReadByte();
                int num57 = reader.ReadByte();
                int num58 = reader.ReadByte();
                int num59 = reader.ReadUInt16();
                T2 t4 = new T2(num55, num56, num57, num58, num59);
                this.to.al2x.Add(t4);
            }
            for (int num60 = 0; num60 < vcc; num60++)
            {
                T2 t5 = this.to.al2x[num60];
                for (int num61 = 0; num61 < t5.c03; num61++)
                {
                    T9 t6 = this.to.al9[t5.c04 + num61];
                    int num62 = t6.c00;
                    int num63 = t6.c02;
                    int num64 = t6.c04;
                    int num65 = t6.c06;
                    t5.al9f.Add(new T9f(t5.c04 + num61, this.to.al11[num62 >> 2], this.to.al10[num63], this.to.al12[num64], this.to.al12[num65]));
                }
            }
            int num66 = tbloff + tbl.ve0;
            int num67 = tbl.ve4;
            si.Position = num66;
            for (int num68 = 0; num68 < num67; num68++)
            {
                int num69 = reader.ReadByte();
                int num70 = reader.ReadByte();
                int num71 = reader.ReadUInt16();
                int num72 = reader.ReadUInt16();
                int num73 = reader.ReadUInt16();
                uint num74 = reader.ReadUInt32();
                this.to.al3.Add(new T3(num69, num70, num71, num72, num73, num74));
            }
            int num75 = tbloff + tbl.vac;
            int num76 = tbl.va2;
            si.Position = num75;
            for (int num77 = 0; num77 < num76; num77++)
            {
                int num78 = reader.ReadUInt16();
                int num79 = reader.ReadUInt16();
                this.to.al4.Add(new T4(num78, num79));
            }
            this.to.off5 = tbloff + tbl.va8;
            this.to.cnt5 = tbl.va2 - tbl.va0;
            si.Position = this.to.off5;
            for (int num80 = 0; num80 < this.to.cnt5; num80++)
            {
                AxBone bone = new AxBone {
                    cur = reader.ReadUInt16(),
                    parent = reader.ReadUInt16(),
                    v08 = reader.ReadUInt16(),
                    v0c = reader.ReadUInt16()
                };
                reader.ReadUInt64();
                bone.x1 = reader.ReadSingle();
                bone.y1 = reader.ReadSingle();
                bone.z1 = reader.ReadSingle();
                bone.w1 = reader.ReadSingle();
                bone.x2 = reader.ReadSingle();
                bone.y2 = reader.ReadSingle();
                bone.z2 = reader.ReadSingle();
                bone.w2 = reader.ReadSingle();
                bone.x3 = reader.ReadSingle();
                bone.y3 = reader.ReadSingle();
                bone.z3 = reader.ReadSingle();
                bone.w3 = reader.ReadSingle();
                this.to.al5.Add(bone);
            }
        }
    }
}

