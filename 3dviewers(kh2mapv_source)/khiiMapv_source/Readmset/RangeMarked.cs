namespace Readmset
{
    using System;
    using System.Drawing;

    public class RangeMarked
    {
        public Color clr;
        public Color clrborder;
        public int len;
        public int off;

        public RangeMarked(int off, int len, Color clr, Color clrborder)
        {
            base..ctor();
            this.off = off;
            this.len = len;
            this.clr = clr;
            this.clrborder = clrborder;
            return;
        }
    }
}

