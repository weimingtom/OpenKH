using System.Collections.Generic;
using System.IO;
using hex04BinTrack;
using khkh_xldMii.Mc;
using khkh_xldMii.Mx;
using khkh_xldMii.V;
using SlimDX;

namespace khiiMapv
{
    public class Parse4Mdlx
    {
        public SortedDictionary<int, Model> dictModel = new SortedDictionary<int, Model>();
        public Mdlxfst mdlx;
        private Naming naming = new Naming();
        /// <summary>
        /// Function that parse MDLX files
        /// </summary>
        /// <param name="entbin">byte array of the file</param>
        public Parse4Mdlx(byte[] entbin)
        {
            mdlx = new Mdlxfst(new MemoryStream(entbin, false));
            float scale = 1f;
            using (List<T31>.Enumerator enumerator = mdlx.alt31.GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    T31 current = enumerator.Current;
                    AxBone[] array = current.t21.alaxb.ToArray();
                    var array2 = new Matrix[array.Length];
                    var array3 = new Vector3[array2.Length];
                    var array4 = new Quaternion[array2.Length];
                    for (int i = 0; i < array2.Length; i++)
                    {
                        AxBone axBone = array[i];
                        int parent = axBone.parent;
                        Quaternion quaternion;
                        Vector3 left;
                        if (parent < 0)
                        {
                            quaternion = Quaternion.Identity;
                            left = Vector3.Zero;
                        }
                        else
                        {
                            quaternion = array4[parent];
                            left = array3[parent];
                        }
                        Vector3 right = Vector3.TransformCoordinate(new Vector3(axBone.x3, axBone.y3, axBone.z3),
                            Matrix.RotationQuaternion(quaternion));
                        array3[i] = left + right;
                        Quaternion left2 = Quaternion.Identity;
                        if (axBone.x2 != 0f)
                        {
                            left2 *= Quaternion.RotationAxis(new Vector3(1f, 0f, 0f), axBone.x2);
                        }
                        if (axBone.y2 != 0f)
                        {
                            left2 *= Quaternion.RotationAxis(new Vector3(0f, 1f, 0f), axBone.y2);
                        }
                        if (axBone.z2 != 0f)
                        {
                            left2 *= Quaternion.RotationAxis(new Vector3(0f, 0f, 1f), axBone.z2);
                        }
                        array4[i] = left2*quaternion;
                    }
                    for (int j = 0; j < array2.Length; j++)
                    {
                        Matrix matrix = Matrix.RotationQuaternion(array4[j]);
                        matrix *= Matrix.Translation(array3[j]);
                        array2[j] = matrix;
                    }
                    var list = new List<Body1e>();
                    Matrix identity = Matrix.Identity;
                    foreach (T13vif current2 in current.al13)
                    {
                        var vU1Mem = new VU1Mem();
                        int tops = 64;
                        int top = 544;
                        new ParseVIF1(vU1Mem).Parse(new MemoryStream(current2.bin, false), tops);
                        Body1e item = SimaVU1e.Sima(vU1Mem, array2, tops, top, current2.texi, current2.alaxi, identity);
                        list.Add(item);
                    }
                    var ffMesh = new ffMesh();
                    int num = 0;
                    int num2 = 0;
                    var array5 = new ff1[4];
                    int num3 = 0;
                    int[] array6 =
                    {
                        1,
                        3,
                        2
                    };
                    foreach (Body1e current3 in list)
                    {
                        for (int k = 0; k < current3.alvi.Length; k++)
                        {
                            var ff = new ff1(num + current3.alvi[k], num2 + k);
                            array5[num3] = ff;
                            num3 = (num3 + 1 & 3);
                            int num4 = current3.alfl[k];
                            if (num4 == 32 || num4 == 0)
                            {
                                var item2 = new ff3(current3.t, array5[num3 - array6[0] & 3],
                                    array5[num3 - array6[1] & 3], array5[num3 - array6[2] & 3]);
                                ffMesh.al3.Add(item2);
                            }
                            if (num4 == 48 || num4 == 0)
                            {
                                var item3 = new ff3(current3.t, array5[num3 - array6[0] & 3],
                                    array5[num3 - array6[2] & 3], array5[num3 - array6[1] & 3]);
                                ffMesh.al3.Add(item3);
                            }
                        }
                        for (int l = 0; l < current3.alvertraw.Length; l++)
                        {
                            if (current3.alalni[l] == null)
                            {
                                ffMesh.alpos.Add(Vector3.Zero);
                                ffMesh.almtxuse.Add(new MJ1[0]);
                            }
                            else
                            {
                                if (current3.alalni[l].Length == 1)
                                {
                                    MJ1 mJ = current3.alalni[l][0];
                                    mJ.factor = 1f;
                                    Vector3 item4 =
                                        Vector3.TransformCoordinate(VCUt.V4To3(current3.alvertraw[mJ.vertexIndex]),
                                            array2[mJ.matrixIndex]);
                                    ffMesh.alpos.Add(item4);
                                }
                                else
                                {
                                    Vector3 vector = Vector3.Zero;
                                    MJ1[] array7 = current3.alalni[l];
                                    for (int m = 0; m < array7.Length; m++)
                                    {
                                        MJ1 mJ2 = array7[m];
                                        vector +=
                                            VCUt.V4To3(Vector4.Transform(current3.alvertraw[mJ2.vertexIndex],
                                                array2[mJ2.matrixIndex]));
                                    }
                                    ffMesh.alpos.Add(vector);
                                }
                                ffMesh.almtxuse.Add(current3.alalni[l]);
                            }
                        }
                        for (int n = 0; n < current3.aluv.Length; n++)
                        {
                            Vector2 item5 = current3.aluv[n];
                            item5.Y = 1f - item5.Y;
                            ffMesh.alst.Add(item5);
                        }
                        num += current3.alvertraw.Length;
                        num2 += current3.aluv.Length;
                    }
                    int count = ffMesh.al3.Count;
                    for (int num5 = 0; num5 < count; num5++)
                    {
                        ff3 ff2 = ffMesh.al3[num5];
                        Model model;
                        if (!dictModel.TryGetValue(ff2.texi, out model))
                        {
                            model = (dictModel[ff2.texi] = new Model());
                        }
                        for (int num6 = 0; num6 < ff2.al1.Length; num6++)
                        {
                            ff1 ff3 = ff2.al1[num6];
                            Vector3 v = ffMesh.alpos[ff3.vi]*scale;
                            Vector2 vector2 = ffMesh.alst[ff3.ti];
                            model.alv.Add(new CustomVertex.PositionColoredTextured(v, -1, vector2.X, 1f - vector2.Y));
                        }
                    }
                }
            }
        }

        private class LUt
        {
            /// <summary>
            /// Get faces of the MDLX files(emotions)
            /// </summary>
            /// <param name="X3">X, Y and Z arrays</param>
            /// <returns></returns>
            public static string GetFaces(ff3 X3)
            {
                string str = "";
                return str + string.Format("{0},{1},{2}", X3.al1[0].vi, X3.al1[1].vi, X3.al1[2].vi);
            }
            /// <summary>
            /// Functions that will get the UV of a file
            /// </summary>
            /// <param name="X3">X, Y and Z arrays</param>
            /// <param name="alst">List of the vectors</param>
            /// <returns></returns>
            public static object GetUV(ff3 X3, List<Vector2> alst)
            {
                string text = "";
                ff1[] al = X3.al1;
                for (int i = 0; i < al.Length; i++)
                {
                    ff1 ff = al[i];
                    Vector2 vector = alst[ff.ti];
                    text += string.Format("Blender.Mathutils.Vector({0:r},{1:r}),", vector.X, vector.Y);
                }
                return text;
            }

            public static string GetVi(List<int> al, IDictionary<int, List<int>> map)
            {
                string text = "";
                foreach (int current in al)
                {
                    if (map.ContainsKey(current))
                    {
                        foreach (int current2 in map[current])
                        {
                            text = text + current2 + ",";
                        }
                    }
                }
                return "[" + text + "]";
            }
        }

        private class LocalsMJ1 : IComparer<MJ1>
        {
            public int Compare(MJ1 x, MJ1 y)
            {
                int num = x.matrixIndex.CompareTo(y.matrixIndex);
                if (num != 0)
                {
                    return num;
                }
                num = -x.factor.CompareTo(y.factor);
                if (num != 0)
                {
                    return num;
                }
                return 0;
            }
        }

        private class Mati
        {
            public string fp;
            public string matname;
            public string texname;

            public Mati(string basename, string fp)
            {
                matname = "mat" + basename;
                texname = "tex" + basename;
                this.fp = fp;
            }
        }

        public class Model
        {
            public List<CustomVertex.PositionColoredTextured> alv = new List<CustomVertex.PositionColoredTextured>();
        }

        private class Naming
        {
            public string B(int x)
            {
                return "B" + x.ToString("000");
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
                svi = startVertexIndex;
                cnt = cntPrimitives;
                t = ti;
                this.sel = sel;
            }
        }

        private class VCUt
        {
            public static Vector3 V4To3(Vector4 v)
            {
                return new Vector3(v.X, v.Y, v.Z);
            }
        }

        private class ff1
        {
            public int ti;
            public int vi;

            public ff1(int vi, int ti)
            {
                this.vi = vi;
                this.ti = ti;
            }
        }

        private class ff3
        {
            public ff1[] al1;
            public int texi;

            public ff3(int texi, ff1 x, ff1 y, ff1 z)
            {
                this.texi = texi;
                al1 = new[]
                {
                    x,
                    y,
                    z
                };
            }
        }

        private class ffMesh
        {
            public List<ff3> al3 = new List<ff3>();
            public List<MJ1[]> almtxuse = new List<MJ1[]>();
            public List<Vector3> alpos = new List<Vector3>();
            public List<Vector2> alst = new List<Vector2>();
        }
    }
}