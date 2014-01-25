namespace khkh_xldMii.Mx
{
    using System;

    internal class RUtil
    {
        public static int RoundUpto16(int val)
        {
            return ((val + 15) & -16);
        }
    }
}

