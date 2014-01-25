namespace khkh_xldM
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class ReadBar
    {
        public ReadBar()
        {
            base..ctor();
            return;
        }

        public static Barent[] Explode(Stream si)
        {
            BinaryReader reader;
            int num;
            List<Barent> list;
            int num2;
            Barent barent;
            int num3;
            Barent barent2;
            char[] chArray;
            reader = new BinaryReader(si);
            if (reader.ReadByte() != 0x42)
            {
                goto Label_002E;
            }
            if (reader.ReadByte() != 0x41)
            {
                goto Label_002E;
            }
            if (reader.ReadByte() != 0x52)
            {
                goto Label_002E;
            }
            if (reader.ReadByte() == 1)
            {
                goto Label_0034;
            }
        Label_002E:
            throw new NotSupportedException();
        Label_0034:
            num = reader.ReadInt32();
            reader.ReadBytes(8);
            list = new List<Barent>();
            num2 = 0;
            goto Label_00AE;
        Label_004D:
            barent = new Barent();
            barent.k = reader.ReadInt32();
            barent.id = Encoding.ASCII.GetString(reader.ReadBytes(4)).TrimEnd(new char[1]);
            barent.off = reader.ReadInt32();
            barent.len = reader.ReadInt32();
            list.Add(barent);
            num2 += 1;
        Label_00AE:
            if (num2 < num)
            {
                goto Label_004D;
            }
            num3 = 0;
            goto Label_00E9;
        Label_00B7:
            barent2 = list[num3];
            si.Position = (long) barent2.off;
            barent2.bin = reader.ReadBytes(barent2.len);
            num3 += 1;
        Label_00E9:
            if (num3 < num)
            {
                goto Label_00B7;
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

            public Barent()
            {
                base..ctor();
                return;
            }
        }
    }
}

