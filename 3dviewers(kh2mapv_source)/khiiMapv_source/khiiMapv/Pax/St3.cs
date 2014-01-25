namespace khiiMapv.Pax
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class St3
    {
        [DebuggerDisplay("")]
        public List<St3r> al3r;
        public int cnt1;
        public int cnt2;

        public St3()
        {
            this.al3r = new List<St3r>();
            base..ctor();
            return;
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", (int) this.cnt1, (int) this.cnt2);
        }

        public St3r[] _al3r
        {
            get
            {
                return this.al3r.ToArray();
            }
        }
    }
}

