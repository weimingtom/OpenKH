using System;
using System.Collections.Generic;
using System.IO;
using Utility;
namespace IDX_Tools
{
	internal class IDXFileWriter
	{
		private List<IDXFile.IDXEntry> entries = new List<IDXFile.IDXEntry>();
		public void AddEntry(IDXFile.IDXEntry entry)
		{
			this.entries.Add(entry);
		}
		public void RelinkEntry(uint hash, uint target)
		{
			IDXFile.IDXEntry iDXEntry = this.entries.Find((IDXFile.IDXEntry e) => e.Hash == target);
			if (iDXEntry.Hash == 0u)
			{
				throw new FileNotFoundException();
			}
			this.entries.Add(new IDXFile.IDXEntry
			{
				Hash = hash,
				HashAlt = 0,
				Compression = iDXEntry.Compression,
				DataLBA = iDXEntry.DataLBA,
				DataLength = iDXEntry.DataLength
			});
		}
		public MemoryStream GetStream()
		{
			this.entries.Sort(delegate(IDXFile.IDXEntry a, IDXFile.IDXEntry b)
			{
				if (a.Hash < b.Hash)
				{
					return -1;
				}
				if (a.Hash <= b.Hash)
				{
					return 0;
				}
				return 1;
			});
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(new NonClosingStream(memoryStream)))
				{
					binaryWriter.Write(this.entries.Count);
					foreach (IDXFile.IDXEntry current in this.entries)
					{
						binaryWriter.Write(current.Hash);
						binaryWriter.Write(current.HashAlt);
						binaryWriter.Write(current.Compression);
						binaryWriter.Write(current.DataLBA);
						binaryWriter.Write(current.DataLength);
					}
				}
			}
			catch (Exception ex)
			{
				memoryStream.Close();
				throw ex;
			}
			memoryStream.Position = 0L;
			return memoryStream;
		}
	}
}
