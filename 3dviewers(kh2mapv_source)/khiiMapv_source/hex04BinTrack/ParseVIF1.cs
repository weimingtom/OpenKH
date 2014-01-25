namespace hex04BinTrack
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ParseVIF1
    {
        public List<byte[]> almsmem;
        private VU1Mem vu1;

        public ParseVIF1(VU1Mem vu1)
        {
            this.almsmem = new List<byte[]>();
            base..ctor();
            this.vu1 = vu1;
            return;
        }

        public void Parse(MemoryStream si)
        {
            this.Parse(si, 0);
            return;
        }

        public void Parse(MemoryStream si, int tops)
        {
            MemoryStream stream;
            BinaryWriter writer;
            byte[] buffer;
            uint[] numArray;
            uint[] numArray2;
            BinaryReader reader;
            uint num;
            int num2;
            uint num3;
            int num4;
            int num5;
            int num6;
            int num7;
            int num8;
            int num9;
            int num10;
            int num11;
            int num12;
            int num13;
            int num14;
            int num15;
            long num16;
            bool flag;
            Reader reader2;
            int num17;
            uint num18;
            byte[] buffer2;
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
            int num30;
            int num31;
            int num32;
            int num33;
            int num34;
            int num35;
            stream = new MemoryStream(this.vu1.vumem, 1);
            writer = new BinaryWriter(stream);
            buffer2 = new byte[0x10];
            buffer = buffer2;
            numArray = new uint[4];
            numArray2 = new uint[4];
            reader = new BinaryReader(si);
            goto Label_08C0;
        Label_0041:
            long num1 = si.Position;
            num = reader.ReadUInt32();
            num2 = (num >> 0x18) & 0x7f;
            num19 = num2;
            if (num19 > 0x20)
            {
                goto Label_00D7;
            }
            switch (num19)
            {
                case 0:
                    goto Label_08C0;

                case 1:
                    goto Label_08C0;

                case 2:
                    goto Label_08C0;

                case 3:
                    goto Label_08C0;

                case 4:
                    goto Label_08C0;

                case 5:
                    goto Label_08C0;

                case 6:
                    goto Label_08C0;

                case 7:
                    goto Label_08C0;

                case 8:
                    goto Label_0219;

                case 9:
                    goto Label_0219;

                case 10:
                    goto Label_0219;

                case 11:
                    goto Label_0219;

                case 12:
                    goto Label_0219;

                case 13:
                    goto Label_0219;

                case 14:
                    goto Label_0219;

                case 15:
                    goto Label_0219;

                case 0x10:
                    goto Label_08C0;

                case 0x11:
                    goto Label_08C0;

                case 0x12:
                    goto Label_0219;

                case 0x13:
                    goto Label_08C0;

                case 20:
                    goto Label_0109;

                case 0x15:
                    goto Label_08C0;

                case 0x16:
                    goto Label_0219;

                case 0x17:
                    goto Label_0114;
            }
            if (num19 == 0x20)
            {
                goto Label_011F;
            }
            goto Label_0219;
        Label_00D7:
            switch ((num19 - 0x30))
            {
                case 0:
                    goto Label_014F;

                case 1:
                    goto Label_0180;
            }
            if (num19 == 0x4a)
            {
                goto Label_08C0;
            }
            switch ((num19 - 80))
            {
                case 0:
                    goto Label_01AD;

                case 1:
                    goto Label_01E3;
            }
            goto Label_0219;
        Label_0109:
            this.SplitMsmem();
            goto Label_08C0;
        Label_0114:
            this.SplitMsmem();
            goto Label_08C0;
        Label_011F:
            num3 = reader.ReadUInt32();
            num4 = 0;
            goto Label_0144;
        Label_012D:
            buffer[num4] = (byte) ((num3 >> ((2 * num4) & 0x1f)) & 3);
            num4 += 1;
        Label_0144:
            if (num4 < 0x10)
            {
                goto Label_012D;
            }
            goto Label_08C0;
        Label_014F:
            numArray2[0] = reader.ReadUInt32();
            numArray2[1] = reader.ReadUInt32();
            numArray2[2] = reader.ReadUInt32();
            numArray2[3] = reader.ReadUInt32();
            goto Label_08C0;
        Label_0180:
            numArray[0] = reader.ReadUInt32();
            numArray[1] = reader.ReadUInt32();
            numArray[2] = reader.ReadUInt32();
            numArray[3] = reader.ReadUInt32();
            goto Label_08C0;
        Label_01AD:
            num5 = num & 0xffff;
            si.Position = (si.Position + 15L) & -16L;
            si.Position += (long) (0x10 * num5);
            goto Label_08C0;
        Label_01E3:
            num6 = num & 0xffff;
            si.Position = (si.Position + 15L) & -16L;
            si.Position += (long) (0x10 * num6);
            goto Label_08C0;
        Label_0219:
            if (0x60 != (num2 & 0x60))
            {
                goto Label_08C0;
            }
            num7 = (num2 >> 4) & 1;
            num8 = (num2 >> 2) & 3;
            num9 = num2 & 3;
            num10 = (num >> 0x10) & 0xff;
            num11 = (num >> 14) & 1;
            num12 = num & 0x3ff;
            num13 = 0;
            num14 = 1;
            num20 = num9;
            switch (num20)
            {
                case 0:
                    goto Label_027E;

                case 1:
                    goto Label_0283;

                case 2:
                    goto Label_0288;

                case 3:
                    goto Label_028D;
            }
            goto Label_0290;
        Label_027E:
            num13 = 4;
            goto Label_0290;
        Label_0283:
            num13 = 2;
            goto Label_0290;
        Label_0288:
            num13 = 1;
            goto Label_0290;
        Label_028D:
            num13 = 2;
        Label_0290:
            num21 = num8;
            switch (num21)
            {
                case 0:
                    goto Label_02AD;

                case 1:
                    goto Label_02B2;

                case 2:
                    goto Label_02B7;

                case 3:
                    goto Label_02BC;
            }
            goto Label_02BF;
        Label_02AD:
            num14 = 1;
            goto Label_02BF;
        Label_02B2:
            num14 = 2;
            goto Label_02BF;
        Label_02B7:
            num14 = 3;
            goto Label_02BF;
        Label_02BC:
            num14 = 4;
        Label_02BF:
            num15 = (((num13 * num14) * num10) + 3) & -4;
            num16 = si.Position + ((long) num15);
            stream.Position = (long) (0x10 * (tops + num12));
            flag = (num7 == null) ? 1 : 0;
            reader2 = new Reader(reader, num9, num11);
            num17 = 0;
            goto Label_08AF;
        Label_0307:
            num22 = num8;
            switch (num22)
            {
                case 0:
                    goto Label_0322;

                case 1:
                    goto Label_04BE;

                case 2:
                    goto Label_05B3;

                case 3:
                    goto Label_0703;
            }
        Label_0322:
            num18 = reader2.Read();
            num23 = buffer[(num17 & 3) * 4] & ((flag != null) ? 0 : 7);
            switch (num23)
            {
                case 0:
                    goto Label_0357;

                case 1:
                    goto Label_0361;

                case 2:
                    goto Label_0370;

                case 3:
                    goto Label_037B;
            }
            goto Label_038A;
        Label_0357:
            writer.Write(num18);
            goto Label_038A;
        Label_0361:
            writer.Write(numArray2[num17 & 3]);
            goto Label_038A;
        Label_0370:
            writer.Write(numArray[0]);
            goto Label_038A;
        Label_037B:
            stream.Position += 4L;
        Label_038A:
            num24 = buffer[((num17 & 3) * 4) + 1] & ((flag != null) ? 0 : 7);
            switch (num24)
            {
                case 0:
                    goto Label_03B8;

                case 1:
                    goto Label_03C2;

                case 2:
                    goto Label_03D1;

                case 3:
                    goto Label_03DC;
            }
            goto Label_03EB;
        Label_03B8:
            writer.Write(num18);
            goto Label_03EB;
        Label_03C2:
            writer.Write(numArray2[num17 & 3]);
            goto Label_03EB;
        Label_03D1:
            writer.Write(numArray[1]);
            goto Label_03EB;
        Label_03DC:
            stream.Position += 4L;
        Label_03EB:
            num25 = buffer[((num17 & 3) * 4) + 2] & ((flag != null) ? 0 : 7);
            switch (num25)
            {
                case 0:
                    goto Label_0419;

                case 1:
                    goto Label_0423;

                case 2:
                    goto Label_0432;

                case 3:
                    goto Label_043D;
            }
            goto Label_044C;
        Label_0419:
            writer.Write(num18);
            goto Label_044C;
        Label_0423:
            writer.Write(numArray2[num17 & 3]);
            goto Label_044C;
        Label_0432:
            writer.Write(numArray[2]);
            goto Label_044C;
        Label_043D:
            stream.Position += 4L;
        Label_044C:
            num26 = buffer[((num17 & 3) * 4) + 3] & ((flag != null) ? 0 : 7);
            switch (num26)
            {
                case 0:
                    goto Label_047D;

                case 1:
                    goto Label_048A;

                case 2:
                    goto Label_049C;

                case 3:
                    goto Label_04AA;
            }
            goto Label_08A9;
        Label_047D:
            writer.Write(num18);
            goto Label_08A9;
        Label_048A:
            writer.Write(numArray2[num17 & 3]);
            goto Label_08A9;
        Label_049C:
            writer.Write(numArray[3]);
            goto Label_08A9;
        Label_04AA:
            stream.Position += 4L;
            goto Label_08A9;
        Label_04BE:
            num18 = reader2.Read();
            num27 = buffer[(num17 & 3) * 4] & ((flag != null) ? 0 : 7);
            switch (num27)
            {
                case 0:
                    goto Label_04F3;

                case 1:
                    goto Label_04FD;

                case 2:
                    goto Label_050C;

                case 3:
                    goto Label_0517;
            }
            goto Label_0526;
        Label_04F3:
            writer.Write(num18);
            goto Label_0526;
        Label_04FD:
            writer.Write(numArray2[num17 & 3]);
            goto Label_0526;
        Label_050C:
            writer.Write(numArray[0]);
            goto Label_0526;
        Label_0517:
            stream.Position += 4L;
        Label_0526:
            num18 = reader2.Read();
            num28 = buffer[((num17 & 3) * 4) + 1] & ((flag != null) ? 0 : 7);
            switch (num28)
            {
                case 0:
                    goto Label_055D;

                case 1:
                    goto Label_0567;

                case 2:
                    goto Label_0576;

                case 3:
                    goto Label_0581;
            }
            goto Label_0590;
        Label_055D:
            writer.Write(num18);
            goto Label_0590;
        Label_0567:
            writer.Write(numArray2[num17 & 3]);
            goto Label_0590;
        Label_0576:
            writer.Write(numArray[1]);
            goto Label_0590;
        Label_0581:
            stream.Position += 4L;
        Label_0590:
            stream.Position += 4L;
            stream.Position += 4L;
            goto Label_08A9;
        Label_05B3:
            num18 = reader2.Read();
            num29 = buffer[(num17 & 3) * 4] & ((flag != null) ? 0 : 7);
            switch (num29)
            {
                case 0:
                    goto Label_05E8;

                case 1:
                    goto Label_05F2;

                case 2:
                    goto Label_0601;

                case 3:
                    goto Label_060C;
            }
            goto Label_061B;
        Label_05E8:
            writer.Write(num18);
            goto Label_061B;
        Label_05F2:
            writer.Write(numArray2[num17 & 3]);
            goto Label_061B;
        Label_0601:
            writer.Write(numArray[0]);
            goto Label_061B;
        Label_060C:
            stream.Position += 4L;
        Label_061B:
            num18 = reader2.Read();
            num30 = buffer[((num17 & 3) * 4) + 1] & ((flag != null) ? 0 : 7);
            switch (num30)
            {
                case 0:
                    goto Label_0652;

                case 1:
                    goto Label_065C;

                case 2:
                    goto Label_066B;

                case 3:
                    goto Label_0676;
            }
            goto Label_0685;
        Label_0652:
            writer.Write(num18);
            goto Label_0685;
        Label_065C:
            writer.Write(numArray2[num17 & 3]);
            goto Label_0685;
        Label_066B:
            writer.Write(numArray[1]);
            goto Label_0685;
        Label_0676:
            stream.Position += 4L;
        Label_0685:
            num18 = reader2.Read();
            num31 = buffer[((num17 & 3) * 4) + 2] & ((flag != null) ? 0 : 7);
            switch (num31)
            {
                case 0:
                    goto Label_06BC;

                case 1:
                    goto Label_06C6;

                case 2:
                    goto Label_06D5;

                case 3:
                    goto Label_06E0;
            }
            goto Label_06EF;
        Label_06BC:
            writer.Write(num18);
            goto Label_06EF;
        Label_06C6:
            writer.Write(numArray2[num17 & 3]);
            goto Label_06EF;
        Label_06D5:
            writer.Write(numArray[2]);
            goto Label_06EF;
        Label_06E0:
            stream.Position += 4L;
        Label_06EF:
            stream.Position += 4L;
            goto Label_08A9;
        Label_0703:
            num18 = reader2.Read();
            num32 = buffer[(num17 & 3) * 4] & ((flag != null) ? 0 : 7);
            switch (num32)
            {
                case 0:
                    goto Label_0738;

                case 1:
                    goto Label_0742;

                case 2:
                    goto Label_0751;

                case 3:
                    goto Label_075C;
            }
            goto Label_076B;
        Label_0738:
            writer.Write(num18);
            goto Label_076B;
        Label_0742:
            writer.Write(numArray2[num17 & 3]);
            goto Label_076B;
        Label_0751:
            writer.Write(numArray[0]);
            goto Label_076B;
        Label_075C:
            stream.Position += 4L;
        Label_076B:
            num18 = reader2.Read();
            num33 = buffer[((num17 & 3) * 4) + 1] & ((flag != null) ? 0 : 7);
            switch (num33)
            {
                case 0:
                    goto Label_07A2;

                case 1:
                    goto Label_07AC;

                case 2:
                    goto Label_07BB;

                case 3:
                    goto Label_07C6;
            }
            goto Label_07D5;
        Label_07A2:
            writer.Write(num18);
            goto Label_07D5;
        Label_07AC:
            writer.Write(numArray2[num17 & 3]);
            goto Label_07D5;
        Label_07BB:
            writer.Write(numArray[1]);
            goto Label_07D5;
        Label_07C6:
            stream.Position += 4L;
        Label_07D5:
            num18 = reader2.Read();
            num34 = buffer[((num17 & 3) * 4) + 2] & ((flag != null) ? 0 : 7);
            switch (num34)
            {
                case 0:
                    goto Label_080C;

                case 1:
                    goto Label_0816;

                case 2:
                    goto Label_0825;

                case 3:
                    goto Label_0830;
            }
            goto Label_083F;
        Label_080C:
            writer.Write(num18);
            goto Label_083F;
        Label_0816:
            writer.Write(numArray2[num17 & 3]);
            goto Label_083F;
        Label_0825:
            writer.Write(numArray[2]);
            goto Label_083F;
        Label_0830:
            stream.Position += 4L;
        Label_083F:
            num18 = reader2.Read();
            num35 = buffer[((num17 & 3) * 4) + 3] & ((flag != null) ? 0 : 7);
            switch (num35)
            {
                case 0:
                    goto Label_0876;

                case 1:
                    goto Label_0880;

                case 2:
                    goto Label_088F;

                case 3:
                    goto Label_089A;
            }
            goto Label_08A9;
        Label_0876:
            writer.Write(num18);
            goto Label_08A9;
        Label_0880:
            writer.Write(numArray2[num17 & 3]);
            goto Label_08A9;
        Label_088F:
            writer.Write(numArray[3]);
            goto Label_08A9;
        Label_089A:
            stream.Position += 4L;
        Label_08A9:
            num17 += 1;
        Label_08AF:
            if (num17 < num10)
            {
                goto Label_0307;
            }
            si.Position = num16;
        Label_08C0:
            if (si.Position < si.Length)
            {
                goto Label_0041;
            }
            return;
        }

        private void SplitMsmem()
        {
            this.almsmem.Add((byte[]) this.vu1.vumem.Clone());
            return;
        }

        private class Reader
        {
            private BinaryReader br;
            private bool usn;
            private int vl;

            public Reader(BinaryReader br, int vl, int usn)
            {
                base..ctor();
                this.br = br;
                this.vl = vl;
                this.usn = (usn != null) ? 1 : 0;
                return;
            }

            public uint Read()
            {
                int num;
                switch (this.vl)
                {
                    case 0:
                        goto Label_001B;

                    case 1:
                        goto Label_0027;

                    case 2:
                        goto Label_0047;
                }
                goto Label_0067;
            Label_001B:
                return this.br.ReadUInt32();
            Label_0027:
                if (this.usn == null)
                {
                    goto Label_003B;
                }
                return this.br.ReadUInt16();
            Label_003B:
                return this.br.ReadInt16();
            Label_0047:
                if (this.usn == null)
                {
                    goto Label_005B;
                }
                return this.br.ReadByte();
            Label_005B:
                return this.br.ReadSByte();
            Label_0067:
                throw new NotSupportedException("vl(" + ((int) this.vl) + ")");
            }
        }
    }
}

