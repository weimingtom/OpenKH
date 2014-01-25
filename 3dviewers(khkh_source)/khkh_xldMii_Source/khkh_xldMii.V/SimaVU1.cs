namespace khkh_xldMii.V
{
    using hex04BinTrack;
    using SlimDX;
    using System;
    using System.IO;

    public class SimaVU1
    {
        public static Body1 Sima(VU1Mem vu1mem, Matrix[] Ma, int tops, int top2, int tsel, int[] alaxi, Matrix Mv)
        {
            MemoryStream input = new MemoryStream(vu1mem.vumem, true);
            BinaryReader reader = new BinaryReader(input);
            input.Position = 0x10 * tops;
            int num = reader.ReadInt32();
            if ((num != 1) && (num != 2))
            {
                throw new ProtInvalidTypeException();
            }
            reader.ReadInt32();
            reader.ReadInt32();
            reader.ReadInt32();
            int num2 = reader.ReadInt32();
            int num3 = reader.ReadInt32();
            int num4 = reader.ReadInt32();
            reader.ReadInt32();
            if (num == 1)
            {
                reader.ReadInt32();
            }
            if (num == 1)
            {
                reader.ReadInt32();
            }
            int num5 = (num == 1) ? reader.ReadInt32() : 0;
            int num6 = (num == 1) ? reader.ReadInt32() : 0;
            int num7 = reader.ReadInt32();
            int num8 = reader.ReadInt32();
            reader.ReadInt32();
            int num9 = reader.ReadInt32();
            input.Position = 0x10 * (tops + num4);
            int[] numArray = new int[num9];
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = reader.ReadInt32();
            }
            Body1 body = new Body1 {
                t = tsel,
                alvert = new Vector3[num7],
                avail = (num5 == 0) && (num == 1)
            };
            Vector3[] vectorArray = new Vector3[num7];
            int index = 0;
            input.Position = 0x10 * (tops + num8);
            for (int j = 0; j < numArray.Length; j++)
            {
                Matrix transformation = Ma[alaxi[j]] * Mv;
                int num13 = numArray[j];
                int num14 = 0;
                while (num14 < num13)
                {
                    float x = reader.ReadSingle();
                    float y = reader.ReadSingle();
                    float z = reader.ReadSingle();
                    float w = reader.ReadSingle();
                    Vector3 coordinate = new Vector3(x, y, z);
                    body.alvert[index] = Vector3.TransformCoordinate(coordinate, transformation);
                    Vector4 vector = new Vector4(x, y, z, w);
                    Vector4 vector4 = Vector4.Transform(vector, transformation);
                    vectorArray[index] = new Vector3(vector4.X, vector4.Y, vector4.Z);
                    num14++;
                    index++;
                }
            }
            body.aluv = new Vector2[num2];
            body.alvi = new int[num2];
            body.alfl = new int[num2];
            input.Position = 0x10 * (tops + num3);
            for (int k = 0; k < num2; k++)
            {
                int num20 = reader.ReadUInt16() / 0x10;
                reader.ReadUInt16();
                int num21 = reader.ReadUInt16() / 0x10;
                reader.ReadUInt16();
                body.aluv[k] = new Vector2(((float) num20) / 256f, ((float) num21) / 256f);
                body.alvi[k] = reader.ReadUInt16();
                reader.ReadUInt16();
                body.alfl[k] = reader.ReadUInt16();
                reader.ReadUInt16();
            }
            if (num5 != 0)
            {
                input.Position = 0x10 * (tops + num6);
                int num22 = reader.ReadInt32();
                int num23 = reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadInt32();
                Vector3[] vectorArray2 = new Vector3[num7];
                int num24 = 0;
                num24 = 0;
                while (num24 < num22)
                {
                    int num25 = reader.ReadInt32();
                    vectorArray2[num24] = body.alvert[num25];
                    num24++;
                }
                if (num5 >= 2)
                {
                    input.Position = (input.Position + 15L) & -16L;
                    int num26 = 0;
                    while (num26 < num23)
                    {
                        int num27 = reader.ReadInt32();
                        int num28 = reader.ReadInt32();
                        vectorArray2[num24] = vectorArray[num27] + vectorArray[num28];
                        num26++;
                        num24++;
                    }
                }
                body.alvert = vectorArray2;
            }
            return body;
        }
    }
}

