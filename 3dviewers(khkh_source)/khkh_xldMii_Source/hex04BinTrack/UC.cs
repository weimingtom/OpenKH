using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace hex04BinTrack
{
    public class UC : UserControl
    {
        private const int WM_ERASEBKGND = 20;
        private IContainer components;
        private bool useTransparent;

        public UC()
        {
            InitializeComponent();
        }

        public bool UseTransparent
        {
            get { return useTransparent; }
            set { useTransparent = value; }
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
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Name = "UC";
            base.Load += UC_Load;
            base.ResumeLayout(false);
        }

        private void UC_Load(object sender, EventArgs e)
        {
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 20 && !base.DesignMode && useTransparent)
            {
                m.Result = new IntPtr(1);
                return;
            }
            base.WndProc(ref m);
        }
    }
}