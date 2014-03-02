using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace BAR_Editor
{
    public partial class FormMain : Form
    {
        private class typeEntry
        {
            public string ext;
            public string name;
            public typeEntry(string e, string n) { this.ext = e; this.name = n; }
        }
        private static readonly System.Diagnostics.FileVersionInfo program = System.Diagnostics.FileVersionInfo.GetVersionInfo(System.Reflection.Assembly.GetEntryAssembly().Location);

        private const string barTypes = "BAR file|*.bar|2DD file|*.2dd|2LD Layout file|*.2ld|A.FM Additional file|*.a.fm|ANB Baked Animation file|*.anb|ARD Event file|*.ard|MAP file|*.map|MAG Magic file|*.mag|MDLX Model file|*.mdlx|MSET Movement Set file|*.mset|All Files|*.*";
        private static Dictionary<uint, typeEntry> typeMap = new Dictionary<uint, typeEntry>{
            //{2, new typeEntry("", "")},
            {0x00, new typeEntry("tmp", "Square temp")},
                {0x01, new typeEntry("bar", "BAR Container")},
                {0x02, new typeEntry("str", "String/unknown")},
                {0x03, new typeEntry("ai", "AI file")},
                {0x04, new typeEntry("mdlx", "3D model")},
                {0x05, new typeEntry("doct", "DOCT model loader")},
                {0x06, new typeEntry("coct", "Hitbox(Collision)")},
                {0x07, new typeEntry("RAW", "RAW textures")},
                {0x0a, new typeEntry("tm2", "TIM2 Image")},
                {0x0b, new typeEntry("coct", "Hitbox(Collision)")},
                {0x0c, new typeEntry("spwn", "Spawn points")},
                {0x0d, new typeEntry("bin", "ARD unknown")},
                {0x0e, new typeEntry("sky", "Sky disposition")},
                {0x0f, new typeEntry("coct", "Hitbox(Collision)")},
                {0x11, new typeEntry("bar", "BAR Container")},
                {0x12, new typeEntry("pax", "PAX Image")},
                {0x13, new typeEntry("coct", "Hitbox(Collision)")},
                {0x14, new typeEntry("bar", "BAR Container")},
                {0x16, new typeEntry("anl", "Animation loader")},
                {0x18, new typeEntry("imd", "IMGD Image")},
                {0x19, new typeEntry("seqd", "SEQD layout")},
                {0x1c, new typeEntry("lad", "LAYERD Layout")},
                {0x1d, new typeEntry("imz", "IMGZ Image")},
                {0x1e, new typeEntry("bar", "BAR Container")},
                {0x1f, new typeEntry("seb", "SeBlock Audio")},
                {0x20, new typeEntry("wd", "WD Audio")},
                {0x22, new typeEntry("vsb", "IopVoice Audio")},
                {0x24, new typeEntry("font", "BMP font")},
                {0x2a, new typeEntry("min", "minigame formats")},
                {0x2c, new typeEntry("bin", "unknown")},
                {0x2e, new typeEntry("bar", "BAR Container")},
                {0x2f, new typeEntry("vibd", "VIBD (vibration.bar)")},
                {0x30, new typeEntry("vag", "VAG Audio")}
        };

        private BAR bar = null;
        string fileName;

        public FormMain()
        {
            InitializeComponent();
            this.Text = program.ProductName + " " + program.FileVersion + " [" + program.CompanyName + "]";
        }
        private void Cleanup()
        {
            this.buttonSave.Enabled = this.buttonAddSelect.Enabled = this.inputType.Enabled = this.inputIDS.Enabled = false;
            this.listBox.Items.Clear();
            if (bar == null) { bar = null; fileName = ""; }
        }
        private void SetUp()
        {
            this.buttonSave.Enabled = this.buttonAddSelect.Enabled = this.inputType.Enabled = this.inputIDS.Enabled = true;
        }
        private void openFile(string filename, bool ro = false)
        {
            Cleanup();
            try { bar = new BAR(System.IO.File.Open(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)); }
            catch (Exception e) { MessageBox.Show(e.Message, "Fatal error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error); bar = null; return; }
            fileName = System.IO.Path.GetFileName(filename);
            this.listBox.BeginUpdate();
            for (int i = 0; i < bar.fileCount; i++)
            {
                BAR.BARFile b = bar.fileList[i];
                this.listBox.Items.Add(b.id + " [0x" + b.type.ToString("X2") + "]");
            }
            if (bar.fileCount > 0)
            {
                this.listBox.SelectedIndex = 0;
            }
            this.listBox.EndUpdate();
            SetUp();
        }

        /// <summary>Open file button</summary>
        private void buttonOpen_Click(object sender, EventArgs e) { openFileDialog.Filter = barTypes; openFileDialog.FileName = ""; openFileDialog.DefaultExt = "bar"; if (openFileDialog.ShowDialog() == DialogResult.OK) { openFile(openFileDialog.FileName, openFileDialog.ReadOnlyChecked); } }
        /// <summary>Change mouse icon on drag+drop</summary>
        private void buttonOpen_DragEnter(object sender, DragEventArgs e) { if (e.Data.GetDataPresent(DataFormats.FileDrop)) { e.Effect = DragDropEffects.Copy; } else { e.Effect = DragDropEffects.None; } }
        /// <summary>Change mouse icon on drag+drop</summary>
        private void buttonOpen_DragDrop(object sender, DragEventArgs e) { if (e.Data.GetDataPresent(DataFormats.FileDrop)) { string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop)); foreach (string fileLoc in filePaths) { if (System.IO.File.Exists(fileLoc)) { openFile(fileLoc); break; } } } }
        private void buttonNew_Click(object sender, EventArgs e)
        {
            Cleanup();
            bar = new BAR();
            SetUp();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = fileName; saveFileDialog.Filter = barTypes; saveFileDialog.DefaultExt = "bar";
            if (bar != null && saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    bar.save(System.IO.File.Open(saveFileDialog.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None));
                }
                catch (Exception r)
                {
                    MessageBox.Show(string.Format("{0}: {1}", r.GetType().ToString(), r.Message), "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void listBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                this.listBox.SelectedIndex = this.listBox.IndexFromPoint(e.Location);
                if (this.listBox.SelectedIndex != -1)
                {
                    this.contextMenuListbox.Show(Cursor.Position);
                }
            }
        }
        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = this.listBox.SelectedIndex;
            if (i != -1)
            {
                this.bar.fileList.RemoveAt(i);
                this.listBox.Items.RemoveAt(i);
            }
        }
        private void extractFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = this.listBox.SelectedIndex;
            if (i != -1)
            {
                BAR.BARFile b = this.bar.fileList[i];
                try
                {
                    typeEntry t = typeMap[b.type];
                    saveFileDialog.Filter = t.name + " file|*." + t.ext + "|All Files|*.*";
                    saveFileDialog.DefaultExt = t.ext;
                }
                catch (KeyNotFoundException) { saveFileDialog.Filter = "All Files|*.*"; saveFileDialog.DefaultExt = "bin"; }
                saveFileDialog.FileName = b.id + " [0x" + b.type.ToString("X2") + "]." + saveFileDialog.DefaultExt;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllBytes(saveFileDialog.FileName, b.data);
                }
            }
        }
        private void replaceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = this.listBox.SelectedIndex;
            if (i != -1)
            {
                BAR.BARFile b = this.bar.fileList[i];
                try {
                    typeEntry t = typeMap[b.type];
                    openFileDialog.Filter = t.name + " file|*." + t.ext + "|All Files|*.*";
                    openFileDialog.DefaultExt = t.ext;
                }
                catch (KeyNotFoundException) { openFileDialog.Filter = "All Files|*.*"; openFileDialog.DefaultExt = "bin"; }
                openFileDialog.FileName = b.id + " [0x" + b.type.ToString("X2") + "]." + openFileDialog.DefaultExt;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    b.data = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                }
            }
        }
        private void updateTypeIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = this.listBox.SelectedIndex;
            if (i != -1)
            {
                BAR.BARFile b = this.bar.fileList[i];
                b.type = (uint)this.inputType.Value;
                b.id = this.inputIDS.Text;
                this.listBox.Items[i] = b.id + " [0x" + b.type.ToString("X2") + "]";
            }
        }

        private void buttonAddSelect_Click(object sender, EventArgs e)
        {
            BAR.BARFile b = new BAR.BARFile()
            {
                type = (uint)this.inputType.Value,
                id = this.inputIDS.Text
            };
            try
            {
                typeEntry t = typeMap[b.type];
                openFileDialog.Filter = t.name + " file|*." + t.ext + "|All Files|*.*";
                openFileDialog.DefaultExt = t.ext;
            }
            catch (KeyNotFoundException) { openFileDialog.Filter = "All Files|*.*"; openFileDialog.DefaultExt = "bin"; }
            openFileDialog.FileName = b.id + " [0x" + b.type.ToString("X2") + "]." + openFileDialog.DefaultExt;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                b.data = System.IO.File.ReadAllBytes(openFileDialog.FileName);
                this.bar.fileList.Add(b);
                this.listBox.Items.Add(b.id + " [0x" + b.type.ToString("X2") + "]");
                this.inputType.Value = 0;
                this.inputIDS.Text = "";
            }
        }


        /// <summary>Show type description</summary>
        private void inputType_ValueChanged(object sender, EventArgs e) { typeEntry t; if (typeMap.TryGetValue((uint)inputType.Value, out t)) { infoType.Text = t.name; } else { infoType.Text = ""; } }
        /// <summary>Show hex version</summary>
        private void inputIDS_TextChanged(object sender, EventArgs e)
        {
            if (inputIDS.Text.Length != 0)
            {
                byte[] b = new byte[4];
                System.Text.Encoding.ASCII.GetBytes(inputIDS.Text, 0, Math.Min(4, inputIDS.Text.Length), b, 0);
                infoID.Text = BitConverter.ToString(b);
            }
            else { infoID.Text = ""; }
        }
    }
}
