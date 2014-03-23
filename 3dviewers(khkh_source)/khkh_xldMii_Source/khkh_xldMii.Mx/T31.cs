using System.Collections.Generic;

namespace khkh_xldMii.Mx
{
    public class T31
    {
        public List<T11> al11 = new List<T11>();
        public List<T12> al12 = new List<T12>();
        public List<T13vif> al13 = new List<T13vif>();
        public int len;
        public int off;
        public int postbl3;
        public T21 t21;
        public T32 t32;

        public T31(int off, int len, int postbl3)
        {
            this.off = off;
            this.len = len;
            this.postbl3 = postbl3;
        }
    }
}