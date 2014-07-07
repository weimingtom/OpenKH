using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using hex04BinTrack;
using khiiMapv.CollTest;
using khiiMapv.Put;
using SlimDX;
using SlimDX.Direct3D9;

namespace khiiMapv
{
    public partial class Visf : Form
    {
        public Visf()
        {
            InitializeComponent();
        }

        public Visf(List<DC> aldc, CollReader coll)
        {
            this.aldc = aldc;
            this.coll = coll;
            InitializeComponent();
        }

        private PresentParameters PP
        {
            get
            {
                return new PresentParameters
                {
                    Windowed = true,
                    SwapEffect = SwapEffect.Discard,
                    AutoDepthStencilFormat = Format.D24X8,
                    EnableAutoDepthStencil = true,
                    BackBufferHeight = 1024,
                    BackBufferWidth = 1024
                };
            }
        }

        private Vector3 CameraEye
        {
            get
            {
                return new Vector3(Convert.ToSingle(eyeX.Value), Convert.ToSingle(eyeY.Value),
                    Convert.ToSingle(eyeZ.Value));
            }
            set
            {
                eyeX.Value = Math.Max(eyeX.Minimum, Math.Min(eyeX.Maximum, (decimal) value.X));
                eyeY.Value = Math.Max(eyeY.Minimum, Math.Min(eyeY.Maximum, (decimal) value.Y));
                eyeZ.Value = Math.Max(eyeZ.Minimum, Math.Min(eyeZ.Maximum, (decimal) value.Z));
            }
        }

        private Vector3 Target
        {
            get
            {
                return Vector3.TransformCoordinate(Vector3.UnitX,
                    Matrix.RotationYawPitchRoll(Convert.ToSingle(yaw.Value)/180f*3.14159f,
                        Convert.ToSingle(pitch.Value)/180f*3.14159f, Convert.ToSingle(roll.Value)/180f*3.14159f));
            }
        }

        private Vector3 CameraUp
        {
            get
            {
                return Vector3.TransformCoordinate(Vector3.UnitY,
                    Matrix.RotationYawPitchRoll(0f, Convert.ToSingle(pitch.Value)/180f*3.14159f, 0f));
            }
        }

        private Vector3 TargetX
        {
            get
            {
                return Vector3.TransformCoordinate(Vector3.UnitX,
                    Matrix.RotationYawPitchRoll(Convert.ToSingle(yaw.Value)/180f*3.14159f, 0f, 0f));
            }
        }

        private Vector3 LeftVec
        {
            get
            {
                return Vector3.TransformCoordinate(Vector3.TransformCoordinate(TargetX, Matrix.RotationY(-1.570795f)),
                    Matrix.RotationYawPitchRoll(0f, Convert.ToSingle(pitch.Value)/180f*3.14159f, 0f));
            }
        }

        private int Speed
        {
            get
            {
                if ((ModifierKeys & Keys.Shift) == Keys.None)
                {
                    return 30;
                }
                return 60;
            }
        }

        private void p1_Load(object sender, EventArgs e)
        {
            p3D = new Direct3D();
            alDeleter.Add(p3D);
            device = new Device(p3D, 0, DeviceType.Hardware, p1.Handle, CreateFlags.HardwareVertexProcessing, new[]
            {
                PP
            });
            alDeleter.Add(device);
            device.SetRenderState(RenderState.Lighting, false);
            device.SetRenderState(RenderState.ZEnable, true);
            device.SetRenderState(RenderState.AlphaTestEnable, true);
            device.SetRenderState(RenderState.AlphaRef, 2);
            device.SetRenderState(RenderState.AlphaFunc, Compare.GreaterEqual);
            device.SetRenderState(RenderState.AlphaBlendEnable, true);
            device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
            device.SetRenderState(RenderState.SourceBlendAlpha, Blend.SourceAlpha);
            device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
            device.SetRenderState(RenderState.DestinationBlendAlpha, Blend.InverseSourceAlpha);
            device.SetRenderState(RenderState.CullMode, Cull.Counterclockwise);
            device.SetRenderState(RenderState.FogColor, p1.BackColor.ToArgb());
            device.SetRenderState(RenderState.FogStart, 5f);
            device.SetRenderState(RenderState.FogEnd, 30000f);
            device.SetRenderState(RenderState.FogDensity, 0.0001f);
            device.SetRenderState(RenderState.FogVertexMode, FogMode.Exponential);
            p1.MouseWheel += p1_MouseWheel;
            var list = new List<CustomVertex.PositionColoredTextured>();
            foreach (DC current in aldc)
            {
                var toolStripButton = new ToolStripButton("Show " + current.name);
                toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
                toolStripButton.CheckOnClick = true;
                toolStripButton.Tag = current.dcId;
                toolStripButton.Checked = true;
                toolStripButton.CheckedChanged += tsiIfRender_CheckedChanged;
                toolStrip1.Items.Insert(toolStrip1.Items.IndexOf(tsbShowColl), toolStripButton);
            }
            alci.Clear();
            altex.Clear();
            var array = new int[4];
            int num = 0;
            foreach (DC current2 in aldc)
            {
                int count = altex.Count;
                Bitmap[] pics = current2.o7.pics;
                for (int i = 0; i < pics.Length; i++)
                {
                    Bitmap bitmap = pics[i];
                    var memoryStream = new MemoryStream();
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    memoryStream.Position = 0L;
                    Texture item;
                    altex.Add(item = Texture.FromStream(device, memoryStream));
                    alDeleter.Add(item);
                }
                if (current2.o4Mdlx != null)
                {
                    using (
                        SortedDictionary<int, Parse4Mdlx.Model>.Enumerator enumerator3 =
                            current2.o4Mdlx.dictModel.GetEnumerator())
                    {
                        while (enumerator3.MoveNext())
                        {
                            KeyValuePair<int, Parse4Mdlx.Model> current3 = enumerator3.Current;
                            var cI = new CI();
                            int count2 = list.Count;
                            Parse4Mdlx.Model value = current3.Value;
                            list.AddRange(value.alv);
                            var list2 = new List<uint>();
                            for (int j = 0; j < value.alv.Count; j++)
                            {
                                list2.Add((uint) (count2 + j));
                            }
                            cI.ali = list2.ToArray();
                            cI.texi = count + current3.Key;
                            cI.vifi = 0;
                            alci.Add(cI);
                        }
                        goto IL_75D;
                    }
                }
                goto IL_3C9;
                IL_75D:
                alalci.Add(alci.ToArray());
                alci.Clear();
                continue;
                IL_3C9:
                if (current2.o4Map != null)
                {
                    for (int k = 0; k < current2.o4Map.alvifpkt.Count; k++)
                    {
                        Vifpli vifpli = current2.o4Map.alvifpkt[k];
                        byte[] vifpkt = vifpli.vifpkt;
                        var vu = new VU1Mem();
                        var parseVIF = new ParseVIF1(vu);
                        parseVIF.Parse(new MemoryStream(vifpkt, false), 0);
                        foreach (var current4 in parseVIF.almsmem)
                        {
                            var cI2 = new CI();
                            var memoryStream2 = new MemoryStream(current4, false);
                            var binaryReader = new BinaryReader(memoryStream2);
                            binaryReader.ReadInt32();
                            binaryReader.ReadInt32();
                            binaryReader.ReadInt32();
                            binaryReader.ReadInt32();
                            int num2 = binaryReader.ReadInt32();
                            int num3 = binaryReader.ReadInt32();
                            binaryReader.ReadInt32();
                            binaryReader.ReadInt32();
                            binaryReader.ReadInt32();
                            int num4 = binaryReader.ReadInt32();
                            binaryReader.ReadInt32();
                            binaryReader.ReadInt32();
                            binaryReader.ReadInt32();
                            int num5 = binaryReader.ReadInt32();
                            var list3 = new List<uint>();
                            int count3 = list.Count;
                            for (int l = 0; l < num2; l++)
                            {
                                memoryStream2.Position = 16*(num3 + l);
                                int num6 = binaryReader.ReadInt16();
                                binaryReader.ReadInt16();
                                int num7 = binaryReader.ReadInt16();
                                binaryReader.ReadInt16();
                                int num8 = binaryReader.ReadInt16();
                                binaryReader.ReadInt16();
                                int num9 = binaryReader.ReadInt16();
                                binaryReader.ReadInt16();
                                memoryStream2.Position = 16*(num5 + num8);
                                Vector3 v;
                                v.X = -binaryReader.ReadSingle();
                                v.Y = binaryReader.ReadSingle();
                                v.Z = binaryReader.ReadSingle();
                                memoryStream2.Position = 16*(num4 + l);
                                int num10 = (byte) binaryReader.ReadUInt32();
                                int num11 = (byte) binaryReader.ReadUInt32();
                                int num12 = (byte) binaryReader.ReadUInt32();
                                int num13 = (byte) binaryReader.ReadUInt32();
                                if (num4 == 0)
                                {
                                    num10 = 255;
                                    num11 = 255;
                                    num12 = 255;
                                    num13 = 255;
                                }
                                array[num & 3] = count3 + l;
                                num++;
                                if (num9 != 0 && num9 != 16)
                                {
                                    if (num9 == 32)
                                    {
                                        list3.Add(Convert.ToUInt32(array[num - 1 & 3]));
                                        list3.Add(Convert.ToUInt32(array[num - 2 & 3]));
                                        list3.Add(Convert.ToUInt32(array[num - 3 & 3]));
                                    }
                                    else
                                    {
                                        if (num9 == 48)
                                        {
                                            list3.Add(Convert.ToUInt32(array[num - 1 & 3]));
                                            list3.Add(Convert.ToUInt32(array[num - 3 & 3]));
                                            list3.Add(Convert.ToUInt32(array[num - 2 & 3]));
                                        }
                                    }
                                }
                                Color color = Color.FromArgb(lm.al[num13], lm.al[num10], lm.al[num11], lm.al[num12]);
                                var item2 = new CustomVertex.PositionColoredTextured(v, color.ToArgb(), num6/16f/256f,
                                    num7/16f/256f);
                                list.Add(item2);
                            }
                            cI2.ali = list3.ToArray();
                            cI2.texi = count + vifpli.texi;
                            cI2.vifi = k;
                            alci.Add(cI2);
                        }
                    }
                }
                goto IL_75D;
            }
            if (alalci.Count != 0)
            {
                alci.Clear();
                alci.AddRange(alalci[0]);
            }
            if (list.Count == 0)
            {
                list.Add(default(CustomVertex.PositionColoredTextured));
            }
            vb = new VertexBuffer(device, (cntVerts = list.Count)*CustomVertex.PositionColoredTextured.Size,
                Usage.Points, CustomVertex.PositionColoredTextured.Format, Pool.Managed);
            alDeleter.Add(vb);
            DataStream dataStream = vb.Lock(0, 0, LockFlags.None);
            try
            {
                foreach (CustomVertex.PositionColoredTextured current5 in list)
                {
                    dataStream.Write(current5);
                }
            }
            finally
            {
                vb.Unlock();
            }
            lCntVert.Text = cntVerts.ToString("#,##0");
            int num14 = 0;
            alib.Clear();
            int num15 = 0;
            foreach (var current6 in alalci)
            {
                CI[] array2 = current6;
                for (int i = 0; i < array2.Length; i++)
                {
                    CI cI3 = array2[i];
                    if (cI3.ali.Length != 0)
                    {
                        var indexBuffer = new IndexBuffer(device, 4*cI3.ali.Length, Usage.None, Pool.Managed, false);
                        num14 += cI3.ali.Length;
                        alDeleter.Add(indexBuffer);
                        DataStream dataStream2 = indexBuffer.Lock(0, 0, LockFlags.None);
                        try
                        {
                            uint[] ali = cI3.ali;
                            for (int m = 0; m < ali.Length; m++)
                            {
                                uint value2 = ali[m];
                                dataStream2.Write(value2);
                            }
                        }
                        finally
                        {
                            indexBuffer.Unlock();
                        }
                        var rIB = new RIB();
                        rIB.ib = indexBuffer;
                        rIB.cnt = cI3.ali.Length;
                        rIB.texi = cI3.texi;
                        rIB.vifi = cI3.vifi;
                        rIB.name = aldc[num15].name;
                        rIB.dcId = aldc[num15].dcId;
                        alib.Add(rIB);
                    }
                    else
                    {
                        var rIB2 = new RIB();
                        rIB2.ib = null;
                        rIB2.cnt = 0;
                        rIB2.texi = cI3.texi;
                        rIB2.vifi = cI3.vifi;
                        rIB2.name = aldc[num15].name;
                        rIB2.dcId = aldc[num15].dcId;
                        alib.Add(rIB2);
                    }
                }
                num15++;
            }
            lCntTris.Text = (num14/3).ToString("#,##0");
            foreach (Co2 current7 in coll.alCo2)
            {
                alpf.Add(putb.Add(current7));
            }
            if (putb.alv.Count != 0)
            {
                pvi = new Putvi(putb, device);
            }
            Console.Write("");
        }

        private void tsiIfRender_CheckedChanged(object sender, EventArgs e)
        {
            var toolStripButton = (ToolStripButton) sender;
            bool @checked = toolStripButton.Checked;
            var g = (Guid) toolStripButton.Tag;
            foreach (RIB current in alib)
            {
                if (current.dcId.Equals(g))
                {
                    current.render = @checked;
                }
            }
            p1.Invalidate();
        }

        private void p1_MouseWheel(object sender, MouseEventArgs e)
        {
            fov.Value =
                (decimal)
                    Math.Max(Convert.ToSingle(fov.Minimum),
                        Math.Min(Convert.ToSingle(fov.Maximum), Math.Max(1f, Convert.ToSingle(fov.Value) + e.Delta/200f)));
            p1.Invalidate();
        }

        private void p1_Paint(object sender, PaintEventArgs e)
        {
            Size clientSize = p1.ClientSize;
            float aspect = (clientSize.Height != 0) ? (clientSize.Width/(float) clientSize.Height) : 0f;
            device.SetTransform(TransformState.World, Matrix.Identity);
            device.SetTransform(TransformState.View, Matrix.LookAtLH(CameraEye, CameraEye + Target, CameraUp));
            device.SetTransform(TransformState.Projection,
                Matrix.PerspectiveFovLH(Convert.ToSingle(fov.Value)/180f*3.14159f, aspect, Convert.ToSingle(50),
                    Convert.ToSingle(5000000)));
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, p1.BackColor, 1f, 0);
            device.BeginScene();
            device.SetTextureStageState(0, TextureStage.ColorOperation,
                cbVertexColor.Checked ? TextureOperation.Modulate : TextureOperation.SelectArg1);
            device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
            device.SetTextureStageState(0, TextureStage.ColorArg2, TextureArgument.Diffuse);
            device.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
            device.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
            device.SetTextureStageState(0, TextureStage.AlphaArg2, TextureArgument.Diffuse);
            device.SetRenderState(RenderState.FogEnable, cbFog.Checked);
            if (vb != null)
            {
                device.SetStreamSource(0, vb, 0, CustomVertex.PositionColoredTextured.Size);
                device.VertexFormat = CustomVertex.PositionColoredTextured.Format;
                foreach (RIB current in alib)
                {
                    if (current.ib != null && current.render)
                    {
                        device.SetRenderState(RenderState.FogEnable, cbFog.Checked && current.name.Equals("MAP"));
                        device.Indices = current.ib;
                        device.SetTexture(0, altex[current.texi & 65535]);
                        device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, cntVerts, 0, current.cnt/3);
                    }
                }
            }
            if (tsbBallGame.Checked)
            {
                var list = new List<CustomVertex.PositionColored>();
                foreach (Ball current2 in alBall)
                {
                    list.Add(new CustomVertex.PositionColored(current2.pos, Color.Red.ToArgb()));
                }
                if (list.Count != 0)
                {
                    device.VertexFormat = CustomVertex.PositionColored.Format;
                    device.SetRenderState(RenderState.PointScaleEnable, true);
                    device.SetRenderState(RenderState.PointSize, 10f);
                    device.SetRenderState(RenderState.PointScaleA, 0f);
                    device.SetRenderState(RenderState.PointScaleB, 0f);
                    device.SetRenderState(RenderState.PointScaleC, 1f);
                    device.DrawUserPrimitives(PrimitiveType.PointList, list.Count, list.ToArray());
                    device.SetRenderState(RenderState.PointScaleEnable, false);
                }
            }
            if (tsbShowColl.Checked)
            {
                vut.Clear();
                foreach (Co2 current3 in coll.alCo2)
                {
                    for (int i = current3.Co3frm; i < current3.Co3to; i++)
                    {
                        Co3 co = coll.alCo3[i];
                        int clr = Color.Yellow.ToArgb();
                        if (0 <= co.vi0 && 0 <= co.vi1 && 0 <= co.vi2)
                        {
                            vut.AddTri(coll.alCo4[co.vi0], coll.alCo4[co.vi2], coll.alCo4[co.vi1], clr);
                            if (0 <= co.vi3)
                            {
                                vut.AddTri(coll.alCo4[co.vi3], coll.alCo4[co.vi2], coll.alCo4[co.vi0], clr);
                            }
                        }
                    }
                }
                if (vut.alv.Count != 0)
                {
                    device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg1);
                    device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Diffuse);
                    device.SetRenderState(RenderState.AlphaBlendEnable, false);
                    device.SetRenderState(RenderState.Lighting, true);
                    device.SetRenderState(RenderState.AlphaTestEnable, false);
                    device.SetRenderState(RenderState.ShadeMode, ShadeMode.Gouraud);
                    device.SetRenderState(RenderState.NormalizeNormals, true);
                    device.EnableLight(0, true);
                    Light light = device.GetLight(0);
                    light.Direction = Target;
                    light.Diffuse = new Color4(-1);
                    device.SetLight(0, light);
                    Material material = device.Material;
                    device.Material = material;
                    Matrix value = Matrix.Scaling(-1f, -1f, -1f);
                    device.SetTransform(TransformState.World, value);
                    device.VertexFormat = CustomVertex.PositionNormalColored.Format;
                    device.DrawUserPrimitives(PrimitiveType.TriangleList, vut.alv.Count/3,
                        vut.alvtmp = vut.alv.ToArray());
                    device.SetRenderState(RenderState.AlphaBlendEnable, true);
                    device.SetRenderState(RenderState.Lighting, false);
                    device.SetRenderState(RenderState.AlphaTestEnable, true);
                }
            }
            device.EndScene();
            device.Present();
        }

        private void p1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                firstcur = p1.PointToScreen(curs = new Point(e.X, e.Y));
            }
            if (e.Button == MouseButtons.Middle)
            {
                var ball = new Ball();
                ball.pos = CameraEye + Target*100f;
                ball.velo = Target;
                alBall.Add(ball);
                tsbBallGame.Checked = true;
                tsbBallGame_Click(sender, e);
            }
        }

        private void PhysicBall()
        {
            foreach (Ball current in alBall)
            {
                Vector3 pos = current.pos;
                Vector3 vector = pos + current.velo*0.5f;
                if (TestColl(pos) == TestColl(vector))
                {
                    current.pos = vector;
                    Ball expr_51_cp_0 = current;
                    expr_51_cp_0.velo.Y = expr_51_cp_0.velo.Y - 3f;
                }
            }
        }

        private void p1_MouseMove(object sender, MouseEventArgs e)
        {
            if (curs != Point.Empty)
            {
                int num = e.X - curs.X;
                int num2 = e.Y - curs.Y;
                if (num != 0 || num2 != 0)
                {
                    if ((e.Button & MouseButtons.Left) != MouseButtons.None)
                    {
                        yaw.Value += (decimal) (num/3f);
                        roll.Value =
                            (decimal) Math.Max(-89f, Math.Min(89f, Convert.ToSingle(roll.Value)%360f - num2/3f));
                        Cursor.Position = firstcur;
                        p1.Invalidate();
                        return;
                    }
                    if ((e.Button & MouseButtons.Right) != MouseButtons.None)
                    {
                        yaw.Value += (decimal) (num/3f);
                        CameraEye += Target*-(float) num2;
                        Cursor.Position = firstcur;
                        p1.Invalidate();
                    }
                }
            }
        }

        private void p1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                curs = Point.Empty;
            }
        }

        private void Visf_Load(object sender, EventArgs e)
        {
            base.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void tsbExpBlenderpy_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory("bpyexp");
            DataStream dataStream = vb.Lock(0, 0, LockFlags.ReadOnly);
            try
            {
                var array = new CustomVertex.PositionColoredTextured[cntVerts];
                for (int i = 0; i < cntVerts; i++)
                {
                    array[i] = dataStream.Read<CustomVertex.PositionColoredTextured>();
                }
                int num = 0;
                for (int j = 0; j < alalci.Count; j++)
                {
                    var mkbpy = new Mkbpy();
                    mkbpy.StartTex();
                    string text = "bpyexp\\" + aldc[j].name;
                    Directory.CreateDirectory(text);
                    int num2 = 0;
                    DC dC = aldc[j];
                    Bitmap[] pics = dC.o7.pics;
                    for (int k = 0; k < pics.Length; k++)
                    {
                        Bitmap bitmap = pics[k];
                        string str = string.Format("t{0:000}.png", num2);
                        bitmap.Save(text + "\\" + str, ImageFormat.Png);
                        mkbpy.AddTex(Path.GetFullPath(text + "\\" + str), string.Format("Tex{0:000}", num2),
                            string.Format("Mat{0:000}", num2));
                        num2++;
                    }
                    mkbpy.EndTex();
                    CI[] array2 = alalci[j];
                    for (int l = 0; l < array2.Length; l++)
                    {
                        CI cI = array2[l];
                        mkbpy.StartMesh();
                        for (int m = 0; m < cI.ali.Length/3; m++)
                        {
                            mkbpy.AddV(array[(int) ((UIntPtr) cI.ali[m*3])]);
                            mkbpy.AddV(array[(int) ((UIntPtr) cI.ali[m*3 + 2])]);
                            mkbpy.AddV(array[(int) ((UIntPtr) cI.ali[m*3 + 1])]);
                            mkbpy.AddColorVtx(new[]
                            {
                                Color.FromArgb(array[(int) ((UIntPtr) cI.ali[m*3])].Color),
                                Color.FromArgb(array[(int) ((UIntPtr) cI.ali[m*3 + 2])].Color),
                                Color.FromArgb(array[(int) ((UIntPtr) cI.ali[m*3 + 1])].Color)
                            });
                            mkbpy.AddTuv((cI.texi & 65535) - num, array[(int) ((UIntPtr) cI.ali[m*3])].Tu,
                                1f - array[(int) ((UIntPtr) cI.ali[m*3])].Tv,
                                array[(int) ((UIntPtr) cI.ali[m*3 + 2])].Tu,
                                1f - array[(int) ((UIntPtr) cI.ali[m*3 + 2])].Tv,
                                array[(int) ((UIntPtr) cI.ali[m*3 + 1])].Tu,
                                1f - array[(int) ((UIntPtr) cI.ali[m*3 + 1])].Tv);
                        }
                        mkbpy.EndMesh(cI.vifi);
                    }
                    mkbpy.Finish();
                    File.WriteAllText(text + "\\mesh.py", mkbpy.ToString(), Encoding.ASCII);
                    num += num2;
                }
            }
            finally
            {
                vb.Unlock();
            }
            Process.Start("explorer.exe", " bpyexp");
        }

        private void eyeX_ValueChanged(object sender, EventArgs e)
        {
            p1.Invalidate();
        }

        private void p1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            if (keyCode <= Keys.A)
            {
                switch (keyCode)
                {
                    case Keys.Up:
                        kr |= Keyrun.Up;
                        break;
                    case Keys.Right:
                        break;
                    case Keys.Down:
                        kr |= Keyrun.Down;
                        break;
                    default:
                        if (keyCode == Keys.A)
                        {
                            kr |= Keyrun.A;
                        }
                        break;
                }
            }
            else
            {
                if (keyCode != Keys.D)
                {
                    if (keyCode != Keys.S)
                    {
                        if (keyCode == Keys.W)
                        {
                            kr |= Keyrun.W;
                        }
                    }
                    else
                    {
                        kr |= Keyrun.S;
                    }
                }
                else
                {
                    kr |= Keyrun.D;
                }
            }
            if (kr != Keyrun.None && !timerRun.Enabled)
            {
                timerRun.Start();
            }
        }

        private void p1_KeyUp(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            if (keyCode <= Keys.A)
            {
                switch (keyCode)
                {
                    case Keys.Up:
                        kr &= ~Keyrun.Up;
                        break;
                    case Keys.Right:
                        break;
                    case Keys.Down:
                        kr &= ~Keyrun.Down;
                        break;
                    default:
                        if (keyCode == Keys.A)
                        {
                            kr &= ~Keyrun.A;
                        }
                        break;
                }
            }
            else
            {
                if (keyCode != Keys.D)
                {
                    if (keyCode != Keys.S)
                    {
                        if (keyCode == Keys.W)
                        {
                            kr &= ~Keyrun.W;
                        }
                    }
                    else
                    {
                        kr &= ~Keyrun.S;
                    }
                }
                else
                {
                    kr &= ~Keyrun.D;
                }
            }
            if (kr == Keyrun.None && timerRun.Enabled)
            {
                timerRun.Stop();
            }
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            if ((kr & Keyrun.W) != Keyrun.None)
            {
                CameraEye += Target*Speed;
            }
            if ((kr & Keyrun.S) != Keyrun.None)
            {
                CameraEye -= Target*Speed;
            }
            if ((kr & Keyrun.A) != Keyrun.None)
            {
                CameraEye += LeftVec*Speed;
            }
            if ((kr & Keyrun.D) != Keyrun.None)
            {
                CameraEye -= LeftVec*Speed;
            }
            if ((kr & Keyrun.Up) != Keyrun.None)
            {
                CameraEye += Vector3.UnitY*Speed;
            }
            if ((kr & Keyrun.Down) != Keyrun.None)
            {
                CameraEye -= Vector3.UnitY*Speed;
            }
            p1.Invalidate();
        }

        private void p1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                    e.IsInputKey = true;
                    break;
                case Keys.Right:
                    break;
                default:
                    return;
            }
        }

        private void Visf_FormClosing(object sender, FormClosingEventArgs e)
        {
            alDeleter.Reverse();
            foreach (ComObject current in alDeleter)
            {
                current.Dispose();
            }
            if (pvi != null)
            {
                pvi.Dispose();
            }
        }

        private void cbFog_CheckedChanged(object sender, EventArgs e)
        {
            p1.Invalidate();
        }

        private bool TestColl(Vector3 v)
        {
            v.X = -v.X;
            v.Y = -v.Y;
            v.Z = -v.Z;
            bool flag = false;
            foreach (Co2 current in coll.alCo2)
            {
                if (current.Min.X <= v.X && current.Min.Y <= v.Y && current.Min.Z <= v.Z && v.X <= current.Max.X &&
                    v.Y <= current.Max.Y && v.Z <= current.Max.Z)
                {
                    int num = 0;
                    int num2 = 0;
                    for (int i = current.Co3frm; i < current.Co3to; i++)
                    {
                        Co3 co = coll.alCo3[i];
                        num += ((Plane.DotCoordinate(coll.alCo5[co.PlaneCo5], v) > 0f) ? 1 : 0);
                        num2++;
                    }
                    flag |= (num2 != 0 && num == num2);
                }
            }
            return flag;
        }

        private void tsbShowColl_Click(object sender, EventArgs e)
        {
            p1.Invalidate();
        }

        private void tsbShowGeo_Click(object sender, EventArgs e)
        {
            p1.Invalidate();
        }

        private void tsbBallGame_Click(object sender, EventArgs e)
        {
            if (!(timerBall.Enabled = tsbBallGame.Checked))
            {
                alBall.Clear();
            }
        }

        private void timerBall_Tick(object sender, EventArgs e)
        {
            PhysicBall();
            p1.Invalidate();
        }

        private void p1_SizeChanged(object sender, EventArgs e)
        {
            p1.Invalidate();
        }

        private class Ball
        {
            public Vector3 pos;
            public Vector3 velo;
        }

        private class CI
        {
            public uint[] ali;
            public int texi;
            public int vifi;
        }

        [Flags]
        private enum Keyrun
        {
            None = 0,
            W = 1,
            S = 2,
            A = 4,
            D = 8,
            Up = 16,
            Down = 32
        }

        private class LMap
        {
            public byte[] al = new byte[256];

            public LMap()
            {
                for (int i = 0; i < 256; i++)
                {
                    al[i] = (byte) Math.Min(255, 2*i);
                }
            }
        }

        private class Mkbpy
        {
            private List<int> alRefMati = new List<int>();
            private int cntv;
            private int i2;
            private Matrix mtxLoc2Blender;
            private int uvi;
            private StringWriter uvs = new StringWriter();
            private string vcoords = "";
            private StringWriter vcs = new StringWriter();
            private string vfaces = "";
            private StringWriter wr = new StringWriter();

            public Mkbpy()
            {
                wr.WriteLine(
                    "# http://f11.aaa.livedoor.jp/~hige/index.php?%5B%5BPython%A5%B9%A5%AF%A5%EA%A5%D7%A5%C8%5D%5D");
                wr.WriteLine("# http://www.blender.org/documentation/248PythonDoc/index.html");
                wr.WriteLine();
                wr.WriteLine("# good for Blender 2.4.8a");
                wr.WriteLine("# with Python 2.5.4");
                wr.WriteLine();
                wr.WriteLine("# Import instruction:");
                wr.WriteLine("# * Launch Blender 2.4.8a");
                wr.WriteLine("# * In Blender, type Shift+F11, then open then Script Window");
                wr.WriteLine("# * Type Alt+O or [Text]menu -> [Open], then select and open mesh.py");
                wr.WriteLine("# * Type Alt+P or [Text]menu -> [Run Python Script] to run the script!");
                wr.WriteLine("# * Use Ctrl+LeftArrow, Ctrl+RightArrow to change window layout.");
                wr.WriteLine();
                wr.WriteLine("print \"-- Start importing \"");
                wr.WriteLine();
                wr.WriteLine("import Blender");
                wr.WriteLine();
                wr.WriteLine("scene = Blender.Scene.GetCurrent()");
                wr.WriteLine();
                mtxLoc2Blender = Matrix.RotationX(1.570795f);
                float num = 0.01f;
                mtxLoc2Blender = Matrix.Multiply(mtxLoc2Blender, Matrix.Scaling(-num, num, num));
            }

            public void StartTex()
            {
                wr.WriteLine("imgs = []");
                wr.WriteLine("mats = []");
            }

            public void AddTex(string fp, string tid, string mid)
            {
                wr.WriteLine("img = Blender.Image.Load('{0}')", fp.Replace("\\", "/"));
                wr.WriteLine("tex = Blender.Texture.New('{0}')", tid);
                wr.WriteLine("tex.image = img");
                wr.WriteLine("mat = Blender.Material.New('{0}')", mid);
                wr.WriteLine("mat.setTexture(0, tex, Blender.Texture.TexCo.UV, Blender.Texture.MapTo.COL)");
                wr.WriteLine("mat.setMode('Shadeless')");
                wr.WriteLine("mats += [mat]");
                wr.WriteLine("imgs += [img]");
            }

            public void EndTex()
            {
            }

            public void StartMesh()
            {
                vcoords = "";
                cntv = 0;
                uvs = new StringWriter();
                uvi = 0;
                alRefMati.Clear();
                vcs = new StringWriter();
            }

            public void AddV(CustomVertex.PositionColoredTextured v)
            {
                if (vcoords != "")
                {
                    vcoords += ",";
                }
                Vector3 vector = Vector3.TransformCoordinate(v.Position, mtxLoc2Blender);
                vcoords += string.Format("[{0},{1},{2}]", vector.X, vector.Y, vector.Z);
                cntv++;
            }

            public void AddColorVtx(Color[] clrs)
            {
                for (int i = 0; i < clrs.Length; i++)
                {
                    vcs.WriteLine("me.faces[{0}].col[{1}].a = {2}", uvi, i, clrs[i].A);
                    vcs.WriteLine("me.faces[{0}].col[{1}].r = {2}", uvi, i, clrs[i].R);
                    vcs.WriteLine("me.faces[{0}].col[{1}].g = {2}", uvi, i, clrs[i].G);
                    vcs.WriteLine("me.faces[{0}].col[{1}].b = {2}", uvi, i, clrs[i].B);
                }
            }

            public void AddTuv(int texi, float tu0, float tv0, float tu1, float tv1, float tu2, float tv2)
            {
                if (alRefMati.IndexOf(texi) < 0)
                {
                    alRefMati.Add(texi);
                }
                int num = alRefMati.IndexOf(texi);
                uvs.WriteLine(
                    "me.faces[{0}].uv = [Blender.Mathutils.Vector({1:0.000},{2:0.000}),Blender.Mathutils.Vector({3:0.000},{4:0.000}),Blender.Mathutils.Vector({5:0.000},{6:0.000}),]",
                    new object[]
                    {
                        uvi,
                        tu0,
                        tv0,
                        tu1,
                        tv1,
                        tu2,
                        tv2
                    });
                uvs.WriteLine("me.faces[{0}].mat = {1}", uvi, num);
                uvs.WriteLine("me.faces[{0}].image = imgs[{1}]", uvi, texi);
                uvi++;
            }

            public void EndMesh(int vifi)
            {
                if (cntv == 0)
                {
                    return;
                }
                vfaces = "";
                for (int i = 0; i < cntv/3; i++)
                {
                    if (vfaces != "")
                    {
                        vfaces += ",";
                    }
                    vfaces += string.Format("[{0},{1},{2}]", 3*i, 3*i + 1, 3*i + 2);
                }
                string str = string.Format("vifpkt{0:0000}-mesh", vifi);
                string str2 = string.Format("vifpkt{0:0000}-obj{1}", vifi, i2);
                i2++;
                wr.WriteLine("coords = [" + vcoords + "]");
                wr.WriteLine("faces = [" + vfaces + "]");
                wr.WriteLine("me = Blender.Mesh.New('" + str + "')");
                wr.WriteLine("me.verts.extend(coords)");
                wr.WriteLine("me.faces.extend(faces)");
                wr.WriteLine("me.faceUV = True");
                for (int j = 0; j < alRefMati.Count; j++)
                {
                    wr.WriteLine("me.materials += [mats[{0}]]", alRefMati[j]);
                }
                wr.Write(uvs.ToString());
                wr.WriteLine("me.vertexColors = True");
                wr.Write(vcs.ToString());
                wr.WriteLine("ob = scene.objects.new(me, '" + str2 + "')");
                wr.WriteLine("");
            }

            public void Finish()
            {
                wr.WriteLine("print \"-- Ended importing \"");
            }

            public override string ToString()
            {
                return wr.ToString();
            }
        }

        private class RIB
        {
            public int cnt;
            public Guid dcId;
            public IndexBuffer ib;
            public string name = "";
            public bool render = true;
            public int texi;
            public int vifi;
        }

        private class VUt
        {
            public List<CustomVertex.PositionNormalColored> alv = new List<CustomVertex.PositionNormalColored>();
            public CustomVertex.PositionNormalColored[] alvtmp;

            public void AddTri(Vector4 w0, Vector4 w1, Vector4 w2, int clr)
            {
                var vector = new Vector3(w0.X, w0.Y, w0.Z);
                var vector2 = new Vector3(w1.X, w1.Y, w1.Z);
                var vector3 = new Vector3(w2.X, w2.Y, w2.Z);
                Vector3 n = Vector3.Cross(vector2 - vector, vector2 - vector3);
                alv.Add(new CustomVertex.PositionNormalColored(vector, n, clr));
                alv.Add(new CustomVertex.PositionNormalColored(vector2, n, clr));
                alv.Add(new CustomVertex.PositionNormalColored(vector3, n, clr));
            }

            public void Clear()
            {
                alv.Clear();
                alvtmp = null;
            }
        }

        private delegate void _SetPos(Vector3 v);
    }
}