using System;
using System.Collections.Generic;
using System.IO;
namespace vconv122
{
	public class TIMc
	{
		public static Texex2[] Load(Stream fs)
		{
			int num = Convert.ToInt32(fs.Position);
			BinaryReader binaryReader = new BinaryReader(fs);
			List<int> list = new List<int>();
			if (binaryReader.ReadUInt32() == 4294967295u)
			{
				int num2 = binaryReader.ReadInt32();
				for (int i = 0; i < num2; i++)
				{
					list.Add(num + binaryReader.ReadInt32());
				}
			}
			else
			{
				list.Add(num);
			}
			List<Texex2> list2 = new List<Texex2>();
			for (int j = 0; j < list.Count; j++)
			{
				int num3 = list[j];
				int num4 = (j + 1 < list.Count) ? list[j + 1] : Convert.ToInt32(fs.Length);
				byte[] buffer = new byte[num4 - num3];
				fs.Position = (long)num3;
				fs.Read(buffer, 0, num4 - num3);
				list2.Add(TIMf.Load(new MemoryStream(buffer, false)));
			}
			return list2.ToArray();
		}
	}
}
