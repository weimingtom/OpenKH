using System;
using System.IO;
namespace IDX_Tools
{
	internal class IDXIMGWriter : IDisposable
	{
		private IDXFileWriter idx = new IDXFileWriter();
		public IMGFile img
		{
			get;
			private set;
		}
		public IDXIMGWriter(Stream img, long imgoffset = 0L)
		{
			this.img = new IMGFile(img, imgoffset);
		}
		public void Dispose()
		{
			this.Dispose(true);
		}
		public void Dispose(bool disposing)
		{
			if (this.img != null)
			{
				this.img.Dispose();
				this.img = null;
			}
			if (disposing && this.idx != null)
			{
				this.idx = null;
			}
		}
		public void AddFile(IDXFile.IDXEntry file, Stream data)
		{
			this.img.AppendFile(file, data);
			this.idx.AddEntry(file);
		}
		public void RelinkFile(uint hash, uint target)
		{
			this.idx.RelinkEntry(hash, target);
		}
		public MemoryStream GetCurrentIDX()
		{
			return this.idx.GetStream();
		}
	}
}
