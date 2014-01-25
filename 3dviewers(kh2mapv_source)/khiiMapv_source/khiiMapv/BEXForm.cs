namespace khiiMapv
{
    using khiiMapv.Properties;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

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
            base..ctor();
            this.parent = exporter;
            this.InitializeComponent();
            return;
        }

        public void Addfp(string fp)
        {
            int num;
            ListViewItem item;
            Icon icon;
            if ((num = this.lvfp.Items.IndexOfKey(fp)) < 0)
            {
                goto Label_002E;
            }
            this.lvfp.Items[num].Selected = 1;
            return;
        Label_002E:
            item = new ListViewItem(Path.GetFileName(fp));
            item.Tag = fp;
            icon = Icon.ExtractAssociatedIcon(fp);
        Label_0048:
            try
            {
                this.il.Images.Add(icon);
                item.ImageIndex = this.il.Images.Count - 1;
                item.Name = fp;
                goto Label_0084;
            }
            finally
            {
            Label_007A:
                if (icon == null)
                {
                    goto Label_0083;
                }
                icon.Dispose();
            Label_0083:;
            }
        Label_0084:
            this.lvfp.Items.Add(item);
            return;
        }

        private void bAddfp_Click(object sender, EventArgs e)
        {
            string str;
            string[] strArray;
            int num;
            this.ofdfp.FileName = "";
            if (this.ofdfp.ShowDialog(this) != 1)
            {
                goto Label_0044;
            }
            strArray = this.ofdfp.FileNames;
            num = 0;
            goto Label_003E;
        Label_002F:
            str = strArray[num];
            this.Addfp(str);
            num += 1;
        Label_003E:
            if (num < ((int) strArray.Length))
            {
                goto Label_002F;
            }
        Label_0044:
            return;
        }

        private void bAddfp_DragDrop(object sender, DragEventArgs e)
        {
            string[] strArray;
            string str;
            string[] strArray2;
            int num;
            strArray = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (strArray == null)
            {
                goto Label_0034;
            }
            strArray2 = strArray;
            num = 0;
            goto Label_002E;
        Label_001F:
            str = strArray2[num];
            this.Addfp(str);
            num += 1;
        Label_002E:
            if (num < ((int) strArray2.Length))
            {
                goto Label_001F;
            }
        Label_0034:
            return;
        }

        private void bAddfp_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect & ((e.Data.GetDataPresent(DataFormats.FileDrop) != null) ? 5 : 0);
            return;
        }

        private void BEXForm_Load(object sender, EventArgs e)
        {
            this.tbOutDir.Text = Path.Combine(Application.StartupPath, "export");
            return;
        }

        private void bExp_Click(object sender, EventArgs e)
        {
            ListViewItem item;
            string str;
            IEnumerator enumerator;
            IDisposable disposable;
            if (MessageBox.Show(this, "Are you ready?", Application.ProductName, 4, 0x30) == 6)
            {
                goto Label_0017;
            }
            return;
        Label_0017:
            enumerator = this.lvfp.Items.GetEnumerator();
        Label_0028:
            try
            {
                goto Label_0086;
            Label_002A:
                item = (ListViewItem) enumerator.Current;
                str = (string) item.Tag;
                this.lCur.Text = str;
                base.Update();
            Label_0054:
                try
                {
                    this.parent.LoadAny(str);
                    this.parent.ExpallTo(Path.Combine(this.tbOutDir.Text, Path.GetFileName(str)));
                    goto Label_0086;
                }
                catch (Exception)
                {
                Label_0083:
                    goto Label_0086;
                }
            Label_0086:
                if (enumerator.MoveNext() != null)
                {
                    goto Label_002A;
                }
                goto Label_00A1;
            }
            finally
            {
            Label_0090:
                disposable = enumerator as IDisposable;
                if (disposable == null)
                {
                    goto Label_00A0;
                }
                disposable.Dispose();
            Label_00A0:;
            }
        Label_00A1:
            this.lCur.Text = "...";
            this.lvfp.Items.Clear();
            Process.Start("explorer.exe", " \"" + this.tbOutDir.Text + "\"");
            return;
        }

        private void bOpenOut_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(this.tbOutDir.Text);
            Process.Start("explorer.exe", " \"" + this.tbOutDir.Text + "\"");
            return;
        }

        private void bSelOut_Click(object sender, EventArgs e)
        {
            this.fbdOut.SelectedPath = this.tbOutDir.Text;
            if (this.fbdOut.ShowDialog(this) != 1)
            {
                goto Label_003B;
            }
            this.tbOutDir.Text = this.fbdOut.SelectedPath;
        Label_003B:
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

        private void InitializeComponent()
        {
            ComponentResourceManager manager;
            this.components = new Container();
            manager = new ComponentResourceManager(typeof(BEXForm));
            this.bSelOut = new Button();
            this.tbOutDir = new TextBox();
            this.fbdOut = new FolderBrowserDialog();
            this.bAddfp = new Button();
            this.il = new ImageList(this.components);
            this.lvfp = new ListView();
            this.bExp = new Button();
            this.ofdfp = new OpenFileDialog();
            this.bOpenOut = new Button();
            this.lCur = new Label();
            base.SuspendLayout();
            this.bSelOut.AutoSize = 1;
            this.bSelOut.AutoSizeMode = 0;
            this.bSelOut.Location = new Point(12, 12);
            this.bSelOut.Name = "bSelOut";
            this.bSelOut.Size = new Size(120, 0x16);
            this.bSelOut.TabIndex = 1;
            this.bSelOut.Text = "Select Output folder:";
            this.bSelOut.UseVisualStyleBackColor = 1;
            this.bSelOut.Click += new EventHandler(this.bSelOut_Click);
            this.tbOutDir.Location = new Point(0x8a, 14);
            this.tbOutDir.Name = "tbOutDir";
            this.tbOutDir.Size = new Size(0x1ba, 0x13);
            this.tbOutDir.TabIndex = 2;
            this.fbdOut.Description = "Select an Output folder:";
            this.bAddfp.AllowDrop = 1;
            this.bAddfp.AutoSize = 1;
            this.bAddfp.AutoSizeMode = 0;
            this.bAddfp.ImageIndex = 0;
            this.bAddfp.ImageList = this.il;
            this.bAddfp.Location = new Point(12, 40);
            this.bAddfp.Name = "bAddfp";
            this.bAddfp.Size = new Size(0x5f, 0x26);
            this.bAddfp.TabIndex = 3;
            this.bAddfp.Text = "Add files:";
            this.bAddfp.TextImageRelation = 4;
            this.bAddfp.UseVisualStyleBackColor = 1;
            this.bAddfp.Click += new EventHandler(this.bAddfp_Click);
            this.bAddfp.DragDrop += new DragEventHandler(this.bAddfp_DragDrop);
            this.bAddfp.DragEnter += new DragEventHandler(this.bAddfp_DragEnter);
            this.il.ImageStream = (ImageListStreamer) manager.GetObject("il.ImageStream");
            this.il.TransparentColor = Color.Transparent;
            this.il.Images.SetKeyName(0, "DROP1PG.ICO");
            this.lvfp.AllowDrop = 1;
            this.lvfp.LargeImageList = this.il;
            this.lvfp.Location = new Point(12, 0x54);
            this.lvfp.Name = "lvfp";
            this.lvfp.Size = new Size(0x238, 0xea);
            this.lvfp.Sorting = 1;
            this.lvfp.TabIndex = 4;
            this.lvfp.UseCompatibleStateImageBehavior = 0;
            this.lvfp.DragDrop += new DragEventHandler(this.bAddfp_DragDrop);
            this.lvfp.DragEnter += new DragEventHandler(this.bAddfp_DragEnter);
            this.lvfp.KeyDown += new KeyEventHandler(this.lvfp_KeyDown);
            this.bExp.AutoSize = 1;
            this.bExp.AutoSizeMode = 0;
            this.bExp.Image = Resources.ROCKET;
            this.bExp.Location = new Point(0x1f9, 40);
            this.bExp.Name = "bExp";
            this.bExp.Size = new Size(0x4b, 0x26);
            this.bExp.TabIndex = 6;
            this.bExp.Text = "Export now!";
            this.bExp.TextImageRelation = 1;
            this.bExp.UseVisualStyleBackColor = 1;
            this.bExp.Click += new EventHandler(this.bExp_Click);
            this.ofdfp.Filter = "KH2 files|*.map;*.mdlx;*.apdx;*.fm;*.2dd;*.bar;*.2ld;*.mset;*.pax;*.wd;*.vsb;*.ard;*.imd;*.mag";
            this.ofdfp.Multiselect = 1;
            this.ofdfp.ReadOnlyChecked = 1;
            this.bOpenOut.AutoSize = 1;
            this.bOpenOut.AutoSizeMode = 0;
            this.bOpenOut.Image = Resources.search4files;
            this.bOpenOut.Location = new Point(0x8a, 0x27);
            this.bOpenOut.Name = "bOpenOut";
            this.bOpenOut.Size = new Size(0x26, 0x26);
            this.bOpenOut.TabIndex = 5;
            this.bOpenOut.UseVisualStyleBackColor = 1;
            this.bOpenOut.Click += new EventHandler(this.bOpenOut_Click);
            this.lCur.AutoSize = 1;
            this.lCur.Location = new Point(0xb6, 0x34);
            this.lCur.Name = "lCur";
            this.lCur.Size = new Size(11, 12);
            this.lCur.TabIndex = 0;
            this.lCur.Text = "...";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = 1;
            base.ClientSize = new Size(0x25a, 0x153);
            base.Controls.Add(this.lCur);
            base.Controls.Add(this.bOpenOut);
            base.Controls.Add(this.bExp);
            base.Controls.Add(this.lvfp);
            base.Controls.Add(this.bAddfp);
            base.Controls.Add(this.tbOutDir);
            base.Controls.Add(this.bSelOut);
            base.Name = "BEXForm";
            base.StartPosition = 4;
            this.Text = "Batch Export";
            base.Load += new EventHandler(this.BEXForm_Load);
            base.ResumeLayout(0);
            base.PerformLayout();
            return;
        }

        private void lvfp_KeyDown(object sender, KeyEventArgs e)
        {
            int num;
            if (e.KeyCode != 0x2e)
            {
                goto Label_0038;
            }
            goto Label_0024;
        Label_000C:
            this.lvfp.SelectedItems[num - 1].Remove();
        Label_0024:
            if ((num = this.lvfp.SelectedItems.Count) != null)
            {
                goto Label_000C;
            }
        Label_0038:
            return;
        }
    }
}

