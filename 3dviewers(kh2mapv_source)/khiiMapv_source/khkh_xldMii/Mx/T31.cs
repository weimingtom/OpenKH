namespace khkh_xldMii.Mx
{
    using System;
    using System.Collections.Generic;

    public class T31
    {
        public List<T11> al11;
        public List<T12> al12;
        public List<T13vif> al13;
        public int len;
        public int off;
        public int postbl3;
        public T21 t21;
        public T32 t32;

        public T31(int off, int len, int postbl3)
        {
            this.al11 = new List<T11>();
            this.al12 = new List<T12>();
            this.al13 = new List<T13vif>();
            base..ctor();
            this.off = off;
            this.len = len;
            this.postbl3 = postbl3;
            return;
        }
    }
}

