using System.Collections.Generic;
using System.IO;
using SlimDX;

namespace khiiMapv.CollTest
{
    public class CollReader
    {
        public List<Co1> alCo1 = new List<Co1>();
        public List<Co2> alCo2 = new List<Co2>();
        public List<Co3> alCo3 = new List<Co3>();
        public List<Vector4> alCo4 = new List<Vector4>();
        public List<Plane> alCo5 = new List<Plane>();

        public void Read(Stream si)
        {
            var binaryReader = new BinaryReader(si);
            if (binaryReader.ReadByte() != 67)
            {
                throw new InvalidDataException();
            }
            if (binaryReader.ReadByte() != 79)
            {
                throw new InvalidDataException();
            }
            if (binaryReader.ReadByte() != 67)
            {
                throw new InvalidDataException();
            }
            if (binaryReader.ReadByte() != 84)
            {
                throw new InvalidDataException();
            }
            if (binaryReader.ReadInt32() != 1)
            {
                throw new InvalidDataException();
            }
            si.Position = 8L;
            int num = binaryReader.ReadInt32();
            si.Position = 24L;
            int num2 = binaryReader.ReadInt32();
            binaryReader.ReadInt32();
            si.Position = num2;
            for (int i = 0; i < num; i++)
            {
                alCo1.Add(new Co1(binaryReader));
            }
            si.Position = 32L;
            int num3 = binaryReader.ReadInt32();
            int num4 = binaryReader.ReadInt32();
            si.Position = num3;
            while (si.Position < num3 + num4)
            {
                alCo2.Add(new Co2(binaryReader));
            }
            si.Position = 40L;
            int num5 = binaryReader.ReadInt32();
            int num6 = binaryReader.ReadInt32();
            si.Position = num5;
            while (si.Position < num5 + num6)
            {
                alCo3.Add(new Co3(binaryReader));
            }
            si.Position = 48L;
            int num7 = binaryReader.ReadInt32();
            int num8 = binaryReader.ReadInt32();
            si.Position = num7;
            while (si.Position < num7 + num8)
            {
                Vector4 item = default(Vector4);
                item.X = binaryReader.ReadSingle();
                item.Y = binaryReader.ReadSingle();
                item.Z = binaryReader.ReadSingle();
                item.W = binaryReader.ReadSingle();
                alCo4.Add(item);
            }
            si.Position = 56L;
            int num9 = binaryReader.ReadInt32();
            int num10 = binaryReader.ReadInt32();
            si.Position = num9;
            while (si.Position < num9 + num10)
            {
                Plane item2 = default(Plane);
                item2.Normal.X = binaryReader.ReadSingle();
                item2.Normal.Y = binaryReader.ReadSingle();
                item2.Normal.Z = binaryReader.ReadSingle();
                item2.D = binaryReader.ReadSingle();
                alCo5.Add(item2);
            }
        }
    }
}