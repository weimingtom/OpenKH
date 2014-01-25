namespace khkh_xldMii
{
    using khkh_xldMii.Properties;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class BCForm : Form
    {
        private Button button1;
        private IContainer components;
        private Label label1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private LinkLabel linkLabel1;
        private ListBox listBox1;
        private string[] missfs;
        private OpenFileDialog openFileDialog1;
        private PictureBox p1;
        private PictureBox p2;
        private PictureBox p3;
        private PictureBox p4;
        private PictureBox p5;
        private PictureBox p6;
        private ILoadf pif;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textBoxLog;
        private ToolTip toolTip1;

        public BCForm()
        {
            this.missfs = ".....".Split(new char[] { '.' });
            this.InitializeComponent();
        }

        public BCForm(ILoadf pf)
        {
            this.missfs = ".....".Split(new char[] { '.' });
            this.pif = pf;
            this.InitializeComponent();
        }

        private void add2Log(string text)
        {
            this.textBoxLog.Select(this.textBoxLog.TextLength, 0);
            this.textBoxLog.SelectedText = text.Replace("\r\n", "\n").Replace("\n", "\r\n");
        }

        private void BCForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(Program.fBPxml))
            {
                XmlDocument document = new XmlDocument();
                document.Load(Program.fBPxml);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(document.NameTable);
                nsmgr.AddNamespace("a", "http://tempuri.org/BindPreset.xsd");
                foreach (XmlElement element in document.SelectNodes("/a:preset/a:item", nsmgr))
                {
                    this.listBox1.Items.Add(new Preset(element));
                }
            }
            string caption = "You can drop a file here to load.";
            this.toolTip1.SetToolTip(this.p1, caption);
            this.p1.AllowDrop = true;
            this.toolTip1.SetToolTip(this.p2, caption);
            this.p2.AllowDrop = true;
            this.toolTip1.SetToolTip(this.p3, caption);
            this.p3.AllowDrop = true;
            this.toolTip1.SetToolTip(this.p4, caption);
            this.p4.AllowDrop = true;
            this.toolTip1.SetToolTip(this.p5, caption);
            this.p5.AllowDrop = true;
            this.toolTip1.SetToolTip(this.p6, caption);
            this.p6.AllowDrop = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ((IVwer) this.pif).BackToViewer();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(BCForm));
            this.label1 = new Label();
            this.listBox1 = new ListBox();
            this.label3 = new Label();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.label4 = new Label();
            this.label5 = new Label();
            this.label6 = new Label();
            this.label7 = new Label();
            this.label8 = new Label();
            this.label9 = new Label();
            this.p1 = new PictureBox();
            this.p2 = new PictureBox();
            this.p3 = new PictureBox();
            this.p4 = new PictureBox();
            this.p5 = new PictureBox();
            this.p6 = new PictureBox();
            this.toolTip1 = new ToolTip(this.components);
            this.textBoxLog = new TextBox();
            this.button1 = new Button();
            this.splitContainer1 = new SplitContainer();
            this.splitContainer2 = new SplitContainer();
            this.openFileDialog1 = new OpenFileDialog();
            this.linkLabel1 = new LinkLabel();
            this.tableLayoutPanel1.SuspendLayout();
            ((ISupportInitialize) this.p1).BeginInit();
            ((ISupportInitialize) this.p2).BeginInit();
            ((ISupportInitialize) this.p3).BeginInit();
            ((ISupportInitialize) this.p4).BeginInit();
            ((ISupportInitialize) this.p5).BeginInit();
            ((ISupportInitialize) this.p6).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0xbf, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose one from below presets:";
            this.listBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new Point(12, 0x1a);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new Size(0xc2, 480);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(3, 9);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0xbb, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "Display the status of bound files:";
            this.tableLayoutPanel1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.p1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.p2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.p3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.p4, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.p5, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.p6, 2, 3);
            this.tableLayoutPanel1.Location = new Point(6, 0x1a);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
            this.tableLayoutPanel1.Size = new Size(0x1c4, 0x194);
            this.tableLayoutPanel1.TabIndex = 4;
            this.label4.AutoSize = true;
            this.label4.Location = new Point(4, 1);
            this.label4.Name = "label4";
            this.label4.Size = new Size(13, 14);
            this.label4.TabIndex = 0;
            this.label4.Text = "  ";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(0x4c, 1);
            this.label5.Name = "label5";
            this.label5.Size = new Size(0x25, 14);
            this.label5.TabIndex = 1;
            this.label5.Text = ".mdlx";
            this.label6.AutoSize = true;
            this.label6.Location = new Point(0x109, 1);
            this.label6.Name = "label6";
            this.label6.Size = new Size(0x27, 14);
            this.label6.TabIndex = 2;
            this.label6.Text = ".mset";
            this.label7.AutoSize = true;
            this.label7.Location = new Point(4, 0x10);
            this.label7.Name = "label7";
            this.label7.Size = new Size(0x22, 14);
            this.label7.TabIndex = 3;
            this.label7.Text = "Body";
            this.label8.AutoSize = true;
            this.label8.Location = new Point(4, 0x91);
            this.label8.Name = "label8";
            this.label8.Size = new Size(0x41, 14);
            this.label8.TabIndex = 4;
            this.label8.Text = "Right hand";
            this.label9.AutoSize = true;
            this.label9.Location = new Point(4, 0x112);
            this.label9.Name = "label9";
            this.label9.Size = new Size(0x3b, 14);
            this.label9.TabIndex = 5;
            this.label9.Text = "Left hand";
            this.p1.Dock = DockStyle.Fill;
            this.p1.Image = (Image) manager.GetObject("p1.Image");
            this.p1.Location = new Point(0x4c, 0x13);
            this.p1.Name = "p1";
            this.p1.Size = new Size(0xb6, 0x7a);
            this.p1.SizeMode = PictureBoxSizeMode.CenterImage;
            this.p1.TabIndex = 6;
            this.p1.TabStop = false;
            this.p1.DoubleClick += new EventHandler(this.p1_DoubleClick);
            this.p1.Click += new EventHandler(this.p1_Click);
            this.p1.DragDrop += new DragEventHandler(this.p1_DragDrop);
            this.p1.DragEnter += new DragEventHandler(this.p1_DragEnter);
            this.p2.Dock = DockStyle.Fill;
            this.p2.Image = (Image) manager.GetObject("p2.Image");
            this.p2.Location = new Point(0x109, 0x13);
            this.p2.Name = "p2";
            this.p2.Size = new Size(0xb7, 0x7a);
            this.p2.SizeMode = PictureBoxSizeMode.CenterImage;
            this.p2.TabIndex = 7;
            this.p2.TabStop = false;
            this.p2.DoubleClick += new EventHandler(this.p1_DoubleClick);
            this.p2.DragDrop += new DragEventHandler(this.p1_DragDrop);
            this.p2.DragEnter += new DragEventHandler(this.p1_DragEnter);
            this.p3.Dock = DockStyle.Fill;
            this.p3.Image = (Image) manager.GetObject("p3.Image");
            this.p3.Location = new Point(0x4c, 0x94);
            this.p3.Name = "p3";
            this.p3.Size = new Size(0xb6, 0x7a);
            this.p3.SizeMode = PictureBoxSizeMode.CenterImage;
            this.p3.TabIndex = 8;
            this.p3.TabStop = false;
            this.p3.DoubleClick += new EventHandler(this.p1_DoubleClick);
            this.p3.DragDrop += new DragEventHandler(this.p1_DragDrop);
            this.p3.DragEnter += new DragEventHandler(this.p1_DragEnter);
            this.p4.Dock = DockStyle.Fill;
            this.p4.Image = (Image) manager.GetObject("p4.Image");
            this.p4.Location = new Point(0x109, 0x94);
            this.p4.Name = "p4";
            this.p4.Size = new Size(0xb7, 0x7a);
            this.p4.SizeMode = PictureBoxSizeMode.CenterImage;
            this.p4.TabIndex = 9;
            this.p4.TabStop = false;
            this.p4.DoubleClick += new EventHandler(this.p1_DoubleClick);
            this.p4.DragDrop += new DragEventHandler(this.p1_DragDrop);
            this.p4.DragEnter += new DragEventHandler(this.p1_DragEnter);
            this.p5.Dock = DockStyle.Fill;
            this.p5.Image = (Image) manager.GetObject("p5.Image");
            this.p5.Location = new Point(0x4c, 0x115);
            this.p5.Name = "p5";
            this.p5.Size = new Size(0xb6, 0x7b);
            this.p5.SizeMode = PictureBoxSizeMode.CenterImage;
            this.p5.TabIndex = 10;
            this.p5.TabStop = false;
            this.p5.DoubleClick += new EventHandler(this.p1_DoubleClick);
            this.p5.DragDrop += new DragEventHandler(this.p1_DragDrop);
            this.p5.DragEnter += new DragEventHandler(this.p1_DragEnter);
            this.p6.Dock = DockStyle.Fill;
            this.p6.Image = (Image) manager.GetObject("p6.Image");
            this.p6.Location = new Point(0x109, 0x115);
            this.p6.Name = "p6";
            this.p6.Size = new Size(0xb7, 0x7b);
            this.p6.SizeMode = PictureBoxSizeMode.CenterImage;
            this.p6.TabIndex = 11;
            this.p6.TabStop = false;
            this.p6.DoubleClick += new EventHandler(this.p1_DoubleClick);
            this.p6.DragDrop += new DragEventHandler(this.p1_DragDrop);
            this.p6.DragEnter += new DragEventHandler(this.p1_DragEnter);
            this.toolTip1.IsBalloon = true;
            this.textBoxLog.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.textBoxLog.BorderStyle = BorderStyle.None;
            this.textBoxLog.Location = new Point(0x7d, 3);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.Size = new Size(0x149, 0x30);
            this.textBoxLog.TabIndex = 6;
            this.toolTip1.SetToolTip(this.textBoxLog, "Log message goes here");
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.Location = new Point(6, 3);
            this.button1.Name = "button1";
            this.button1.Size = new Size(0x71, 0x2c);
            this.button1.TabIndex = 5;
            this.button1.Text = "Back to viewer";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.FixedPanel = FixedPanel.Panel1;
            this.splitContainer1.Location = new Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new Size(0x2b5, 0x206);
            this.splitContainer1.SplitterDistance = 0xda;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 7;
            this.splitContainer2.Dock = DockStyle.Fill;
            this.splitContainer2.FixedPanel = FixedPanel.Panel2;
            this.splitContainer2.Location = new Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = Orientation.Horizontal;
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer2.Panel2.Controls.Add(this.linkLabel1);
            this.splitContainer2.Panel2.Controls.Add(this.textBoxLog);
            this.splitContainer2.Panel2.Controls.Add(this.button1);
            this.splitContainer2.Size = new Size(0x1d5, 0x206);
            this.splitContainer2.SplitterDistance = 0x1b1;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 5;
            this.linkLabel1.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.ForeColor = SystemColors.ButtonShadow;
            this.linkLabel1.LinkArea = new LinkArea(0x36, 0x25);
            this.linkLabel1.Location = new Point(6, 0x36);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new Size(0x1f8, 0x12);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Emoticons are taken from wikipedia. Thanks wikipedia! http://en.wikipedia.org/wiki/Emoticon";
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            base.AutoScaleDimensions = new SizeF(7f, 14f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2b5, 0x206);
            base.Controls.Add(this.splitContainer1);
            this.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            base.Name = "BCForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Bind controller";
            base.Load += new EventHandler(this.BCForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((ISupportInitialize) this.p1).EndInit();
            ((ISupportInitialize) this.p2).EndInit();
            ((ISupportInitialize) this.p3).EndInit();
            ((ISupportInitialize) this.p4).EndInit();
            ((ISupportInitialize) this.p5).EndInit();
            ((ISupportInitialize) this.p6).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (new WC())
            {
                Process.Start("http://en.wikipedia.org/wiki/Emoticon");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PictureBox[] pS = this.PS;
            foreach (Preset preset in this.listBox1.SelectedItems)
            {
                for (int i = 0; i < 6; i++)
                {
                    pS[i].Image = null;
                    this.toolTip1.SetToolTip(pS[i], null);
                    this.missfs[i] = null;
                }
                int num2 = 0;
                for (int j = 0; j < 6; j++)
                {
                    string nameOf = preset.GetNameOf(j);
                    if (nameOf.Length == 0)
                    {
                        pS[j].Image = Resources.DFH;
                    }
                    else
                    {
                        string path = UtSearchf.Find(nameOf);
                        if (path == null)
                        {
                            this.openFileDialog1.FileName = nameOf;
                            this.openFileDialog1.Filter = "Missing file " + nameOf + "|" + nameOf + "|*.*|*.*||";
                            if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
                            {
                                path = this.openFileDialog1.FileName;
                                UtSearchf.AddDirToList(Path.GetDirectoryName(path));
                            }
                        }
                        if (path == null)
                        {
                            pS[j].Image = Resources.NG;
                            this.toolTip1.SetToolTip(pS[j], "Missing file --- Find that file, then drag it and drop here.\n\n" + nameOf);
                            this.add2Log("Missing --- " + nameOf + "\n");
                            this.missfs[j] = nameOf;
                            num2++;
                        }
                        else
                        {
                            this.pif.LoadOf(j, path);
                            pS[j].Image = Resources.Happy;
                            this.toolTip1.SetToolTip(pS[j], "Happy! no error. Loaded file is ...\n\n" + path);
                        }
                    }
                }
                this.pif.SetJointOf(1, preset.GetJointOf(1));
                this.pif.SetJointOf(2, preset.GetJointOf(2));
                if (num2 == 0)
                {
                    this.add2Log("OK\n");
                }
                this.pif.DoRecalc();
                break;
            }
        }

        private void p1_Click(object sender, EventArgs e)
        {
        }

        private void p1_DoubleClick(object sender, EventArgs e)
        {
            int index = Array.IndexOf(this.PS, sender);
            if (index >= 0)
            {
                string str = this.missfs[index];
                this.openFileDialog1.Filter = "*.*|*.*||";
                if (str != null)
                {
                    this.openFileDialog1.Filter = str + "|" + str + "|*.*|*.*||";
                }
                this.openFileDialog1.FileName = str;
                if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = this.openFileDialog1.FileName;
                    this.pif.LoadOf(index, fileName);
                    this.PS[index].Image = Resources.Happy;
                    this.toolTip1.SetToolTip(this.PS[index], "Happy! there is no problem. Loaded file is ...\n\n" + fileName);
                    UtSearchf.AddDirToList(Path.GetDirectoryName(fileName));
                }
            }
        }

        private void p1_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (data != null)
            {
                string[] strArray2 = data;
                int index = 0;
                while (index < strArray2.Length)
                {
                    string fp = strArray2[index];
                    PictureBox[] pS = this.PS;
                    int x = Array.IndexOf(pS, sender);
                    if (x < 0)
                    {
                        break;
                    }
                    this.pif.LoadOf(x, fp);
                    this.pif.DoRecalc();
                    pS[x].Image = Resources.Happy;
                    this.toolTip1.SetToolTip(pS[x], "Happy! there is no problem. Loaded file is ...\n\n" + fp);
                    UtSearchf.AddDirToList(Path.GetDirectoryName(fp));
                    return;
                }
            }
        }

        private void p1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = ((e.Data != null) && e.Data.GetDataPresent(DataFormats.FileDrop)) ? DragDropEffects.Copy : e.Effect;
        }

        private PictureBox[] PS
        {
            get
            {
                return new PictureBox[] { this.p1, this.p2, this.p3, this.p4, this.p5, this.p6 };
            }
        }

        private class Preset
        {
            private XmlElement eli;
            private XmlNamespaceManager xns;

            public Preset(XmlElement eli)
            {
                this.xns = new XmlNamespaceManager(eli.OwnerDocument.NameTable);
                this.xns.AddNamespace("a", "http://tempuri.org/BindPreset.xsd");
                this.eli = eli;
            }

            public int GetJointOf(int i)
            {
                string[] strArray = new string[] { "body-joint", "right-hand-joint", "left-hand-joint" };
                string s = UtXmlGettext.Select(this.eli, "a:" + strArray[i] + "/text()", this.xns);
                if (s.Length != 0)
                {
                    return XmlConvert.ToInt32(s);
                }
                return -1;
            }

            public string GetNameOf(int i)
            {
                string[] strArray = new string[] { "body-mdlx", "body-mset", "right-hand-mdlx", "right-hand-mset", "left-hand-mdlx", "left-hand-mset" };
                return UtXmlGettext.Select(this.eli, "a:" + strArray[i] + "/text()", this.xns);
            }

            public override string ToString()
            {
                return UtXmlGettext.Select(this.eli, "a:display-name/text()", this.xns);
            }
        }
    }
}

