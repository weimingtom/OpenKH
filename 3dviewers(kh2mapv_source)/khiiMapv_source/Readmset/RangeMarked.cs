using System;
using System.Drawing;
namespace Readmset
{
	public class RangeMarked
	{
		public int off;
		public int len;
		public Color clr;
		public Color clrborder;
		public RangeMarked(int off, int len, Color clr, Color clrborder)
		{
			this.off = off;
			this.len = len;
			this.clr = clr;
			this.clrborder = clrborder;
		}
	}
}
