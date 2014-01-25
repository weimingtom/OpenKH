namespace ParseAI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class Parse03
    {
        private BinaryReader br;
        private SortedDictionary<int, Dis> dict;
        private SortedDictionary<int, string> labels;
        private MemoryStream si;
        private TextWriter wri;

        public Parse03(TextWriter wri)
        {
            this.dict = new SortedDictionary<int, Dis>();
            this.labels = new SortedDictionary<int, string>();
            base..ctor();
            this.wri = wri;
            return;
        }

        private unsafe void Gen()
        {
            int num;
            int num2;
            string str;
            Dis dis;
            int num3;
            Func<int, bool> func;
            <>c__DisplayClass2 class2;
            num = this.dict.Keys.Max<int>();
            num2 = 0;
            goto Label_0115;
        Label_0018:
            if (this.labels.TryGetValue(num2, &str) == null)
            {
                goto Label_0039;
            }
            this.wri.WriteLine("{0}:", str);
        Label_0039:
            if (this.dict.TryGetValue(num2, &dis) == null)
            {
                goto Label_0111;
            }
            this.wri.WriteLine(" {0,-40}; {1,4} {2:x}", dis.Desc, (int) num2, (int) (0x10 + (2 * num2)));
            if (num2 >= num)
            {
                goto Label_0111;
            }
            func = null;
            class2 = new <>c__DisplayClass2();
            class2.x1 = num2 + (dis.Len / 2);
            if (this.dict.ContainsKey(class2.x1) != null)
            {
                goto Label_0111;
            }
            if (func != null)
            {
                goto Label_00C7;
            }
            func = new Func<int, bool>(class2.<Gen>b__0);
        Label_00C7:
            num3 = this.dict.Keys.First<int>(func);
            if ((num3 - class2.x1) <= 1)
            {
                goto Label_0111;
            }
            this.wri.WriteLine(" ; Unscanned {2} words. {0} to {1}", (int) class2.x1, (int) (num3 - 1), (int) (num3 - class2.x1));
        Label_0111:
            num2 += 1;
        Label_0115:
            if (num2 <= num)
            {
                goto Label_0018;
            }
            return;
        }

        private unsafe string GenLabel(int newoff)
        {
            string str;
            if (this.labels.TryGetValue(newoff, &str) != null)
            {
                goto Label_002E;
            }
            str = string.Format("L{0:x4}", (int) newoff);
            this.labels[newoff] = str;
        Label_002E:
            return str;
        }

        public void Run(byte[] bin)
        {
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            this.si = new MemoryStream(bin, 0);
            this.br = new BinaryReader(this.si);
            this.si.Position = 0L;
            this.wri.WriteLine("#{0}", Ut.Read0Str(this.br));
            this.wri.WriteLine("#Parseai 20100831");
            this.si.Position = 0x10L;
            num = this.br.ReadInt32();
            num2 = this.br.ReadInt32();
            num3 = this.br.ReadInt32();
            this.wri.WriteLine("#{0:x} {1:x} {2:x}", (int) num, (int) num2, (int) num3);
            num4 = 0;
        Label_00AC:
            this.si.Position = (long) (0x1c + (8 * num4));
            num5 = this.br.ReadInt32();
            num6 = this.br.ReadInt32();
            if (num5 != null)
            {
                goto Label_00E0;
            }
            if (num6 == null)
            {
                goto Label_00F0;
            }
        Label_00E0:
            this.Walk(num5, num6);
            num4 += 1;
            goto Label_00AC;
        Label_00F0:
            this.Gen();
            return;
        }

        private void Walk(int key, int off)
        {
            Queue<int> queue;
            CmdObs obs;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            int num14;
            int num15;
            int num16;
            int num17;
            int num18;
            int num19;
            int num20;
            int num21;
            int num22;
            int num23;
            int num24;
            int num25;
            int num26;
            int num27;
            int num28;
            int num29;
            bool flag;
            int num30;
            int num31;
            int num32;
            int num33;
            object[] objArray;
            object[] objArray2;
            queue = new Queue<int>();
            queue.Enqueue(off);
            this.labels[off] = "K" + ((int) key);
            obs = new CmdObs();
            goto Label_0875;
        Label_0034:
            num = queue.Dequeue();
        Label_003B:
            off = num;
            if (this.dict.ContainsKey(off) != null)
            {
                goto Label_0875;
            }
            this.si.Position = (long) (0x10 + (off * 2));
            if (this.si.Position >= this.si.Length)
            {
                goto Label_0875;
            }
            num2 = this.br.ReadUInt16();
            num3 = num2 & 15;
            num4 = num2 & 0xff;
            obs.Eat(num2);
            if (num2 != 0xffff)
            {
                goto Label_00CD;
            }
            this.dict[off] = new Dis(2, string.Format("TERM", new object[0]));
            goto Label_0875;
        Label_00CD:
            if (num4 != 0x30)
            {
                goto Label_0128;
            }
            num5 = this.br.ReadByte();
            num6 = this.br.ReadByte();
            this.dict[off] = new Dis(4, string.Format("Cmd30 {0:x2} {1:x2} {2:x2} ", (int) (num2 >> 8), (int) num5, (int) num6));
            num = off + 2;
            goto Label_003B;
        Label_0128:
            if (num4 != 0x60)
            {
                goto Label_0183;
            }
            num7 = this.br.ReadByte();
            num8 = this.br.ReadByte();
            this.dict[off] = new Dis(4, string.Format("Cmd60 {0:x2} {1:x2} {2:x2} ", (int) (num2 >> 8), (int) num7, (int) num8));
            num = off + 2;
            goto Label_003B;
        Label_0183:
            if (num4 != 160)
            {
                goto Label_01E1;
            }
            num9 = this.br.ReadByte();
            num10 = this.br.ReadByte();
            this.dict[off] = new Dis(4, string.Format("Cmda0 {0:x2} {1:x2} {2:x2} ", (int) (num2 >> 8), (int) num9, (int) num10));
            num = off + 2;
            goto Label_003B;
        Label_01E1:
            if (num4 != 0xc0)
            {
                goto Label_0275;
            }
            num11 = this.br.ReadInt32();
            if (obs.Curt != 1)
            {
                goto Label_0241;
            }
            if (num11 == null)
            {
                goto Label_0241;
            }
            num12 = num11;
            queue.Enqueue(num12);
            this.dict[off] = new Dis(6, string.Format("Cmdc0l {0:x2} {1} ", (int) (num2 >> 8), this.GenLabel(num12)));
            goto Label_026C;
        Label_0241:
            this.dict[off] = new Dis(6, string.Format("Cmdc0i {0:x2} {1:x8} ", (int) (num2 >> 8), (int) num11));
        Label_026C:
            num = off + 3;
            goto Label_003B;
        Label_0275:
            if (num4 != 0xe0)
            {
                goto Label_02D6;
            }
            num13 = this.br.ReadUInt16();
            this.si.Position = (long) (0x10 + (num13 * 2));
            this.dict[off] = new Dis(4, string.Format("Print {0:x2} '{1}' ", (int) (num2 >> 8), Ut.Read0Str(this.br)));
            num = off + 2;
            goto Label_003B;
        Label_02D6:
            if (num4 != 11)
            {
                goto Label_0335;
            }
            num14 = this.br.ReadInt32();
            num15 = (off + 3) + num14;
            this.dict[off] = new Dis(6, string.Format("Call {0:x3} {1} ; {2} ", (int) (num2 >> 4), this.GenLabel(num15), (int) num14));
            queue.Enqueue(num15);
            num = off + 3;
            goto Label_003B;
        Label_0335:
            if (num4 != 0x89)
            {
                goto Label_0367;
            }
            this.dict[off] = new Dis(2, string.Format("Ret {0:x2}", (int) (num2 >> 8)));
            goto Label_0875;
        Label_0367:
            if (num3 != null)
            {
                goto Label_0409;
            }
            num16 = this.br.ReadByte();
            num17 = this.br.ReadByte();
            num18 = this.br.ReadByte();
            num19 = this.br.ReadByte();
            this.dict[off] = new Dis(6, string.Format("Cmd0 {0:x3} {1:x2} {2:x2} {3:x2} {4:x2} ", new object[] { (int) (num2 >> 4), (int) num16, (int) num17, (int) num18, (int) num19 }));
            num = off + 3;
            goto Label_003B;
        Label_0409:
            if (num3 != 1)
            {
                goto Label_0463;
            }
            num20 = this.br.ReadByte();
            num21 = this.br.ReadByte();
            this.dict[off] = new Dis(4, string.Format("Cmd1 {0:x3} {1:x2} {2:x2} ", (int) (num2 >> 4), (int) num20, (int) num21));
            num = off + 2;
            goto Label_003B;
        Label_0463:
            if (num3 != 2)
            {
                goto Label_0506;
            }
            num22 = this.br.ReadByte();
            num23 = this.br.ReadByte();
            num24 = this.br.ReadByte();
            num25 = this.br.ReadByte();
            this.dict[off] = new Dis(6, string.Format("Cmd2 {0:x3} {1:x2} {2:x2} {3:x2} {4:x2} ", new object[] { (int) (num2 >> 4), (int) num22, (int) num23, (int) num24, (int) num25 }));
            num = off + 3;
            goto Label_003B;
        Label_0506:
            if (num3 != 3)
            {
                goto Label_0560;
            }
            num26 = this.br.ReadByte();
            num27 = this.br.ReadByte();
            this.dict[off] = new Dis(4, string.Format("Cmd3 {0:x3} {1:x2} {2:x2} ", (int) (num2 >> 4), (int) num26, (int) num27));
            num = off + 2;
            goto Label_003B;
        Label_0560:
            if (num3 != 4)
            {
                goto Label_0592;
            }
            this.dict[off] = new Dis(2, string.Format("Cmd4 {0:x3} ", (int) (num2 >> 4)));
            num = off + 1;
            goto Label_003B;
        Label_0592:
            if (num3 != 5)
            {
                goto Label_05C4;
            }
            this.dict[off] = new Dis(2, string.Format("Cmd5 {0:x3} ", (int) (num2 >> 4)));
            num = off + 1;
            goto Label_003B;
        Label_05C4:
            if (num3 != 6)
            {
                goto Label_05F6;
            }
            this.dict[off] = new Dis(2, string.Format("Cmd6 {0:x3} ", (int) (num2 >> 4)));
            num = off + 1;
            goto Label_003B;
        Label_05F6:
            if (num3 != 7)
            {
                goto Label_065C;
            }
            num28 = this.br.ReadInt16();
            num29 = (off + 2) + num28;
            this.dict[off] = new Dis(4, string.Format("J7 {0:x3} {1} ", (int) (num2 >> 4), this.GenLabel(num29)));
            queue.Enqueue(num29);
            if (((num2 >> 4) == 0) != null)
            {
                goto Label_0875;
            }
            num = off + 2;
            goto Label_003B;
        Label_065C:
            if (num3 != 8)
            {
                goto Label_06B3;
            }
            num30 = this.br.ReadInt16();
            num31 = (off + 2) + num30;
            this.dict[off] = new Dis(4, string.Format("J8 {0:x3} {1} ", (int) (num2 >> 4), this.GenLabel(num31)));
            queue.Enqueue(num31);
            num = off + 2;
            goto Label_003B;
        Label_06B3:
            if (num3 != 9)
            {
                goto Label_06E6;
            }
            this.dict[off] = new Dis(2, string.Format("Pause {0:x3} ", (int) (num2 >> 4)));
            num = off + 1;
            goto Label_003B;
        Label_06E6:
            if (num3 != 10)
            {
                goto Label_0741;
            }
            num32 = this.br.ReadByte();
            num33 = this.br.ReadByte();
            this.dict[off] = new Dis(4, string.Format("Cmda {0:x3} {1:x2} {2:x2} ", (int) (num2 >> 4), (int) num32, (int) num33));
            num = off + 2;
            goto Label_003B;
        Label_0741:
            if (num3 != 11)
            {
                goto Label_0774;
            }
            this.dict[off] = new Dis(2, string.Format("Cmdb {0:x3} ", (int) (num2 >> 4)));
            num = off + 1;
            goto Label_003B;
        Label_0774:
            if (num3 != 12)
            {
                goto Label_07A7;
            }
            this.dict[off] = new Dis(2, string.Format("Cmdc {0:x3} ", (int) (num2 >> 4)));
            num = off + 1;
            goto Label_003B;
        Label_07A7:
            if (num3 != 13)
            {
                goto Label_07DA;
            }
            this.dict[off] = new Dis(2, string.Format("Cmdd {0:x3} ", (int) (num2 >> 4)));
            num = off + 1;
            goto Label_003B;
        Label_07DA:
            if (num3 != 14)
            {
                goto Label_080D;
            }
            this.dict[off] = new Dis(2, string.Format("Cmde {0:x3} ", (int) (num2 >> 4)));
            num = off + 1;
            goto Label_003B;
        Label_080D:
            if (num3 != 15)
            {
                goto Label_0840;
            }
            this.dict[off] = new Dis(2, string.Format("Cmdf {0:x3} ", (int) (num2 >> 4)));
            num = off + 1;
            goto Label_003B;
        Label_0840:
            this.dict[off] = new Dis(1, string.Format("? {0:x4} ", (int) num2, (long) (this.si.Position - 2L)));
        Label_0875:
            if (queue.Count != null)
            {
                goto Label_0034;
            }
            return;
        }

        [CompilerGenerated]
        private sealed class <>c__DisplayClass2
        {
            public int x1;

            public <>c__DisplayClass2()
            {
                base..ctor();
                return;
            }

            public bool <Gen>b__0(int p)
            {
                return (p > this.x1);
            }
        }

        private class CmdObs
        {
            private int stat;

            public CmdObs()
            {
                this.stat = -1;
                base..ctor();
                return;
            }

            public void Eat(int v0)
            {
                if (0xe0 != (0xff & v0))
                {
                    goto Label_0016;
                }
                this.stat = 0;
                return;
            Label_0016:
                if (0xc0 != (0xff & v0))
                {
                    goto Label_003C;
                }
                if (this.stat < 0)
                {
                    goto Label_003C;
                }
                this.stat += 1;
                return;
            Label_003C:
                this.stat = -1;
                return;
            }

            public T Curt
            {
                get
                {
                    int num;
                    switch ((this.stat - 2))
                    {
                        case 0:
                            goto Label_0035;

                        case 1:
                            goto Label_0035;

                        case 2:
                            goto Label_0035;

                        case 3:
                            goto Label_0035;

                        case 4:
                            goto Label_0035;

                        case 5:
                            goto Label_0035;

                        case 6:
                            goto Label_0035;

                        case 7:
                            goto Label_0035;

                        case 8:
                            goto Label_0035;
                    }
                    goto Label_0037;
                Label_0035:
                    return 1;
                Label_0037:
                    return 0;
                }
            }

            public enum T
            {
                Val,
                Label
            }
        }

        private class Dis
        {
            public string Desc;
            public int Len;

            public Dis(int cb, string s)
            {
                this.Desc = string.Empty;
                base..ctor();
                this.Len = cb;
                this.Desc = s;
                return;
            }
        }

        private class Ut
        {
            public Ut()
            {
                base..ctor();
                return;
            }

            public static string Read0Str(BinaryReader br)
            {
                string str;
                int num;
                str = "";
            Label_0006:
                num = br.ReadByte();
                if (num == null)
                {
                    goto Label_0020;
                }
                str = str + ((char) ((ushort) num));
                goto Label_0006;
            Label_0020:
                return str;
            }
        }
    }
}

