using khkh_xldMii.Mc;
using System;
using System.IO;
namespace khkh_xldMii.Mx
{
	internal class UtilAxBoneReader
	{
		public static AxBone read(BinaryReader br)
		{
			return new AxBone
			{
				cur = br.ReadInt32(),
				parent = br.ReadInt32(),
				v08 = br.ReadInt32(),
				v0c = br.ReadInt32(),
				x1 = br.ReadSingle(),
				y1 = br.ReadSingle(),
				z1 = br.ReadSingle(),
				w1 = br.ReadSingle(),
				x2 = br.ReadSingle(),
				y2 = br.ReadSingle(),
				z2 = br.ReadSingle(),
				w2 = br.ReadSingle(),
				x3 = br.ReadSingle(),
				y3 = br.ReadSingle(),
				z3 = br.ReadSingle(),
				w3 = br.ReadSingle()
			};
		}
	}
}
