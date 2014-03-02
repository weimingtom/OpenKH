using System;
using System.Windows.Forms;

namespace BAR_Editor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length != 0)
            {
                // Return codes:
                // * 0 Success
                // * -1 No BAR file has been opened\made (aka bar is null)
                // * -2 Not enough arguments
                // * -3 Failed to parse argument
                // * -4 Index is out of range (too high)
                // * -10 File not found
                // * -1313 Unknown error
                try
                {
                    BAR bar = null;
                    for (int i = 0; i < args.Length; i++)
                    {
                        int index, i2;
                        string s;
                        BAR.BARFile bf;
                        switch (args[i])
                        {
                            case "-newbar": bar = new BAR(); break;
                            case "-openbar":
                                if (i + 1 >= args.Length) { return -2; }
                                try { bar = new BAR(System.IO.File.Open(args[++i], System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)); }
                                catch (System.IO.FileNotFoundException) { return -10; }
                                break;
                            case "-savebar":
                                if (bar == null) { return -1; }
                                if (i + 1 >= args.Length) { return -2; }
                                bar.save(System.IO.File.Open(args[++i], System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None));
                                break;
                            case "-addfile":
                                if (bar == null) { return -1; }
                                if (i + 3 >= args.Length) { return -2; }
                                bf = new BAR.BARFile();
                                if (!uint.TryParse(args[++i], out bf.type)) { return -3; }
                                bf.id = args[++i];
                                try { bf.data = System.IO.File.ReadAllBytes(args[++i]); }
                                catch (System.IO.FileNotFoundException) { return -10; }
                                bar.fileList.Add(bf);
                                bf = null;
                                break;
                            case "-replacefile":
                                if (bar == null) { return -1; }
                                if (i + 2 >= args.Length) { return -2; }
                                if (!int.TryParse(args[++i], out index) || index < 0) { return -3; }
                                if (index >= bar.fileList.Count) { return -4; }
                                try { bar.fileList[index].data = System.IO.File.ReadAllBytes(args[++i]); }
                                catch (System.IO.FileNotFoundException) { return -10; }
                                break;
                            case "-deletefile":
                                if (bar == null) { return -1; }
                                if (i + 1 >= args.Length) { return -2; }
                                if (!int.TryParse(args[++i], out index) || index < 0) { return -3; }
                                if (index >= bar.fileList.Count) { return -4; }
                                bar.fileList.RemoveAt(index);
                                break;
                            case "-extractfile":
                                if (bar == null) { return -1; }
                                if (i + 2 >= args.Length) { return -2; }
                                if (!int.TryParse(args[++i], out index) || index < 0) { return -3; }
                                if (index >= bar.fileList.Count) { return -4; }
                                System.IO.File.WriteAllBytes(args[++i], bar.fileList[index].data);
                                break;
                            case "-extractmatch":
                                if (bar == null) { return -1; }
                                if (i + 2 >= args.Length) { return -2; }
                                if (!int.TryParse(args[++i], out index)) { return -3; } else if (index < -1) { index = -1; }
                                s = args[++i].Trim();
                                if (s.Length > 4) { s = s.Substring(0, 4); }
                                i2 = 0;
                                foreach (BAR.BARFile bfl in bar.fileList)
                                {
                                    if ((index == -1 || bfl.type == index) && (s.Length == 0 || bfl.id.Equals(s, StringComparison.InvariantCultureIgnoreCase)))
                                    {
                                        System.IO.File.WriteAllBytes(string.Format("{0}. {1} [0x{2:X2}].bin", ++i2, bfl.id, bfl.type), bfl.data);
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception) { return -1313; }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
            return 0;
        }
    }
}
