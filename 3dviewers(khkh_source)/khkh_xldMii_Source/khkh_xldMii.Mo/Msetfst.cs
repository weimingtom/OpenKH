namespace khkh_xldMii.Mo
{
    using khkh_xldMii;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class Msetfst
    {
        public List<Mt1> al1 = new List<Mt1>();

        public Msetfst(Stream fs, string motionId)
        {
            ReadBar.Barent[] barentArray = ReadBar.Explode(fs);
            int num = 0;
            foreach (ReadBar.Barent barent in barentArray)
            {
                if ((barent.k == 0x11) && (barent.len != 0))
                {
                    Mt1 item = new Mt1();
                    ReadBar.Barent[] barentArray2 = ReadBar.Explode2(new MemoryStream(barent.bin, false));
                    int num2 = 0;
                    foreach (ReadBar.Barent barent2 in barentArray2)
                    {
                        switch (barent2.k)
                        {
                            case 9:
                                item.off = (uint) (barent.off + barent2.off);
                                item.len = (uint) barent.len;
                                item.id = barent.id + "#" + barent2.id;
                                item.bin = barent2.bin;
                                item.k1 = num;
                                item.isRaw = barent2.id.Equals("raw");
                                break;

                            case 0x10:
                                item.fm = new FacMod(new MemoryStream(barent2.bin, false));
                                break;
                        }
                        num2++;
                    }
                    this.al1.Add(item);
                }
                num++;
            }
        }
    }
}

