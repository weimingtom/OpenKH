using System.Collections.Generic;
using System.IO;

namespace khkh_xldMii.Mo
{
    public class Msetfst
    {
        public List<Mt1> al1 = new List<Mt1>();

        public Msetfst(Stream fs, string motionId)
        {
            ReadBar.Barent[] array = ReadBar.Explode(fs);
            int num = 0;
            ReadBar.Barent[] array2 = array;
            for (int i = 0; i < array2.Length; i++)
            {
                ReadBar.Barent barent = array2[i];
                int k = barent.k;
                if (k == 17 && barent.len != 0)
                {
                    var mt = new Mt1();
                    ReadBar.Barent[] array3 = ReadBar.Explode2(new MemoryStream(barent.bin, false));
                    int num2 = 0;
                    ReadBar.Barent[] array4 = array3;
                    for (int j = 0; j < array4.Length; j++)
                    {
                        ReadBar.Barent barent2 = array4[j];
                        int k2 = barent2.k;
                        if (k2 != 9)
                        {
                            if (k2 == 16)
                            {
                                mt.fm = new FacMod(new MemoryStream(barent2.bin, false));
                            }
                        }
                        else
                        {
                            mt.off = (uint) (barent.off + barent2.off);
                            mt.len = (uint) barent.len;
                            mt.id = barent.id + "#" + barent2.id;
                            mt.bin = barent2.bin;
                            mt.k1 = num;
                            mt.isRaw = barent2.id.Equals("raw");
                        }
                        num2++;
                    }
                    al1.Add(mt);
                }
                num++;
            }
        }
    }
}