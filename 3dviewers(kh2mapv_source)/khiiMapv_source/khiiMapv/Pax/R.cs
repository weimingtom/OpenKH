namespace khiiMapv.Pax
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class R
    {
        [DebuggerDisplay("")]
        public List<St3> als3;
        public List<Bitmap> pics;

        public R()
        {
            this.pics = new List<Bitmap>();
            this.als3 = new List<St3>();
            base..ctor();
            return;
        }

        public St3[] _als3
        {
            get
            {
                return this.als3.ToArray();
            }
        }
    }
}

