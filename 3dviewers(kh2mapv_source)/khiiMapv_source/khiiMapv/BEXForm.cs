using khiiMapv.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace khiiMapv
{
	public class BEXForm : Form
	{
		private RDForm parent;
		private IContainer components;
		private Button bSelOut;
		private TextBox tbOutDir;
		private FolderBrowserDialog fbdOut;
		private Button bAddfp;
		private ImageList il;
		private ListView lvfp;
		private Button bExp;
		private OpenFileDialog ofdfp;
		private Button bOpenOut;
		private Label lCur;
		public BEXForm(RDForm exporter)
		{
			this.parent = exporter;
			this.InitializeComponent();
		}
		private void bSelOut_Click(object sender, EventArgs e)
		{
			this.fbdOut.SelectedPath = this.tbOutDir.Text;
			if (this.fbdOut.ShowDialog(this) == DialogResult.OK)
			{
				this.tbOutDir.Text = this.fbdOut.SelectedPath;
			}
		}
		private void bAddfp_Click(object sender, EventArgs e)
		{
			this.ofdfp.FileName = "";
			if (this.ofdfp.ShowDialog(this) == DialogResult.OK)
			{
				string[] fileNames = this.ofdfp.FileNames;
				for (int i = 0; i < fileNames.Length; i++)
				{
					string fp = fileNames[i];
					this.Addfp(fp);
				}
			}
		}
		public void Addfp(string fp)
		{
			int index;
			if ((index = this.lvfp.Items.IndexOfKey(fp)) >= 0)
			{
				this.lvfp.Items[index].Selected = true;
				return;
			}
			ListViewItem listViewItem = new ListViewItem(Path.GetFileName(fp));
			listViewItem.Tag = fp;
			using (Icon icon = Icon.ExtractAssociatedIcon(fp))
			{
				this.il.Images.Add(icon);
				listViewItem.ImageIndex = this.il.Images.Count - 1;
				listViewItem.Name = fp;
			}
			this.lvfp.Items.Add(listViewItem);
		}
		private void BEXForm_Load(object sender, EventArgs e)
		{
			this.tbOutDir.Text = Path.Combine(Application.StartupPath, "export");
		}
		private void bAddfp_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = (e.AllowedEffect & (e.Data.GetDataPresent(DataFormats.FileDrop) ? (DragDropEffects.Copy | DragDropEffects.Link) : DragDropEffects.None));
		}
		private void bAddfp_DragDrop(object sender, DragEventArgs e)
		{
			string[] array = e.Data.GetData(DataFormats.FileDrop) as string[];
			if (array != null)
			{
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string fp = array2[i];
					this.Addfp(fp);
				}
			}
		}
		private void lvfp_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				int count;
				while ((count = this.lvfp.SelectedItems.Count) != 0)
				{
					this.lvfp.SelectedItems[count - 1].Remove();
				}
			}
		}
		private void bExp_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show(this, "Are you ready?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
			{
				return;
			}
			foreach (ListViewItem listViewItem in this.lvfp.Items)
			{
				string text = (string)listViewItem.Tag;
				this.lCur.Text = text;
				base.Update();
				try
				{
					this.parent.LoadAny(text);
					this.parent.ExpallTo(Path.Combine(this.tbOutDir.Text, Path.GetFileName(text)));
				}
				catch (Exception)
				{
				}
			}
			this.lCur.Text = "...";
			this.lvfp.Items.Clear();
			Process.Start("explorer.exe", " \"" + this.tbOutDir.Text + "\"");
		}
		private void bOpenOut_Click(object sender, EventArgs e)
		{
			Directory.CreateDirectory(this.tbOutDir.Text);
			Process.Start("explorer.exe", " \"" + this.tbOutDir.Text + "\"");
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(BEXForm));
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
			this.bSelOut.AutoSize = true;
			this.bSelOut.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.bSelOut.Location = new Point(12, 12);
			this.bSelOut.Name = "bSelOut";
			this.bSelOut.Size = new Size(120, 22);
			this.bSelOut.TabIndex = 1;
			this.bSelOut.Text = "Select Output folder:";
			this.bSelOut.UseVisualStyleBackColor = true;
			this.bSelOut.Click += new EventHandler(this.bSelOut_Click);
			this.tbOutDir.Location = new Point(138, 14);
			this.tbOutDir.Name = "tbOutDir";
			this.tbOutDir.Size = new Size(442, 19);
			this.tbOutDir.TabIndex = 2;
			this.fbdOut.Description = "Select an Output folder:";
			this.bAddfp.AllowDrop = true;
			this.bAddfp.AutoSize = true;
			this.bAddfp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.bAddfp.ImageIndex = 0;
			this.bAddfp.ImageList = this.il;
			this.bAddfp.Location = new Point(12, 40);
			this.bAddfp.Name = "bAddfp";
			this.bAddfp.Size = new Size(95, 38);
			this.bAddfp.TabIndex = 3;
			this.bAddfp.Text = "Add files:";
			this.bAddfp.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.bAddfp.UseVisualStyleBackColor = true;
			this.bAddfp.Click += new EventHandler(this.bAddfp_Click);
			this.bAddfp.DragDrop += new DragEventHandler(this.bAddfp_DragDrop);
			this.bAddfp.DragEnter += new DragEventHandler(this.bAddfp_DragEnter);
			this.il.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("il.ImageStream");
			this.il.TransparentColor = Color.Transparent;
			this.il.Images.SetKeyName(0, "DROP1PG.ICO");
			this.lvfp.AllowDrop = true;
			this.lvfp.LargeImageList = this.il;
			this.lvfp.Location = new Point(12, 84);
			this.lvfp.Name = "lvfp";
			this.lvfp.Size = new Size(568, 234);
			this.lvfp.Sorting = SortOrder.Ascending;
			this.lvfp.TabIndex = 4;
			this.lvfp.UseCompatibleStateImageBehavior = false;
			this.lvfp.DragDrop += new DragEventHandler(this.bAddfp_DragDrop);
			this.lvfp.DragEnter += new DragEventHandler(this.bAddfp_DragEnter);
			this.lvfp.KeyDown += new KeyEventHandler(this.lvfp_KeyDown);
			this.bExp.AutoSize = true;
			this.bExp.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.bExp.Image = Resources.ROCKET;
			this.bExp.Location = new Point(505, 40);
			this.bExp.Name = "bExp";
			this.bExp.Size = new Size(75, 38);
			this.bExp.TabIndex = 6;
			this.bExp.Text = "Export now!";
			this.bExp.TextImageRelation = TextImageRelation.ImageAboveText;
			this.bExp.UseVisualStyleBackColor = true;
			this.bExp.Click += new EventHandler(this.bExp_Click);
			this.ofdfp.Filter = "KH2 files|*.map;*.mdlx;*.apdx;*.fm;*.2dd;*.bar;*.2ld;*.mset;*.pax;*.wd;*.vsb;*.ard;*.imd;*.mag";
			this.ofdfp.Multiselect = true;
			this.ofdfp.ReadOnlyChecked = true;
			this.bOpenOut.AutoSize = true;
			this.bOpenOut.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.bOpenOut.Image = Resources.search4files;
			this.bOpenOut.Location = new Point(138, 39);
			this.bOpenOut.Name = "bOpenOut";
			this.bOpenOut.Size = new Size(38, 38);
			this.bOpenOut.TabIndex = 5;
			this.bOpenOut.UseVisualStyleBackColor = true;
			this.bOpenOut.Click += new EventHandler(this.bOpenOut_Click);
			this.lCur.AutoSize = true;
			this.lCur.Location = new Point(182, 52);
			this.lCur.Name = "lCur";
			this.lCur.Size = new Size(11, 12);
			this.lCur.TabIndex = 0;
			this.lCur.Text = "...";
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(602, 339);
			base.Controls.Add(this.lCur);
			base.Controls.Add(this.bOpenOut);
			base.Controls.Add(this.bExp);
			base.Controls.Add(this.lvfp);
			base.Controls.Add(this.bAddfp);
			base.Controls.Add(this.tbOutDir);
			base.Controls.Add(this.bSelOut);
			base.Name = "BEXForm";
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "Batch Export";
			base.Load += new EventHandler(this.BEXForm_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
