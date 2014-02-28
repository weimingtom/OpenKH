using System;
namespace vwBinTex2
{
	internal class KHcv4pal
	{
		private static readonly sbyte[] tbl = new sbyte[]
		{
			0,
			1,
			4,
			5,
			8,
			9,
			12,
			13,
			2,
			3,
			6,
			7,
			10,
			11,
			14,
			15
		};
		public static int repl(int t)
		{
			return (int)KHcv4pal.tbl[t];
		}
	}
}
