namespace khkh_xldMii.Mo
{
    using System;

    public class T1
    {
        public int c00;
        public int c02;
        public float c04;

        public T1(int c00, int c02, float c04)
        {
            this.c00 = c00;
            this.c02 = c02;
            this.c04 = c04;
        }

        public override string ToString()
        {
            return string.Format("{0:X4} {1:X4} {2}", this.c00, this.c02, this.c04);
        }
    }
}
