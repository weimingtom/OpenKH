namespace khiiMapv.Pax
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    public class St3r
    {
        [DebuggerDisplay("")]
        public List<STXYC> alv;
        public int v0;
        public int v2;
        public int v4;

        public St3r()
        {
            this.alv = new List<STXYC>();
            base..ctor();
            return;
        }

        public override string ToString()
        {
            return string.Format("{0:x4},{1:x4},{2:x4}", (int) this.v0, (int) this.v2, (int) this.v4);
        }

        public STXYC[] _alv
        {
            get
            {
                return this.alv.ToArray();
            }
        }
    }
}

