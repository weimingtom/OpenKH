namespace khiiMapv
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static void Main(string[] ala)
        {
            string str;
            string str2;
            string[] strArray;
            int num;
            str = null;
            strArray = ala;
            num = 0;
            goto Label_001C;
        Label_0008:
            str2 = strArray[num];
            if (File.Exists(str2) == null)
            {
                goto Label_0018;
            }
            str = str2;
            goto Label_0022;
        Label_0018:
            num += 1;
        Label_001C:
            if (num < ((int) strArray.Length))
            {
                goto Label_0008;
            }
        Label_0022:
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(0);
            Application.Run(new RDForm(str));
            return;
        }
    }
}

