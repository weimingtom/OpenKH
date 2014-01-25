namespace khiiMapv
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    public class ParseSD
    {
        public ParseSD()
        {
            base..ctor();
            return;
        }

        public static unsafe Wavo[] ReadIV(Stream fs)
        {
            BinaryReader reader;
            BER ber;
            int num;
            KeyValuePair<int, int>[] pairArray;
            int num2;
            int num3;
            int num4;
            List<Wavo> list;
            int num5;
            int num6;
            int num7;
            int num8;
            reader = new BinaryReader(fs);
            ber = new BER(reader);
            fs.Position = 12L;
            num = reader.ReadInt32();
            pairArray = new KeyValuePair<int, int>[num];
            fs.Position = 0x10L;
            num2 = 0;
            goto Label_006F;
        Label_0033:
            num3 = reader.ReadInt32();
            if (num3 < 0)
            {
                goto Label_004B;
            }
            num3 += 0x10 + (8 * num);
        Label_004B:
            num4 = reader.ReadInt32();
            *(&(pairArray[num2])) = new KeyValuePair<int, int>(num3, num4);
            num2 += 1;
        Label_006F:
            if (num2 < num)
            {
                goto Label_0033;
            }
            list = new List<Wavo>();
            num5 = 0;
            goto Label_010A;
        Label_0083:
            num6 = &(pairArray[num5]).Key;
            if (num6 < 0)
            {
                goto Label_0104;
            }
            fs.Position = (long) (num6 + 12);
            num7 = ber.ReadInt32() - 0x20;
            fs.Position = (long) (num6 + 0x10);
            num8 = ber.ReadInt32();
            fs.Position = (long) (num6 + 0x40);
            list.Add(new Wavo(&num5.ToString("00") + ".wav", SPUConv.ToWave(new MemoryStream(reader.ReadBytes(num7)), num8)));
        Label_0104:
            num5 += 1;
        Label_010A:
            if (num5 < ((int) pairArray.Length))
            {
                goto Label_0083;
            }
            return list.ToArray();
        }

        public static unsafe Wavo[] ReadWD(Stream fs)
        {
            BinaryReader reader;
            int num;
            int num2;
            int[] numArray;
            int num3;
            int num4;
            int num5;
            List<Wavi> list;
            int num6;
            int num7;
            int num8;
            Wavi wavi;
            List<Wavo> list2;
            int num9;
            Wavi wavi2;
            int num10;
            byte[] buffer;
            int num11;
            int num12;
            int num13;
            reader = new BinaryReader(fs);
            fs.Position = 8L;
            num = reader.ReadInt32();
            num2 = reader.ReadInt32();
            numArray = new int[num];
            fs.Position = 0x20L;
            num3 = 0;
            goto Label_0042;
        Label_0032:
            numArray[num3] = reader.ReadInt32();
            num3 += 1;
        Label_0042:
            if (num3 < num)
            {
                goto Label_0032;
            }
            num4 = 0;
            num5 = 0;
            list = new List<Wavi>();
            num6 = 0;
            goto Label_0104;
        Label_005C:
            num7 = (0x20 + (4 * ((num + 3) & -4))) + (0x20 * num6);
            num8 = Array.IndexOf<int>(numArray, num7);
            if (num8 < 0)
            {
                goto Label_0085;
            }
            num4 = num8;
            num5 = 0;
        Label_0085:
            fs.Position = (long) (num7 + 0x10);
            if (reader.ReadInt64() != 0L)
            {
                goto Label_00A5;
            }
            if (reader.ReadInt64() == 0L)
            {
                goto Label_00FE;
            }
        Label_00A5:
            fs.Position = (long) (num7 + 4);
            wavi = new Wavi();
            wavi.off = reader.ReadInt32();
            wavi.gakki = num4;
            wavi.ontei = num5;
            num5 += 1;
            fs.Position = (long) (num7 + 0x16);
            wavi.sps = reader.ReadUInt16();
            list.Add(wavi);
        Label_00FE:
            num6 += 1;
        Label_0104:
            if (num6 < num2)
            {
                goto Label_005C;
            }
            list2 = new List<Wavo>();
            num9 = 0;
            goto Label_01FE;
        Label_011B:
            wavi2 = list[num9];
            num10 = ((0x20 + (0x10 * ((num + 3) & -4))) + (0x20 * num2)) + wavi2.off;
            fs.Position = (long) num10;
            goto Label_0174;
        Label_014C:
            buffer = reader.ReadBytes(0x10);
            num11 = 0;
            goto Label_0161;
        Label_015B:
            num11 += 1;
        Label_0161:
            if (num11 >= 0x10)
            {
                goto Label_016E;
            }
            if (buffer[num11] == null)
            {
                goto Label_015B;
            }
        Label_016E:
            if (num11 == 0x10)
            {
                goto Label_0182;
            }
        Label_0174:
            if (fs.Position < fs.Length)
            {
                goto Label_014C;
            }
        Label_0182:
            num12 = wavi2.sps;
            num13 = (Convert.ToInt32(fs.Position) - num10) - 0x20;
            fs.Position = (long) num10;
            list2.Add(new Wavo(&wavi2.gakki.ToString("000") + "." + &wavi2.ontei.ToString("00") + ".wav", SPUConv.ToWave(new MemoryStream(reader.ReadBytes(num13)), num12)));
            num9 += 1;
        Label_01FE:
            if (num9 < list.Count)
            {
                goto Label_011B;
            }
            return list2.ToArray();
        }

        private class BER
        {
            private BinaryReader br;

            public BER(BinaryReader br)
            {
                base..ctor();
                this.br = br;
                return;
            }

            public int ReadInt32()
            {
                byte[] buffer;
                buffer = this.br.ReadBytes(4);
                return ((((buffer[0] << 0x18) | (buffer[1] << 0x10)) | (buffer[2] << 8)) | buffer[3]);
            }

            public short ReadUInt16()
            {
                int num;
                num = this.br.ReadByte() << 8;
                return (short) (num | this.br.ReadByte());
            }
        }

        private class SPUConv
        {
            private static int[,] f;

            static SPUConv()
            {
                f = new int[,] { { 0, 0 }, { 60, 0 }, { 0x73, -52 }, { 0x62, -55 }, { 0x7a, -60 } };
                return;
            }

            public SPUConv()
            {
                base..ctor();
                return;
            }

            public static byte[] ToWave(MemoryStream fs, int nSamplesPerSec)
            {
                int num;
                int num2;
                List<int> list;
                byte[] buffer;
                int num3;
                int num4;
                int num5;
                int num6;
                int num7;
                int num8;
                MemoryStream stream;
                int num9;
                BinaryWriter writer;
                int num10;
                int num11;
                num = 0;
                num2 = 0;
                list = new List<int>();
                goto Label_011B;
            Label_000F:
                buffer = new byte[0x10];
                Trace.Assert(0x10 == fs.Read(buffer, 0, 0x10));
                num3 = buffer[0];
                num4 = num3 & 15;
                num3 = num3 >> 4;
                byte num1 = buffer[1];
                num5 = 0;
                goto Label_0112;
            Label_0048:
                num6 = buffer[2 + num5];
                num7 = (num6 & 15) << 12;
                if ((num7 & 0x8000) == null)
                {
                    goto Label_006E;
                }
                num7 |= -65536;
            Label_006E:
                num8 = num7 >> (num4 & 0x1f);
                num8 = (num8 + ((num * f[num3, 0]) >> 6)) + ((num2 * f[num3, 1]) >> 6);
                num2 = num;
                num = num8;
                num7 = (num6 & 240) << 8;
                list.Add(num8);
                if ((num7 & 0x8000) == null)
                {
                    goto Label_00CD;
                }
                num7 |= -65536;
            Label_00CD:
                num8 = num7 >> (num4 & 0x1f);
                num8 = (num8 + ((num * f[num3, 0]) >> 6)) + ((num2 * f[num3, 1]) >> 6);
                num2 = num;
                num = num8;
                list.Add(num8);
                num5 += 1;
            Label_0112:
                if (num5 < 14)
                {
                    goto Label_0048;
                }
            Label_011B:
                if ((fs.Position + 0x10L) <= fs.Length)
                {
                    goto Label_000F;
                }
                stream = new MemoryStream();
                num9 = list.Count;
                writer = new BinaryWriter(stream);
                writer.Write(Encoding.ASCII.GetBytes("RIFF"));
                writer.Write(50 + (2 * num9));
                writer.Write(Encoding.ASCII.GetBytes("WAVE"));
                writer.Write(Encoding.ASCII.GetBytes("fmt "));
                writer.Write(0x12);
                writer.Write(1);
                writer.Write(1);
                writer.Write(nSamplesPerSec);
                writer.Write(nSamplesPerSec * 2);
                writer.Write(2);
                writer.Write(0x10);
                writer.Write(0);
                writer.Write(Encoding.ASCII.GetBytes("fact"));
                writer.Write(4);
                writer.Write(num9);
                writer.Write(Encoding.ASCII.GetBytes("data"));
                writer.Write(2 * num9);
                num10 = 0;
                goto Label_0257;
            Label_0229:
                num11 = Math.Max(-32768, Math.Min(0x7fff, list[num10]));
                writer.Write((ushort) num11);
                num10 += 1;
            Label_0257:
                if (num10 < num9)
                {
                    goto Label_0229;
                }
                return stream.ToArray();
            }

            public static byte[] ToWave2ch(MemoryStream fs, int nSamplesPerSec)
            {
                int num;
                int num2;
                int num3;
                int num4;
                List<int> list;
                List<int> list2;
                byte[] buffer;
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
                MemoryStream stream;
                int num17;
                BinaryWriter writer;
                int num18;
                int num19;
                int num20;
                num = 0;
                num2 = 0;
                num3 = 0;
                num4 = 0;
                list = new List<int>();
                list2 = new List<int>();
                goto Label_0238;
            Label_001B:
                buffer = new byte[0x10];
                Trace.Assert(0x10 == fs.Read(buffer, 0, 0x10));
                num5 = buffer[0];
                num6 = num5 & 15;
                num5 = num5 >> 4;
                byte num1 = buffer[1];
                num7 = 0;
                goto Label_0125;
            Label_0058:
                num8 = buffer[2 + num7];
                num9 = (num8 & 15) << 12;
                if ((num9 & 0x8000) == null)
                {
                    goto Label_007F;
                }
                num9 |= -65536;
            Label_007F:
                num10 = num9 >> (num6 & 0x1f);
                num10 = (num10 + ((num * f[num5, 0]) >> 6)) + ((num2 * f[num5, 1]) >> 6);
                num2 = num;
                num = num10;
                num9 = (num8 & 240) << 8;
                list.Add(num10);
                if ((num9 & 0x8000) == null)
                {
                    goto Label_00DF;
                }
                num9 |= -65536;
            Label_00DF:
                num10 = num9 >> (num6 & 0x1f);
                num10 = (num10 + ((num * f[num5, 0]) >> 6)) + ((num2 * f[num5, 1]) >> 6);
                num2 = num;
                num = num10;
                list.Add(num10);
                num7 += 1;
            Label_0125:
                if (num7 < 14)
                {
                    goto Label_0058;
                }
                Trace.Assert(0x10 == fs.Read(buffer, 0, 0x10));
                num11 = buffer[0];
                num12 = num11 & 15;
                num11 = num11 >> 4;
                byte num21 = buffer[1];
                num13 = 0;
                goto Label_022F;
            Label_0162:
                num14 = buffer[2 + num13];
                num15 = (num14 & 15) << 12;
                if ((num15 & 0x8000) == null)
                {
                    goto Label_0189;
                }
                num15 |= -65536;
            Label_0189:
                num16 = num15 >> (num12 & 0x1f);
                num16 = (num16 + ((num3 * f[num11, 0]) >> 6)) + ((num4 * f[num11, 1]) >> 6);
                num4 = num3;
                num3 = num16;
                num15 = (num14 & 240) << 8;
                list2.Add(num16);
                if ((num15 & 0x8000) == null)
                {
                    goto Label_01E9;
                }
                num15 |= -65536;
            Label_01E9:
                num16 = num15 >> (num12 & 0x1f);
                num16 = (num16 + ((num3 * f[num11, 0]) >> 6)) + ((num4 * f[num11, 1]) >> 6);
                num4 = num3;
                num3 = num16;
                list2.Add(num16);
                num13 += 1;
            Label_022F:
                if (num13 < 14)
                {
                    goto Label_0162;
                }
            Label_0238:
                if ((fs.Position + 0x20L) <= fs.Length)
                {
                    goto Label_001B;
                }
                stream = new MemoryStream();
                num17 = list.Count;
                writer = new BinaryWriter(stream);
                writer.Write(Encoding.ASCII.GetBytes("RIFF"));
                writer.Write(50 + (4 * num17));
                writer.Write(Encoding.ASCII.GetBytes("WAVE"));
                writer.Write(Encoding.ASCII.GetBytes("fmt "));
                writer.Write(0x12);
                writer.Write(1);
                writer.Write(2);
                writer.Write(nSamplesPerSec);
                writer.Write(nSamplesPerSec * 4);
                writer.Write(4);
                writer.Write(0x10);
                writer.Write(0);
                writer.Write(Encoding.ASCII.GetBytes("fact"));
                writer.Write(4);
                writer.Write(num17);
                writer.Write(Encoding.ASCII.GetBytes("data"));
                writer.Write(4 * num17);
                num18 = 0;
                goto Label_039F;
            Label_0347:
                num19 = Math.Max(-32768, Math.Min(0x7fff, list[num18]));
                writer.Write((ushort) num19);
                num20 = Math.Max(-32768, Math.Min(0x7fff, list2[num18]));
                writer.Write((ushort) num20);
                num18 += 1;
            Label_039F:
                if (num18 < num17)
                {
                    goto Label_0347;
                }
                return stream.ToArray();
            }
        }

        private class Wavi
        {
            public int gakki;
            public int off;
            public int ontei;
            public int sps;

            public Wavi()
            {
                base..ctor();
                return;
            }
        }
    }
}

