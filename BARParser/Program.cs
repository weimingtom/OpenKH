using System;
using System.Collections.Generic;
using System.IO;

namespace BARParser
{
    class Program
    {
        static void Main(string[] args)
        {
            Kh.IDX idx = new Kh.IDX(
                new FileStream(@"D:\Hacking\OpenKH\KH2.IDX", FileMode.Open, FileAccess.Read),
                new FileStream(@"D:\Hacking\OpenKH\KH2.IMG", FileMode.Open, FileAccess.Read));

            Stream file = idx.OpenFile(@"msg/jp/sys.bar");
            Console.WriteLine("File size: " + file.Length.ToString());

            Kh.Bar msgSys = new Kh.Bar(file);
            Console.WriteLine("BAR files: " + msgSys.Count);

            Console.ReadLine();
        }
    }
}
