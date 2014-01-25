namespace khkh_xldMii
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    internal class UtSearchf
    {
        public static void AddDirToList(string dir)
        {
            List<string> list = new List<string>();
            if (!File.Exists(Program.fSPtext) || ((File.GetAttributes(Program.fSPtext) & FileAttributes.ReadOnly) == 0))
            {
                if (File.Exists(Program.fSPtext))
                {
                    list.AddRange(File.ReadAllLines(Program.fSPtext, Encoding.UTF8));
                }
                if (list.IndexOf(dir) < 0)
                {
                    list.Add(dir);
                    File.WriteAllLines(Program.fSPtext, list.ToArray(), Encoding.UTF8);
                }
            }
        }

        public static string Find(string fn)
        {
            if (File.Exists(Program.fSPtext))
            {
                foreach (string str in File.ReadAllLines(Program.fSPtext, Encoding.UTF8))
                {
                    try
                    {
                        string path = Path.Combine(str, fn);
                        if (File.Exists(path))
                        {
                            return path;
                        }
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }
            return null;
        }
    }
}

