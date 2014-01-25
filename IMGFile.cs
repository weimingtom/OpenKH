namespace IDX_Tools
{
    using KH2ISO_Tools;
    using KHCompress;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using Utility;

    internal class IMGFile : IDisposable
    {
        private static byte[] copyBuffer = new byte[0x1000];
        internal Stream file;
        private long offset;

        public IMGFile(Stream file, long offset = 0L)
        {
            this.file = file;
            this.offset = offset;
        }

        public void AppendFile(IDXFile.IDXEntry entry, Stream data)
        {
            if (data.Length > 0xffffffffL)
            {
                throw new NotSupportedException("data too big to store");
            }
            this.file.Seek(0L, SeekOrigin.End);
            this.EnsureBoundary();
            entry.DataLBA = ((uint)(this.file.Position - this.offset)) / 0x800;
            data.CopyTo(this.file);
            this.EnsureBoundary();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (this.file != null)
            {
                this.file.Dispose();
                this.file = null;
            }
        }

        private void EnsureBoundary()
        {
            if (((this.file.Position - this.offset) % 0x800L) != 0L)
            {
                int num = 0x800 - ((int)((this.file.Position - this.offset) % 0x800L));
                byte[] buffer = new byte[4];
                while (num > 3)
                {
                    this.file.Write(buffer, 0, 4);
                    num -= 4;
                }
                while (--num >= 0)
                {
                    this.file.WriteByte(0);
                }
            }
            if (((this.file.Position - this.offset) % 0x800L) != 0L)
            {
                throw new DataMisalignedException();
            }
        }

        public Stream GetFileStream(IDXFile.IDXEntry entry)
        {
            return new Substream(this.file, this.offset + (entry.DataLBA * 0x800), entry.IsCompressed ? ((long)entry.CompressedDataLength) : ((long)entry.DataLength));
        }

        public void ReadFile(IDXFile.IDXEntry entry, Stream target)
        {
            if (entry.IsCompressed)
            {
                if (entry.CompressedDataLength > 0x7fffffff)
                {
                    throw new NotSupportedException("File to big to decompress");
                }
                byte[] buffer = new byte[entry.CompressedDataLength];
                this.Seek(entry.DataLBA);
                this.file.Read(buffer, 0, (int)entry.CompressedDataLength);
                try
                {
                    byte[] buffer2 = KHCompress.KHCompress.KH2Compressor.decompress(buffer, entry.DataLength);
                    target.Write(buffer2, 0, buffer2.Length);
                }
                catch (Exception exception)
                {
                    Program.WriteError(" ERROR: Failed to decompress: " + exception.Message, new object[0]);
                }
            }
            else
            {
                this.ReadFileBuffer(target, entry.Offset, entry.DataLength);
            }
        }

        private void ReadFileBuffer(Stream destination, long origin, uint length)
        {
            int num;
            this.file.Position = this.offset + origin;
            while ((length > 0) && ((num = this.file.Read(copyBuffer, 0, (int)Math.Min(0x1000, length))) != 0))
            {
                destination.Write(copyBuffer, 0, num);
                length -= (uint)num;
            }
        }

        public void Seek(uint sector)
        {
            this.file.Position = this.offset + (sector * 0x800);
        }

        public void WriteFile(Stream data)
        {
            if (data.Length > 0xffffffffL)
            {
                throw new NotSupportedException("data too big to store");
            }
            this.EnsureBoundary();
            data.CopyTo(this.file);
            this.EnsureBoundary();
        }
    }
}

