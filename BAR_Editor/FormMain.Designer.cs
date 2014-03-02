namespace BAR_Editor
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.listBox = new System.Windows.Forms.ListBox();
            this.contextMenuListbox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateTypeIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonSave = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.buttonNew = new System.Windows.Forms.Button();
            this.labelType = new System.Windows.Forms.Label();
            this.inputType = new System.Windows.Forms.NumericUpDown();
            this.labelIDS = new System.Windows.Forms.Label();
            this.inputIDS = new System.Windows.Forms.TextBox();
            this.buttonAddSelect = new System.Windows.Forms.Button();
            this.infoType = new System.Windows.Forms.Label();
            this.infoID = new System.Windows.Forms.Label();
            this.contextMenuListbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputType)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.AllowDrop = true;
            this.buttonOpen.Location = new System.Drawing.Point(12, 42);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            this.buttonOpen.DragDrop += new System.Windows.Forms.DragEventHandler(this.buttonOpen_DragDrop);
            this.buttonOpen.DragEnter += new System.Windows.Forms.DragEventHandler(this.buttonOpen_DragEnter);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(13, 108);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(283, 147);
            this.listBox.TabIndex = 1;
            this.listBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBox_MouseUp);
            // 
            // contextMenuListbox
            // 
            this.contextMenuListbox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteFileToolStripMenuItem,
            this.extractFileToolStripMenuItem,
            this.replaceFileToolStripMenuItem,
            this.updateTypeIDToolStripMenuItem});
            this.contextMenuListbox.Name = "contextMenuListbox";
            this.contextMenuListbox.ShowItemToolTips = false;
            this.contextMenuListbox.Size = new System.Drawing.Size(169, 92);
            // 
            // deleteFileToolStripMenuItem
            // 
            this.deleteFileToolStripMenuItem.Name = "deleteFileToolStripMenuItem";
            this.deleteFileToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.deleteFileToolStripMenuItem.Text = "Delete file";
            this.deleteFileToolStripMenuItem.Click += new System.EventHandler(this.deleteFileToolStripMenuItem_Click);
            // 
            // extractFileToolStripMenuItem
            // 
            this.extractFileToolStripMenuItem.Name = "extractFileToolStripMenuItem";
            this.extractFileToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.extractFileToolStripMenuItem.Text = "Extract file";
            this.extractFileToolStripMenuItem.Click += new System.EventHandler(this.extractFileToolStripMenuItem_Click);
            // 
            // replaceFileToolStripMenuItem
            // 
            this.replaceFileToolStripMenuItem.Name = "replaceFileToolStripMenuItem";
            this.replaceFileToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.replaceFileToolStripMenuItem.Text = "Replace file";
            this.replaceFileToolStripMenuItem.Click += new System.EventHandler(this.replaceFileToolStripMenuItem_Click);
            // 
            // updateTypeIDToolStripMenuItem
            // 
            this.updateTypeIDToolStripMenuItem.Name = "updateTypeIDToolStripMenuItem";
            this.updateTypeIDToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.updateTypeIDToolStripMenuItem.Text = "Update Type && ID";
            this.updateTypeIDToolStripMenuItem.Click += new System.EventHandler(this.updateTypeIDToolStripMenuItem_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(12, 71);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Location = new System.Drawing.Point(13, 13);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(75, 23);
            this.buttonNew.TabIndex = 3;
            this.buttonNew.Text = "New";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(94, 18);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(34, 13);
            this.labelType.TabIndex = 5;
            this.labelType.Text = "Type:";
            // 
            // inputType
            // 
            this.inputType.Enabled = false;
            this.inputType.Hexadecimal = true;
            this.inputType.Location = new System.Drawing.Point(135, 15);
            this.inputType.Name = "inputType";
            this.inputType.Size = new System.Drawing.Size(58, 20);
            this.inputType.TabIndex = 6;
            this.inputType.ValueChanged += new System.EventHandler(this.inputType_ValueChanged);
            // 
            // labelIDS
            // 
            this.labelIDS.AutoSize = true;
            this.labelIDS.Location = new System.Drawing.Point(97, 47);
            this.labelIDS.Name = "labelIDS";
            this.labelIDS.Size = new System.Drawing.Size(21, 13);
            this.labelIDS.TabIndex = 7;
            this.labelIDS.Text = "ID:";
            // 
            // inputIDS
            // 
            this.inputIDS.Enabled = false;
            this.inputIDS.Location = new System.Drawing.Point(135, 43);
            this.inputIDS.MaxLength = 4;
            this.inputIDS.Name = "inputIDS";
            this.inputIDS.Size = new System.Drawing.Size(58, 20);
            this.inputIDS.TabIndex = 8;
            this.inputIDS.TextChanged += new System.EventHandler(this.inputIDS_TextChanged);
            // 
            // buttonAddSelect
            // 
            this.buttonAddSelect.AutoSize = true;
            this.buttonAddSelect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAddSelect.Enabled = false;
            this.buttonAddSelect.Location = new System.Drawing.Point(229, 71);
            this.buttonAddSelect.Name = "buttonAddSelect";
            this.buttonAddSelect.Size = new System.Drawing.Size(55, 23);
            this.buttonAddSelect.TabIndex = 10;
            this.buttonAddSelect.Text = "Add File";
            this.buttonAddSelect.UseVisualStyleBackColor = true;
            this.buttonAddSelect.Click += new System.EventHandler(this.buttonAddSelect_Click);
            // 
            // infoType
            // 
            this.infoType.AutoSize = true;
            this.infoType.Location = new System.Drawing.Point(200, 18);
            this.infoType.Name = "infoType";
            this.infoType.Size = new System.Drawing.Size(0, 13);
            this.infoType.TabIndex = 11;
            // 
            // infoID
            // 
            this.infoID.AutoSize = true;
            this.infoID.Location = new System.Drawing.Point(201, 48);
            this.infoID.Name = "infoID";
            this.infoID.Size = new System.Drawing.Size(0, 13);
            this.infoID.TabIndex = 12;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 262);
            this.Controls.Add(this.infoID);
            this.Controls.Add(this.infoType);
            this.Controls.Add(this.buttonAddSelect);
            this.Controls.Add(this.inputIDS);
            this.Controls.Add(this.labelIDS);
            this.Controls.Add(this.inputType);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.buttonOpen);
            this.Name = "FormMain";
            this.contextMenuListbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuListbox;
        private System.Windows.Forms.ToolStripMenuItem deleteFileToolStripMenuItem;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.ToolStripMenuItem extractFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceFileToolStripMenuItem;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.NumericUpDown inputType;
        private System.Windows.Forms.Label labelIDS;
        private System.Windows.Forms.TextBox inputIDS;
        private System.Windows.Forms.Button buttonAddSelect;
        private System.Windows.Forms.ToolStripMenuItem updateTypeIDToolStripMenuItem;
        private System.Windows.Forms.Label infoType;
        private System.Windows.Forms.Label infoID;
    }
}

