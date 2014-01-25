namespace khkh_xldMii
{
    using SlimDX;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class MsetRawblk
    {
        public List<MsetRM> alrm = new List<MsetRM>();
        public int cntJoints;

        public MsetRawblk(Stream si)
        {
            BinaryReader reader = new BinaryReader(si);
            si.Position = 0x90L;
            if (reader.ReadInt32() != 1)
            {
                throw new NotSupportedException("v90 != 1");
            }
            si.Position = 160L;
            int num2 = this.cntJoints = reader.ReadInt32();
            si.Position = 180L;
            int num3 = reader.ReadInt32();
            si.Position = 240L;
            this.alrm.Capacity = num3;
            for (int i = 0; i < num3; i++)
            {
                MsetRM item = new MsetRM {
                    al = { Capacity = num2 }
                };
                this.alrm.Add(item);
                for (int j = 0; j < num2; j++)
                {
                    Matrix matrix = new Matrix {
                        M11 = reader.ReadSingle(),
                        M12 = reader.ReadSingle(),
                        M13 = reader.ReadSingle(),
                        M14 = reader.ReadSingle(),
                        M21 = reader.ReadSingle(),
                        M22 = reader.ReadSingle(),
                        M23 = reader.ReadSingle(),
                        M24 = reader.ReadSingle(),
                        M31 = reader.ReadSingle(),
                        M32 = reader.ReadSingle(),
                        M33 = reader.ReadSingle(),
                        M34 = reader.ReadSingle(),
                        M41 = reader.ReadSingle(),
                        M42 = reader.ReadSingle(),
                        M43 = reader.ReadSingle(),
                        M44 = reader.ReadSingle()
                    };
                    item.al.Add(matrix);
                }
            }
        }

        public int cntFrames
        {
            get
            {
                return this.alrm.Count;
            }
        }
    }
}

