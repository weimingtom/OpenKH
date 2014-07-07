using System;
using System.IO;
using System.Windows.Forms;

namespace khiiMapv
{
    internal static class Program
    {
        [STAThread]
        private static void Main(string[] ala)
        {
            string fpread = null;
            for (int i = 0; i < ala.Length; i++)
            {
                string text = ala[i];
                if (File.Exists(text))
                {
                    fpread = text;
                    break;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RDForm(fpread));
        }
    }
}