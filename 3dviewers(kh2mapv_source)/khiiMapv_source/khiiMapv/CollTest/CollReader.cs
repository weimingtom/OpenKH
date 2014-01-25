namespace khiiMapv.CollTest
{
    using SlimDX;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class CollReader
    {
        public List<Co1> alCo1;
        public List<Co2> alCo2;
        public List<Co3> alCo3;
        public List<Vector4> alCo4;
        public List<Plane> alCo5;

        public CollReader()
        {
            this.alCo1 = new List<Co1>();
            this.alCo2 = new List<Co2>();
            this.alCo3 = new List<Co3>();
            this.alCo4 = new List<Vector4>();
            this.alCo5 = new List<Plane>();
            base..ctor();
            return;
        }

        public unsafe void Read(Stream si)
        {
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
            Vector4 vector;
            int num10;
            int num11;
            Plane plane;
            reader = new BinaryReader(si);
            if (reader.ReadByte() == 0x43)
            {
                goto Label_0017;
            }
            throw new InvalidDataException();
        Label_0017:
            if (reader.ReadByte() == 0x4f)
            {
                goto Label_0027;
            }
            throw new InvalidDataException();
        Label_0027:
            if (reader.ReadByte() == 0x43)
            {
                goto Label_0037;
            }
            throw new InvalidDataException();
        Label_0037:
            if (reader.ReadByte() == 0x54)
            {
                goto Label_0047;
            }
            throw new InvalidDataException();
        Label_0047:
            if (reader.ReadInt32() == 1)
            {
                goto Label_0056;
            }
            throw new InvalidDataException();
        Label_0056:
            si.Position = 8L;
            num = reader.ReadInt32();
            si.Position = 0x18L;
            num2 = reader.ReadInt32();
            reader.ReadInt32();
            si.Position = (long) num2;
            num3 = 0;
            goto Label_009D;
        Label_0088:
            this.alCo1.Add(new Co1(reader));
            num3 += 1;
        Label_009D:
            if (num3 < num)
            {
                goto Label_0088;
            }
            si.Position = 0x20L;
            num4 = reader.ReadInt32();
            num5 = reader.ReadInt32();
            si.Position = (long) num4;
            goto Label_00D6;
        Label_00C5:
            this.alCo2.Add(new Co2(reader));
        Label_00D6:
            if (si.Position < ((long) (num4 + num5)))
            {
                goto Label_00C5;
            }
            si.Position = 40L;
            num6 = reader.ReadInt32();
            num7 = reader.ReadInt32();
            si.Position = (long) num6;
            goto Label_0119;
        Label_0108:
            this.alCo3.Add(new Co3(reader));
        Label_0119:
            if (si.Position < ((long) (num6 + num7)))
            {
                goto Label_0108;
            }
            si.Position = 0x30L;
            num8 = reader.ReadInt32();
            num9 = reader.ReadInt32();
            si.Position = (long) num8;
            goto Label_0194;
        Label_014B:
            vector = new Vector4();
            &vector.X = reader.ReadSingle();
            &vector.Y = reader.ReadSingle();
            &vector.Z = reader.ReadSingle();
            &vector.W = reader.ReadSingle();
            this.alCo4.Add(vector);
        Label_0194:
            if (si.Position < ((long) (num8 + num9)))
            {
                goto Label_014B;
            }
            si.Position = 0x38L;
            num10 = reader.ReadInt32();
            num11 = reader.ReadInt32();
            si.Position = (long) num10;
            goto Label_021E;
        Label_01C6:
            plane = new Plane();
            &&plane.Normal.X = reader.ReadSingle();
            &&plane.Normal.Y = reader.ReadSingle();
            &&plane.Normal.Z = reader.ReadSingle();
            &plane.D = reader.ReadSingle();
            this.alCo5.Add(plane);
        Label_021E:
            if (si.Position < ((long) (num10 + num11)))
            {
                goto Label_01C6;
            }
            return;
        }
    }
}

