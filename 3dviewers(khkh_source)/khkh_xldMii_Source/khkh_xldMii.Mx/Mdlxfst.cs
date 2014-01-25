namespace khkh_xldMii.Mx
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Mdlxfst
    {
        public List<T31> alt31 = new List<T31>();

        public Mdlxfst(Stream fs)
        {
            BinaryReader br = new BinaryReader(fs);
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(0x90);
            int num = 0;
            while (queue.Count != 0)
            {
                T31 t;
                int off = queue.Dequeue();
                fs.Position = off + 0x10;
                int num3 = br.ReadUInt16();
                fs.Position = off + 0x1c;
                int num4 = br.ReadUInt16();
                this.alt31.Add(t = new T31(off, 0x20 * (1 + num4), num));
                num++;
                for (int i = 0; i < num4; i++)
                {
                    fs.Position = ((off + 0x20L) + (0x20L * i)) + 0x10L;
                    int num6 = br.ReadInt32() + off;
                    int num7 = br.ReadInt32() + off;
                    fs.Position = ((off + 0x20) + (0x20 * i)) + 4;
                    int texi = br.ReadInt32();
                    fs.Position = num7;
                    int capacity = br.ReadInt32();
                    t.al11.Add(new T11(num7, RUtil.RoundUpto16(4 + (4 * capacity)), i));
                    List<int> list = new List<int>(capacity);
                    for (int j = 0; j < capacity; j++)
                    {
                        list.Add(br.ReadInt32());
                    }
                    List<int> list2 = new List<int>();
                    List<int[]> list3 = new List<int[]>();
                    List<int> list4 = new List<int> {
                        num6
                    };
                    fs.Position = num6 + 0x10;
                    for (int k = 0; k < capacity; k++)
                    {
                        if (list[k] == -1)
                        {
                            list2.Add(((int) fs.Position) + 0x10);
                            fs.Position += 0x20L;
                        }
                        else
                        {
                            fs.Position += 0x10L;
                        }
                    }
                    for (int m = 0; m < capacity; m++)
                    {
                        if ((m + 1) == capacity)
                        {
                            list4.Add(list[m]);
                            list3.Add(list4.ToArray());
                            list4.Clear();
                        }
                        else if (list[m] == -1)
                        {
                            list3.Add(list4.ToArray());
                            list4.Clear();
                        }
                        else
                        {
                            list4.Add(list[m]);
                        }
                    }
                    int position = (int) fs.Position;
                    t.al12.Add(new T12(num6, position - num6, i));
                    int num14 = 0;
                    foreach (int num15 in list2)
                    {
                        fs.Position = num15;
                        int num16 = br.ReadInt32() & 0xffff;
                        int num17 = (br.ReadInt32() & 0x7fffffff) + off;
                        fs.Position = num17;
                        byte[] bin = br.ReadBytes(0x10 * num16);
                        t.al13.Add(new T13vif(num17, 0x10 * num16, texi, list3[num14], bin));
                        num14++;
                    }
                }
                fs.Position = off + 20;
                int num18 = br.ReadInt32();
                if (num18 != 0)
                {
                    num18 += off;
                    int len = 0x40 * num3;
                    t.t21 = new T21(num18, len);
                    fs.Position = num18;
                    for (int n = 0; n < (len / 0x40); n++)
                    {
                        t.t21.alaxb.Add(UtilAxBoneReader.read(br));
                    }
                }
                fs.Position = off + 0x18;
                int num21 = br.ReadInt32();
                if (num21 != 0)
                {
                    num21 += off;
                    int num22 = num18 - num21;
                    t.t32 = new T32(num21, num22);
                }
                fs.Position = off + 12;
                int item = br.ReadInt32();
                if (item != 0)
                {
                    item += off;
                    queue.Enqueue(item);
                }
            }
        }
    }
}

