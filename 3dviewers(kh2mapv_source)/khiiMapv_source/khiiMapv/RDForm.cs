namespace khiiMapv
{
    using khiiMapv.CollTest;
    using khiiMapv.Parse02;
    using khiiMapv.Pax;
    using khiiMapv.Properties;
    using khkh_xldM;
    using khkh_xldMii.Mc;
    using khkh_xldMii.Mx;
    using ParseAI;
    using Readmset;
    using SlimDX;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using vcBinTex4;

    public class RDForm : Form
    {
        private List<DC> aldc;
        private const int BASE = 0x90;
        private LinkLabel bBatExp;
        private Button bExportAll;
        private Button bExportBin;
        private Button bserr;
        private Button button1;
        private CollReader coll;
        private IContainer components;
        private SortedDictionary<int, string> dictObjName;
        private ListDictionary errcol;
        private string fpld;
        private string fpread;
        private HexVwer hv1;
        private ImageList IL;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label labelHelpMore;
        private Label labelPixi;
        private LinkLabel linkLabel1;
        private ListView lvMI;
        private Panel p1;
        private byte[] rdbin;
        private TreeView treeView1;
        private Visf visf;
        private WavePlayerDelegate WavePlayer;

        public RDForm()
        {
            this.rdbin = new byte[0];
            this.aldc = new List<DC>();
            this.coll = new CollReader();
            this.dictObjName = new SortedDictionary<int, string>();
            this.errcol = new ListDictionary();
            base..ctor();
            this.InitializeComponent();
            return;
        }

        public RDForm(string fpread)
        {
            this.rdbin = new byte[0];
            this.aldc = new List<DC>();
            this.coll = new CollReader();
            this.dictObjName = new SortedDictionary<int, string>();
            this.errcol = new ListDictionary();
            base..ctor();
            this.fpread = fpread;
            this.InitializeComponent();
            return;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (this.fpld == null)
            {
                goto Label_0032;
            }
            Application.Idle -= new EventHandler(this.Application_Idle);
            this.LoadMap(this.WraponDemand(this.fpld));
            this.fpld = null;
        Label_0032:
            return;
        }

        private void bBatExp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BEXForm form;
            form = new BEXForm(this);
            form.Show();
            return;
        }

        private void bExportAll_Click(object sender, EventArgs e)
        {
            string str;
            if (MessageBox.Show(this, "Are you really explode all?", Application.ProductName, 4, 0x30) == 6)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            str = Path.Combine(Application.StartupPath, @"export\" + Path.GetFileName(this.OpenedPath).Replace(".", "_"));
            this.Expall(this.treeView1.Nodes, str);
            Process.Start("explorer.exe", " \"" + str + "\"");
            return;
        }

        private void bExportBin_Click(object sender, EventArgs e)
        {
            TreeNode node;
            Hexi hexi;
            SaveFileDialog dialog;
            FileStream stream;
            node = this.treeView1.SelectedNode;
            if (node != null)
            {
                goto Label_0024;
            }
            MessageBox.Show(this, "No item selected!", Application.ProductName, 0, 0x30);
            return;
        Label_0024:
            hexi = node.Tag as Hexi;
            if (hexi != null)
            {
                goto Label_0048;
            }
            MessageBox.Show(this, "Export not supported!", Application.ProductName, 0, 0x30);
            return;
        Label_0048:
            if (hexi.len != null)
            {
                goto Label_0065;
            }
            MessageBox.Show(this, "It does not declare bin LENGTH!", Application.ProductName, 0, 0x30);
            return;
        Label_0065:
            dialog = new SaveFileDialog();
            dialog.FileName = node.Text + ".bin";
            dialog.Filter = "*.bin|*.bin||";
            if (dialog.ShowDialog(this) != 1)
            {
                goto Label_00E0;
            }
            stream = File.Create(dialog.FileName);
        Label_00A2:
            try
            {
                stream.Write(this.rdbin, hexi.off, hexi.len);
                stream.Close();
                goto Label_00CC;
            }
            finally
            {
            Label_00C2:
                if (stream == null)
                {
                    goto Label_00CB;
                }
                stream.Dispose();
            Label_00CB:;
            }
        Label_00CC:
            MessageBox.Show(this, "Saved!", Application.ProductName, 0, 0x40);
        Label_00E0:
            return;
        }

        private void bserr_Click(object sender, EventArgs e)
        {
            Form form;
            TextBox box;
            Button button;
            form = new Form();
            form.StartPosition = 0;
            form.Width = base.Width;
            form.Height = base.Height / 4;
            form.Location = this.bserr.PointToScreen(new Point(0, this.bserr.Height));
            form.FormBorderStyle = 6;
            form.Text = "Error message from analiser";
            form.Show(this);
            box = new TextBox();
            box.BorderStyle = 0;
            box.Multiline = 1;
            box.Parent = form;
            box.Dock = 5;
            box.ScrollBars = 2;
            box.BackColor = Color.FromKnownColor(0x13);
            box.ForeColor = Color.FromKnownColor(20);
            box.Text = ((Exception) this.bserr.Tag).ToString();
            box.Show();
            button = new Button();
            button.Click += new EventHandler(this.buttonCloseMe_Click);
            button.Parent = form;
            button.Text = "Close!";
            form.AcceptButton = button;
            form.CancelButton = button;
            form.Activate();
            box.Select();
            box.SelectAll();
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.visf = new Visf(this.aldc, this.coll);
            this.visf.Show();
            return;
        }

        private void buttonCloseMe_Click(object sender, EventArgs e)
        {
            ((Form) ((Button) sender).Parent).Close();
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

        private void Expall(TreeNodeCollection tnc, string dirTo)
        {
            TreeNode node;
            UniName name;
            TreeNode node2;
            IEnumerator enumerator;
            IEnumerator enumerator2;
            IDisposable disposable;
            IDisposable disposable2;
            enumerator = tnc.GetEnumerator();
        Label_0007:
            try
            {
                goto Label_0060;
            Label_0009:
                node = (TreeNode) enumerator.Current;
                name = new UniName();
                enumerator2 = node.Nodes.GetEnumerator();
            Label_0028:
                try
                {
                    goto Label_0040;
                Label_002A:
                    node2 = (TreeNode) enumerator2.Current;
                    this.Expthem(dirTo, node2, name);
                Label_0040:
                    if (enumerator2.MoveNext() != null)
                    {
                        goto Label_002A;
                    }
                    goto Label_0060;
                }
                finally
                {
                Label_004B:
                    disposable = enumerator2 as IDisposable;
                    if (disposable == null)
                    {
                        goto Label_005F;
                    }
                    disposable.Dispose();
                Label_005F:;
                }
            Label_0060:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0009;
                }
                goto Label_007E;
            }
            finally
            {
            Label_006A:
                disposable2 = enumerator as IDisposable;
                if (disposable2 == null)
                {
                    goto Label_007D;
                }
                disposable2.Dispose();
            Label_007D:;
            }
        Label_007E:
            return;
        }

        public void ExpallTo(string dirTo)
        {
            this.Expall(this.treeView1.Nodes, dirTo);
            return;
        }

        private void Expthem(string dirTo, TreeNode tn, UniName un)
        {
            Control control;
            PictureBox box;
            TextBox box2;
            int num;
            Control control2;
            PictureBox box3;
            Hexi hexi;
            FileStream stream;
            UniName name;
            string str;
            TreeNode node;
            <>c__DisplayClass1 class2;
            IEnumerator enumerator;
            IEnumerator enumerator2;
            object[] objArray;
            IDisposable disposable;
            IDisposable disposable2;
            IEnumerator enumerator3;
            IDisposable disposable3;
            class2 = new <>c__DisplayClass1();
            class2.dirTo = dirTo;
            class2.un = un;
            goto Label_002F;
        Label_0019:
            this.p1.Controls[0].Dispose();
        Label_002F:
            if (this.p1.Controls.Count != null)
            {
                goto Label_0019;
            }
            class2.dirn = class2.un.Get(tn.Text);
            this.WavePlayer = new WavePlayerDelegate(class2.<Expthem>b__0);
            this.treeView1.SelectedNode = tn;
            this.treeView1_AfterSelect(this.treeView1, new TreeViewEventArgs(tn, 1));
            base.Update();
            this.WavePlayer = null;
            enumerator = this.p1.Controls.GetEnumerator();
        Label_00AB:
            try
            {
                goto Label_0239;
            Label_00B0:
                control = (Control) enumerator.Current;
                if ((control as PictureBox) == null)
                {
                    goto Label_0112;
                }
                Directory.CreateDirectory(class2.dirTo);
                box = (PictureBox) control;
                box.Image.Save(Path.Combine(class2.dirTo, class2.un.Get(class2.dirn + ".png")), ImageFormat.Png);
            Label_0112:
                if ((control as TextBox) == null)
                {
                    goto Label_0167;
                }
                Directory.CreateDirectory(class2.dirTo);
                box2 = (TextBox) control;
                File.WriteAllText(Path.Combine(class2.dirTo, class2.un.Get(class2.dirn + ".txt")), box2.Text, Encoding.Default);
            Label_0167:
                if ((control as FlowLayoutPanel) == null)
                {
                    goto Label_0239;
                }
                num = 1;
                enumerator2 = control.Controls.GetEnumerator();
            Label_0181:
                try
                {
                    goto Label_0216;
                Label_0186:
                    control2 = (Control) enumerator2.Current;
                    if ((control2 as PictureBox) == null)
                    {
                        goto Label_0216;
                    }
                    Directory.CreateDirectory(class2.dirTo);
                    box3 = (PictureBox) control2;
                    box3.Image.Save(Path.Combine(class2.dirTo, class2.un.Get(string.Concat(new object[] { class2.dirn, "_", (int) num, ".png" }))), ImageFormat.Png);
                    num += 1;
                Label_0216:
                    if (enumerator2.MoveNext() != null)
                    {
                        goto Label_0186;
                    }
                    goto Label_0239;
                }
                finally
                {
                Label_0224:
                    disposable = enumerator2 as IDisposable;
                    if (disposable == null)
                    {
                        goto Label_0238;
                    }
                    disposable.Dispose();
                Label_0238:;
                }
            Label_0239:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_00B0;
                }
                goto Label_025C;
            }
            finally
            {
            Label_0247:
                disposable2 = enumerator as IDisposable;
                if (disposable2 == null)
                {
                    goto Label_025B;
                }
                disposable2.Dispose();
            Label_025B:;
            }
        Label_025C:
            hexi = tn.Tag as Hexi;
            if (hexi.len == null)
            {
                goto Label_02DF;
            }
            Directory.CreateDirectory(class2.dirTo);
            stream = File.Create(Path.Combine(class2.dirTo, class2.un.Get(class2.dirn + ".bin")));
        Label_02AF:
            try
            {
                stream.Write(this.rdbin, hexi.off, hexi.len);
                stream.Close();
                goto Label_02DF;
            }
            finally
            {
            Label_02D3:
                if (stream == null)
                {
                    goto Label_02DE;
                }
                stream.Dispose();
            Label_02DE:;
            }
        Label_02DF:
            name = new UniName();
            str = name.Get(class2.dirn);
            enumerator3 = tn.Nodes.GetEnumerator();
        Label_0303:
            try
            {
                goto Label_032B;
            Label_0305:
                node = (TreeNode) enumerator3.Current;
                this.Expthem(Path.Combine(class2.dirTo, str), node, name);
            Label_032B:
                if (enumerator3.MoveNext() != null)
                {
                    goto Label_0305;
                }
                goto Label_034B;
            }
            finally
            {
            Label_0336:
                disposable3 = enumerator3 as IDisposable;
                if (disposable3 == null)
                {
                    goto Label_034A;
                }
                disposable3.Dispose();
            Label_034A:;
            }
        Label_034B:
            return;
        }

        private unsafe string GetObjName(int modelId)
        {
            string str;
            if (this.dictObjName.TryGetValue(modelId, &str) == null)
            {
                goto Label_0012;
            }
            return str;
        Label_0012:
            return null;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager;
            this.components = new Container();
            manager = new ComponentResourceManager(typeof(RDForm));
            this.treeView1 = new TreeView();
            this.IL = new ImageList(this.components);
            this.linkLabel1 = new LinkLabel();
            this.p1 = new Panel();
            this.lvMI = new ListView();
            this.button1 = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.bserr = new Button();
            this.label3 = new Label();
            this.labelPixi = new Label();
            this.labelHelpMore = new Label();
            this.bExportBin = new Button();
            this.hv1 = new HexVwer();
            this.bExportAll = new Button();
            this.bBatExp = new LinkLabel();
            base.SuspendLayout();
            this.treeView1.Font = new Font("Microsoft Sans Serif", 12f, 0, 3, 0);
            this.treeView1.HideSelection = 0;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.IL;
            this.treeView1.Location = new Point(12, 0x17);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new Size(0xf6, 0x107);
            this.treeView1.TabIndex = 1;
            this.treeView1.DoubleClick += new EventHandler(this.treeView1_DoubleClick);
            this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
            this.IL.ImageStream = (ImageListStreamer) manager.GetObject("IL.ImageStream");
            this.IL.TransparentColor = Color.Magenta;
            this.IL.Images.SetKeyName(0, "RightArrowHS.png");
            this.IL.Images.SetKeyName(1, "RightsRestrictedHS.png");
            this.IL.Images.SetKeyName(2, "SpeechMicHS.png");
            this.linkLabel1.AutoSize = 1;
            this.linkLabel1.Location = new Point(12, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new Size(0x38, 12);
            this.linkLabel1.TabIndex = 0;
            this.linkLabel1.TabStop = 1;
            this.linkLabel1.Text = "linkLabel1";
            this.p1.Anchor = 13;
            this.p1.AutoScroll = 1;
            this.p1.BorderStyle = 1;
            this.p1.Location = new Point(0x108, 0x18);
            this.p1.Name = "p1";
            this.p1.Size = new Size(370, 0x106);
            this.p1.TabIndex = 2;
            this.lvMI.Anchor = 11;
            this.lvMI.HideSelection = 0;
            this.lvMI.Location = new Point(640, 0x142);
            this.lvMI.MultiSelect = 0;
            this.lvMI.Name = "lvMI";
            this.lvMI.Size = new Size(0x35, 0xfc);
            this.lvMI.TabIndex = 10;
            this.lvMI.UseCompatibleStateImageBehavior = 0;
            this.lvMI.View = 3;
            this.lvMI.SelectedIndexChanged += new EventHandler(this.lvMI_SelectedIndexChanged);
            this.button1.Anchor = 9;
            this.button1.FlatStyle = 0;
            this.button1.Location = new Point(640, 0x18);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x3b, 0x38);
            this.button1.TabIndex = 3;
            this.button1.Text = "Map &viewer";
            this.button1.UseVisualStyleBackColor = 1;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label1.Anchor = 6;
            this.label1.AutoSize = 1;
            this.label1.Location = new Point(12, 0x242);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x127, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "* Drop a map file to window. Then click \"viewer\" button.";
            this.label2.Anchor = 6;
            this.label2.AutoSize = 1;
            this.label2.Location = new Point(12, 0x254);
            this.label2.Margin = new Padding(3, 6, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x1a7, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "* Currently accepts map mdlx apdx fm 2dd bar 2ld mset pax wd vsb ard imd mag";
            this.bserr.Image = Resources.ActualSizeHS;
            this.bserr.ImageAlign = 0x40;
            this.bserr.Location = new Point(12, 0x124);
            this.bserr.Name = "bserr";
            this.bserr.Size = new Size(0x83, 0x17);
            this.bserr.TabIndex = 7;
            this.bserr.Text = "Show error popup";
            this.bserr.TextAlign = 0x10;
            this.bserr.UseVisualStyleBackColor = 1;
            this.bserr.Visible = 0;
            this.bserr.Click += new EventHandler(this.bserr_Click);
            this.label3.Anchor = 9;
            this.label3.AutoSize = 1;
            this.label3.Location = new Point(640, 0x53);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x37, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Pixel info:";
            this.labelPixi.Anchor = 9;
            this.labelPixi.AutoSize = 1;
            this.labelPixi.Location = new Point(640, 0x5f);
            this.labelPixi.Name = "labelPixi";
            this.labelPixi.Size = new Size(11, 0x60);
            this.labelPixi.TabIndex = 5;
            this.labelPixi.Text = "...\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n";
            this.labelHelpMore.Font = new Font("ＭＳ ゴシック", 9f, 0, 3, 0x80);
            this.labelHelpMore.Location = new Point(0x95, 0x121);
            this.labelHelpMore.Name = "labelHelpMore";
            this.labelHelpMore.Size = new Size(0x220, 30);
            this.labelHelpMore.TabIndex = 8;
            this.labelHelpMore.Text = "...";
            this.bExportBin.Anchor = 9;
            this.bExportBin.FlatStyle = 0;
            this.bExportBin.Location = new Point(640, 0xc2);
            this.bExportBin.Name = "bExportBin";
            this.bExportBin.Size = new Size(0x3b, 40);
            this.bExportBin.TabIndex = 6;
            this.bExportBin.Text = "&Export bin";
            this.bExportBin.UseVisualStyleBackColor = 1;
            this.bExportBin.Click += new EventHandler(this.bExportBin_Click);
            this.hv1.Anchor = 15;
            this.hv1.AntiFlick = 1;
            this.hv1.BorderStyle = 1;
            this.hv1.ByteWidth = 0x10;
            this.hv1.Font = new Font("Courier New", 9f, 0, 3, 0);
            this.hv1.Location = new Point(12, 0x142);
            this.hv1.Margin = new Padding(3, 4, 3, 4);
            this.hv1.Name = "hv1";
            this.hv1.OffDelta = 0;
            this.hv1.PgScroll = 1;
            this.hv1.Size = new Size(0x26e, 0xfc);
            this.hv1.TabIndex = 9;
            this.hv1.UnitPg = 0x200;
            this.bExportAll.Anchor = 9;
            this.bExportAll.FlatStyle = 0;
            this.bExportAll.Location = new Point(640, 240);
            this.bExportAll.Name = "bExportAll";
            this.bExportAll.Size = new Size(0x3b, 0x2e);
            this.bExportAll.TabIndex = 13;
            this.bExportAll.Text = "Export &all";
            this.bExportAll.UseVisualStyleBackColor = 1;
            this.bExportAll.Click += new EventHandler(this.bExportAll_Click);
            this.bBatExp.AutoSize = 1;
            this.bBatExp.LinkArea = new LinkArea(5, 14);
            this.bBatExp.Location = new Point(0x139, 0x242);
            this.bBatExp.Name = "bBatExp";
            this.bBatExp.Size = new Size(0x68, 0x11);
            this.bBatExp.TabIndex = 14;
            this.bBatExp.TabStop = 1;
            this.bBatExp.Text = "New: &Batch Export!";
            this.bBatExp.UseCompatibleTextRendering = 1;
            this.bBatExp.LinkClicked += new LinkLabelLinkClickedEventHandler(this.bBatExp_LinkClicked);
            this.AllowDrop = 1;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = 1;
            base.ClientSize = new Size(0x2c7, 0x278);
            base.Controls.Add(this.bBatExp);
            base.Controls.Add(this.bExportAll);
            base.Controls.Add(this.bExportBin);
            base.Controls.Add(this.labelHelpMore);
            base.Controls.Add(this.labelPixi);
            base.Controls.Add(this.label3);
            base.Controls.Add(this.bserr);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.lvMI);
            base.Controls.Add(this.p1);
            base.Controls.Add(this.linkLabel1);
            base.Controls.Add(this.treeView1);
            base.Controls.Add(this.hv1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "RDForm";
            this.Text = "map exploring";
            base.Load += new EventHandler(this.RDForm_Load);
            base.DragDrop += new DragEventHandler(this.RDForm_DragDrop);
            base.DragEnter += new DragEventHandler(this.RDForm_DragEnter);
            base.ResumeLayout(0);
            base.PerformLayout();
            return;
        }

        public void LoadAny(string fpld)
        {
            this.LoadMap(this.WraponDemand(fpld));
            return;
        }

        private void LoadMap(string fp)
        {
            TreeNode node;
            DC dc;
            int num;
            ReadBar.Barent barent;
            TreeNode node2;
            NotSupportedException exception;
            ReadBar.Barent[] barentArray;
            int num2;
            int num3;
            string str;
            this.linkLabel1.Text = fp;
            this.rdbin = File.ReadAllBytes(fp);
            this.hv1.SetBin(this.rdbin);
            this.treeView1.Nodes.Clear();
            this.errcol.Clear();
            node = this.treeView1.Nodes.Add(Path.GetExtension(fp));
            node.Tag = new Hexi(0);
            dc = new DC();
            this.aldc.Clear();
            num = 0;
            barentArray = ReadBar.Explode(new MemoryStream(this.rdbin, 0));
            num2 = 0;
            goto Label_02FF;
        Label_0095:
            barent = barentArray[num2];
            node2 = node.Nodes.Add(string.Format("{0} ({1:x2})", barent.id, (int) (barent.k & 0xff)));
            node2.Tag = new Hexi(barent.off, barent.len);
            if (barent.k >= num)
            {
                goto Label_0114;
            }
            if (dc.o4Map != null)
            {
                goto Label_00FA;
            }
            if (dc.o4Mdlx == null)
            {
                goto Label_010E;
            }
        Label_00FA:
            if (dc.o7 == null)
            {
                goto Label_010E;
            }
            this.aldc.Add(dc);
        Label_010E:
            dc = new DC();
        Label_0114:
            num = barent.k;
        Label_011B:
            try
            {
                num3 = barent.k;
                switch ((num3 - 2))
                {
                    case 0:
                        goto Label_02BD;

                    case 1:
                        goto Label_02AC;

                    case 2:
                        goto Label_01C8;

                    case 3:
                        goto Label_02CC;

                    case 4:
                        goto Label_01E9;

                    case 5:
                        goto Label_0240;

                    case 6:
                        goto Label_02CC;

                    case 7:
                        goto Label_02CC;

                    case 8:
                        goto Label_0203;

                    case 9:
                        goto Label_022B;

                    case 10:
                        goto Label_0217;

                    case 11:
                        goto Label_02CC;

                    case 12:
                        goto Label_02CC;

                    case 13:
                        goto Label_022B;

                    case 14:
                        goto Label_02CC;

                    case 15:
                        goto Label_02CC;

                    case 0x10:
                        goto Label_029B;

                    case 0x11:
                        goto Label_022B;

                    case 0x12:
                        goto Label_02CC;

                    case 0x13:
                        goto Label_02CC;

                    case 20:
                        goto Label_02CC;

                    case 0x15:
                        goto Label_02CC;

                    case 0x16:
                        goto Label_0257;
                }
                if (num3 == 0x1d)
                {
                    goto Label_0279;
                }
                switch ((num3 - 0x20))
                {
                    case 0:
                        goto Label_0268;

                    case 1:
                        goto Label_02CC;

                    case 2:
                        goto Label_028A;

                    case 3:
                        goto Label_02CC;

                    case 4:
                        goto Label_01B4;
                }
                goto Label_02CC;
            Label_01B4:
                this.Parse24(node2, barent.off, barent);
                goto Label_02CC;
            Label_01C8:
                this.Parse4(node2, barent.off, barent, dc);
                dc.name = barent.id;
                goto Label_02CC;
            Label_01E9:
                this.coll = this.Parse6(node2, barent.off, barent);
                goto Label_02CC;
            Label_0203:
                this.Parse0a(node2, barent.off, barent);
                goto Label_02CC;
            Label_0217:
                this.Parse0c(node2, barent.off, barent);
                goto Label_02CC;
            Label_022B:
                this.Parse6(node2, barent.off, barent);
                goto Label_02CC;
            Label_0240:
                dc.o7 = this.ParseTex(node2, barent.off, barent);
                goto Label_02CC;
            Label_0257:
                this.Parse18(node2, barent.off, barent);
                goto Label_02CC;
            Label_0268:
                this.Parse20(node2, barent.off, barent);
                goto Label_02CC;
            Label_0279:
                this.Parse1D(node2, barent.off, barent);
                goto Label_02CC;
            Label_028A:
                this.Parse22(node2, barent.off, barent);
                goto Label_02CC;
            Label_029B:
                this.Parse12(node2, barent.off, barent);
                goto Label_02CC;
            Label_02AC:
                this.Parse03(node2, barent.off, barent);
                goto Label_02CC;
            Label_02BD:
                this.Parse02(node2, barent.off, barent);
            Label_02CC:
                goto Label_02F9;
            }
            catch (NotSupportedException exception1)
            {
            Label_02CE:
                exception = exception1;
                this.errcol[node2] = exception;
                node2.ImageKey = node2.SelectedImageKey = "RightsRestrictedHS.png";
                goto Label_02F9;
            }
        Label_02F9:
            num2 += 1;
        Label_02FF:
            if (num2 < ((int) barentArray.Length))
            {
                goto Label_0095;
            }
            if (dc.o4Map != null)
            {
                goto Label_031A;
            }
            if (dc.o4Mdlx == null)
            {
                goto Label_032E;
            }
        Label_031A:
            if (dc.o7 == null)
            {
                goto Label_032E;
            }
            this.aldc.Add(dc);
        Label_032E:
            node.Expand();
            return;
        }

        private void lvMI_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item;
            IEnumerator enumerator;
            IDisposable disposable;
            enumerator = this.lvMI.SelectedItems.GetEnumerator();
        Label_0011:
            try
            {
                goto Label_0037;
            Label_0013:
                item = (ListViewItem) enumerator.Current;
                this.hv1.SetPos((int) item.Tag);
                goto Label_003F;
            Label_0037:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_0013;
                }
            Label_003F:
                goto Label_0052;
            }
            finally
            {
            Label_0041:
                disposable = enumerator as IDisposable;
                if (disposable == null)
                {
                    goto Label_0051;
                }
                disposable.Dispose();
            Label_0051:;
            }
        Label_0052:
            return;
        }

        private unsafe void Parse02(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            MemoryStream stream;
            BinaryReader reader;
            byte[] buffer;
            int num;
            StringWriter writer;
            int num2;
            StrDec dec;
            int num3;
            int num4;
            int num5;
            StrCode code;
            byte num6;
            TreeNode node;
            Exception exception;
            List<StrCode>.Enumerator enumerator;
            byte[] buffer2;
            int num7;
        Label_0000:
            try
            {
                stream = new MemoryStream(ent.bin, 0);
                reader = new BinaryReader(stream);
                buffer = ent.bin;
                if (reader.ReadInt32() == 1)
                {
                    goto Label_0031;
                }
                throw new InvalidDataException("w0 != 1");
            Label_0031:
                writer = new StringWriter();
                num2 = reader.ReadInt32();
                dec = new StrDec();
                num3 = 0;
                goto Label_011F;
            Label_004F:
                stream.Position = (long) (8 + (8 * num3));
                num4 = reader.ReadInt32();
                num5 = reader.ReadInt32();
                stream.Position = (long) num5;
                writer.WriteLine("{0:x8} {1} --", (int) num4, (int) num5);
                enumerator = dec.DecodeEvt(buffer, num5).GetEnumerator();
            Label_00A0:
                try
                {
                    goto Label_00E1;
                Label_00A2:
                    code = &enumerator.Current;
                    buffer2 = code.bin;
                    num7 = 0;
                    goto Label_00D9;
                Label_00B9:
                    num6 = buffer2[num7];
                    writer.Write("{0:x2} ", (byte) num6);
                    num7 += 1;
                Label_00D9:
                    if (num7 < ((int) buffer2.Length))
                    {
                        goto Label_00B9;
                    }
                Label_00E1:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_00A2;
                    }
                    goto Label_00FA;
                }
                finally
                {
                Label_00EC:
                    &enumerator.Dispose();
                }
            Label_00FA:
                writer.WriteLine();
                writer.WriteLine(dec.DecodeEvt(buffer, num5));
                writer.WriteLine();
                num3 += 1;
            Label_011F:
                if (num3 < num2)
                {
                    goto Label_004F;
                }
                tn.Nodes.Add("String table").Tag = new Strif(xoff, writer.ToString());
                goto Label_015F;
            }
            catch (Exception exception1)
            {
            Label_0150:
                exception = exception1;
                throw new NotSupportedException("Parser error.", exception);
            }
        Label_015F:
            return;
        }

        private void Parse03(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            StringWriter writer;
            TreeNode node;
            Exception exception;
        Label_0000:
            try
            {
                writer = new StringWriter();
                new ParseAI.Parse03(writer).Run(ent.bin);
                tn.Nodes.Add("A.I. code").Tag = new Strif(xoff, writer.ToString());
                goto Label_0049;
            }
            catch (Exception exception1)
            {
            Label_003C:
                exception = exception1;
                throw new NotSupportedException("Parser error.", exception);
            }
        Label_0049:
            return;
        }

        private void Parse0a(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            MemoryStream stream;
            ParseRADA erada;
            TreeNode node;
            IMGDi di;
            stream = new MemoryStream(ent.bin, 0);
            erada = new ParseRADA(stream);
            erada.Parse();
            node = tn.Nodes.Add("radar");
            di = new IMGDi(xoff, erada.pic);
            node.Tag = di;
            return;
        }

        private void Parse0c(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            MemoryStream stream;
            BinaryReader reader;
            int num;
            int num2;
            int num3;
            StringWriter writer;
            int num4;
            int num5;
            float num6;
            float num7;
            float num8;
            float num9;
            int num10;
            int num11;
            int num12;
            float num13;
            float num14;
            float num15;
            float num16;
            float num17;
            float num18;
            float num19;
            float num20;
            TreeNode node;
            EndOfStreamException exception;
            object[] objArray;
            object[] objArray2;
            stream = new MemoryStream(ent.bin, 0);
            reader = new BinaryReader(stream);
        Label_0014:
            try
            {
                stream.Position = 12L;
                num = reader.ReadUInt16();
                num2 = reader.ReadUInt16();
                num3 = 0x34;
                writer = new StringWriter();
                num4 = 0;
                goto Label_00E8;
            Label_003E:
                stream.Position = (long) (num3 + (0x40 * num4));
                num5 = reader.ReadInt32();
                num6 = reader.ReadSingle();
                num7 = reader.ReadSingle();
                num8 = reader.ReadSingle();
                num9 = reader.ReadSingle();
                objArray = new object[7];
                objArray[0] = (int) num4;
                objArray[1] = (int) num5;
                objArray[2] = (float) num6;
                objArray[3] = (float) num7;
                objArray[4] = (float) num8;
                objArray[5] = (float) num9;
            Label_00DA:
                objArray[6] = this.GetObjName(num5) ?? "?";
                writer.WriteLine("a.{0} {1:X4} ({2}, {3}, {4}, {5}) ; {6}", objArray);
                num4 += 1;
            Label_00E8:
                if (num4 < num)
                {
                    goto Label_003E;
                }
                num3 += 0x40 * num;
                writer.WriteLine();
                num10 = 0;
                goto Label_01FF;
            Label_0108:
                stream.Position = (long) (num3 + (0x40 * num10));
                num11 = reader.ReadUInt16();
                num12 = reader.ReadUInt16();
                num13 = reader.ReadSingle();
                num14 = reader.ReadSingle();
                num15 = reader.ReadSingle();
                num16 = reader.ReadSingle();
                num17 = reader.ReadSingle();
                num18 = reader.ReadSingle();
                num19 = reader.ReadSingle();
                num20 = reader.ReadSingle();
                writer.WriteLine("b.{0} {1:X4} {10:X} ({2}, {3}, {4}, {5}) ({6}, {7}, {8}, {9})", new object[] { (int) num10, (int) num11, (float) num13, (float) num14, (float) num15, (float) num16, (float) num17, (float) num18, (float) num19, (float) num20, (int) num12 });
                num10 += 1;
            Label_01FF:
                if (num10 < num2)
                {
                    goto Label_0108;
                }
                num3 += 0x40 * num2;
                tn.Nodes.Add("appear").Tag = new Strif(xoff, writer.ToString());
                goto Label_0247;
            }
            catch (EndOfStreamException exception1)
            {
            Label_0238:
                exception = exception1;
                throw new NotSupportedException("EOF", exception);
            }
        Label_0247:
            return;
        }

        private unsafe void Parse12(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            MemoryStream stream;
            PicPAX cpax;
            int num;
            R r;
            int num2;
            Bitmap bitmap;
            TreeNode node;
            List<R>.Enumerator enumerator;
            List<Bitmap>.Enumerator enumerator2;
            object[] objArray;
            stream = new MemoryStream(ent.bin, 0);
            new BinaryReader(stream);
            cpax = ParsePAX.ReadPAX(stream);
            num = 0;
            enumerator = cpax.alr.GetEnumerator();
        Label_002A:
            try
            {
                goto Label_00D7;
            Label_002F:
                r = &enumerator.Current;
                num += 1;
                num2 = 0;
                enumerator2 = r.pics.GetEnumerator();
            Label_004B:
                try
                {
                    goto Label_00BE;
                Label_004D:
                    bitmap = &enumerator2.Current;
                    num2 += 1;
                    tn.Nodes.Add(string.Concat(new object[] { (int) num, ".", (int) num2, ".p.", PalC2s.Guess(bitmap) })).Tag = new IMGDi(ent.off, bitmap);
                Label_00BE:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_004D;
                    }
                    goto Label_00D7;
                }
                finally
                {
                Label_00C9:
                    &enumerator2.Dispose();
                }
            Label_00D7:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_002F;
                }
                goto Label_00F3;
            }
            finally
            {
            Label_00E5:
                &enumerator.Dispose();
            }
        Label_00F3:
            return;
        }

        private void Parse18(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            MemoryStream stream;
            PicIMGD cimgd;
            TreeNode node;
            stream = new MemoryStream(ent.bin, 0);
            new BinaryReader(stream);
            cimgd = ParseIMGD.TakeIMGD(stream);
            tn.Nodes.Add("IMGD." + PalC2s.Guess(cimgd)).Tag = new IMGDi(ent.off, cimgd.pic);
            return;
        }

        private void Parse1D(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            MemoryStream stream;
            BinaryReader reader;
            int num;
            int num2;
            int num3;
            int num4;
            MemoryStream stream2;
            PicIMGD cimgd;
            TreeNode node;
            stream = new MemoryStream(ent.bin, 0);
            reader = new BinaryReader(stream);
            stream.Position = 12L;
            num = reader.ReadInt32();
            num2 = 0;
            goto Label_008E;
        Label_0028:
            num3 = reader.ReadInt32();
            num4 = reader.ReadInt32();
            stream2 = new MemoryStream(ent.bin, num3, num4, 0);
            cimgd = ParseIMGD.TakeIMGD(stream2);
            tn.Nodes.Add("IMGD." + PalC2s.Guess(cimgd)).Tag = new IMGDi(ent.off, cimgd.pic);
            num2 += 1;
        Label_008E:
            if (num2 < num)
            {
                goto Label_0028;
            }
            return;
        }

        private void Parse20(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            MemoryStream stream;
            Wavo[] wavoArray;
            Wavo wavo;
            TreeNode node;
            Wavo[] wavoArray2;
            int num;
            stream = new MemoryStream(ent.bin, 0);
            new BinaryReader(stream);
            wavoArray2 = ParseSD.ReadWD(stream);
            num = 0;
            goto Label_005A;
        Label_0023:
            wavo = wavoArray2[num];
            node = tn.Nodes.Add(wavo.fn);
            node.Tag = new WAVi(ent.off, wavo);
            this.setWAVi(node);
            num += 1;
        Label_005A:
            if (num < ((int) wavoArray2.Length))
            {
                goto Label_0023;
            }
            return;
        }

        private void Parse22(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            MemoryStream stream;
            Wavo[] wavoArray;
            Wavo wavo;
            TreeNode node;
            Wavo[] wavoArray2;
            int num;
            stream = new MemoryStream(ent.bin, 0);
            new BinaryReader(stream);
            wavoArray2 = ParseSD.ReadIV(stream);
            num = 0;
            goto Label_005A;
        Label_0023:
            wavo = wavoArray2[num];
            node = tn.Nodes.Add(wavo.fn);
            node.Tag = new WAVi(ent.off, wavo);
            this.setWAVi(node);
            num += 1;
        Label_005A:
            if (num < ((int) wavoArray2.Length))
            {
                goto Label_0023;
            }
            return;
        }

        private unsafe void Parse24(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            int num;
            Bitmap bitmap;
            byte[] buffer;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            TreeNode node;
            int num10;
            Bitmap bitmap2;
            byte[] buffer2;
            int num11;
            int num12;
            int num13;
            int num14;
            int num15;
            int num16;
            int num17;
            int num18;
            TreeNode node2;
            Bitmap bitmap3;
            byte[] buffer3;
            BitmapData data;
            ColorPalette palette;
            byte[] buffer4;
            Color[] colorArray;
            int num19;
            int num20;
            int num21;
            int num22;
            TreeNode node3;
            if ((ent.id == "evt") == null)
            {
                goto Label_0135;
            }
            num = ((int) ent.bin.Length) / 0x100;
            bitmap = new Bitmap(0x100, 2 * num);
            buffer = ent.bin;
            num2 = 0;
            goto Label_009B;
        Label_003D:
            num3 = (num2 & 0x1ff) + (2 * (num2 & -512));
            num4 = 0;
            goto Label_008E;
        Label_0055:
            num5 = buffer[num4 + (0x100 * num2)];
            num5 = ((num5 & 0x30) << 2) | ((num5 & 3) << 4);
            bitmap.SetPixel(num4, num3, Color.FromArgb(num5, num5, num5));
            num4 += 1;
        Label_008E:
            if (num4 < 0x100)
            {
                goto Label_0055;
            }
            num2 += 1;
        Label_009B:
            if (num2 < num)
            {
                goto Label_003D;
            }
            num6 = 0;
            goto Label_010F;
        Label_00A4:
            num7 = ((num6 & 0x1ff) + (2 * (num6 & -512))) + 0x200;
            num8 = 0;
            goto Label_0100;
        Label_00C4:
            num9 = buffer[num8 + (0x100 * num6)];
            num9 = (num9 & 0xc0) | ((num9 & 12) << 2);
            bitmap.SetPixel(num8, num7, Color.FromArgb(num9, num9, num9));
            num8 += 1;
        Label_0100:
            if (num8 < 0x100)
            {
                goto Label_00C4;
            }
            num6 += 1;
        Label_010F:
            if (num6 < num)
            {
                goto Label_00A4;
            }
            tn.Nodes.Add("font").Tag = new IMGDi(xoff, bitmap);
            return;
        Label_0135:
            if ((ent.id == "sys") == null)
            {
                goto Label_0257;
            }
            num10 = ((int) ent.bin.Length) / 0x100;
            bitmap2 = new Bitmap(0x100, 2 * num10);
            buffer2 = ent.bin;
            num11 = 0;
            goto Label_01CB;
        Label_0177:
            num12 = num11;
            num13 = 0;
            goto Label_01BC;
        Label_0180:
            num14 = buffer2[num13 + (0x100 * num11)];
            num14 = ((num14 & 0x30) << 2) | ((num14 & 3) << 4);
            bitmap2.SetPixel(num13, num12, Color.FromArgb(num14, num14, num14));
            num13 += 1;
        Label_01BC:
            if (num13 < 0x100)
            {
                goto Label_0180;
            }
            num11 += 1;
        Label_01CB:
            if (num11 < num10)
            {
                goto Label_0177;
            }
            num15 = 0;
            goto Label_022F;
        Label_01D6:
            num16 = num15 + num10;
            num17 = 0;
            goto Label_0220;
        Label_01E2:
            num18 = buffer2[num17 + (0x100 * num15)];
            num18 = (num18 & 0xc0) | ((num18 & 12) << 2);
            bitmap2.SetPixel(num17, num16, Color.FromArgb(num18, num18, num18));
            num17 += 1;
        Label_0220:
            if (num17 < 0x100)
            {
                goto Label_01E2;
            }
            num15 += 1;
        Label_022F:
            if (num15 < num10)
            {
                goto Label_01D6;
            }
            tn.Nodes.Add("font").Tag = new IMGDi(xoff, bitmap2);
            return;
        Label_0257:
            if ((ent.id == "icon") == null)
            {
                goto Label_0385;
            }
            bitmap3 = new Bitmap(0x100, 160, 0x30803);
            buffer3 = ent.bin;
            data = bitmap3.LockBits(new Rectangle(0, 0, bitmap3.Width, bitmap3.Height), 2, 0x30803);
        Label_02AE:
            try
            {
                Marshal.Copy(buffer3, 0, data.Scan0, 0xa000);
                goto Label_02CE;
            }
            finally
            {
            Label_02C4:
                bitmap3.UnlockBits(data);
            }
        Label_02CE:
            palette = bitmap3.Palette;
            buffer4 = new byte[0x2000];
            Buffer.BlockCopy(buffer3, 0xa000, buffer4, 0, 0x400);
            colorArray = palette.Entries;
            num19 = 0;
            goto Label_0352;
        Label_0305:
            num20 = num19;
            num21 = num19;
            num22 = 4 * num20;
            *(&(colorArray[num21])) = Color.FromArgb(Math.Min(0xff, buffer4[num22 + 3] * 2), buffer4[num22], buffer4[num22 + 1], buffer4[num22 + 2]);
            num19 += 1;
        Label_0352:
            if (num19 < 0x100)
            {
                goto Label_0305;
            }
            bitmap3.Palette = palette;
            tn.Nodes.Add("font").Tag = new IMGDi(xoff, bitmap3);
        Label_0385:
            return;
        }

        private void Parse4(TreeNode tn, int xoff, ReadBar.Barent ent, DC curdc)
        {
            MemoryStream stream;
            BinaryReader reader;
            int num;
            stream = new MemoryStream(ent.bin, 0);
            reader = new BinaryReader(stream);
            stream.Position = 0x90L;
            num = reader.ReadInt32();
            if (num != 2)
            {
                goto Label_003C;
            }
            curdc.o4Map = this.Parse4_02(tn, xoff, ent);
            return;
        Label_003C:
            if (num != 3)
            {
                goto Label_0051;
            }
            curdc.o4Mdlx = this.Parse4_03(tn, xoff, ent);
            return;
        Label_0051:
            throw new NotSupportedException("Unknown @90 .. " + ((int) num));
        }

        private M4 Parse4_02(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            MemoryStream stream;
            BinaryReader reader;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            List<int[]> list;
            int num6;
            int num7;
            List<int> list2;
            int num8;
            List<int> list3;
            int num9;
            int num10;
            int num11;
            List<Vifpli> list4;
            int num12;
            int num13;
            int num14;
            MemoryStream stream2;
            int num15;
            int num16;
            byte[] buffer;
            byte[] buffer2;
            TreeNode node;
            MI mi;
            M4 m;
            stream = new MemoryStream(ent.bin, 0);
            reader = new BinaryReader(stream);
            stream.Position = 0x90L;
            if (reader.ReadInt32() == 2)
            {
                goto Label_0036;
            }
            throw new NotSupportedException("@90 != 2");
        Label_0036:
            stream.Position = 160L;
            num2 = reader.ReadInt32();
            reader.ReadUInt16();
            num3 = reader.ReadUInt16();
            num4 = reader.ReadInt32();
            num5 = reader.ReadInt32();
            list = new List<int[]>();
            num6 = 0;
            goto Label_00D6;
        Label_0074:
            stream.Position = (long) ((0x90 + num4) + (4 * num6));
            num7 = reader.ReadInt32();
            stream.Position = (long) (0x90 + num7);
            list2 = new List<int>();
        Label_00A6:
            num8 = reader.ReadUInt16();
            if (num8 == 0xffff)
            {
                goto Label_00C2;
            }
            list2.Add(num8);
            goto Label_00A6;
        Label_00C2:
            list.Add(list2.ToArray());
            num6 += 1;
        Label_00D6:
            if (num6 < num3)
            {
                goto Label_0074;
            }
            list3 = new List<int>();
            stream.Position = (long) (0x90 + num5);
            num9 = 0;
            goto Label_0131;
        Label_00F7:
            num10 = reader.ReadInt32();
            stream.Position = (long) (0x90 + num10);
            num11 = 0;
            goto Label_0126;
        Label_0113:
            list3.Add(reader.ReadUInt16());
            num11 += 1;
        Label_0126:
            if (num11 < num2)
            {
                goto Label_0113;
            }
            num9 += 1;
        Label_0131:
            if (num9 < 1)
            {
                goto Label_00F7;
            }
            list4 = new List<Vifpli>();
            num12 = 0;
            goto Label_0245;
        Label_0145:
            stream.Position = (long) (0xb0 + (0x10 * num12));
            num13 = reader.ReadInt32();
            num14 = reader.ReadInt32();
            stream2 = new MemoryStream();
        Label_016E:
            stream.Position = (long) (0x90 + num13);
            num15 = reader.ReadInt32();
            num16 = num15 & 0xffff;
            reader.ReadInt32();
            buffer = reader.ReadBytes(8 + (0x10 * num16));
            stream2.Write(buffer, 0, (int) buffer.Length);
            if ((num15 >> 0x1c) == 6)
            {
                goto Label_01CA;
            }
            num13 += 0x10 + (0x10 * num16);
            goto Label_016E;
        Label_01CA:
            buffer2 = stream2.ToArray();
            list4.Add(new Vifpli(buffer2, num14));
            node = tn.Nodes.Add(string.Format("vifpkt{0} ({1})", (int) num12, (int) num14));
            mi = new MI();
            mi.Add("vifpkt", (xoff + 0x90) + num13);
            node.Tag = new Vifi((xoff + 0x90) + num13, mi, buffer2);
            num12 += 1;
        Label_0245:
            if (num12 < num2)
            {
                goto Label_0145;
            }
            m = new M4();
            m.alb1t2 = list3;
            m.alb2 = list;
            m.alvifpkt = list4;
            return m;
        }

        private unsafe Parse4Mdlx Parse4_03(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            Parse4Mdlx mdlx;
            T31 t;
            TreeNode node;
            TreeNode node2;
            StringWriter writer;
            AxBone bone;
            int num;
            T13vif tvif;
            TreeNode node3;
            List<T31>.Enumerator enumerator;
            List<AxBone>.Enumerator enumerator2;
            object[] objArray;
            object[] objArray2;
            object[] objArray3;
            List<T13vif>.Enumerator enumerator3;
            mdlx = new Parse4Mdlx(ent.bin);
            enumerator = mdlx.mdlx.alt31.GetEnumerator();
        Label_001E:
            try
            {
                goto Label_02C2;
            Label_0023:
                t = &enumerator.Current;
                node = tn.Nodes.Add("Mdlx.T31");
                node.Tag = new Hexi(xoff + t.off, t.len);
                if (t.t21 == null)
                {
                    goto Label_0243;
                }
                node2 = node.Nodes.Add("T21.alaxb");
                writer = new StringWriter();
                writer.WriteLine("alaxb = Array of joints");
                writer.WriteLine();
                writer.WriteLine("current-joint-index,parent-joint-index");
                writer.WriteLine(" scale x,y,z,w");
                writer.WriteLine(" rotate x,y,z,w");
                writer.WriteLine(" translate x,y,z,w");
                writer.WriteLine();
                enumerator2 = t.t21.alaxb.GetEnumerator();
            Label_00D4:
                try
                {
                    goto Label_0208;
                Label_00D9:
                    bone = &enumerator2.Current;
                    writer.WriteLine("{0},{1}", (int) bone.cur, (int) bone.parent);
                    writer.WriteLine(" {0},{1},{2},{3}", new object[] { (float) bone.x1, (float) bone.y1, (float) bone.z1, (float) bone.w1 });
                    writer.WriteLine(" {0},{1},{2},{3}", new object[] { (float) bone.x2, (float) bone.y2, (float) bone.z2, (float) bone.w2 });
                    writer.WriteLine(" {0},{1},{2},{3}", new object[] { (float) bone.x3, (float) bone.y3, (float) bone.z3, (float) bone.w3 });
                Label_0208:
                    if (&enumerator2.MoveNext() != null)
                    {
                        goto Label_00D9;
                    }
                    goto Label_0224;
                }
                finally
                {
                Label_0216:
                    &enumerator2.Dispose();
                }
            Label_0224:
                node2.Tag = new Strif(xoff + t.t21.off, writer.ToString());
            Label_0243:
                num = 0;
                enumerator3 = t.al13.GetEnumerator();
            Label_0253:
                try
                {
                    goto Label_02A9;
                Label_0255:
                    tvif = &enumerator3.Current;
                    node.Nodes.Add(string.Format("vifpkt{0} ({1})", (int) num++, (int) tvif.texi)).Tag = new Vifi(xoff + tvif.off, tvif.bin);
                Label_02A9:
                    if (&enumerator3.MoveNext() != null)
                    {
                        goto Label_0255;
                    }
                    goto Label_02C2;
                }
                finally
                {
                Label_02B4:
                    &enumerator3.Dispose();
                }
            Label_02C2:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0023;
                }
                goto Label_02DE;
            }
            finally
            {
            Label_02D0:
                &enumerator.Dispose();
            }
        Label_02DE:
            return mdlx;
        }

        private unsafe CollReader Parse6(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            MemoryStream stream;
            CollReader reader;
            StringWriter writer;
            int num;
            Co1 co;
            int num2;
            Co2 co2;
            int num3;
            Co3 co3;
            int num4;
            Vector4 vector;
            int num5;
            Plane plane;
            TreeNode node;
            List<Co1>.Enumerator enumerator;
            List<Co2>.Enumerator enumerator2;
            List<Co3>.Enumerator enumerator3;
            List<Vector4>.Enumerator enumerator4;
            List<Plane>.Enumerator enumerator5;
            stream = new MemoryStream(ent.bin, 0);
            new BinaryReader(stream);
            reader = new CollReader();
            reader.Read(stream);
            writer = new StringWriter();
            num = 0;
            enumerator = reader.alCo1.GetEnumerator();
        Label_0036:
            try
            {
                goto Label_0058;
            Label_0038:
                co = &enumerator.Current;
                writer.WriteLine("Co1[{0,2}] {1}", (int) num, co);
                num += 1;
            Label_0058:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0038;
                }
                goto Label_0071;
            }
            finally
            {
            Label_0063:
                &enumerator.Dispose();
            }
        Label_0071:
            writer.WriteLine("--");
            num2 = 0;
            enumerator2 = reader.alCo2.GetEnumerator();
        Label_008C:
            try
            {
                goto Label_00B1;
            Label_008E:
                co2 = &enumerator2.Current;
                writer.WriteLine("Co2[{0,3}] {1}", (int) num2, co2);
                num2 += 1;
            Label_00B1:
                if (&enumerator2.MoveNext() != null)
                {
                    goto Label_008E;
                }
                goto Label_00CA;
            }
            finally
            {
            Label_00BC:
                &enumerator2.Dispose();
            }
        Label_00CA:
            writer.WriteLine("--");
            num3 = 0;
            enumerator3 = reader.alCo3.GetEnumerator();
        Label_00E5:
            try
            {
                goto Label_010A;
            Label_00E7:
                co3 = &enumerator3.Current;
                writer.WriteLine("Co3[{0,3}] {1}", (int) num3, co3);
                num3 += 1;
            Label_010A:
                if (&enumerator3.MoveNext() != null)
                {
                    goto Label_00E7;
                }
                goto Label_0123;
            }
            finally
            {
            Label_0115:
                &enumerator3.Dispose();
            }
        Label_0123:
            writer.WriteLine("--");
            num4 = 0;
            enumerator4 = reader.alCo4.GetEnumerator();
        Label_013E:
            try
            {
                goto Label_0168;
            Label_0140:
                vector = &enumerator4.Current;
                writer.WriteLine("Co4[{0,3}] {1}", (int) num4, (Vector4) vector);
                num4 += 1;
            Label_0168:
                if (&enumerator4.MoveNext() != null)
                {
                    goto Label_0140;
                }
                goto Label_0181;
            }
            finally
            {
            Label_0173:
                &enumerator4.Dispose();
            }
        Label_0181:
            writer.WriteLine("--");
            num5 = 0;
            enumerator5 = reader.alCo5.GetEnumerator();
        Label_019C:
            try
            {
                goto Label_01C6;
            Label_019E:
                plane = &enumerator5.Current;
                writer.WriteLine("Co5[{0,3}] {1}", (int) num5, (Plane) plane);
                num5 += 1;
            Label_01C6:
                if (&enumerator5.MoveNext() != null)
                {
                    goto Label_019E;
                }
                goto Label_01DF;
            }
            finally
            {
            Label_01D1:
                &enumerator5.Dispose();
            }
        Label_01DF:
            writer.WriteLine("--");
            tn.Nodes.Add("collision").Tag = new Strif(xoff, writer.ToString());
            return reader;
        }

        private MTex ParseTex(TreeNode tn, int xoff, ReadBar.Barent ent)
        {
            MemoryStream stream;
            BinaryReader reader;
            int num;
            int num2;
            List<int> list;
            int num3;
            List<MTex> list2;
            int num4;
            byte[] buffer;
            TreeNode node;
            MemoryStream stream2;
            BinaryReader reader2;
            stream = new MemoryStream(ent.bin, 0);
            reader = new BinaryReader(stream);
            num = reader.ReadInt32();
            if (num != null)
            {
                goto Label_0031;
            }
            stream.Position = 0L;
            return this.ParseTex_TIMf(tn, xoff, stream, reader);
        Label_0031:
            if (num != -1)
            {
                goto Label_0124;
            }
            num2 = reader.ReadInt32();
            list = new List<int>();
            num3 = 0;
            goto Label_005E;
        Label_004B:
            list.Add(reader.ReadInt32());
            num3 += 1;
        Label_005E:
            if (num3 < num2)
            {
                goto Label_004B;
            }
            list.Add((int) ent.bin.Length);
            list2 = new List<MTex>();
            num4 = 0;
            goto Label_0108;
        Label_0081:
            stream.Position = (long) list[num4];
            buffer = reader.ReadBytes(list[num4 + 1] - list[num4]);
            node = tn.Nodes.Add("TIMc" + ((int) num4));
            stream2 = new MemoryStream(buffer, 0);
            reader2 = new BinaryReader(stream2);
            list2.Add(this.ParseTex_TIMf(node, ent.off + list[num4], stream2, reader2));
            num4 += 1;
        Label_0108:
            if (num4 < num2)
            {
                goto Label_0081;
            }
            if (list2.Count != null)
            {
                goto Label_011B;
            }
            return null;
        Label_011B:
            return list2[0];
        Label_0124:
            throw new NotSupportedException("Unknown v00 .. " + ((int) num));
        }

        private MTex ParseTex_TIMf(TreeNode tn, int xoff, MemoryStream si, BinaryReader br)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            SortedDictionary<int, int> dictionary;
            int num6;
            Texctx texctx;
            int num7;
            List<Bitmap> list;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            STim tim;
            MI mi;
            TreeNode node;
            br.ReadInt32();
            br.ReadInt32();
            num = br.ReadInt32();
            num2 = br.ReadInt32();
            num3 = br.ReadInt32();
            num4 = br.ReadInt32();
            num5 = br.ReadInt32();
            br.ReadInt32();
            br.ReadInt32();
            dictionary = new SortedDictionary<int, int>();
            si.Position = (long) num3;
            num6 = 0;
            goto Label_0073;
        Label_005D:
            dictionary[num6] = br.ReadByte();
            num6 += 1;
        Label_0073:
            if (num6 < num2)
            {
                goto Label_005D;
            }
            texctx = new Texctx();
            new List<int>();
            num7 = 0;
            goto Label_0090;
        Label_008A:
            num7 += 1;
        Label_0090:
            if (num7 < (num + 1))
            {
                goto Label_008A;
            }
            list = new List<Bitmap>();
            num8 = 0;
            goto Label_01C4;
        Label_00A6:
            num9 = num4;
            si.Position = (long) (num9 + 0x20);
            texctx.Do1(si);
            num10 = texctx.offBin;
            num11 = num4 + (0x90 * (1 + dictionary[num8]));
            si.Position = (long) (num11 + 0x20);
            texctx.Do1(si);
            num12 = texctx.offBin;
            num13 = (num5 + (160 * num8)) + 0x20;
            si.Position = (long) num13;
            tim = texctx.Do2(si);
            mi = new MI();
            mi.Add("offp1pal", xoff + num9);
            mi.Add("offp1tex", xoff + num11);
            mi.Add("offp2", xoff + num13);
            mi.Add("offTex", xoff + num12);
            mi.Add("offPal", xoff + num10);
            tn.Nodes.Add(string.Format("tex{0} ({1})", (int) num8, (int) texctx.t0PSM)).Tag = new Texi(xoff + num12, mi, tim);
            list.Add(tim.Generate());
            num8 += 1;
        Label_01C4:
            if (num8 < num2)
            {
                goto Label_00A6;
            }
            return new MTex(list);
        }

        private unsafe void pb_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox box;
            Bitmap bitmap;
            Color color;
            object[] objArray;
            box = (PictureBox) sender;
            bitmap = (Bitmap) box.Image;
            if (e.X >= bitmap.Width)
            {
                goto Label_00F5;
            }
            if (e.Y >= bitmap.Height)
            {
                goto Label_00F5;
            }
            color = bitmap.GetPixel(e.X, e.Y);
            this.labelPixi.Text = string.Concat(new object[] { "x:", (int) e.X, "\ny:", (int) e.Y, "\n\nR:", (byte) &color.R, "\nG:", (byte) &color.G, "\nB:", (byte) &color.B, "\nA:", (byte) &color.A, "\n" });
        Label_00F5:
            return;
        }

        private unsafe void RDForm_DragDrop(object sender, DragEventArgs e)
        {
            Dictionary<string, int> dictionary1;
            string[] strArray;
            BEXForm form;
            string str;
            string str2;
            DialogResult result;
            string[] strArray2;
            int num;
            string[] strArray3;
            int num2;
            string str3;
            int num3;
            strArray = (string[]) e.Data.GetData(DataFormats.FileDrop);
            if (strArray == null)
            {
                goto Label_01E1;
            }
            if (((int) strArray.Length) < 2)
            {
                goto Label_007A;
            }
            switch ((MessageBox.Show(this, "Would you try batch export instead of viewer?", Application.ProductName, 3, 0x30) - 6))
            {
                case 0:
                    goto Label_0049;

                case 1:
                    goto Label_007A;
            }
            return;
        Label_0049:
            form = new BEXForm(this);
            strArray2 = strArray;
            num = 0;
            goto Label_006B;
        Label_0058:
            str = strArray2[num];
            form.Addfp(str);
            num += 1;
        Label_006B:
            if (num < ((int) strArray2.Length))
            {
                goto Label_0058;
            }
            form.Show();
            return;
        Label_007A:
            strArray3 = strArray;
            num2 = 0;
            goto Label_01D6;
        Label_0085:
            str2 = strArray3[num2];
            if ((str3 = Path.GetExtension(str2).ToLowerInvariant()) == null)
            {
                goto Label_01D0;
            }
            if (<PrivateImplementationDetails>{D7D9E262-EFBD-4B49-9758-30F2559CA52B}.$$method0x600010a-1 != null)
            {
                goto Label_0165;
            }
            dictionary1 = new Dictionary<string, int>(14);
            dictionary1.Add(".map", 0);
            dictionary1.Add(".mdlx", 1);
            dictionary1.Add(".apdx", 2);
            dictionary1.Add(".fm", 3);
            dictionary1.Add(".2dd", 4);
            dictionary1.Add(".bar", 5);
            dictionary1.Add(".2ld", 6);
            dictionary1.Add(".mset", 7);
            dictionary1.Add(".pax", 8);
            dictionary1.Add(".wd", 9);
            dictionary1.Add(".vsb", 10);
            dictionary1.Add(".ard", 11);
            dictionary1.Add(".imd", 12);
            dictionary1.Add(".mag", 13);
            <PrivateImplementationDetails>{D7D9E262-EFBD-4B49-9758-30F2559CA52B}.$$method0x600010a-1 = dictionary1;
        Label_0165:
            if (<PrivateImplementationDetails>{D7D9E262-EFBD-4B49-9758-30F2559CA52B}.$$method0x600010a-1.TryGetValue(str3, &num3) == null)
            {
                goto Label_01D0;
            }
            switch (num3)
            {
                case 0:
                    goto Label_01B8;

                case 1:
                    goto Label_01B8;

                case 2:
                    goto Label_01B8;

                case 3:
                    goto Label_01B8;

                case 4:
                    goto Label_01B8;

                case 5:
                    goto Label_01B8;

                case 6:
                    goto Label_01B8;

                case 7:
                    goto Label_01B8;

                case 8:
                    goto Label_01B8;

                case 9:
                    goto Label_01B8;

                case 10:
                    goto Label_01B8;

                case 11:
                    goto Label_01B8;

                case 12:
                    goto Label_01B8;

                case 13:
                    goto Label_01B8;
            }
            goto Label_01D0;
        Label_01B8:
            this.fpld = str2;
            Application.Idle += new EventHandler(this.Application_Idle);
        Label_01D0:
            num2 += 1;
        Label_01D6:
            if (num2 < ((int) strArray3.Length))
            {
                goto Label_0085;
            }
        Label_01E1:
            return;
        }

        private void RDForm_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) != null) ? 1 : 0;
            return;
        }

        private void RDForm_Load(object sender, EventArgs e)
        {
            Match match;
            IEnumerator enumerator;
            IDisposable disposable;
            enumerator = Regex.Matches(File.ReadAllText(Path.Combine(Application.StartupPath, "objname.txt"), Encoding.UTF8).Replace("\r", "\n"), "^(?<d>[0-9A-F]{4})=(?<n>.+)$", 3).GetEnumerator();
        Label_0039:
            try
            {
                goto Label_0083;
            Label_003B:
                match = (Match) enumerator.Current;
                this.dictObjName[Convert.ToInt32(match.Groups["d"].Value, 0x10)] = match.Groups["n"].Value;
            Label_0083:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_003B;
                }
                goto Label_009E;
            }
            finally
            {
            Label_008D:
                disposable = enumerator as IDisposable;
                if (disposable == null)
                {
                    goto Label_009D;
                }
                disposable.Dispose();
            Label_009D:;
            }
        Label_009E:
            if (this.fpread == null)
            {
                goto Label_00B8;
            }
            this.LoadMap(this.WraponDemand(this.fpread));
        Label_00B8:
            return;
        }

        private void setPicPane(Image pic)
        {
            PictureBox box;
            goto Label_0018;
        Label_0002:
            this.p1.Controls[0].Dispose();
        Label_0018:
            if (this.p1.Controls.Count != null)
            {
                goto Label_0002;
            }
            box = new PictureBox();
            box.Parent = this.p1;
            box.Image = pic;
            box.SizeMode = 2;
            box.Visible = 1;
            box.MouseMove += new MouseEventHandler(this.pb_MouseMove);
            this.labelHelpMore.Text = "";
            return;
        }

        private void setPicPane(Image[] pics)
        {
            FlowLayoutPanel panel;
            Image image;
            PictureBox box;
            Image[] imageArray;
            int num;
            goto Label_0018;
        Label_0002:
            this.p1.Controls[0].Dispose();
        Label_0018:
            if (this.p1.Controls.Count != null)
            {
                goto Label_0002;
            }
            panel = new FlowLayoutPanel();
            panel.Parent = this.p1;
            panel.Dock = 5;
            imageArray = pics;
            num = 0;
            goto Label_008E;
        Label_004A:
            image = imageArray[num];
            box = new PictureBox();
            box.Image = image;
            box.SizeMode = 2;
            box.Visible = 1;
            box.MouseMove += new MouseEventHandler(this.pb_MouseMove);
            panel.Controls.Add(box);
            num += 1;
        Label_008E:
            if (num < ((int) imageArray.Length))
            {
                goto Label_004A;
            }
            this.labelHelpMore.Text = "";
            return;
        }

        private void setTextPane(string text, bool fixedfont)
        {
            TextBox box;
            goto Label_0018;
        Label_0002:
            this.p1.Controls[0].Dispose();
        Label_0018:
            if (this.p1.Controls.Count != null)
            {
                goto Label_0002;
            }
            box = new TextBox();
            box.Multiline = 1;
            box.ScrollBars = 2;
            box.BorderStyle = 0;
            box.Dock = 5;
            box.ReadOnly = 1;
            box.Visible = 1;
            box.Parent = this.p1;
            if (fixedfont == null)
            {
                goto Label_0092;
            }
            box.Font = new Font(FontFamily.GenericMonospace, box.Font.SizeInPoints);
            box.WordWrap = 0;
            box.ScrollBars = 3;
        Label_0092:
            box.Text = text;
            this.labelHelpMore.Text = "";
            return;
        }

        private void setWAVi(TreeNode tnt)
        {
            string str;
            tnt.ImageKey = tnt.SelectedImageKey = "SpeechMicHS.png";
            return;
        }

        private unsafe void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node;
            Exception exception;
            Hexi hexi;
            KeyValuePair<string, int> pair;
            ListViewItem item;
            Texi texi;
            Vifi vifi;
            string str;
            IMGDi di;
            WAVi vi;
            string str2;
            Stri stri;
            Strif strif;
            Btni btni;
            Button button;
            TreeViewAction action;
            SortedDictionary<string, int>.Enumerator enumerator;
            Image[] imageArray;
            string[] strArray;
            object[] objArray;
            switch ((e.Action - 1))
            {
                case 0:
                    goto Label_001A;

                case 1:
                    goto Label_001A;
            }
            return;
        Label_001A:
            node = e.Node;
            this.bserr.Hide();
            exception = (Exception) this.errcol[node];
            if (exception == null)
            {
                goto Label_0058;
            }
            this.bserr.Show();
            this.bserr.Tag = exception;
        Label_0058:
            if (node == null)
            {
                goto Label_0475;
            }
            hexi = node.Tag as Hexi;
            if (hexi == null)
            {
                goto Label_014E;
            }
            this.hv1.RangeMarkedList.Clear();
            if (hexi.off < 0)
            {
                goto Label_00D5;
            }
            this.hv1.SetPos(hexi.off);
            if (hexi.len <= 0)
            {
                goto Label_00D5;
            }
            this.hv1.AddRangeMarked(hexi.off, hexi.len, Color.FromArgb(50, Color.LimeGreen), Color.FromArgb(200, Color.LimeGreen));
        Label_00D5:
            this.lvMI.Items.Clear();
            if (hexi.mi == null)
            {
                goto Label_014E;
            }
            enumerator = hexi.mi.col2off.GetEnumerator();
        Label_00FF:
            try
            {
                goto Label_0135;
            Label_0101:
                pair = &enumerator.Current;
                ListViewItem introduced20 = this.lvMI.Items.Add(&pair.Key);
                introduced20.Tag = (int) &pair.Value;
            Label_0135:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0101;
                }
                goto Label_014E;
            }
            finally
            {
            Label_0140:
                &enumerator.Dispose();
            }
        Label_014E:
            texi = hexi as Texi;
            if (texi == null)
            {
                goto Label_032B;
            }
            this.setPicPane(new Image[] { texi.st.pic, texi.st.Generate() });
            this.labelHelpMore.Text = string.Format("tfx({0,-10}) tcc({1,-4}) wms({2,-7}) wmt({3,-7})\n", new object[] { (TFX) texi.st.tfx, (TCC) texi.st.tcc, (WM) texi.st.wms, (WM) texi.st.wmt }) + ((texi.st.wms == 2) ? string.Format("Horz-clamp({0,4},{1,4}) ", (int) texi.st.minu, (int) texi.st.maxu) : "") + ((texi.st.wms == 3) ? string.Format("UMSK({0,4}) UFIX({1,4}) ", (int) texi.st.minu, (int) texi.st.maxu) : "") + ((texi.st.wmt == 2) ? string.Format("Vert-clamp({0,4},{1,4}) ", (int) texi.st.minv, (int) texi.st.maxv) : "") + ((texi.st.wmt == 3) ? string.Format("VMSK({0,4}) VFIX({1,4}) ", (int) texi.st.minv, (int) texi.st.maxv) : "");
        Label_032B:
            vifi = hexi as Vifi;
            if (vifi == null)
            {
                goto Label_034E;
            }
            str = Parseivif.Parse(vifi.vifpkt);
            this.setTextPane(str, 0);
        Label_034E:
            di = hexi as IMGDi;
            if (di == null)
            {
                goto Label_0367;
            }
            this.setPicPane(di.p);
        Label_0367:
            vi = hexi as WAVi;
            if (vi == null)
            {
                goto Label_03C8;
            }
            if (this.WavePlayer == null)
            {
                goto Label_038A;
            }
            this.WavePlayer(vi);
            goto Label_03C8;
        Label_038A:
            Directory.CreateDirectory("tmp");
            str2 = @"tmp\_" + vi.w.fn;
            File.WriteAllBytes(str2, vi.w.bin);
            Process.Start(str2);
        Label_03C8:
            stri = hexi as Stri;
            if (stri == null)
            {
                goto Label_03E2;
            }
            this.setTextPane(stri.s, 0);
        Label_03E2:
            strif = hexi as Strif;
            if (strif == null)
            {
                goto Label_03FC;
            }
            this.setTextPane(strif.s, 1);
        Label_03FC:
            btni = hexi as Btni;
            if (btni == null)
            {
                goto Label_0475;
            }
            goto Label_0420;
        Label_040A:
            this.p1.Controls[0].Dispose();
        Label_0420:
            if (this.p1.Controls.Count != null)
            {
                goto Label_040A;
            }
            button = new Button();
            button.AutoSize = 1;
            button.AutoSizeMode = 0;
            button.Text = "Click";
            button.Click += btni.onClicked;
            this.p1.Controls.Add(button);
        Label_0475:
            return;
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
        }

        private string WraponDemand(string fpld)
        {
            string str;
            if ((str = Path.GetExtension(fpld).ToLowerInvariant()) == null)
            {
                goto Label_007D;
            }
            if ((str == ".wd") != null)
            {
                goto Label_0045;
            }
            if ((str == ".vsb") != null)
            {
                goto Label_0053;
            }
            if ((str == ".pax") != null)
            {
                goto Label_0061;
            }
            if ((str == ".imd") != null)
            {
                goto Label_006F;
            }
            goto Label_007D;
        Label_0045:
            return WUt.Usebar(fpld, 0x20, "wd");
        Label_0053:
            return WUt.Usebar(fpld, 0x22, "vsb");
        Label_0061:
            return WUt.Usebar(fpld, 0x12, "pax");
        Label_006F:
            return WUt.Usebar(fpld, 0x18, "imd");
        Label_007D:
            return fpld;
        }

        private string OpenedPath
        {
            get
            {
                return this.linkLabel1.Text;
            }
        }

        [CompilerGenerated]
        private sealed class <>c__DisplayClass1
        {
            public string dirn;
            public string dirTo;
            public RDForm.UniName un;

            public <>c__DisplayClass1()
            {
                base..ctor();
                return;
            }

            public void <Expthem>b__0(RDForm.WAVi wi)
            {
                Directory.CreateDirectory(this.dirTo);
                File.WriteAllBytes(Path.Combine(this.dirTo, this.un.Get(this.dirn)), wi.w.bin);
                return;
            }
        }

        private class Btni : RDForm.Hexi
        {
            public EventHandler onClicked;

            public Btni(int off, EventHandler onClicked)
            {
                base..ctor(off);
                this.onClicked = onClicked;
                return;
            }
        }

        private class Hexi
        {
            public int len;
            public RDForm.MI mi;
            public int off;

            public Hexi(int off)
            {
                base..ctor();
                this.off = off;
                this.len = 0;
                return;
            }

            public Hexi(int off, RDForm.MI mi)
            {
                base..ctor();
                this.off = off;
                this.mi = mi;
                return;
            }

            public Hexi(int off, int len)
            {
                base..ctor();
                this.off = off;
                this.len = len;
                return;
            }
        }

        private class IMGDi : RDForm.Hexi
        {
            public Bitmap p;

            public IMGDi(int off, Bitmap p)
            {
                base..ctor(off);
                this.p = p;
                return;
            }
        }

        private class MI
        {
            public SortedDictionary<string, int> col2off;

            public MI()
            {
                this.col2off = new SortedDictionary<string, int>();
                base..ctor();
                return;
            }

            public void Add(string col, int off)
            {
                this.col2off[col] = off;
                return;
            }
        }

        private class PalC2s
        {
            public PalC2s()
            {
                base..ctor();
                return;
            }

            public static string Guess(PicIMGD p)
            {
                return Guess(p.pic);
            }

            public static string Guess(Bitmap p)
            {
                PixelFormat format;
                format = p.PixelFormat;
                if (format == 0x30402)
                {
                    goto Label_001D;
                }
                if (format != 0x30803)
                {
                    goto Label_0023;
                }
                return "8";
            Label_001D:
                return "4";
            Label_0023:
                return "?";
            }
        }

        private class Parseivif
        {
            public Parseivif()
            {
                base..ctor();
                return;
            }

            public static string Parse(byte[] bin04)
            {
                MemoryStream stream;
                BinaryReader reader;
                StringBuilder builder;
                long num;
                uint num2;
                int num3;
                int num4;
                int num5;
                uint num6;
                string str;
                int num7;
                uint num8;
                uint num9;
                uint num10;
                uint num11;
                uint num12;
                uint num13;
                uint num14;
                uint num15;
                int num16;
                int num17;
                RangeUtil util;
                int num18;
                int num19;
                int num20;
                int num21;
                int num22;
                int num23;
                int num24;
                int num25;
                int num26;
                string str2;
                string str3;
                int num27;
                long num28;
                int num29;
                int num30;
                int num31;
                int num32;
                int num33;
                int num34;
                int num35;
                object[] objArray;
                object[] objArray2;
                int num36;
                int num37;
                object[] objArray3;
                stream = new MemoryStream(bin04, 0);
                reader = new BinaryReader(stream);
                builder = new StringBuilder();
                goto Label_072C;
            Label_001A:
                num = stream.Position;
                num2 = reader.ReadUInt32();
                num3 = (num2 >> 0x18) & 0x7f;
                builder.AppendFormat("{0:X4} {1} ", (long) num, (char) (((num2 & -2147483648) != null) ? 0x49 : 0x20));
                num35 = num3;
                if (num35 > 0x20)
                {
                    goto Label_00D9;
                }
                switch (num35)
                {
                    case 0:
                        goto Label_010B;

                    case 1:
                        goto Label_0122;

                    case 2:
                        goto Label_0157;

                    case 3:
                        goto Label_016E;

                    case 4:
                        goto Label_0185;

                    case 5:
                        goto Label_019C;

                    case 6:
                        goto Label_01B3;

                    case 7:
                        goto Label_01CA;

                    case 8:
                        goto Label_0448;

                    case 9:
                        goto Label_0448;

                    case 10:
                        goto Label_0448;

                    case 11:
                        goto Label_0448;

                    case 12:
                        goto Label_0448;

                    case 13:
                        goto Label_0448;

                    case 14:
                        goto Label_0448;

                    case 15:
                        goto Label_0448;

                    case 0x10:
                        goto Label_01E1;

                    case 0x11:
                        goto Label_01F8;

                    case 0x12:
                        goto Label_0448;

                    case 0x13:
                        goto Label_020F;

                    case 20:
                        goto Label_0226;

                    case 0x15:
                        goto Label_0254;

                    case 0x16:
                        goto Label_0448;

                    case 0x17:
                        goto Label_023D;
                }
                if (num35 == 0x20)
                {
                    goto Label_026B;
                }
                goto Label_0448;
            Label_00D9:
                switch ((num35 - 0x30))
                {
                    case 0:
                        goto Label_02D3;

                    case 1:
                        goto Label_033A;
                }
                if (num35 == 0x4a)
                {
                    goto Label_03A1;
                }
                switch ((num35 - 80))
                {
                    case 0:
                        goto Label_03B8;

                    case 1:
                        goto Label_0400;
                }
                goto Label_0448;
            Label_010B:
                builder.AppendFormat("nop\n", new object[0]);
                goto Label_072C;
            Label_0122:
                num4 = num2 & 0xff;
                num5 = (num2 >> 8) & 0xff;
                builder.AppendFormat("stcycl cl {0:x2} wl {1:x2}\n", (int) num4, (int) num5);
                goto Label_072C;
            Label_0157:
                builder.AppendFormat("offset\n", new object[0]);
                goto Label_072C;
            Label_016E:
                builder.AppendFormat("base\n", new object[0]);
                goto Label_072C;
            Label_0185:
                builder.AppendFormat("itop\n", new object[0]);
                goto Label_072C;
            Label_019C:
                builder.AppendFormat("stmod\n", new object[0]);
                goto Label_072C;
            Label_01B3:
                builder.AppendFormat("mskpath3\n", new object[0]);
                goto Label_072C;
            Label_01CA:
                builder.AppendFormat("mark\n", new object[0]);
                goto Label_072C;
            Label_01E1:
                builder.AppendFormat("flushe\n", new object[0]);
                goto Label_072C;
            Label_01F8:
                builder.AppendFormat("flush\n", new object[0]);
                goto Label_072C;
            Label_020F:
                builder.AppendFormat("flusha\n", new object[0]);
                goto Label_072C;
            Label_0226:
                builder.AppendFormat("mscal\n", new object[0]);
                goto Label_072C;
            Label_023D:
                builder.AppendFormat("mscnt\n", new object[0]);
                goto Label_072C;
            Label_0254:
                builder.AppendFormat("mscalf\n", new object[0]);
                goto Label_072C;
            Label_026B:
                num6 = reader.ReadUInt32();
                str = "";
                num7 = 0;
                goto Label_02BA;
            Label_027F:
                if ((num7 & 3) != null)
                {
                    goto Label_0295;
                }
                str = str + ((char) 0x20);
            Label_0295:
                str = str + ((int) ((num6 >> ((2 * num7) & 0x1f)) & 3)) + " ";
                num7 += 1;
            Label_02BA:
                if (num7 < 0x10)
                {
                    goto Label_027F;
                }
                builder.AppendFormat("stmask {0}\n", str);
                goto Label_072C;
            Label_02D3:
                num8 = reader.ReadUInt32();
                num9 = reader.ReadUInt32();
                num10 = reader.ReadUInt32();
                num11 = reader.ReadUInt32();
                builder.AppendFormat("strow {0:x8} {1:x8} {2:x8} {3:x8}\n", new object[] { (uint) num8, (uint) num9, (uint) num10, (uint) num11 });
                goto Label_072C;
            Label_033A:
                num12 = reader.ReadUInt32();
                num13 = reader.ReadUInt32();
                num14 = reader.ReadUInt32();
                num15 = reader.ReadUInt32();
                builder.AppendFormat("stcol {0:x8} {1:x8} {2:x8} {3:x8}\n", new object[] { (uint) num12, (uint) num13, (uint) num14, (uint) num15 });
                goto Label_072C;
            Label_03A1:
                builder.AppendFormat("mpg\n", new object[0]);
                goto Label_072C;
            Label_03B8:
                builder.AppendFormat("direct\n", new object[0]);
                num16 = num2 & 0xffff;
                stream.Position = (stream.Position + 15L) & -16L;
                stream.Position += (long) (0x10 * num16);
                goto Label_072C;
            Label_0400:
                builder.AppendFormat("directhl\n", new object[0]);
                num17 = num2 & 0xffff;
                stream.Position = (stream.Position + 15L) & -16L;
                stream.Position += (long) (0x10 * num17);
                goto Label_072C;
            Label_0448:
                if (0x60 != (num3 & 0x60))
                {
                    goto Label_0719;
                }
                util = new RangeUtil();
                num18 = (num3 >> 4) & 1;
                num19 = (num3 >> 2) & 3;
                num20 = num3 & 3;
                num21 = (num2 >> 0x10) & 0xff;
                num22 = (num2 >> 15) & 1;
                num23 = (num2 >> 14) & 1;
                num24 = num2 & 0x3ff;
                num25 = 0;
                num26 = 1;
                str2 = "";
                num36 = num20;
                switch (num36)
                {
                    case 0:
                        goto Label_04C4;

                    case 1:
                        goto Label_04D0;

                    case 2:
                        goto Label_04DC;

                    case 3:
                        goto Label_04E8;
                }
                goto Label_04F2;
            Label_04C4:
                num25 = 4;
                str2 = "32";
                goto Label_04F2;
            Label_04D0:
                num25 = 2;
                str2 = "16";
                goto Label_04F2;
            Label_04DC:
                num25 = 1;
                str2 = "8";
                goto Label_04F2;
            Label_04E8:
                num25 = 2;
                str2 = "5+5+5+1";
            Label_04F2:
                str3 = "";
                num37 = num19;
                switch (num37)
                {
                    case 0:
                        goto Label_0516;

                    case 1:
                        goto Label_0522;

                    case 2:
                        goto Label_052E;

                    case 3:
                        goto Label_053A;
                }
                goto Label_0544;
            Label_0516:
                num26 = 1;
                str3 = "S";
                goto Label_0544;
            Label_0522:
                num26 = 2;
                str3 = "V2";
                goto Label_0544;
            Label_052E:
                num26 = 3;
                str3 = "V3";
                goto Label_0544;
            Label_053A:
                num26 = 4;
                str3 = "V4";
            Label_0544:
                num27 = (((num25 * num26) * num21) + 3) & -4;
                num28 = stream.Position + ((long) num27);
                builder.AppendFormat("unpack {0}-{1} c {2} a {3:X3} usn {4} flg {5} m {6}\n", new object[] { str3, str2, (int) num21, (int) num24, (int) num23, (int) num22, (int) num18 });
                if (num20 != null)
                {
                    goto Label_0623;
                }
                if (num19 == 2)
                {
                    goto Label_05C6;
                }
                if (num19 != 3)
                {
                    goto Label_0623;
                }
            Label_05C6:
                num29 = 0;
                goto Label_0618;
            Label_05CB:
                builder.Append("    ");
                num30 = 0;
                goto Label_0600;
            Label_05DC:
                builder.AppendFormat("{0:x8} ", (uint) util.pass(reader.ReadUInt32()));
                num30 += 1;
            Label_0600:
                if (num30 < num26)
                {
                    goto Label_05DC;
                }
                builder.Append("\n");
                num29 += 1;
            Label_0618:
                if (num29 < num21)
                {
                    goto Label_05CB;
                }
                goto Label_06DF;
            Label_0623:
                if (num20 != 1)
                {
                    goto Label_0682;
                }
                num31 = 0;
                goto Label_067A;
            Label_062D:
                builder.Append("    ");
                num32 = 0;
                goto Label_0662;
            Label_063E:
                builder.AppendFormat("{0:x4} ", (ushort) util.pass(reader.ReadUInt16()));
                num32 += 1;
            Label_0662:
                if (num32 < num26)
                {
                    goto Label_063E;
                }
                builder.Append("\n");
                num31 += 1;
            Label_067A:
                if (num31 < num21)
                {
                    goto Label_062D;
                }
                goto Label_06DF;
            Label_0682:
                if (num20 != 2)
                {
                    goto Label_06DF;
                }
                num33 = 0;
                goto Label_06D9;
            Label_068C:
                builder.Append("    ");
                num34 = 0;
                goto Label_06C1;
            Label_069D:
                builder.AppendFormat("{0:x2} ", (byte) util.pass(reader.ReadByte()));
                num34 += 1;
            Label_06C1:
                if (num34 < num26)
                {
                    goto Label_069D;
                }
                builder.Append("\n");
                num33 += 1;
            Label_06D9:
                if (num33 < num21)
                {
                    goto Label_068C;
                }
            Label_06DF:
                stream.Position = num28;
                builder.Append("    ");
                builder.AppendFormat("min({0}), max({1})\n", (uint) util.min, (uint) util.max);
                goto Label_072C;
            Label_0719:
                builder.AppendFormat("{0:X2}\n", (int) num3);
            Label_072C:
                if (stream.Position < stream.Length)
                {
                    goto Label_001A;
                }
                builder.Replace("\n", "\r\n");
                return builder.ToString();
            }

            private class RangeUtil
            {
                public uint max;
                public uint min;

                public RangeUtil()
                {
                    this.min = -1;
                    base..ctor();
                    return;
                }

                public byte pass(byte val)
                {
                    this.min = Math.Min(val, this.min);
                    this.max = Math.Max(val, this.max);
                    return val;
                }

                public ushort pass(ushort val)
                {
                    this.min = Math.Min(val, this.min);
                    this.max = Math.Max(val, this.max);
                    return val;
                }

                public uint pass(uint val)
                {
                    this.min = Math.Min(val, this.min);
                    this.max = Math.Max(val, this.max);
                    return val;
                }
            }
        }

        private class Stri : RDForm.Hexi
        {
            public string s;

            public Stri(int off, string s)
            {
                base..ctor(off);
                this.s = s;
                return;
            }
        }

        private class Strif : RDForm.Hexi
        {
            public string s;

            public Strif(int off, string s)
            {
                base..ctor(off);
                this.s = s;
                return;
            }
        }

        private class Texctx
        {
            public byte[] gs;
            public int offBin;
            public int t0PSM;

            public Texctx()
            {
                this.gs = new byte[0x400000];
                base..ctor();
                return;
            }

            public void Do1(Stream si)
            {
                BinaryReader reader;
                ulong num;
                int num2;
                int num3;
                int num4;
                int num5;
                int num6;
                int num7;
                ulong num8;
                int num9;
                int num10;
                int num11;
                int num12;
                int num13;
                ulong num14;
                ulong num15;
                int num16;
                int num17;
                int num18;
                int num19;
                byte[] buffer;
                byte[] buffer2;
                reader = new BinaryReader(si);
                num = reader.ReadUInt64();
                reader.ReadUInt64();
                num2 = ((int) num) & 0x3fff;
                num3 = ((int) (num >> 0x10)) & 0x3f;
                num4 = ((int) (num >> 0x18)) & 0x3f;
                num5 = ((int) (num >> 0x20)) & 0x3fff;
                num6 = ((int) (num >> 0x30)) & 0x3f;
                num7 = ((int) (num >> 0x38)) & 0x3f;
                Trace.Assert(num2 == 0);
                Trace.Assert(num3 == 0);
                Trace.Assert(num4 == 0);
                Trace.Assert(((num7 == null) || (num7 == 0x13)) ? 1 : (num7 == 20));
                num8 = reader.ReadUInt64();
                reader.ReadUInt64();
                num9 = ((int) num8) & 0x7ff;
                num10 = ((int) (num8 >> 0x10)) & 0x7ff;
                num11 = ((int) (num8 >> 0x20)) & 0x7ff;
                num12 = ((int) (num8 >> 0x30)) & 0x7ff;
                num13 = ((int) (num8 >> 0x3b)) & 3;
                Trace.Assert(num9 == 0);
                Trace.Assert(num10 == 0);
                Trace.Assert(num11 == 0);
                Trace.Assert(num12 == 0);
                Trace.Assert(num13 == 0);
                num14 = reader.ReadUInt64();
                reader.ReadUInt64();
                num15 = reader.ReadUInt64();
                reader.ReadUInt64();
                num16 = ((int) num15) & 2;
                Trace.Assert(num16 == 0);
                num17 = reader.ReadUInt16();
                Trace.Assert((8 == (num17 & 0x8000)) == 0);
                si.Position += 0x12L;
                this.offBin = reader.ReadInt32();
                num18 = (num17 & 0x7fff) << 4;
                num19 = (num18 / 0x2000) / num6;
                buffer = new byte[Math.Max(0x2000, num18)];
                si.Position = (long) this.offBin;
                si.Read(buffer, 0, num18);
                if (num7 != null)
                {
                    goto Label_01C4;
                }
                buffer2 = Reform32.Encode32(buffer, num6, num19);
                goto Label_022A;
            Label_01C4:
                if (num7 != 0x13)
                {
                    goto Label_01E6;
                }
                buffer2 = Reform8.Encode8(buffer, num6 / 2, (num18 / 0x2000) / (num6 / 2));
                goto Label_022A;
            Label_01E6:
                if (num7 != 20)
                {
                    goto Label_020E;
                }
                buffer2 = Reform4.Encode4(buffer, num6 / 2, (num18 / 0x2000) / Math.Max(1, num6 / 2));
                goto Label_022A;
            Label_020E:
                throw new NotSupportedException("DPSM = " + ((int) num7) + "?");
            Label_022A:
                Array.Copy(buffer2, 0, this.gs, 0x100 * num5, num18);
                return;
            }

            public STim Do2(Stream si)
            {
                BinaryReader reader;
                ulong num;
                int num2;
                int num3;
                int num4;
                int num5;
                int num6;
                ulong num7;
                ulong num8;
                int num9;
                int num10;
                int num11;
                int num12;
                int num13;
                int num14;
                int num15;
                int num16;
                int num17;
                int num18;
                int num19;
                ulong num20;
                int num21;
                int num22;
                int num23;
                int num24;
                int num25;
                int num26;
                int num27;
                byte[] buffer;
                byte[] buffer2;
                STim tim;
                reader = new BinaryReader(si);
                reader.ReadUInt64();
                Trace.Assert(reader.ReadUInt64() == 0x3fL);
                reader.ReadUInt64();
                Trace.Assert(reader.ReadUInt64() == 0x34L);
                reader.ReadUInt64();
                Trace.Assert(reader.ReadUInt64() == 0x36L);
                num = reader.ReadUInt64();
                Trace.Assert(reader.ReadUInt64() == 0x16L);
                num2 = ((int) (num >> 20)) & 0x3f;
                num3 = ((int) (num >> 0x33)) & 15;
                num4 = ((int) (num >> 0x37)) & 1;
                num5 = ((int) (num >> 0x38)) & 0x1f;
                num6 = ((int) (num >> 0x3d)) & 7;
                Trace.Assert(num2 == 0x13);
                Trace.Assert(num3 == 0);
                Trace.Assert(num4 == 0);
                Trace.Assert(num5 == 0);
                Trace.Assert(num6 == 4);
                num7 = reader.ReadUInt64();
                Trace.Assert(reader.ReadUInt64() == 20L);
                num8 = reader.ReadUInt64();
                Trace.Assert(reader.ReadUInt64() == 6L);
                num9 = ((int) num8) & 0x3fff;
                num10 = ((int) (num8 >> 14)) & 0x3f;
                this.t0PSM = ((int) (num8 >> 20)) & 0x3f;
                num11 = ((int) (num8 >> 0x1a)) & 15;
                num12 = ((int) (num8 >> 30)) & 15;
                num13 = ((int) (num8 >> 0x22)) & 1;
                num14 = ((int) (num8 >> 0x23)) & 3;
                num15 = ((int) (num8 >> 0x25)) & 0x3fff;
                num16 = ((int) (num8 >> 0x33)) & 15;
                num17 = ((int) (num8 >> 0x37)) & 1;
                num18 = ((int) (num8 >> 0x38)) & 0x1f;
                num19 = ((int) (num8 >> 0x3d)) & 7;
                Trace.Assert((this.t0PSM == 0x13) ? 1 : (this.t0PSM == 20));
                Trace.Assert(num13 == 1);
                Trace.Assert(num16 == 0);
                Trace.Assert(num17 == 0);
                Trace.Assert(num19 == 0);
                num20 = reader.ReadUInt64();
                Trace.Assert(reader.ReadUInt64() == 8L);
                num21 = ((int) num20) & 3;
                num22 = ((int) (num20 >> 2)) & 3;
                num23 = ((int) (num20 >> 4)) & 0x3ff;
                num24 = ((int) (num20 >> 14)) & 0x3ff;
                num25 = ((int) (num20 >> 0x18)) & 0x3ff;
                num26 = ((int) (num20 >> 0x22)) & 0x3ff;
                num27 = (1 << (num11 & 0x1f)) * (1 << (num12 & 0x1f));
                buffer = new byte[Math.Max(0x2000, num27)];
                Array.Copy(this.gs, 0x100 * num9, buffer, 0, Math.Min(((int) this.gs.Length) - (0x100 * num9), Math.Min((int) buffer.Length, num27)));
                buffer2 = new byte[0x2000];
                Array.Copy(this.gs, 0x100 * num15, buffer2, 0, (int) buffer2.Length);
                tim = null;
                if (this.t0PSM != 0x13)
                {
                    goto Label_02C3;
                }
                tim = TexUt2.Decode8(buffer, buffer2, num10, 1 << (num11 & 0x1f), 1 << (num12 & 0x1f));
            Label_02C3:
                if (this.t0PSM != 20)
                {
                    goto Label_02EA;
                }
                tim = TexUt2.Decode4Ps(buffer, buffer2, num10, 1 << (num11 & 0x1f), 1 << (num12 & 0x1f), num18);
            Label_02EA:
                if (tim == null)
                {
                    goto Label_0336;
                }
                tim.tfx = num14;
                tim.tcc = num13;
                tim.wms = num21;
                tim.wmt = num22;
                tim.minu = num23;
                tim.maxu = num24;
                tim.minv = num25;
                tim.maxv = num26;
            Label_0336:
                return tim;
            }
        }

        private class Texi : RDForm.Hexi
        {
            public STim st;

            public Texi(int off, STim st)
            {
                base..ctor(off);
                this.st = st;
                return;
            }

            public Texi(int off, RDForm.MI mi, STim st)
            {
                base..ctor(off, mi);
                this.st = st;
                return;
            }
        }

        private class UniName
        {
            private SortedDictionary<string, string> dictUsed;

            public UniName()
            {
                this.dictUsed = new SortedDictionary<string, string>();
                base..ctor();
                return;
            }

            public string Get(string fn)
            {
                int num;
                string str;
                num = 0;
                goto Label_0053;
            Label_0004:
                str = Path.GetFileNameWithoutExtension(fn) + ((num != null) ? ("~" + ((int) (1 + num))) : "") + Path.GetExtension(fn);
                if (this.dictUsed.ContainsKey(str) != null)
                {
                    goto Label_004F;
                }
                this.dictUsed[str] = null;
                return str;
            Label_004F:
                num += 1;
            Label_0053:
                if (num < 100)
                {
                    goto Label_0004;
                }
                return fn;
            }
        }

        private class Vifi : RDForm.Hexi
        {
            public byte[] vifpkt;

            public Vifi(int off, byte[] vifpkt)
            {
                base..ctor(off);
                this.vifpkt = vifpkt;
                return;
            }

            public Vifi(int off, RDForm.MI mi, byte[] vifpkt)
            {
                base..ctor(off, mi);
                this.vifpkt = vifpkt;
                return;
            }
        }

        private delegate void WavePlayerDelegate(RDForm.WAVi wi);

        private class WAVi : RDForm.Hexi
        {
            public Wavo w;

            public WAVi(int off, Wavo w)
            {
                base..ctor(off);
                base.off = off;
                this.w = w;
                return;
            }
        }

        private class WUt
        {
            public WUt()
            {
                base..ctor();
                return;
            }

            public static string Usebar(string fpld, int kid, string nid)
            {
                string str;
                FileStream stream;
                BinaryWriter writer;
                FileStream stream2;
                Directory.CreateDirectory("tmp");
                str = Path.GetFullPath(@"tmp\" + Path.GetFileName(fpld) + ".bar");
                stream = File.Create(str);
            Label_002D:
                try
                {
                    writer = new BinaryWriter(stream, Encoding.ASCII);
                    writer.Write(Encoding.ASCII.GetBytes("BAR\x0001"));
                    writer.Write(1);
                    writer.Write(0);
                    writer.Write(0);
                    writer.Write(kid);
                    writer.Write(Encoding.ASCII.GetBytes(nid.PadRight(4, 0).Substring(0, 4)));
                    writer.Write(0x20);
                    stream2 = File.OpenRead(fpld);
                Label_0098:
                    try
                    {
                        writer.Write((int) stream2.Length);
                        writer.Write(new BinaryReader(stream2).ReadBytes((int) stream2.Length));
                        goto Label_00C9;
                    }
                    finally
                    {
                    Label_00BF:
                        if (stream2 == null)
                        {
                            goto Label_00C8;
                        }
                        stream2.Dispose();
                    Label_00C8:;
                    }
                Label_00C9:
                    goto Label_00D5;
                }
                finally
                {
                Label_00CB:
                    if (stream == null)
                    {
                        goto Label_00D4;
                    }
                    stream.Dispose();
                Label_00D4:;
                }
            Label_00D5:
                return str;
            }
        }
    }
}

