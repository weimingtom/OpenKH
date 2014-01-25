namespace khiiMapv
{
    using hex04BinTrack;
    using khkh_xldMii.Mc;
    using khkh_xldMii.Mx;
    using khkh_xldMii.V;
    using SlimDX;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Parse4Mdlx
    {
        public SortedDictionary<int, Model> dictModel;
        public Mdlxfst mdlx;
        private Naming naming;

        public unsafe Parse4Mdlx(byte[] entbin)
        {
            float num;
            T31 t;
            AxBone[] boneArray;
            Matrix[] matrixArray;
            Vector3[] vectorArray;
            Quaternion[] quaternionArray;
            int num2;
            Quaternion quaternion;
            Vector3 vector;
            AxBone bone;
            int num3;
            Vector3 vector2;
            Quaternion quaternion2;
            int num4;
            Matrix matrix;
            List<Body1e> list;
            Matrix matrix2;
            T13vif tvif;
            VU1Mem mem;
            int num5;
            int num6;
            Body1e bodye;
            ffMesh mesh;
            int num7;
            int num8;
            ff1[] ffArray;
            int num9;
            int[] numArray;
            Body1e bodye2;
            int num10;
            ff1 ff;
            int num11;
            Parse4Mdlx.ff3 ff2;
            Parse4Mdlx.ff3 ff3;
            int num12;
            MJ1 mj;
            Vector3 vector3;
            Vector3 vector4;
            MJ1 mj2;
            int num13;
            Vector2 vector5;
            int num14;
            int num15;
            Parse4Mdlx.ff3 ff4;
            Model model;
            int num16;
            ff1 ff5;
            Vector3 vector6;
            Vector2 vector7;
            List<T31>.Enumerator enumerator;
            List<T13vif>.Enumerator enumerator2;
            List<Body1e>.Enumerator enumerator3;
            MJ1[] mjArray;
            int num17;
            this.dictModel = new SortedDictionary<int, Model>();
            this.naming = new Naming();
            base..ctor();
            this.mdlx = new Mdlxfst(new MemoryStream(entbin, 0));
            num = 1f;
            enumerator = this.mdlx.alt31.GetEnumerator();
        Label_0046:
            try
            {
                goto Label_06A5;
            Label_004B:
                t = &enumerator.Current;
                boneArray = t.t21.alaxb.ToArray();
                matrixArray = new Matrix[(int) boneArray.Length];
                vectorArray = new Vector3[(int) matrixArray.Length];
                quaternionArray = new Quaternion[(int) matrixArray.Length];
                num2 = 0;
                goto Label_01D5;
            Label_0089:
                bone = boneArray[num2];
                num3 = bone.parent;
                if (num3 >= 0)
                {
                    goto Label_00AD;
                }
                quaternion = Quaternion.Identity;
                vector = Vector3.Zero;
                goto Label_00CD;
            Label_00AD:
                quaternion = *(&(quaternionArray[num3]));
                vector = *(&(vectorArray[num3]));
            Label_00CD:
                vector2 = Vector3.TransformCoordinate(new Vector3(bone.x3, bone.y3, bone.z3), Matrix.RotationQuaternion(quaternion));
                *(&(vectorArray[num2])) = vector + vector2;
                quaternion2 = Quaternion.Identity;
                if (bone.x2 == 0f)
                {
                    goto Label_014A;
                }
                quaternion2 *= Quaternion.RotationAxis(new Vector3(1f, 0f, 0f), bone.x2);
            Label_014A:
                if (bone.y2 == 0f)
                {
                    goto Label_0181;
                }
                quaternion2 *= Quaternion.RotationAxis(new Vector3(0f, 1f, 0f), bone.y2);
            Label_0181:
                if (bone.z2 == 0f)
                {
                    goto Label_01B8;
                }
                quaternion2 *= Quaternion.RotationAxis(new Vector3(0f, 0f, 1f), bone.z2);
            Label_01B8:
                *(&(quaternionArray[num2])) = quaternion2 * quaternion;
                num2 += 1;
            Label_01D5:
                if (num2 < ((int) matrixArray.Length))
                {
                    goto Label_0089;
                }
                num4 = 0;
                goto Label_022A;
            Label_01E4:
                matrix = Matrix.RotationQuaternion(*(&(quaternionArray[num4]))) * Matrix.Translation(*(&(vectorArray[num4])));
                *(&(matrixArray[num4])) = matrix;
                num4 += 1;
            Label_022A:
                if (num4 < ((int) matrixArray.Length))
                {
                    goto Label_01E4;
                }
                list = new List<Body1e>();
                matrix2 = Matrix.Identity;
                enumerator2 = t.al13.GetEnumerator();
            Label_024C:
                try
                {
                    goto Label_02AB;
                Label_024E:
                    tvif = &enumerator2.Current;
                    mem = new VU1Mem();
                    num5 = 0x40;
                    num6 = 0x220;
                    new ParseVIF1(mem).Parse(new MemoryStream(tvif.bin, 0), num5);
                    bodye = SimaVU1e.Sima(mem, matrixArray, num5, num6, tvif.texi, tvif.alaxi, matrix2);
                    list.Add(bodye);
                Label_02AB:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_024E;
                    }
                    goto Label_02C4;
                }
                finally
                {
                Label_02B6:
                    &enumerator2.Dispose();
                }
            Label_02C4:
                mesh = new ffMesh();
                num7 = 0;
                num8 = 0;
                ffArray = new ff1[4];
                num9 = 0;
                numArray = new int[] { 1, 3, 2 };
                enumerator3 = list.GetEnumerator();
            Label_02F8:
                try
                {
                    goto Label_05A6;
                Label_02FD:
                    bodye2 = &enumerator3.Current;
                    num10 = 0;
                    goto Label_03DC;
                Label_030E:
                    ff = new ff1(num7 + bodye2.alvi[num10], num8 + num10);
                    ffArray[num9] = ff;
                    num9 = (num9 + 1) & 3;
                    num11 = bodye2.alfl[num10];
                    if (num11 == 0x20)
                    {
                        goto Label_034C;
                    }
                    if (num11 != null)
                    {
                        goto Label_038C;
                    }
                Label_034C:
                    ff2 = new Parse4Mdlx.ff3(bodye2.t, ffArray[(num9 - numArray[0]) & 3], ffArray[(num9 - numArray[1]) & 3], ffArray[(num9 - numArray[2]) & 3]);
                    mesh.al3.Add(ff2);
                Label_038C:
                    if (num11 == 0x30)
                    {
                        goto Label_0396;
                    }
                    if (num11 != null)
                    {
                        goto Label_03D6;
                    }
                Label_0396:
                    ff3 = new Parse4Mdlx.ff3(bodye2.t, ffArray[(num9 - numArray[0]) & 3], ffArray[(num9 - numArray[2]) & 3], ffArray[(num9 - numArray[1]) & 3]);
                    mesh.al3.Add(ff3);
                Label_03D6:
                    num10 += 1;
                Label_03DC:
                    if (num10 < ((int) bodye2.alvi.Length))
                    {
                        goto Label_030E;
                    }
                    num12 = 0;
                    goto Label_052B;
                Label_03F4:
                    if (bodye2.alalni[num12] != null)
                    {
                        goto Label_0428;
                    }
                    mesh.alpos.Add(Vector3.Zero);
                    mesh.almtxuse.Add(new MJ1[0]);
                    goto Label_0525;
                Label_0428:
                    if (((int) bodye2.alalni[num12].Length) != 1)
                    {
                        goto Label_0497;
                    }
                    mj = bodye2.alalni[num12][0];
                    mj.factor = 1f;
                    vector3 = Vector3.TransformCoordinate(VCUt.V4To3(*(&(bodye2.alvertraw[mj.vertexIndex]))), *(&(matrixArray[mj.matrixIndex])));
                    mesh.alpos.Add(vector3);
                    goto Label_050F;
                Label_0497:
                    vector4 = Vector3.Zero;
                    mjArray = bodye2.alalni[num12];
                    num17 = 0;
                    goto Label_04F9;
                Label_04AF:
                    mj2 = mjArray[num17];
                    vector4 += VCUt.V4To3(Vector4.Transform(*(&(bodye2.alvertraw[mj2.vertexIndex])), *(&(matrixArray[mj2.matrixIndex]))));
                    num17 += 1;
                Label_04F9:
                    if (num17 < ((int) mjArray.Length))
                    {
                        goto Label_04AF;
                    }
                    mesh.alpos.Add(vector4);
                Label_050F:
                    mesh.almtxuse.Add(bodye2.alalni[num12]);
                Label_0525:
                    num12 += 1;
                Label_052B:
                    if (num12 < ((int) bodye2.alvertraw.Length))
                    {
                        goto Label_03F4;
                    }
                    num13 = 0;
                    goto Label_057D;
                Label_0540:
                    vector5 = *(&(bodye2.aluv[num13]));
                    &vector5.Y = 1f - &vector5.Y;
                    mesh.alst.Add(vector5);
                    num13 += 1;
                Label_057D:
                    if (num13 < ((int) bodye2.aluv.Length))
                    {
                        goto Label_0540;
                    }
                    num7 += (int) bodye2.alvertraw.Length;
                    num8 += (int) bodye2.aluv.Length;
                Label_05A6:
                    if (&enumerator3.MoveNext() != null)
                    {
                        goto Label_02FD;
                    }
                    goto Label_05C2;
                }
                finally
                {
                Label_05B4:
                    &enumerator3.Dispose();
                }
            Label_05C2:
                num14 = mesh.al3.Count;
                num15 = 0;
                goto Label_069A;
            Label_05D8:
                ff4 = mesh.al3[num15];
                if (this.dictModel.TryGetValue(ff4.texi, &model) != null)
                {
                    goto Label_0618;
                }
                this.dictModel[ff4.texi] = model = new Model();
            Label_0618:
                num16 = 0;
                goto Label_0687;
            Label_061D:
                ff5 = ff4.al1[num16];
                vector6 = mesh.alpos[ff5.vi] * num;
                vector7 = mesh.alst[ff5.ti];
                model.alv.Add(new CustomVertex.PositionColoredTextured(vector6, -1, &vector7.X, 1f - &vector7.Y));
                num16 += 1;
            Label_0687:
                if (num16 < ((int) ff4.al1.Length))
                {
                    goto Label_061D;
                }
                num15 += 1;
            Label_069A:
                if (num15 < num14)
                {
                    goto Label_05D8;
                }
                goto Label_06B1;
            Label_06A5:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_004B;
                }
            Label_06B1:
                goto Label_06C1;
            }
            finally
            {
            Label_06B3:
                &enumerator.Dispose();
            }
        Label_06C1:
            return;
        }

        private class ff1
        {
            public int ti;
            public int vi;

            public ff1(int vi, int ti)
            {
                base..ctor();
                this.vi = vi;
                this.ti = ti;
                return;
            }
        }

        private class ff3
        {
            public Parse4Mdlx.ff1[] al1;
            public int texi;

            public ff3(int texi, Parse4Mdlx.ff1 x, Parse4Mdlx.ff1 y, Parse4Mdlx.ff1 z)
            {
                Parse4Mdlx.ff1[] ffArray;
                base..ctor();
                this.texi = texi;
                this.al1 = new Parse4Mdlx.ff1[] { x, y, z };
                return;
            }
        }

        private class ffMesh
        {
            public List<Parse4Mdlx.ff3> al3;
            public List<MJ1[]> almtxuse;
            public List<Vector3> alpos;
            public List<Vector2> alst;

            public ffMesh()
            {
                this.alpos = new List<Vector3>();
                this.alst = new List<Vector2>();
                this.al3 = new List<Parse4Mdlx.ff3>();
                this.almtxuse = new List<MJ1[]>();
                base..ctor();
                return;
            }
        }

        private class LocalsMJ1 : IComparer<MJ1>
        {
            public LocalsMJ1()
            {
                base..ctor();
                return;
            }

            public unsafe int Compare(MJ1 x, MJ1 y)
            {
                int num;
                num = &x.matrixIndex.CompareTo(y.matrixIndex);
                if (num == null)
                {
                    goto Label_0017;
                }
                return num;
            Label_0017:
                num = -&x.factor.CompareTo(y.factor);
                if (num == null)
                {
                    goto Label_002F;
                }
                return num;
            Label_002F:
                return 0;
            }
        }

        private class LUt
        {
            public LUt()
            {
                base..ctor();
                return;
            }

            public static string GetFaces(Parse4Mdlx.ff3 X3)
            {
                string str;
                str = "";
                return (str + string.Format("{0},{1},{2}", (int) X3.al1[0].vi, (int) X3.al1[1].vi, (int) X3.al1[2].vi));
            }

            public static unsafe object GetUV(Parse4Mdlx.ff3 X3, List<Vector2> alst)
            {
                string str;
                Parse4Mdlx.ff1 ff;
                Vector2 vector;
                Parse4Mdlx.ff1[] ffArray;
                int num;
                str = "";
                ffArray = X3.al1;
                num = 0;
                goto Label_0053;
            Label_0012:
                ff = ffArray[num];
                vector = alst[ff.ti];
                str = str + string.Format("Blender.Mathutils.Vector({0:r},{1:r}),", (float) &vector.X, (float) &vector.Y);
                num += 1;
            Label_0053:
                if (num < ((int) ffArray.Length))
                {
                    goto Label_0012;
                }
                return str;
            }

            public static unsafe string GetVi(List<int> al, IDictionary<int, List<int>> map)
            {
                string str;
                int num;
                int num2;
                List<int>.Enumerator enumerator;
                List<int>.Enumerator enumerator2;
                str = "";
                enumerator = al.GetEnumerator();
            Label_000D:
                try
                {
                    goto Label_0063;
                Label_000F:
                    num = &enumerator.Current;
                    if (map.ContainsKey(num) == null)
                    {
                        goto Label_0063;
                    }
                    enumerator2 = map[num].GetEnumerator();
                Label_002E:
                    try
                    {
                        goto Label_004A;
                    Label_0030:
                        num2 = &enumerator2.Current;
                        str = str + ((int) num2) + ",";
                    Label_004A:
                        if (&enumerator2.MoveNext() != null)
                        {
                            goto Label_0030;
                        }
                        goto Label_0063;
                    }
                    finally
                    {
                    Label_0055:
                        &enumerator2.Dispose();
                    }
                Label_0063:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_000F;
                    }
                    goto Label_007C;
                }
                finally
                {
                Label_006E:
                    &enumerator.Dispose();
                }
            Label_007C:
                return ("[" + str + "]");
            }
        }

        private class Mati
        {
            public string fp;
            public string matname;
            public string texname;

            public Mati(string basename, string fp)
            {
                base..ctor();
                this.matname = "mat" + basename;
                this.texname = "tex" + basename;
                this.fp = fp;
                return;
            }
        }

        public class Model
        {
            public List<CustomVertex.PositionColoredTextured> alv;

            public Model()
            {
                this.alv = new List<CustomVertex.PositionColoredTextured>();
                base..ctor();
                return;
            }
        }

        private class Naming
        {
            public Naming()
            {
                base..ctor();
                return;
            }

            public unsafe string B(int x)
            {
                return ("B" + &x.ToString("000"));
            }
        }

        private class Sepa
        {
            public int cnt;
            public int sel;
            public int svi;
            public int t;

            public Sepa(int startVertexIndex, int cntPrimitives, int ti, int sel)
            {
                base..ctor();
                this.svi = startVertexIndex;
                this.cnt = cntPrimitives;
                this.t = ti;
                this.sel = sel;
                return;
            }
        }

        private class VCUt
        {
            public VCUt()
            {
                base..ctor();
                return;
            }

            public static unsafe Vector3 V4To3(Vector4 v)
            {
                return new Vector3(&v.X, &v.Y, &v.Z);
            }
        }
    }
}

