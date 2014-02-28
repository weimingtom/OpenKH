using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
namespace Readmset
{
	public class HexVwer : UserControl
	{
		private class MeasureStrUtil
		{
			public static SizeF calc1(Graphics cv, Font font)
			{
				SizeF sizeF = cv.MeasureString("iw", font);
				SizeF sizeF2 = cv.MeasureString("iwiw", font);
				return new SizeF((sizeF2.Width - sizeF.Width) / 2f, sizeF2.Height);
			}
		}
		private const int WM_ERASEBKGND = 20;
		[NonSerialized]
		private MemoryStream si;
		[NonSerialized]
		private int curoff;
		[NonSerialized]
		private Hashtable marks = new Hashtable();
		[NonSerialized]
		private int cursel = -1;
		[NonSerialized]
		private Hashtable mark2 = new Hashtable();
		private int unitPg = 512;
		private bool antiFlick = true;
		private int offDelta;
		private int byteWidth = 16;
		private PgScrollType pgScrollType = PgScrollType.ScreenSizeBased;
		private Bitmap pic = new Bitmap(1, 1);
		[NonSerialized]
		private List<RangeMarked> alrm = new List<RangeMarked>();
		private IContainer components;
		[Description("絶対量でページスクロールする時の移動量。PgScrollがAbsoluteである場合に採用されます。"), EditorBrowsable(EditorBrowsableState.Always)]
		public int UnitPg
		{
			get
			{
				return this.unitPg;
			}
			set
			{
				this.unitPg = value;
			}
		}
		[Description("再描画する時のちらつきを軽減します(trueの場合)。"), EditorBrowsable(EditorBrowsableState.Always)]
		public bool AntiFlick
		{
			get
			{
				return this.antiFlick;
			}
			set
			{
				this.antiFlick = value;
			}
		}
		[Description("左端に表示されるアドレスに加算する定数を指定します。"), EditorBrowsable(EditorBrowsableState.Always)]
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
			}
		}
		[Description("一行に表示するバイト数を指定します。"), EditorBrowsable(EditorBrowsableState.Always)]
		public int ByteWidth
		{
			get
			{
				return this.byteWidth;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.byteWidth = value;
				base.Invalidate();
			}
		}
		[Description("ページスクロールする時の移動量の考え方を指定します。\nAbsoluteの場合，常にUnitPgで指定された量を移動します。\nScreenSizeBasedの場合，画面に表示されている量を移動します。"), EditorBrowsable(EditorBrowsableState.Always)]
		public PgScrollType PgScroll
		{
			get
			{
				return this.pgScrollType;
			}
			set
			{
				this.pgScrollType = value;
			}
		}
		public IDictionary Mark2
		{
			get
			{
				return this.mark2;
			}
		}
		public List<RangeMarked> RangeMarkedList
		{
			get
			{
				return this.alrm;
			}
		}
		public HexVwer()
		{
			this.InitializeComponent();
		}
		public void SetBin(byte[] bin)
		{
			this.SetBin(bin, 0, Convert.ToInt32(bin.Length));
		}
		public void SetBin(byte[] bin, int off, int len)
		{
			this.si = new MemoryStream(bin, off, len, false);
			base.Invalidate();
		}
		public int GetLineCnt()
		{
			if (this.pic.Height == 1)
			{
				return 1;
			}
			return base.ClientSize.Height / this.pic.Height;
		}
		private void HexVwer_Paint(object sender, PaintEventArgs e)
		{
			if (this.si == null)
			{
				return;
			}
			SizeF sizeF = HexVwer.MeasureStrUtil.calc1(e.Graphics, this.Font);
			int num = (int)(sizeF.Width * (float)(10 + 3 * this.byteWidth + 1 + this.byteWidth)) + 1;
			int num2 = (int)sizeF.Height;
			if (this.pic.Width < num || this.pic.Height < num2)
			{
				this.pic = new Bitmap(num, num2);
			}
			Brush brush = new SolidBrush(this.ForeColor);
			Brush brush2 = new SolidBrush(Color.FromArgb(30, Color.BlueViolet));
			Brush brush3 = new SolidBrush(Color.FromArgb(80, Color.Green));
			using (Graphics graphics = Graphics.FromImage(this.pic))
			{
				int num3 = base.ClientSize.Height / num2;
				int num4 = this.curoff;
				this.si.Position = (long)num4;
				int num5 = 0;
				int num6 = 0;
				while (num6 < num3 && num4 < (int)this.si.Length)
				{
					graphics.Clear(this.BackColor);
					string text = string.Format("{0:X8}: ", num4 + this.offDelta);
					byte[] array = new byte[this.byteWidth];
					for (int i = 0; i < this.byteWidth; i++)
					{
						if (i + num4 < (int)this.si.Length)
						{
							text += (array[i] = (byte)this.si.ReadByte()).ToString("X2");
						}
						else
						{
							text += "  ";
						}
						text += ' ';
					}
					text += ' ';
					for (int j = 0; j < this.byteWidth; j++)
					{
						char c = (char)array[j];
						if (char.IsLetterOrDigit(c) || c == ' ' || c == '_')
						{
							text += c;
						}
						else
						{
							text += '.';
						}
					}
					for (int k = 0; k < this.byteWidth; k++)
					{
						if (this.cursel == num4 + k)
						{
							graphics.FillRectangle(brush3, new RectangleF(sizeF.Width * (float)(10 + 3 * k), 0f, sizeF.Width * 2f + 1f, sizeF.Height));
						}
						else
						{
							if (this.mark2.ContainsKey(num4 + k))
							{
								graphics.FillRectangle(new SolidBrush((Color)this.mark2[num4 + k]), new RectangleF(sizeF.Width * (float)(10 + 3 * k), 0f, sizeF.Width * 2f + 1f, sizeF.Height));
							}
							else
							{
								if (this.marks.ContainsKey(num4 + k))
								{
									graphics.FillRectangle(brush2, new RectangleF(sizeF.Width * (float)(10 + 3 * k), 0f, sizeF.Width * 2f + 1f, sizeF.Height));
								}
							}
						}
					}
					float num7 = sizeF.Width * 10f;
					foreach (RangeMarked current in this.alrm)
					{
						int num8 = current.off - num4;
						int num9 = num8 + current.len;
						int num10 = Math.Max(0, Math.Min(this.byteWidth, num8));
						int num11 = Math.Max(0, Math.Min(this.byteWidth, num9));
						if (num10 != num11)
						{
							SolidBrush brush4 = new SolidBrush(current.clr);
							RectangleF rect = new RectangleF(sizeF.Width * (float)(10 + 3 * num10), 0f, sizeF.Width * (float)(3 * (num11 - num10)), (float)this.pic.Height);
							graphics.FillRectangle(brush4, rect);
							Pen pen = new Pen(current.clrborder);
							int val = Math.Max(0, Math.Min(this.byteWidth, num8 - this.byteWidth));
							int val2 = Math.Max(0, Math.Min(this.byteWidth, num9 - this.byteWidth));
							int val3 = Math.Max(0, Math.Min(this.byteWidth, num8 + this.byteWidth));
							int val4 = Math.Max(0, Math.Min(this.byteWidth, num9 + this.byteWidth));
							int num12 = Math.Min(val3, num10);
							int num13 = Math.Max(val3, num10);
							if (num12 != num13)
							{
								graphics.DrawLine(pen, num7 + sizeF.Width * (float)(3 * num12), 0f, num7 + sizeF.Width * (float)(3 * num13) - 1f, 0f);
							}
							int num14 = Math.Min(val4, num11);
							int num15 = Math.Max(val4, num11);
							if (num14 != num15)
							{
								graphics.DrawLine(pen, num7 + sizeF.Width * (float)(3 * num14), 0f, num7 + sizeF.Width * (float)(3 * num15) - 1f, 0f);
							}
							int num16 = Math.Min(val, num10);
							int num17 = Math.Max(val, num10);
							if (num16 != num17)
							{
								graphics.DrawLine(pen, num7 + sizeF.Width * (float)(3 * num16), (float)(this.pic.Height - 1), num7 + sizeF.Width * (float)(3 * num17) - 1f, (float)(this.pic.Height - 1));
							}
							int num18 = Math.Min(val2, num11);
							int num19 = Math.Max(val2, num11);
							if (num18 != num19)
							{
								graphics.DrawLine(pen, num7 + sizeF.Width * (float)(3 * num18), (float)(this.pic.Height - 1), num7 + sizeF.Width * (float)(3 * num19) - 1f, (float)(this.pic.Height - 1));
							}
							graphics.DrawLine(pen, rect.X, 0f, rect.X, (float)this.pic.Height);
							graphics.DrawLine(pen, rect.Right - 1f, 0f, rect.Right - 1f, (float)this.pic.Height);
						}
					}
					graphics.DrawString(text, this.Font, brush, 0f, 0f);
					e.Graphics.DrawImageUnscaled(this.pic, 0, num5);
					num5 += this.pic.Height;
					num6++;
					num4 += this.byteWidth;
				}
				if (this.antiFlick)
				{
					e.Graphics.FillRectangle(new SolidBrush(this.BackColor), Rectangle.FromLTRB(0, num5, this.pic.Width, base.ClientSize.Height));
				}
			}
		}
		public void SetPos(int pos)
		{
			this.curoff = Math.Max(0, pos);
			base.Invalidate();
		}
		public int GetPos()
		{
			return this.curoff;
		}
		public int CalcPgSize()
		{
			switch (this.pgScrollType)
			{
			case PgScrollType.Absolute:
				return this.unitPg;
			case PgScrollType.ScreenSizeBased:
				return this.byteWidth * this.GetLineCnt();
			default:
				throw new NotSupportedException();
			}
		}
		private void HexVwer_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
			case Keys.Prior:
				this.SetPos(this.curoff - this.CalcPgSize());
				e.Handled = true;
				return;
			case Keys.Next:
				this.SetPos(this.curoff + this.CalcPgSize());
				e.Handled = true;
				break;
			case Keys.End:
				break;
			case Keys.Home:
				this.SetPos(0);
				e.Handled = true;
				return;
			case Keys.Left:
				this.SetPos(this.curoff + 1);
				e.Handled = true;
				return;
			case Keys.Up:
				this.SetPos(this.curoff - this.byteWidth);
				e.Handled = true;
				return;
			case Keys.Right:
				this.SetPos(this.curoff - 1);
				e.Handled = true;
				return;
			case Keys.Down:
				this.SetPos(this.curoff + this.byteWidth);
				e.Handled = true;
				return;
			default:
				return;
			}
		}
		protected override bool ProcessDialogKey(Keys keyData)
		{
			switch (keyData)
			{
			case Keys.Prior:
			case Keys.Next:
			case Keys.Home:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
			{
				KeyEventArgs keyEventArgs = new KeyEventArgs(keyData);
				this.HexVwer_KeyDown(this, keyEventArgs);
				return keyEventArgs.Handled;
			}
			}
			return base.ProcessDialogKey(keyData);
		}
		private void HexVwer_Load(object sender, EventArgs e)
		{
			base.SetStyle(ControlStyles.ResizeRedraw, true);
		}
		public void AddMark(int addr)
		{
			this.marks[addr] = null;
			base.Invalidate();
		}
		public void SetSel(int sel)
		{
			this.cursel = sel;
			base.Invalidate();
		}
		protected override void WndProc(ref Message m)
		{
			if (!base.DesignMode && this.si != null && this.antiFlick && m.Msg == 20)
			{
				using (Graphics graphics = Graphics.FromHdc(m.WParam))
				{
					GraphicsState gstate = graphics.Save();
					graphics.ExcludeClip(Rectangle.FromLTRB(0, 0, this.pic.Width, base.ClientSize.Height));
					graphics.FillRectangle(new SolidBrush(this.BackColor), base.ClientRectangle);
					graphics.Restore(gstate);
					m.Result = new IntPtr(1);
					return;
				}
			}
			base.WndProc(ref m);
		}
		public void AddRangeMarked(int off, int len, Color color, Color clrborder)
		{
			this.alrm.Add(new RangeMarked(off, len, color, clrborder));
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
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 12f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.Font = new Font("ＭＳ ゴシック", 9f, FontStyle.Regular, GraphicsUnit.Point, 128);
			base.Name = "HexVwer";
			base.Load += new EventHandler(this.HexVwer_Load);
			base.Paint += new PaintEventHandler(this.HexVwer_Paint);
			base.KeyDown += new KeyEventHandler(this.HexVwer_KeyDown);
			base.ResumeLayout(false);
		}
	}
}
