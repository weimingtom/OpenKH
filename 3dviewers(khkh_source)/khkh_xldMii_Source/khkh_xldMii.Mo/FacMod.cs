namespace khkh_xldMii.Mo
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class FacMod
    {
        public List<Fac1> alf1 = new List<Fac1>();

        public FacMod(MemoryStream si)
        {
            if (si.Length >= 2L)
            {
                BinaryReader reader = new BinaryReader(si);
                byte num = reader.ReadByte();
                reader.ReadByte();
                reader.ReadUInt16();
                for (int i = 0; i < num; i++)
                {
                    Fac1 item = new Fac1();
                    try
                    {
                        item.v0 = reader.ReadInt16();
                        item.v2 = reader.ReadInt16();
                        if (((item.v0 == 0) && (item.v2 == -1)) && (i != 0))
                        {
                            item.v4 = 0;
                            item.v6 = 0;
                        }
                        else
                        {
                            item.v4 = reader.ReadInt16();
                            if (item.v2 != -1)
                            {
                                item.v6 = reader.ReadInt16();
                            }
                            else
                            {
                                item.v6 = 0;
                            }
                        }
                    }
                    catch (EndOfStreamException)
                    {
                    }
                    this.alf1.Add(item);
                }
            }
        }
    }
}

