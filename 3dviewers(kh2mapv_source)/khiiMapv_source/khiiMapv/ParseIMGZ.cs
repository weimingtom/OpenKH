namespace khiiMapv
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ParseIMGZ
    {
        public ParseIMGZ()
        {
            base..ctor();
            return;
        }

        public static PicIMGD[] TakeIMGZ(byte[] bin)
        {
            MemoryStream stream;
            BinaryReader reader;
            List<PicIMGD> list;
            int num;
            int num2;
            int num3;
            int num4;
            MemoryStream stream2;
            PicIMGD cimgd;
            stream = new MemoryStream(bin, 0);
            reader = new BinaryReader(stream);
            list = new List<PicIMGD>();
            stream.Position = 12L;
            num = reader.ReadInt32();
            num2 = 0;
            goto Label_005E;
        Label_002A:
            num3 = reader.ReadInt32();
            num4 = reader.ReadInt32();
            stream2 = new MemoryStream(bin, num3, num4, 0);
            cimgd = ParseIMGD.TakeIMGD(stream2);
            list.Add(cimgd);
            num2 += 1;
        Label_005E:
            if (num2 < num)
            {
                goto Label_002A;
            }
            return list.ToArray();
        }
    }
}

