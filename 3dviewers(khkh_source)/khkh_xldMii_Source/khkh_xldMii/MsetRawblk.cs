using SlimDX;
using System;
using System.Collections.Generic;
using System.IO;
namespace khkh_xldMii
{
	public class MsetRawblk
	{
		public List<MsetRM> alrm = new List<MsetRM>();
		public int cntJoints;
		public int cntFrames
		{
			get
			{
				return this.alrm.Count;
			}
		}
		public MsetRawblk(Stream si)
		{
			BinaryReader binaryReader = new BinaryReader(si);
			si.Position = 144L;
			int num = binaryReader.ReadInt32();
			if (num != 1)
			{
				throw new NotSupportedException("v90 != 1");
			}
			si.Position = 160L;
			int num2 = this.cntJoints = binaryReader.ReadInt32();
			si.Position = 180L;
			int num3 = binaryReader.ReadInt32();
			si.Position = 240L;
			this.alrm.Capacity = num3;
			for (int i = 0; i < num3; i++)
			{
				MsetRM msetRM = new MsetRM();
				msetRM.al.Capacity = num2;
				this.alrm.Add(msetRM);
				for (int j = 0; j < num2; j++)
				{
					Matrix item = default(Matrix);
					item.M11 = binaryReader.ReadSingle();
					item.M12 = binaryReader.ReadSingle();
					item.M13 = binaryReader.ReadSingle();
					item.M14 = binaryReader.ReadSingle();
					item.M21 = binaryReader.ReadSingle();
					item.M22 = binaryReader.ReadSingle();
					item.M23 = binaryReader.ReadSingle();
					item.M24 = binaryReader.ReadSingle();
					item.M31 = binaryReader.ReadSingle();
					item.M32 = binaryReader.ReadSingle();
					item.M33 = binaryReader.ReadSingle();
					item.M34 = binaryReader.ReadSingle();
					item.M41 = binaryReader.ReadSingle();
					item.M42 = binaryReader.ReadSingle();
					item.M43 = binaryReader.ReadSingle();
					item.M44 = binaryReader.ReadSingle();
					msetRM.al.Add(item);
				}
			}
		}
	}
}
