using System;
using System.Collections.Generic;
using System.Drawing;
namespace khiiMapv
{
	public class MTex
	{
		public Bitmap[] pics;
		public int[] alIndirIndex = new int[0];
		public MTex(List<Bitmap> pics)
		{
			this.pics = pics.ToArray();
		}
	}
}
