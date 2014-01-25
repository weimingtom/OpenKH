namespace hex04BinTrack
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class UC : UserControl
    {
        private IContainer components;
        private bool useTransparent;
        private const int WM_ERASEBKGND = 20;

        public UC()
        {
            this.InitializeComponent();
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "UC";
            base.Load += new EventHandler(this.UC_Load);
            base.ResumeLayout(false);
        }

        private void UC_Load(object sender, EventArgs e)
        {
        }

        protected override void WndProc(ref Message m)
        {
            if (((m.Msg == 20) && !base.DesignMode) && this.useTransparent)
            {
                m.Result = new IntPtr(1);
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        public bool UseTransparent
        {
            get
            {
                return this.useTransparent;
            }
            set
            {
                this.useTransparent = value;
            }
        }
    }
}

