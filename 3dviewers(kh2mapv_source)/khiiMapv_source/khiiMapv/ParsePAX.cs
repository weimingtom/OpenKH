namespace khiiMapv
{
    using khiiMapv.Pax;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using vwBinTex2;

    public class ParsePAX
    {
        public ParsePAX()
        {
            base..ctor();
            return;
        }

        public static unsafe PicPAX ReadPAX(MemoryStream si)
        {
            BinaryReader reader;
            int num;
            int num2;
            int num3;
            PicPAX cpax;
            int num4;
            int num5;
            int num6;
            int num7;
            List<int> list;
            int num8;
            int num9;
            List<int> list2;
            int num10;
            int num11;
            List<int> list3;
            int num12;
            int num13;
            List<int> list4;
            int num14;
            int num15;
            List<int> list5;
            int num16;
            R r;
            int num17;
            int num18;
            int num19;
            int num20;
            int num21;
            byte[] buffer;
            byte[] buffer2;
            int num22;
            int num23;
            St3 st;
            int num24;
            int num25;
            St3r str;
            int num26;
            STXYC stxyc;
            STXYC stxyc2;
            int num27;
            STXYC stxyc3;
            int num28;
            int num29;
            int num30;
            int num31;
            si.Position = 0L;
            reader = new BinaryReader(si);
            if (si.ReadByte() == 80)
            {
                goto Label_0024;
            }
            throw new NotSupportedException("!PAX");
        Label_0024:
            if (si.ReadByte() == 0x41)
            {
                goto Label_0039;
            }
            throw new NotSupportedException("!PAX");
        Label_0039:
            if (si.ReadByte() == 0x58)
            {
                goto Label_004E;
            }
            throw new NotSupportedException("!PAX");
        Label_004E:
            if (si.ReadByte() == 0x5f)
            {
                goto Label_0063;
            }
            throw new NotSupportedException("!PAX");
        Label_0063:
            si.Position = 12L;
            num = reader.ReadInt32();
            si.Position = (long) num;
            if (reader.ReadInt32() == 130)
            {
                goto Label_0095;
            }
            throw new NotSupportedException("!82");
        Label_0095:
            si.Position = (long) (num + 12);
            num3 = reader.ReadInt32();
            cpax = new PicPAX();
            num4 = 0;
            goto Label_04B8;
        Label_00B6:
            si.Position = (long) ((num + 0x10) + (0x20 * num4));
            num5 = num + reader.ReadInt32();
            si.Position = (long) num5;
            if (reader.ReadInt32() == 150)
            {
                goto Label_00F6;
            }
            throw new NotSupportedException("!96");
        Label_00F6:
            num7 = reader.ReadInt32();
            list = new List<int>();
            num8 = 0;
            goto Label_0120;
        Label_010A:
            list.Add(num5 + reader.ReadInt32());
            num8 += 1;
        Label_0120:
            if (num8 < num7)
            {
                goto Label_010A;
            }
            num9 = reader.ReadInt32();
            list2 = new List<int>();
            num10 = 0;
            goto Label_0150;
        Label_013A:
            list2.Add(num5 + reader.ReadInt32());
            num10 += 1;
        Label_0150:
            if (num10 < num9)
            {
                goto Label_013A;
            }
            num11 = reader.ReadInt32();
            list3 = new List<int>();
            num12 = 0;
            goto Label_0180;
        Label_016A:
            list3.Add(num5 + reader.ReadInt32());
            num12 += 1;
        Label_0180:
            if (num12 < num11)
            {
                goto Label_016A;
            }
            num13 = reader.ReadInt32();
            list4 = new List<int>();
            num14 = 0;
            goto Label_01B0;
        Label_019A:
            list4.Add(num5 + reader.ReadInt32());
            num14 += 1;
        Label_01B0:
            if (num14 < num13)
            {
                goto Label_019A;
            }
            num15 = reader.ReadInt32();
            list5 = new List<int>();
            num16 = 0;
            goto Label_01E0;
        Label_01CA:
            list5.Add(num5 + reader.ReadInt32());
            num16 += 1;
        Label_01E0:
            if (num16 < num15)
            {
                goto Label_01CA;
            }
            r = new R();
            cpax.alr.Add(r);
            num17 = 0;
            goto Label_028F;
        Label_0203:
            num18 = list2[num17];
            si.Position = (long) num18;
            reader.ReadInt32();
            reader.ReadInt16();
            num19 = reader.ReadInt16();
            reader.ReadInt32();
            num20 = reader.ReadInt16();
            num21 = reader.ReadInt16();
            if (num19 != 0x13)
            {
                goto Label_0289;
            }
            si.Position = (long) (num18 + 0x20);
            buffer = reader.ReadBytes(num20 * num21);
            buffer2 = reader.ReadBytes(0x400);
            r.pics.Add(BUt.Make8(buffer, buffer2, num20, num21));
        Label_0289:
            num17 += 1;
        Label_028F:
            if (num17 < list2.Count)
            {
                goto Label_0203;
            }
            num22 = 0;
            goto Label_04A4;
        Label_02A5:
            num23 = list3[num22];
            si.Position = (long) (num23 + 20);
            st = new St3();
            r.als3.Add(st);
            st.cnt1 = reader.ReadInt16();
            st.cnt2 = reader.ReadInt16();
            num24 = 0;
            goto Label_0490;
        Label_02F3:
            si.Position = (long) ((num23 + 0x20) + (8 * num24));
            num25 = reader.ReadUInt16();
            str = new St3r();
            st.al3r.Add(str);
            si.Position = (long) ((num23 + 0x10) + num25);
            str.v0 = reader.ReadUInt16();
            str.v2 = reader.ReadUInt16();
            str.v4 = reader.ReadUInt16();
            si.Position = (long) (((num23 + 0x10) + num25) + 0x20);
            num26 = 0;
            goto Label_040B;
        Label_0371:
            stxyc = new STXYC();
            stxyc2 = new STXYC();
            &stxyc.x = reader.ReadInt16();
            &stxyc.y = reader.ReadInt16();
            &stxyc2.x = reader.ReadInt16();
            &stxyc2.y = reader.ReadInt16();
            &stxyc.s = reader.ReadInt16();
            &stxyc.t = reader.ReadInt16();
            &stxyc2.s = reader.ReadInt16();
            &stxyc2.t = reader.ReadInt16();
            str.alv.Add(stxyc);
            str.alv.Add(stxyc2);
            num26 += 1;
        Label_040B:
            if (num26 < str.v2)
            {
                goto Label_0371;
            }
            num27 = 0;
            goto Label_047F;
        Label_041E:
            stxyc3 = str.alv[num27];
            num28 = reader.ReadByte();
            num29 = reader.ReadByte();
            num30 = reader.ReadByte();
            num31 = reader.ReadByte();
            &stxyc3.clr = Color.FromArgb(num31, num28, num29, num30);
            str.alv[num27] = stxyc3;
            reader.ReadInt32();
            num27 += 1;
        Label_047F:
            if (num27 < str.v2)
            {
                goto Label_041E;
            }
            num24 += 1;
        Label_0490:
            if (num24 < st.cnt1)
            {
                goto Label_02F3;
            }
            num22 += 1;
        Label_04A4:
            if (num22 < list3.Count)
            {
                goto Label_02A5;
            }
            num4 += 1;
        Label_04B8:
            if (num4 < num3)
            {
                goto Label_00B6;
            }
            return cpax;
        }

        private class BUt
        {
            public BUt()
            {
                base..ctor();
                return;
            }

            public static unsafe Bitmap Make8(byte[] pic, byte[] pal, int cx, int cy)
            {
                Bitmap bitmap;
                BitmapData data;
                ColorPalette palette;
                int num;
                int num2;
                int num3;
                bitmap = new Bitmap(cx, cy, 0x30803);
                data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 2, 0x30803);
            Label_002D:
                try
                {
                    Marshal.Copy(pic, 0, data.Scan0, Math.Min(data.Stride * data.Height, (int) pic.Length));
                    goto Label_0059;
                }
                finally
                {
                Label_0051:
                    bitmap.UnlockBits(data);
                }
            Label_0059:
                palette = bitmap.Palette;
                num = 0;
                goto Label_00B4;
            Label_0064:
                num2 = num;
                num3 = KHcv8pal_swap34.repl(num);
                *(&(palette.Entries[num3])) = Color.FromArgb(Math.Min(0xff, pal[(4 * num2) + 3] * 2), pal[4 * num2], pal[(4 * num2) + 1], pal[(4 * num2) + 2]);
                num += 1;
            Label_00B4:
                if (num < 0x100)
                {
                    goto Label_0064;
                }
                bitmap.Palette = palette;
                return bitmap;
            }
        }
    }
}

