namespace vconv122
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class TIMc
    {
        public static Texex2[] Load(Stream fs)
        {
            int item = Convert.ToInt32(fs.Position);
            BinaryReader reader = new BinaryReader(fs);
            List<int> list = new List<int>();
            if (reader.ReadUInt32() == uint.MaxValue)
            {
                int num2 = reader.ReadInt32();
                for (int j = 0; j < num2; j++)
                {
                    list.Add(item + reader.ReadInt32());
                }
            }
            else
            {
                list.Add(item);
            }
            List<Texex2> list2 = new List<Texex2>();
            for (int i = 0; i < list.Count; i++)
            {
                int num5 = list[i];
                int num6 = ((i + 1) < list.Count) ? list[i + 1] : Convert.ToInt32(fs.Length);
                byte[] buffer = new byte[num6 - num5];
                fs.Position = num5;
                fs.Read(buffer, 0, num6 - num5);
                list2.Add(TIMf.Load(new MemoryStream(buffer, false)));
            }
            return list2.ToArray();
        }
    }
}

