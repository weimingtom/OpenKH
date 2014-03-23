using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace khkh_xldMii
{
    public class ReadBar
    {
        public static Barent[] Explode(Stream si)
        {
            var binaryReader = new BinaryReader(si);
            if (binaryReader.ReadByte() != 66 || binaryReader.ReadByte() != 65 || binaryReader.ReadByte() != 82 ||
                binaryReader.ReadByte() != 1)
            {
                throw new NotSupportedException();
            }
            int num = binaryReader.ReadInt32();
            binaryReader.ReadBytes(8);
            var list = new List<Barent>();
            for (int i = 0; i < num; i++)
            {
                var barent = new Barent();
                barent.k = binaryReader.ReadInt32();
                Barent arg_83_0 = barent;
                string arg_7E_0 = Encoding.ASCII.GetString(binaryReader.ReadBytes(4));
                var trimChars = new char[1];
                arg_83_0.id = arg_7E_0.TrimEnd(trimChars);
                barent.off = binaryReader.ReadInt32();
                barent.len = binaryReader.ReadInt32();
                list.Add(barent);
            }
            for (int j = 0; j < num; j++)
            {
                Barent barent2 = list[j];
                si.Position = barent2.off;
                barent2.bin = binaryReader.ReadBytes(barent2.len);
            }
            return list.ToArray();
        }

        public static Barent[] Explode2(MemoryStream si)
        {
            var binaryReader = new BinaryReader(si);
            if (binaryReader.ReadByte() != 66 || binaryReader.ReadByte() != 65 || binaryReader.ReadByte() != 82 ||
                binaryReader.ReadByte() != 1)
            {
                throw new NotSupportedException();
            }
            int num = binaryReader.ReadInt32();
            binaryReader.ReadBytes(8);
            var list = new List<Barent>();
            for (int i = 0; i < num; i++)
            {
                var barent = new Barent();
                barent.k = binaryReader.ReadInt32();
                Barent arg_86_0 = barent;
                string arg_81_0 = Encoding.ASCII.GetString(binaryReader.ReadBytes(4));
                var trimChars = new char[1];
                arg_86_0.id = arg_81_0.TrimEnd(trimChars);
                barent.off = binaryReader.ReadInt32();
                barent.len = binaryReader.ReadInt32();
                if ((barent.off + barent.len & 15) != 0)
                {
                    barent.len += 16 - (barent.off + barent.len & 15);
                }
                list.Add(barent);
            }
            for (int j = 0; j < num; j++)
            {
                Barent barent2 = list[j];
                si.Position = barent2.off;
                barent2.bin = new byte[barent2.len];
                si.Read(barent2.bin, 0, barent2.len);
            }
            return list.ToArray();
        }

        public class Barent
        {
            public byte[] bin;
            public string id;
            public int k;
            public int len;
            public int off;
        }
    }
}