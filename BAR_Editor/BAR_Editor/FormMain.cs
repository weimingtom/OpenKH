using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace BAR_Editor
{
    public class FormMain : Form
    {
        private static readonly FileVersionInfo Program =
            FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);

        private static readonly Dictionary<uint, TypeEntry> TypeMap;

        private Bar _bar;
        private Button _buttonAddSelect;
        private Button _buttonNew;
        private Button _buttonOpen;
        private Button _buttonSave;
        private ContextMenuStrip _contextMenuListbox;
        private ToolStripMenuItem _deleteFileToolStripMenuItem;
        private ToolStripMenuItem _extractFileToolStripMenuItem;
        private IContainer components;
        private string _fileName;
        private Label _infoId;
        private Label _infoType;
        private TextBox _inputIds;
        private NumericUpDown _inputType;
        private Label _labelIds;
        private Label _labelType;
        private ListBox _listBox;
        private OpenFileDialog _openFileDialog;

        private ToolStripMenuItem _replaceFileToolStripMenuItem;
        private SaveFileDialog _saveFileDialog;

        //The dictionarry I'm using.Contains all formats I know
        static FormMain()
        {
            var dictionary = new Dictionary<uint, TypeEntry>
            {
                {0x00, new TypeEntry("tmp", "Square temp")},
                {0x01, new TypeEntry("bar", "BAR Container")},
                {0x02, new TypeEntry("txt", "String/unknown")},
                {0x03, new TypeEntry("ai", "AI file")},
                {0x04, new TypeEntry("mdlx", "3D model")},
                {0x05, new TypeEntry("doct", "DOCT model loader")},
                {0x06, new TypeEntry("coct", "Hitbox(Collision)")},
                {0x07, new TypeEntry("RAW", "RAW textures")},
                {0x0a, new TypeEntry("tm2", "TIM2 Image")},
                {0x0b, new TypeEntry("coct", "Hitbox(Collision)")},
                {0x0c, new TypeEntry("spwn", "Spawn points")},
                {0x0d, new TypeEntry("bin", "ARD unknown")},
                {0x0e, new TypeEntry("sky", "Sky disposition")},
                {0x0f, new TypeEntry("coct", "Hitbox(Collision)")},
                {0x11, new TypeEntry("bar", "BAR Container")},
                {0x12, new TypeEntry("pax", "PAX Image")},
                {0x13, new TypeEntry("coct", "Hitbox(Collision)")},
                {0x14, new TypeEntry("bar", "BAR Container")},
                {0x16, new TypeEntry("anl", "Animation loader")},
                {0x18, new TypeEntry("imd", "IMGD Image")},
                {0x19, new TypeEntry("seqd", "SEQD layout")},
                {0x1c, new TypeEntry("lad", "LAYERD Layout")},
                {0x1d, new TypeEntry("imz", "IMGZ Image")},
                {0x1e, new TypeEntry("bar", "BAR Container")},
                {0x1f, new TypeEntry("seb", "SeBlock Audio")},
                {0x20, new TypeEntry("wd", "WD Audio")},
                {0x22, new TypeEntry("vsb", "IopVoice Audio")},
                {0x24, new TypeEntry("font", "BMP font")},
                {0x2a, new TypeEntry("min", "minigame formats")},
                {0x2c, new TypeEntry("bin", "unknown")},
                {0x2e, new TypeEntry("bar", "BAR Container")},
                {0x2f, new TypeEntry("vibd", "VIBD (vibration.bar)")},
                {0x30, new TypeEntry("vag", "VAG Audio")}
            };
            TypeMap = dictionary;
        }

        //Title of the windows
        public FormMain()
        {
            InitializeComponent();
            Text = Program.ProductName + " " + Program.FileVersion + " by " + Program.CompanyName;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        //Button Add
        private void buttonAddSelect_Click(object sender, EventArgs e)
        {
            var item = new Bar.BarFile
            {
                Type = (uint) _inputType.Value,
                Id = _inputIds.Text
            };
            try
            {
                TypeEntry entry = TypeMap[item.Type];
                _openFileDialog.Filter = entry.name + " file|*." + entry.Ext + "|All Files|*.*";
                _openFileDialog.DefaultExt = entry.Ext;
            }
            catch (KeyNotFoundException)
            {
                _openFileDialog.Filter = "All Files|*.*";
                _openFileDialog.DefaultExt = "bin";
            }
            _openFileDialog.FileName = item.Id + " [0x" + item.Type.ToString("X2") + "]." + _openFileDialog.DefaultExt;
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                item.Data = File.ReadAllBytes(_openFileDialog.FileName);
                _bar.FileList.Add(item);
                _listBox.Items.Add(item.Id + " [0x" + item.Type.ToString("X2") + "]");
                _inputType.Value = 0M;
                _inputIds.Text = "";
            }
        }

        //Button New
        private void buttonNew_Click(object sender, EventArgs e)
        {
            Cleanup();
            _bar = new Bar();
            SetUp();
        }

        //Button Open
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            _openFileDialog.Filter =
                "BAR file|*.bar|2DD file|*.2dd|2LD Layout file|*.2ld|A.FM Audio file|*.a.fm|ANB Baked Animation file|*.anb|ARD Event file|*.ard|MAP file|*.map|MAG Magic file|*.mag|MDLX Model file|*.mdlx|MSET Movement Set file|*.mset|PAX image|*.pax|AI file|*.ai|All Files|*.*";
            _openFileDialog.FileName = "";
            _openFileDialog.DefaultExt = "bar";
            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                OpenFile(_openFileDialog.FileName);
            }
        }

        //Button Open but with drag 'n drop
        private void buttonOpen_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var data = (string[]) e.Data.GetData(DataFormats.FileDrop);
                foreach (string str in data)
                {
                    if (File.Exists(str))
                    {
                        OpenFile(str);
                        return;
                    }
                }
            }
        }

        //Button Open but with drag 'n drop
        private void buttonOpen_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        //Button Save
        private void buttonSave_Click(object sender, EventArgs e)
        {
            _saveFileDialog.FileName = _fileName;
            _saveFileDialog.Filter =
                "BAR file|*.bar|2DD file|*.2dd|2LD Layout file|*.2ld|A.FM Audio file|*.a.fm|ANB Baked Animation file|*.anb|ARD Event file|*.ard|MAP file|*.map|MAG Magic file|*.mag|MDLX Model file|*.mdlx|MSET Movement Set file|*.mset|PAX image|*.pax|AI file|*.ai|All Files|*.*";
            _saveFileDialog.DefaultExt = "bar";
            if ((_bar != null) && (_saveFileDialog.ShowDialog() == DialogResult.OK))
            {
                _bar.Save(File.Open(_saveFileDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None));
            }
        }

        //Cleanup the BAR Editor
        private void Cleanup()
        {
            _buttonSave.Enabled = _buttonAddSelect.Enabled = _inputType.Enabled = _inputIds.Enabled = false;
            _listBox.Items.Clear();
            if (_bar == null)
            {
                _bar = null;
                _fileName = "";
            }
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = _listBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                _bar.FileList.RemoveAt(selectedIndex);
                _listBox.Items.RemoveAt(selectedIndex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void extractFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = _listBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                Bar.BarFile file = _bar.FileList[selectedIndex];
                try
                {
                    TypeEntry entry = TypeMap[file.Type];
                    _saveFileDialog.Filter = entry.name + " file|*." + entry.Ext + "|All Files|*.*";
                    _saveFileDialog.DefaultExt = entry.Ext;
                }
                catch (KeyNotFoundException)
                {
                    _saveFileDialog.Filter = "All Files|*.*";
                    _saveFileDialog.DefaultExt = "bin";
                }
                _saveFileDialog.FileName = file.Id + " [0x" + file.Type.ToString("X2") + "]." + _saveFileDialog.DefaultExt;
                if (_saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(_saveFileDialog.FileName, file.Data);
                }
            }
        }

        private void InitializeComponent()
        {
            components = new Container();
            _buttonOpen = new Button();
            _openFileDialog = new OpenFileDialog();
            _listBox = new ListBox();
            _contextMenuListbox = new ContextMenuStrip(components);
            _deleteFileToolStripMenuItem = new ToolStripMenuItem();
            _extractFileToolStripMenuItem = new ToolStripMenuItem();
            _replaceFileToolStripMenuItem = new ToolStripMenuItem();
            _buttonSave = new Button();
            _saveFileDialog = new SaveFileDialog();
            _buttonNew = new Button();
            _labelType = new Label();
            _inputType = new NumericUpDown();
            _labelIds = new Label();
            _inputIds = new TextBox();
            _buttonAddSelect = new Button();
            _infoType = new Label();
            _infoId = new Label();
            _contextMenuListbox.SuspendLayout();
            ((ISupportInitialize) (_inputType)).BeginInit();
            SuspendLayout();
            // 
            // buttonOpen
            // 
            _buttonOpen.AllowDrop = true;
            _buttonOpen.Location = new Point(12, 42);
            _buttonOpen.Name = "_buttonOpen";
            _buttonOpen.Size = new Size(75, 23);
            _buttonOpen.TabIndex = 0;
            _buttonOpen.Text = "Open";
            _buttonOpen.UseVisualStyleBackColor = true;
            _buttonOpen.Click += buttonOpen_Click;
            _buttonOpen.DragDrop += buttonOpen_DragDrop;
            _buttonOpen.DragEnter += buttonOpen_DragEnter;
            // 
            // openFileDialog
            // 
            _openFileDialog.FileName = "openFileDialog";
            // 
            // listBox
            // 
            _listBox.FormattingEnabled = true;
            _listBox.Location = new Point(13, 108);
            _listBox.Name = "_listBox";
            _listBox.Size = new Size(105, 147);
            _listBox.TabIndex = 1;
            _listBox.MouseUp += listBox_MouseUp;
            // 
            // contextMenuListbox
            // 
            _contextMenuListbox.Items.AddRange(new ToolStripItem[]
            {
                _deleteFileToolStripMenuItem,
                _extractFileToolStripMenuItem,
                _replaceFileToolStripMenuItem
            });
            _contextMenuListbox.Name = "_contextMenuListbox";
            _contextMenuListbox.ShowItemToolTips = false;
            _contextMenuListbox.Size = new Size(135, 70);
            // 
            // deleteFileToolStripMenuItem
            // 
            _deleteFileToolStripMenuItem.Name = "_deleteFileToolStripMenuItem";
            _deleteFileToolStripMenuItem.Size = new Size(134, 22);
            _deleteFileToolStripMenuItem.Text = "Delete file";
            _deleteFileToolStripMenuItem.Click += deleteFileToolStripMenuItem_Click;
            // 
            // extractFileToolStripMenuItem
            // 
            _extractFileToolStripMenuItem.Name = "_extractFileToolStripMenuItem";
            _extractFileToolStripMenuItem.Size = new Size(134, 22);
            _extractFileToolStripMenuItem.Text = "Extract file";
            _extractFileToolStripMenuItem.Click += extractFileToolStripMenuItem_Click;
            // 
            // replaceFileToolStripMenuItem
            // 
            _replaceFileToolStripMenuItem.Name = "_replaceFileToolStripMenuItem";
            _replaceFileToolStripMenuItem.Size = new Size(134, 22);
            _replaceFileToolStripMenuItem.Text = "Replace file";
            _replaceFileToolStripMenuItem.Click += replaceFileToolStripMenuItem_Click;
            // 
            // buttonSave
            // 
            _buttonSave.Enabled = false;
            _buttonSave.Location = new Point(12, 71);
            _buttonSave.Name = "_buttonSave";
            _buttonSave.Size = new Size(75, 23);
            _buttonSave.TabIndex = 2;
            _buttonSave.Text = "Save";
            _buttonSave.UseVisualStyleBackColor = true;
            _buttonSave.Click += buttonSave_Click;
            // 
            // buttonNew
            // 
            _buttonNew.Location = new Point(13, 13);
            _buttonNew.Name = "_buttonNew";
            _buttonNew.Size = new Size(75, 23);
            _buttonNew.TabIndex = 3;
            _buttonNew.Text = "New";
            _buttonNew.UseVisualStyleBackColor = true;
            _buttonNew.Click += buttonNew_Click;
            // 
            // labelType
            // 
            _labelType.AutoSize = true;
            _labelType.Location = new Point(94, 18);
            _labelType.Name = "_labelType";
            _labelType.Size = new Size(34, 13);
            _labelType.TabIndex = 5;
            _labelType.Text = "Type:";
            // 
            // inputType
            // 
            _inputType.Enabled = false;
            _inputType.Hexadecimal = true;
            _inputType.Location = new Point(135, 15);
            _inputType.Name = "_inputType";
            _inputType.Size = new Size(58, 20);
            _inputType.TabIndex = 6;
            _inputType.ValueChanged += inputType_ValueChanged;
            // 
            // labelIDS
            // 
            _labelIds.AutoSize = true;
            _labelIds.Location = new Point(97, 47);
            _labelIds.Name = "_labelIds";
            _labelIds.Size = new Size(21, 13);
            _labelIds.TabIndex = 7;
            _labelIds.Text = "ID:";
            // 
            // inputIDS
            // 
            _inputIds.Enabled = false;
            _inputIds.Location = new Point(135, 43);
            _inputIds.MaxLength = 4;
            _inputIds.Name = "_inputIds";
            _inputIds.Size = new Size(58, 20);
            _inputIds.TabIndex = 8;
            _inputIds.TextChanged += inputIDS_TextChanged;
            // 
            // buttonAddSelect
            // 
            _buttonAddSelect.AutoSize = true;
            _buttonAddSelect.Enabled = false;
            _buttonAddSelect.Location = new Point(135, 69);
            _buttonAddSelect.Name = "_buttonAddSelect";
            _buttonAddSelect.Size = new Size(55, 23);
            _buttonAddSelect.TabIndex = 10;
            _buttonAddSelect.Text = "Add File";
            _buttonAddSelect.UseVisualStyleBackColor = true;
            _buttonAddSelect.Click += buttonAddSelect_Click;
            // 
            // infoType
            // 
            _infoType.AutoSize = true;
            _infoType.Location = new Point(200, 18);
            _infoType.Name = "_infoType";
            _infoType.Size = new Size(0, 13);
            _infoType.TabIndex = 11;
            // 
            // infoID
            // 
            _infoId.AutoSize = true;
            _infoId.Location = new Point(200, 46);
            _infoId.Name = "_infoId";
            _infoId.Size = new Size(0, 13);
            _infoId.TabIndex = 12;
            // 
            // FormMain
            // 
            ClientSize = new Size(291, 262);
            Controls.Add(_infoId);
            Controls.Add(_infoType);
            Controls.Add(_buttonAddSelect);
            Controls.Add(_inputIds);
            Controls.Add(_labelIds);
            Controls.Add(_inputType);
            Controls.Add(_labelType);
            Controls.Add(_buttonNew);
            Controls.Add(_buttonSave);
            Controls.Add(_listBox);
            Controls.Add(_buttonOpen);
            Name = "FormMain";
            _contextMenuListbox.ResumeLayout(false);
            ((ISupportInitialize) (_inputType)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void inputIDS_TextChanged(object sender, EventArgs e)
        {
            if (_inputIds.Text.Length != 0)
            {
                var bytes = new byte[4];
                Encoding.ASCII.GetBytes(_inputIds.Text, 0, Math.Min(4, _inputIds.Text.Length), bytes, 0);
                _infoId.Text = BitConverter.ToString(bytes);
            }
            else
            {
                _infoId.Text = "";
            }
        }

        private void inputType_ValueChanged(object sender, EventArgs e)
        {
            TypeEntry entry;
            _infoType.Text = TypeMap.TryGetValue((uint) _inputType.Value, out entry) ? entry.name : "";
        }

        private void listBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _listBox.SelectedIndex = _listBox.IndexFromPoint(e.Location);
                if (_listBox.SelectedIndex != -1)
                {
                    _contextMenuListbox.Show(Cursor.Position);
                }
            }
        }

        private void OpenFile(string filename)
        {
            Cleanup();
            try
            {
                _bar = new Bar(File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.None));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Fatal error opening file", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                _bar = null;
                return;
            }
            _fileName = Path.GetFileName(filename);
            _listBox.BeginUpdate();
            for (int i = 0; i < _bar.FileCount; i++)
            {
                Bar.BarFile file = _bar.FileList[i];
                _listBox.Items.Add(file.Id + " [0x" + file.Type.ToString("X2") + "]");
            }
            if (_bar.FileCount > 0)
            {
                _listBox.SelectedIndex = 0;
            }
            _listBox.EndUpdate();
            SetUp();
        }

        private void replaceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = _listBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                Bar.BarFile file = _bar.FileList[selectedIndex];
                try
                {
                    TypeEntry entry = TypeMap[file.Type];
                    _openFileDialog.Filter = entry.name + " file|*." + entry.Ext + "|All Files|*.*";
                    _openFileDialog.DefaultExt = entry.Ext;
                }
                catch (KeyNotFoundException)
                {
                    _openFileDialog.Filter = "All Files|*.*";
                    _openFileDialog.DefaultExt = "bin";
                }
                _openFileDialog.FileName = file.Id + " [0x" + file.Type.ToString("X2") + "]." + _openFileDialog.DefaultExt;
                if (_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    file.Data = File.ReadAllBytes(_openFileDialog.FileName);
                }
            }
        }

        private void SetUp()
        {
            _buttonSave.Enabled = _buttonAddSelect.Enabled = _inputType.Enabled = _inputIds.Enabled = true;
        }

        private class TypeEntry
        {
            public readonly string Ext;
// ReSharper disable once InconsistentNaming
            public readonly string name;

            public TypeEntry(string e, string n)
            {
                Ext = e;
                name = n;
            }
        }
    }
}