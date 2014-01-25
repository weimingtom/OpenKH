namespace BAR_Editor
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            if (args.Length != 0)
            {
                Bar bar = null;
                for (var i = 0; i < args.Length; i++)
                {
                    int num2;
                    var str = args[i];
                    if (str != null)
                    {
                        if (!(str == "-newbar"))
                        {
                            if (str == "-openbar")
                            {
                                goto Label_0089;
                            }
                            if (str == "-savebar")
                            {
                                goto Label_00B8;
                            }
                            if (str == "-addfile")
                            {
                                goto Label_00E2;
                            }
                            if (str == "-replacefile")
                            {
                                goto Label_014B;
                            }
                            if (str == "-deletefile")
                            {
                                goto Label_01A9;
                            }
                        }
                        else
                        {
                            bar = new Bar();
                        }
                    }
                    continue;
                Label_0089:
                    if ((i + 1) >= args.Length)
                    {
                        return -2;
                    }
                    try
                    {
                        bar = new Bar(File.Open(args[++i], FileMode.Open, FileAccess.Read, FileShare.Read));
                        continue;
                    }
                    catch (FileNotFoundException)
                    {
                        return -10;
                    }
                Label_00B8:
                    if (bar == null)
                    {
                        return -1;
                    }
                    if ((i + 1) >= args.Length)
                    {
                        return -2;
                    }
                    bar.Save(File.Open(args[++i], FileMode.Create, FileAccess.Write, FileShare.None));
                    continue;
                Label_00E2:
                    if (bar == null)
                    {
                        return -1;
                    }
                    if ((i + 3) >= args.Length)
                    {
                        return -2;
                    }
                    var item = new Bar.BarFile();
                    if (!uint.TryParse(args[++i], out item.Type))
                    {
                        return -3;
                    }
                    item.Id = args[++i];
                    try
                    {
                        item.Data = File.ReadAllBytes(args[++i]);
                    }
                    catch (FileNotFoundException)
                    {
                        return -10;
                    }
                    bar.FileList.Add(item);
                    continue;
                Label_014B:
                    if (bar == null)
                    {
                        return -1;
                    }
                    if ((i + 2) >= args.Length)
                    {
                        return -2;
                    }
                    if (!int.TryParse(args[++i], out num2) || (num2 < 0))
                    {
                        return -3;
                    }
                    if (num2 >= bar.FileList.Count)
                    {
                        return -4;
                    }
                    try
                    {
                        bar.FileList[num2].Data = File.ReadAllBytes(args[++i]);
                        continue;
                    }
                    catch (FileNotFoundException)
                    {
                        return -10;
                    }
                Label_01A9:
                    if (bar == null)
                    {
                        return -1;
                    }
                    if ((i + 1) >= args.Length)
                    {
                        return -2;
                    }
                    if (!int.TryParse(args[++i], out num2) || (num2 < 0))
                    {
                        return -3;
                    }
                    if (num2 >= bar.FileList.Count)
                    {
                        return -4;
                    }
                    bar.FileList.RemoveAt(num2);
                }
                return 0;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
            return 0;
        }
    }
}

