using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace khiiMapv.Pax
{
	public class St3r
	{
		public int v0;
		public int v2;
		public int v4;
		[DebuggerDisplay("")]
		public List<STXYC> alv = new List<STXYC>();
		public STXYC[] _alv
		{
			get
			{
				return this.alv.ToArray();
			}
		}
		public override string ToString()
		{
			return string.Format("{0:x4},{1:x4},{2:x4}", this.v0, this.v2, this.v4);
		}
	}
}
