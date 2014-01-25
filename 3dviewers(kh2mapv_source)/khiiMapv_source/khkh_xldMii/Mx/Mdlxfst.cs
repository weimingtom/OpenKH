namespace khkh_xldMii.Mx
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Mdlxfst
    {
        public List<T31> alt31;

        public unsafe Mdlxfst(Stream fs)
        {
            BinaryReader reader;
            Queue<int> queue;
            int num;
            int num2;
            int num3;
            int num4;
            T31 t;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            List<int> list;
            int num10;
            List<int> list2;
            List<int[]> list3;
            List<int> list4;
            int num11;
            int num12;
            int num13;
            int num14;
            int num15;
            int num16;
            int num17;
            byte[] buffer;
            int num18;
            int num19;
            int num20;
            int num21;
            int num22;
            int num23;
            List<int>.Enumerator enumerator;
            this.alt31 = new List<T31>();
            base..ctor();
            reader = new BinaryReader(fs);
            queue = new Queue<int>();
            queue.Enqueue(0x90);
            num = 0;
            goto Label_039C;
        Label_0030:
            num2 = queue.Dequeue();
            fs.Position = (long) (num2 + 0x10);
            num3 = reader.ReadUInt16();
            fs.Position = (long) (num2 + 0x1c);
            num4 = reader.ReadUInt16();
            this.alt31.Add(t = new T31(num2, 0x20 * (1 + num4), num));
            num += 1;
            num5 = 0;
            goto Label_02D2;
        Label_0085:
            fs.Position = ((((long) num2) + 0x20L) + (0x20L * ((long) num5))) + 0x10L;
            num6 = reader.ReadInt32() + num2;
            num7 = reader.ReadInt32() + num2;
            fs.Position = (long) (((num2 + 0x20) + (0x20 * num5)) + 4);
            num8 = reader.ReadInt32();
            fs.Position = (long) num7;
            num9 = reader.ReadInt32();
            t.al11.Add(new T11(num7, RUtil.RoundUpto16(4 + (4 * num9)), num5));
            list = new List<int>(num9);
            num10 = 0;
            goto Label_011E;
        Label_010B:
            list.Add(reader.ReadInt32());
            num10 += 1;
        Label_011E:
            if (num10 < num9)
            {
                goto Label_010B;
            }
            list2 = new List<int>();
            list3 = new List<int[]>();
            list4 = new List<int>();
            list2.Add(num6);
            fs.Position = (long) (num6 + 0x10);
            num11 = 0;
            goto Label_0198;
        Label_0153:
            if (list[num11] != -1)
            {
                goto Label_0182;
            }
            list2.Add(((int) fs.Position) + 0x10);
            fs.Position += 0x20L;
            goto Label_0192;
        Label_0182:
            fs.Position += 0x10L;
        Label_0192:
            num11 += 1;
        Label_0198:
            if (num11 < num9)
            {
                goto Label_0153;
            }
            num12 = 0;
            goto Label_020B;
        Label_01A3:
            if ((num12 + 1) != num9)
            {
                goto Label_01D2;
            }
            list4.Add(list[num12]);
            list3.Add(list4.ToArray());
            list4.Clear();
            goto Label_0205;
        Label_01D2:
            if (list[num12] != -1)
            {
                goto Label_01F5;
            }
            list3.Add(list4.ToArray());
            list4.Clear();
            goto Label_0205;
        Label_01F5:
            list4.Add(list[num12]);
        Label_0205:
            num12 += 1;
        Label_020B:
            if (num12 < num9)
            {
                goto Label_01A3;
            }
            num13 = (int) fs.Position;
            t.al12.Add(new T12(num6, num13 - num6, num5));
            num14 = 0;
            enumerator = list2.GetEnumerator();
        Label_0240:
            try
            {
                goto Label_02B3;
            Label_0242:
                num15 = &enumerator.Current;
                fs.Position = (long) num15;
                num16 = reader.ReadInt32() & 0xffff;
                num17 = (reader.ReadInt32() & 0x7fffffff) + num2;
                fs.Position = (long) num17;
                buffer = reader.ReadBytes(0x10 * num16);
                t.al13.Add(new T13vif(num17, 0x10 * num16, num8, list3[num14], buffer));
                num14 += 1;
            Label_02B3:
                if (&enumerator.MoveNext() != null)
                {
                    goto Label_0242;
                }
                goto Label_02CC;
            }
            finally
            {
            Label_02BE:
                &enumerator.Dispose();
            }
        Label_02CC:
            num5 += 1;
        Label_02D2:
            if (num5 < num4)
            {
                goto Label_0085;
            }
            fs.Position = (long) (num2 + 20);
            num18 = reader.ReadInt32();
            if (num18 == null)
            {
                goto Label_0343;
            }
            num18 += num2;
            num19 = 0x40 * num3;
            t.t21 = new T21(num18, num19);
            fs.Position = (long) num18;
            num20 = 0;
            goto Label_033A;
        Label_031D:
            t.t21.alaxb.Add(UtilAxBoneReader.read(reader));
            num20 += 1;
        Label_033A:
            if (num20 < (num19 / 0x40))
            {
                goto Label_031D;
            }
        Label_0343:
            fs.Position = (long) (num2 + 0x18);
            num21 = reader.ReadInt32();
            if (num21 == null)
            {
                goto Label_0377;
            }
            num21 += num2;
            num22 = num18 - num21;
            t.t32 = new T32(num21, num22);
        Label_0377:
            fs.Position = (long) (num2 + 12);
            num23 = reader.ReadInt32();
            if (num23 == null)
            {
                goto Label_039C;
            }
            num23 += num2;
            queue.Enqueue(num23);
        Label_039C:
            if (queue.Count != null)
            {
                goto Label_0030;
            }
            return;
        }
    }
}

