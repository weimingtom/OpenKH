namespace BAR_Editor
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    internal class Bar
    {
        public List<BarFile> FileList;

        public Bar()
        {
            FileList = new List<BarFile>();
        }

        public Bar(Stream file)
        {
            if (!file.CanRead || !file.CanSeek)
            {
                throw new NotSupportedException("Cannot read or seek in stream");
            }
            using (var reader = new BinaryReader(file))
            {
                if ((file.Length < 0x10L) || (reader.ReadUInt32() != 0x1524142))
                {
                    throw new InvalidDataException("Invalid signature");
                }
                var capacity = reader.ReadInt32();
                FileList = new List<BarFile>(capacity);
                file.Position = 0x10L;
                for (var i = 0; i < capacity; i++)
                {
                    var item = new BarFile {
                        Type = reader.ReadUInt32()
                    };
                    Buffer.BlockCopy(reader.ReadBytes(4), 0, item._id, 0, 4);
                    var num3 = file.Position + 8L;
                    var num4 = reader.ReadUInt32();
                    var count = reader.ReadInt32();
                    file.Position = num4;
                    item.Data = reader.ReadBytes(count);
                    FileList.Add(item);
                    file.Position = num3;
                }
            }
        }

        public void Save(Stream file)
        {
            if (!file.CanWrite)
            {
                throw new NotSupportedException("Cannot write to stream");
            }
            using (var writer = new BinaryWriter(file))
            {
                var position = file.Position;
                writer.Write((uint) 0x1524142);
                var count = FileList.Count;
                writer.Write(count);
                writer.Write((uint) 0);
                writer.Write((uint) 0);
                var num3 = (uint) (0x10 * (count + 1));
                for (var i = 0; i < count; i++)
                {
                    file.Position = position + (0x10 * (i + 1));
                    var file2 = FileList[i];
                    writer.Write(file2.Type);
                    writer.Write(file2._id);
                    writer.Write(num3);
                    writer.Write(file2.Data.Length);
                    var num5 = ((uint) Math.Ceiling(file2.Data.Length / 16.0)) * 0x10;
                    file.Position = position + num3;
                    writer.Write(file2.Data);
                    num3 += num5;
                }
            }
        }

        public int FileCount
        {
            get
            {
                return FileList.Count;
            }
        }

        public class BarFile
        {
            public byte[] Data;
            public uint Type;
            private byte[] _id1;

            public BarFile()
            {
                _id = new byte[4];
            }

            public byte[] _id
            {
                get { return _id1; }
                private set { _id1 = value; }
            }

            public string Id
            {
                get
                {
                    return Encoding.ASCII.GetString(_id).TrimEnd(new char[1]);
                }
                set
                {
                    Array.Clear(_id, 0, 4);
                    Encoding.ASCII.GetBytes(value, 0, Math.Min(4, value.Length), _id, 0);
                }
            }
        }
    }
}

