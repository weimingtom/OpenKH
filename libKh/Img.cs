using System.IO;

namespace Kh
{
    public class Img
    {
        private Stream[] eStream;
        private Entry[] entry;
        private Header header;

        public Img(string filename)
        {
            Open(filename);
        }

        public Img(Stream stream)
        {
            Open(stream);
        }

        public int Count
        {
            get { return header.filesCount; }
        }

        public void Open(string filename)
        {
            var fStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            Open(fStream);
            fStream.Close();
        }

        public void Open(Stream stream)
        {
            header.Read(stream);
            entry = new Entry[Count];
            eStream = new MemoryStream[Count];
            for (int i = 0; i < Count; i++)
            {
                entry[i].Read(stream);

                var data = new byte[entry[i].size];
                eStream[i] = new MemoryStream(data);
            }
            for (int i = 0; i < Count; i++)
            {
                stream.Position = entry[i].position;
                var data = new byte[entry[i].size];
                stream.Read(data, 0, entry[i].size);
                eStream[i] = new MemoryStream(data);
            }
        }

        public Stream Get(int index)
        {
            eStream[index].Position = 0;
            return eStream[index];
        }

        private struct Entry
        {
            public int position;
            public int size;

            public void Read(Stream stream)
            {
                var reader = new BinaryReader(stream);
                position = reader.ReadInt32();
                size = reader.ReadInt32();
            }
        }

        private struct Header
        {
            public int dummy1;
            public int dummy2;
            public int dummy3;
            public int filesCount;

            public void Read(Stream stream)
            {
                var reader = new BinaryReader(stream);
                filesCount = reader.ReadInt32();
                dummy1 = reader.ReadInt32();
                dummy2 = reader.ReadInt32();
                dummy3 = reader.ReadInt32();
            }
        }
    }
}