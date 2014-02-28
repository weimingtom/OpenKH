using System;
namespace vconv122
{
	public class Patc
	{
		public byte[] bits;
		public int px;
		public int py;
		public int pcx;
		public int pcy;
		public int ycnt;
		public int texi;
		public Texfac[] altf = new Texfac[0];
		public Patc(byte[] bits, int px, int py, int pcx, int pcy, int ycnt, int texi)
		{
			this.bits = bits;
			this.px = px;
			this.py = py;
			this.pcx = pcx;
			this.pcy = pcy;
			this.ycnt = ycnt;
			this.texi = texi;
		}
	}
}
