namespace khkh_xldMii
{
    using System;
    using System.Windows.Forms;

    public class WC : IDisposable
    {
        public WC()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        public void Dispose()
        {
            Cursor.Current = Cursors.Default;
        }
    }
}

