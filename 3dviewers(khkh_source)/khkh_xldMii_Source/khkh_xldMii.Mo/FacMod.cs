using System;
using System.Collections.Generic;
using System.IO;
namespace khkh_xldMii.Mo
{
	public class FacMod
	{
		public List<Fac1> alf1 = new List<Fac1>();
		public FacMod(MemoryStream si)
		{
			if (si.Length < 2L)
			{
				return;
			}
			BinaryReader binaryReader = new BinaryReader(si);
			byte b = binaryReader.ReadByte();
			binaryReader.ReadByte();
			binaryReader.ReadUInt16();
			for (int i = 0; i < (int)b; i++)
			{
				Fac1 fac = new Fac1();
				try
				{
					fac.v0 = binaryReader.ReadInt16();
					fac.v2 = binaryReader.ReadInt16();
					if (fac.v0 == 0 && fac.v2 == -1 && i != 0)
					{
						fac.v4 = 0;
						fac.v6 = 0;
					}
					else
					{
						fac.v4 = binaryReader.ReadInt16();
						if (fac.v2 != -1)
						{
							fac.v6 = binaryReader.ReadInt16();
						}
						else
						{
							fac.v6 = 0;
						}
					}
				}
				catch (EndOfStreamException)
				{
				}
				this.alf1.Add(fac);
			}
		}
	}
}
