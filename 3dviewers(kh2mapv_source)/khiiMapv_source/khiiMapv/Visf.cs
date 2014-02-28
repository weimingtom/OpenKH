using hex04BinTrack;
using khiiMapv.CollTest;
using khiiMapv.Put;
using SlimDX;
using SlimDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace khiiMapv
{
	public class Visf : Form
	{
		private class CI
		{
			public uint[] ali;
			public int texi;
			public int vifi;
		}
		private class LMap
		{
			public byte[] al = new byte[256];
			public LMap()
			{
				for (int i = 0; i < 256; i++)
				{
					this.al[i] = (byte)Math.Min(255, 2 * i);
				}
			}
		}
		private class RIB
		{
			public IndexBuffer ib;
			public int cnt;
			public int texi;
			public int vifi;
			public bool render = true;
			public string name = "";
			public Guid dcId;
		}
		private class VUt
		{
			public List<CustomVertex.PositionNormalColored> alv = new List<CustomVertex.PositionNormalColored>();
			public CustomVertex.PositionNormalColored[] alvtmp;
			public void AddTri(Vector4 w0, Vector4 w1, Vector4 w2, int clr)
			{
				Vector3 vector = new Vector3(w0.X, w0.Y, w0.Z);
				Vector3 vector2 = new Vector3(w1.X, w1.Y, w1.Z);
				Vector3 vector3 = new Vector3(w2.X, w2.Y, w2.Z);
				Vector3 n = Vector3.Cross(vector2 - vector, vector2 - vector3);
				this.alv.Add(new CustomVertex.PositionNormalColored(vector, n, clr));
				this.alv.Add(new CustomVertex.PositionNormalColored(vector2, n, clr));
				this.alv.Add(new CustomVertex.PositionNormalColored(vector3, n, clr));
			}
			public void Clear()
			{
				this.alv.Clear();
				this.alvtmp = null;
			}
		}
		private class Ball
		{
			public Vector3 pos;
			public Vector3 velo;
		}
		private class Mkbpy
		{
			private int i;
			private int cntv;
			private StringWriter wr = new StringWriter();
			private string vcoords = "";
			private string vfaces = "";
			private StringWriter uvs = new StringWriter();
			private int uvi;
			private List<int> alRefMati = new List<int>();
			private StringWriter vcs = new StringWriter();
			private Matrix mtxLoc2Blender;
			public Mkbpy()
			{
				this.wr.WriteLine("# http://f11.aaa.livedoor.jp/~hige/index.php?%5B%5BPython%A5%B9%A5%AF%A5%EA%A5%D7%A5%C8%5D%5D");
				this.wr.WriteLine("# http://www.blender.org/documentation/248PythonDoc/index.html");
				this.wr.WriteLine();
				this.wr.WriteLine("# good for Blender 2.4.8a");
				this.wr.WriteLine("# with Python 2.5.4");
				this.wr.WriteLine();
				this.wr.WriteLine("# Import instruction:");
				this.wr.WriteLine("# * Launch Blender 2.4.8a");
				this.wr.WriteLine("# * In Blender, type Shift+F11, then open then Script Window");
				this.wr.WriteLine("# * Type Alt+O or [Text]menu -> [Open], then select and open mesh.py");
				this.wr.WriteLine("# * Type Alt+P or [Text]menu -> [Run Python Script] to run the script!");
				this.wr.WriteLine("# * Use Ctrl+LeftArrow, Ctrl+RightArrow to change window layout.");
				this.wr.WriteLine();
				this.wr.WriteLine("print \"-- Start importing \"");
				this.wr.WriteLine();
				this.wr.WriteLine("import Blender");
				this.wr.WriteLine();
				this.wr.WriteLine("scene = Blender.Scene.GetCurrent()");
				this.wr.WriteLine();
				this.mtxLoc2Blender = Matrix.RotationX(1.570795f);
				float num = 0.01f;
				this.mtxLoc2Blender = Matrix.Multiply(this.mtxLoc2Blender, Matrix.Scaling(-num, num, num));
			}
			public void StartTex()
			{
				this.wr.WriteLine("imgs = []");
				this.wr.WriteLine("mats = []");
			}
			public void AddTex(string fp, string tid, string mid)
			{
				this.wr.WriteLine("img = Blender.Image.Load('{0}')", fp.Replace("\\", "/"));
				this.wr.WriteLine("tex = Blender.Texture.New('{0}')", tid);
				this.wr.WriteLine("tex.image = img");
				this.wr.WriteLine("mat = Blender.Material.New('{0}')", mid);
				this.wr.WriteLine("mat.setTexture(0, tex, Blender.Texture.TexCo.UV, Blender.Texture.MapTo.COL)");
				this.wr.WriteLine("mat.setMode('Shadeless')");
				this.wr.WriteLine("mats += [mat]");
				this.wr.WriteLine("imgs += [img]");
			}
			public void EndTex()
			{
			}
			public void StartMesh()
			{
				this.vcoords = "";
				this.cntv = 0;
				this.uvs = new StringWriter();
				this.uvi = 0;
				this.alRefMati.Clear();
				this.vcs = new StringWriter();
			}
			public void AddV(CustomVertex.PositionColoredTextured v)
			{
				if (this.vcoords != "")
				{
					this.vcoords += ",";
				}
				Vector3 vector = Vector3.TransformCoordinate(v.Position, this.mtxLoc2Blender);
				this.vcoords += string.Format("[{0},{1},{2}]", vector.X, vector.Y, vector.Z);
				this.cntv++;
			}
			public void AddColorVtx(Color[] clrs)
			{
				for (int i = 0; i < clrs.Length; i++)
				{
					this.vcs.WriteLine("me.faces[{0}].col[{1}].a = {2}", this.uvi, i, clrs[i].A);
					this.vcs.WriteLine("me.faces[{0}].col[{1}].r = {2}", this.uvi, i, clrs[i].R);
					this.vcs.WriteLine("me.faces[{0}].col[{1}].g = {2}", this.uvi, i, clrs[i].G);
					this.vcs.WriteLine("me.faces[{0}].col[{1}].b = {2}", this.uvi, i, clrs[i].B);
				}
			}
			public void AddTuv(int texi, float tu0, float tv0, float tu1, float tv1, float tu2, float tv2)
			{
				if (this.alRefMati.IndexOf(texi) < 0)
				{
					this.alRefMati.Add(texi);
				}
				int num = this.alRefMati.IndexOf(texi);
				this.uvs.WriteLine("me.faces[{0}].uv = [Blender.Mathutils.Vector({1:0.000},{2:0.000}),Blender.Mathutils.Vector({3:0.000},{4:0.000}),Blender.Mathutils.Vector({5:0.000},{6:0.000}),]", new object[]
				{
					this.uvi,
					tu0,
					tv0,
					tu1,
					tv1,
					tu2,
					tv2
				});
				this.uvs.WriteLine("me.faces[{0}].mat = {1}", this.uvi, num);
				this.uvs.WriteLine("me.faces[{0}].image = imgs[{1}]", this.uvi, texi);
				this.uvi++;
			}
			public void EndMesh(int vifi)
			{
				if (this.cntv == 0)
				{
					return;
				}
				this.vfaces = "";
				for (int i = 0; i < this.cntv / 3; i++)
				{
					if (this.vfaces != "")
					{
						this.vfaces += ",";
					}
					this.vfaces += string.Format("[{0},{1},{2}]", 3 * i, 3 * i + 1, 3 * i + 2);
				}
				string str = string.Format("vifpkt{0:0000}-mesh", vifi);
				string str2 = string.Format("vifpkt{0:0000}-obj{1}", vifi, this.i);
				this.i++;
				this.wr.WriteLine("coords = [" + this.vcoords + "]");
				this.wr.WriteLine("faces = [" + this.vfaces + "]");
				this.wr.WriteLine("me = Blender.Mesh.New('" + str + "')");
				this.wr.WriteLine("me.verts.extend(coords)");
				this.wr.WriteLine("me.faces.extend(faces)");
				this.wr.WriteLine("me.faceUV = True");
				for (int j = 0; j < this.alRefMati.Count; j++)
				{
					this.wr.WriteLine("me.materials += [mats[{0}]]", this.alRefMati[j]);
				}
				this.wr.Write(this.uvs.ToString());
				this.wr.WriteLine("me.vertexColors = True");
				this.wr.Write(this.vcs.ToString());
				this.wr.WriteLine("ob = scene.objects.new(me, '" + str2 + "')");
				this.wr.WriteLine("");
			}
			public void Finish()
			{
				this.wr.WriteLine("print \"-- Ended importing \"");
			}
			public override string ToString()
			{
				return this.wr.ToString();
			}
		}
		private delegate void _SetPos(Vector3 v);
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
		private List<DC> aldc;
		private CollReader coll;
		private Device device;
		private List<Visf.CI> alci = new List<Visf.CI>();
		private List<Visf.CI[]> alalci = new List<Visf.CI[]>();
		private List<Texture> altex = new List<Texture>();
		private Direct3D p3D;
		private Visf.LMap lm = new Visf.LMap();
		private Putbox putb = new Putbox();
		private Putvi pvi;
		private List<Putfragment> alpf = new List<Putfragment>();
		private List<ComObject> alDeleter = new List<ComObject>();
		private List<Visf.RIB> alib = new List<Visf.RIB>();
		private VertexBuffer vb;
		private int cntVerts;
		private Visf.VUt vut = new Visf.VUt();
		private Point curs = Point.Empty;
		private Point firstcur = Point.Empty;
		private List<Visf.Ball> alBall = new List<Visf.Ball>();
		private Visf.Keyrun kr;
		private IContainer components;
		private UC p1;
		private Label label1;
		private ToolStrip toolStrip1;
		private ToolStripButton tsbExpBlenderpy;
		private FlowLayoutPanel flppos;
		private Label label2;
		private NumericUpDown eyeX;
		private NumericUpDown eyeY;
		private NumericUpDown eyeZ;
		private Label label3;
		private NumericUpDown fov;
		private Label label4;
		private NumericUpDown yaw;
		private NumericUpDown pitch;
		private NumericUpDown roll;
		private Timer timerRun;
		private Label label5;
		private Label label6;
		private CheckBox cbFog;
		private CheckBox cbVertexColor;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripLabel lCntVert;
		private ToolStripLabel toolStripLabel1;
		private ToolStripLabel lCntTris;
		private ToolStripLabel toolStripLabel2;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripButton tsbShowColl;
		private ToolStripLabel tslMdls;
		private ToolStripButton tsbBallGame;
		private Timer timerBall;
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
				return new Vector3(Convert.ToSingle(this.eyeX.Value), Convert.ToSingle(this.eyeY.Value), Convert.ToSingle(this.eyeZ.Value));
			}
			set
			{
				this.eyeX.Value = Math.Max(this.eyeX.Minimum, Math.Min(this.eyeX.Maximum, (decimal)value.X));
				this.eyeY.Value = Math.Max(this.eyeY.Minimum, Math.Min(this.eyeY.Maximum, (decimal)value.Y));
				this.eyeZ.Value = Math.Max(this.eyeZ.Minimum, Math.Min(this.eyeZ.Maximum, (decimal)value.Z));
			}
		}
		private Vector3 Target
		{
			get
			{
				return Vector3.TransformCoordinate(Vector3.UnitX, Matrix.RotationYawPitchRoll(Convert.ToSingle(this.yaw.Value) / 180f * 3.14159f, Convert.ToSingle(this.pitch.Value) / 180f * 3.14159f, Convert.ToSingle(this.roll.Value) / 180f * 3.14159f));
			}
		}
		private Vector3 CameraUp
		{
			get
			{
				return Vector3.TransformCoordinate(Vector3.UnitY, Matrix.RotationYawPitchRoll(0f, Convert.ToSingle(this.pitch.Value) / 180f * 3.14159f, 0f));
			}
		}
		private Vector3 TargetX
		{
			get
			{
				return Vector3.TransformCoordinate(Vector3.UnitX, Matrix.RotationYawPitchRoll(Convert.ToSingle(this.yaw.Value) / 180f * 3.14159f, 0f, 0f));
			}
		}
		private Vector3 LeftVec
		{
			get
			{
				return Vector3.TransformCoordinate(Vector3.TransformCoordinate(this.TargetX, Matrix.RotationY(-1.570795f)), Matrix.RotationYawPitchRoll(0f, Convert.ToSingle(this.pitch.Value) / 180f * 3.14159f, 0f));
			}
		}
		private int Speed
		{
			get
			{
				if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
				{
					return 30;
				}
				return 60;
			}
		}
		public Visf()
		{
			this.InitializeComponent();
		}
		public Visf(List<DC> aldc, CollReader coll)
		{
			this.aldc = aldc;
			this.coll = coll;
			this.InitializeComponent();
		}
		private void p1_Load(object sender, EventArgs e)
		{
			this.p3D = new Direct3D();
			this.alDeleter.Add(this.p3D);
			this.device = new Device(this.p3D, 0, DeviceType.Hardware, this.p1.Handle, CreateFlags.HardwareVertexProcessing, new PresentParameters[]
			{
				this.PP
			});
			this.alDeleter.Add(this.device);
			this.device.SetRenderState(RenderState.Lighting, false);
			this.device.SetRenderState(RenderState.ZEnable, true);
			this.device.SetRenderState(RenderState.AlphaTestEnable, true);
			this.device.SetRenderState(RenderState.AlphaRef, 2);
			this.device.SetRenderState<Compare>(RenderState.AlphaFunc, Compare.GreaterEqual);
			this.device.SetRenderState(RenderState.AlphaBlendEnable, true);
			this.device.SetRenderState<Blend>(RenderState.SourceBlend, Blend.SourceAlpha);
			this.device.SetRenderState<Blend>(RenderState.SourceBlendAlpha, Blend.SourceAlpha);
			this.device.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			this.device.SetRenderState<Blend>(RenderState.DestinationBlendAlpha, Blend.InverseSourceAlpha);
			this.device.SetRenderState<Cull>(RenderState.CullMode, Cull.Counterclockwise);
			this.device.SetRenderState(RenderState.FogColor, this.p1.BackColor.ToArgb());
			this.device.SetRenderState(RenderState.FogStart, 5f);
			this.device.SetRenderState(RenderState.FogEnd, 30000f);
			this.device.SetRenderState(RenderState.FogDensity, 0.0001f);
			this.device.SetRenderState<FogMode>(RenderState.FogVertexMode, FogMode.Exponential);
			this.p1.MouseWheel += new MouseEventHandler(this.p1_MouseWheel);
			List<CustomVertex.PositionColoredTextured> list = new List<CustomVertex.PositionColoredTextured>();
			foreach (DC current in this.aldc)
			{
				ToolStripButton toolStripButton = new ToolStripButton("Show " + current.name);
				toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
				toolStripButton.CheckOnClick = true;
				toolStripButton.Tag = current.dcId;
				toolStripButton.Checked = true;
				toolStripButton.CheckedChanged += new EventHandler(this.tsiIfRender_CheckedChanged);
				this.toolStrip1.Items.Insert(this.toolStrip1.Items.IndexOf(this.tsbShowColl), toolStripButton);
			}
			this.alci.Clear();
			this.altex.Clear();
			int[] array = new int[4];
			int num = 0;
			foreach (DC current2 in this.aldc)
			{
				int count = this.altex.Count;
				Bitmap[] pics = current2.o7.pics;
				for (int i = 0; i < pics.Length; i++)
				{
					Bitmap bitmap = pics[i];
					MemoryStream memoryStream = new MemoryStream();
					bitmap.Save(memoryStream, ImageFormat.Png);
					memoryStream.Position = 0L;
					Texture item;
					this.altex.Add(item = Texture.FromStream(this.device, memoryStream));
					this.alDeleter.Add(item);
				}
				if (current2.o4Mdlx != null)
				{
					using (SortedDictionary<int, Parse4Mdlx.Model>.Enumerator enumerator3 = current2.o4Mdlx.dictModel.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							KeyValuePair<int, Parse4Mdlx.Model> current3 = enumerator3.Current;
							Visf.CI cI = new Visf.CI();
							int count2 = list.Count;
							Parse4Mdlx.Model value = current3.Value;
							list.AddRange(value.alv);
							List<uint> list2 = new List<uint>();
							for (int j = 0; j < value.alv.Count; j++)
							{
								list2.Add((uint)(count2 + j));
							}
							cI.ali = list2.ToArray();
							cI.texi = count + current3.Key;
							cI.vifi = 0;
							this.alci.Add(cI);
						}
						goto IL_75D;
					}
					goto IL_3C9;
				}
				goto IL_3C9;
				IL_75D:
				this.alalci.Add(this.alci.ToArray());
				this.alci.Clear();
				continue;
				IL_3C9:
				if (current2.o4Map != null)
				{
					for (int k = 0; k < current2.o4Map.alvifpkt.Count; k++)
					{
						Vifpli vifpli = current2.o4Map.alvifpkt[k];
						byte[] vifpkt = vifpli.vifpkt;
						VU1Mem vu = new VU1Mem();
						ParseVIF1 parseVIF = new ParseVIF1(vu);
						parseVIF.Parse(new MemoryStream(vifpkt, false), 0);
						foreach (byte[] current4 in parseVIF.almsmem)
						{
							Visf.CI cI2 = new Visf.CI();
							MemoryStream memoryStream2 = new MemoryStream(current4, false);
							BinaryReader binaryReader = new BinaryReader(memoryStream2);
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
							List<uint> list3 = new List<uint>();
							int count3 = list.Count;
							for (int l = 0; l < num2; l++)
							{
								memoryStream2.Position = (long)(16 * (num3 + l));
								int num6 = (int)binaryReader.ReadInt16();
								binaryReader.ReadInt16();
								int num7 = (int)binaryReader.ReadInt16();
								binaryReader.ReadInt16();
								int num8 = (int)binaryReader.ReadInt16();
								binaryReader.ReadInt16();
								int num9 = (int)binaryReader.ReadInt16();
								binaryReader.ReadInt16();
								memoryStream2.Position = (long)(16 * (num5 + num8));
								Vector3 v;
								v.X = -binaryReader.ReadSingle();
								v.Y = binaryReader.ReadSingle();
								v.Z = binaryReader.ReadSingle();
								memoryStream2.Position = (long)(16 * (num4 + l));
								int num10 = (int)((byte)binaryReader.ReadUInt32());
								int num11 = (int)((byte)binaryReader.ReadUInt32());
								int num12 = (int)((byte)binaryReader.ReadUInt32());
								int num13 = (int)((byte)binaryReader.ReadUInt32());
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
								Color color = Color.FromArgb((int)this.lm.al[num13], (int)this.lm.al[num10], (int)this.lm.al[num11], (int)this.lm.al[num12]);
								CustomVertex.PositionColoredTextured item2 = new CustomVertex.PositionColoredTextured(v, color.ToArgb(), (float)num6 / 16f / 256f, (float)num7 / 16f / 256f);
								list.Add(item2);
							}
							cI2.ali = list3.ToArray();
							cI2.texi = count + vifpli.texi;
							cI2.vifi = k;
							this.alci.Add(cI2);
						}
					}
					goto IL_75D;
				}
				goto IL_75D;
			}
			if (this.alalci.Count != 0)
			{
				this.alci.Clear();
				this.alci.AddRange(this.alalci[0]);
			}
			if (list.Count == 0)
			{
				list.Add(default(CustomVertex.PositionColoredTextured));
			}
			this.vb = new VertexBuffer(this.device, (this.cntVerts = list.Count) * CustomVertex.PositionColoredTextured.Size, Usage.Points, CustomVertex.PositionColoredTextured.Format, Pool.Managed);
			this.alDeleter.Add(this.vb);
			DataStream dataStream = this.vb.Lock(0, 0, LockFlags.None);
			try
			{
				foreach (CustomVertex.PositionColoredTextured current5 in list)
				{
					dataStream.Write<CustomVertex.PositionColoredTextured>(current5);
				}
			}
			finally
			{
				this.vb.Unlock();
			}
			this.lCntVert.Text = this.cntVerts.ToString("#,##0");
			int num14 = 0;
			this.alib.Clear();
			int num15 = 0;
			foreach (Visf.CI[] current6 in this.alalci)
			{
				Visf.CI[] array2 = current6;
				for (int i = 0; i < array2.Length; i++)
				{
					Visf.CI cI3 = array2[i];
					if (cI3.ali.Length != 0)
					{
						IndexBuffer indexBuffer = new IndexBuffer(this.device, 4 * cI3.ali.Length, Usage.None, Pool.Managed, false);
						num14 += cI3.ali.Length;
						this.alDeleter.Add(indexBuffer);
						DataStream dataStream2 = indexBuffer.Lock(0, 0, LockFlags.None);
						try
						{
							uint[] ali = cI3.ali;
							for (int m = 0; m < ali.Length; m++)
							{
								uint value2 = ali[m];
								dataStream2.Write<uint>(value2);
							}
						}
						finally
						{
							indexBuffer.Unlock();
						}
						Visf.RIB rIB = new Visf.RIB();
						rIB.ib = indexBuffer;
						rIB.cnt = cI3.ali.Length;
						rIB.texi = cI3.texi;
						rIB.vifi = cI3.vifi;
						rIB.name = this.aldc[num15].name;
						rIB.dcId = this.aldc[num15].dcId;
						this.alib.Add(rIB);
					}
					else
					{
						Visf.RIB rIB2 = new Visf.RIB();
						rIB2.ib = null;
						rIB2.cnt = 0;
						rIB2.texi = cI3.texi;
						rIB2.vifi = cI3.vifi;
						rIB2.name = this.aldc[num15].name;
						rIB2.dcId = this.aldc[num15].dcId;
						this.alib.Add(rIB2);
					}
				}
				num15++;
			}
			this.lCntTris.Text = (num14 / 3).ToString("#,##0");
			foreach (Co2 current7 in this.coll.alCo2)
			{
				this.alpf.Add(this.putb.Add(current7));
			}
			if (this.putb.alv.Count != 0)
			{
				this.pvi = new Putvi(this.putb, this.device);
			}
			Console.Write("");
		}
		private void tsiIfRender_CheckedChanged(object sender, EventArgs e)
		{
			ToolStripButton toolStripButton = (ToolStripButton)sender;
			bool @checked = toolStripButton.Checked;
			Guid g = (Guid)toolStripButton.Tag;
			foreach (Visf.RIB current in this.alib)
			{
				if (current.dcId.Equals(g))
				{
					current.render = @checked;
				}
			}
			this.p1.Invalidate();
		}
		private void p1_MouseWheel(object sender, MouseEventArgs e)
		{
			this.fov.Value = (decimal)Math.Max(Convert.ToSingle(this.fov.Minimum), Math.Min(Convert.ToSingle(this.fov.Maximum), Math.Max(1f, Convert.ToSingle(this.fov.Value) + (float)e.Delta / 200f)));
			this.p1.Invalidate();
		}
		private void p1_Paint(object sender, PaintEventArgs e)
		{
			Size clientSize = this.p1.ClientSize;
			float aspect = (clientSize.Height != 0) ? ((float)clientSize.Width / (float)clientSize.Height) : 0f;
			this.device.SetTransform(TransformState.World, Matrix.Identity);
			this.device.SetTransform(TransformState.View, Matrix.LookAtLH(this.CameraEye, this.CameraEye + this.Target, this.CameraUp));
			this.device.SetTransform(TransformState.Projection, Matrix.PerspectiveFovLH(Convert.ToSingle(this.fov.Value) / 180f * 3.14159f, aspect, Convert.ToSingle(50), Convert.ToSingle(5000000)));
			this.device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, this.p1.BackColor, 1f, 0);
			this.device.BeginScene();
			this.device.SetTextureStageState(0, TextureStage.ColorOperation, this.cbVertexColor.Checked ? TextureOperation.Modulate : TextureOperation.SelectArg1);
			this.device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
			this.device.SetTextureStageState(0, TextureStage.ColorArg2, TextureArgument.Diffuse);
			this.device.SetTextureStageState(0, TextureStage.AlphaOperation, TextureOperation.Modulate);
			this.device.SetTextureStageState(0, TextureStage.AlphaArg1, TextureArgument.Texture);
			this.device.SetTextureStageState(0, TextureStage.AlphaArg2, TextureArgument.Diffuse);
			this.device.SetRenderState(RenderState.FogEnable, this.cbFog.Checked);
			if (this.vb != null)
			{
				this.device.SetStreamSource(0, this.vb, 0, CustomVertex.PositionColoredTextured.Size);
				this.device.VertexFormat = CustomVertex.PositionColoredTextured.Format;
				foreach (Visf.RIB current in this.alib)
				{
					if (current.ib != null && current.render)
					{
						this.device.SetRenderState(RenderState.FogEnable, this.cbFog.Checked && current.name.Equals("MAP"));
						this.device.Indices = current.ib;
						this.device.SetTexture(0, this.altex[current.texi & 65535]);
						this.device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, this.cntVerts, 0, current.cnt / 3);
					}
				}
			}
			if (this.tsbBallGame.Checked)
			{
				List<CustomVertex.PositionColored> list = new List<CustomVertex.PositionColored>();
				foreach (Visf.Ball current2 in this.alBall)
				{
					list.Add(new CustomVertex.PositionColored(current2.pos, Color.Red.ToArgb()));
				}
				if (list.Count != 0)
				{
					this.device.VertexFormat = CustomVertex.PositionColored.Format;
					this.device.SetRenderState(RenderState.PointScaleEnable, true);
					this.device.SetRenderState(RenderState.PointSize, 10f);
					this.device.SetRenderState(RenderState.PointScaleA, 0f);
					this.device.SetRenderState(RenderState.PointScaleB, 0f);
					this.device.SetRenderState(RenderState.PointScaleC, 1f);
					this.device.DrawUserPrimitives<CustomVertex.PositionColored>(PrimitiveType.PointList, list.Count, list.ToArray());
					this.device.SetRenderState(RenderState.PointScaleEnable, false);
				}
			}
			if (this.tsbShowColl.Checked)
			{
				this.vut.Clear();
				foreach (Co2 current3 in this.coll.alCo2)
				{
					for (int i = current3.Co3frm; i < current3.Co3to; i++)
					{
						Co3 co = this.coll.alCo3[i];
						int clr = Color.Yellow.ToArgb();
						if (0 <= co.vi0 && 0 <= co.vi1 && 0 <= co.vi2)
						{
							this.vut.AddTri(this.coll.alCo4[co.vi0], this.coll.alCo4[co.vi2], this.coll.alCo4[co.vi1], clr);
							if (0 <= co.vi3)
							{
								this.vut.AddTri(this.coll.alCo4[co.vi3], this.coll.alCo4[co.vi2], this.coll.alCo4[co.vi0], clr);
							}
						}
					}
				}
				if (this.vut.alv.Count != 0)
				{
					this.device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg1);
					this.device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Diffuse);
					this.device.SetRenderState(RenderState.AlphaBlendEnable, false);
					this.device.SetRenderState(RenderState.Lighting, true);
					this.device.SetRenderState(RenderState.AlphaTestEnable, false);
					this.device.SetRenderState<ShadeMode>(RenderState.ShadeMode, ShadeMode.Gouraud);
					this.device.SetRenderState(RenderState.NormalizeNormals, true);
					this.device.EnableLight(0, true);
					Light light = this.device.GetLight(0);
					light.Direction = this.Target;
					light.Diffuse = new Color4(-1);
					this.device.SetLight(0, light);
					Material material = this.device.Material;
					this.device.Material = material;
					Matrix value = Matrix.Scaling(-1f, -1f, -1f);
					this.device.SetTransform(TransformState.World, value);
					this.device.VertexFormat = CustomVertex.PositionNormalColored.Format;
					this.device.DrawUserPrimitives<CustomVertex.PositionNormalColored>(PrimitiveType.TriangleList, this.vut.alv.Count / 3, this.vut.alvtmp = this.vut.alv.ToArray());
					this.device.SetRenderState(RenderState.AlphaBlendEnable, true);
					this.device.SetRenderState(RenderState.Lighting, false);
					this.device.SetRenderState(RenderState.AlphaTestEnable, true);
				}
			}
			this.device.EndScene();
			this.device.Present();
		}
		private void p1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
			{
				this.firstcur = this.p1.PointToScreen(this.curs = new Point(e.X, e.Y));
			}
			if (e.Button == MouseButtons.Middle)
			{
				Visf.Ball ball = new Visf.Ball();
				ball.pos = this.CameraEye + this.Target * 100f;
				ball.velo = this.Target;
				this.alBall.Add(ball);
				this.tsbBallGame.Checked = true;
				this.tsbBallGame_Click(sender, e);
			}
		}
		private void PhysicBall()
		{
			foreach (Visf.Ball current in this.alBall)
			{
				Vector3 pos = current.pos;
				Vector3 vector = pos + current.velo * 0.5f;
				if (this.TestColl(pos) == this.TestColl(vector))
				{
					current.pos = vector;
					Visf.Ball expr_51_cp_0 = current;
					expr_51_cp_0.velo.Y = expr_51_cp_0.velo.Y - 3f;
				}
			}
		}
		private void p1_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.curs != Point.Empty)
			{
				int num = e.X - this.curs.X;
				int num2 = e.Y - this.curs.Y;
				if (num != 0 || num2 != 0)
				{
					if ((e.Button & MouseButtons.Left) != MouseButtons.None)
					{
						this.yaw.Value += (decimal)((float)num / 3f);
						this.roll.Value = (decimal)Math.Max(-89f, Math.Min(89f, Convert.ToSingle(this.roll.Value) % 360f - (float)num2 / 3f));
						Cursor.Position = this.firstcur;
						this.p1.Invalidate();
						return;
					}
					if ((e.Button & MouseButtons.Right) != MouseButtons.None)
					{
						this.yaw.Value += (decimal)((float)num / 3f);
						this.CameraEye += this.Target * (float)(-(float)num2);
						Cursor.Position = this.firstcur;
						this.p1.Invalidate();
					}
				}
			}
		}
		private void p1_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.curs = Point.Empty;
			}
		}
		private void Visf_Load(object sender, EventArgs e)
		{
			base.SetStyle(ControlStyles.ResizeRedraw, true);
		}
		private void tsbExpBlenderpy_Click(object sender, EventArgs e)
		{
			Directory.CreateDirectory("bpyexp");
			DataStream dataStream = this.vb.Lock(0, 0, LockFlags.ReadOnly);
			try
			{
				CustomVertex.PositionColoredTextured[] array = new CustomVertex.PositionColoredTextured[this.cntVerts];
				for (int i = 0; i < this.cntVerts; i++)
				{
					array[i] = dataStream.Read<CustomVertex.PositionColoredTextured>();
				}
				int num = 0;
				for (int j = 0; j < this.alalci.Count; j++)
				{
					Visf.Mkbpy mkbpy = new Visf.Mkbpy();
					mkbpy.StartTex();
					string text = "bpyexp\\" + this.aldc[j].name;
					Directory.CreateDirectory(text);
					int num2 = 0;
					DC dC = this.aldc[j];
					Bitmap[] pics = dC.o7.pics;
					for (int k = 0; k < pics.Length; k++)
					{
						Bitmap bitmap = pics[k];
						string str = string.Format("t{0:000}.png", num2);
						bitmap.Save(text + "\\" + str, ImageFormat.Png);
						mkbpy.AddTex(Path.GetFullPath(text + "\\" + str), string.Format("Tex{0:000}", num2), string.Format("Mat{0:000}", num2));
						num2++;
					}
					mkbpy.EndTex();
					Visf.CI[] array2 = this.alalci[j];
					for (int l = 0; l < array2.Length; l++)
					{
						Visf.CI cI = array2[l];
						mkbpy.StartMesh();
						for (int m = 0; m < cI.ali.Length / 3; m++)
						{
							mkbpy.AddV(array[(int)((UIntPtr)cI.ali[m * 3])]);
							mkbpy.AddV(array[(int)((UIntPtr)cI.ali[m * 3 + 2])]);
							mkbpy.AddV(array[(int)((UIntPtr)cI.ali[m * 3 + 1])]);
							mkbpy.AddColorVtx(new Color[]
							{
								Color.FromArgb(array[(int)((UIntPtr)cI.ali[m * 3])].Color),
								Color.FromArgb(array[(int)((UIntPtr)cI.ali[m * 3 + 2])].Color),
								Color.FromArgb(array[(int)((UIntPtr)cI.ali[m * 3 + 1])].Color)
							});
							mkbpy.AddTuv((cI.texi & 65535) - num, array[(int)((UIntPtr)cI.ali[m * 3])].Tu, 1f - array[(int)((UIntPtr)cI.ali[m * 3])].Tv, array[(int)((UIntPtr)cI.ali[m * 3 + 2])].Tu, 1f - array[(int)((UIntPtr)cI.ali[m * 3 + 2])].Tv, array[(int)((UIntPtr)cI.ali[m * 3 + 1])].Tu, 1f - array[(int)((UIntPtr)cI.ali[m * 3 + 1])].Tv);
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
				this.vb.Unlock();
			}
			Process.Start("explorer.exe", " bpyexp");
		}
		private void eyeX_ValueChanged(object sender, EventArgs e)
		{
			this.p1.Invalidate();
		}
		private void p1_KeyDown(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			if (keyCode <= Keys.A)
			{
				switch (keyCode)
				{
				case Keys.Up:
					this.kr |= Visf.Keyrun.Up;
					break;
				case Keys.Right:
					break;
				case Keys.Down:
					this.kr |= Visf.Keyrun.Down;
					break;
				default:
					if (keyCode == Keys.A)
					{
						this.kr |= Visf.Keyrun.A;
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
							this.kr |= Visf.Keyrun.W;
						}
					}
					else
					{
						this.kr |= Visf.Keyrun.S;
					}
				}
				else
				{
					this.kr |= Visf.Keyrun.D;
				}
			}
			if (this.kr != Visf.Keyrun.None && !this.timerRun.Enabled)
			{
				this.timerRun.Start();
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
					this.kr &= ~Visf.Keyrun.Up;
					break;
				case Keys.Right:
					break;
				case Keys.Down:
					this.kr &= ~Visf.Keyrun.Down;
					break;
				default:
					if (keyCode == Keys.A)
					{
						this.kr &= ~Visf.Keyrun.A;
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
							this.kr &= ~Visf.Keyrun.W;
						}
					}
					else
					{
						this.kr &= ~Visf.Keyrun.S;
					}
				}
				else
				{
					this.kr &= ~Visf.Keyrun.D;
				}
			}
			if (this.kr == Visf.Keyrun.None && this.timerRun.Enabled)
			{
				this.timerRun.Stop();
			}
		}
		private void timerRun_Tick(object sender, EventArgs e)
		{
			if ((this.kr & Visf.Keyrun.W) != Visf.Keyrun.None)
			{
				this.CameraEye += this.Target * (float)this.Speed;
			}
			if ((this.kr & Visf.Keyrun.S) != Visf.Keyrun.None)
			{
				this.CameraEye -= this.Target * (float)this.Speed;
			}
			if ((this.kr & Visf.Keyrun.A) != Visf.Keyrun.None)
			{
				this.CameraEye += this.LeftVec * (float)this.Speed;
			}
			if ((this.kr & Visf.Keyrun.D) != Visf.Keyrun.None)
			{
				this.CameraEye -= this.LeftVec * (float)this.Speed;
			}
			if ((this.kr & Visf.Keyrun.Up) != Visf.Keyrun.None)
			{
				this.CameraEye += Vector3.UnitY * (float)this.Speed;
			}
			if ((this.kr & Visf.Keyrun.Down) != Visf.Keyrun.None)
			{
				this.CameraEye -= Vector3.UnitY * (float)this.Speed;
			}
			this.p1.Invalidate();
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
			this.alDeleter.Reverse();
			foreach (ComObject current in this.alDeleter)
			{
				current.Dispose();
			}
			if (this.pvi != null)
			{
				this.pvi.Dispose();
			}
		}
		private void cbFog_CheckedChanged(object sender, EventArgs e)
		{
			this.p1.Invalidate();
		}
		private bool TestColl(Vector3 v)
		{
			v.X = -v.X;
			v.Y = -v.Y;
			v.Z = -v.Z;
			bool flag = false;
			foreach (Co2 current in this.coll.alCo2)
			{
				if (current.Min.X <= v.X && current.Min.Y <= v.Y && current.Min.Z <= v.Z && v.X <= current.Max.X && v.Y <= current.Max.Y && v.Z <= current.Max.Z)
				{
					int num = 0;
					int num2 = 0;
					for (int i = current.Co3frm; i < current.Co3to; i++)
					{
						Co3 co = this.coll.alCo3[i];
						num += ((Plane.DotCoordinate(this.coll.alCo5[co.PlaneCo5], v) > 0f) ? 1 : 0);
						num2++;
					}
					flag |= (num2 != 0 && num == num2);
				}
			}
			return flag;
		}
		private void tsbShowColl_Click(object sender, EventArgs e)
		{
			this.p1.Invalidate();
		}
		private void tsbShowGeo_Click(object sender, EventArgs e)
		{
			this.p1.Invalidate();
		}
		private void tsbBallGame_Click(object sender, EventArgs e)
		{
			if (!(this.timerBall.Enabled = this.tsbBallGame.Checked))
			{
				this.alBall.Clear();
			}
		}
		private void timerBall_Tick(object sender, EventArgs e)
		{
			this.PhysicBall();
			this.p1.Invalidate();
		}
		private void p1_SizeChanged(object sender, EventArgs e)
		{
			this.p1.Invalidate();
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Visf));
			this.label1 = new Label();
			this.toolStrip1 = new ToolStrip();
			this.tsbExpBlenderpy = new ToolStripButton();
			this.toolStripSeparator1 = new ToolStripSeparator();
			this.lCntVert = new ToolStripLabel();
			this.toolStripLabel1 = new ToolStripLabel();
			this.lCntTris = new ToolStripLabel();
			this.toolStripLabel2 = new ToolStripLabel();
			this.toolStripSeparator2 = new ToolStripSeparator();
			this.tslMdls = new ToolStripLabel();
			this.tsbShowColl = new ToolStripButton();
			this.tsbBallGame = new ToolStripButton();
			this.flppos = new FlowLayoutPanel();
			this.label2 = new Label();
			this.eyeX = new NumericUpDown();
			this.eyeY = new NumericUpDown();
			this.eyeZ = new NumericUpDown();
			this.label3 = new Label();
			this.fov = new NumericUpDown();
			this.label4 = new Label();
			this.yaw = new NumericUpDown();
			this.pitch = new NumericUpDown();
			this.roll = new NumericUpDown();
			this.cbFog = new CheckBox();
			this.cbVertexColor = new CheckBox();
			this.timerRun = new Timer(this.components);
			this.label5 = new Label();
			this.label6 = new Label();
			this.p1 = new UC();
			this.timerBall = new Timer(this.components);
			this.toolStrip1.SuspendLayout();
			this.flppos.SuspendLayout();
			((ISupportInitialize)this.eyeX).BeginInit();
			((ISupportInitialize)this.eyeY).BeginInit();
			((ISupportInitialize)this.eyeZ).BeginInit();
			((ISupportInitialize)this.fov).BeginInit();
			((ISupportInitialize)this.yaw).BeginInit();
			((ISupportInitialize)this.pitch).BeginInit();
			((ISupportInitialize)this.roll).BeginInit();
			base.SuspendLayout();
			this.label1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(12, 674);
			this.label1.Name = "label1";
			this.label1.Size = new Size(193, 48);
			this.label1.TabIndex = 1;
			this.label1.Text = "* Mouse wheel: Zoom\r\n* Left btn drag: Rotate\r\n* Right btn drag: Move forward/back\r\n* Middle btn: toss ball";
			this.toolStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.tsbExpBlenderpy,
				this.toolStripSeparator1,
				this.lCntVert,
				this.toolStripLabel1,
				this.lCntTris,
				this.toolStripLabel2,
				this.toolStripSeparator2,
				this.tslMdls,
				this.tsbShowColl,
				this.tsbBallGame
			});
			this.toolStrip1.Location = new Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new Size(885, 25);
			this.toolStrip1.TabIndex = 2;
			this.toolStrip1.Text = "toolStrip1";
			this.tsbExpBlenderpy.Image = (Image)componentResourceManager.GetObject("tsbExpBlenderpy.Image");
			this.tsbExpBlenderpy.ImageTransparentColor = Color.Magenta;
			this.tsbExpBlenderpy.Name = "tsbExpBlenderpy";
			this.tsbExpBlenderpy.Size = new Size(169, 22);
			this.tsbExpBlenderpy.Text = "Export to blender script ";
			this.tsbExpBlenderpy.Click += new EventHandler(this.tsbExpBlenderpy_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new Size(6, 25);
			this.lCntVert.Name = "lCntVert";
			this.lCntVert.Size = new Size(15, 22);
			this.lCntVert.Text = "0";
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new Size(62, 22);
			this.toolStripLabel1.Text = "vertices, ";
			this.lCntTris.Name = "lCntTris";
			this.lCntTris.Size = new Size(15, 22);
			this.lCntTris.Text = "0";
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new Size(31, 22);
			this.toolStripLabel2.Text = "tris.";
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new Size(6, 25);
			this.tslMdls.Name = "tslMdls";
			this.tslMdls.Size = new Size(53, 22);
			this.tslMdls.Text = "Models:";
			this.tsbShowColl.CheckOnClick = true;
			this.tsbShowColl.Image = (Image)componentResourceManager.GetObject("tsbShowColl.Image");
			this.tsbShowColl.ImageTransparentColor = Color.Magenta;
			this.tsbShowColl.Name = "tsbShowColl";
			this.tsbShowColl.Size = new Size(75, 22);
			this.tsbShowColl.Text = "Collision";
			this.tsbShowColl.Click += new EventHandler(this.tsbShowColl_Click);
			this.tsbBallGame.CheckOnClick = true;
			this.tsbBallGame.Image = (Image)componentResourceManager.GetObject("tsbBallGame.Image");
			this.tsbBallGame.ImageTransparentColor = Color.Magenta;
			this.tsbBallGame.Name = "tsbBallGame";
			this.tsbBallGame.Size = new Size(86, 22);
			this.tsbBallGame.Text = "Ball game";
			this.tsbBallGame.Click += new EventHandler(this.tsbBallGame_Click);
			this.flppos.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.flppos.Controls.Add(this.label2);
			this.flppos.Controls.Add(this.eyeX);
			this.flppos.Controls.Add(this.eyeY);
			this.flppos.Controls.Add(this.eyeZ);
			this.flppos.Controls.Add(this.label3);
			this.flppos.Controls.Add(this.fov);
			this.flppos.Controls.Add(this.label4);
			this.flppos.Controls.Add(this.yaw);
			this.flppos.Controls.Add(this.pitch);
			this.flppos.Controls.Add(this.roll);
			this.flppos.Controls.Add(this.cbFog);
			this.flppos.Controls.Add(this.cbVertexColor);
			this.flppos.Location = new Point(12, 641);
			this.flppos.Name = "flppos";
			this.flppos.Size = new Size(873, 30);
			this.flppos.TabIndex = 3;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(60, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "eye (x y z)";
			this.eyeX.Location = new Point(69, 3);
			NumericUpDown arg_7C6_0 = this.eyeX;
			int[] array = new int[4];
			array[0] = 64000;
			arg_7C6_0.Maximum = new decimal(array);
			this.eyeX.Minimum = new decimal(new int[]
			{
				64000,
				0,
				0,
				-2147483648
			});
			this.eyeX.Name = "eyeX";
			this.eyeX.Size = new Size(59, 19);
			this.eyeX.TabIndex = 1;
			this.eyeX.TextAlign = HorizontalAlignment.Right;
			this.eyeX.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
			this.eyeY.Location = new Point(134, 3);
			NumericUpDown arg_87A_0 = this.eyeY;
			int[] array2 = new int[4];
			array2[0] = 64000;
			arg_87A_0.Maximum = new decimal(array2);
			this.eyeY.Minimum = new decimal(new int[]
			{
				64000,
				0,
				0,
				-2147483648
			});
			this.eyeY.Name = "eyeY";
			this.eyeY.Size = new Size(59, 19);
			this.eyeY.TabIndex = 2;
			this.eyeY.TextAlign = HorizontalAlignment.Right;
			NumericUpDown arg_905_0 = this.eyeY;
			int[] array3 = new int[4];
			array3[0] = 500;
			arg_905_0.Value = new decimal(array3);
			this.eyeY.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
			this.eyeZ.Location = new Point(199, 3);
			NumericUpDown arg_955_0 = this.eyeZ;
			int[] array4 = new int[4];
			array4[0] = 64000;
			arg_955_0.Maximum = new decimal(array4);
			this.eyeZ.Minimum = new decimal(new int[]
			{
				64000,
				0,
				0,
				-2147483648
			});
			this.eyeZ.Name = "eyeZ";
			this.eyeZ.Size = new Size(59, 19);
			this.eyeZ.TabIndex = 3;
			this.eyeZ.TextAlign = HorizontalAlignment.Right;
			this.eyeZ.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
			this.label3.AutoSize = true;
			this.label3.Location = new Point(264, 0);
			this.label3.Name = "label3";
			this.label3.Size = new Size(21, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "fov";
			this.fov.Location = new Point(291, 3);
			NumericUpDown arg_A6F_0 = this.fov;
			int[] array5 = new int[4];
			array5[0] = 180;
			arg_A6F_0.Maximum = new decimal(array5);
			this.fov.Name = "fov";
			this.fov.Size = new Size(59, 19);
			this.fov.TabIndex = 5;
			this.fov.TextAlign = HorizontalAlignment.Right;
			NumericUpDown arg_ACB_0 = this.fov;
			int[] array6 = new int[4];
			array6[0] = 70;
			arg_ACB_0.Value = new decimal(array6);
			this.fov.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
			this.label4.AutoSize = true;
			this.label4.Location = new Point(356, 0);
			this.label4.Name = "label4";
			this.label4.Size = new Size(125, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "rotation (yaw pitch roll)";
			this.yaw.Location = new Point(487, 3);
			NumericUpDown arg_B7D_0 = this.yaw;
			int[] array7 = new int[4];
			array7[0] = 36000;
			arg_B7D_0.Maximum = new decimal(array7);
			this.yaw.Minimum = new decimal(new int[]
			{
				36000,
				0,
				0,
				-2147483648
			});
			this.yaw.Name = "yaw";
			this.yaw.Size = new Size(59, 19);
			this.yaw.TabIndex = 7;
			this.yaw.TextAlign = HorizontalAlignment.Right;
			this.yaw.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
			this.pitch.Location = new Point(552, 3);
			NumericUpDown arg_C35_0 = this.pitch;
			int[] array8 = new int[4];
			array8[0] = 36000;
			arg_C35_0.Maximum = new decimal(array8);
			this.pitch.Minimum = new decimal(new int[]
			{
				36000,
				0,
				0,
				-2147483648
			});
			this.pitch.Name = "pitch";
			this.pitch.Size = new Size(59, 19);
			this.pitch.TabIndex = 8;
			this.pitch.TextAlign = HorizontalAlignment.Right;
			this.pitch.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
			this.roll.Location = new Point(617, 3);
			NumericUpDown arg_CED_0 = this.roll;
			int[] array9 = new int[4];
			array9[0] = 36000;
			arg_CED_0.Maximum = new decimal(array9);
			this.roll.Minimum = new decimal(new int[]
			{
				36000,
				0,
				0,
				-2147483648
			});
			this.roll.Name = "roll";
			this.roll.Size = new Size(59, 19);
			this.roll.TabIndex = 9;
			this.roll.TextAlign = HorizontalAlignment.Right;
			this.roll.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
			this.cbFog.AutoSize = true;
			this.cbFog.Checked = true;
			this.cbFog.CheckState = CheckState.Checked;
			this.cbFog.Location = new Point(682, 3);
			this.cbFog.Name = "cbFog";
			this.cbFog.Size = new Size(64, 16);
			this.cbFog.TabIndex = 10;
			this.cbFog.Text = "Use &fog";
			this.cbFog.UseVisualStyleBackColor = true;
			this.cbFog.CheckedChanged += new EventHandler(this.cbFog_CheckedChanged);
			this.cbVertexColor.AutoSize = true;
			this.cbVertexColor.Checked = true;
			this.cbVertexColor.CheckState = CheckState.Checked;
			this.cbVertexColor.Location = new Point(752, 3);
			this.cbVertexColor.Name = "cbVertexColor";
			this.cbVertexColor.Size = new Size(97, 16);
			this.cbVertexColor.TabIndex = 11;
			this.cbVertexColor.Text = "Use vertex &clr";
			this.cbVertexColor.UseVisualStyleBackColor = true;
			this.cbVertexColor.CheckedChanged += new EventHandler(this.cbFog_CheckedChanged);
			this.timerRun.Interval = 25;
			this.timerRun.Tick += new EventHandler(this.timerRun_Tick);
			this.label5.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.label5.AutoSize = true;
			this.label5.Location = new Point(229, 675);
			this.label5.Name = "label5";
			this.label5.Size = new Size(99, 48);
			this.label5.TabIndex = 1;
			this.label5.Text = "* W: move forward\r\n* S: move back\r\n* A: move left\r\n* D: move right";
			this.label6.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.label6.AutoSize = true;
			this.label6.Location = new Point(353, 675);
			this.label6.Name = "label6";
			this.label6.Size = new Size(106, 36);
			this.label6.TabIndex = 1;
			this.label6.Text = "* Shift: Move fast\r\n* Up: move up\r\n* Down: move down";
			this.p1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.p1.BackColor = SystemColors.ControlDarkDark;
			this.p1.BorderStyle = BorderStyle.FixedSingle;
			this.p1.Location = new Point(12, 28);
			this.p1.Name = "p1";
			this.p1.Size = new Size(861, 607);
			this.p1.TabIndex = 0;
			this.p1.UseTransparent = true;
			this.p1.Load += new EventHandler(this.p1_Load);
			this.p1.Paint += new PaintEventHandler(this.p1_Paint);
			this.p1.PreviewKeyDown += new PreviewKeyDownEventHandler(this.p1_PreviewKeyDown);
			this.p1.MouseMove += new MouseEventHandler(this.p1_MouseMove);
			this.p1.KeyUp += new KeyEventHandler(this.p1_KeyUp);
			this.p1.MouseDown += new MouseEventHandler(this.p1_MouseDown);
			this.p1.MouseUp += new MouseEventHandler(this.p1_MouseUp);
			this.p1.SizeChanged += new EventHandler(this.p1_SizeChanged);
			this.p1.KeyDown += new KeyEventHandler(this.p1_KeyDown);
			this.timerBall.Interval = 50;
			this.timerBall.Tick += new EventHandler(this.timerBall_Tick);
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(885, 732);
			base.Controls.Add(this.flppos);
			base.Controls.Add(this.toolStrip1);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.p1);
			base.Name = "Visf";
			this.Text = "map viewer test";
			base.Load += new EventHandler(this.Visf_Load);
			base.FormClosing += new FormClosingEventHandler(this.Visf_FormClosing);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.flppos.ResumeLayout(false);
			this.flppos.PerformLayout();
			((ISupportInitialize)this.eyeX).EndInit();
			((ISupportInitialize)this.eyeY).EndInit();
			((ISupportInitialize)this.eyeZ).EndInit();
			((ISupportInitialize)this.fov).EndInit();
			((ISupportInitialize)this.yaw).EndInit();
			((ISupportInitialize)this.pitch).EndInit();
			((ISupportInitialize)this.roll).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
