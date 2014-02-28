using ef1Declib;
using hex04BinTrack;
using khkh_xldMii.Mo;
using khkh_xldMii.Mx;
using khkh_xldMii.V;
using SlimDX;
using SlimDX.Direct3D9;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using vconv122;
namespace khkh_xldMii
{
	public class FormII : Form, ILoadf, IVwer
	{
		private class MatchUt
		{
			public static string findMset(string fmdlx)
			{
				string text = fmdlx.Substring(0, fmdlx.Length - 5) + ".mset";
				if (File.Exists(text))
				{
					return text;
				}
				string text2 = Regex.Replace(text, "_[a-z]+\\.", ".", RegexOptions.IgnoreCase);
				if (File.Exists(text2))
				{
					return text2;
				}
				string text3 = Regex.Replace(text, "_[a-z]+(_[a-z]+\\.)", "$1", RegexOptions.IgnoreCase);
				if (File.Exists(text3))
				{
					return text3;
				}
				string text4 = Regex.Replace(text, "_[a-z]+_[a-z]+\\.", ".", RegexOptions.IgnoreCase);
				if (File.Exists(text4))
				{
					return text4;
				}
				return text;
			}
		}
		private class MotInf
		{
			public Mt1 mt1;
			public float maxtick;
			public float mintick;
			public bool isRaw;
		}
		private class PatTexSel
		{
			public byte texi;
			public byte pati;
			public PatTexSel(byte texi, byte pati)
			{
				this.texi = texi;
				this.pati = pati;
			}
		}
		private class Mesh : IDisposable
		{
			public Mdlxfst mdlx;
			public Texex2[] timc;
			public Texex2 timf;
			public Msetfst mset;
			public List<Texture> altex = new List<Texture>();
			public List<Texture> altex1 = new List<Texture>();
			public List<Texture> altex2 = new List<Texture>();
			public List<Body1> albody1 = new List<Body1>();
			public byte[] binMdlx;
			public byte[] binMset;
			public FormII.CaseTris ctb = new FormII.CaseTris();
			public Mlink ol;
			public FormII.PatTexSel[] pts = new FormII.PatTexSel[0];
			public Matrix[] Ma;
			public FormII.Mesh parent;
			public int iMa = -1;
			public bool Present
			{
				get
				{
					return this.mdlx != null && this.mset != null;
				}
			}
			public void Dispose()
			{
				this.DisposeMdlx();
				this.DisposeMset();
			}
			public void DisposeMset()
			{
				this.mset = null;
				this.binMset = null;
				this.ol = null;
			}
			public void DisposeMdlx()
			{
				this.mdlx = null;
				this.timc = null;
				this.timf = null;
				this.altex.Clear();
				this.altex1.Clear();
				this.altex2.Clear();
				this.albody1.Clear();
				this.binMdlx = null;
				this.ctb.Close();
				this.ol = null;
				this.Ma = null;
			}
		}
		private class CaseTris : IDisposable
		{
			public VertexBuffer vb;
			public VertexFormat vf;
			public int cntPrimitives;
			public int cntVert;
			public FormII.Sepa[] alsepa;
			public void Dispose()
			{
				this.Close();
			}
			public void Close()
			{
				if (this.vb != null)
				{
					this.vb.Dispose();
				}
				this.vb = null;
				this.vf = VertexFormat.Texture0;
				this.cntPrimitives = 0;
				this.cntVert = 0;
			}
		}
		private class Sepa
		{
			public int svi;
			public int cnt;
			public int t;
			public int sel;
			public Sepa(int startVertexIndex, int cntPrimitives, int ti, int sel)
			{
				this.svi = startVertexIndex;
				this.cnt = cntPrimitives;
				this.t = ti;
				this.sel = sel;
			}
		}
		private class UtwexMotionSel
		{
			public static Mt1 Sel(int k1, List<Mt1> al1)
			{
				foreach (Mt1 current in al1)
				{
					if (current.k1 == k1)
					{
						return current;
					}
				}
				return null;
			}
		}
		private class SelTexfacUt
		{
			public static FormII.PatTexSel[] Sel(List<Patc> alp, float tick, FacMod fm)
			{
				FormII.PatTexSel[] array = new FormII.PatTexSel[alp.Count];
				foreach (Fac1 current in fm.alf1)
				{
					if (current.v2 != -1 && (float)current.v0 <= tick && tick < (float)current.v2)
					{
						for (int i = 0; i < alp.Count; i++)
						{
							int num = (int)(tick - (float)current.v0) / 8;
							Texfac[] altf = alp[i].altf;
							for (int j = 0; j < altf.Length; j++)
							{
								Texfac texfac = altf[j];
								if (texfac.i0 == (int)current.v6)
								{
									if (num <= 0 && array[i] == null)
									{
										array[i] = new FormII.PatTexSel((byte)alp[i].texi, (byte)texfac.v6);
										break;
									}
									num -= (int)texfac.v2;
								}
							}
						}
					}
				}
				return array;
			}
		}
		private class TUt
		{
			public static Texture FromBitmap(Device device, Bitmap p)
			{
				MemoryStream memoryStream = new MemoryStream();
				p.Save(memoryStream, ImageFormat.Png);
				memoryStream.Position = 0L;
				return Texture.FromStream(device, memoryStream);
			}
		}
		private FormII.Mesh[] _Sora = new FormII.Mesh[]
		{
			new FormII.Mesh(),
			new FormII.Mesh(),
			new FormII.Mesh()
		};
		private string fmdlxDropped;
		private float tick;
		private Device device;
		private Direct3D d3d = new Direct3D();
		private float fzval = 5f;
		private Vector3 offset = new Vector3(-4f, -66f, 0f);
		private Quaternion quat = new Quaternion(0f, 0f, 0f, 1f);
		private Point curs = Point.Empty;
		private bool hasLDn;
		private int enterLock;
		private bool captureNow;
		private BCForm bcform;
		private IContainer components;
		private SplitContainer splitContainer1;
		private UC panel1;
		private Label label1;
		private SplitContainer splitContainer2;
		private ListView listView1;
		private ColumnHeader columnHeaderRxxx;
		private Label label2;
		private CheckBox checkBoxAnim;
		private NumericUpDown numericUpDownTick;
		private Label label3;
		private Timer timerTick;
		private NumericUpDown numericUpDownStep;
		private NumericUpDown numericUpDownAutoNext;
		private CheckBox checkBoxAutoNext;
		private CheckBox checkBoxAutoRec;
		private NumericUpDown numericUpDownFrame;
		private CheckBox checkBoxAutoFill;
		private Label label5;
		private Label label4;
		private CheckBox checkBoxKeys;
		private Label labelHelpKeys;
		private Button button1;
		private Button buttonBC;
		private CheckBox checkBoxLooks;
		private RadioButton radioButton60fps;
		private RadioButton radioButton30fps;
		private RadioButton radioButton10fps;
		private CheckBox checkBoxAsPNG;
		private CheckBox checkBoxKeepCur;
		private FlowLayoutPanel flowLayoutPanel1;
		private Label label6;
		private NumericUpDown numnear;
		private Label label7;
		private NumericUpDown numfar;
		private Label label8;
		private NumericUpDown numx;
		private Label label9;
		private NumericUpDown numy;
		private Label label10;
		private NumericUpDown numz;
		private Label label11;
		private NumericUpDown numfov;
		private CheckBox checkBoxPerspective;
		private PresentParameters PP
		{
			get
			{
				return new PresentParameters
				{
					Windowed = true,
					SwapEffect = SwapEffect.Discard,
					EnableAutoDepthStencil = true,
					AutoDepthStencilFormat = Format.D24X8,
					BackBufferWidth = this.panel1.ClientSize.Width,
					BackBufferHeight = this.panel1.ClientSize.Height
				};
			}
		}
		public FormII()
		{
			this._Sora[1].parent = this._Sora[0];
			this._Sora[1].iMa = 178;
			this._Sora[2].parent = this._Sora[0];
			this._Sora[2].iMa = 86;
			this.InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			this.listView1.Items.Add("(Drop your .mdlx file to window)");
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			for (int i = 1; i < commandLineArgs.Length; i++)
			{
				string text = commandLineArgs[i];
				if (File.Exists(text) && Path.GetExtension(text).ToLower().Equals(".mdlx"))
				{
					this.loadMdlx(text, 0);
					this.loadMset(FormII.MatchUt.findMset(text), 0);
				}
			}
			this.radioButtonAny_CheckedChanged(null, null);
		}
		private void loadMset(string fmset, int ty)
		{
			FormII.Mesh mesh = this._Sora[ty];
			mesh.DisposeMset();
			if (File.Exists(fmset))
			{
				using (FileStream fileStream = File.OpenRead(fmset))
				{
					mesh.mset = new Msetfst(fileStream, Path.GetFileName(fmset));
				}
				if (ty == 0)
				{
					this.listView1.Items.Clear();
					foreach (Mt1 current in mesh.mset.al1)
					{
						ListViewItem listViewItem = this.listView1.Items.Add(current.id);
						FormII.MotInf motInf = new FormII.MotInf();
						motInf.mt1 = current;
						if (current.isRaw)
						{
							MsetRawblk msetRawblk = new MsetRawblk(new MemoryStream(current.bin, false));
							motInf.maxtick = (float)msetRawblk.cntFrames;
							motInf.mintick = 0f;
						}
						else
						{
							Msetblk msetblk = new Msetblk(new MemoryStream(current.bin, false));
							motInf.maxtick = ((msetblk.to.al11.Length != 0) ? msetblk.to.al11[msetblk.to.al11.Length - 1] : 0f);
							motInf.mintick = ((msetblk.to.al11.Length != 0) ? msetblk.to.al11[0] : 0f);
						}
						listViewItem.Tag = motInf;
					}
					this.listView1.Sorting = SortOrder.Ascending;
					this.listView1.Sort();
				}
				mesh.binMset = File.ReadAllBytes(fmset);
			}
			mesh.ol = null;
		}
		private void loadMdlx(string fmdlx, int ty)
		{
			if (ty == 0)
			{
				this.listView1.Items.Clear();
				this.listView1.Items.Add("(Drop your .mdlx file to window)");
			}
			FormII.Mesh mesh = this._Sora[ty];
			mesh.DisposeMdlx();
			using (FileStream fileStream = File.OpenRead(fmdlx))
			{
				ReadBar.Barent[] array = ReadBar.Explode(fileStream);
				ReadBar.Barent[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					ReadBar.Barent barent = array2[i];
					int k = barent.k;
					if (k != 4)
					{
						if (k == 7)
						{
							mesh.timc = TIMc.Load(new MemoryStream(barent.bin, false));
							mesh.timf = ((mesh.timc.Length >= 1) ? mesh.timc[0] : null);
						}
					}
					else
					{
						mesh.mdlx = new Mdlxfst(new MemoryStream(barent.bin, false));
					}
				}
			}
			mesh.binMdlx = File.ReadAllBytes(fmdlx);
			mesh.ol = null;
			this.reloadTex(ty);
		}
		private void FormII_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
		}
		private void FormII_DragDrop(object sender, DragEventArgs e)
		{
			string[] array = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (array != null)
			{
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string path = array2[i];
					if (Path.GetExtension(path).ToLower().Equals(".mdlx"))
					{
						this.fmdlxDropped = path;
						Timer timer = new Timer();
						timer.Tick += new EventHandler(this.t_Tick);
						timer.Start();
						return;
					}
				}
			}
		}
		private void t_Tick(object sender, EventArgs e)
		{
			((Timer)sender).Dispose();
			string fmdlx = this.fmdlxDropped;
			this.loadMdlx(fmdlx, 0);
			this.loadMset(FormII.MatchUt.findMset(fmdlx), 0);
			this.recalc();
		}
		private void recalc()
		{
			FormII.Mesh[] sora = this._Sora;
			for (int i = 0; i < sora.Length; i++)
			{
				FormII.Mesh mesh = sora[i];
				mesh.ctb.Close();
			}
			IEnumerator enumerator = this.listView1.SelectedItems.GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.Current;
					if (listViewItem.Tag != null)
					{
						this.calcbody(this._Sora[0].ctb, this._Sora[0], ((FormII.MotInf)listViewItem.Tag).mt1);
						if (this._Sora[1].Present)
						{
							Mt1 mt = FormII.UtwexMotionSel.Sel(((FormII.MotInf)listViewItem.Tag).mt1.k1, this._Sora[1].mset.al1);
							if (mt != null && this._Sora[1].iMa != -1 && this._Sora[1].Present)
							{
								this.calcbody(this._Sora[1].ctb, this._Sora[1], mt);
							}
						}
						if (this._Sora[2].Present)
						{
							Mt1 mt2 = FormII.UtwexMotionSel.Sel(((FormII.MotInf)listViewItem.Tag).mt1.k1, this._Sora[2].mset.al1);
							if (mt2 != null && this._Sora[2].iMa != -1 && this._Sora[2].Present)
							{
								this.calcbody(this._Sora[2].ctb, this._Sora[2], mt2);
							}
						}
						this.calcPattex(this._Sora[0], (float)this.numericUpDownTick.Value);
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			this.panel1.Invalidate();
		}
		private void calcPattex(FormII.Mesh M, float tick)
		{
			IEnumerator enumerator = this.listView1.SelectedItems.GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.Current;
					FormII.MotInf motInf = (FormII.MotInf)listViewItem.Tag;
					M.pts = FormII.SelTexfacUt.Sel(M.timf.alp, tick, motInf.mt1.fm);
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		private void calcbody(FormII.CaseTris ct, FormII.Mesh M, Mt1 mt1)
		{
			Mdlxfst mdlx = M.mdlx;
			Msetfst mset = M.mset;
			List<Body1> albody = M.albody1;
			Mlink mlink = M.ol;
			ct.Close();
			albody.Clear();
			if (mdlx != null && mset != null)
			{
				T31 t = mdlx.alt31[0];
				Matrix[] array;
				if (mt1.isRaw)
				{
					MsetRawblk msetRawblk = new MsetRawblk(new MemoryStream(mt1.bin, false));
					int num = Math.Max(0, Math.Min(msetRawblk.cntFrames - 1, (int)Math.Floor((double)this.tick)));
					int num2 = Math.Max(0, Math.Min(msetRawblk.cntFrames - 1, (int)Math.Ceiling((double)this.tick)));
					if (num == num2)
					{
						MsetRM msetRM = msetRawblk.alrm[num];
						array = (M.Ma = msetRM.al.ToArray());
					}
					else
					{
						MsetRM msetRM2 = msetRawblk.alrm[num];
						float num3 = this.tick % 1f;
						MsetRM msetRM3 = msetRawblk.alrm[num2];
						float num4 = 1f - num3;
						array = (M.Ma = new Matrix[msetRawblk.cntJoints]);
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = new Matrix
							{
								M11 = msetRM2.al[i].M11 * num4 + msetRM3.al[i].M11 * num3,
								M21 = msetRM2.al[i].M21 * num4 + msetRM3.al[i].M21 * num3,
								M31 = msetRM2.al[i].M31 * num4 + msetRM3.al[i].M31 * num3,
								M41 = msetRM2.al[i].M41 * num4 + msetRM3.al[i].M41 * num3,
								M12 = msetRM2.al[i].M12 * num4 + msetRM3.al[i].M12 * num3,
								M22 = msetRM2.al[i].M22 * num4 + msetRM3.al[i].M22 * num3,
								M32 = msetRM2.al[i].M32 * num4 + msetRM3.al[i].M32 * num3,
								M42 = msetRM2.al[i].M42 * num4 + msetRM3.al[i].M42 * num3,
								M13 = msetRM2.al[i].M13 * num4 + msetRM3.al[i].M13 * num3,
								M23 = msetRM2.al[i].M23 * num4 + msetRM3.al[i].M23 * num3,
								M33 = msetRM2.al[i].M33 * num4 + msetRM3.al[i].M33 * num3,
								M43 = msetRM2.al[i].M43 * num4 + msetRM3.al[i].M43 * num3,
								M14 = msetRM2.al[i].M14 * num4 + msetRM3.al[i].M14 * num3,
								M24 = msetRM2.al[i].M24 * num4 + msetRM3.al[i].M24 * num3,
								M34 = msetRM2.al[i].M34 * num4 + msetRM3.al[i].M34 * num3,
								M44 = msetRM2.al[i].M44 * num4 + msetRM3.al[i].M44 * num3
							};
						}
					}
				}
				else
				{
					Msetblk msetblk = new Msetblk(new MemoryStream(mt1.bin, false));
					MemoryStream memoryStream = new MemoryStream();
					if (mlink == null)
					{
						mlink = (M.ol = new Mlink());
					}
					mlink.Permit(new MemoryStream(M.binMdlx, false), msetblk.cntb1, new MemoryStream(M.binMset, false), msetblk.cntb2, mt1.off, this.tick, memoryStream);
					BinaryReader binaryReader = new BinaryReader(memoryStream);
					memoryStream.Position = 0L;
					array = (M.Ma = new Matrix[msetblk.cntb1]);
					for (int j = 0; j < msetblk.cntb1; j++)
					{
						array[j] = new Matrix
						{
							M11 = binaryReader.ReadSingle(),
							M12 = binaryReader.ReadSingle(),
							M13 = binaryReader.ReadSingle(),
							M14 = binaryReader.ReadSingle(),
							M21 = binaryReader.ReadSingle(),
							M22 = binaryReader.ReadSingle(),
							M23 = binaryReader.ReadSingle(),
							M24 = binaryReader.ReadSingle(),
							M31 = binaryReader.ReadSingle(),
							M32 = binaryReader.ReadSingle(),
							M33 = binaryReader.ReadSingle(),
							M34 = binaryReader.ReadSingle(),
							M41 = binaryReader.ReadSingle(),
							M42 = binaryReader.ReadSingle(),
							M43 = binaryReader.ReadSingle(),
							M44 = binaryReader.ReadSingle()
						};
					}
				}
				Matrix mv = Matrix.Identity;
				if (M.parent != null && M.iMa != -1)
				{
					mv = M.parent.Ma[M.iMa];
				}
				foreach (T13vif current in t.al13)
				{
					int tops = 544;
					int top = 0;
					VU1Mem vU1Mem = new VU1Mem();
					new ParseVIF1(vU1Mem).Parse(new MemoryStream(current.bin, false), tops);
					Body1 item = SimaVU1.Sima(vU1Mem, array, tops, top, current.texi, current.alaxi, mv);
					albody.Add(item);
				}
				List<uint> list = new List<uint>();
				List<FormII.Sepa> list2 = new List<FormII.Sepa>();
				int num5 = 0;
				int num6 = 0;
				uint[] array2 = new uint[4];
				int num7 = 0;
				int num8 = (int)this.tick;
				int[] array3 = new int[]
				{
					1,
					2,
					3
				};
				foreach (Body1 current2 in albody)
				{
					int num9 = 0;
					for (int k = 0; k < current2.alvi.Length; k++)
					{
						array2[num7] = (uint)(current2.alvi[k] | num6 << 12 | k << 24);
						num7 = (num7 + 1 & 3);
						if (current2.alfl[k] == 32)
						{
							list.Add(array2[num7 - array3[num8 * 103 % 3] & 3]);
							list.Add(array2[num7 - array3[(1 + num8 * 103) % 3] & 3]);
							list.Add(array2[num7 - array3[(2 + num8 * 103) % 3] & 3]);
							num9++;
						}
						else
						{
							if (current2.alfl[k] == 48)
							{
								list.Add(array2[num7 - array3[(num8 << 1) % 3] & 3]);
								list.Add(array2[num7 - array3[(2 + (num8 << 1)) % 3] & 3]);
								list.Add(array2[num7 - array3[(1 + (num8 << 1)) % 3] & 3]);
								num9++;
							}
						}
					}
					list2.Add(new FormII.Sepa(num5, num9, current2.t, num6));
					num5 += 3 * num9;
					num6++;
				}
				ct.alsepa = list2.ToArray();
				ct.cntVert = list.Count;
				ct.cntPrimitives = 0;
				if (ct.cntVert != 0)
				{
					ct.vb = new VertexBuffer(this.device, 36 * ct.cntVert, Usage.None, ct.vf = (VertexFormat.Texture3 | VertexFormat.Position), Pool.Managed);
					DataStream dataStream = ct.vb.Lock(0, 0, LockFlags.None);
					try
					{
						int count = list.Count;
						for (int l = 0; l < count; l++)
						{
							uint num10 = list[l];
							uint num11 = num10 & 4095u;
							uint index = num10 >> 12 & 4095u;
							uint num12 = num10 >> 24 & 4095u;
							Body1 body = albody[(int)index];
							PTex3 value = new PTex3(body.alvert[(int)((UIntPtr)num11)], new Vector2(body.aluv[(int)((UIntPtr)num12)].X, body.aluv[(int)((UIntPtr)num12)].Y));
							dataStream.Write<PTex3>(value);
						}
						dataStream.Position = 0L;
					}
					finally
					{
						ct.vb.Unlock();
					}
				}
			}
		}
		private void panel1_Load(object sender, EventArgs e)
		{
			this.device = new Device(this.d3d, 0, DeviceType.Hardware, this.panel1.Handle, CreateFlags.SoftwareVertexProcessing, new PresentParameters[]
			{
				this.PP
			});
			this.devReset();
			this.reshape();
			this.panel1.MouseWheel += new MouseEventHandler(this.panel1_MouseWheel);
		}
		private void panel1_MouseWheel(object sender, MouseEventArgs e)
		{
			this.fzval = Math.Max(1f, Math.Min(100f, this.fzval + (float)(e.Delta / 120)));
			this.doReshape();
		}
		private void doReshape()
		{
			this.reshape();
			this.panel1.Invalidate();
		}
		private void reshape()
		{
			int width = this.panel1.Width;
			int height = this.panel1.Height;
			float num = (width > height) ? ((float)width / (float)height) : 1f;
			float num2 = (width < height) ? ((float)height / (float)width) : 1f;
			float num3 = this.fzval * 0.5f * 100f;
			if (this.checkBoxPerspective.Checked)
			{
				float znear = Convert.ToSingle(this.numnear.Value);
				float zfar = Convert.ToSingle(this.numfar.Value);
				float num4 = Convert.ToSingle(this.numx.Value);
				float num5 = Convert.ToSingle(this.numy.Value);
				float num6 = Convert.ToSingle(this.numz.Value);
				float fov = Convert.ToSingle(this.numfov.Value) / 180f * 3.14159f;
				this.device.SetTransform(TransformState.Projection, Matrix.PerspectiveFovRH(fov, (width != 0) ? ((float)width / (float)height) : 1f, znear, zfar));
				Matrix value = Matrix.RotationQuaternion(this.quat);
				value.M41 += this.offset.X + num4;
				value.M42 += this.offset.Y + num5;
				value.M43 += this.offset.Z + num6;
				this.device.SetTransform(TransformState.View, value);
				return;
			}
			this.device.SetTransform(TransformState.Projection, Matrix.OrthoLH(num * num3, num2 * num3, num3 * 10f, num3 * -10f));
			Matrix value2 = Matrix.RotationQuaternion(this.quat);
			value2.M41 += this.offset.X;
			value2.M42 += this.offset.Y;
			value2.M43 += this.offset.Z;
			this.device.SetTransform(TransformState.View, value2);
		}
		private void devReset()
		{
			this.device.SetRenderState(RenderState.Lighting, false);
			this.device.SetRenderState(RenderState.ZEnable, true);
			this.device.SetRenderState(RenderState.AlphaBlendEnable, true);
			this.device.SetRenderState<Blend>(RenderState.SourceBlend, Blend.SourceAlpha);
			this.device.SetRenderState<Blend>(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
			this.reloadTex(-1);
		}
		private void reloadTex(int ty)
		{
			if (this.device != null)
			{
				int num = 0;
				FormII.Mesh[] sora = this._Sora;
				for (int i = 0; i < sora.Length; i++)
				{
					FormII.Mesh mesh = sora[i];
					if (num == ty || ty == -1)
					{
						mesh.altex.Clear();
						mesh.altex1.Clear();
						mesh.altex2.Clear();
						if (mesh.timf != null)
						{
							foreach (STim current in mesh.timf.alt)
							{
								mesh.altex.Add(FormII.TUt.FromBitmap(this.device, current.pic));
							}
							if (num == 0)
							{
								for (int j = 0; j < 2; j++)
								{
									int num2 = 0;
									while (true)
									{
										Bitmap pattex = mesh.timf.GetPattex(j, num2);
										if (pattex == null)
										{
											break;
										}
										switch (j)
										{
										case 0:
											mesh.altex1.Add(FormII.TUt.FromBitmap(this.device, pattex));
											break;
										case 1:
											mesh.altex2.Add(FormII.TUt.FromBitmap(this.device, pattex));
											break;
										}
										num2++;
									}
								}
							}
						}
					}
					num++;
				}
			}
		}
		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			if (this.device == null || this.device.TestCooperativeLevel().IsFailure)
			{
				return;
			}
			bool flag = false;
			bool flag2 = true;
			bool @checked = this.checkBoxLooks.Checked;
			this.device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, this.panel1.BackColor, 1f, 0);
			this.device.BeginScene();
			this.device.SetRenderState<FillMode>(RenderState.FillMode, flag ? FillMode.Wireframe : FillMode.Solid);
			FormII.Mesh[] sora = this._Sora;
			for (int i = 0; i < sora.Length; i++)
			{
				FormII.Mesh mesh = sora[i];
				FormII.CaseTris ctb = mesh.ctb;
				if (ctb != null && ctb.vb != null)
				{
					this.device.SetStreamSource(0, ctb.vb, 0, 36);
					this.device.VertexFormat = ctb.vf;
					this.device.Indices = null;
					FormII.Sepa[] alsepa = ctb.alsepa;
					for (int j = 0; j < alsepa.Length; j++)
					{
						FormII.Sepa sepa = alsepa[j];
						this.device.SetTextureStageState(1, TextureStage.ColorOperation, TextureOperation.Disable);
						this.device.SetTextureStageState(2, TextureStage.ColorOperation, TextureOperation.Disable);
						this.device.SetTexture(0, null);
						List<BaseTexture> list = new List<BaseTexture>();
						if (flag2)
						{
							if (sepa.t < mesh.altex.Count)
							{
								list.Add(mesh.altex[sepa.t]);
							}
							if (@checked && mesh.pts.Length >= 1 && mesh.pts[0] != null && sepa.t == (int)mesh.pts[0].texi)
							{
								list.Add(mesh.altex1[(int)mesh.pts[0].pati]);
							}
							if (@checked && mesh.pts.Length >= 2 && mesh.pts[1] != null && sepa.t == (int)mesh.pts[1].texi)
							{
								list.Add(mesh.altex2[(int)mesh.pts[1].pati]);
							}
							list.Remove(null);
							this.device.SetTextureStageState(0, TextureStage.ColorOperation, TextureOperation.SelectArg1);
							this.device.SetTextureStageState(0, TextureStage.ColorArg1, TextureArgument.Texture);
							this.device.SetTexture(0, (list.Count < 1) ? null : list[0]);
							if (list.Count >= 2)
							{
								this.device.SetTextureStageState(1, TextureStage.ColorOperation, TextureOperation.BlendTextureAlpha);
								this.device.SetTextureStageState(1, TextureStage.ColorArg1, TextureArgument.Texture);
								this.device.SetTextureStageState(1, TextureStage.ColorArg2, TextureArgument.Current);
								this.device.SetTexture(1, list[1]);
								if (list.Count >= 3)
								{
									this.device.SetTextureStageState(2, TextureStage.ColorOperation, TextureOperation.BlendTextureAlpha);
									this.device.SetTextureStageState(2, TextureStage.ColorArg1, TextureArgument.Texture);
									this.device.SetTextureStageState(2, TextureStage.ColorArg2, TextureArgument.Current);
									this.device.SetTexture(2, list[2]);
								}
							}
						}
						this.device.DrawPrimitives(PrimitiveType.TriangleList, sepa.svi, sepa.cnt);
					}
				}
			}
			this.device.EndScene();
			if (this.captureNow)
			{
				this.captureNow = false;
				using (Surface backBuffer = this.device.GetBackBuffer(0, 0))
				{
					int num = (int)this.numericUpDownFrame.Value;
					if (this.checkBoxAsPNG.Checked)
					{
						Surface.ToFile(backBuffer, "_cap" + num.ToString("00000") + ".png", ImageFileFormat.Png);
					}
					else
					{
						Surface.ToFile(backBuffer, "_cap" + num.ToString("00000") + ".jpg", ImageFileFormat.Jpg);
					}
					NumericUpDown expr_3AF = this.numericUpDownFrame;
					expr_3AF.Value = ++expr_3AF.Value;
				}
			}
			this.device.Present();
		}
		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listView1.SelectedIndices.GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					int arg_1E_0 = (int)enumerator.Current;
					this.numericUpDownTick.Value = 0m;
					this.checkBoxAutoFill_CheckedChanged(null, null);
					this.recalc();
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != MouseButtons.None)
			{
				this.curs = e.Location;
				this.hasLDn = true;
			}
		}
		private void panel1_MouseUp(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != MouseButtons.None && this.hasLDn)
			{
				this.hasLDn = false;
			}
		}
		private void panel1_MouseMove(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) != MouseButtons.None && this.hasLDn)
			{
				int num = e.X - this.curs.X;
				int num2 = e.Y - this.curs.Y;
				if (num != 0 || num2 != 0)
				{
					this.curs = e.Location;
					if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
					{
						this.quat *= Quaternion.RotationYawPitchRoll((float)num / 100f, (float)num2 / 100f, 0f);
					}
					else
					{
						this.offset += new Vector3((float)num, (float)(-(float)num2), 0f);
					}
					this.doReshape();
				}
			}
		}
		private void checkBoxAnim_CheckedChanged(object sender, EventArgs e)
		{
			this.timerTick.Enabled = this.checkBoxAnim.Checked;
		}
		private void timerTick_Tick(object sender, EventArgs e)
		{
			try
			{
				if (this.enterLock++ == 0)
				{
					try
					{
						this.tick = (float)(this.numericUpDownTick.Value + this.numericUpDownStep.Value);
						if (this.checkBoxAutoNext.Checked && (float)this.numericUpDownAutoNext.Value <= this.tick)
						{
							if (this.checkBoxKeepCur.Checked)
							{
								this.tick = 1f;
							}
							else
							{
								IEnumerator enumerator = this.listView1.SelectedIndices.GetEnumerator();
								try
								{
									if (enumerator.MoveNext())
									{
										int num = (int)enumerator.Current;
										int num2 = num + 1;
										if (num2 < this.listView1.Items.Count)
										{
											this.listView1.Items[num].Selected = false;
											this.listView1.Items[num2].Selected = true;
											this.checkBoxAutoFill_CheckedChanged(null, null);
										}
										else
										{
											this.checkBoxAnim.Checked = false;
										}
									}
								}
								finally
								{
									IDisposable disposable = enumerator as IDisposable;
									if (disposable != null)
									{
										disposable.Dispose();
									}
								}
							}
						}
						if (this.checkBoxAutoRec.Checked)
						{
							this.captureNow = true;
						}
						this.numericUpDownTick.Value = (decimal)this.tick;
					}
					catch (Exception)
					{
						this.timerTick.Stop();
						this.checkBoxAnim.Checked = false;
						throw;
					}
				}
			}
			finally
			{
				this.enterLock--;
			}
		}
		private void numericUpDownTick_ValueChanged(object sender, EventArgs e)
		{
			this.tick = (float)this.numericUpDownTick.Value;
			this.recalc();
		}
		private void checkBoxAutoFill_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxAutoFill.Checked)
			{
				IEnumerator enumerator = this.listView1.SelectedItems.GetEnumerator();
				try
				{
					if (enumerator.MoveNext())
					{
						ListViewItem listViewItem = (ListViewItem)enumerator.Current;
						this.numericUpDownAutoNext.Value = (decimal)((FormII.MotInf)listViewItem.Tag).maxtick + this.numericUpDownStep.Value;
					}
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
		}
		private void panel1_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Prior:
				this.fzval = 5f;
				this.offset = new Vector3(-4f, -66f, 0f);
				this.quat = new Quaternion(0f, 0f, 0f, 1f);
				this.doReshape();
				break;
			case Keys.Next:
				this.fzval = 4f;
				this.offset = new Vector3(28f, -92f, 0f);
				this.quat = new Quaternion(-0.01664254f, -0.1349622f, -0.01049327f, 0.9906555f);
				this.doReshape();
				return;
			case Keys.End:
				break;
			case Keys.Home:
				this.fzval = 1f;
				this.offset = Vector3.Zero;
				this.quat = Quaternion.Identity;
				this.doReshape();
				return;
			default:
				return;
			}
		}
		private void checkBoxKeys_CheckedChanged(object sender, EventArgs e)
		{
			this.labelHelpKeys.Visible = this.checkBoxKeys.Checked;
		}
		private void button1_Click(object sender, EventArgs e)
		{
			IEnumerator enumerator = this.listView1.SelectedItems.GetEnumerator();
			try
			{
				if (enumerator.MoveNext())
				{
					ListViewItem listViewItem = (ListViewItem)enumerator.Current;
					if (listViewItem.Tag != null)
					{
						Mt1 mt = ((FormII.MotInf)listViewItem.Tag).mt1;
						Msetblk msetblk = new Msetblk(new MemoryStream(mt.bin, false));
						T31 arg_6A_0 = this._Sora[0].mdlx.alt31[0];
						Mlink mlink = this._Sora[0].ol = new Mlink();
						MemoryStream fsMdlx = new MemoryStream(this._Sora[0].binMdlx, false);
						MemoryStream fsMset = new MemoryStream(this._Sora[0].binMset, false);
						for (float num = 0f; num <= 300f; num += 1f)
						{
							float[] array;
							float[] array2;
							float[] array3;
							mlink.Permit_DEB(fsMdlx, msetblk.cntb1, fsMset, msetblk.cntb2, mt.off, num, out array, out array2, out array3);
						}
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		private void label3_DoubleClick(object sender, EventArgs e)
		{
			this.button1.Show();
		}
		private void fl1_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = ((e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop)) ? DragDropEffects.Copy : e.Effect);
		}
		private void fl1_DragDrop(object sender, DragEventArgs e)
		{
			MessageBox.Show("ok");
		}
		private void buttonBC_Click(object sender, EventArgs e)
		{
			if (this.bcform == null || (this.bcform != null && this.bcform.IsDisposed))
			{
				this.bcform = new BCForm(this);
			}
			this.bcform.Show();
			this.bcform.Activate();
		}
		public void LoadOf(int x, string fp)
		{
			using (new WC())
			{
				switch (x)
				{
				case 0:
					this.loadMdlx(fp, 0);
					break;
				case 1:
					this.loadMset(fp, 0);
					break;
				case 2:
					this.loadMdlx(fp, 1);
					break;
				case 3:
					this.loadMset(fp, 1);
					break;
				case 4:
					this.loadMdlx(fp, 2);
					break;
				case 5:
					this.loadMset(fp, 2);
					break;
				default:
					throw new NotSupportedException();
				}
			}
		}
		public void SetJointOf(int x, int joint)
		{
			switch (x)
			{
			case 1:
				this._Sora[1].iMa = joint;
				return;
			case 2:
				this._Sora[2].iMa = joint;
				return;
			default:
				throw new NotSupportedException();
			}
		}
		public void DoRecalc()
		{
			this.recalc();
			this.panel1.Invalidate();
		}
		public void BackToViewer()
		{
			base.Activate();
		}
		private void checkBoxLooks_CheckedChanged(object sender, EventArgs e)
		{
			this.panel1.Invalidate();
		}
		private void radioButtonAny_CheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButton10fps.Checked)
			{
				this.timerTick.Interval = 100;
				return;
			}
			if (this.radioButton30fps.Checked)
			{
				this.timerTick.Interval = 33;
				return;
			}
			if (this.radioButton60fps.Checked)
			{
				this.timerTick.Interval = 16;
			}
		}
		private void panel1_Resize(object sender, EventArgs e)
		{
			if (this.device != null)
			{
				this.device.Reset(this.PP);
				this.panel1.Invalidate();
				this.reshape();
			}
		}
		private void numnear_ValueChanged(object sender, EventArgs e)
		{
			this.panel1.Invalidate();
			this.reshape();
		}
		private void checkBoxPerspective_CheckedChanged(object sender, EventArgs e)
		{
			this.panel1.Invalidate();
			this.reshape();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormII));
			this.timerTick = new Timer(this.components);
			this.splitContainer1 = new SplitContainer();
			this.splitContainer2 = new SplitContainer();
			this.listView1 = new ListView();
			this.columnHeaderRxxx = new ColumnHeader();
			this.label2 = new Label();
			this.checkBoxPerspective = new CheckBox();
			this.checkBoxKeepCur = new CheckBox();
			this.checkBoxAsPNG = new CheckBox();
			this.radioButton60fps = new RadioButton();
			this.radioButton30fps = new RadioButton();
			this.radioButton10fps = new RadioButton();
			this.checkBoxLooks = new CheckBox();
			this.buttonBC = new Button();
			this.button1 = new Button();
			this.checkBoxKeys = new CheckBox();
			this.label4 = new Label();
			this.label5 = new Label();
			this.checkBoxAutoFill = new CheckBox();
			this.checkBoxAutoRec = new CheckBox();
			this.numericUpDownFrame = new NumericUpDown();
			this.numericUpDownStep = new NumericUpDown();
			this.numericUpDownAutoNext = new NumericUpDown();
			this.checkBoxAutoNext = new CheckBox();
			this.checkBoxAnim = new CheckBox();
			this.numericUpDownTick = new NumericUpDown();
			this.label3 = new Label();
			this.flowLayoutPanel1 = new FlowLayoutPanel();
			this.label6 = new Label();
			this.numnear = new NumericUpDown();
			this.label7 = new Label();
			this.numfar = new NumericUpDown();
			this.label11 = new Label();
			this.numfov = new NumericUpDown();
			this.label8 = new Label();
			this.numx = new NumericUpDown();
			this.label9 = new Label();
			this.numy = new NumericUpDown();
			this.label10 = new Label();
			this.numz = new NumericUpDown();
			this.labelHelpKeys = new Label();
			this.panel1 = new UC();
			this.label1 = new Label();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((ISupportInitialize)this.numericUpDownFrame).BeginInit();
			((ISupportInitialize)this.numericUpDownStep).BeginInit();
			((ISupportInitialize)this.numericUpDownAutoNext).BeginInit();
			((ISupportInitialize)this.numericUpDownTick).BeginInit();
			this.flowLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.numnear).BeginInit();
			((ISupportInitialize)this.numfar).BeginInit();
			((ISupportInitialize)this.numfov).BeginInit();
			((ISupportInitialize)this.numx).BeginInit();
			((ISupportInitialize)this.numy).BeginInit();
			((ISupportInitialize)this.numz).BeginInit();
			base.SuspendLayout();
			this.timerTick.Interval = 16;
			this.timerTick.Tick += new EventHandler(this.timerTick_Tick);
			this.splitContainer1.Dock = DockStyle.Fill;
			this.splitContainer1.FixedPanel = FixedPanel.Panel1;
			this.splitContainer1.Location = new Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			this.splitContainer1.Panel1.Padding = new Padding(3);
			this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel1);
			this.splitContainer1.Panel2.Controls.Add(this.labelHelpKeys);
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Panel2.Controls.Add(this.label1);
			this.splitContainer1.Panel2.Padding = new Padding(3);
			this.splitContainer1.Size = new Size(715, 540);
			this.splitContainer1.SplitterDistance = 242;
			this.splitContainer1.TabIndex = 1;
			this.splitContainer2.Dock = DockStyle.Fill;
			this.splitContainer2.FixedPanel = FixedPanel.Panel2;
			this.splitContainer2.Location = new Point(3, 3);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = Orientation.Horizontal;
			this.splitContainer2.Panel1.Controls.Add(this.listView1);
			this.splitContainer2.Panel1.Controls.Add(this.label2);
			this.splitContainer2.Panel2.Controls.Add(this.checkBoxPerspective);
			this.splitContainer2.Panel2.Controls.Add(this.checkBoxKeepCur);
			this.splitContainer2.Panel2.Controls.Add(this.checkBoxAsPNG);
			this.splitContainer2.Panel2.Controls.Add(this.radioButton60fps);
			this.splitContainer2.Panel2.Controls.Add(this.radioButton30fps);
			this.splitContainer2.Panel2.Controls.Add(this.radioButton10fps);
			this.splitContainer2.Panel2.Controls.Add(this.checkBoxLooks);
			this.splitContainer2.Panel2.Controls.Add(this.buttonBC);
			this.splitContainer2.Panel2.Controls.Add(this.button1);
			this.splitContainer2.Panel2.Controls.Add(this.checkBoxKeys);
			this.splitContainer2.Panel2.Controls.Add(this.label4);
			this.splitContainer2.Panel2.Controls.Add(this.label5);
			this.splitContainer2.Panel2.Controls.Add(this.checkBoxAutoFill);
			this.splitContainer2.Panel2.Controls.Add(this.checkBoxAutoRec);
			this.splitContainer2.Panel2.Controls.Add(this.numericUpDownFrame);
			this.splitContainer2.Panel2.Controls.Add(this.numericUpDownStep);
			this.splitContainer2.Panel2.Controls.Add(this.numericUpDownAutoNext);
			this.splitContainer2.Panel2.Controls.Add(this.checkBoxAutoNext);
			this.splitContainer2.Panel2.Controls.Add(this.checkBoxAnim);
			this.splitContainer2.Panel2.Controls.Add(this.numericUpDownTick);
			this.splitContainer2.Panel2.Controls.Add(this.label3);
			this.splitContainer2.Size = new Size(236, 534);
			this.splitContainer2.SplitterDistance = 170;
			this.splitContainer2.TabIndex = 0;
			this.listView1.Columns.AddRange(new ColumnHeader[]
			{
				this.columnHeaderRxxx
			});
			this.listView1.Dock = DockStyle.Fill;
			this.listView1.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new Point(0, 14);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new Size(236, 156);
			this.listView1.TabIndex = 2;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = View.Details;
			this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
			this.columnHeaderRxxx.Text = "Motion";
			this.columnHeaderRxxx.Width = 188;
			this.label2.AutoSize = true;
			this.label2.BorderStyle = BorderStyle.Fixed3D;
			this.label2.Dock = DockStyle.Top;
			this.label2.Location = new Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(33, 14);
			this.label2.TabIndex = 1;
			this.label2.Text = "Anim";
			this.checkBoxPerspective.AutoSize = true;
			this.checkBoxPerspective.FlatStyle = FlatStyle.Flat;
			this.checkBoxPerspective.Location = new Point(5, 294);
			this.checkBoxPerspective.Name = "checkBoxPerspective";
			this.checkBoxPerspective.Size = new Size(210, 16);
			this.checkBoxPerspective.TabIndex = 19;
			this.checkBoxPerspective.Text = "Perspective &view (3D Ripper likes it)";
			this.checkBoxPerspective.UseVisualStyleBackColor = true;
			this.checkBoxPerspective.CheckedChanged += new EventHandler(this.checkBoxPerspective_CheckedChanged);
			this.checkBoxKeepCur.AutoSize = true;
			this.checkBoxKeepCur.FlatStyle = FlatStyle.Flat;
			this.checkBoxKeepCur.Location = new Point(5, 123);
			this.checkBoxKeepCur.Name = "checkBoxKeepCur";
			this.checkBoxKeepCur.Size = new Size(123, 16);
			this.checkBoxKeepCur.TabIndex = 9;
			this.checkBoxKeepCur.Text = "&Loop current motion";
			this.checkBoxKeepCur.UseVisualStyleBackColor = true;
			this.checkBoxAsPNG.AutoSize = true;
			this.checkBoxAsPNG.FlatStyle = FlatStyle.Flat;
			this.checkBoxAsPNG.Location = new Point(5, 218);
			this.checkBoxAsPNG.Name = "checkBoxAsPNG";
			this.checkBoxAsPNG.Size = new Size(100, 16);
			this.checkBoxAsPNG.TabIndex = 14;
			this.checkBoxAsPNG.Text = "Use &png format";
			this.checkBoxAsPNG.UseVisualStyleBackColor = true;
			this.radioButton60fps.Appearance = Appearance.Button;
			this.radioButton60fps.AutoSize = true;
			this.radioButton60fps.Checked = true;
			this.radioButton60fps.FlatStyle = FlatStyle.Flat;
			this.radioButton60fps.Location = new Point(172, 264);
			this.radioButton60fps.Name = "radioButton60fps";
			this.radioButton60fps.Size = new Size(45, 24);
			this.radioButton60fps.TabIndex = 18;
			this.radioButton60fps.TabStop = true;
			this.radioButton60fps.Text = "&60fps";
			this.radioButton60fps.UseVisualStyleBackColor = true;
			this.radioButton60fps.CheckedChanged += new EventHandler(this.radioButtonAny_CheckedChanged);
			this.radioButton30fps.Appearance = Appearance.Button;
			this.radioButton30fps.AutoSize = true;
			this.radioButton30fps.FlatStyle = FlatStyle.Flat;
			this.radioButton30fps.Location = new Point(121, 264);
			this.radioButton30fps.Name = "radioButton30fps";
			this.radioButton30fps.Size = new Size(45, 24);
			this.radioButton30fps.TabIndex = 17;
			this.radioButton30fps.Text = "&30fps";
			this.radioButton30fps.UseVisualStyleBackColor = true;
			this.radioButton30fps.CheckedChanged += new EventHandler(this.radioButtonAny_CheckedChanged);
			this.radioButton10fps.Appearance = Appearance.Button;
			this.radioButton10fps.AutoSize = true;
			this.radioButton10fps.FlatStyle = FlatStyle.Flat;
			this.radioButton10fps.Location = new Point(70, 264);
			this.radioButton10fps.Name = "radioButton10fps";
			this.radioButton10fps.Size = new Size(45, 24);
			this.radioButton10fps.TabIndex = 16;
			this.radioButton10fps.Text = "&10fps";
			this.radioButton10fps.UseVisualStyleBackColor = true;
			this.radioButton10fps.CheckedChanged += new EventHandler(this.radioButtonAny_CheckedChanged);
			this.checkBoxLooks.AutoSize = true;
			this.checkBoxLooks.FlatStyle = FlatStyle.Flat;
			this.checkBoxLooks.Location = new Point(5, 240);
			this.checkBoxLooks.Name = "checkBoxLooks";
			this.checkBoxLooks.Size = new Size(152, 16);
			this.checkBoxLooks.TabIndex = 15;
			this.checkBoxLooks.Text = "&Enable face looks change";
			this.checkBoxLooks.UseVisualStyleBackColor = true;
			this.checkBoxLooks.CheckedChanged += new EventHandler(this.checkBoxLooks_CheckedChanged);
			this.buttonBC.FlatStyle = FlatStyle.Flat;
			this.buttonBC.Location = new Point(5, 316);
			this.buttonBC.Name = "buttonBC";
			this.buttonBC.Size = new Size(212, 34);
			this.buttonBC.TabIndex = 20;
			this.buttonBC.Text = "&Bind Controller";
			this.buttonBC.UseVisualStyleBackColor = true;
			this.buttonBC.Click += new EventHandler(this.buttonBC_Click);
			this.button1.FlatStyle = FlatStyle.Flat;
			this.button1.Location = new Point(151, 3);
			this.button1.Name = "button1";
			this.button1.Size = new Size(70, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "&DEB";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.checkBoxKeys.AutoSize = true;
			this.checkBoxKeys.FlatStyle = FlatStyle.Flat;
			this.checkBoxKeys.Location = new Point(5, 147);
			this.checkBoxKeys.Name = "checkBoxKeys";
			this.checkBoxKeys.Size = new Size(126, 16);
			this.checkBoxKeys.TabIndex = 10;
			this.checkBoxKeys.Text = "Show short cut &keys";
			this.checkBoxKeys.UseVisualStyleBackColor = true;
			this.checkBoxKeys.CheckedChanged += new EventHandler(this.checkBoxKeys_CheckedChanged);
			this.label4.AutoSize = true;
			this.label4.Location = new Point(49, 195);
			this.label4.Name = "label4";
			this.label4.Size = new Size(75, 12);
			this.label4.TabIndex = 12;
			this.label4.Text = "Next &file no ...";
			this.label5.AutoSize = true;
			this.label5.Location = new Point(3, 29);
			this.label5.Name = "label5";
			this.label5.Size = new Size(46, 12);
			this.label5.TabIndex = 2;
			this.label5.Text = "&Cur tick";
			this.checkBoxAutoFill.AutoSize = true;
			this.checkBoxAutoFill.FlatStyle = FlatStyle.Flat;
			this.checkBoxAutoFill.Location = new Point(5, 101);
			this.checkBoxAutoFill.Name = "checkBoxAutoFill";
			this.checkBoxAutoFill.Size = new Size(110, 16);
			this.checkBoxAutoFill.TabIndex = 8;
			this.checkBoxAutoFill.Text = "Auto fill max &tick";
			this.checkBoxAutoFill.UseVisualStyleBackColor = true;
			this.checkBoxAutoFill.CheckedChanged += new EventHandler(this.checkBoxAutoFill_CheckedChanged);
			this.checkBoxAutoRec.AutoSize = true;
			this.checkBoxAutoRec.FlatStyle = FlatStyle.Flat;
			this.checkBoxAutoRec.Location = new Point(5, 169);
			this.checkBoxAutoRec.Name = "checkBoxAutoRec";
			this.checkBoxAutoRec.Size = new Size(178, 16);
			this.checkBoxAutoRec.TabIndex = 11;
			this.checkBoxAutoRec.Text = "Capture &screen shot per frame";
			this.checkBoxAutoRec.UseVisualStyleBackColor = true;
			this.numericUpDownFrame.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.numericUpDownFrame.Location = new Point(147, 191);
			NumericUpDown arg_1069_0 = this.numericUpDownFrame;
			int[] array = new int[4];
			array[0] = 99999;
			arg_1069_0.Maximum = new decimal(array);
			this.numericUpDownFrame.Name = "numericUpDownFrame";
			this.numericUpDownFrame.Size = new Size(70, 21);
			this.numericUpDownFrame.TabIndex = 13;
			this.numericUpDownFrame.TextAlign = HorizontalAlignment.Right;
			NumericUpDown arg_10C2_0 = this.numericUpDownFrame;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_10C2_0.Value = new decimal(array2);
			this.numericUpDownStep.DecimalPlaces = 2;
			this.numericUpDownStep.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.numericUpDownStep.Location = new Point(151, 49);
			NumericUpDown arg_1125_0 = this.numericUpDownStep;
			int[] array3 = new int[4];
			array3[0] = 1000;
			arg_1125_0.Maximum = new decimal(array3);
			this.numericUpDownStep.Name = "numericUpDownStep";
			this.numericUpDownStep.Size = new Size(70, 21);
			this.numericUpDownStep.TabIndex = 5;
			this.numericUpDownStep.TextAlign = HorizontalAlignment.Right;
			NumericUpDown arg_1180_0 = this.numericUpDownStep;
			int[] array4 = new int[4];
			array4[0] = 1;
			arg_1180_0.Value = new decimal(array4);
			this.numericUpDownAutoNext.DecimalPlaces = 2;
			this.numericUpDownAutoNext.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.numericUpDownAutoNext.Location = new Point(151, 76);
			NumericUpDown arg_11E3_0 = this.numericUpDownAutoNext;
			int[] array5 = new int[4];
			array5[0] = 9999;
			arg_11E3_0.Maximum = new decimal(array5);
			this.numericUpDownAutoNext.Name = "numericUpDownAutoNext";
			this.numericUpDownAutoNext.Size = new Size(70, 21);
			this.numericUpDownAutoNext.TabIndex = 7;
			this.numericUpDownAutoNext.TextAlign = HorizontalAlignment.Right;
			NumericUpDown arg_123F_0 = this.numericUpDownAutoNext;
			int[] array6 = new int[4];
			array6[0] = 100;
			arg_123F_0.Value = new decimal(array6);
			this.checkBoxAutoNext.AutoSize = true;
			this.checkBoxAutoNext.FlatStyle = FlatStyle.Flat;
			this.checkBoxAutoNext.Location = new Point(5, 79);
			this.checkBoxAutoNext.Name = "checkBoxAutoNext";
			this.checkBoxAutoNext.Size = new Size(132, 16);
			this.checkBoxAutoNext.TabIndex = 6;
			this.checkBoxAutoNext.Text = "&Next motion on tick ...";
			this.checkBoxAutoNext.UseVisualStyleBackColor = true;
			this.checkBoxAnim.AutoSize = true;
			this.checkBoxAnim.FlatStyle = FlatStyle.Flat;
			this.checkBoxAnim.Location = new Point(55, 52);
			this.checkBoxAnim.Name = "checkBoxAnim";
			this.checkBoxAnim.Size = new Size(87, 16);
			this.checkBoxAnim.TabIndex = 4;
			this.checkBoxAnim.Text = "&Auto step by";
			this.checkBoxAnim.UseVisualStyleBackColor = true;
			this.checkBoxAnim.CheckedChanged += new EventHandler(this.checkBoxAnim_CheckedChanged);
			this.numericUpDownTick.DecimalPlaces = 2;
			this.numericUpDownTick.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.numericUpDownTick.Location = new Point(55, 25);
			NumericUpDown arg_13A8_0 = this.numericUpDownTick;
			int[] array7 = new int[4];
			array7[0] = 10000;
			arg_13A8_0.Maximum = new decimal(array7);
			this.numericUpDownTick.Name = "numericUpDownTick";
			this.numericUpDownTick.Size = new Size(76, 21);
			this.numericUpDownTick.TabIndex = 3;
			this.numericUpDownTick.TextAlign = HorizontalAlignment.Right;
			this.numericUpDownTick.ValueChanged += new EventHandler(this.numericUpDownTick_ValueChanged);
			this.label3.AutoSize = true;
			this.label3.BorderStyle = BorderStyle.Fixed3D;
			this.label3.Location = new Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new Size(44, 14);
			this.label3.TabIndex = 0;
			this.label3.Text = "Control";
			this.label3.DoubleClick += new EventHandler(this.label3_DoubleClick);
			this.flowLayoutPanel1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
			this.flowLayoutPanel1.Controls.Add(this.label6);
			this.flowLayoutPanel1.Controls.Add(this.numnear);
			this.flowLayoutPanel1.Controls.Add(this.label7);
			this.flowLayoutPanel1.Controls.Add(this.numfar);
			this.flowLayoutPanel1.Controls.Add(this.label11);
			this.flowLayoutPanel1.Controls.Add(this.numfov);
			this.flowLayoutPanel1.Controls.Add(this.label8);
			this.flowLayoutPanel1.Controls.Add(this.numx);
			this.flowLayoutPanel1.Controls.Add(this.label9);
			this.flowLayoutPanel1.Controls.Add(this.numy);
			this.flowLayoutPanel1.Controls.Add(this.label10);
			this.flowLayoutPanel1.Controls.Add(this.numz);
			this.flowLayoutPanel1.Location = new Point(259, 345);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new Size(198, 175);
			this.flowLayoutPanel1.TabIndex = 4;
			this.flowLayoutPanel1.Visible = false;
			this.label6.Anchor = AnchorStyles.Left;
			this.label6.AutoSize = true;
			this.label6.Location = new Point(3, 6);
			this.label6.Name = "label6";
			this.label6.Size = new Size(27, 12);
			this.label6.TabIndex = 0;
			this.label6.Text = "near";
			this.flowLayoutPanel1.SetFlowBreak(this.numnear, true);
			this.numnear.Location = new Point(36, 3);
			NumericUpDown arg_16AB_0 = this.numnear;
			int[] array8 = new int[4];
			array8[0] = 10000;
			arg_16AB_0.Maximum = new decimal(array8);
			this.numnear.Minimum = new decimal(new int[]
			{
				10000,
				0,
				0,
				-2147483648
			});
			this.numnear.Name = "numnear";
			this.numnear.Size = new Size(60, 19);
			this.numnear.TabIndex = 1;
			this.numnear.TextAlign = HorizontalAlignment.Right;
			NumericUpDown arg_1733_0 = this.numnear;
			int[] array9 = new int[4];
			array9[0] = 20;
			arg_1733_0.Value = new decimal(array9);
			this.numnear.ValueChanged += new EventHandler(this.numnear_ValueChanged);
			this.label7.Anchor = AnchorStyles.Left;
			this.label7.AutoSize = true;
			this.label7.Location = new Point(3, 31);
			this.label7.Name = "label7";
			this.label7.Size = new Size(19, 12);
			this.label7.TabIndex = 2;
			this.label7.Text = "far";
			this.flowLayoutPanel1.SetFlowBreak(this.numfar, true);
			this.numfar.Location = new Point(28, 28);
			NumericUpDown arg_17FE_0 = this.numfar;
			int[] array10 = new int[4];
			array10[0] = 10000;
			arg_17FE_0.Maximum = new decimal(array10);
			this.numfar.Minimum = new decimal(new int[]
			{
				10000,
				0,
				0,
				-2147483648
			});
			this.numfar.Name = "numfar";
			this.numfar.Size = new Size(60, 19);
			this.numfar.TabIndex = 3;
			this.numfar.TextAlign = HorizontalAlignment.Right;
			NumericUpDown arg_1889_0 = this.numfar;
			int[] array11 = new int[4];
			array11[0] = 500;
			arg_1889_0.Value = new decimal(array11);
			this.numfar.ValueChanged += new EventHandler(this.numnear_ValueChanged);
			this.label11.Anchor = AnchorStyles.Left;
			this.label11.AutoSize = true;
			this.label11.Location = new Point(3, 56);
			this.label11.Name = "label11";
			this.label11.Size = new Size(21, 12);
			this.label11.TabIndex = 10;
			this.label11.Text = "fov";
			this.flowLayoutPanel1.SetFlowBreak(this.numfov, true);
			this.numfov.Location = new Point(30, 53);
			NumericUpDown arg_1955_0 = this.numfov;
			int[] array12 = new int[4];
			array12[0] = 360;
			arg_1955_0.Maximum = new decimal(array12);
			NumericUpDown arg_1974_0 = this.numfov;
			int[] array13 = new int[4];
			array13[0] = 1;
			arg_1974_0.Minimum = new decimal(array13);
			this.numfov.Name = "numfov";
			this.numfov.Size = new Size(60, 19);
			this.numfov.TabIndex = 11;
			this.numfov.TextAlign = HorizontalAlignment.Right;
			NumericUpDown arg_19D1_0 = this.numfov;
			int[] array14 = new int[4];
			array14[0] = 60;
			arg_19D1_0.Value = new decimal(array14);
			this.numfov.ValueChanged += new EventHandler(this.numnear_ValueChanged);
			this.label8.Anchor = AnchorStyles.Left;
			this.label8.AutoSize = true;
			this.label8.Location = new Point(3, 81);
			this.label8.Name = "label8";
			this.label8.Size = new Size(11, 12);
			this.label8.TabIndex = 4;
			this.label8.Text = "x";
			this.flowLayoutPanel1.SetFlowBreak(this.numx, true);
			this.numx.Location = new Point(20, 78);
			NumericUpDown arg_1A9C_0 = this.numx;
			int[] array15 = new int[4];
			array15[0] = 10000;
			arg_1A9C_0.Maximum = new decimal(array15);
			this.numx.Minimum = new decimal(new int[]
			{
				10000,
				0,
				0,
				-2147483648
			});
			this.numx.Name = "numx";
			this.numx.Size = new Size(60, 19);
			this.numx.TabIndex = 5;
			this.numx.TextAlign = HorizontalAlignment.Right;
			this.numx.ValueChanged += new EventHandler(this.numnear_ValueChanged);
			this.label9.Anchor = AnchorStyles.Left;
			this.label9.AutoSize = true;
			this.label9.Location = new Point(3, 106);
			this.label9.Name = "label9";
			this.label9.Size = new Size(11, 12);
			this.label9.TabIndex = 6;
			this.label9.Text = "y";
			this.flowLayoutPanel1.SetFlowBreak(this.numy, true);
			this.numy.Location = new Point(20, 103);
			NumericUpDown arg_1BCF_0 = this.numy;
			int[] array16 = new int[4];
			array16[0] = 10000;
			arg_1BCF_0.Maximum = new decimal(array16);
			this.numy.Minimum = new decimal(new int[]
			{
				10000,
				0,
				0,
				-2147483648
			});
			this.numy.Name = "numy";
			this.numy.Size = new Size(60, 19);
			this.numy.TabIndex = 7;
			this.numy.TextAlign = HorizontalAlignment.Right;
			this.numy.ValueChanged += new EventHandler(this.numnear_ValueChanged);
			this.label10.Anchor = AnchorStyles.Left;
			this.label10.AutoSize = true;
			this.label10.Location = new Point(3, 131);
			this.label10.Name = "label10";
			this.label10.Size = new Size(10, 12);
			this.label10.TabIndex = 8;
			this.label10.Text = "z";
			this.flowLayoutPanel1.SetFlowBreak(this.numz, true);
			this.numz.Location = new Point(19, 128);
			NumericUpDown arg_1D08_0 = this.numz;
			int[] array17 = new int[4];
			array17[0] = 10000;
			arg_1D08_0.Maximum = new decimal(array17);
			this.numz.Minimum = new decimal(new int[]
			{
				10000,
				0,
				0,
				-2147483648
			});
			this.numz.Name = "numz";
			this.numz.Size = new Size(60, 19);
			this.numz.TabIndex = 9;
			this.numz.TextAlign = HorizontalAlignment.Right;
			this.numz.Value = new decimal(new int[]
			{
				200,
				0,
				0,
				-2147483648
			});
			this.numz.ValueChanged += new EventHandler(this.numnear_ValueChanged);
			this.labelHelpKeys.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.labelHelpKeys.AutoSize = true;
			this.labelHelpKeys.BorderStyle = BorderStyle.FixedSingle;
			this.labelHelpKeys.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.labelHelpKeys.Location = new Point(6, 383);
			this.labelHelpKeys.Name = "labelHelpKeys";
			this.labelHelpKeys.Size = new Size(247, 137);
			this.labelHelpKeys.TabIndex = 3;
			this.labelHelpKeys.Text = componentResourceManager.GetString("labelHelpKeys.Text");
			this.labelHelpKeys.Visible = false;
			this.panel1.BorderStyle = BorderStyle.FixedSingle;
			this.panel1.Dock = DockStyle.Fill;
			this.panel1.Location = new Point(3, 17);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(463, 520);
			this.panel1.TabIndex = 2;
			this.panel1.UseTransparent = true;
			this.panel1.Load += new EventHandler(this.panel1_Load);
			this.panel1.Paint += new PaintEventHandler(this.panel1_Paint);
			this.panel1.MouseMove += new MouseEventHandler(this.panel1_MouseMove);
			this.panel1.MouseDown += new MouseEventHandler(this.panel1_MouseDown);
			this.panel1.Resize += new EventHandler(this.panel1_Resize);
			this.panel1.MouseUp += new MouseEventHandler(this.panel1_MouseUp);
			this.panel1.KeyDown += new KeyEventHandler(this.panel1_KeyDown);
			this.label1.AutoSize = true;
			this.label1.BorderStyle = BorderStyle.Fixed3D;
			this.label1.Dock = DockStyle.Top;
			this.label1.Location = new Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new Size(68, 14);
			this.label1.TabIndex = 1;
			this.label1.Text = "3D viewport";
			this.AllowDrop = true;
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(715, 540);
			base.Controls.Add(this.splitContainer1);
			base.Name = "FormII";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "khkh_xldM ][";
			base.Load += new EventHandler(this.Form1_Load);
			base.DragDrop += new DragEventHandler(this.FormII_DragDrop);
			base.DragEnter += new DragEventHandler(this.FormII_DragEnter);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel1.PerformLayout();
			this.splitContainer2.Panel2.ResumeLayout(false);
			this.splitContainer2.Panel2.PerformLayout();
			this.splitContainer2.ResumeLayout(false);
			((ISupportInitialize)this.numericUpDownFrame).EndInit();
			((ISupportInitialize)this.numericUpDownStep).EndInit();
			((ISupportInitialize)this.numericUpDownAutoNext).EndInit();
			((ISupportInitialize)this.numericUpDownTick).EndInit();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((ISupportInitialize)this.numnear).EndInit();
			((ISupportInitialize)this.numfar).EndInit();
			((ISupportInitialize)this.numfov).EndInit();
			((ISupportInitialize)this.numx).EndInit();
			((ISupportInitialize)this.numy).EndInit();
			((ISupportInitialize)this.numz).EndInit();
			base.ResumeLayout(false);
		}
	}
}
