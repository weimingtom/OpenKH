namespace khiiMapv.Parse02
{
    using khiiMapv.Properties;
    using System;
    using System.Collections.Generic;

    public class StrDec
    {
        public SortedDictionary<int, string> evt;
        public SortedDictionary<int, string> sys;

        public StrDec()
        {
            string[] strArray;
            int num;
            string[] strArray2;
            int num2;
            string str;
            string[] strArray3;
            int num3;
            string str2;
            string[] strArray4;
            int num4;
            string str3;
            string[] strArray5;
            int num5;
            string str4;
            string[] strArray6;
            char[] chArray;
            string[] strArray7;
            int num6;
            char[] chArray2;
            string[] strArray8;
            int num7;
            char[] chArray3;
            string[] strArray9;
            int num8;
            char[] chArray4;
            string[] strArray10;
            int num9;
            this.evt = new SortedDictionary<int, string>();
            this.sys = new SortedDictionary<int, string>();
            base..ctor();
            strArray = new string[] { Resources.EVTjp0, Resources.EVTjp1, Resources.EVTjp2, Resources.EVTjp3 };
            num = 0;
            goto Label_00FD;
        Label_0052:;
            strArray2 = strArray[num].Replace("\r\n", "\n").Split(new char[] { 10 });
            num2 = 0;
            strArray7 = strArray2;
            num6 = 0;
            goto Label_00F1;
        Label_0084:
            str = strArray7[num6];
            strArray3 = str.Split(new char[] { 9 });
            num3 = 0;
            strArray8 = strArray3;
            num7 = 0;
            goto Label_00DF;
        Label_00B0:
            str2 = strArray8[num7];
            this.evt[(((num * 0x15) * 0x15) + (num2 * 0x15)) + num3] = str2;
            num3 += 1;
            num7 += 1;
        Label_00DF:
            if (num7 < ((int) strArray8.Length))
            {
                goto Label_00B0;
            }
            num2 += 1;
            num6 += 1;
        Label_00F1:
            if (num6 < ((int) strArray7.Length))
            {
                goto Label_0084;
            }
            num += 1;
        Label_00FD:
            if (num < 4)
            {
                goto Label_0052;
            }
            strArray4 = Resources.SYSjp.Replace("\r\n", "\n").Split(new char[] { 10 });
            num4 = 0;
            strArray9 = strArray4;
            num8 = 0;
            goto Label_01A3;
        Label_013B:
            str3 = strArray9[num8];
            strArray5 = str3.Split(new char[] { 9 });
            num5 = 0;
            strArray10 = strArray5;
            num9 = 0;
            goto Label_018F;
        Label_0167:
            str4 = strArray10[num9];
            this.sys[(0x1c * num4) + num5] = str4;
            num5 += 1;
            num9 += 1;
        Label_018F:
            if (num9 < ((int) strArray10.Length))
            {
                goto Label_0167;
            }
            num4 += 1;
            num8 += 1;
        Label_01A3:
            if (num8 < ((int) strArray9.Length))
            {
                goto Label_013B;
            }
            return;
        }

        public StrCodeCollection DecodeEvt(byte[] bin, int start)
        {
            StrCodeCollection codes;
            int num;
            byte num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            byte num10;
            int num11;
            string str;
            string str2;
            byte[] buffer;
            byte[] buffer2;
            byte[] buffer3;
            byte[] buffer4;
            byte[] buffer5;
            byte[] buffer6;
            byte[] buffer7;
            byte[] buffer8;
            byte[] buffer9;
            codes = new StrCodeCollection();
            num = start;
            goto Label_02B8;
        Label_000D:
            num2 = bin[num];
            num += 1;
            if (num2 != null)
            {
                goto Label_0037;
            }
            codes.Add(new EndCode(new byte[] { num2 }));
            goto Label_02C1;
        Label_0037:
            if (num2 != 1)
            {
                goto Label_005F;
            }
            codes.Add(new CharCode(" ", new byte[] { num2 }));
            goto Label_02B8;
        Label_005F:
            if (num2 != 2)
            {
                goto Label_0087;
            }
            codes.Add(new CharCode("\r\n", new byte[] { num2 }));
            goto Label_02B8;
        Label_0087:
            if (num2 != 3)
            {
                goto Label_00AA;
            }
            codes.Add(new Code03(new byte[] { num2 }));
            goto Label_02B8;
        Label_00AA:
            if (num2 != 4)
            {
                goto Label_00D0;
            }
            num3 = 1;
            codes.Add(new Code04(BUt.Copy(bin, num - 1, num3 + 1)));
            num += num3;
            goto Label_02B8;
        Label_00D0:
            if (num2 != 6)
            {
                goto Label_00F9;
            }
            num4 = 5;
            codes.Add(new Code06(BUt.Copy(bin, num - 1, num4 + 1)));
            num += num4;
            goto Label_02B8;
        Label_00F9:
            if (num2 != 8)
            {
                goto Label_0122;
            }
            num5 = 3;
            codes.Add(new Code08(BUt.Copy(bin, num - 1, num5 + 1)));
            num += num5;
            goto Label_02B8;
        Label_0122:
            if (num2 != 13)
            {
                goto Label_0146;
            }
            codes.Add(new VarCode(new byte[] { num2 }));
            goto Label_02B8;
        Label_0146:
            if (num2 != 0x10)
            {
                goto Label_016A;
            }
            codes.Add(new WaitCode(new byte[] { num2 }));
            goto Label_02B8;
        Label_016A:
            if (num2 != 0x13)
            {
                goto Label_0194;
            }
            num6 = 4;
            codes.Add(new Code13(BUt.Copy(bin, num - 1, num6 + 1)));
            num += num6;
            goto Label_02B8;
        Label_0194:
            if (num2 != 20)
            {
                goto Label_01BE;
            }
            num7 = 2;
            codes.Add(new Code14(BUt.Copy(bin, num - 1, num7 + 1)));
            num += num7;
            goto Label_02B8;
        Label_01BE:
            if (num2 != 0x15)
            {
                goto Label_01E8;
            }
            num8 = 2;
            codes.Add(new Code15(BUt.Copy(bin, num - 1, num8 + 1)));
            num += num8;
            goto Label_02B8;
        Label_01E8:
            if (num2 != 0x17)
            {
                goto Label_0212;
            }
            num9 = 3;
            codes.Add(new Code17(BUt.Copy(bin, num - 1, num9 + 1)));
            num += num9;
            goto Label_02B8;
        Label_0212:
            if (num2 < 0x18)
            {
                goto Label_026A;
            }
            if (30 < num2)
            {
                goto Label_026A;
            }
            num10 = bin[num];
            num += 1;
            num11 = 0x100 * (num2 - 0x18);
            str = this.evt[(num11 + num10) - 0x20];
            codes.Add(new KCode(str, new byte[] { num2, num10 }));
            goto Label_02B8;
        Label_026A:
            if (num2 < 0x20)
            {
                goto Label_029E;
            }
            str2 = this.evt[num2 - 0x20];
            codes.Add(new CharCode(str2, new byte[] { num2 }));
            goto Label_02B8;
        Label_029E:;
            codes.Add(new CodeX(new byte[] { num2 }));
        Label_02B8:
            if (num < ((int) bin.Length))
            {
                goto Label_000D;
            }
        Label_02C1:
            return codes;
        }

        public string DecodeSys(byte[] bin)
        {
            string str;
            int num;
            int num2;
            byte num3;
            byte num4;
            str = "";
            num = 0;
            num2 = 0;
            goto Label_00D6;
        Label_000F:
            num3 = bin[num2];
            num4 = num3;
            switch ((num4 - 0x18))
            {
                case 0:
                    goto Label_003A;

                case 1:
                    goto Label_004D;

                case 2:
                    goto Label_0061;

                case 3:
                    goto Label_0075;

                case 4:
                    goto Label_0089;

                case 5:
                    goto Label_009D;
            }
            goto Label_00B1;
        Label_003A:
            num = 0;
            str = str + "∇";
            goto Label_00D2;
        Label_004D:
            num = 0x100;
            str = str + "∇";
            goto Label_00D2;
        Label_0061:
            num = 0x200;
            str = str + "∇";
            goto Label_00D2;
        Label_0075:
            num = 0x300;
            str = str + "∇";
            goto Label_00D2;
        Label_0089:
            num = 0x400;
            str = str + "∇";
            goto Label_00D2;
        Label_009D:
            num = 0x500;
            str = str + "∇";
            goto Label_00D2;
        Label_00B1:
            if (num3 < 0x20)
            {
                goto Label_00D2;
            }
            str = str + this.sys[(num + bin[num2]) - 0x20];
            num = 0;
        Label_00D2:
            num2 += 1;
        Label_00D6:
            if (num2 < ((int) bin.Length))
            {
                goto Label_000F;
            }
            return str;
        }

        private class BUt
        {
            public BUt()
            {
                base..ctor();
                return;
            }

            internal static byte[] Copy(byte[] bin, int start, int count)
            {
                byte[] buffer;
                int num;
                buffer = new byte[count];
                num = 0;
                goto Label_0017;
            Label_000B:
                buffer[num] = bin[start + num];
                num += 1;
            Label_0017:
                if (num < count)
                {
                    goto Label_000B;
                }
                return buffer;
            }
        }

        private class CharCode : StrCode
        {
            public string s;

            public CharCode(string s, byte[] bin)
            {
                base..ctor(bin);
                this.s = s;
                return;
            }

            public override string ToString()
            {
                return this.s;
            }
        }

        private class Code03 : StrCode
        {
            public Code03(byte[] bin)
            {
                base..ctor(bin);
                return;
            }

            public override string ToString()
            {
                return "』";
            }
        }

        private class Code04 : StrCode
        {
            public Code04(byte[] bin)
            {
                base..ctor(bin);
                return;
            }

            public override string ToString()
            {
                return "『";
            }
        }

        private class Code06 : StrCode
        {
            public Code06(byte[] bin)
            {
                base..ctor(bin);
                return;
            }
        }

        private class Code08 : StrCode
        {
            public Code08(byte[] bin)
            {
                base..ctor(bin);
                return;
            }
        }

        private class Code13 : StrCode
        {
            public Code13(byte[] bin)
            {
                base..ctor(bin);
                return;
            }
        }

        private class Code14 : StrCode
        {
            public Code14(byte[] bin)
            {
                base..ctor(bin);
                return;
            }
        }

        private class Code15 : StrCode
        {
            public Code15(byte[] bin)
            {
                base..ctor(bin);
                return;
            }

            public override string ToString()
            {
                return "Select: ";
            }
        }

        private class Code17 : StrCode
        {
            public Code17(byte[] bin)
            {
                base..ctor(bin);
                return;
            }
        }

        private class CodeX : StrCode
        {
            public CodeX(byte[] bin)
            {
                base..ctor(bin);
                return;
            }
        }

        private class EndCode : StrCode
        {
            public EndCode(byte[] bin)
            {
                base..ctor(bin);
                return;
            }

            public override string ToString()
            {
                return "";
            }
        }

        private class KCode : StrCode
        {
            public string s;

            public KCode(string s, byte[] bin)
            {
                base..ctor(bin);
                this.s = s;
                return;
            }

            public override string ToString()
            {
                return this.s;
            }
        }

        private class VarCode : StrCode
        {
            public VarCode(byte[] bin)
            {
                base..ctor(bin);
                return;
            }

            public override string ToString()
            {
                return "#";
            }
        }

        private class WaitCode : StrCode
        {
            public WaitCode(byte[] bin)
            {
                base..ctor(bin);
                return;
            }

            public override string ToString()
            {
                return "\r\n〆\r\n";
            }
        }
    }
}

