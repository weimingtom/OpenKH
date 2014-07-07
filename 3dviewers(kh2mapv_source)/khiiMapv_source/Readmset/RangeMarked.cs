using System.Drawing;

namespace Readmset
{
    public class RangeMarked
    {
        public Color clr;
        public Color clrborder;
        public int len;
        public int off;

        public RangeMarked(int off, int len, Color clr, Color clrborder)
        {
            this.off = off;
            this.len = len;
            this.clr = clr;
            this.clrborder = clrborder;
        }
    }
}