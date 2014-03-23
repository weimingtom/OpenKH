using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace BARParser
{
    class Program
    {
        
        static void Main(string[] args)
        {

            Kh.IDX2 idx = new Kh.IDX2(
                new FileStream(@"D:\Hacking\OpenKH\KH2.IDX", FileMode.Open, FileAccess.Read),
                new FileStream(@"D:\Hacking\OpenKH\KH2.IMG", FileMode.Open, FileAccess.Read));

            Kh.Msg msg = new Kh.Msg();

            Stream file = idx.OpenFile(@"msg/jp/sys.bar");
            Console.WriteLine("File size: " + file.Length.ToString());
#if TEST
            msg.Add(file);
            Random rnd = new Random();
            int int5 = 32768;
            Stream test = msg.Get(int);
            Int32 length = unchecked((int)test.Length);
            Byte[] buffer = new Byte[length];
            test.Read(buffer, 0, length);
            byte[] a = buffer;
            Console.Write("Random string: {0}", a);
            Console.Read();
#endif
            Kh.Bar msgSys = new Kh.Bar(file);
            Console.WriteLine("BAR files: " + msgSys.Count);
            for (int i = 0; i < msgSys.Count; i++)
            {
                Console.WriteLine(
                    "[" + i.ToString() + "] " + 
                    msgSys.GetName(i) + " Type " + msgSys.GetType(i).ToString());

                FileStream fDump = new FileStream(msgSys.GetName(i), FileMode.Create);
                Stream stream = msgSys.GetData(i);
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                fDump.Write(data, 0, data.Length);
                fDump.Close();
                stream.Position = 0;

                if (msgSys.GetType(i) == Kh.Bar.Type.MSG)
                {
                    msg.Add(stream);
                }
            }

            Console.ReadLine();

        }
    }
}
