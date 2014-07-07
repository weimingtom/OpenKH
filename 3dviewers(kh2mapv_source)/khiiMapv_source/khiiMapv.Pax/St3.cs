using System.Collections.Generic;
using System.Diagnostics;

namespace khiiMapv.Pax
{
    public class St3
    {
        [DebuggerDisplay("")] public List<St3r> al3r = new List<St3r>();
        public int cnt1;
        public int cnt2;

        public St3r[] _al3r
        {
            get { return al3r.ToArray(); }
        }

        public override string ToString()
        {
            return string.Format("{0},{1}", cnt1, cnt2);
        }
    }
}