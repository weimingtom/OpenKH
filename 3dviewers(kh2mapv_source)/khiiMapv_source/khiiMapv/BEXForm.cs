using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using khiiMapv.Properties;

namespace khiiMapv
{
    public class BEXForm : Form
    {
        private Button bAddfp;
        private Button bExp;
        private Button bOpenOut;
        private Button bSelOut;
        private IContainer components;
        private FolderBrowserDialog fbdOut;
        private ImageList il;
        private Label lCur;
        private ListView lvfp;
        private OpenFileDialog ofdfp;
        private RDForm parent;
        private TextBox tbOutDir;

        public BEXForm(RDForm exporter)
        {
            parent = exporter;
            InitializeComponent();
        }

        private void bSelOut_Click(object sender, EventArgs e)
        {
            fbdOut.SelectedPath = tbOutDir.Text;
            if (fbdOut.ShowDialog(this) == DialogResult.OK)
            {
                tbOutDir.Text = fbdOut.SelectedPath;
            }
        }

        private void bAddfp_Click(object sender, EventArgs e)
        {
            ofdfp.FileName = "";
            if (ofdfp.ShowDialog(this) == DialogResult.OK)
            {
                string[] fileNames = ofdfp.FileNames;
                for (int i = 0; i < fileNames.Length; i++)
                {
                    string fp = fileNames[i];
                    Addfp(fp);
                }
            }
        }

        public void Addfp(string fp)
        {
            int index;
            if ((index = lvfp.Items.IndexOfKey(fp)) >= 0)
            {
                lvfp.Items[index].Selected = true;
                return;
            }
            var listViewItem = new ListViewItem(Path.GetFileName(fp));
            listViewItem.Tag = fp;
            using (Icon icon = Icon.ExtractAssociatedIcon(fp))
            {
                il.Images.Add(icon);
                listViewItem.ImageIndex = il.Images.Count - 1;
                listViewItem.Name = fp;
            }
            lvfp.Items.Add(listViewItem);
        }

        private void BEXForm_Load(object sender, EventArgs e)
        {
            tbOutDir.Text = Path.Combine(Application.StartupPath, "export");
        }

        private void bAddfp_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = (e.AllowedEffect &
                        (e.Data.GetDataPresent(DataFormats.FileDrop)
                            ? (DragDropEffects.Copy | DragDropEffects.Link)
                            : DragDropEffects.None));
        }

        private void bAddfp_DragDrop(object sender, DragEventArgs e)
        {
            var array = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (array != null)
            {
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    string fp = array2[i];
                    Addfp(fp);
                }
            }
        }

        private void lvfp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                int count;
                while ((count = lvfp.SelectedItems.Count) != 0)
                {
                    lvfp.SelectedItems[count - 1].Remove();
                }
            }
        }

        private void bExp_Click(object sender, EventArgs e)
        {
            if (
                MessageBox.Show(this, "Are you ready?", Application.ProductName, MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation) != DialogResult.Yes)
            {
                return;
            }
            foreach (ListViewItem listViewItem in lvfp.Items)
            {
                var text = (string) listViewItem.Tag;
                lCur.Text = text;
                base.Update();
                try
                {
                    parent.LoadAny(text);
                    parent.ExpallTo(Path.Combine(tbOutDir.Text, Path.GetFileName(text)));
                }
                catch (Exception)
                {
                }
            }
            lCur.Text = "...";
            lvfp.Items.Clear();
            Process.Start("explorer.exe", " \"" + tbOutDir.Text + "\"");
        }

        private void bOpenOut_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(tbOutDir.Text);
            Process.Start("explorer.exe", " \"" + tbOutDir.Text + "\"");
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
            var componentResourceManager = new ComponentResourceManager(typeof (BEXForm));
            bSelOut = new Button();
            tbOutDir = new TextBox();
            fbdOut = new FolderBrowserDialog();
            bAddfp = new Button();
            il = new ImageList(components);
            lvfp = new ListView();
            bExp = new Button();
            ofdfp = new OpenFileDialog();
            bOpenOut = new Button();
            lCur = new Label();
            base.SuspendLayout();
            bSelOut.AutoSize = true;
            bSelOut.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bSelOut.Location = new Point(12, 12);
            bSelOut.Name = "bSelOut";
            bSelOut.Size = new Size(120, 22);
            bSelOut.TabIndex = 1;
            bSelOut.Text = "Select Output folder:";
            bSelOut.UseVisualStyleBackColor = true;
            bSelOut.Click += bSelOut_Click;
            tbOutDir.Location = new Point(138, 14);
            tbOutDir.Name = "tbOutDir";
            tbOutDir.Size = new Size(442, 19);
            tbOutDir.TabIndex = 2;
            fbdOut.Description = "Select an Output folder:";
            bAddfp.AllowDrop = true;
            bAddfp.AutoSize = true;
            bAddfp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bAddfp.ImageIndex = 0;
            bAddfp.ImageList = il;
            bAddfp.Location = new Point(12, 40);
            bAddfp.Name = "bAddfp";
            bAddfp.Size = new Size(95, 38);
            bAddfp.TabIndex = 3;
            bAddfp.Text = "Add files:";
            bAddfp.TextImageRelation = TextImageRelation.ImageBeforeText;
            bAddfp.UseVisualStyleBackColor = true;
            bAddfp.Click += bAddfp_Click;
            bAddfp.DragDrop += bAddfp_DragDrop;
            bAddfp.DragEnter += bAddfp_DragEnter;
            il.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("il.ImageStream");
            il.TransparentColor = Color.Transparent;
            il.Images.SetKeyName(0, "DROP1PG.ICO");
            lvfp.AllowDrop = true;
            lvfp.LargeImageList = il;
            lvfp.Location = new Point(12, 84);
            lvfp.Name = "lvfp";
            lvfp.Size = new Size(568, 234);
            lvfp.Sorting = SortOrder.Ascending;
            lvfp.TabIndex = 4;
            lvfp.UseCompatibleStateImageBehavior = false;
            lvfp.DragDrop += bAddfp_DragDrop;
            lvfp.DragEnter += bAddfp_DragEnter;
            lvfp.KeyDown += lvfp_KeyDown;
            bExp.AutoSize = true;
            bExp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bExp.Image = Resources.ROCKET;
            bExp.Location = new Point(505, 40);
            bExp.Name = "bExp";
            bExp.Size = new Size(75, 38);
            bExp.TabIndex = 6;
            bExp.Text = "Export now!";
            bExp.TextImageRelation = TextImageRelation.ImageAboveText;
            bExp.UseVisualStyleBackColor = true;
            bExp.Click += bExp_Click;
            ofdfp.Filter =
                "KH2 files|*.map;*.mdlx;*.apdx;*.fm;*.2dd;*.bar;*.2ld;*.mset;*.pax;*.wd;*.vsb;*.ard;*.imd;*.mag";
            ofdfp.Multiselect = true;
            ofdfp.ReadOnlyChecked = true;
            bOpenOut.AutoSize = true;
            bOpenOut.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bOpenOut.Image = Resources.search4files;
            bOpenOut.Location = new Point(138, 39);
            bOpenOut.Name = "bOpenOut";
            bOpenOut.Size = new Size(38, 38);
            bOpenOut.TabIndex = 5;
            bOpenOut.UseVisualStyleBackColor = true;
            bOpenOut.Click += bOpenOut_Click;
            lCur.AutoSize = true;
            lCur.Location = new Point(182, 52);
            lCur.Name = "lCur";
            lCur.Size = new Size(11, 12);
            lCur.TabIndex = 0;
            lCur.Text = "...";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(602, 339);
            base.Controls.Add(lCur);
            base.Controls.Add(bOpenOut);
            base.Controls.Add(bExp);
            base.Controls.Add(lvfp);
            base.Controls.Add(bAddfp);
            base.Controls.Add(tbOutDir);
            base.Controls.Add(bSelOut);
            base.Name = "BEXForm";
            base.StartPosition = FormStartPosition.CenterParent;
            Text = "Batch Export";
            base.Load += BEXForm_Load;
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}