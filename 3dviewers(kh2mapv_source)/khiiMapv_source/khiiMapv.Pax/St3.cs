using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace khiiMapv.Pax
{
	public class St3
	{
		public int cnt1;
		public int cnt2;
		[DebuggerDisplay("")]
		public List<St3r> al3r = new List<St3r>();
		public St3r[] _al3r
		{
			get
			{
				return this.al3r.ToArray();
			}
		}
		public override string ToString()
		{
			return string.Format("{0},{1}", this.cnt1, this.cnt2);
		}
	}
}
