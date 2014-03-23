using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LIBKH;

namespace BARParser
{
    class Program
    {
        
        static void Main(string[] args)
        {

            LIBKH.KH2.IDX idx = new LIBKH.KH2.IDX(
                new FileStream(@"D:\Hacking\OpenKH\KH2.IDX", FileMode.Open, FileAccess.Read),
                new FileStream(@"D:\Hacking\OpenKH\KH2.IMG", FileMode.Open, FileAccess.Read));

            LIBKH.KH2.MSG msg = new LIBKH.KH2.MSG();

            Stream file = idx.OpenFile(@"msg/jp/sys.bar");
            Console.WriteLine("File size: " + file.Length.ToString());
#if TEST
            msg.Add(file);
            Random rnd = new Random();
            int int5 = 32768;
            Stream test = msg.Get(int5);
            Int32 length = unchecked((int)test.Length);
            Byte[] buffer = new Byte[length];
            test.Read(buffer, 0, length);
            byte[] a = buffer;
            Console.Write("Random string: {0}", a);
            Console.Read();
#endif
            LIBKH.KH2.BAR msgSys = new LIBKH.KH2.BAR(file);
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

                if (msgSys.GetType(i) == LIBKH.KH2.BAR.Type.MSG)
                {
                    msg.Add(stream);
                }
            }

            Console.ReadLine();

        }
    }
}
