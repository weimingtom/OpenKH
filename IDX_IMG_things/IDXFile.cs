using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Utility;

namespace IDX_Tools
{
    internal class IDXFile : IDisposable, IEnumerable<IDXFile.IDXEntry>, IEnumerable
    {
        private BinaryReader file;

        public uint Count { get; private set; }

        public uint Position { get; private set; }

        protected IDXFile()
        {
            this.file = (BinaryReader)null;
        }

        public IDXFile(Stream input, bool newidx = false)
        {
            this.file = new BinaryReader(input);
            input.Position = 0L;
            if (newidx)
            {
                this.Count = 0U;
                input.Write(new byte[4], 0, 4);
            }
            else
                this.Count = this.file.ReadUInt32();
            this.Position = 0U;
        }

        public void Dispose()
        {
            if (this.file == null)
                return;
            this.file.Close();
            this.file = (BinaryReader)null;
        }

        public IEnumerator<IDXFile.IDXEntry> GetEnumerator()
        {
            for (uint i = 0U; i < this.Count; ++i)
                yield return this.ReadEntry((long)i);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.GetEnumerator();
        }

        public IDXFile.IDXEntry ReadEntry(long index = -1L)
        {
            if (index >= 0L)
                this.Position = (uint)index;
            if (this.Position >= this.Count)
            {
                return new IDXFile.IDXEntry()
                {
                    Hash = 0U
                };
            }
            else
            {
                this.file.BaseStream.Position = (long)(uint)(4 + 16 * (int)this.Position++);
                return new IDXFile.IDXEntry()
                {
                    Hash = this.file.ReadUInt32(),
                    HashAlt = this.file.ReadUInt16(),
                    Compression = this.file.ReadUInt16(),
                    DataLBA = this.file.ReadUInt32(),
                    DataLength = this.file.ReadUInt32()
                };
            }
        }

        private void WriteEntry(IDXFile.IDXEntry entry, uint count = 0U)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter((Stream)new NonClosingStream(this.file.BaseStream)))
            {
                binaryWriter.Write(entry.Hash);
                binaryWriter.Write(entry.HashAlt);
                binaryWriter.Write(entry.Compression);
                binaryWriter.Write(entry.DataLBA);
                binaryWriter.Write(entry.DataLength);
                if ((int)count == 0)
                    return;
                binaryWriter.BaseStream.Position = 0L;
                binaryWriter.Write(count);
            }
        }

        private void FindEntryByHash(uint hash)
        {
            this.file.BaseStream.Position = 4L;
            for (uint index = 0U; index < this.Count; ++index)
            {
                if ((int)this.file.ReadUInt32() == (int)hash)
                {
                    this.file.BaseStream.Position -= 4L;
                    return;
                }
                else
                    this.file.BaseStream.Position += 12L;
            }
            throw new FileNotFoundException();
        }

        public void RelinkEntry(uint hash, uint target)
        {
            this.FindEntryByHash(target);
            this.file.BaseStream.Position += 4L;
            IDXFile.IDXEntry entry = new IDXFile.IDXEntry()
            {
                Hash = hash,
                HashAlt = this.file.ReadUInt16(),
                Compression = this.file.ReadUInt16(),
                DataLBA = this.file.ReadUInt32(),
                DataLength = this.file.ReadUInt32()
            };
            this.FindEntryByHash(hash);
            this.WriteEntry(entry, 0U);
        }

        public void ModifyEntry(IDXFile.IDXEntry entry)
        {
            this.FindEntryByHash(entry.Hash);
            this.WriteEntry(entry, 0U);
        }

        public void AddEntry(IDXFile.IDXEntry entry)
        {
            this.file.BaseStream.Position = (long)(uint)(4 + 16 * (int)this.Count);
            this.WriteEntry(entry, ++this.Count);
        }

        public class IDXEntry
        {
            public uint Hash;
            public ushort HashAlt;
            public ushort Compression;
            public uint DataLBA;
            public uint DataLength;

            public long Offset
            {
                get
                {
                    return (long)(this.DataLBA * 2048U);
                }
            }

            public bool IsCompressed
            {
                get
                {
                    return ((int)this.Compression & 16384) == 16384;
                }
                set
                {
                    this.Compression = (ushort)((value ? 16384 : 0) | (int)this.Compression & 49151);
                }
            }

            public bool IsDualHash
            {
                get
                {
                    return ((int)this.Compression & 32768) == 32768;
                }
                set
                {
                    this.Compression = (ushort)((value ? 32768 : 0) | (int)this.Compression & (int)short.MaxValue);
                }
            }

            public uint CompressedDataLength
            {
                get
                {
                    uint num = (uint)(((int)this.Compression & 16383) + 1);
                    if ((int)this.Hash == 271597423 && this.IsCompressed && this.DataLength > 12582912U)
                        num += 4096U;
                    return num * 2048U;
                }
                set
                {
                    if ((int)value == 0)
                        this.Compression &= (ushort)49152;
                    else
                        this.Compression = (ushort)((int)this.Compression & 49152 | (int)((uint)Math.Ceiling((double)value / 2048.0) - 1U) & 12287);
                }
            }
        }
    }
}
