namespace khkh_xldMii.V
{
    using hex04BinTrack;
    using SlimDX;
    using System;
    using System.IO;

    public class SimaVU1e
    {
        public SimaVU1e()
        {
            base..ctor();
            return;
        }

        public static unsafe Body1e Sima(VU1Mem vu1mem, Matrix[] Ma, int tops, int top2, int tsel, int[] alaxi, Matrix Mv)
        {
            MemoryStream stream;
            BinaryReader reader;
            int num;
            int num2;
            int num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int[] numArray;
            int num10;
            Body1e bodye;
            MJ1[] mjArray;
            int num11;
            int num12;
            int num13;
            int num14;
            float num15;
            float num16;
            float num17;
            float num18;
            Vector4 vector;
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
            MJ1[][] mjArray2;
            int num30;
            int num31;
            int num32;
            int num33;
            int num34;
            int num35;
            int num36;
            int num37;
            int num38;
            int num39;
            int num40;
            int num41;
            int num42;
            int num43;
            int num44;
            int num45;
            int num46;
            int num47;
            int num48;
            int num49;
            int num50;
            MJ1[] mjArray3;
            MJ1 mj;
            int num51;
            MJ1[] mjArray4;
            stream = new MemoryStream(vu1mem.vumem, 1);
            reader = new BinaryReader(stream);
            stream.Position = (long) (0x10 * tops);
            num = reader.ReadInt32();
            if ((num == 1) || (num == 2))
            {
                goto Label_0034;
            }
            throw new ProtInvalidTypeException();
        Label_0034:
            reader.ReadInt32();
            reader.ReadInt32();
            reader.ReadInt32();
            num2 = reader.ReadInt32();
            num3 = reader.ReadInt32();
            num4 = reader.ReadInt32();
            reader.ReadInt32();
            if (num != 1)
            {
                goto Label_0072;
            }
            reader.ReadInt32();
        Label_0072:
            if (num != 1)
            {
                goto Label_007D;
            }
            reader.ReadInt32();
        Label_007D:
            num5 = (num == 1) ? reader.ReadInt32() : 0;
            num6 = (num == 1) ? reader.ReadInt32() : 0;
            num7 = reader.ReadInt32();
            num8 = reader.ReadInt32();
            reader.ReadInt32();
            num9 = reader.ReadInt32();
            stream.Position = (long) (0x10 * (tops + num4));
            numArray = new int[num9];
            num10 = 0;
            goto Label_00E7;
        Label_00D6:
            numArray[num10] = reader.ReadInt32();
            num10 += 1;
        Label_00E7:
            if (num10 < ((int) numArray.Length))
            {
                goto Label_00D6;
            }
            bodye = new Body1e();
            bodye.t = tsel;
            bodye.alvertraw = new Vector4[num7];
            bodye.avail = (num5 != null) ? 0 : (num == 1);
            bodye.alalni = new MJ1[num7][];
            mjArray = new MJ1[num7];
            num11 = 0;
            stream.Position = (long) (0x10 * (tops + num8));
            num12 = 0;
            goto Label_01F4;
        Label_014F:
            num13 = numArray[num12];
            num14 = 0;
            goto Label_01E5;
        Label_015E:
            num15 = reader.ReadSingle();
            num16 = reader.ReadSingle();
            num17 = reader.ReadSingle();
            num18 = reader.ReadSingle();
            &vector = new Vector4(num15, num16, num17, num18);
            *(&(bodye.alvertraw[num11])) = Vector4.Transform(vector, Mv);
            mjArray3 = new MJ1[1];
            mjArray[num11] = mj = new MJ1(alaxi[num12], num11, num18);
            mjArray3[0] = mj;
            bodye.alalni[num11] = mjArray3;
            num14 += 1;
            num11 += 1;
        Label_01E5:
            if (num14 < num13)
            {
                goto Label_015E;
            }
            num12 += 1;
        Label_01F4:
            if (num12 < ((int) numArray.Length))
            {
                goto Label_014F;
            }
            bodye.aluv = new Vector2[num2];
            bodye.alvi = new int[num2];
            bodye.alfl = new int[num2];
            num19 = 0x7fffffff;
            num20 = -2147483648;
            stream.Position = (long) (0x10 * (tops + num3));
            num21 = 0;
            goto Label_02E9;
        Label_024A:
            num22 = reader.ReadUInt16() / 0x10;
            reader.ReadUInt16();
            num23 = reader.ReadUInt16() / 0x10;
            reader.ReadUInt16();
            *(&(bodye.aluv[num21])) = new Vector2(((float) num22) / 256f, ((float) num23) / 256f);
            bodye.alvi[num21] = num51 = reader.ReadUInt16();
            num24 = num51;
            reader.ReadUInt16();
            bodye.alfl[num21] = reader.ReadUInt16();
            reader.ReadUInt16();
            num19 = Math.Min(num19, num24);
            num20 = Math.Max(num20, num24);
            num21 += 1;
        Label_02E9:
            if (num21 < num2)
            {
                goto Label_024A;
            }
            if (num5 == null)
            {
                goto Label_0595;
            }
            stream.Position = (long) (0x10 * (tops + num6));
            num25 = reader.ReadInt32();
            num26 = reader.ReadInt32();
            num27 = reader.ReadInt32();
            num28 = reader.ReadInt32();
            num29 = 0;
            if (num5 < 5)
            {
                goto Label_034B;
            }
            num29 = reader.ReadInt32();
            reader.ReadInt32();
            reader.ReadInt32();
            reader.ReadInt32();
        Label_034B:
            mjArray2 = new MJ1[num7][];
            num30 = 0;
            num30 = 0;
            goto Label_0382;
        Label_035C:
            num31 = reader.ReadInt32();
            mjArray2[num30] = new MJ1[] { mjArray[num31] };
            num30 += 1;
        Label_0382:
            if (num30 < num25)
            {
                goto Label_035C;
            }
            if (num5 < 2)
            {
                goto Label_03E9;
            }
            stream.Position = (stream.Position + 15L) & -16L;
            num32 = 0;
            goto Label_03E3;
        Label_03A6:
            num33 = reader.ReadInt32();
            num34 = reader.ReadInt32();
            mjArray2[num30] = new MJ1[] { mjArray[num33], mjArray[num34] };
            num32 += 1;
            num30 += 1;
        Label_03E3:
            if (num32 < num26)
            {
                goto Label_03A6;
            }
        Label_03E9:
            if (num5 < 3)
            {
                goto Label_045B;
            }
            stream.Position = (stream.Position + 15L) & -16L;
            num35 = 0;
            goto Label_0455;
        Label_0407:
            num36 = reader.ReadInt32();
            num37 = reader.ReadInt32();
            num38 = reader.ReadInt32();
            mjArray2[num30] = new MJ1[] { mjArray[num36], mjArray[num37], mjArray[num38] };
            num35 += 1;
            num30 += 1;
        Label_0455:
            if (num35 < num27)
            {
                goto Label_0407;
            }
        Label_045B:
            if (num5 < 4)
            {
                goto Label_04DE;
            }
            stream.Position = (stream.Position + 15L) & -16L;
            num39 = 0;
            goto Label_04D8;
        Label_0479:
            num40 = reader.ReadInt32();
            num41 = reader.ReadInt32();
            num42 = reader.ReadInt32();
            num43 = reader.ReadInt32();
            mjArray2[num30] = new MJ1[] { mjArray[num40], mjArray[num41], mjArray[num42], mjArray[num43] };
            num39 += 1;
            num30 += 1;
        Label_04D8:
            if (num39 < num28)
            {
                goto Label_0479;
            }
        Label_04DE:
            if (num5 < 5)
            {
                goto Label_0575;
            }
            stream.Position = (stream.Position + 15L) & -16L;
            num44 = 0;
            goto Label_056F;
        Label_04FF:
            num45 = reader.ReadInt32();
            num46 = reader.ReadInt32();
            num47 = reader.ReadInt32();
            num48 = reader.ReadInt32();
            num49 = reader.ReadInt32();
            mjArray2[num30] = new MJ1[] { mjArray[num45], mjArray[num46], mjArray[num47], mjArray[num48], mjArray[num49] };
            num44 += 1;
            num30 += 1;
        Label_056F:
            if (num44 < num29)
            {
                goto Label_04FF;
            }
        Label_0575:
            num50 = num19;
            goto Label_0586;
        Label_0580:
            num50 += 1;
        Label_0586:
            if (num50 <= num20)
            {
                goto Label_0580;
            }
            bodye.alalni = mjArray2;
        Label_0595:
            return bodye;
        }
    }
}

