using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hex04BinTrack;
using khiiMapv.CollTest;
using khiiMapv.Put;
using SlimDX;
using SlimDX.Direct3D9;

namespace khiiMapv
{
    partial class Visf
    {
        private List<Ball> alBall = new List<Ball>();
        private List<ComObject> alDeleter = new List<ComObject>();
        private List<CI[]> alalci = new List<CI[]>();
        private List<CI> alci = new List<CI>();
        private List<DC> aldc;
        private List<RIB> alib = new List<RIB>();
        private List<Putfragment> alpf = new List<Putfragment>();
        private List<Texture> altex = new List<Texture>();
        private CheckBox cbFog;
        private CheckBox cbVertexColor;
        private int cntVerts;
        private CollReader coll;
        private IContainer components;
        private Point curs = Point.Empty;
        private Device device;
        private NumericUpDown eyeX;
        private NumericUpDown eyeY;
        private NumericUpDown eyeZ;
        private Point firstcur = Point.Empty;
        private FlowLayoutPanel flppos;
        private NumericUpDown fov;
        private Keyrun kr;
        private ToolStripLabel lCntTris;
        private ToolStripLabel lCntVert;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private LMap lm = new LMap();
        private UC p1;
        private Direct3D p3D;
        private NumericUpDown pitch;
        private Putbox putb = new Putbox();
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
        private VUt vut = new VUt();
        private NumericUpDown yaw;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            var componentResourceManager = new ComponentResourceManager(typeof(Visf));
            label1 = new Label();
            toolStrip1 = new ToolStrip();
            tsbExpBlenderpy = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            lCntVert = new ToolStripLabel();
            toolStripLabel1 = new ToolStripLabel();
            lCntTris = new ToolStripLabel();
            toolStripLabel2 = new ToolStripLabel();
            toolStripSeparator2 = new ToolStripSeparator();
            tslMdls = new ToolStripLabel();
            tsbShowColl = new ToolStripButton();
            tsbBallGame = new ToolStripButton();
            flppos = new FlowLayoutPanel();
            label2 = new Label();
            eyeX = new NumericUpDown();
            eyeY = new NumericUpDown();
            eyeZ = new NumericUpDown();
            label3 = new Label();
            fov = new NumericUpDown();
            label4 = new Label();
            yaw = new NumericUpDown();
            pitch = new NumericUpDown();
            roll = new NumericUpDown();
            cbFog = new CheckBox();
            cbVertexColor = new CheckBox();
            timerRun = new Timer(components);
            label5 = new Label();
            label6 = new Label();
            p1 = new UC();
            timerBall = new Timer(components);
            toolStrip1.SuspendLayout();
            flppos.SuspendLayout();
            ((ISupportInitialize)eyeX).BeginInit();
            ((ISupportInitialize)eyeY).BeginInit();
            ((ISupportInitialize)eyeZ).BeginInit();
            ((ISupportInitialize)fov).BeginInit();
            ((ISupportInitialize)yaw).BeginInit();
            ((ISupportInitialize)pitch).BeginInit();
            ((ISupportInitialize)roll).BeginInit();
            base.SuspendLayout();
            label1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            label1.AutoSize = true;
            label1.Location = new Point(12, 674);
            label1.Name = "label1";
            label1.Size = new Size(193, 48);
            label1.TabIndex = 1;
            label1.Text =
                "* Mouse wheel: Zoom\r\n* Left btn drag: Rotate\r\n* Right btn drag: Move forward/back\r\n* Middle btn: toss ball";
            toolStrip1.Items.AddRange(new ToolStripItem[]
            {
                tsbExpBlenderpy,
                toolStripSeparator1,
                lCntVert,
                toolStripLabel1,
                lCntTris,
                toolStripLabel2,
                toolStripSeparator2,
                tslMdls,
                tsbShowColl,
                tsbBallGame
            });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(885, 25);
            toolStrip1.TabIndex = 2;
            toolStrip1.Text = "toolStrip1";
            tsbExpBlenderpy.Image = (Image)componentResourceManager.GetObject("tsbExpBlenderpy.Image");
            tsbExpBlenderpy.ImageTransparentColor = Color.Magenta;
            tsbExpBlenderpy.Name = "tsbExpBlenderpy";
            tsbExpBlenderpy.Size = new Size(169, 22);
            tsbExpBlenderpy.Text = "Export to blender script ";
            tsbExpBlenderpy.Click += tsbExpBlenderpy_Click;
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            lCntVert.Name = "lCntVert";
            lCntVert.Size = new Size(15, 22);
            lCntVert.Text = "0";
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(62, 22);
            toolStripLabel1.Text = "vertices, ";
            lCntTris.Name = "lCntTris";
            lCntTris.Size = new Size(15, 22);
            lCntTris.Text = "0";
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(31, 22);
            toolStripLabel2.Text = "tris.";
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            tslMdls.Name = "tslMdls";
            tslMdls.Size = new Size(53, 22);
            tslMdls.Text = "Models:";
            tsbShowColl.CheckOnClick = true;
            tsbShowColl.Image = (Image)componentResourceManager.GetObject("tsbShowColl.Image");
            tsbShowColl.ImageTransparentColor = Color.Magenta;
            tsbShowColl.Name = "tsbShowColl";
            tsbShowColl.Size = new Size(75, 22);
            tsbShowColl.Text = "Collision";
            tsbShowColl.Click += tsbShowColl_Click;
            tsbBallGame.CheckOnClick = true;
            tsbBallGame.Image = (Image)componentResourceManager.GetObject("tsbBallGame.Image");
            tsbBallGame.ImageTransparentColor = Color.Magenta;
            tsbBallGame.Name = "tsbBallGame";
            tsbBallGame.Size = new Size(86, 22);
            tsbBallGame.Text = "Ball game";
            tsbBallGame.Click += tsbBallGame_Click;
            flppos.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            flppos.Controls.Add(label2);
            flppos.Controls.Add(eyeX);
            flppos.Controls.Add(eyeY);
            flppos.Controls.Add(eyeZ);
            flppos.Controls.Add(label3);
            flppos.Controls.Add(fov);
            flppos.Controls.Add(label4);
            flppos.Controls.Add(yaw);
            flppos.Controls.Add(pitch);
            flppos.Controls.Add(roll);
            flppos.Controls.Add(cbFog);
            flppos.Controls.Add(cbVertexColor);
            flppos.Location = new Point(12, 641);
            flppos.Name = "flppos";
            flppos.Size = new Size(873, 30);
            flppos.TabIndex = 3;
            label2.AutoSize = true;
            label2.Location = new Point(3, 0);
            label2.Name = "label2";
            label2.Size = new Size(60, 12);
            label2.TabIndex = 0;
            label2.Text = "eye (x y z)";
            eyeX.Location = new Point(69, 3);
            NumericUpDown arg_7C6_0 = eyeX;
            var array = new int[4];
            array[0] = 64000;
            arg_7C6_0.Maximum = new decimal(array);
            eyeX.Minimum = new decimal(new[]
            {
                64000,
                0,
                0,
                -2147483648
            });
            eyeX.Name = "eyeX";
            eyeX.Size = new Size(59, 19);
            eyeX.TabIndex = 1;
            eyeX.TextAlign = HorizontalAlignment.Right;
            eyeX.ValueChanged += eyeX_ValueChanged;
            eyeY.Location = new Point(134, 3);
            NumericUpDown arg_87A_0 = eyeY;
            var array2 = new int[4];
            array2[0] = 64000;
            arg_87A_0.Maximum = new decimal(array2);
            eyeY.Minimum = new decimal(new[]
            {
                64000,
                0,
                0,
                -2147483648
            });
            eyeY.Name = "eyeY";
            eyeY.Size = new Size(59, 19);
            eyeY.TabIndex = 2;
            eyeY.TextAlign = HorizontalAlignment.Right;
            NumericUpDown arg_905_0 = eyeY;
            var array3 = new int[4];
            array3[0] = 500;
            arg_905_0.Value = new decimal(array3);
            eyeY.ValueChanged += eyeX_ValueChanged;
            eyeZ.Location = new Point(199, 3);
            NumericUpDown arg_955_0 = eyeZ;
            var array4 = new int[4];
            array4[0] = 64000;
            arg_955_0.Maximum = new decimal(array4);
            eyeZ.Minimum = new decimal(new[]
            {
                64000,
                0,
                0,
                -2147483648
            });
            eyeZ.Name = "eyeZ";
            eyeZ.Size = new Size(59, 19);
            eyeZ.TabIndex = 3;
            eyeZ.TextAlign = HorizontalAlignment.Right;
            eyeZ.ValueChanged += eyeX_ValueChanged;
            label3.AutoSize = true;
            label3.Location = new Point(264, 0);
            label3.Name = "label3";
            label3.Size = new Size(21, 12);
            label3.TabIndex = 4;
            label3.Text = "fov";
            fov.Location = new Point(291, 3);
            NumericUpDown arg_A6F_0 = fov;
            var array5 = new int[4];
            array5[0] = 180;
            arg_A6F_0.Maximum = new decimal(array5);
            fov.Name = "fov";
            fov.Size = new Size(59, 19);
            fov.TabIndex = 5;
            fov.TextAlign = HorizontalAlignment.Right;
            NumericUpDown arg_ACB_0 = fov;
            var array6 = new int[4];
            array6[0] = 70;
            arg_ACB_0.Value = new decimal(array6);
            fov.ValueChanged += eyeX_ValueChanged;
            label4.AutoSize = true;
            label4.Location = new Point(356, 0);
            label4.Name = "label4";
            label4.Size = new Size(125, 12);
            label4.TabIndex = 6;
            label4.Text = "rotation (yaw pitch roll)";
            yaw.Location = new Point(487, 3);
            NumericUpDown arg_B7D_0 = yaw;
            var array7 = new int[4];
            array7[0] = 36000;
            arg_B7D_0.Maximum = new decimal(array7);
            yaw.Minimum = new decimal(new[]
            {
                36000,
                0,
                0,
                -2147483648
            });
            yaw.Name = "yaw";
            yaw.Size = new Size(59, 19);
            yaw.TabIndex = 7;
            yaw.TextAlign = HorizontalAlignment.Right;
            yaw.ValueChanged += eyeX_ValueChanged;
            pitch.Location = new Point(552, 3);
            NumericUpDown arg_C35_0 = pitch;
            var array8 = new int[4];
            array8[0] = 36000;
            arg_C35_0.Maximum = new decimal(array8);
            pitch.Minimum = new decimal(new[]
            {
                36000,
                0,
                0,
                -2147483648
            });
            pitch.Name = "pitch";
            pitch.Size = new Size(59, 19);
            pitch.TabIndex = 8;
            pitch.TextAlign = HorizontalAlignment.Right;
            pitch.ValueChanged += eyeX_ValueChanged;
            roll.Location = new Point(617, 3);
            NumericUpDown arg_CED_0 = roll;
            var array9 = new int[4];
            array9[0] = 36000;
            arg_CED_0.Maximum = new decimal(array9);
            roll.Minimum = new decimal(new[]
            {
                36000,
                0,
                0,
                -2147483648
            });
            roll.Name = "roll";
            roll.Size = new Size(59, 19);
            roll.TabIndex = 9;
            roll.TextAlign = HorizontalAlignment.Right;
            roll.ValueChanged += eyeX_ValueChanged;
            cbFog.AutoSize = true;
            cbFog.Checked = true;
            cbFog.CheckState = CheckState.Checked;
            cbFog.Location = new Point(682, 3);
            cbFog.Name = "cbFog";
            cbFog.Size = new Size(64, 16);
            cbFog.TabIndex = 10;
            cbFog.Text = "Use &fog";
            cbFog.UseVisualStyleBackColor = true;
            cbFog.CheckedChanged += cbFog_CheckedChanged;
            cbVertexColor.AutoSize = true;
            cbVertexColor.Checked = true;
            cbVertexColor.CheckState = CheckState.Checked;
            cbVertexColor.Location = new Point(752, 3);
            cbVertexColor.Name = "cbVertexColor";
            cbVertexColor.Size = new Size(97, 16);
            cbVertexColor.TabIndex = 11;
            cbVertexColor.Text = "Use vertex &clr";
            cbVertexColor.UseVisualStyleBackColor = true;
            cbVertexColor.CheckedChanged += cbFog_CheckedChanged;
            timerRun.Interval = 25;
            timerRun.Tick += timerRun_Tick;
            label5.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            label5.AutoSize = true;
            label5.Location = new Point(229, 675);
            label5.Name = "label5";
            label5.Size = new Size(99, 48);
            label5.TabIndex = 1;
            label5.Text = "* W: move forward\r\n* S: move back\r\n* A: move left\r\n* D: move right";
            label6.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            label6.AutoSize = true;
            label6.Location = new Point(353, 675);
            label6.Name = "label6";
            label6.Size = new Size(106, 36);
            label6.TabIndex = 1;
            label6.Text = "* Shift: Move fast\r\n* Up: move up\r\n* Down: move down";
            p1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            p1.BackColor = SystemColors.ControlDarkDark;
            p1.BorderStyle = BorderStyle.FixedSingle;
            p1.Location = new Point(12, 28);
            p1.Name = "p1";
            p1.Size = new Size(861, 607);
            p1.TabIndex = 0;
            p1.UseTransparent = true;
            p1.Load += p1_Load;
            p1.Paint += p1_Paint;
            p1.PreviewKeyDown += p1_PreviewKeyDown;
            p1.MouseMove += p1_MouseMove;
            p1.KeyUp += p1_KeyUp;
            p1.MouseDown += p1_MouseDown;
            p1.MouseUp += p1_MouseUp;
            p1.SizeChanged += p1_SizeChanged;
            p1.KeyDown += p1_KeyDown;
            timerBall.Interval = 50;
            timerBall.Tick += timerBall_Tick;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(885, 732);
            base.Controls.Add(flppos);
            base.Controls.Add(toolStrip1);
            base.Controls.Add(label6);
            base.Controls.Add(label5);
            base.Controls.Add(label1);
            base.Controls.Add(p1);
            base.Name = "Visf";
            Text = "map viewer test";
            base.Load += Visf_Load;
            base.FormClosing += Visf_FormClosing;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            flppos.ResumeLayout(false);
            flppos.PerformLayout();
            ((ISupportInitialize)eyeX).EndInit();
            ((ISupportInitialize)eyeY).EndInit();
            ((ISupportInitialize)eyeZ).EndInit();
            ((ISupportInitialize)fov).EndInit();
            ((ISupportInitialize)yaw).EndInit();
            ((ISupportInitialize)pitch).EndInit();
            ((ISupportInitialize)roll).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}
