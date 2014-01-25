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
            base..ctor();
            this.InitializeComponent();
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = 1;
            base.Name = "UC";
            base.Load += new EventHandler(this.UC_Load);
            base.ResumeLayout(0);
            return;
        }

        private void UC_Load(object sender, EventArgs e)
        {
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 20)
            {
                goto Label_0027;
            }
            if (base.DesignMode != null)
            {
                goto Label_0027;
            }
            if (this.useTransparent == null)
            {
                goto Label_0027;
            }
            m.Result = new IntPtr(1);
            return;
        Label_0027:
            base.WndProc(m);
            return;
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
                return;
            }
        }
    }
}

