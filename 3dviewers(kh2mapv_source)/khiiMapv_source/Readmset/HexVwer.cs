namespace Readmset
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Windows.Forms;

    public class HexVwer : UserControl
    {
        [NonSerialized]
        private List<RangeMarked> alrm;
        private bool antiFlick;
        private int byteWidth;
        private IContainer components;
        [NonSerialized]
        private int curoff;
        [NonSerialized]
        private int cursel;
        [NonSerialized]
        private Hashtable mark2;
        [NonSerialized]
        private Hashtable marks;
        private int offDelta;
        private PgScrollType pgScrollType;
        private Bitmap pic;
        [NonSerialized]
        private MemoryStream si;
        private int unitPg;
        private const int WM_ERASEBKGND = 20;

        public HexVwer()
        {
            this.marks = new Hashtable();
            this.cursel = -1;
            this.mark2 = new Hashtable();
            this.unitPg = 0x200;
            this.antiFlick = 1;
            this.byteWidth = 0x10;
            this.pgScrollType = 1;
            this.pic = new Bitmap(1, 1);
            this.alrm = new List<RangeMarked>();
            base..ctor();
            this.InitializeComponent();
            return;
        }

        public void AddMark(int addr)
        {
            this.marks[(int) addr] = null;
            base.Invalidate();
            return;
        }

        public void AddRangeMarked(int off, int len, Color color, Color clrborder)
        {
            this.alrm.Add(new RangeMarked(off, len, color, clrborder));
            return;
        }

        public int CalcPgSize()
        {
            PgScrollType type;
            switch (this.pgScrollType)
            {
                case 0:
                    goto Label_0017;

                case 1:
                    goto Label_001E;
            }
            goto Label_002C;
        Label_0017:
            return this.unitPg;
        Label_001E:
            return (this.byteWidth * this.GetLineCnt());
        Label_002C:
            throw new NotSupportedException();
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

        public unsafe int GetLineCnt()
        {
            Size size;
            if (this.pic.Height != 1)
            {
                goto Label_0010;
            }
            return 1;
        Label_0010:
            return (&base.ClientSize.Height / this.pic.Height);
        }

        public int GetPos()
        {
            return this.curoff;
        }

        private void HexVwer_KeyDown(object sender, KeyEventArgs e)
        {
            Keys keys;
            switch ((e.KeyCode - 0x21))
            {
                case 0:
                    goto Label_00A2;

                case 1:
                    goto Label_00BD;

                case 2:
                    goto Label_00D7;

                case 3:
                    goto Label_0093;

                case 4:
                    goto Label_0067;

                case 5:
                    goto Label_0031;

                case 6:
                    goto Label_007D;

                case 7:
                    goto Label_004C;
            }
            return;
        Label_0031:
            this.SetPos(this.curoff - this.byteWidth);
            e.Handled = 1;
            return;
        Label_004C:
            this.SetPos(this.curoff + this.byteWidth);
            e.Handled = 1;
            return;
        Label_0067:
            this.SetPos(this.curoff + 1);
            e.Handled = 1;
            return;
        Label_007D:
            this.SetPos(this.curoff - 1);
            e.Handled = 1;
            return;
        Label_0093:
            this.SetPos(0);
            e.Handled = 1;
            return;
        Label_00A2:
            this.SetPos(this.curoff - this.CalcPgSize());
            e.Handled = 1;
            return;
        Label_00BD:
            this.SetPos(this.curoff + this.CalcPgSize());
            e.Handled = 1;
        Label_00D7:
            return;
        }

        private void HexVwer_Load(object sender, EventArgs e)
        {
            base.SetStyle(0x10, 1);
            return;
        }

        private unsafe void HexVwer_Paint(object sender, PaintEventArgs e)
        {
            SizeF ef;
            int num;
            int num2;
            Brush brush;
            Brush brush2;
            Brush brush3;
            Graphics graphics;
            int num3;
            int num4;
            int num5;
            int num6;
            string str;
            byte[] buffer;
            int num7;
            int num8;
            char ch;
            int num9;
            float num10;
            RangeMarked marked;
            int num11;
            int num12;
            int num13;
            int num14;
            SolidBrush brush4;
            RectangleF ef2;
            Pen pen;
            int num15;
            int num16;
            int num17;
            int num18;
            int num19;
            int num20;
            int num21;
            int num22;
            int num23;
            int num24;
            int num25;
            int num26;
            Size size;
            byte num27;
            byte num28;
            List<RangeMarked>.Enumerator enumerator;
            Size size2;
            if (this.si != null)
            {
                goto Label_0009;
            }
            return;
        Label_0009:
            ef = MeasureStrUtil.calc1(e.Graphics, this.Font);
            num = ((int) (&ef.Width * ((float) (((10 + (3 * this.byteWidth)) + 1) + this.byteWidth)))) + 1;
            num2 = (int) &ef.Height;
            if (this.pic.Width < num)
            {
                goto Label_0061;
            }
            if (this.pic.Height >= num2)
            {
                goto Label_006E;
            }
        Label_0061:
            this.pic = new Bitmap(num, num2);
        Label_006E:
            brush = new SolidBrush(this.ForeColor);
            brush2 = new SolidBrush(Color.FromArgb(30, Color.BlueViolet));
            brush3 = new SolidBrush(Color.FromArgb(80, Color.Green));
            graphics = Graphics.FromImage(this.pic);
        Label_00AD:
            try
            {
                num3 = &base.ClientSize.Height / num2;
                num4 = this.curoff;
                this.si.Position = (long) num4;
                num5 = 0;
                num6 = 0;
                goto Label_06A1;
            Label_00E1:
                graphics.Clear(this.BackColor);
                str = string.Format("{0:X8}: ", (int) (num4 + this.offDelta));
                buffer = new byte[this.byteWidth];
                num7 = 0;
                goto Label_0180;
            Label_011A:
                if ((num7 + num4) >= ((int) this.si.Length))
                {
                    goto Label_015C;
                }
                buffer[num7] = num27 = (byte) this.si.ReadByte();
                num28 = num27;
                str = str + &num28.ToString("X2");
                goto Label_016A;
            Label_015C:
                str = str + "  ";
            Label_016A:
                str = str + ((char) 0x20);
                num7 += 1;
            Label_0180:
                if (num7 < this.byteWidth)
                {
                    goto Label_011A;
                }
                str = str + ((char) 0x20);
                num8 = 0;
                goto Label_01E3;
            Label_019F:
                ch = buffer[num8];
                if (char.IsLetterOrDigit(ch) != null)
                {
                    goto Label_01BB;
                }
                if (ch == 0x20)
                {
                    goto Label_01BB;
                }
                if (ch != 0x5f)
                {
                    goto Label_01CD;
                }
            Label_01BB:
                str = str + ((char) ch);
                goto Label_01DD;
            Label_01CD:
                str = str + ((char) 0x2e);
            Label_01DD:
                num8 += 1;
            Label_01E3:
                if (num8 < this.byteWidth)
                {
                    goto Label_019F;
                }
                num9 = 0;
                goto Label_0311;
            Label_01F5:
                if (this.cursel != (num4 + num9))
                {
                    goto Label_0244;
                }
                graphics.FillRectangle(brush3, new RectangleF(&ef.Width * ((float) (10 + (3 * num9))), 0f, (&ef.Width * 2f) + 1f, &ef.Height));
                goto Label_030B;
            Label_0244:
                if (this.mark2.ContainsKey((int) (num4 + num9)) == null)
                {
                    goto Label_02B7;
                }
                graphics.FillRectangle(new SolidBrush((Color) this.mark2[(int) (num4 + num9)]), new RectangleF(&ef.Width * ((float) (10 + (3 * num9))), 0f, (&ef.Width * 2f) + 1f, &ef.Height));
                goto Label_030B;
            Label_02B7:
                if (this.marks.ContainsKey((int) (num4 + num9)) == null)
                {
                    goto Label_030B;
                }
                graphics.FillRectangle(brush2, new RectangleF(&ef.Width * ((float) (10 + (3 * num9))), 0f, (&ef.Width * 2f) + 1f, &ef.Height));
            Label_030B:
                num9 += 1;
            Label_0311:
                if (num9 < this.byteWidth)
                {
                    goto Label_01F5;
                }
                num10 = &ef.Width * 10f;
                enumerator = this.alrm.GetEnumerator();
            Label_033A:
                try
                {
                    goto Label_0636;
                Label_033F:
                    marked = &enumerator.Current;
                    num11 = marked.off - num4;
                    num12 = num11 + marked.len;
                    num13 = Math.Max(0, Math.Min(this.byteWidth, num11));
                    num14 = Math.Max(0, Math.Min(this.byteWidth, num12));
                    if (num13 == num14)
                    {
                        goto Label_0636;
                    }
                    brush4 = new SolidBrush(marked.clr);
                    &ef2 = new RectangleF(&ef.Width * ((float) (10 + (3 * num13))), 0f, &ef.Width * ((float) (3 * (num14 - num13))), (float) this.pic.Height);
                    graphics.FillRectangle(brush4, ef2);
                    pen = new Pen(marked.clrborder);
                    num15 = Math.Max(0, Math.Min(this.byteWidth, num11 - this.byteWidth));
                    num16 = Math.Max(0, Math.Min(this.byteWidth, num12 - this.byteWidth));
                    num17 = Math.Max(0, Math.Min(this.byteWidth, num11 + this.byteWidth));
                    num18 = Math.Max(0, Math.Min(this.byteWidth, num12 + this.byteWidth));
                    num19 = Math.Min(num17, num13);
                    num20 = Math.Max(num17, num13);
                    if (num19 == num20)
                    {
                        goto Label_04B7;
                    }
                    graphics.DrawLine(pen, num10 + (&ef.Width * ((float) (3 * num19))), 0f, (num10 + (&ef.Width * ((float) (3 * num20)))) - 1f, 0f);
                Label_04B7:
                    num21 = Math.Min(num18, num14);
                    num22 = Math.Max(num18, num14);
                    if (num21 == num22)
                    {
                        goto Label_050C;
                    }
                    graphics.DrawLine(pen, num10 + (&ef.Width * ((float) (3 * num21))), 0f, (num10 + (&ef.Width * ((float) (3 * num22)))) - 1f, 0f);
                Label_050C:
                    num23 = Math.Min(num15, num13);
                    num24 = Math.Max(num15, num13);
                    if (num23 == num24)
                    {
                        goto Label_0573;
                    }
                    graphics.DrawLine(pen, num10 + (&ef.Width * ((float) (3 * num23))), (float) (this.pic.Height - 1), (num10 + (&ef.Width * ((float) (3 * num24)))) - 1f, (float) (this.pic.Height - 1));
                Label_0573:
                    num25 = Math.Min(num16, num14);
                    num26 = Math.Max(num16, num14);
                    if (num25 == num26)
                    {
                        goto Label_05DA;
                    }
                    graphics.DrawLine(pen, num10 + (&ef.Width * ((float) (3 * num25))), (float) (this.pic.Height - 1), (num10 + (&ef.Width * ((float) (3 * num26)))) - 1f, (float) (this.pic.Height - 1));
                Label_05DA:
                    graphics.DrawLine(pen, &ef2.X, 0f, &ef2.X, (float) this.pic.Height);
                    graphics.DrawLine(pen, &ef2.Right - 1f, 0f, &ef2.Right - 1f, (float) this.pic.Height);
                Label_0636:
                    if (&enumerator.MoveNext() != null)
                    {
                        goto Label_033F;
                    }
                    goto Label_0652;
                }
                finally
                {
                Label_0644:
                    &enumerator.Dispose();
                }
            Label_0652:
                graphics.DrawString(str, this.Font, brush, 0f, 0f);
                e.Graphics.DrawImageUnscaled(this.pic, 0, num5);
                num5 += this.pic.Height;
                num6 += 1;
                num4 += this.byteWidth;
            Label_06A1:
                if (num6 >= num3)
                {
                    goto Label_06BA;
                }
                if (num4 < ((int) this.si.Length))
                {
                    goto Label_00E1;
                }
            Label_06BA:
                if (this.antiFlick == null)
                {
                    goto Label_06FA;
                }
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), Rectangle.FromLTRB(0, num5, this.pic.Width, &base.ClientSize.Height));
            Label_06FA:
                goto Label_0708;
            }
            finally
            {
            Label_06FC:
                if (graphics == null)
                {
                    goto Label_0707;
                }
                graphics.Dispose();
            Label_0707:;
            }
        Label_0708:
            return;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = 1;
            this.Font = new Font("ＭＳ ゴシック", 9f, 0, 3, 0x80);
            base.Name = "HexVwer";
            base.Load += new EventHandler(this.HexVwer_Load);
            base.Paint += new PaintEventHandler(this.HexVwer_Paint);
            base.KeyDown += new KeyEventHandler(this.HexVwer_KeyDown);
            base.ResumeLayout(0);
            return;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            KeyEventArgs args;
            Keys keys;
            keys = keyData;
            switch ((keys - 0x21))
            {
                case 0:
                    goto Label_002D;

                case 1:
                    goto Label_002D;

                case 2:
                    goto Label_0043;

                case 3:
                    goto Label_002D;

                case 4:
                    goto Label_002D;

                case 5:
                    goto Label_002D;

                case 6:
                    goto Label_002D;

                case 7:
                    goto Label_002D;
            }
            goto Label_0043;
        Label_002D:
            args = new KeyEventArgs(keyData);
            this.HexVwer_KeyDown(this, args);
            return args.Handled;
        Label_0043:
            return base.ProcessDialogKey(keyData);
        }

        public void SetBin(byte[] bin)
        {
            this.SetBin(bin, 0, Convert.ToInt32((int) bin.Length));
            return;
        }

        public void SetBin(byte[] bin, int off, int len)
        {
            this.si = new MemoryStream(bin, off, len, 0);
            base.Invalidate();
            return;
        }

        public void SetPos(int pos)
        {
            this.curoff = Math.Max(0, pos);
            base.Invalidate();
            return;
        }

        public void SetSel(int sel)
        {
            this.cursel = sel;
            base.Invalidate();
            return;
        }

        protected override unsafe void WndProc(ref Message m)
        {
            Graphics graphics;
            GraphicsState state;
            Size size;
            if (base.DesignMode != null)
            {
                goto Label_0097;
            }
            if (this.si == null)
            {
                goto Label_0097;
            }
            if (this.antiFlick == null)
            {
                goto Label_0097;
            }
            if (m.Msg != 20)
            {
                goto Label_0097;
            }
            graphics = Graphics.FromHdc(m.WParam);
        Label_0034:
            try
            {
                state = graphics.Save();
                graphics.ExcludeClip(Rectangle.FromLTRB(0, 0, this.pic.Width, &base.ClientSize.Height));
                graphics.FillRectangle(new SolidBrush(this.BackColor), base.ClientRectangle);
                graphics.Restore(state);
                m.Result = new IntPtr(1);
                goto Label_009E;
            }
            finally
            {
            Label_008D:
                if (graphics == null)
                {
                    goto Label_0096;
                }
                graphics.Dispose();
            Label_0096:;
            }
        Label_0097:
            base.WndProc(m);
        Label_009E:
            return;
        }

        [EditorBrowsable(0), Description("再描画する時のちらつきを軽減します(trueの場合)。")]
        public bool AntiFlick
        {
            get
            {
                return this.antiFlick;
            }
            set
            {
                this.antiFlick = value;
                return;
            }
        }

        [EditorBrowsable(0), Description("一行に表示するバイト数を指定します。")]
        public int ByteWidth
        {
            get
            {
                return this.byteWidth;
            }
            set
            {
                if (value >= 1)
                {
                    goto Label_000F;
                }
                throw new ArgumentOutOfRangeException("value");
            Label_000F:
                this.byteWidth = value;
                base.Invalidate();
                return;
            }
        }

        public IDictionary Mark2
        {
            get
            {
                return this.mark2;
            }
        }

        [EditorBrowsable(0), Description("左端に表示されるアドレスに加算する定数を指定します。")]
        public int OffDelta
        {
            get
            {
                return this.offDelta;
            }
            set
            {
                this.offDelta = value;
                base.Invalidate();
                return;
            }
        }

        [EditorBrowsable(0), Description("ページスクロールする時の移動量の考え方を指定します。\nAbsoluteの場合，常にUnitPgで指定された量を移動します。\nScreenSizeBasedの場合，画面に表示されている量を移動します。")]
        public PgScrollType PgScroll
        {
            get
            {
                return this.pgScrollType;
            }
            set
            {
                this.pgScrollType = value;
                return;
            }
        }

        public List<RangeMarked> RangeMarkedList
        {
            get
            {
                return this.alrm;
            }
        }

        [Description("絶対量でページスクロールする時の移動量。PgScrollがAbsoluteである場合に採用されます。"), EditorBrowsable(0)]
        public int UnitPg
        {
            get
            {
                return this.unitPg;
            }
            set
            {
                this.unitPg = value;
                return;
            }
        }

        private class MeasureStrUtil
        {
            public MeasureStrUtil()
            {
                base..ctor();
                return;
            }

            public static unsafe SizeF calc1(Graphics cv, Font font)
            {
                SizeF ef;
                SizeF ef2;
                ef = cv.MeasureString("iw", font);
                ef2 = cv.MeasureString("iwiw", font);
                return new SizeF((&ef2.Width - &ef.Width) / 2f, &ef2.Height);
            }
        }
    }
}

