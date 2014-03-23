using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using khkh_xldMii.Properties;

namespace khkh_xldMii
{
    public class BCForm : Form
    {
        private readonly string[] missfs = ".....".Split(new[]
        {
            '.'
        });

        private readonly ILoadf pif;
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
        private OpenFileDialog openFileDialog1;
        private PictureBox p1;
        private PictureBox p2;
        private PictureBox p3;
        private PictureBox p4;
        private PictureBox p5;
        private PictureBox p6;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textBoxLog;
        private ToolTip toolTip1;

        public BCForm()
        {
            InitializeComponent();
        }

        public BCForm(ILoadf pf)
        {
            pif = pf;
            InitializeComponent();
        }

        private PictureBox[] PS
        {
            get
            {
                return new[]
                {
                    p1,
                    p2,
                    p3,
                    p4,
                    p5,
                    p6
                };
            }
        }

        private void BCForm_Load(object sender, EventArgs e)
        {
            if (File.Exists(Program.fBPxml))
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(Program.fBPxml);
                var xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
                xmlNamespaceManager.AddNamespace("a", "http://tempuri.org/BindPreset.xsd");
                foreach (XmlElement eli in xmlDocument.SelectNodes("/a:preset/a:item", xmlNamespaceManager))
                {
                    listBox1.Items.Add(new Preset(eli));
                }
            }
            string caption = "You can drop a file here to load.";
            toolTip1.SetToolTip(p1, caption);
            p1.AllowDrop = true;
            toolTip1.SetToolTip(p2, caption);
            p2.AllowDrop = true;
            toolTip1.SetToolTip(p3, caption);
            p3.AllowDrop = true;
            toolTip1.SetToolTip(p4, caption);
            p4.AllowDrop = true;
            toolTip1.SetToolTip(p5, caption);
            p5.AllowDrop = true;
            toolTip1.SetToolTip(p6, caption);
            p6.AllowDrop = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PictureBox[] pS = PS;
            IEnumerator enumerator = listBox1.SelectedItems.GetEnumerator();
            try
            {
                if (enumerator.MoveNext())
                {
                    var preset = (Preset) enumerator.Current;
                    for (int i = 0; i < 6; i++)
                    {
                        pS[i].Image = null;
                        toolTip1.SetToolTip(pS[i], null);
                        missfs[i] = null;
                    }
                    int num = 0;
                    for (int j = 0; j < 6; j++)
                    {
                        string nameOf = preset.GetNameOf(j);
                        if (nameOf.Length == 0)
                        {
                            pS[j].Image = Resources.DFH;
                        }
                        else
                        {
                            string text = UtSearchf.Find(nameOf);
                            if (text == null)
                            {
                                openFileDialog1.FileName = nameOf;
                                openFileDialog1.Filter = string.Concat(new[]
                                {
                                    "Missing file ",
                                    nameOf,
                                    "|",
                                    nameOf,
                                    "|*.*|*.*||"
                                });
                                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                                {
                                    text = openFileDialog1.FileName;
                                    UtSearchf.AddDirToList(Path.GetDirectoryName(text));
                                }
                            }
                            if (text == null)
                            {
                                pS[j].Image = Resources.NG;
                                toolTip1.SetToolTip(pS[j],
                                    "Missing file --- Find that file, then drag it and drop here.\n\n" + nameOf);
                                add2Log("Missing --- " + nameOf + "\n");
                                missfs[j] = nameOf;
                                num++;
                            }
                            else
                            {
                                pif.LoadOf(j, text);
                                pS[j].Image = Resources.Happy;
                                toolTip1.SetToolTip(pS[j], "Happy! no error. Loaded file is ...\n\n" + text);
                            }
                        }
                    }
                    pif.SetJointOf(1, preset.GetJointOf(1));
                    pif.SetJointOf(2, preset.GetJointOf(2));
                    if (num == 0)
                    {
                        add2Log("OK\n");
                    }
                    pif.DoRecalc();
                }
            }
            finally
            {
                var disposable = enumerator as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
        }

        private void add2Log(string text)
        {
            textBoxLog.Select(textBoxLog.TextLength, 0);
            textBoxLog.SelectedText = text.Replace("\r\n", "\n").Replace("\n", "\r\n");
        }

        private void p1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = ((e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
                ? DragDropEffects.Copy
                : e.Effect);
        }

        private void p1_DragDrop(object sender, DragEventArgs e)
        {
            var array = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (array != null)
            {
                string[] array2 = array;
                int num = 0;
                if (num < array2.Length)
                {
                    string text = array2[num];
                    PictureBox[] pS = PS;
                    int num2 = Array.IndexOf(pS, sender);
                    if (num2 >= 0)
                    {
                        pif.LoadOf(num2, text);
                        pif.DoRecalc();
                        pS[num2].Image = Resources.Happy;
                        toolTip1.SetToolTip(pS[num2], "Happy! there is no problem. Loaded file is ...\n\n" + text);
                        UtSearchf.AddDirToList(Path.GetDirectoryName(text));
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ((IVwer) pif).BackToViewer();
        }

        private void p1_Click(object sender, EventArgs e)
        {
        }

        private void p1_DoubleClick(object sender, EventArgs e)
        {
            int num = Array.IndexOf(PS, sender);
            if (num >= 0)
            {
                string text = missfs[num];
                openFileDialog1.Filter = "*.*|*.*||";
                if (text != null)
                {
                    openFileDialog1.Filter = text + "|" + text + "|*.*|*.*||";
                }
                openFileDialog1.FileName = text;
                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    string fileName = openFileDialog1.FileName;
                    pif.LoadOf(num, fileName);
                    PS[num].Image = Resources.Happy;
                    toolTip1.SetToolTip(PS[num], "Happy! there is no problem. Loaded file is ...\n\n" + fileName);
                    UtSearchf.AddDirToList(Path.GetDirectoryName(fileName));
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (new WC())
            {
                Process.Start("http://en.wikipedia.org/wiki/Emoticon");
            }
        }

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
            var componentResourceManager = new ComponentResourceManager(typeof (BCForm));
            label1 = new Label();
            listBox1 = new ListBox();
            label3 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            p1 = new PictureBox();
            p2 = new PictureBox();
            p3 = new PictureBox();
            p4 = new PictureBox();
            p5 = new PictureBox();
            p6 = new PictureBox();
            toolTip1 = new ToolTip(components);
            textBoxLog = new TextBox();
            button1 = new Button();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            openFileDialog1 = new OpenFileDialog();
            linkLabel1 = new LinkLabel();
            tableLayoutPanel1.SuspendLayout();
            ((ISupportInitialize) p1).BeginInit();
            ((ISupportInitialize) p2).BeginInit();
            ((ISupportInitialize) p3).BeginInit();
            ((ISupportInitialize) p4).BeginInit();
            ((ISupportInitialize) p5).BeginInit();
            ((ISupportInitialize) p6).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            base.SuspendLayout();
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(191, 14);
            label1.TabIndex = 0;
            label1.Text = "Choose one from below presets:";
            listBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            listBox1.FormattingEnabled = true;
            listBox1.IntegralHeight = false;
            listBox1.ItemHeight = 14;
            listBox1.Location = new Point(12, 26);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(194, 480);
            listBox1.TabIndex = 1;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            label3.AutoSize = true;
            label3.Location = new Point(3, 9);
            label3.Name = "label3";
            label3.Size = new Size(187, 14);
            label3.TabIndex = 3;
            label3.Text = "Display the status of bound files:";
            tableLayoutPanel1.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            tableLayoutPanel1.Controls.Add(label4, 0, 0);
            tableLayoutPanel1.Controls.Add(label5, 1, 0);
            tableLayoutPanel1.Controls.Add(label6, 2, 0);
            tableLayoutPanel1.Controls.Add(label7, 0, 1);
            tableLayoutPanel1.Controls.Add(label8, 0, 2);
            tableLayoutPanel1.Controls.Add(label9, 0, 3);
            tableLayoutPanel1.Controls.Add(p1, 1, 1);
            tableLayoutPanel1.Controls.Add(p2, 2, 1);
            tableLayoutPanel1.Controls.Add(p3, 1, 2);
            tableLayoutPanel1.Controls.Add(p4, 2, 2);
            tableLayoutPanel1.Controls.Add(p5, 1, 3);
            tableLayoutPanel1.Controls.Add(p6, 2, 3);
            tableLayoutPanel1.Location = new Point(6, 26);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33333f));
            tableLayoutPanel1.Size = new Size(452, 404);
            tableLayoutPanel1.TabIndex = 4;
            label4.AutoSize = true;
            label4.Location = new Point(4, 1);
            label4.Name = "label4";
            label4.Size = new Size(13, 14);
            label4.TabIndex = 0;
            label4.Text = "  ";
            label5.AutoSize = true;
            label5.Location = new Point(76, 1);
            label5.Name = "label5";
            label5.Size = new Size(37, 14);
            label5.TabIndex = 1;
            label5.Text = ".mdlx";
            label6.AutoSize = true;
            label6.Location = new Point(265, 1);
            label6.Name = "label6";
            label6.Size = new Size(39, 14);
            label6.TabIndex = 2;
            label6.Text = ".mset";
            label7.AutoSize = true;
            label7.Location = new Point(4, 16);
            label7.Name = "label7";
            label7.Size = new Size(34, 14);
            label7.TabIndex = 3;
            label7.Text = "Body";
            label8.AutoSize = true;
            label8.Location = new Point(4, 145);
            label8.Name = "label8";
            label8.Size = new Size(65, 14);
            label8.TabIndex = 4;
            label8.Text = "Right hand";
            label9.AutoSize = true;
            label9.Location = new Point(4, 274);
            label9.Name = "label9";
            label9.Size = new Size(59, 14);
            label9.TabIndex = 5;
            label9.Text = "Left hand";
            p1.Dock = DockStyle.Fill;
            p1.Image = (Image) componentResourceManager.GetObject("p1.Image");
            p1.Location = new Point(76, 19);
            p1.Name = "p1";
            p1.Size = new Size(182, 122);
            p1.SizeMode = PictureBoxSizeMode.CenterImage;
            p1.TabIndex = 6;
            p1.TabStop = false;
            p1.DoubleClick += p1_DoubleClick;
            p1.Click += p1_Click;
            p1.DragDrop += p1_DragDrop;
            p1.DragEnter += p1_DragEnter;
            p2.Dock = DockStyle.Fill;
            p2.Image = (Image) componentResourceManager.GetObject("p2.Image");
            p2.Location = new Point(265, 19);
            p2.Name = "p2";
            p2.Size = new Size(183, 122);
            p2.SizeMode = PictureBoxSizeMode.CenterImage;
            p2.TabIndex = 7;
            p2.TabStop = false;
            p2.DoubleClick += p1_DoubleClick;
            p2.DragDrop += p1_DragDrop;
            p2.DragEnter += p1_DragEnter;
            p3.Dock = DockStyle.Fill;
            p3.Image = (Image) componentResourceManager.GetObject("p3.Image");
            p3.Location = new Point(76, 148);
            p3.Name = "p3";
            p3.Size = new Size(182, 122);
            p3.SizeMode = PictureBoxSizeMode.CenterImage;
            p3.TabIndex = 8;
            p3.TabStop = false;
            p3.DoubleClick += p1_DoubleClick;
            p3.DragDrop += p1_DragDrop;
            p3.DragEnter += p1_DragEnter;
            p4.Dock = DockStyle.Fill;
            p4.Image = (Image) componentResourceManager.GetObject("p4.Image");
            p4.Location = new Point(265, 148);
            p4.Name = "p4";
            p4.Size = new Size(183, 122);
            p4.SizeMode = PictureBoxSizeMode.CenterImage;
            p4.TabIndex = 9;
            p4.TabStop = false;
            p4.DoubleClick += p1_DoubleClick;
            p4.DragDrop += p1_DragDrop;
            p4.DragEnter += p1_DragEnter;
            p5.Dock = DockStyle.Fill;
            p5.Image = (Image) componentResourceManager.GetObject("p5.Image");
            p5.Location = new Point(76, 277);
            p5.Name = "p5";
            p5.Size = new Size(182, 123);
            p5.SizeMode = PictureBoxSizeMode.CenterImage;
            p5.TabIndex = 10;
            p5.TabStop = false;
            p5.DoubleClick += p1_DoubleClick;
            p5.DragDrop += p1_DragDrop;
            p5.DragEnter += p1_DragEnter;
            p6.Dock = DockStyle.Fill;
            p6.Image = (Image) componentResourceManager.GetObject("p6.Image");
            p6.Location = new Point(265, 277);
            p6.Name = "p6";
            p6.Size = new Size(183, 123);
            p6.SizeMode = PictureBoxSizeMode.CenterImage;
            p6.TabIndex = 11;
            p6.TabStop = false;
            p6.DoubleClick += p1_DoubleClick;
            p6.DragDrop += p1_DragDrop;
            p6.DragEnter += p1_DragEnter;
            toolTip1.IsBalloon = true;
            textBoxLog.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            textBoxLog.BorderStyle = BorderStyle.None;
            textBoxLog.Location = new Point(125, 3);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ReadOnly = true;
            textBoxLog.Size = new Size(329, 48);
            textBoxLog.TabIndex = 6;
            toolTip1.SetToolTip(textBoxLog, "Log message goes here");
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(6, 3);
            button1.Name = "button1";
            button1.Size = new Size(113, 44);
            button1.TabIndex = 5;
            button1.Text = "Back to viewer";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(listBox1);
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(693, 518);
            splitContainer1.SplitterDistance = 218;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 7;
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel2;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            splitContainer2.Panel1.Controls.Add(label3);
            splitContainer2.Panel1.Controls.Add(tableLayoutPanel1);
            splitContainer2.Panel2.Controls.Add(linkLabel1);
            splitContainer2.Panel2.Controls.Add(textBoxLog);
            splitContainer2.Panel2.Controls.Add(button1);
            splitContainer2.Size = new Size(469, 518);
            splitContainer2.SplitterDistance = 433;
            splitContainer2.SplitterWidth = 6;
            splitContainer2.TabIndex = 5;
            linkLabel1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            linkLabel1.AutoSize = true;
            linkLabel1.ForeColor = SystemColors.ButtonShadow;
            linkLabel1.LinkArea = new LinkArea(54, 37);
            linkLabel1.Location = new Point(6, 54);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(504, 18);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text =
                "Emoticons are taken from wikipedia. Thanks wikipedia! http://en.wikipedia.org/wiki/Emoticon";
            linkLabel1.UseCompatibleTextRendering = true;
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            base.AutoScaleDimensions = new SizeF(7f, 14f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(693, 518);
            base.Controls.Add(splitContainer1);
            Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            base.Name = "BCForm";
            base.StartPosition = FormStartPosition.CenterScreen;
            Text = "Bind controller";
            base.Load += BCForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((ISupportInitialize) p1).EndInit();
            ((ISupportInitialize) p2).EndInit();
            ((ISupportInitialize) p3).EndInit();
            ((ISupportInitialize) p4).EndInit();
            ((ISupportInitialize) p5).EndInit();
            ((ISupportInitialize) p6).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            splitContainer2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private class Preset
        {
            private readonly XmlElement eli;
            private readonly XmlNamespaceManager xns;

            public Preset(XmlElement eli)
            {
                xns = new XmlNamespaceManager(eli.OwnerDocument.NameTable);
                xns.AddNamespace("a", "http://tempuri.org/BindPreset.xsd");
                this.eli = eli;
            }

            public override string ToString()
            {
                return UtXmlGettext.Select(eli, "a:display-name/text()", xns);
            }

            public int GetJointOf(int i)
            {
                string[] array =
                {
                    "body-joint",
                    "right-hand-joint",
                    "left-hand-joint"
                };
                string text = UtXmlGettext.Select(eli, "a:" + array[i] + "/text()", xns);
                if (text.Length != 0)
                {
                    return XmlConvert.ToInt32(text);
                }
                return -1;
            }

            public string GetNameOf(int i)
            {
                string[] array =
                {
                    "body-mdlx",
                    "body-mset",
                    "right-hand-mdlx",
                    "right-hand-mset",
                    "left-hand-mdlx",
                    "left-hand-mset"
                };
                return UtXmlGettext.Select(eli, "a:" + array[i] + "/text()", xns);
            }
        }
    }
}