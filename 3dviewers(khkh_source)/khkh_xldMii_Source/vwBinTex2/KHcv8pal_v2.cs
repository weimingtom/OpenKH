using System;
namespace vwBinTex2
{
	internal class KHcv8pal_v2
	{
		private static readonly byte[] alt = new byte[]
		{
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			24,
			25,
			26,
			27,
			28,
			29,
			30,
			31
		};
		public static void repl(byte[] bSrc, int offSrc, byte[] bDst, int offDst)
		{
			for (int i = 0; i < 256; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					bDst[offDst + 4 * i + j] = bSrc[offSrc + 4 * ((int)KHcv8pal_v2.alt[i & 31] + (i & -32)) + j];
				}
			}
		}
	}
}
