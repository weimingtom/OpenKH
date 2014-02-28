using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
namespace khiiMapv.Pax
{
	public class R
	{
		public List<Bitmap> pics = new List<Bitmap>();
		[DebuggerDisplay("")]
		public List<St3> als3 = new List<St3>();
		public St3[] _als3
		{
			get
			{
				return this.als3.ToArray();
			}
		}
	}
}
