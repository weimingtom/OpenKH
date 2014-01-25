namespace khiiMapv.Pax
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), DebuggerDisplay("({x},{y}),({s},{t}),{clr}")]
    public struct STXYC
    {
        public int x;
        public int y;
        public int s;
        public int t;
        public Color clr;
        public STXYC(int x, int y, int s, int t, Color clr)
        {
            this.x = x;
            this.y = y;
            this.s = s;
            this.t = t;
            this.clr = clr;
            return;
        }

        public override string ToString()
        {
            object[] objArray;
            return string.Format("{0},{1},{2},[3},{4}", new object[] { (int) this.x, (int) this.y, (int) this.s, (int) this.t, (Color) this.clr });
        }
    }
}

