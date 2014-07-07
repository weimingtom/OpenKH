using System.Collections.Generic;
using System.Diagnostics;

namespace khiiMapv.Pax
{
    public class St3r
    {
        [DebuggerDisplay("")] public List<STXYC> alv = new List<STXYC>();
        public int v0;
        public int v2;
        public int v4;

        public STXYC[] _alv
        {
            get { return alv.ToArray(); }
        }

        public override string ToString()
        {
            return string.Format("{0:x4},{1:x4},{2:x4}", v0, v2, v4);
        }
    }
}