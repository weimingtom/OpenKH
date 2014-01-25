namespace khkh_xldMii
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormII());
            }
            catch (Exception exception)
            {
                MessageBox.Show("Sorry.\n\nThis viewer stopped due to program error.\n\nHere is a bit information of error.\n\nPress Ok to halt.\n---\n" + exception, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                throw;
            }
        }

        public static string fBPxml
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BindPreset.xml");
            }
        }

        public static string fSPtext
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SearchFolders.txt");
            }
        }
    }
}

