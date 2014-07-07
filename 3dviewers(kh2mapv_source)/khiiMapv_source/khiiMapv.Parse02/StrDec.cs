using System.Collections.Generic;
using khiiMapv.Properties;

namespace khiiMapv.Parse02
{
    public class StrDec
    {
        public SortedDictionary<int, string> evt = new SortedDictionary<int, string>();
        public SortedDictionary<int, string> sys = new SortedDictionary<int, string>();

        public StrDec()
        {
            string[] array =
            {
                Resources.EVTjp0,
                Resources.EVTjp1,
                Resources.EVTjp2,
                Resources.EVTjp3
            };
            for (int i = 0; i < 4; i++)
            {
                string[] array2 = array[i].Replace("\r\n", "\n").Split(new[]
                {
                    '\n'
                });
                int num = 0;
                string[] array3 = array2;
                for (int j = 0; j < array3.Length; j++)
                {
                    string text = array3[j];
                    string[] array4 = text.Split(new[]
                    {
                        '\t'
                    });
                    int num2 = 0;
                    string[] array5 = array4;
                    for (int k = 0; k < array5.Length; k++)
                    {
                        string value = array5[k];
                        evt[i*21*21 + num*21 + num2] = value;
                        num2++;
                    }
                    num++;
                }
            }
            string[] array6 = Resources.SYSjp.Replace("\r\n", "\n").Split(new[]
            {
                '\n'
            });
            int num3 = 0;
            string[] array7 = array6;
            for (int l = 0; l < array7.Length; l++)
            {
                string text2 = array7[l];
                string[] array8 = text2.Split(new[]
                {
                    '\t'
                });
                int num4 = 0;
                string[] array9 = array8;
                for (int m = 0; m < array9.Length; m++)
                {
                    string value2 = array9[m];
                    sys[28*num3 + num4] = value2;
                    num4++;
                }
                num3++;
            }
        }

        public string DecodeSys(byte[] bin)
        {
            string text = "";
            int num = 0;
            for (int i = 0; i < bin.Length; i++)
            {
                byte b = bin[i];
                switch (b)
                {
                    case 24:
                        num = 0;
                        text += "∇";
                        break;
                    case 25:
                        num = 256;
                        text += "∇";
                        break;
                    case 26:
                        num = 512;
                        text += "∇";
                        break;
                    case 27:
                        num = 768;
                        text += "∇";
                        break;
                    case 28:
                        num = 1024;
                        text += "∇";
                        break;
                    case 29:
                        num = 1280;
                        text += "∇";
                        break;
                    default:
                        if (b >= 32)
                        {
                            text += sys[num + bin[i] - 32];
                            num = 0;
                        }
                        break;
                }
            }
            return text;
        }

        public StrCodeCollection DecodeEvt(byte[] bin, int start)
        {
            var strCodeCollection = new StrCodeCollection();
            int i = start;
            while (i < bin.Length)
            {
                byte b = bin[i];
                i++;
                if (b == 0)
                {
                    strCodeCollection.Add(new EndCode(new[]
                    {
                        b
                    }));
                    break;
                }
                if (b == 1)
                {
                    strCodeCollection.Add(new CharCode(" ", new[]
                    {
                        b
                    }));
                }
                else
                {
                    if (b == 2)
                    {
                        strCodeCollection.Add(new CharCode("\r\n", new[]
                        {
                            b
                        }));
                    }
                    else
                    {
                        if (b == 3)
                        {
                            strCodeCollection.Add(new Code03(new[]
                            {
                                b
                            }));
                        }
                        else
                        {
                            if (b == 4)
                            {
                                int num = 1;
                                strCodeCollection.Add(new Code04(BUt.Copy(bin, i - 1, num + 1)));
                                i += num;
                            }
                            else
                            {
                                if (b == 6)
                                {
                                    int num2 = 5;
                                    strCodeCollection.Add(new Code06(BUt.Copy(bin, i - 1, num2 + 1)));
                                    i += num2;
                                }
                                else
                                {
                                    if (b == 8)
                                    {
                                        int num3 = 3;
                                        strCodeCollection.Add(new Code08(BUt.Copy(bin, i - 1, num3 + 1)));
                                        i += num3;
                                    }
                                    else
                                    {
                                        if (b == 13)
                                        {
                                            strCodeCollection.Add(new VarCode(new[]
                                            {
                                                b
                                            }));
                                        }
                                        else
                                        {
                                            if (b == 16)
                                            {
                                                strCodeCollection.Add(new WaitCode(new[]
                                                {
                                                    b
                                                }));
                                            }
                                            else
                                            {
                                                if (b == 19)
                                                {
                                                    int num4 = 4;
                                                    strCodeCollection.Add(new Code13(BUt.Copy(bin, i - 1, num4 + 1)));
                                                    i += num4;
                                                }
                                                else
                                                {
                                                    if (b == 20)
                                                    {
                                                        int num5 = 2;
                                                        strCodeCollection.Add(new Code14(BUt.Copy(bin, i - 1, num5 + 1)));
                                                        i += num5;
                                                    }
                                                    else
                                                    {
                                                        if (b == 21)
                                                        {
                                                            int num6 = 2;
                                                            strCodeCollection.Add(
                                                                new Code15(BUt.Copy(bin, i - 1, num6 + 1)));
                                                            i += num6;
                                                        }
                                                        else
                                                        {
                                                            if (b == 23)
                                                            {
                                                                int num7 = 3;
                                                                strCodeCollection.Add(
                                                                    new Code17(BUt.Copy(bin, i - 1, num7 + 1)));
                                                                i += num7;
                                                            }
                                                            else
                                                            {
                                                                if (b >= 24 && 30 >= b)
                                                                {
                                                                    byte b2 = bin[i];
                                                                    i++;
                                                                    int num8 = 256*(b - 24);
                                                                    string s = evt[num8 + b2 - 32];
                                                                    strCodeCollection.Add(new KCode(s, new[]
                                                                    {
                                                                        b,
                                                                        b2
                                                                    }));
                                                                }
                                                                else
                                                                {
                                                                    if (b >= 32)
                                                                    {
                                                                        string s2 = evt[b - 32];
                                                                        strCodeCollection.Add(new CharCode(s2, new[]
                                                                        {
                                                                            b
                                                                        }));
                                                                    }
                                                                    else
                                                                    {
                                                                        strCodeCollection.Add(new CodeX(new[]
                                                                        {
                                                                            b
                                                                        }));
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return strCodeCollection;
        }

        private class BUt
        {
            internal static byte[] Copy(byte[] bin, int start, int count)
            {
                var array = new byte[count];
                for (int i = 0; i < count; i++)
                {
                    array[i] = bin[start + i];
                }
                return array;
            }
        }

        private class CharCode : StrCode
        {
            public string s;

            public CharCode(string s, byte[] bin) : base(bin)
            {
                this.s = s;
            }

            public override string ToString()
            {
                return s;
            }
        }

        private class Code03 : StrCode
        {
            public Code03(byte[] bin) : base(bin)
            {
            }

            public override string ToString()
            {
                return "』";
            }
        }

        private class Code04 : StrCode
        {
            public Code04(byte[] bin) : base(bin)
            {
            }

            public override string ToString()
            {
                return "『";
            }
        }

        private class Code06 : StrCode
        {
            public Code06(byte[] bin) : base(bin)
            {
            }
        }

        private class Code08 : StrCode
        {
            public Code08(byte[] bin) : base(bin)
            {
            }
        }

        private class Code13 : StrCode
        {
            public Code13(byte[] bin) : base(bin)
            {
            }
        }

        private class Code14 : StrCode
        {
            public Code14(byte[] bin) : base(bin)
            {
            }
        }

        private class Code15 : StrCode
        {
            public Code15(byte[] bin) : base(bin)
            {
            }

            public override string ToString()
            {
                return "Select: ";
            }
        }

        private class Code17 : StrCode
        {
            public Code17(byte[] bin) : base(bin)
            {
            }
        }

        private class CodeX : StrCode
        {
            public CodeX(byte[] bin) : base(bin)
            {
            }
        }

        private class EndCode : StrCode
        {
            public EndCode(byte[] bin) : base(bin)
            {
            }

            public override string ToString()
            {
                return "";
            }
        }

        private class KCode : StrCode
        {
            public string s;

            public KCode(string s, byte[] bin) : base(bin)
            {
                this.s = s;
            }

            public override string ToString()
            {
                return s;
            }
        }

        private class VarCode : StrCode
        {
            public VarCode(byte[] bin) : base(bin)
            {
            }

            public override string ToString()
            {
                return "#";
            }
        }

        private class WaitCode : StrCode
        {
            public WaitCode(byte[] bin) : base(bin)
            {
            }

            public override string ToString()
            {
                return "\r\n〆\r\n";
            }
        }
    }
}