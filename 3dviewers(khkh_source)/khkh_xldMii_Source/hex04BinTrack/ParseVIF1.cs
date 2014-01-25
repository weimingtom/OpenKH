namespace hex04BinTrack
{
    using System;
    using System.IO;

    public class ParseVIF1
    {
        private VU1Mem vu1;

        public ParseVIF1(VU1Mem vu1)
        {
            this.vu1 = vu1;
        }

        public void Parse(MemoryStream si)
        {
            this.Parse(si, 0);
        }

        public void Parse(MemoryStream si, int tops)
        {
            MemoryStream output = new MemoryStream(this.vu1.vumem, true);
            BinaryWriter writer = new BinaryWriter(output);
            byte[] buffer = new byte[0x10];
            uint[] numArray = new uint[4];
            uint[] numArray2 = new uint[4];
            BinaryReader br = new BinaryReader(si);
            while (si.Position < si.Length)
            {
                long position = si.Position;
                uint num = br.ReadUInt32();
                int num2 = ((int) (num >> 0x18)) & 0x7f;
                switch (num2)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 0x10:
                    case 0x11:
                    case 0x13:
                    case 20:
                    case 0x15:
                    case 0x17:
                    case 0x4a:
                    {
                        continue;
                    }
                    case 0x20:
                    {
                        uint num3 = br.ReadUInt32();
                        for (int i = 0; i < 0x10; i++)
                        {
                            buffer[i] = (byte) ((num3 >> (2 * i)) & 3);
                        }
                        continue;
                    }
                    case 0x30:
                    {
                        numArray2[0] = br.ReadUInt32();
                        numArray2[1] = br.ReadUInt32();
                        numArray2[2] = br.ReadUInt32();
                        numArray2[3] = br.ReadUInt32();
                        continue;
                    }
                    case 0x31:
                    {
                        numArray[0] = br.ReadUInt32();
                        numArray[1] = br.ReadUInt32();
                        numArray[2] = br.ReadUInt32();
                        numArray[3] = br.ReadUInt32();
                        continue;
                    }
                    case 80:
                    {
                        int num5 = ((int) num) & 0xffff;
                        si.Position = (si.Position + 15L) & -16L;
                        si.Position += 0x10 * num5;
                        continue;
                    }
                    case 0x51:
                    {
                        int num6 = ((int) num) & 0xffff;
                        si.Position = (si.Position + 15L) & -16L;
                        si.Position += 0x10 * num6;
                        continue;
                    }
                }
                if (0x60 == (num2 & 0x60))
                {
                    int num7 = (num2 >> 4) & 1;
                    int num8 = (num2 >> 2) & 3;
                    int vl = num2 & 3;
                    int num10 = ((int) (num >> 0x10)) & 0xff;
                    int usn = ((int) (num >> 14)) & 1;
                    int num12 = ((int) num) & 0x3ff;
                    int num13 = 0;
                    int num14 = 1;
                    switch (vl)
                    {
                        case 0:
                            num13 = 4;
                            break;

                        case 1:
                            num13 = 2;
                            break;

                        case 2:
                            num13 = 1;
                            break;

                        case 3:
                            num13 = 2;
                            break;
                    }
                    switch (num8)
                    {
                        case 0:
                            num14 = 1;
                            break;

                        case 1:
                            num14 = 2;
                            break;

                        case 2:
                            num14 = 3;
                            break;

                        case 3:
                            num14 = 4;
                            break;
                    }
                    int num15 = (((num13 * num14) * num10) + 3) & -4;
                    long num16 = si.Position + num15;
                    output.Position = 0x10 * (tops + num12);
                    bool flag = num7 == 0;
                    Reader reader2 = new Reader(br, vl, usn);
                    for (int j = 0; j < num10; j++)
                    {
                        uint num18;
                        switch (num8)
                        {
                            case 1:
                                num18 = reader2.Read();
                                switch ((buffer[(j & 3) * 4] & (flag ? 0 : 7)))
                                {
                                    case 1:
                                        goto Label_04E7;

                                    case 2:
                                        goto Label_04F6;

                                    case 3:
                                        goto Label_0501;
                                }
                                goto Label_0510;

                            case 2:
                                num18 = reader2.Read();
                                switch ((buffer[(j & 3) * 4] & (flag ? 0 : 7)))
                                {
                                    case 0:
                                        goto Label_05D2;

                                    case 1:
                                        goto Label_05DC;

                                    case 2:
                                        goto Label_05EB;

                                    case 3:
                                        goto Label_05F6;
                                }
                                goto Label_0605;

                            case 3:
                                num18 = reader2.Read();
                                switch ((buffer[(j & 3) * 4] & (flag ? 0 : 7)))
                                {
                                    case 0:
                                        goto Label_0722;

                                    case 1:
                                        goto Label_072C;

                                    case 2:
                                        goto Label_073B;

                                    case 3:
                                        goto Label_0746;
                                }
                                goto Label_0755;

                            default:
                                num18 = reader2.Read();
                                switch ((buffer[(j & 3) * 4] & (flag ? 0 : 7)))
                                {
                                    case 0:
                                        writer.Write(num18);
                                        break;

                                    case 1:
                                        writer.Write(numArray2[j & 3]);
                                        break;

                                    case 2:
                                        writer.Write(numArray[0]);
                                        break;

                                    case 3:
                                        output.Position += 4L;
                                        break;
                                }
                                switch ((buffer[((j & 3) * 4) + 1] & (flag ? 0 : 7)))
                                {
                                    case 0:
                                        writer.Write(num18);
                                        break;

                                    case 1:
                                        writer.Write(numArray2[j & 3]);
                                        break;

                                    case 2:
                                        writer.Write(numArray[1]);
                                        break;

                                    case 3:
                                        output.Position += 4L;
                                        break;
                                }
                                switch ((buffer[((j & 3) * 4) + 2] & (flag ? 0 : 7)))
                                {
                                    case 0:
                                        writer.Write(num18);
                                        break;

                                    case 1:
                                        writer.Write(numArray2[j & 3]);
                                        break;

                                    case 2:
                                        writer.Write(numArray[2]);
                                        break;

                                    case 3:
                                        output.Position += 4L;
                                        break;
                                }
                                switch ((buffer[((j & 3) * 4) + 3] & (flag ? 0 : 7)))
                                {
                                    case 0:
                                    {
                                        writer.Write(num18);
                                        continue;
                                    }
                                    case 1:
                                    {
                                        writer.Write(numArray2[j & 3]);
                                        continue;
                                    }
                                    case 2:
                                    {
                                        writer.Write(numArray[3]);
                                        continue;
                                    }
                                    case 3:
                                    {
                                        output.Position += 4L;
                                        continue;
                                    }
                                    default:
                                    {
                                        continue;
                                    }
                                }
                                break;
                        }
                        writer.Write(num18);
                        goto Label_0510;
                    Label_04E7:
                        writer.Write(numArray2[j & 3]);
                        goto Label_0510;
                    Label_04F6:
                        writer.Write(numArray[0]);
                        goto Label_0510;
                    Label_0501:
                        output.Position += 4L;
                    Label_0510:
                        num18 = reader2.Read();
                        switch ((buffer[((j & 3) * 4) + 1] & (flag ? 0 : 7)))
                        {
                            case 0:
                                writer.Write(num18);
                                break;

                            case 1:
                                writer.Write(numArray2[j & 3]);
                                break;

                            case 2:
                                writer.Write(numArray[1]);
                                break;

                            case 3:
                                output.Position += 4L;
                                break;
                        }
                        output.Position += 4L;
                        output.Position += 4L;
                        continue;
                    Label_05D2:
                        writer.Write(num18);
                        goto Label_0605;
                    Label_05DC:
                        writer.Write(numArray2[j & 3]);
                        goto Label_0605;
                    Label_05EB:
                        writer.Write(numArray[0]);
                        goto Label_0605;
                    Label_05F6:
                        output.Position += 4L;
                    Label_0605:
                        num18 = reader2.Read();
                        switch ((buffer[((j & 3) * 4) + 1] & (flag ? 0 : 7)))
                        {
                            case 0:
                                writer.Write(num18);
                                break;

                            case 1:
                                writer.Write(numArray2[j & 3]);
                                break;

                            case 2:
                                writer.Write(numArray[1]);
                                break;

                            case 3:
                                output.Position += 4L;
                                break;
                        }
                        num18 = reader2.Read();
                        switch ((buffer[((j & 3) * 4) + 2] & (flag ? 0 : 7)))
                        {
                            case 0:
                                writer.Write(num18);
                                break;

                            case 1:
                                writer.Write(numArray2[j & 3]);
                                break;

                            case 2:
                                writer.Write(numArray[2]);
                                break;

                            case 3:
                                output.Position += 4L;
                                break;
                        }
                        output.Position += 4L;
                        continue;
                    Label_0722:
                        writer.Write(num18);
                        goto Label_0755;
                    Label_072C:
                        writer.Write(numArray2[j & 3]);
                        goto Label_0755;
                    Label_073B:
                        writer.Write(numArray[0]);
                        goto Label_0755;
                    Label_0746:
                        output.Position += 4L;
                    Label_0755:
                        num18 = reader2.Read();
                        switch ((buffer[((j & 3) * 4) + 1] & (flag ? 0 : 7)))
                        {
                            case 0:
                                writer.Write(num18);
                                break;

                            case 1:
                                writer.Write(numArray2[j & 3]);
                                break;

                            case 2:
                                writer.Write(numArray[1]);
                                break;

                            case 3:
                                output.Position += 4L;
                                break;
                        }
                        num18 = reader2.Read();
                        switch ((buffer[((j & 3) * 4) + 2] & (flag ? 0 : 7)))
                        {
                            case 0:
                                writer.Write(num18);
                                break;

                            case 1:
                                writer.Write(numArray2[j & 3]);
                                break;

                            case 2:
                                writer.Write(numArray[2]);
                                break;

                            case 3:
                                output.Position += 4L;
                                break;
                        }
                        num18 = reader2.Read();
                        switch ((buffer[((j & 3) * 4) + 3] & (flag ? 0 : 7)))
                        {
                            case 0:
                            {
                                writer.Write(num18);
                                continue;
                            }
                            case 1:
                            {
                                writer.Write(numArray2[j & 3]);
                                continue;
                            }
                            case 2:
                            {
                                writer.Write(numArray[3]);
                                continue;
                            }
                            case 3:
                            {
                                output.Position += 4L;
                                continue;
                            }
                        }
                    }
                    si.Position = num16;
                }
            }
        }

        private class Reader
        {
            private BinaryReader br;
            private bool usn;
            private int vl;

            public Reader(BinaryReader br, int vl, int usn)
            {
                this.br = br;
                this.vl = vl;
                this.usn = usn != 0;
            }

            public uint Read()
            {
                switch (this.vl)
                {
                    case 0:
                        return this.br.ReadUInt32();

                    case 1:
                        if (!this.usn)
                        {
                            return (uint) this.br.ReadInt16();
                        }
                        return this.br.ReadUInt16();

                    case 2:
                        if (!this.usn)
                        {
                            return (uint) this.br.ReadSByte();
                        }
                        return this.br.ReadByte();
                }
                throw new NotSupportedException("vl(" + this.vl + ")");
            }
        }
    }
}

