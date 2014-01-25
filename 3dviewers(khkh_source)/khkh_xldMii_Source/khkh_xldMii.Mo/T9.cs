namespace khkh_xldMii.Mo
{
    using System;

    public class T9
    {
        public int c00;
        public int c02;
        public int c04;
        public int c06;

        public T9(int c00, int c02, int c04, int c06)
        {
            this.c00 = c00;
            this.c02 = c02;
            this.c04 = c04;
            this.c06 = c06;
        }

        public override string ToString()
        {
            return string.Format("{0:X4} {1:X4} {2:X4} {3:X4}", new object[] { this.c00, this.c02, this.c04, this.c06 });
        }
    }
}

