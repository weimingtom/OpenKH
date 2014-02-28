using System;
namespace vwBinTex2
{
	internal class KHcv8pal_swap34
	{
		public static int repl(int x)
		{
			return (x & 231) | (((x & 16) != 0) ? 8 : 0) | (((x & 8) != 0) ? 16 : 0);
		}
	}
}
