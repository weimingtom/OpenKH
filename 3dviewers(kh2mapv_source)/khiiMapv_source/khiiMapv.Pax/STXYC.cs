using System.Diagnostics;
using System.Drawing;

namespace khiiMapv.Pax
{
    [DebuggerDisplay("({x},{y}),({s},{t}),{clr}")]
    public struct STXYC
    {
        public Color clr;
        public int s;
        public int t;
        public int x;
        public int y;

        public STXYC(int x, int y, int s, int t, Color clr)
        {
            this.x = x;
            this.y = y;
            this.s = s;
            this.t = t;
            this.clr = clr;
        }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},[3},{4}", new object[]
            {
                x,
                y,
                s,
                t,
                clr
            });
        }
    }
}