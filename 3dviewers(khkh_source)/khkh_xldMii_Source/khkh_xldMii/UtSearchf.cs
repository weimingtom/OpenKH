using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace khkh_xldMii
{
    internal class UtSearchf
    {
        public static string Find(string fn)
        {
            if (File.Exists(Program.fSPtext))
            {
                string[] array = File.ReadAllLines(Program.fSPtext, Encoding.UTF8);
                for (int i = 0; i < array.Length; i++)
                {
                    string path = array[i];
                    try
                    {
                        string text = Path.Combine(path, fn);
                        if (File.Exists(text))
                        {
                            return text;
                        }
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }
            return null;
        }

        public static void AddDirToList(string dir)
        {
            var list = new List<string>();
            if (File.Exists(Program.fSPtext) && (File.GetAttributes(Program.fSPtext) & FileAttributes.ReadOnly) != 0)
            {
                return;
            }
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
}