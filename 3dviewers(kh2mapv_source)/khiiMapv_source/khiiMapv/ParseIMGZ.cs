using System;
using System.Collections.Generic;
using System.IO;
namespace khiiMapv
{
	public class ParseIMGZ
	{
		public static PicIMGD[] TakeIMGZ(byte[] bin)
		{
			MemoryStream memoryStream = new MemoryStream(bin, false);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			List<PicIMGD> list = new List<PicIMGD>();
			memoryStream.Position = 12L;
			int num = binaryReader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int index = binaryReader.ReadInt32();
				int count = binaryReader.ReadInt32();
				MemoryStream si = new MemoryStream(bin, index, count, false);
				PicIMGD item = ParseIMGD.TakeIMGD(si);
				list.Add(item);
			}
			return list.ToArray();
		}
	}
}
