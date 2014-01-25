namespace khkh_xldMii
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class ReadBar
    {
        public static Barent[] Explode(Stream si)
        {
            BinaryReader reader = new BinaryReader(si);
            if (((reader.ReadByte() != 0x42) || (reader.ReadByte() != 0x41)) || ((reader.ReadByte() != 0x52) || (reader.ReadByte() != 1)))
            {
                throw new NotSupportedException();
            }
            int num = reader.ReadInt32();
            reader.ReadBytes(8);
            List<Barent> list = new List<Barent>();
            for (int i = 0; i < num; i++)
            {
                Barent item = new Barent {
                    k = reader.ReadInt32(),
                    id = Encoding.ASCII.GetString(reader.ReadBytes(4)).TrimEnd(new char[1]),
                    off = reader.ReadInt32(),
                    len = reader.ReadInt32()
                };
                list.Add(item);
            }
            for (int j = 0; j < num; j++)
            {
                Barent barent2 = list[j];
                si.Position = barent2.off;
                barent2.bin = reader.ReadBytes(barent2.len);
            }
            return list.ToArray();
        }

        public static Barent[] Explode2(MemoryStream si)
        {
            BinaryReader reader = new BinaryReader(si);
            if (((reader.ReadByte() != 0x42) || (reader.ReadByte() != 0x41)) || ((reader.ReadByte() != 0x52) || (reader.ReadByte() != 1)))
            {
                throw new NotSupportedException();
            }
            int num = reader.ReadInt32();
            reader.ReadBytes(8);
            List<Barent> list = new List<Barent>();
            for (int i = 0; i < num; i++)
            {
                Barent item = new Barent {
                    k = reader.ReadInt32(),
                    id = Encoding.ASCII.GetString(reader.ReadBytes(4)).TrimEnd(new char[1]),
                    off = reader.ReadInt32(),
                    len = reader.ReadInt32()
                };
                if (((item.off + item.len) & 15) != 0)
                {
                    item.len += 0x10 - ((item.off + item.len) & 15);
                }
                list.Add(item);
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

