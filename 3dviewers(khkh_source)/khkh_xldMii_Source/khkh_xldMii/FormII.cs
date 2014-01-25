namespace khkh_xldMii
{
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

    public class FormII : Form, ILoadf, IVwer
    {
        private Mesh[] _Sora = new Mesh[] { new Mesh(), new Mesh(), new Mesh() };
        private BCForm bcform;
        private Button button1;
        private Button buttonBC;
        private bool captureNow;
        private CheckBox checkBoxAnim;
        private CheckBox checkBoxAsPNG;
        private CheckBox checkBoxAutoFill;
        private CheckBox checkBoxAutoNext;
        private CheckBox checkBoxAutoRec;
        private CheckBox checkBoxKeepCur;
        private CheckBox checkBoxKeys;
        private CheckBox checkBoxLooks;
        private ColumnHeader columnHeaderRxxx;
        private IContainer components;
        private Point curs = Point.Empty;
        private Direct3D d3d = new Direct3D();
        private Device device;
        private int enterLock;
        private string fmdlxDropped;
        private float fzval = 5f;
        private bool hasLDn;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label labelHelpKeys;
        private ListView listView1;
        private NumericUpDown numericUpDownAutoNext;
        private NumericUpDown numericUpDownFrame;
        private NumericUpDown numericUpDownStep;
        private NumericUpDown numericUpDownTick;
        private Vector3 offset = new Vector3(-4f, -66f, 0f);
        private UC panel1;
        private Quaternion quat = new Quaternion(0f, 0f, 0f, 1f);
        private RadioButton radioButton10fps;
        private RadioButton radioButton30fps;
        private RadioButton radioButton60fps;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private float tick;
        private Timer timerTick;

        public FormII()
        {
            this._Sora[1].parent = this._Sora[0];
            this._Sora[1].iMa = 0xb2;
            this._Sora[2].parent = this._Sora[0];
            this._Sora[2].iMa = 0x56;
            this.InitializeComponent();
        }

        public void BackToViewer()
        {
            base.Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                if (item.Tag != null)
                {
                    Mt1 mt = ((MotInf) item.Tag).mt1;
                    Msetblk msetblk = new Msetblk(new MemoryStream(mt.bin, false));
                    T31 local1 = this._Sora[0].mdlx.alt31[0];
                    Mlink mlink = this._Sora[0].ol = new Mlink();
                    MemoryStream fsMdlx = new MemoryStream(this._Sora[0].binMdlx, false);
                    MemoryStream fsMset = new MemoryStream(this._Sora[0].binMset, false);
                    for (float i = 0f; i <= 300f; i++)
                    {
                        float[] numArray;
                        float[] numArray2;
                        float[] numArray3;
                        mlink.Permit_DEB(fsMdlx, msetblk.cntb1, fsMset, msetblk.cntb2, mt.off, i, out numArray, out numArray2, out numArray3);
                    }
                }
                break;
            }
        }

        private void buttonBC_Click(object sender, EventArgs e)
        {
            if ((this.bcform == null) || ((this.bcform != null) && this.bcform.IsDisposed))
            {
                this.bcform = new BCForm(this);
            }
            this.bcform.Show();
            this.bcform.Activate();
        }

        private void calcbody(CaseTris ct, Mesh M, Mt1 mt1)
        {
            Mdlxfst mdlx = M.mdlx;
            Msetfst mset = M.mset;
            List<Body1> list = M.albody1;
            Mlink ol = M.ol;
            ct.Close();
            list.Clear();
            if ((mdlx != null) && (mset != null))
            {
                Matrix[] matrixArray;
                T31 t = mdlx.alt31[0];
                if (mt1.isRaw)
                {
                    MsetRawblk rawblk = new MsetRawblk(new MemoryStream(mt1.bin, false));
                    int num = Math.Max(0, Math.Min(rawblk.cntFrames - 1, (int) Math.Floor((double) this.tick)));
                    int num2 = Math.Max(0, Math.Min(rawblk.cntFrames - 1, (int) Math.Ceiling((double) this.tick)));
                    if (num == num2)
                    {
                        MsetRM trm = rawblk.alrm[num];
                        matrixArray = M.Ma = trm.al.ToArray();
                    }
                    else
                    {
                        MsetRM trm2 = rawblk.alrm[num];
                        float num3 = this.tick % 1f;
                        MsetRM trm3 = rawblk.alrm[num2];
                        float num4 = 1f - num3;
                        matrixArray = M.Ma = new Matrix[rawblk.cntJoints];
                        for (int i = 0; i < matrixArray.Length; i++)
                        {
                            matrixArray[i] = new Matrix { M11 = (trm2.al[i].M11 * num4) + (trm3.al[i].M11 * num3), M21 = (trm2.al[i].M21 * num4) + (trm3.al[i].M21 * num3), M31 = (trm2.al[i].M31 * num4) + (trm3.al[i].M31 * num3), M41 = (trm2.al[i].M41 * num4) + (trm3.al[i].M41 * num3), M12 = (trm2.al[i].M12 * num4) + (trm3.al[i].M12 * num3), M22 = (trm2.al[i].M22 * num4) + (trm3.al[i].M22 * num3), M32 = (trm2.al[i].M32 * num4) + (trm3.al[i].M32 * num3), M42 = (trm2.al[i].M42 * num4) + (trm3.al[i].M42 * num3), M13 = (trm2.al[i].M13 * num4) + (trm3.al[i].M13 * num3), M23 = (trm2.al[i].M23 * num4) + (trm3.al[i].M23 * num3), M33 = (trm2.al[i].M33 * num4) + (trm3.al[i].M33 * num3), M43 = (trm2.al[i].M43 * num4) + (trm3.al[i].M43 * num3), M14 = (trm2.al[i].M14 * num4) + (trm3.al[i].M14 * num3), M24 = (trm2.al[i].M24 * num4) + (trm3.al[i].M24 * num3), M34 = (trm2.al[i].M34 * num4) + (trm3.al[i].M34 * num3), M44 = (trm2.al[i].M44 * num4) + (trm3.al[i].M44 * num3) };
                        }
                    }
                }
                else
                {
                    Msetblk msetblk = new Msetblk(new MemoryStream(mt1.bin, false));
                    MemoryStream os = new MemoryStream();
                    if (ol == null)
                    {
                        ol = M.ol = new Mlink();
                    }
                    ol.Permit(new MemoryStream(M.binMdlx, false), msetblk.cntb1, new MemoryStream(M.binMset, false), msetblk.cntb2, mt1.off, this.tick, os);
                    BinaryReader reader = new BinaryReader(os);
                    os.Position = 0L;
                    matrixArray = M.Ma = new Matrix[msetblk.cntb1];
                    for (int j = 0; j < msetblk.cntb1; j++)
                    {
                        matrixArray[j] = new Matrix { M11 = reader.ReadSingle(), M12 = reader.ReadSingle(), M13 = reader.ReadSingle(), M14 = reader.ReadSingle(), M21 = reader.ReadSingle(), M22 = reader.ReadSingle(), M23 = reader.ReadSingle(), M24 = reader.ReadSingle(), M31 = reader.ReadSingle(), M32 = reader.ReadSingle(), M33 = reader.ReadSingle(), M34 = reader.ReadSingle(), M41 = reader.ReadSingle(), M42 = reader.ReadSingle(), M43 = reader.ReadSingle(), M44 = reader.ReadSingle() };
                    }
                }
                Matrix identity = Matrix.Identity;
                if ((M.parent != null) && (M.iMa != -1))
                {
                    identity = M.parent.Ma[M.iMa];
                }
                foreach (T13vif tvif in t.al13)
                {
                    int tops = 0x220;
                    int num8 = 0;
                    VU1Mem mem = new VU1Mem();
                    new ParseVIF1(mem).Parse(new MemoryStream(tvif.bin, false), tops);
                    Body1 item = SimaVU1.Sima(mem, matrixArray, tops, num8, tvif.texi, tvif.alaxi, identity);
                    list.Add(item);
                }
                List<uint> list2 = new List<uint>();
                List<Sepa> list3 = new List<Sepa>();
                int startVertexIndex = 0;
                int sel = 0;
                uint[] numArray = new uint[4];
                int index = 0;
                int tick = (int) this.tick;
                int[] numArray2 = new int[] { 1, 2, 3 };
                foreach (Body1 body2 in list)
                {
                    int cntPrimitives = 0;
                    for (int k = 0; k < body2.alvi.Length; k++)
                    {
                        numArray[index] = (uint) ((body2.alvi[k] | (sel << 12)) | (k << 0x18));
                        index = (index + 1) & 3;
                        if (body2.alfl[k] == 0x20)
                        {
                            list2.Add(numArray[(index - numArray2[(tick * 0x67) % 3]) & 3]);
                            list2.Add(numArray[(index - numArray2[(1 + (tick * 0x67)) % 3]) & 3]);
                            list2.Add(numArray[(index - numArray2[(2 + (tick * 0x67)) % 3]) & 3]);
                            cntPrimitives++;
                        }
                        else if (body2.alfl[k] == 0x30)
                        {
                            list2.Add(numArray[(index - numArray2[(tick << 1) % 3]) & 3]);
                            list2.Add(numArray[(index - numArray2[(2 + (tick << 1)) % 3]) & 3]);
                            list2.Add(numArray[(index - numArray2[(1 + (tick << 1)) % 3]) & 3]);
                            cntPrimitives++;
                        }
                    }
                    list3.Add(new Sepa(startVertexIndex, cntPrimitives, body2.t, sel));
                    startVertexIndex += 3 * cntPrimitives;
                    sel++;
                }
                ct.alsepa = list3.ToArray();
                ct.cntVert = list2.Count;
                ct.cntPrimitives = 0;
                if (ct.cntVert != 0)
                {
                    ct.vb = new VertexBuffer(this.device, 0x24 * ct.cntVert, Usage.None, ct.vf = VertexFormat.Texture3 | VertexFormat.Position, Pool.Managed);
                    DataStream stream2 = ct.vb.Lock(0, 0, LockFlags.None);
                    try
                    {
                        int count = list2.Count;
                        for (int m = 0; m < count; m++)
                        {
                            uint num17 = list2[m];
                            uint num18 = num17 & 0xfff;
                            uint num19 = (num17 >> 12) & 0xfff;
                            uint num20 = (num17 >> 0x18) & 0xfff;
                            Body1 body3 = list[(int) num19];
                            PTex3 tex = new PTex3(body3.alvert[num18], new Vector2(body3.aluv[num20].X, body3.aluv[num20].Y));
                            stream2.Write<PTex3>(tex);
                        }
                        stream2.Position = 0L;
                    }
                    finally
                    {
                        ct.vb.Unlock();
                    }
                }
            }
        }

        private void calcPattex(Mesh M, float tick)
        {
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                MotInf tag = (MotInf) item.Tag;
                M.pts = SelTexfacUt.Sel(M.timf.alp, tick, tag.mt1.fm);
                break;
            }
        }

        private void checkBoxAnim_CheckedChanged(object sender, EventArgs e)
        {
            this.timerTick.Enabled = this.checkBoxAnim.Checked;
        }

        private void checkBoxAutoFill_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxAutoFill.Checked)
            {
                foreach (ListViewItem item in this.listView1.SelectedItems)
                {
                    this.numericUpDownAutoNext.Value = ((decimal) ((MotInf) item.Tag).maxtick) + this.numericUpDownStep.Value;
                    break;
                }
            }
        }

        private void checkBoxKeys_CheckedChanged(object sender, EventArgs e)
        {
            this.labelHelpKeys.Visible = this.checkBoxKeys.Checked;
        }

        private void checkBoxLooks_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Invalidate();
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void DoRecalc()
        {
            this.recalc();
            this.panel1.Invalidate();
        }

        private void doReshape()
        {
            this.reshape();
            this.panel1.Invalidate();
        }

        private void fl1_DragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void fl1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = ((e.Data != null) && e.Data.GetDataPresent(DataFormats.FileDrop)) ? DragDropEffects.Copy : e.Effect;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.listView1.Items.Add("(Drop your .mdlx file to window)");
            string[] commandLineArgs = Environment.GetCommandLineArgs();
            for (int i = 1; i < commandLineArgs.Length; i++)
            {
                string path = commandLineArgs[i];
                if (File.Exists(path) && Path.GetExtension(path).ToLower().Equals(".mdlx"))
                {
                    this.loadMdlx(path, 0);
                    this.loadMset(MatchUt.findMset(path), 0);
                }
            }
            this.radioButtonAny_CheckedChanged(null, null);
        }

        private void FormII_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (data != null)
            {
                foreach (string str in data)
                {
                    if (Path.GetExtension(str).ToLower().Equals(".mdlx"))
                    {
                        this.fmdlxDropped = str;
                        Timer timer = new Timer();
                        timer.Tick += new EventHandler(this.t_Tick);
                        timer.Start();
                        return;
                    }
                }
            }
        }

        private void FormII_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FormII));
            this.timerTick = new Timer(this.components);
            this.splitContainer1 = new SplitContainer();
            this.splitContainer2 = new SplitContainer();
            this.listView1 = new ListView();
            this.columnHeaderRxxx = new ColumnHeader();
            this.label2 = new Label();
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
            this.labelHelpKeys = new Label();
            this.panel1 = new UC();
            this.label1 = new Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.numericUpDownFrame.BeginInit();
            this.numericUpDownStep.BeginInit();
            this.numericUpDownAutoNext.BeginInit();
            this.numericUpDownTick.BeginInit();
            base.SuspendLayout();
            this.timerTick.Interval = 0x10;
            this.timerTick.Tick += new EventHandler(this.timerTick_Tick);
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.FixedPanel = FixedPanel.Panel1;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Padding = new Padding(3);
            this.splitContainer1.Panel2.Controls.Add(this.labelHelpKeys);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Padding = new Padding(3);
            this.splitContainer1.Size = new Size(0x2cb, 540);
            this.splitContainer1.SplitterDistance = 0xf2;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer2.Dock = DockStyle.Fill;
            this.splitContainer2.FixedPanel = FixedPanel.Panel2;
            this.splitContainer2.Location = new Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = Orientation.Horizontal;
            this.splitContainer2.Panel1.Controls.Add(this.listView1);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
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
            this.splitContainer2.Size = new Size(0xec, 0x216);
            this.splitContainer2.SplitterDistance = 0xbf;
            this.splitContainer2.TabIndex = 0;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeaderRxxx });
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new Point(0, 14);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(0xec, 0xb1);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
            this.columnHeaderRxxx.Text = "Motion";
            this.columnHeaderRxxx.Width = 0xbc;
            this.label2.AutoSize = true;
            this.label2.BorderStyle = BorderStyle.Fixed3D;
            this.label2.Dock = DockStyle.Top;
            this.label2.Location = new Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x21, 14);
            this.label2.TabIndex = 1;
            this.label2.Text = "Anim";
            this.checkBoxKeepCur.AutoSize = true;
            this.checkBoxKeepCur.FlatStyle = FlatStyle.Flat;
            this.checkBoxKeepCur.Location = new Point(5, 0x7b);
            this.checkBoxKeepCur.Name = "checkBoxKeepCur";
            this.checkBoxKeepCur.Size = new Size(0x7b, 0x10);
            this.checkBoxKeepCur.TabIndex = 9;
            this.checkBoxKeepCur.Text = "&Loop current motion";
            this.checkBoxKeepCur.UseVisualStyleBackColor = true;
            this.checkBoxAsPNG.AutoSize = true;
            this.checkBoxAsPNG.FlatStyle = FlatStyle.Flat;
            this.checkBoxAsPNG.Location = new Point(5, 0xda);
            this.checkBoxAsPNG.Name = "checkBoxAsPNG";
            this.checkBoxAsPNG.Size = new Size(100, 0x10);
            this.checkBoxAsPNG.TabIndex = 14;
            this.checkBoxAsPNG.Text = "Use &png format";
            this.checkBoxAsPNG.UseVisualStyleBackColor = true;
            this.radioButton60fps.Appearance = Appearance.Button;
            this.radioButton60fps.AutoSize = true;
            this.radioButton60fps.Checked = true;
            this.radioButton60fps.FlatStyle = FlatStyle.Flat;
            this.radioButton60fps.Location = new Point(0xac, 0x108);
            this.radioButton60fps.Name = "radioButton60fps";
            this.radioButton60fps.Size = new Size(0x2d, 0x18);
            this.radioButton60fps.TabIndex = 0x12;
            this.radioButton60fps.TabStop = true;
            this.radioButton60fps.Text = "&60fps";
            this.radioButton60fps.UseVisualStyleBackColor = true;
            this.radioButton60fps.CheckedChanged += new EventHandler(this.radioButtonAny_CheckedChanged);
            this.radioButton30fps.Appearance = Appearance.Button;
            this.radioButton30fps.AutoSize = true;
            this.radioButton30fps.FlatStyle = FlatStyle.Flat;
            this.radioButton30fps.Location = new Point(0x79, 0x108);
            this.radioButton30fps.Name = "radioButton30fps";
            this.radioButton30fps.Size = new Size(0x2d, 0x18);
            this.radioButton30fps.TabIndex = 0x11;
            this.radioButton30fps.Text = "&30fps";
            this.radioButton30fps.UseVisualStyleBackColor = true;
            this.radioButton30fps.CheckedChanged += new EventHandler(this.radioButtonAny_CheckedChanged);
            this.radioButton10fps.Appearance = Appearance.Button;
            this.radioButton10fps.AutoSize = true;
            this.radioButton10fps.FlatStyle = FlatStyle.Flat;
            this.radioButton10fps.Location = new Point(70, 0x108);
            this.radioButton10fps.Name = "radioButton10fps";
            this.radioButton10fps.Size = new Size(0x2d, 0x18);
            this.radioButton10fps.TabIndex = 0x10;
            this.radioButton10fps.Text = "&10fps";
            this.radioButton10fps.UseVisualStyleBackColor = true;
            this.radioButton10fps.CheckedChanged += new EventHandler(this.radioButtonAny_CheckedChanged);
            this.checkBoxLooks.AutoSize = true;
            this.checkBoxLooks.FlatStyle = FlatStyle.Flat;
            this.checkBoxLooks.Location = new Point(5, 240);
            this.checkBoxLooks.Name = "checkBoxLooks";
            this.checkBoxLooks.Size = new Size(0x98, 0x10);
            this.checkBoxLooks.TabIndex = 15;
            this.checkBoxLooks.Text = "&Enable face looks change";
            this.checkBoxLooks.UseVisualStyleBackColor = true;
            this.checkBoxLooks.CheckedChanged += new EventHandler(this.checkBoxLooks_CheckedChanged);
            this.buttonBC.FlatStyle = FlatStyle.Flat;
            this.buttonBC.Location = new Point(5, 0x128);
            this.buttonBC.Name = "buttonBC";
            this.buttonBC.Size = new Size(0xd4, 0x22);
            this.buttonBC.TabIndex = 0x13;
            this.buttonBC.Text = "&Bind Controller";
            this.buttonBC.UseVisualStyleBackColor = true;
            this.buttonBC.Click += new EventHandler(this.buttonBC_Click);
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.Location = new Point(0x97, 3);
            this.button1.Name = "button1";
            this.button1.Size = new Size(70, 0x17);
            this.button1.TabIndex = 1;
            this.button1.Text = "&DEB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.checkBoxKeys.AutoSize = true;
            this.checkBoxKeys.FlatStyle = FlatStyle.Flat;
            this.checkBoxKeys.Location = new Point(5, 0x93);
            this.checkBoxKeys.Name = "checkBoxKeys";
            this.checkBoxKeys.Size = new Size(0x7e, 0x10);
            this.checkBoxKeys.TabIndex = 10;
            this.checkBoxKeys.Text = "Show short cut &keys";
            this.checkBoxKeys.UseVisualStyleBackColor = true;
            this.checkBoxKeys.CheckedChanged += new EventHandler(this.checkBoxKeys_CheckedChanged);
            this.label4.AutoSize = true;
            this.label4.Location = new Point(0x31, 0xc3);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x4b, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "Next &file no ...";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(3, 0x1d);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x2e, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "&Cur tick";
            this.checkBoxAutoFill.AutoSize = true;
            this.checkBoxAutoFill.FlatStyle = FlatStyle.Flat;
            this.checkBoxAutoFill.Location = new Point(5, 0x65);
            this.checkBoxAutoFill.Name = "checkBoxAutoFill";
            this.checkBoxAutoFill.Size = new Size(110, 0x10);
            this.checkBoxAutoFill.TabIndex = 8;
            this.checkBoxAutoFill.Text = "Auto fill max &tick";
            this.checkBoxAutoFill.UseVisualStyleBackColor = true;
            this.checkBoxAutoFill.CheckedChanged += new EventHandler(this.checkBoxAutoFill_CheckedChanged);
            this.checkBoxAutoRec.AutoSize = true;
            this.checkBoxAutoRec.FlatStyle = FlatStyle.Flat;
            this.checkBoxAutoRec.Location = new Point(5, 0xa9);
            this.checkBoxAutoRec.Name = "checkBoxAutoRec";
            this.checkBoxAutoRec.Size = new Size(0xb2, 0x10);
            this.checkBoxAutoRec.TabIndex = 11;
            this.checkBoxAutoRec.Text = "Capture &screen shot per frame";
            this.checkBoxAutoRec.UseVisualStyleBackColor = true;
            this.numericUpDownFrame.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.numericUpDownFrame.Location = new Point(0x93, 0xbf);
            int[] bits = new int[4];
            bits[0] = 0x1869f;
            this.numericUpDownFrame.Maximum = new decimal(bits);
            this.numericUpDownFrame.Name = "numericUpDownFrame";
            this.numericUpDownFrame.Size = new Size(70, 0x15);
            this.numericUpDownFrame.TabIndex = 13;
            this.numericUpDownFrame.TextAlign = HorizontalAlignment.Right;
            int[] numArray2 = new int[4];
            numArray2[0] = 1;
            this.numericUpDownFrame.Value = new decimal(numArray2);
            this.numericUpDownStep.DecimalPlaces = 2;
            this.numericUpDownStep.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.numericUpDownStep.Location = new Point(0x97, 0x31);
            int[] numArray3 = new int[4];
            numArray3[0] = 0x3e8;
            this.numericUpDownStep.Maximum = new decimal(numArray3);
            this.numericUpDownStep.Name = "numericUpDownStep";
            this.numericUpDownStep.Size = new Size(70, 0x15);
            this.numericUpDownStep.TabIndex = 5;
            this.numericUpDownStep.TextAlign = HorizontalAlignment.Right;
            int[] numArray4 = new int[4];
            numArray4[0] = 1;
            this.numericUpDownStep.Value = new decimal(numArray4);
            this.numericUpDownAutoNext.DecimalPlaces = 2;
            this.numericUpDownAutoNext.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.numericUpDownAutoNext.Location = new Point(0x97, 0x4c);
            int[] numArray5 = new int[4];
            numArray5[0] = 0x270f;
            this.numericUpDownAutoNext.Maximum = new decimal(numArray5);
            this.numericUpDownAutoNext.Name = "numericUpDownAutoNext";
            this.numericUpDownAutoNext.Size = new Size(70, 0x15);
            this.numericUpDownAutoNext.TabIndex = 7;
            this.numericUpDownAutoNext.TextAlign = HorizontalAlignment.Right;
            int[] numArray6 = new int[4];
            numArray6[0] = 100;
            this.numericUpDownAutoNext.Value = new decimal(numArray6);
            this.checkBoxAutoNext.AutoSize = true;
            this.checkBoxAutoNext.FlatStyle = FlatStyle.Flat;
            this.checkBoxAutoNext.Location = new Point(5, 0x4f);
            this.checkBoxAutoNext.Name = "checkBoxAutoNext";
            this.checkBoxAutoNext.Size = new Size(0x84, 0x10);
            this.checkBoxAutoNext.TabIndex = 6;
            this.checkBoxAutoNext.Text = "&Next motion on tick ...";
            this.checkBoxAutoNext.UseVisualStyleBackColor = true;
            this.checkBoxAnim.AutoSize = true;
            this.checkBoxAnim.FlatStyle = FlatStyle.Flat;
            this.checkBoxAnim.Location = new Point(0x37, 0x34);
            this.checkBoxAnim.Name = "checkBoxAnim";
            this.checkBoxAnim.Size = new Size(0x57, 0x10);
            this.checkBoxAnim.TabIndex = 4;
            this.checkBoxAnim.Text = "&Auto step by";
            this.checkBoxAnim.UseVisualStyleBackColor = true;
            this.checkBoxAnim.CheckedChanged += new EventHandler(this.checkBoxAnim_CheckedChanged);
            this.numericUpDownTick.DecimalPlaces = 2;
            this.numericUpDownTick.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.numericUpDownTick.Location = new Point(0x37, 0x19);
            int[] numArray7 = new int[4];
            numArray7[0] = 0x2710;
            this.numericUpDownTick.Maximum = new decimal(numArray7);
            this.numericUpDownTick.Name = "numericUpDownTick";
            this.numericUpDownTick.Size = new Size(0x4c, 0x15);
            this.numericUpDownTick.TabIndex = 3;
            this.numericUpDownTick.TextAlign = HorizontalAlignment.Right;
            this.numericUpDownTick.ValueChanged += new EventHandler(this.numericUpDownTick_ValueChanged);
            this.label3.AutoSize = true;
            this.label3.BorderStyle = BorderStyle.Fixed3D;
            this.label3.Location = new Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x2c, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Control";
            this.label3.DoubleClick += new EventHandler(this.label3_DoubleClick);
            this.labelHelpKeys.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.labelHelpKeys.AutoSize = true;
            this.labelHelpKeys.BorderStyle = BorderStyle.FixedSingle;
            this.labelHelpKeys.Font = new System.Drawing.Font("Arial", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.labelHelpKeys.Location = new Point(6, 0x17f);
            this.labelHelpKeys.Name = "labelHelpKeys";
            this.labelHelpKeys.Size = new Size(0xf7, 0x89);
            this.labelHelpKeys.TabIndex = 3;
            this.labelHelpKeys.Text = manager.GetString("labelHelpKeys.Text");
            this.labelHelpKeys.Visible = false;
            this.panel1.BorderStyle = BorderStyle.FixedSingle;
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(3, 0x11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x1cf, 520);
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
            this.label1.Size = new Size(0x44, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "3D viewport";
            this.AllowDrop = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2cb, 540);
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
            this.numericUpDownFrame.EndInit();
            this.numericUpDownStep.EndInit();
            this.numericUpDownAutoNext.EndInit();
            this.numericUpDownTick.EndInit();
            base.ResumeLayout(false);
        }

        private void label3_DoubleClick(object sender, EventArgs e)
        {
            this.button1.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (IEnumerator enumerator = this.listView1.SelectedIndices.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    int current = (int) enumerator.Current;
                    this.numericUpDownTick.Value = 0M;
                    this.checkBoxAutoFill_CheckedChanged(null, null);
                    this.recalc();
                    return;
                }
            }
        }

        private void loadMdlx(string fmdlx, int ty)
        {
            if (ty == 0)
            {
                this.listView1.Items.Clear();
                this.listView1.Items.Add("(Drop your .mdlx file to window)");
            }
            Mesh mesh = this._Sora[ty];
            mesh.DisposeMdlx();
            using (FileStream stream = File.OpenRead(fmdlx))
            {
                foreach (ReadBar.Barent barent in ReadBar.Explode(stream))
                {
                    switch (barent.k)
                    {
                        case 4:
                            mesh.mdlx = new Mdlxfst(new MemoryStream(barent.bin, false));
                            break;

                        case 7:
                            mesh.timc = TIMc.Load(new MemoryStream(barent.bin, false));
                            mesh.timf = (mesh.timc.Length >= 1) ? mesh.timc[0] : null;
                            break;
                    }
                }
            }
            mesh.binMdlx = File.ReadAllBytes(fmdlx);
            mesh.ol = null;
            this.reloadTex(ty);
        }

        private void loadMset(string fmset, int ty)
        {
            Mesh mesh = this._Sora[ty];
            mesh.DisposeMset();
            if (File.Exists(fmset))
            {
                using (FileStream stream = File.OpenRead(fmset))
                {
                    mesh.mset = new Msetfst(stream, Path.GetFileName(fmset));
                }
                if (ty == 0)
                {
                    this.listView1.Items.Clear();
                    foreach (Mt1 mt in mesh.mset.al1)
                    {
                        ListViewItem item = this.listView1.Items.Add(mt.id);
                        MotInf inf = new MotInf {
                            mt1 = mt
                        };
                        if (mt.isRaw)
                        {
                            MsetRawblk rawblk = new MsetRawblk(new MemoryStream(mt.bin, false));
                            inf.maxtick = rawblk.cntFrames;
                            inf.mintick = 0f;
                        }
                        else
                        {
                            Msetblk msetblk = new Msetblk(new MemoryStream(mt.bin, false));
                            inf.maxtick = (msetblk.to.al11.Length != 0) ? msetblk.to.al11[msetblk.to.al11.Length - 1] : 0f;
                            inf.mintick = (msetblk.to.al11.Length != 0) ? msetblk.to.al11[0] : 0f;
                        }
                        item.Tag = inf;
                    }
                    this.listView1.Sorting = SortOrder.Ascending;
                    this.listView1.Sort();
                }
                mesh.binMset = File.ReadAllBytes(fmset);
            }
            mesh.ol = null;
        }

        public void LoadOf(int x, string fp)
        {
            using (new WC())
            {
                switch (x)
                {
                    case 0:
                        this.loadMdlx(fp, 0);
                        return;

                    case 1:
                        this.loadMset(fp, 0);
                        return;

                    case 2:
                        this.loadMdlx(fp, 1);
                        return;

                    case 3:
                        this.loadMset(fp, 1);
                        return;

                    case 4:
                        this.loadMdlx(fp, 2);
                        return;

                    case 5:
                        this.loadMset(fp, 2);
                        return;
                }
                throw new NotSupportedException();
            }
        }

        private void numericUpDownTick_ValueChanged(object sender, EventArgs e)
        {
            this.tick = (float) this.numericUpDownTick.Value;
            this.recalc();
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

        private void panel1_Load(object sender, EventArgs e)
        {
            this.device = new Device(this.d3d, 0, DeviceType.Hardware, this.panel1.Handle, CreateFlags.SoftwareVertexProcessing, new PresentParameters[] { this.PP });
            this.devReset();
            this.reshape();
            this.panel1.MouseWheel += new MouseEventHandler(this.panel1_MouseWheel);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != MouseButtons.None)
            {
                this.curs = e.Location;
                this.hasLDn = true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (((e.Button & MouseButtons.Left) != MouseButtons.None) && this.hasLDn)
            {
                int num = e.X - this.curs.X;
                int num2 = e.Y - this.curs.Y;
                if ((num != 0) || (num2 != 0))
                {
                    this.curs = e.Location;
                    if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
                    {
                        this.quat *= Quaternion.RotationYawPitchRoll(((float) num) / 100f, ((float) num2) / 100f, 0f);
                    }
                    else
                    {
                        this.offset += new Vector3((float) num, (float) -num2, 0f);
                    }
                    this.doReshape();
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (((e.Button & MouseButtons.Left) != MouseButtons.None) && this.hasLDn)
            {
                this.hasLDn = false;
            }
        }

        private void panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            this.fzval = Math.Max(1f, Math.Min((float) 100f, (float) (this.fzval + (e.Delta / 120))));
            this.doReshape();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if ((this.device != null) && !this.device.TestCooperativeLevel().IsFailure)
            {
                bool flag = false;
                bool flag2 = true;
                bool flag3 = this.checkBoxLooks.Checked;
                this.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, this.panel1.BackColor, 1f, 0);
                this.device.BeginScene();
                this.device.SetRenderState<FillMode>(RenderState.FillMode, flag ? FillMode.Wireframe : FillMode.Solid);
                foreach (Mesh mesh in this._Sora)
                {
                    CaseTris ctb = mesh.ctb;
                    if ((ctb != null) && (ctb.vb != null))
                    {
                        this.device.SetStreamSource(0, ctb.vb, 0, 0x24);
                        this.device.VertexFormat = ctb.vf;
                        this.device.Indices = null;
                        foreach (Sepa sepa in ctb.alsepa)
                        {
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
                                if ((flag3 && (mesh.pts.Length >= 1)) && ((mesh.pts[0] != null) && (sepa.t == mesh.pts[0].texi)))
                                {
                                    list.Add(mesh.altex1[mesh.pts[0].pati]);
                                }
                                if ((flag3 && (mesh.pts.Length >= 2)) && ((mesh.pts[1] != null) && (sepa.t == mesh.pts[1].texi)))
                                {
                                    list.Add(mesh.altex2[mesh.pts[1].pati]);
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
                    using (Surface surface = this.device.GetBackBuffer(0, 0))
                    {
                        int num = (int) this.numericUpDownFrame.Value;
                        if (this.checkBoxAsPNG.Checked)
                        {
                            Surface.ToFile(surface, "_cap" + num.ToString("00000") + ".png", ImageFileFormat.Png);
                        }
                        else
                        {
                            Surface.ToFile(surface, "_cap" + num.ToString("00000") + ".jpg", ImageFileFormat.Jpg);
                        }
                        this.numericUpDownFrame.Value = decimal.op_Increment(this.numericUpDownFrame.Value);
                    }
                }
                this.device.Present();
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

        private void radioButtonAny_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radioButton10fps.Checked)
            {
                this.timerTick.Interval = 100;
            }
            else if (this.radioButton30fps.Checked)
            {
                this.timerTick.Interval = 0x21;
            }
            else if (this.radioButton60fps.Checked)
            {
                this.timerTick.Interval = 0x10;
            }
        }

        private void recalc()
        {
            foreach (Mesh mesh in this._Sora)
            {
                mesh.ctb.Close();
            }
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                if (item.Tag != null)
                {
                    this.calcbody(this._Sora[0].ctb, this._Sora[0], ((MotInf) item.Tag).mt1);
                    if (this._Sora[1].Present)
                    {
                        Mt1 mt = UtwexMotionSel.Sel(((MotInf) item.Tag).mt1.k1, this._Sora[1].mset.al1);
                        if (((mt != null) && (this._Sora[1].iMa != -1)) && this._Sora[1].Present)
                        {
                            this.calcbody(this._Sora[1].ctb, this._Sora[1], mt);
                        }
                    }
                    if (this._Sora[2].Present)
                    {
                        Mt1 mt2 = UtwexMotionSel.Sel(((MotInf) item.Tag).mt1.k1, this._Sora[2].mset.al1);
                        if (((mt2 != null) && (this._Sora[2].iMa != -1)) && this._Sora[2].Present)
                        {
                            this.calcbody(this._Sora[2].ctb, this._Sora[2], mt2);
                        }
                    }
                    this.calcPattex(this._Sora[0], (float) this.numericUpDownTick.Value);
                }
                break;
            }
            this.panel1.Invalidate();
        }

        private void reloadTex(int ty)
        {
            if (this.device != null)
            {
                int num = 0;
                foreach (Mesh mesh in this._Sora)
                {
                    if ((num == ty) || (ty == -1))
                    {
                        mesh.altex.Clear();
                        mesh.altex1.Clear();
                        mesh.altex2.Clear();
                        if (mesh.timf != null)
                        {
                            foreach (STim tim in mesh.timf.alt)
                            {
                                mesh.altex.Add(TUt.FromBitmap(this.device, tim.pic));
                            }
                            if (num == 0)
                            {
                                for (int i = 0; i < 2; i++)
                                {
                                    int pi = 0;
                                    while (true)
                                    {
                                        Bitmap pattex = mesh.timf.GetPattex(i, pi);
                                        if (pattex == null)
                                        {
                                            break;
                                        }
                                        switch (i)
                                        {
                                            case 0:
                                                mesh.altex1.Add(TUt.FromBitmap(this.device, pattex));
                                                break;

                                            case 1:
                                                mesh.altex2.Add(TUt.FromBitmap(this.device, pattex));
                                                break;
                                        }
                                        pi++;
                                    }
                                }
                            }
                        }
                    }
                    num++;
                }
            }
        }

        private void reshape()
        {
            int width = this.panel1.Width;
            int height = this.panel1.Height;
            float num3 = (width > height) ? (((float) width) / ((float) height)) : 1f;
            float num4 = (width < height) ? (((float) height) / ((float) width)) : 1f;
            float num5 = (this.fzval * 0.5f) * 100f;
            this.device.SetTransform(TransformState.Projection, Matrix.OrthoLH(num3 * num5, num4 * num5, num5 * 10f, num5 * -10f));
            Matrix matrix = Matrix.RotationQuaternion(this.quat);
            matrix.M41 += this.offset.X;
            matrix.M42 += this.offset.Y;
            matrix.M43 += this.offset.Z;
            this.device.SetTransform(TransformState.View, matrix);
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
            }
            throw new NotSupportedException();
        }

        private void t_Tick(object sender, EventArgs e)
        {
            ((Timer) sender).Dispose();
            string fmdlxDropped = this.fmdlxDropped;
            this.loadMdlx(fmdlxDropped, 0);
            this.loadMset(MatchUt.findMset(fmdlxDropped), 0);
            this.recalc();
        }

        private void timerTick_Tick(object sender, EventArgs e)
        {
            try
            {
                if (this.enterLock++ == 0)
                {
                    try
                    {
                        this.tick = (float) (this.numericUpDownTick.Value + this.numericUpDownStep.Value);
                        if (this.checkBoxAutoNext.Checked && (((float) this.numericUpDownAutoNext.Value) <= this.tick))
                        {
                            if (this.checkBoxKeepCur.Checked)
                            {
                                this.tick = 1f;
                            }
                            else
                            {
                                foreach (int num in this.listView1.SelectedIndices)
                                {
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
                                    break;
                                }
                            }
                        }
                        if (this.checkBoxAutoRec.Checked)
                        {
                            this.captureNow = true;
                        }
                        this.numericUpDownTick.Value = (decimal) this.tick;
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

        private PresentParameters PP
        {
            get
            {
                return new PresentParameters { Windowed = true, SwapEffect = SwapEffect.Discard, EnableAutoDepthStencil = true, AutoDepthStencilFormat = SlimDX.Direct3D9.Format.D24X8, BackBufferWidth = this.panel1.ClientSize.Width, BackBufferHeight = this.panel1.ClientSize.Height };
            }
        }

        private class CaseTris : IDisposable
        {
            public FormII.Sepa[] alsepa;
            public int cntPrimitives;
            public int cntVert;
            public VertexBuffer vb;
            public VertexFormat vf;

            public void Close()
            {
                if (this.vb != null)
                {
                    this.vb.Dispose();
                }
                this.vb = null;
                this.vf = VertexFormat.None;
                this.cntPrimitives = 0;
                this.cntVert = 0;
            }

            public void Dispose()
            {
                this.Close();
            }
        }

        private class MatchUt
        {
            public static string findMset(string fmdlx)
            {
                string path = fmdlx.Substring(0, fmdlx.Length - 5) + ".mset";
                if (!File.Exists(path))
                {
                    string str2 = Regex.Replace(path, @"_[a-z]+\.", ".", RegexOptions.IgnoreCase);
                    if (File.Exists(str2))
                    {
                        return str2;
                    }
                    string str3 = Regex.Replace(path, @"_[a-z]+(_[a-z]+\.)", "$1", RegexOptions.IgnoreCase);
                    if (File.Exists(str3))
                    {
                        return str3;
                    }
                    string str4 = Regex.Replace(path, @"_[a-z]+_[a-z]+\.", ".", RegexOptions.IgnoreCase);
                    if (File.Exists(str4))
                    {
                        return str4;
                    }
                }
                return path;
            }
        }

        private class Mesh : IDisposable
        {
            public List<Body1> albody1 = new List<Body1>();
            public List<Texture> altex = new List<Texture>();
            public List<Texture> altex1 = new List<Texture>();
            public List<Texture> altex2 = new List<Texture>();
            public byte[] binMdlx;
            public byte[] binMset;
            public FormII.CaseTris ctb = new FormII.CaseTris();
            public int iMa = -1;
            public Matrix[] Ma;
            public Mdlxfst mdlx;
            public Msetfst mset;
            public Mlink ol;
            public FormII.Mesh parent;
            public FormII.PatTexSel[] pts = new FormII.PatTexSel[0];
            public Texex2[] timc;
            public Texex2 timf;

            public void Dispose()
            {
                this.DisposeMdlx();
                this.DisposeMset();
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

            public void DisposeMset()
            {
                this.mset = null;
                this.binMset = null;
                this.ol = null;
            }

            public bool Present
            {
                get
                {
                    return ((this.mdlx != null) && (this.mset != null));
                }
            }
        }

        private class MotInf
        {
            public bool isRaw;
            public float maxtick;
            public float mintick;
            public Mt1 mt1;
        }

        private class PatTexSel
        {
            public byte pati;
            public byte texi;

            public PatTexSel(byte texi, byte pati)
            {
                this.texi = texi;
                this.pati = pati;
            }
        }

        private class SelTexfacUt
        {
            public static FormII.PatTexSel[] Sel(List<Patc> alp, float tick, FacMod fm)
            {
                FormII.PatTexSel[] selArray = new FormII.PatTexSel[alp.Count];
                foreach (Fac1 fac in fm.alf1)
                {
                    if (((fac.v2 != -1) && (fac.v0 <= tick)) && (tick < fac.v2))
                    {
                        for (int i = 0; i < alp.Count; i++)
                        {
                            int num2 = ((int) (tick - fac.v0)) / 8;
                            foreach (Texfac texfac in alp[i].altf)
                            {
                                if (texfac.i0 == fac.v6)
                                {
                                    if ((num2 <= 0) && (selArray[i] == null))
                                    {
                                        selArray[i] = new FormII.PatTexSel((byte) alp[i].texi, (byte) texfac.v6);
                                        break;
                                    }
                                    num2 -= texfac.v2;
                                }
                            }
                        }
                    }
                }
                return selArray;
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
                this.svi = startVertexIndex;
                this.cnt = cntPrimitives;
                this.t = ti;
                this.sel = sel;
            }
        }

        private class TUt
        {
            public static Texture FromBitmap(Device device, Bitmap p)
            {
                MemoryStream stream = new MemoryStream();
                p.Save(stream, ImageFormat.Png);
                stream.Position = 0L;
                return Texture.FromStream(device, stream);
            }
        }

        private class UtwexMotionSel
        {
            public static Mt1 Sel(int k1, List<Mt1> al1)
            {
                foreach (Mt1 mt in al1)
                {
                    if (mt.k1 == k1)
                    {
                        return mt;
                    }
                }
                return null;
            }
        }
    }
}

