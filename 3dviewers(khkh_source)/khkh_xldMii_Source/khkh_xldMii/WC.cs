using System;
using System.Windows.Forms;
namespace khkh_xldMii
{
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
