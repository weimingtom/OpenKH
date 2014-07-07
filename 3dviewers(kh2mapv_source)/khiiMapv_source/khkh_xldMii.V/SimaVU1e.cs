using System;
using System.IO;
using hex04BinTrack;
using SlimDX;

namespace khkh_xldMii.V
{
    public class SimaVU1e
    {
        public static Body1e Sima(VU1Mem vu1mem, Matrix[] Ma, int tops, int top2, int tsel, int[] alaxi, Matrix Mv)
        {
            var memoryStream = new MemoryStream(vu1mem.vumem, true);
            var binaryReader = new BinaryReader(memoryStream);
            memoryStream.Position = 16*tops;
            int num = binaryReader.ReadInt32();
            if (num != 1 && num != 2)
            {
                throw new ProtInvalidTypeException();
            }
            binaryReader.ReadInt32();
            binaryReader.ReadInt32();
            binaryReader.ReadInt32();
            int num2 = binaryReader.ReadInt32();
            int num3 = binaryReader.ReadInt32();
            int num4 = binaryReader.ReadInt32();
            binaryReader.ReadInt32();
            if (num == 1)
            {
                binaryReader.ReadInt32();
            }
            if (num == 1)
            {
                binaryReader.ReadInt32();
            }
            int num5 = (num == 1) ? binaryReader.ReadInt32() : 0;
            int num6 = (num == 1) ? binaryReader.ReadInt32() : 0;
            int num7 = binaryReader.ReadInt32();
            int num8 = binaryReader.ReadInt32();
            binaryReader.ReadInt32();
            int num9 = binaryReader.ReadInt32();
            memoryStream.Position = 16*(tops + num4);
            var array = new int[num9];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = binaryReader.ReadInt32();
            }
            var body1e = new Body1e();
            body1e.t = tsel;
            body1e.alvertraw = new Vector4[num7];
            body1e.avail = (num5 == 0 && num == 1);
            body1e.alalni = new MJ1[num7][];
            var array2 = new MJ1[num7];
            int num10 = 0;
            memoryStream.Position = 16*(tops + num8);
            for (int j = 0; j < array.Length; j++)
            {
                int num11 = array[j];
                int k = 0;
                while (k < num11)
                {
                    float x = binaryReader.ReadSingle();
                    float y = binaryReader.ReadSingle();
                    float z = binaryReader.ReadSingle();
                    float num12 = binaryReader.ReadSingle();
                    var vector = new Vector4(x, y, z, num12);
                    body1e.alvertraw[num10] = Vector4.Transform(vector, Mv);
                    body1e.alalni[num10] = new[]
                    {
                        array2[num10] = new MJ1(alaxi[j], num10, num12)
                    };
                    k++;
                    num10++;
                }
            }
            body1e.aluv = new Vector2[num2];
            body1e.alvi = new int[num2];
            body1e.alfl = new int[num2];
            int num13 = 2147483647;
            int num14 = -2147483648;
            memoryStream.Position = 16*(tops + num3);
            for (int l = 0; l < num2; l++)
            {
                int num15 = binaryReader.ReadUInt16()/16;
                binaryReader.ReadUInt16();
                int num16 = binaryReader.ReadUInt16()/16;
                binaryReader.ReadUInt16();
                body1e.aluv[l] = new Vector2(num15/256f, num16/256f);
                int val = body1e.alvi[l] = binaryReader.ReadUInt16();
                binaryReader.ReadUInt16();
                body1e.alfl[l] = binaryReader.ReadUInt16();
                binaryReader.ReadUInt16();
                num13 = Math.Min(num13, val);
                num14 = Math.Max(num14, val);
            }
            if (num5 != 0)
            {
                memoryStream.Position = 16*(tops + num6);
                int num17 = binaryReader.ReadInt32();
                int num18 = binaryReader.ReadInt32();
                int num19 = binaryReader.ReadInt32();
                int num20 = binaryReader.ReadInt32();
                int num21 = 0;
                if (num5 >= 5)
                {
                    num21 = binaryReader.ReadInt32();
                    binaryReader.ReadInt32();
                    binaryReader.ReadInt32();
                    binaryReader.ReadInt32();
                }
                var array3 = new MJ1[num7][];
                int m;
                for (m = 0; m < num17; m++)
                {
                    int num22 = binaryReader.ReadInt32();
                    array3[m] = new[]
                    {
                        array2[num22]
                    };
                }
                if (num5 >= 2)
                {
                    memoryStream.Position = (memoryStream.Position + 15L & -16L);
                    int n = 0;
                    while (n < num18)
                    {
                        int num23 = binaryReader.ReadInt32();
                        int num24 = binaryReader.ReadInt32();
                        array3[m] = new[]
                        {
                            array2[num23],
                            array2[num24]
                        };
                        n++;
                        m++;
                    }
                }
                if (num5 >= 3)
                {
                    memoryStream.Position = (memoryStream.Position + 15L & -16L);
                    int num25 = 0;
                    while (num25 < num19)
                    {
                        int num26 = binaryReader.ReadInt32();
                        int num27 = binaryReader.ReadInt32();
                        int num28 = binaryReader.ReadInt32();
                        array3[m] = new[]
                        {
                            array2[num26],
                            array2[num27],
                            array2[num28]
                        };
                        num25++;
                        m++;
                    }
                }
                if (num5 >= 4)
                {
                    memoryStream.Position = (memoryStream.Position + 15L & -16L);
                    int num29 = 0;
                    while (num29 < num20)
                    {
                        int num30 = binaryReader.ReadInt32();
                        int num31 = binaryReader.ReadInt32();
                        int num32 = binaryReader.ReadInt32();
                        int num33 = binaryReader.ReadInt32();
                        array3[m] = new[]
                        {
                            array2[num30],
                            array2[num31],
                            array2[num32],
                            array2[num33]
                        };
                        num29++;
                        m++;
                    }
                }
                if (num5 >= 5)
                {
                    memoryStream.Position = (memoryStream.Position + 15L & -16L);
                    int num34 = 0;
                    while (num34 < num21)
                    {
                        int num35 = binaryReader.ReadInt32();
                        int num36 = binaryReader.ReadInt32();
                        int num37 = binaryReader.ReadInt32();
                        int num38 = binaryReader.ReadInt32();
                        int num39 = binaryReader.ReadInt32();
                        array3[m] = new[]
                        {
                            array2[num35],
                            array2[num36],
                            array2[num37],
                            array2[num38],
                            array2[num39]
                        };
                        num34++;
                        m++;
                    }
                }
                for (int num40 = num13; num40 <= num14; num40++)
                {
                }
                body1e.alalni = array3;
            }
            return body1e;
        }
    }
}