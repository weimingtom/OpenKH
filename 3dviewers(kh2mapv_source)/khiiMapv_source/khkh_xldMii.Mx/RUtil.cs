using System;
namespace khkh_xldMii.Mx
{
	internal class RUtil
	{
		public static int RoundUpto16(int val)
		{
			return val + 15 & -16;
		}
	}
}
