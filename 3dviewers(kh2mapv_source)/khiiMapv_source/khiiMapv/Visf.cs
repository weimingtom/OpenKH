namespace khiiMapv
{
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
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Windows.Forms;

    public class Visf : Form
    {
        private List<CI[]> alalci;
        private List<Ball> alBall;
        private List<CI> alci;
        private List<DC> aldc;
        private List<ComObject> alDeleter;
        private List<RIB> alib;
        private List<Putfragment> alpf;
        private List<Texture> altex;
        private CheckBox cbFog;
        private CheckBox cbVertexColor;
        private int cntVerts;
        private CollReader coll;
        private IContainer components;
        private Point curs;
        private Device device;
        private NumericUpDown eyeX;
        private NumericUpDown eyeY;
        private NumericUpDown eyeZ;
        private Point firstcur;
        private FlowLayoutPanel flppos;
        private NumericUpDown fov;
        private Keyrun kr;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private ToolStripLabel lCntTris;
        private ToolStripLabel lCntVert;
        private LMap lm;
        private UC p1;
        private Direct3D p3D;
        private NumericUpDown pitch;
        private Putbox putb;
        private Putvi pvi;
        private NumericUpDown roll;
        private Timer timerBall;
        private Timer timerRun;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripLabel toolStripLabel2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbBallGame;
        private ToolStripButton tsbExpBlenderpy;
        private ToolStripButton tsbShowColl;
        private ToolStripLabel tslMdls;
        private VertexBuffer vb;
        private VUt vut;
        private NumericUpDown yaw;

        public Visf()
        {
            this.alci = new List<CI>();
            this.alalci = new List<CI[]>();
            this.altex = new List<Texture>();
            this.lm = new LMap();
            this.putb = new Putbox();
            this.alpf = new List<Putfragment>();
            this.alDeleter = new List<ComObject>();
            this.alib = new List<RIB>();
            this.vut = new VUt();
            this.curs = Point.Empty;
            this.firstcur = Point.Empty;
            this.alBall = new List<Ball>();
            base..ctor();
            this.InitializeComponent();
            return;
        }

        public Visf(List<DC> aldc, CollReader coll)
        {
            this.alci = new List<CI>();
            this.alalci = new List<CI[]>();
            this.altex = new List<Texture>();
            this.lm = new LMap();
            this.putb = new Putbox();
            this.alpf = new List<Putfragment>();
            this.alDeleter = new List<ComObject>();
            this.alib = new List<RIB>();
            this.vut = new VUt();
            this.curs = Point.Empty;
            this.firstcur = Point.Empty;
            this.alBall = new List<Ball>();
            base..ctor();
            this.aldc = aldc;
            this.coll = coll;
            this.InitializeComponent();
            return;
        }

        private void cbFog_CheckedChanged(object sender, EventArgs e)
        {
            this.p1.Invalidate();
            return;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing == null)
            {
                goto Label_0016;
            }
            if (this.components == null)
            {
                goto Label_0016;
            }
            this.components.Dispose();
        Label_0016:
            base.Dispose(disposing);
            return;
        }

        private void eyeX_ValueChanged(object sender, EventArgs e)
        {
            this.p1.Invalidate();
            return;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager;
            ToolStripItem[] itemArray;
            int[] numArray;
            int[] numArray2;
            int[] numArray3;
            int[] numArray4;
            int[] numArray5;
            int[] numArray6;
            int[] numArray7;
            int[] numArray8;
            int[] numArray9;
            int[] numArray10;
            int[] numArray11;
            int[] numArray12;
            int[] numArray13;
            int[] numArray14;
            int[] numArray15;
            this.components = new Container();
            manager = new ComponentResourceManager(typeof(Visf));
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
            this.eyeX.BeginInit();
            this.eyeY.BeginInit();
            this.eyeZ.BeginInit();
            this.fov.BeginInit();
            this.yaw.BeginInit();
            this.pitch.BeginInit();
            this.roll.BeginInit();
            base.SuspendLayout();
            this.label1.Anchor = 6;
            this.label1.AutoSize = 1;
            this.label1.Location = new Point(12, 0x2a2);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xc1, 0x30);
            this.label1.TabIndex = 1;
            this.label1.Text = "* Mouse wheel: Zoom\r\n* Left btn drag: Rotate\r\n* Right btn drag: Move forward/back\r\n* Middle btn: toss ball";
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.tsbExpBlenderpy, this.toolStripSeparator1, this.lCntVert, this.toolStripLabel1, this.lCntTris, this.toolStripLabel2, this.toolStripSeparator2, this.tslMdls, this.tsbShowColl, this.tsbBallGame });
            this.toolStrip1.Location = new Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new Size(0x375, 0x19);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.tsbExpBlenderpy.Image = (Image) manager.GetObject("tsbExpBlenderpy.Image");
            this.tsbExpBlenderpy.ImageTransparentColor = Color.Magenta;
            this.tsbExpBlenderpy.Name = "tsbExpBlenderpy";
            this.tsbExpBlenderpy.Size = new Size(0xa9, 0x16);
            this.tsbExpBlenderpy.Text = "Export to blender script ";
            this.tsbExpBlenderpy.Click += new EventHandler(this.tsbExpBlenderpy_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(6, 0x19);
            this.lCntVert.Name = "lCntVert";
            this.lCntVert.Size = new Size(15, 0x16);
            this.lCntVert.Text = "0";
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new Size(0x3e, 0x16);
            this.toolStripLabel1.Text = "vertices, ";
            this.lCntTris.Name = "lCntTris";
            this.lCntTris.Size = new Size(15, 0x16);
            this.lCntTris.Text = "0";
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new Size(0x1f, 0x16);
            this.toolStripLabel2.Text = "tris.";
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(6, 0x19);
            this.tslMdls.Name = "tslMdls";
            this.tslMdls.Size = new Size(0x35, 0x16);
            this.tslMdls.Text = "Models:";
            this.tsbShowColl.CheckOnClick = 1;
            this.tsbShowColl.Image = (Image) manager.GetObject("tsbShowColl.Image");
            this.tsbShowColl.ImageTransparentColor = Color.Magenta;
            this.tsbShowColl.Name = "tsbShowColl";
            this.tsbShowColl.Size = new Size(0x4b, 0x16);
            this.tsbShowColl.Text = "Collision";
            this.tsbShowColl.Click += new EventHandler(this.tsbShowColl_Click);
            this.tsbBallGame.CheckOnClick = 1;
            this.tsbBallGame.Image = (Image) manager.GetObject("tsbBallGame.Image");
            this.tsbBallGame.ImageTransparentColor = Color.Magenta;
            this.tsbBallGame.Name = "tsbBallGame";
            this.tsbBallGame.Size = new Size(0x56, 0x16);
            this.tsbBallGame.Text = "Ball game";
            this.tsbBallGame.Click += new EventHandler(this.tsbBallGame_Click);
            this.flppos.Anchor = 6;
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
            this.flppos.Location = new Point(12, 0x281);
            this.flppos.Name = "flppos";
            this.flppos.Size = new Size(0x369, 30);
            this.flppos.TabIndex = 3;
            this.label2.AutoSize = 1;
            this.label2.Location = new Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(60, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "eye (x y z)";
            this.eyeX.Location = new Point(0x45, 3);
            numArray = new int[4];
            numArray[0] = 0xfa00;
            this.eyeX.Maximum = new decimal(numArray);
            numArray2 = new int[4];
            numArray2[0] = 0xfa00;
            numArray2[3] = -2147483648;
            this.eyeX.Minimum = new decimal(numArray2);
            this.eyeX.Name = "eyeX";
            this.eyeX.Size = new Size(0x3b, 0x13);
            this.eyeX.TabIndex = 1;
            this.eyeX.TextAlign = 1;
            this.eyeX.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
            this.eyeY.Location = new Point(0x86, 3);
            numArray3 = new int[4];
            numArray3[0] = 0xfa00;
            this.eyeY.Maximum = new decimal(numArray3);
            numArray4 = new int[4];
            numArray4[0] = 0xfa00;
            numArray4[3] = -2147483648;
            this.eyeY.Minimum = new decimal(numArray4);
            this.eyeY.Name = "eyeY";
            this.eyeY.Size = new Size(0x3b, 0x13);
            this.eyeY.TabIndex = 2;
            this.eyeY.TextAlign = 1;
            numArray5 = new int[4];
            numArray5[0] = 500;
            this.eyeY.Value = new decimal(numArray5);
            this.eyeY.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
            this.eyeZ.Location = new Point(0xc7, 3);
            numArray6 = new int[4];
            numArray6[0] = 0xfa00;
            this.eyeZ.Maximum = new decimal(numArray6);
            numArray7 = new int[4];
            numArray7[0] = 0xfa00;
            numArray7[3] = -2147483648;
            this.eyeZ.Minimum = new decimal(numArray7);
            this.eyeZ.Name = "eyeZ";
            this.eyeZ.Size = new Size(0x3b, 0x13);
            this.eyeZ.TabIndex = 3;
            this.eyeZ.TextAlign = 1;
            this.eyeZ.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
            this.label3.AutoSize = 1;
            this.label3.Location = new Point(0x108, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x15, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "fov";
            this.fov.Location = new Point(0x123, 3);
            numArray8 = new int[4];
            numArray8[0] = 180;
            this.fov.Maximum = new decimal(numArray8);
            this.fov.Name = "fov";
            this.fov.Size = new Size(0x3b, 0x13);
            this.fov.TabIndex = 5;
            this.fov.TextAlign = 1;
            numArray9 = new int[4];
            numArray9[0] = 70;
            this.fov.Value = new decimal(numArray9);
            this.fov.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
            this.label4.AutoSize = 1;
            this.label4.Location = new Point(0x164, 0);
            this.label4.Name = "label4";
            this.label4.Size = new Size(0x7d, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "rotation (yaw pitch roll)";
            this.yaw.Location = new Point(0x1e7, 3);
            numArray10 = new int[4];
            numArray10[0] = 0x8ca0;
            this.yaw.Maximum = new decimal(numArray10);
            numArray11 = new int[4];
            numArray11[0] = 0x8ca0;
            numArray11[3] = -2147483648;
            this.yaw.Minimum = new decimal(numArray11);
            this.yaw.Name = "yaw";
            this.yaw.Size = new Size(0x3b, 0x13);
            this.yaw.TabIndex = 7;
            this.yaw.TextAlign = 1;
            this.yaw.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
            this.pitch.Location = new Point(0x228, 3);
            numArray12 = new int[4];
            numArray12[0] = 0x8ca0;
            this.pitch.Maximum = new decimal(numArray12);
            numArray13 = new int[4];
            numArray13[0] = 0x8ca0;
            numArray13[3] = -2147483648;
            this.pitch.Minimum = new decimal(numArray13);
            this.pitch.Name = "pitch";
            this.pitch.Size = new Size(0x3b, 0x13);
            this.pitch.TabIndex = 8;
            this.pitch.TextAlign = 1;
            this.pitch.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
            this.roll.Location = new Point(0x269, 3);
            numArray14 = new int[4];
            numArray14[0] = 0x8ca0;
            this.roll.Maximum = new decimal(numArray14);
            numArray15 = new int[4];
            numArray15[0] = 0x8ca0;
            numArray15[3] = -2147483648;
            this.roll.Minimum = new decimal(numArray15);
            this.roll.Name = "roll";
            this.roll.Size = new Size(0x3b, 0x13);
            this.roll.TabIndex = 9;
            this.roll.TextAlign = 1;
            this.roll.ValueChanged += new EventHandler(this.eyeX_ValueChanged);
            this.cbFog.AutoSize = 1;
            this.cbFog.Checked = 1;
            this.cbFog.CheckState = 1;
            this.cbFog.Location = new Point(0x2aa, 3);
            this.cbFog.Name = "cbFog";
            this.cbFog.Size = new Size(0x40, 0x10);
            this.cbFog.TabIndex = 10;
            this.cbFog.Text = "Use &fog";
            this.cbFog.UseVisualStyleBackColor = 1;
            this.cbFog.CheckedChanged += new EventHandler(this.cbFog_CheckedChanged);
            this.cbVertexColor.AutoSize = 1;
            this.cbVertexColor.Checked = 1;
            this.cbVertexColor.CheckState = 1;
            this.cbVertexColor.Location = new Point(0x2f0, 3);
            this.cbVertexColor.Name = "cbVertexColor";
            this.cbVertexColor.Size = new Size(0x61, 0x10);
            this.cbVertexColor.TabIndex = 11;
            this.cbVertexColor.Text = "Use vertex &clr";
            this.cbVertexColor.UseVisualStyleBackColor = 1;
            this.cbVertexColor.CheckedChanged += new EventHandler(this.cbFog_CheckedChanged);
            this.timerRun.Interval = 0x19;
            this.timerRun.Tick += new EventHandler(this.timerRun_Tick);
            this.label5.Anchor = 6;
            this.label5.AutoSize = 1;
            this.label5.Location = new Point(0xe5, 0x2a3);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x63, 0x30);
            this.label5.TabIndex = 1;
            this.label5.Text = "* W: move forward\r\n* S: move back\r\n* A: move left\r\n* D: move right";
            this.label6.Anchor = 6;
            this.label6.AutoSize = 1;
            this.label6.Location = new Point(0x161, 0x2a3);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x6a, 0x24);
            this.label6.TabIndex = 1;
            this.label6.Text = "* Shift: Move fast\r\n* Up: move up\r\n* Down: move down";
            this.p1.Anchor = 15;
            this.p1.BackColor = SystemColors.ControlDarkDark;
            this.p1.BorderStyle = 1;
            this.p1.Location = new Point(12, 0x1c);
            this.p1.Name = "p1";
            this.p1.Size = new Size(0x35d, 0x25f);
            this.p1.TabIndex = 0;
            this.p1.UseTransparent = 1;
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
            base.AutoScaleMode = 1;
            base.ClientSize = new Size(0x375, 0x2dc);
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
            this.toolStrip1.ResumeLayout(0);
            this.toolStrip1.PerformLayout();
            this.flppos.ResumeLayout(0);
            this.flppos.PerformLayout();
            this.eyeX.EndInit();
            this.eyeY.EndInit();
            this.eyeZ.EndInit();
            this.fov.EndInit();
            this.yaw.EndInit();
            this.pitch.EndInit();
            this.roll.EndInit();
            base.ResumeLayout(0);
            base.PerformLayout();
            return;
        }

        private void p1_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keys;
            keys = e.KeyCode;
            if (keys > 0x41)
            {
                goto Label_0028;
            }
            switch ((keys - 0x26))
            {
                case 0:
                    goto Label_0077;

                case 1:
                    goto Label_0097;

                case 2:
                    goto Label_0088;
            }
            if (keys == 0x41)
            {
                goto Label_0057;
            }
            goto Label_0097;
        Label_0028:
            if (keys == 0x44)
            {
                goto Label_0067;
            }
            if (keys == 0x53)
            {
                goto Label_0047;
            }
            if (keys != 0x57)
            {
                goto Label_0097;
            }
            this.kr |= 1;
            goto Label_0097;
        Label_0047:
            this.kr |= 2;
            goto Label_0097;
        Label_0057:
            this.kr |= 4;
            goto Label_0097;
        Label_0067:
            this.kr |= 8;
            goto Label_0097;
        Label_0077:
            this.kr |= 0x10;
            goto Label_0097;
        Label_0088:
            this.kr |= 0x20;
        Label_0097:
            if (this.kr == null)
            {
                goto Label_00B7;
            }
            if (this.timerRun.Enabled != null)
            {
                goto Label_00B7;
            }
            this.timerRun.Start();
        Label_00B7:
            return;
        }

        private void p1_KeyUp(object sender, KeyEventArgs e)
        {
            Keys keys;
            keys = e.KeyCode;
            if (keys > 0x41)
            {
                goto Label_0028;
            }
            switch ((keys - 0x26))
            {
                case 0:
                    goto Label_007B;

                case 1:
                    goto Label_009B;

                case 2:
                    goto Label_008C;
            }
            if (keys == 0x41)
            {
                goto Label_0059;
            }
            goto Label_009B;
        Label_0028:
            if (keys == 0x44)
            {
                goto Label_006A;
            }
            if (keys == 0x53)
            {
                goto Label_0048;
            }
            if (keys != 0x57)
            {
                goto Label_009B;
            }
            this.kr &= -2;
            goto Label_009B;
        Label_0048:
            this.kr &= -3;
            goto Label_009B;
        Label_0059:
            this.kr &= -5;
            goto Label_009B;
        Label_006A:
            this.kr &= -9;
            goto Label_009B;
        Label_007B:
            this.kr &= -17;
            goto Label_009B;
        Label_008C:
            this.kr &= -33;
        Label_009B:
            if (this.kr != null)
            {
                goto Label_00BB;
            }
            if (this.timerRun.Enabled == null)
            {
                goto Label_00BB;
            }
            this.timerRun.Stop();
        Label_00BB:
            return;
        }

        private unsafe void p1_Load(object sender, EventArgs e)
        {
            List<CustomVertex.PositionColoredTextured> list;
            DC dc;
            ToolStripButton button;
            int[] numArray;
            int num;
            DC dc2;
            int num2;
            Bitmap bitmap;
            MemoryStream stream;
            Texture texture;
            KeyValuePair<int, Parse4Mdlx.Model> pair;
            CI ci;
            int num3;
            Parse4Mdlx.Model model;
            List<uint> list2;
            int num4;
            int num5;
            Vifpli vifpli;
            byte[] buffer;
            VU1Mem mem;
            ParseVIF1 evif;
            byte[] buffer2;
            CI ci2;
            MemoryStream stream2;
            BinaryReader reader;
            int num6;
            int num7;
            int num8;
            int num9;
            List<uint> list3;
            int num10;
            int num11;
            int num12;
            int num13;
            int num14;
            int num15;
            Vector3 vector;
            int num16;
            int num17;
            int num18;
            int num19;
            Color color;
            CustomVertex.PositionColoredTextured textured;
            DataStream stream3;
            CustomVertex.PositionColoredTextured textured2;
            int num20;
            int num21;
            CI[] ciArray;
            CI ci3;
            IndexBuffer buffer3;
            DataStream stream4;
            uint num22;
            RIB rib;
            RIB rib2;
            Co2 co;
            PresentParameters[] parametersArray;
            Color color2;
            List<DC>.Enumerator enumerator;
            List<DC>.Enumerator enumerator2;
            Bitmap[] bitmapArray;
            int num23;
            SortedDictionary<int, Parse4Mdlx.Model>.Enumerator enumerator3;
            List<byte[]>.Enumerator enumerator4;
            CustomVertex.PositionColoredTextured textured3;
            int num24;
            List<CustomVertex.PositionColoredTextured>.Enumerator enumerator5;
            List<CI[]>.Enumerator enumerator6;
            CI[] ciArray2;
            uint[] numArray2;
            int num25;
            List<Co2>.Enumerator enumerator7;
            this.p3D = new Direct3D();
            this.alDeleter.Add(this.p3D);
            this.device = new Device(this.p3D, 0, 1, this.p1.Handle, 0x40, new PresentParameters[] { this.PP });
            this.alDeleter.Add(this.device);
            this.device.SetRenderState(0x89, 0);
            this.device.SetRenderState(7, 1);
            this.device.SetRenderState(15, 1);
            this.device.SetRenderState(0x18, 2);
            this.device.SetRenderState<Compare>(0x19, 7);
            this.device.SetRenderState(0x1b, 1);
            this.device.SetRenderState<Blend>(0x13, 5);
            this.device.SetRenderState<Blend>(0xcf, 5);
            this.device.SetRenderState<Blend>(20, 6);
            this.device.SetRenderState<Blend>(0xd0, 6);
            this.device.SetRenderState<Cull>(0x16, 3);
            this.device.SetRenderState(0x22, &this.p1.BackColor.ToArgb());
            this.device.SetRenderState(0x24, 5f);
            this.device.SetRenderState(0x25, 30000f);
            this.device.SetRenderState(0x26, 0.0001f);
            this.device.SetRenderState<FogMode>(140, 1);
            this.p1.MouseWheel += new MouseEventHandler(this.p1_MouseWheel);
            list = new List<CustomVertex.PositionColoredTextured>();
            enumerator = this.aldc.GetEnumerator();
        Label_01A5:
            try
            {
                goto Label_0224;
            Label_01A7:
                dc = &enumerator.Current;
                button = new ToolStripButton("Show " + dc.name);
                button.DisplayStyle = 1;
                button.CheckOnClick = 1;
                button.Tag = (Guid) dc.dcId;
                button.Checked = 1;
                button.CheckedChanged += new EventHandler(this.tsiIfRender_CheckedChanged);
                this.toolStrip1.Items.Insert(this.toolStrip1.Items.IndexOf(this.tsbShowColl), button);
            Label_0224:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_01A7;
                }
                goto Label_0240;
            }
            finally
            {
            Label_0232:
                &enumerator.Dispose();
            }
        Label_0240:
            this.alci.Clear();
            this.altex.Clear();
            numArray = new int[4];
            num = 0;
            enumerator2 = this.aldc.GetEnumerator();
        Label_026D:
            try
            {
                goto Label_077E;
            Label_0272:
                dc2 = &enumerator2.Current;
                num2 = this.altex.Count;
                bitmapArray = dc2.o7.pics;
                num23 = 0;
                goto Label_02EE;
            Label_029B:
                bitmap = bitmapArray[num23];
                stream = new MemoryStream();
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0L;
                this.altex.Add(texture = Texture.FromStream(this.device, stream));
                this.alDeleter.Add(texture);
                num23 += 1;
            Label_02EE:
                if (num23 < ((int) bitmapArray.Length))
                {
                    goto Label_029B;
                }
                if (dc2.o4Mdlx == null)
                {
                    goto Label_03C9;
                }
                enumerator3 = dc2.o4Mdlx.dictModel.GetEnumerator();
            Label_0315:
                try
                {
                    goto Label_03AA;
                Label_031A:
                    pair = &enumerator3.Current;
                    ci = new CI();
                    num3 = list.Count;
                    model = &pair.Value;
                    list.AddRange(model.alv);
                    list2 = new List<uint>();
                    num4 = 0;
                    goto Label_0366;
                Label_0354:
                    list2.Add(num3 + num4);
                    num4 += 1;
                Label_0366:
                    if (num4 < model.alv.Count)
                    {
                        goto Label_0354;
                    }
                    ci.ali = list2.ToArray();
                    ci.texi = num2 + &pair.Key;
                    ci.vifi = 0;
                    this.alci.Add(ci);
                Label_03AA:
                    if (&enumerator3.MoveNext() != null)
                    {
                        goto Label_031A;
                    }
                    goto Label_075D;
                }
                finally
                {
                Label_03BB:
                    &enumerator3.Dispose();
                }
            Label_03C9:
                if (dc2.o4Map == null)
                {
                    goto Label_075D;
                }
                num5 = 0;
                goto Label_0745;
            Label_03DD:
                vifpli = dc2.o4Map.alvifpkt[num5];
                buffer = vifpli.vifpkt;
                mem = new VU1Mem();
                evif = new ParseVIF1(mem);
                evif.Parse(new MemoryStream(buffer, 0), 0);
                enumerator4 = evif.almsmem.GetEnumerator();
            Label_0429:
                try
                {
                    goto Label_0723;
                Label_042E:
                    buffer2 = &enumerator4.Current;
                    ci2 = new CI();
                    stream2 = new MemoryStream(buffer2, 0);
                    reader = new BinaryReader(stream2);
                    reader.ReadInt32();
                    reader.ReadInt32();
                    reader.ReadInt32();
                    reader.ReadInt32();
                    num6 = reader.ReadInt32();
                    num7 = reader.ReadInt32();
                    reader.ReadInt32();
                    reader.ReadInt32();
                    reader.ReadInt32();
                    num8 = reader.ReadInt32();
                    reader.ReadInt32();
                    reader.ReadInt32();
                    reader.ReadInt32();
                    num9 = reader.ReadInt32();
                    list3 = new List<uint>();
                    num10 = list.Count;
                    num11 = 0;
                    goto Label_06E5;
                Label_04DC:
                    stream2.Position = (long) (0x10 * (num7 + num11));
                    num12 = reader.ReadInt16();
                    reader.ReadInt16();
                    num13 = reader.ReadInt16();
                    reader.ReadInt16();
                    num14 = reader.ReadInt16();
                    reader.ReadInt16();
                    num15 = reader.ReadInt16();
                    reader.ReadInt16();
                    stream2.Position = (long) (0x10 * (num9 + num14));
                    &vector.X = -reader.ReadSingle();
                    &vector.Y = reader.ReadSingle();
                    &vector.Z = reader.ReadSingle();
                    stream2.Position = (long) (0x10 * (num8 + num11));
                    num16 = (byte) reader.ReadUInt32();
                    num17 = (byte) reader.ReadUInt32();
                    num18 = (byte) reader.ReadUInt32();
                    num19 = (byte) reader.ReadUInt32();
                    if (num8 != null)
                    {
                        goto Label_05C3;
                    }
                    num16 = 0xff;
                    num17 = 0xff;
                    num18 = 0xff;
                    num19 = 0xff;
                Label_05C3:
                    numArray[num & 3] = num10 + num11;
                    num += 1;
                    if (num15 == null)
                    {
                        goto Label_066A;
                    }
                    if (num15 == 0x10)
                    {
                        goto Label_066A;
                    }
                    if (num15 != 0x20)
                    {
                        goto Label_0628;
                    }
                    list3.Add(Convert.ToUInt32(numArray[(num - 1) & 3]));
                    list3.Add(Convert.ToUInt32(numArray[(num - 2) & 3]));
                    list3.Add(Convert.ToUInt32(numArray[(num - 3) & 3]));
                    goto Label_066A;
                Label_0628:
                    if (num15 != 0x30)
                    {
                        goto Label_066A;
                    }
                    list3.Add(Convert.ToUInt32(numArray[(num - 1) & 3]));
                    list3.Add(Convert.ToUInt32(numArray[(num - 3) & 3]));
                    list3.Add(Convert.ToUInt32(numArray[(num - 2) & 3]));
                Label_066A:
                    color = Color.FromArgb(this.lm.al[num19], this.lm.al[num16], this.lm.al[num17], this.lm.al[num18]);
                    &textured = new CustomVertex.PositionColoredTextured(vector, &color.ToArgb(), (((float) num12) / 16f) / 256f, (((float) num13) / 16f) / 256f);
                    list.Add(textured);
                    num11 += 1;
                Label_06E5:
                    if (num11 < num6)
                    {
                        goto Label_04DC;
                    }
                    ci2.ali = list3.ToArray();
                    ci2.texi = num2 + vifpli.texi;
                    ci2.vifi = num5;
                    this.alci.Add(ci2);
                Label_0723:
                    if (&enumerator4.MoveNext() != null)
                    {
                        goto Label_042E;
                    }
                    goto Label_073F;
                }
                finally
                {
                Label_0731:
                    &enumerator4.Dispose();
                }
            Label_073F:
                num5 += 1;
            Label_0745:
                if (num5 < dc2.o4Map.alvifpkt.Count)
                {
                    goto Label_03DD;
                }
            Label_075D:
                this.alalci.Add(this.alci.ToArray());
                this.alci.Clear();
            Label_077E:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_0272;
                }
                goto Label_079A;
            }
            finally
            {
            Label_078C:
                &enumerator2.Dispose();
            }
        Label_079A:
            if (this.alalci.Count == null)
            {
                goto Label_07C9;
            }
            this.alci.Clear();
            this.alci.AddRange(this.alalci[0]);
        Label_07C9:
            if (list.Count != null)
            {
                goto Label_07E1;
            }
            list.Add(new CustomVertex.PositionColoredTextured());
        Label_07E1:
            this.vb = new VertexBuffer(this.device, (this.cntVerts = list.Count) * CustomVertex.PositionColoredTextured.Size, 0x40, CustomVertex.PositionColoredTextured.Format, 1);
            this.alDeleter.Add(this.vb);
            stream3 = this.vb.Lock(0, 0, 0);
        Label_0832:
            try
            {
                enumerator5 = list.GetEnumerator();
            Label_083A:
                try
                {
                    goto Label_084E;
                Label_083C:
                    textured2 = &enumerator5.Current;
                    stream3.Write<CustomVertex.PositionColoredTextured>(textured2);
                Label_084E:
                    if (&enumerator5.MoveNext() != null)
                    {
                        goto Label_083C;
                    }
                    goto Label_0867;
                }
                finally
                {
                Label_0859:
                    &enumerator5.Dispose();
                }
            Label_0867:
                goto Label_0876;
            }
            finally
            {
            Label_0869:
                this.vb.Unlock();
            }
        Label_0876:
            this.lCntVert.Text = &this.cntVerts.ToString("#,##0");
            num20 = 0;
            this.alib.Clear();
            num21 = 0;
            enumerator6 = this.alalci.GetEnumerator();
        Label_08AF:
            try
            {
                goto Label_0A5D;
            Label_08B4:
                ciArray = &enumerator6.Current;
                ciArray2 = ciArray;
                num23 = 0;
                goto Label_0A4C;
            Label_08C9:
                ci3 = ciArray2[num23];
                if (((int) ci3.ali.Length) == null)
                {
                    goto Label_09D4;
                }
                buffer3 = new IndexBuffer(this.device, 4 * ((int) ci3.ali.Length), 0, 1, 0);
                num20 += (int) ci3.ali.Length;
                this.alDeleter.Add(buffer3);
                stream4 = buffer3.Lock(0, 0, 0);
            Label_0920:
                try
                {
                    numArray2 = ci3.ali;
                    num25 = 0;
                    goto Label_0944;
                Label_092E:
                    num22 = numArray2[num25];
                    stream4.Write<uint>(num22);
                    num25 += 1;
                Label_0944:
                    if (num25 < ((int) numArray2.Length))
                    {
                        goto Label_092E;
                    }
                    goto Label_0957;
                }
                finally
                {
                Label_094E:
                    buffer3.Unlock();
                }
            Label_0957:
                rib = new RIB();
                rib.ib = buffer3;
                rib.cnt = (int) ci3.ali.Length;
                rib.texi = ci3.texi;
                rib.vifi = ci3.vifi;
                rib.name = this.aldc[num21].name;
                rib.dcId = this.aldc[num21].dcId;
                this.alib.Add(rib);
                goto Label_0A46;
            Label_09D4:
                rib2 = new RIB();
                rib2.ib = null;
                rib2.cnt = 0;
                rib2.texi = ci3.texi;
                rib2.vifi = ci3.vifi;
                rib2.name = this.aldc[num21].name;
                rib2.dcId = this.aldc[num21].dcId;
                this.alib.Add(rib2);
            Label_0A46:
                num23 += 1;
            Label_0A4C:
                if (num23 < ((int) ciArray2.Length))
                {
                    goto Label_08C9;
                }
                num21 += 1;
            Label_0A5D:
                if (&enumerator6.MoveNext() != null)
                {
                    goto Label_08B4;
                }
                goto Label_0A79;
            }
            finally
            {
            Label_0A6B:
                &enumerator6.Dispose();
            }
        Label_0A79:
            num24 = num20 / 3;
            this.lCntTris.Text = &num24.ToString("#,##0");
            enumerator7 = this.coll.alCo2.GetEnumerator();
        Label_0AA8:
            try
            {
                goto Label_0ACB;
            Label_0AAA:
                co = &enumerator7.Current;
                this.alpf.Add(this.putb.Add(co));
            Label_0ACB:
                if (&enumerator7.MoveNext() != null)
                {
                    goto Label_0AAA;
                }
                goto Label_0AE4;
            }
            finally
            {
            Label_0AD6:
                &enumerator7.Dispose();
            }
        Label_0AE4:
            if (this.putb.alv.Count == null)
            {
                goto Label_0B0D;
            }
            this.pvi = new Putvi(this.putb, this.device);
        Label_0B0D:
            Console.Write("");
            return;
        }

        private void p1_MouseDown(object sender, MouseEventArgs e)
        {
            Ball ball;
            Point point;
            if (e.Button == 0x100000)
            {
                goto Label_001A;
            }
            if (e.Button != 0x200000)
            {
                goto Label_0045;
            }
        Label_001A:
            this.firstcur = this.p1.PointToScreen(this.curs = new Point(e.X, e.Y));
        Label_0045:
            if (e.Button != 0x400000)
            {
                goto Label_00A5;
            }
            ball = new Ball();
            ball.pos = this.CameraEye + (this.Target * 100f);
            ball.velo = this.Target;
            this.alBall.Add(ball);
            this.tsbBallGame.Checked = 1;
            this.tsbBallGame_Click(sender, e);
        Label_00A5:
            return;
        }

        private unsafe void p1_MouseMove(object sender, MouseEventArgs e)
        {
            int num;
            int num2;
            if ((this.curs != Point.Empty) == null)
            {
                goto Label_0138;
            }
            num = e.X - &this.curs.X;
            num2 = e.Y - &this.curs.Y;
            if (num != null)
            {
                goto Label_0044;
            }
            if (num2 == null)
            {
                goto Label_0138;
            }
        Label_0044:
            if ((e.Button & 0x100000) == null)
            {
                goto Label_00D1;
            }
            this.yaw.Value += (decimal) ((float) (((float) num) / 3f));
            this.roll.Value = (decimal) ((float) Math.Max(-89f, Math.Min(89f, (Convert.ToSingle(this.roll.Value) % 360f) - (((float) num2) / 3f))));
            Cursor.Position = this.firstcur;
            this.p1.Invalidate();
            return;
        Label_00D1:
            if ((e.Button & 0x200000) == null)
            {
                goto Label_0138;
            }
            this.yaw.Value += (decimal) ((float) (((float) num) / 3f));
            this.CameraEye += this.Target * ((float) -num2);
            Cursor.Position = this.firstcur;
            this.p1.Invalidate();
        Label_0138:
            return;
        }

        private void p1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != 0x100000)
            {
                goto Label_0018;
            }
            this.curs = Point.Empty;
        Label_0018:
            return;
        }

        private void p1_MouseWheel(object sender, MouseEventArgs e)
        {
            this.fov.Value = (decimal) ((float) Math.Max(Convert.ToSingle(this.fov.Minimum), Math.Min(Convert.ToSingle(this.fov.Maximum), Math.Max(1f, Convert.ToSingle(this.fov.Value) + (((float) e.Delta) / 200f)))));
            this.p1.Invalidate();
            return;
        }

        private unsafe void p1_Paint(object sender, PaintEventArgs e)
        {
            Size size;
            float num;
            RIB rib;
            List<CustomVertex.PositionColored> list;
            Ball ball;
            Co2 co;
            int num2;
            Co3 co2;
            int num3;
            Light light;
            Material material;
            Matrix matrix;
            List<RIB>.Enumerator enumerator;
            List<Ball>.Enumerator enumerator2;
            Color color;
            List<Co2>.Enumerator enumerator3;
            Color color2;
            CustomVertex.PositionNormalColored[] coloredArray;
            size = this.p1.ClientSize;
            num = (&size.Height != null) ? (((float) &size.Width) / ((float) &size.Height)) : 0f;
            this.device.SetTransform(0x100, Matrix.Identity);
            this.device.SetTransform(2, Matrix.LookAtLH(this.CameraEye, this.CameraEye + this.Target, this.CameraUp));
            this.device.SetTransform(3, Matrix.PerspectiveFovLH((Convert.ToSingle(this.fov.Value) / 180f) * 3.14159f, num, Convert.ToSingle(50), Convert.ToSingle(0x4c4b40)));
            this.device.Clear(3, this.p1.BackColor, 1f, 0);
            this.device.BeginScene();
            this.device.SetTextureStageState(0, 1, (this.cbVertexColor.Checked != null) ? 4 : 2);
            this.device.SetTextureStageState(0, 2, 2);
            this.device.SetTextureStageState(0, 3, 0);
            this.device.SetTextureStageState(0, 4, 4);
            this.device.SetTextureStageState(0, 5, 2);
            this.device.SetTextureStageState(0, 6, 0);
            this.device.SetRenderState(0x1c, this.cbFog.Checked);
            if (this.vb == null)
            {
                goto Label_0266;
            }
            this.device.SetStreamSource(0, this.vb, 0, CustomVertex.PositionColoredTextured.Size);
            this.device.VertexFormat = CustomVertex.PositionColoredTextured.Format;
            enumerator = this.alib.GetEnumerator();
        Label_01A6:
            try
            {
                goto Label_024A;
            Label_01AB:
                rib = &enumerator.Current;
                if ((rib.ib == null) || (rib.render == null))
                {
                    goto Label_024A;
                }
                this.device.SetRenderState(0x1c, (this.cbFog.Checked == null) ? 0 : rib.name.Equals("MAP"));
                this.device.Indices = rib.ib;
                this.device.SetTexture(0, this.altex[rib.texi & 0xffff]);
                this.device.DrawIndexedPrimitives(4, 0, 0, this.cntVerts, 0, rib.cnt / 3);
            Label_024A:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_01AB;
                }
                goto Label_0266;
            }
            finally
            {
            Label_0258:
                &enumerator.Dispose();
            }
        Label_0266:
            if (this.tsbBallGame.Checked == null)
            {
                goto Label_037D;
            }
            list = new List<CustomVertex.PositionColored>();
            enumerator2 = this.alBall.GetEnumerator();
        Label_0289:
            try
            {
                goto Label_02B4;
            Label_028B:
                ball = &enumerator2.Current;
                list.Add(new CustomVertex.PositionColored(ball.pos, &Color.Red.ToArgb()));
            Label_02B4:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_028B;
                }
                goto Label_02CD;
            }
            finally
            {
            Label_02BF:
                &enumerator2.Dispose();
            }
        Label_02CD:
            if (list.Count == null)
            {
                goto Label_037D;
            }
            this.device.VertexFormat = CustomVertex.PositionColored.Format;
            this.device.SetRenderState(0x9d, 1);
            this.device.SetRenderState(0x9a, 10f);
            this.device.SetRenderState(0x9e, 0f);
            this.device.SetRenderState(0x9f, 0f);
            this.device.SetRenderState(160, 1f);
            this.device.DrawUserPrimitives<CustomVertex.PositionColored>(1, list.Count, list.ToArray());
            this.device.SetRenderState(0x9d, 0);
        Label_037D:
            if (this.tsbShowColl.Checked == null)
            {
                goto Label_067A;
            }
            this.vut.Clear();
            enumerator3 = this.coll.alCo2.GetEnumerator();
        Label_03AA:
            try
            {
                goto Label_04D3;
            Label_03AF:
                co = &enumerator3.Current;
                num2 = co.Co3frm;
                goto Label_04C5;
            Label_03C6:
                co2 = this.coll.alCo3[num2];
                num3 = &Color.Yellow.ToArgb();
                if (0 > co2.vi0)
                {
                    goto Label_04BF;
                }
                if (0 > co2.vi1)
                {
                    goto Label_04BF;
                }
                if (0 > co2.vi2)
                {
                    goto Label_04BF;
                }
                this.vut.AddTri(this.coll.alCo4[co2.vi0], this.coll.alCo4[co2.vi2], this.coll.alCo4[co2.vi1], num3);
                if (0 > co2.vi3)
                {
                    goto Label_04BF;
                }
                this.vut.AddTri(this.coll.alCo4[co2.vi3], this.coll.alCo4[co2.vi2], this.coll.alCo4[co2.vi0], num3);
            Label_04BF:
                num2 += 1;
            Label_04C5:
                if (num2 < co.Co3to)
                {
                    goto Label_03C6;
                }
            Label_04D3:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_03AF;
                }
                goto Label_04EF;
            }
            finally
            {
            Label_04E1:
                &enumerator3.Dispose();
            }
        Label_04EF:
            if (this.vut.alv.Count == null)
            {
                goto Label_067A;
            }
            this.device.SetTextureStageState(0, 1, 2);
            this.device.SetTextureStageState(0, 2, 0);
            this.device.SetRenderState(0x1b, 0);
            this.device.SetRenderState(0x89, 1);
            this.device.SetRenderState(15, 0);
            this.device.SetRenderState<ShadeMode>(9, 2);
            this.device.SetRenderState(0x8f, 1);
            this.device.EnableLight(0, 1);
            light = this.device.GetLight(0);
            &light.Direction = this.Target;
            &light.Diffuse = new Color4(-1);
            this.device.SetLight(0, light);
            material = this.device.Material;
            this.device.Material = material;
            matrix = Matrix.Scaling(-1f, -1f, -1f);
            this.device.SetTransform(0x100, matrix);
            this.device.VertexFormat = CustomVertex.PositionNormalColored.Format;
            this.device.DrawUserPrimitives<CustomVertex.PositionNormalColored>(4, this.vut.alv.Count / 3, this.vut.alvtmp = this.vut.alv.ToArray());
            this.device.SetRenderState(0x1b, 1);
            this.device.SetRenderState(0x89, 0);
            this.device.SetRenderState(15, 1);
        Label_067A:
            this.device.EndScene();
            this.device.Present();
            return;
        }

        private void p1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Keys keys;
            switch ((e.KeyCode - 0x26))
            {
                case 0:
                    goto Label_001D;

                case 1:
                    goto Label_0024;

                case 2:
                    goto Label_001D;
            }
            return;
        Label_001D:
            e.IsInputKey = 1;
        Label_0024:
            return;
        }

        private void p1_SizeChanged(object sender, EventArgs e)
        {
            this.p1.Invalidate();
            return;
        }

        private unsafe void PhysicBall()
        {
            Ball ball;
            Vector3 vector;
            Vector3 vector2;
            List<Ball>.Enumerator enumerator;
            enumerator = this.alBall.GetEnumerator();
        Label_000C:
            try
            {
                goto Label_0062;
            Label_000E:
                ball = &enumerator.Current;
                vector = ball.pos;
                vector2 = vector + (ball.velo * 0.5f);
                if (this.TestColl(vector) != this.TestColl(vector2))
                {
                    goto Label_0062;
                }
                ball.pos = vector2;
                &ball.velo.Y -= 3f;
            Label_0062:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_000E;
                }
                goto Label_007B;
            }
            finally
            {
            Label_006D:
                &enumerator.Dispose();
            }
        Label_007B:
            return;
        }

        private unsafe bool TestColl(Vector3 v)
        {
            bool flag;
            Co2 co;
            int num;
            int num2;
            int num3;
            Co3 co2;
            List<Co2>.Enumerator enumerator;
            &v.X = -&v.X;
            &v.Y = -&v.Y;
            &v.Z = -&v.Z;
            flag = 0;
            enumerator = this.coll.alCo2.GetEnumerator();
        Label_0041:
            try
            {
                goto Label_0143;
            Label_0046:
                co = &enumerator.Current;
                if ((((co.Min.X > &v.X) || (co.Min.Y > &v.Y)) || ((co.Min.Z > &v.Z) || (&v.X > co.Max.X))) || ((&v.Y > co.Max.Y) || (&v.Z > co.Max.Z)))
                {
                    goto Label_0143;
                }
                num = 0;
                num2 = 0;
                num3 = co.Co3frm;
                goto Label_012C;
            Label_00E3:
                co2 = this.coll.alCo3[num3];
                num += (Plane.DotCoordinate(this.coll.alCo5[co2.PlaneCo5], v) > 0f) ? 1 : 0;
                num2 += 1;
                num3 += 1;
            Label_012C:
                if (num3 < co.Co3to)
                {
                    goto Label_00E3;
                }
                flag |= (num2 == null) ? 0 : (num == num2);
            Label_0143:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0046;
                }
                goto Label_015F;
            }
            finally
            {
            Label_0151:
                &enumerator.Dispose();
            }
        Label_015F:
            return flag;
        }

        private void timerBall_Tick(object sender, EventArgs e)
        {
            this.PhysicBall();
            this.p1.Invalidate();
            return;
        }

        private void timerRun_Tick(object sender, EventArgs e)
        {
            if ((this.kr & 1) == null)
            {
                goto Label_002D;
            }
            this.CameraEye += this.Target * ((float) this.Speed);
        Label_002D:
            if ((this.kr & 2) == null)
            {
                goto Label_005A;
            }
            this.CameraEye -= this.Target * ((float) this.Speed);
        Label_005A:
            if ((this.kr & 4) == null)
            {
                goto Label_0087;
            }
            this.CameraEye += this.LeftVec * ((float) this.Speed);
        Label_0087:
            if ((this.kr & 8) == null)
            {
                goto Label_00B4;
            }
            this.CameraEye -= this.LeftVec * ((float) this.Speed);
        Label_00B4:
            if ((this.kr & 0x10) == null)
            {
                goto Label_00E1;
            }
            this.CameraEye += Vector3.UnitY * ((float) this.Speed);
        Label_00E1:
            if ((this.kr & 0x20) == null)
            {
                goto Label_010E;
            }
            this.CameraEye -= Vector3.UnitY * ((float) this.Speed);
        Label_010E:
            this.p1.Invalidate();
            return;
        }

        private void tsbBallGame_Click(object sender, EventArgs e)
        {
            bool flag;
            if ((this.timerBall.Enabled = this.tsbBallGame.Checked) != null)
            {
                goto Label_0026;
            }
            this.alBall.Clear();
        Label_0026:
            return;
        }

        private unsafe void tsbExpBlenderpy_Click(object sender, EventArgs e)
        {
            DataStream stream;
            CustomVertex.PositionColoredTextured[] texturedArray;
            int num;
            int num2;
            int num3;
            Mkbpy mkbpy;
            string str;
            int num4;
            DC dc;
            Bitmap bitmap;
            string str2;
            CI ci;
            int num5;
            Bitmap[] bitmapArray;
            int num6;
            CI[] ciArray;
            int num7;
            Color[] colorArray;
            Directory.CreateDirectory("bpyexp");
            stream = this.vb.Lock(0, 0, 0x10);
        Label_001B:
            try
            {
                texturedArray = new CustomVertex.PositionColoredTextured[this.cntVerts];
                num = 0;
                goto Label_0041;
            Label_002B:
                *(&(texturedArray[num])) = stream.Read<CustomVertex.PositionColoredTextured>();
                num += 1;
            Label_0041:
                if (num < this.cntVerts)
                {
                    goto Label_002B;
                }
                num2 = 0;
                num3 = 0;
                goto Label_0382;
            Label_0054:
                mkbpy = new Mkbpy();
                mkbpy.StartTex();
                str = @"bpyexp\" + this.aldc[num3].name;
                Directory.CreateDirectory(str);
                num4 = 0;
                dc = this.aldc[num3];
                bitmapArray = dc.o7.pics;
                num6 = 0;
                goto Label_0129;
            Label_00AD:
                bitmap = bitmapArray[num6];
                str2 = string.Format("t{0:000}.png", (int) num4);
                bitmap.Save(str + @"\" + str2, ImageFormat.Png);
                mkbpy.AddTex(Path.GetFullPath(str + @"\" + str2), string.Format("Tex{0:000}", (int) num4), string.Format("Mat{0:000}", (int) num4));
                num4 += 1;
                num6 += 1;
            Label_0129:
                if (num6 < ((int) bitmapArray.Length))
                {
                    goto Label_00AD;
                }
                mkbpy.EndTex();
                ciArray = this.alalci[num3];
                num7 = 0;
                goto Label_0348;
            Label_0152:
                ci = ciArray[num7];
                mkbpy.StartMesh();
                num5 = 0;
                goto Label_0322;
            Label_0168:
                mkbpy.AddV(*(&(texturedArray[(IntPtr) ci.ali[num5 * 3]])));
                mkbpy.AddV(*(&(texturedArray[(IntPtr) ci.ali[(num5 * 3) + 2]])));
                mkbpy.AddV(*(&(texturedArray[(IntPtr) ci.ali[(num5 * 3) + 1]])));
                colorArray = new Color[3];
                *(&(colorArray[0])) = Color.FromArgb(&(texturedArray[(IntPtr) ci.ali[num5 * 3]]).Color);
                *(&(colorArray[1])) = Color.FromArgb(&(texturedArray[(IntPtr) ci.ali[(num5 * 3) + 2]]).Color);
                *(&(colorArray[2])) = Color.FromArgb(&(texturedArray[(IntPtr) ci.ali[(num5 * 3) + 1]]).Color);
                mkbpy.AddColorVtx(colorArray);
                mkbpy.AddTuv((ci.texi & 0xffff) - num2, &(texturedArray[(IntPtr) ci.ali[num5 * 3]]).Tu, 1f - &(texturedArray[(IntPtr) ci.ali[num5 * 3]]).Tv, &(texturedArray[(IntPtr) ci.ali[(num5 * 3) + 2]]).Tu, 1f - &(texturedArray[(IntPtr) ci.ali[(num5 * 3) + 2]]).Tv, &(texturedArray[(IntPtr) ci.ali[(num5 * 3) + 1]]).Tu, 1f - &(texturedArray[(IntPtr) ci.ali[(num5 * 3) + 1]]).Tv);
                num5 += 1;
            Label_0322:
                if (num5 < (((int) ci.ali.Length) / 3))
                {
                    goto Label_0168;
                }
                mkbpy.EndMesh(ci.vifi);
                num7 += 1;
            Label_0348:
                if (num7 < ((int) ciArray.Length))
                {
                    goto Label_0152;
                }
                mkbpy.Finish();
                File.WriteAllText(str + @"\mesh.py", mkbpy.ToString(), Encoding.ASCII);
                num2 += num4;
                num3 += 1;
            Label_0382:
                if (num3 < this.alalci.Count)
                {
                    goto Label_0054;
                }
                goto Label_03A3;
            }
            finally
            {
            Label_0396:
                this.vb.Unlock();
            }
        Label_03A3:
            Process.Start("explorer.exe", " bpyexp");
            return;
        }

        private void tsbShowColl_Click(object sender, EventArgs e)
        {
            this.p1.Invalidate();
            return;
        }

        private void tsbShowGeo_Click(object sender, EventArgs e)
        {
            this.p1.Invalidate();
            return;
        }

        private unsafe void tsiIfRender_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripButton button;
            bool flag;
            Guid guid;
            RIB rib;
            List<RIB>.Enumerator enumerator;
            button = (ToolStripButton) sender;
            flag = button.Checked;
            guid = (Guid) button.Tag;
            enumerator = this.alib.GetEnumerator();
        Label_0027:
            try
            {
                goto Label_0046;
            Label_0029:
                rib = &enumerator.Current;
                if (&rib.dcId.Equals(guid) == null)
                {
                    goto Label_0046;
                }
                rib.render = flag;
            Label_0046:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0029;
                }
                goto Label_005F;
            }
            finally
            {
            Label_0051:
                &enumerator.Dispose();
            }
        Label_005F:
            this.p1.Invalidate();
            return;
        }

        private unsafe void Visf_FormClosing(object sender, FormClosingEventArgs e)
        {
            ComObject obj2;
            List<ComObject>.Enumerator enumerator;
            this.alDeleter.Reverse();
            enumerator = this.alDeleter.GetEnumerator();
        Label_0017:
            try
            {
                goto Label_0027;
            Label_0019:
                obj2 = &enumerator.Current;
                obj2.Dispose();
            Label_0027:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0019;
                }
                goto Label_0040;
            }
            finally
            {
            Label_0032:
                &enumerator.Dispose();
            }
        Label_0040:
            if (this.pvi == null)
            {
                goto Label_0053;
            }
            this.pvi.Dispose();
        Label_0053:
            return;
        }

        private void Visf_Load(object sender, EventArgs e)
        {
            base.SetStyle(0x10, 1);
            return;
        }

        private Vector3 CameraEye
        {
            get
            {
                return new Vector3(Convert.ToSingle(this.eyeX.Value), Convert.ToSingle(this.eyeY.Value), Convert.ToSingle(this.eyeZ.Value));
            }
            set
            {
                this.eyeX.Value = Math.Max(this.eyeX.Minimum, Math.Min(this.eyeX.Maximum, (decimal) ((float) &value.X)));
                this.eyeY.Value = Math.Max(this.eyeY.Minimum, Math.Min(this.eyeY.Maximum, (decimal) ((float) &value.Y)));
                this.eyeZ.Value = Math.Max(this.eyeZ.Minimum, Math.Min(this.eyeZ.Maximum, (decimal) ((float) &value.Z)));
                return;
            }
        }

        private Vector3 CameraUp
        {
            get
            {
                return Vector3.TransformCoordinate(Vector3.UnitY, Matrix.RotationYawPitchRoll(0f, (Convert.ToSingle(this.pitch.Value) / 180f) * 3.14159f, 0f));
            }
        }

        private Vector3 LeftVec
        {
            get
            {
                return Vector3.TransformCoordinate(Vector3.TransformCoordinate(this.TargetX, Matrix.RotationY(-1.570795f)), Matrix.RotationYawPitchRoll(0f, (Convert.ToSingle(this.pitch.Value) / 180f) * 3.14159f, 0f));
            }
        }

        private PresentParameters PP
        {
            get
            {
                PresentParameters parameters;
                parameters = new PresentParameters();
                parameters.Windowed = 1;
                parameters.SwapEffect = 1;
                parameters.AutoDepthStencilFormat = 0x4d;
                parameters.EnableAutoDepthStencil = 1;
                parameters.BackBufferHeight = 0x400;
                parameters.BackBufferWidth = 0x400;
                return parameters;
            }
        }

        private int Speed
        {
            get
            {
                if ((Control.ModifierKeys & 0x10000) != null)
                {
                    goto Label_0010;
                }
                return 30;
            Label_0010:
                return 60;
            }
        }

        private Vector3 Target
        {
            get
            {
                return Vector3.TransformCoordinate(Vector3.UnitX, Matrix.RotationYawPitchRoll((Convert.ToSingle(this.yaw.Value) / 180f) * 3.14159f, (Convert.ToSingle(this.pitch.Value) / 180f) * 3.14159f, (Convert.ToSingle(this.roll.Value) / 180f) * 3.14159f));
            }
        }

        private Vector3 TargetX
        {
            get
            {
                return Vector3.TransformCoordinate(Vector3.UnitX, Matrix.RotationYawPitchRoll((Convert.ToSingle(this.yaw.Value) / 180f) * 3.14159f, 0f, 0f));
            }
        }

        private delegate void _SetPos(Vector3 v);

        private class Ball
        {
            public Vector3 pos;
            public Vector3 velo;

            public Ball()
            {
                base..ctor();
                return;
            }
        }

        private class CI
        {
            public uint[] ali;
            public int texi;
            public int vifi;

            public CI()
            {
                base..ctor();
                return;
            }
        }

        [Flags]
        private enum Keyrun
        {
            A = 4,
            D = 8,
            Down = 0x20,
            None = 0,
            S = 2,
            Up = 0x10,
            W = 1
        }

        private class LMap
        {
            public byte[] al;

            public LMap()
            {
                int num;
                this.al = new byte[0x100];
                base..ctor();
                num = 0;
                goto Label_0034;
            Label_001A:
                this.al[num] = (byte) Math.Min(0xff, 2 * num);
                num += 1;
            Label_0034:
                if (num < 0x100)
                {
                    goto Label_001A;
                }
                return;
            }
        }

        private class Mkbpy
        {
            private List<int> alRefMati;
            private int cntv;
            private int i;
            private Matrix mtxLoc2Blender;
            private int uvi;
            private StringWriter uvs;
            private string vcoords;
            private StringWriter vcs;
            private string vfaces;
            private StringWriter wr;

            public Mkbpy()
            {
                float num;
                this.wr = new StringWriter();
                this.vcoords = "";
                this.vfaces = "";
                this.uvs = new StringWriter();
                this.alRefMati = new List<int>();
                this.vcs = new StringWriter();
                base..ctor();
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
                num = 0.01f;
                this.mtxLoc2Blender = Matrix.Multiply(this.mtxLoc2Blender, Matrix.Scaling(-num, num, num));
                return;
            }

            public unsafe void AddColorVtx(Color[] clrs)
            {
                int num;
                num = 0;
                goto Label_00D3;
            Label_0007:
                this.vcs.WriteLine("me.faces[{0}].col[{1}].a = {2}", (int) this.uvi, (int) num, (byte) &(clrs[num]).A);
                this.vcs.WriteLine("me.faces[{0}].col[{1}].r = {2}", (int) this.uvi, (int) num, (byte) &(clrs[num]).R);
                this.vcs.WriteLine("me.faces[{0}].col[{1}].g = {2}", (int) this.uvi, (int) num, (byte) &(clrs[num]).G);
                this.vcs.WriteLine("me.faces[{0}].col[{1}].b = {2}", (int) this.uvi, (int) num, (byte) &(clrs[num]).B);
                num += 1;
            Label_00D3:
                if (num < ((int) clrs.Length))
                {
                    goto Label_0007;
                }
                return;
            }

            public void AddTex(string fp, string tid, string mid)
            {
                this.wr.WriteLine("img = Blender.Image.Load('{0}')", fp.Replace(@"\", "/"));
                this.wr.WriteLine("tex = Blender.Texture.New('{0}')", tid);
                this.wr.WriteLine("tex.image = img");
                this.wr.WriteLine("mat = Blender.Material.New('{0}')", mid);
                this.wr.WriteLine("mat.setTexture(0, tex, Blender.Texture.TexCo.UV, Blender.Texture.MapTo.COL)");
                this.wr.WriteLine("mat.setMode('Shadeless')");
                this.wr.WriteLine("mats += [mat]");
                this.wr.WriteLine("imgs += [img]");
                return;
            }

            public void AddTuv(int texi, float tu0, float tv0, float tu1, float tv1, float tu2, float tv2)
            {
                int num;
                object[] objArray;
                if (this.alRefMati.IndexOf(texi) >= 0)
                {
                    goto Label_001B;
                }
                this.alRefMati.Add(texi);
            Label_001B:
                num = this.alRefMati.IndexOf(texi);
                this.uvs.WriteLine("me.faces[{0}].uv = [Blender.Mathutils.Vector({1:0.000},{2:0.000}),Blender.Mathutils.Vector({3:0.000},{4:0.000}),Blender.Mathutils.Vector({5:0.000},{6:0.000}),]", new object[] { (int) this.uvi, (float) tu0, (float) tv0, (float) tu1, (float) tv1, (float) tu2, (float) tv2 });
                this.uvs.WriteLine("me.faces[{0}].mat = {1}", (int) this.uvi, (int) num);
                this.uvs.WriteLine("me.faces[{0}].image = imgs[{1}]", (int) this.uvi, (int) texi);
                this.uvi += 1;
                return;
            }

            public unsafe void AddV(CustomVertex.PositionColoredTextured v)
            {
                Vector3 vector;
                if ((this.vcoords != "") == null)
                {
                    goto Label_0028;
                }
                this.vcoords = this.vcoords + ",";
            Label_0028:
                vector = Vector3.TransformCoordinate(&v.Position, this.mtxLoc2Blender);
                this.vcoords = this.vcoords + string.Format("[{0},{1},{2}]", (float) &vector.X, (float) &vector.Y, (float) &vector.Z);
                this.cntv += 1;
                return;
            }

            public void EndMesh(int vifi)
            {
                int num;
                string str;
                string str2;
                int num2;
                if (this.cntv != null)
                {
                    goto Label_0009;
                }
                return;
            Label_0009:
                this.vfaces = "";
                num = 0;
                goto Label_007B;
            Label_0018:
                if ((this.vfaces != "") == null)
                {
                    goto Label_0040;
                }
                this.vfaces = this.vfaces + ",";
            Label_0040:
                this.vfaces = this.vfaces + string.Format("[{0},{1},{2}]", (int) (3 * num), (int) ((3 * num) + 1), (int) ((3 * num) + 2));
                num += 1;
            Label_007B:
                if (num < (this.cntv / 3))
                {
                    goto Label_0018;
                }
                str = string.Format("vifpkt{0:0000}-mesh", (int) vifi);
                str2 = string.Format("vifpkt{0:0000}-obj{1}", (int) vifi, (int) this.i);
                this.i += 1;
                this.wr.WriteLine("coords = [" + this.vcoords + "]");
                this.wr.WriteLine("faces = [" + this.vfaces + "]");
                this.wr.WriteLine("me = Blender.Mesh.New('" + str + "')");
                this.wr.WriteLine("me.verts.extend(coords)");
                this.wr.WriteLine("me.faces.extend(faces)");
                this.wr.WriteLine("me.faceUV = True");
                num2 = 0;
                goto Label_0175;
            Label_0150:
                this.wr.WriteLine("me.materials += [mats[{0}]]", (int) this.alRefMati[num2]);
                num2 += 1;
            Label_0175:
                if (num2 < this.alRefMati.Count)
                {
                    goto Label_0150;
                }
                this.wr.Write(this.uvs.ToString());
                this.wr.WriteLine("me.vertexColors = True");
                this.wr.Write(this.vcs.ToString());
                this.wr.WriteLine("ob = scene.objects.new(me, '" + str2 + "')");
                this.wr.WriteLine("");
                return;
            }

            public void EndTex()
            {
            }

            public void Finish()
            {
                this.wr.WriteLine("print \"-- Ended importing \"");
                return;
            }

            public void StartMesh()
            {
                this.vcoords = "";
                this.cntv = 0;
                this.uvs = new StringWriter();
                this.uvi = 0;
                this.alRefMati.Clear();
                this.vcs = new StringWriter();
                return;
            }

            public void StartTex()
            {
                this.wr.WriteLine("imgs = []");
                this.wr.WriteLine("mats = []");
                return;
            }

            public override string ToString()
            {
                return this.wr.ToString();
            }
        }

        private class RIB
        {
            public int cnt;
            public Guid dcId;
            public IndexBuffer ib;
            public string name;
            public bool render;
            public int texi;
            public int vifi;

            public RIB()
            {
                this.render = 1;
                this.name = "";
                base..ctor();
                return;
            }
        }

        private class VUt
        {
            public List<CustomVertex.PositionNormalColored> alv;
            public CustomVertex.PositionNormalColored[] alvtmp;

            public VUt()
            {
                this.alv = new List<CustomVertex.PositionNormalColored>();
                base..ctor();
                return;
            }

            public unsafe void AddTri(Vector4 w0, Vector4 w1, Vector4 w2, int clr)
            {
                Vector3 vector;
                Vector3 vector2;
                Vector3 vector3;
                Vector3 vector4;
                &vector = new Vector3(&w0.X, &w0.Y, &w0.Z);
                &vector2 = new Vector3(&w1.X, &w1.Y, &w1.Z);
                &vector3 = new Vector3(&w2.X, &w2.Y, &w2.Z);
                vector4 = Vector3.Cross(vector2 - vector, vector2 - vector3);
                this.alv.Add(new CustomVertex.PositionNormalColored(vector, vector4, clr));
                this.alv.Add(new CustomVertex.PositionNormalColored(vector2, vector4, clr));
                this.alv.Add(new CustomVertex.PositionNormalColored(vector3, vector4, clr));
                return;
            }

            public void Clear()
            {
                this.alv.Clear();
                this.alvtmp = null;
                return;
            }
        }
    }
}

