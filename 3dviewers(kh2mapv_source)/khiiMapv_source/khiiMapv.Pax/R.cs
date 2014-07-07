using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace khiiMapv.Pax
{
    public class R
    {
        [DebuggerDisplay("")] public List<St3> als3 = new List<St3>();
        public List<Bitmap> pics = new List<Bitmap>();

        public St3[] _als3
        {
            get { return als3.ToArray(); }
        }
    }
}