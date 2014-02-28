using System;
using System.Collections.Generic;
using System.IO;
namespace khkh_xldMii.Mx
{
	public class Mdlxfst
	{
		public List<T31> alt31 = new List<T31>();
		public Mdlxfst(Stream fs)
		{
			BinaryReader binaryReader = new BinaryReader(fs);
			Queue<int> queue = new Queue<int>();
			queue.Enqueue(144);
			int num = 0;
			while (queue.Count != 0)
			{
				int num2 = queue.Dequeue();
				fs.Position = (long)(num2 + 16);
				int num3 = (int)binaryReader.ReadUInt16();
				fs.Position = (long)(num2 + 28);
				int num4 = (int)binaryReader.ReadUInt16();
				T31 t;
				this.alt31.Add(t = new T31(num2, 32 * (1 + num4), num));
				num++;
				for (int i = 0; i < num4; i++)
				{
					fs.Position = (long)num2 + 32L + 32L * (long)i + 16L;
					int num5 = binaryReader.ReadInt32() + num2;
					int num6 = binaryReader.ReadInt32() + num2;
					fs.Position = (long)(num2 + 32 + 32 * i + 4);
					int texi = binaryReader.ReadInt32();
					fs.Position = (long)num6;
					int num7 = binaryReader.ReadInt32();
					t.al11.Add(new T11(num6, RUtil.RoundUpto16(4 + 4 * num7), i));
					List<int> list = new List<int>(num7);
					for (int j = 0; j < num7; j++)
					{
						list.Add(binaryReader.ReadInt32());
					}
					List<int> list2 = new List<int>();
					List<int[]> list3 = new List<int[]>();
					List<int> list4 = new List<int>();
					list2.Add(num5);
					fs.Position = (long)(num5 + 16);
					for (int k = 0; k < num7; k++)
					{
						if (list[k] == -1)
						{
							list2.Add((int)fs.Position + 16);
							fs.Position += 32L;
						}
						else
						{
							fs.Position += 16L;
						}
					}
					for (int l = 0; l < num7; l++)
					{
						if (l + 1 == num7)
						{
							list4.Add(list[l]);
							list3.Add(list4.ToArray());
							list4.Clear();
						}
						else
						{
							if (list[l] == -1)
							{
								list3.Add(list4.ToArray());
								list4.Clear();
							}
							else
							{
								list4.Add(list[l]);
							}
						}
					}
					int num8 = (int)fs.Position;
					t.al12.Add(new T12(num5, num8 - num5, i));
					int num9 = 0;
					foreach (int current in list2)
					{
						fs.Position = (long)current;
						int num10 = binaryReader.ReadInt32() & 65535;
						int num11 = (binaryReader.ReadInt32() & 2147483647) + num2;
						fs.Position = (long)num11;
						byte[] bin = binaryReader.ReadBytes(16 * num10);
						t.al13.Add(new T13vif(num11, 16 * num10, texi, list3[num9], bin));
						num9++;
					}
				}
				fs.Position = (long)(num2 + 20);
				int num12 = binaryReader.ReadInt32();
				if (num12 != 0)
				{
					num12 += num2;
					int num13 = 64 * num3;
					t.t21 = new T21(num12, num13);
					fs.Position = (long)num12;
					for (int m = 0; m < num13 / 64; m++)
					{
						t.t21.alaxb.Add(UtilAxBoneReader.read(binaryReader));
					}
				}
				fs.Position = (long)(num2 + 24);
				int num14 = binaryReader.ReadInt32();
				if (num14 != 0)
				{
					num14 += num2;
					int len = num12 - num14;
					t.t32 = new T32(num14, len);
				}
				fs.Position = (long)(num2 + 12);
				int num15 = binaryReader.ReadInt32();
				if (num15 != 0)
				{
					num15 += num2;
					queue.Enqueue(num15);
				}
			}
		}
	}
}
