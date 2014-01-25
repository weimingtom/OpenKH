using System;
using System.IO;

namespace Kh
{
    class KhFileStream : System.IO.Stream
    {
        IDX.FileEntry entry;

        long position;

        /// <summary>
        /// Open a Kingdom Hearts' file stream
        /// </summary>
        /// <param name="idx">IDX object</param>
        /// <param name="filename">name of the file to open</param>
        public KhFileStream(IDX idx, string filename)
        {
            position = 0;
            entry = idx.OpenFile(filename);
            if (entry.index == -1)
                throw new System.IO.FileNotFoundException();
        }

        public override long Position
        {
            get { return position; }
            set { if (CanSeek) position = value; }
        }
        public override long Length
        {
            get { return entry.length; }
        }
        public override bool CanRead
        {
            get { return true; }
        }
        public override bool CanSeek
        {
            get { return entry.streamed == false; }
        }
        public override bool CanWrite
        {
            get { return false; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (CanRead)
            {
                if (Position + count > Length)
                    count = (int)(Length - Position);
                entry.stream.Position = entry.position + Position;
                entry.stream.Read(buffer, offset, count);
                position += count;
                return count;
            }
            return 0;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {

        }
        public override void Flush()
        {

        }
        public override void SetLength(long value)
        {

        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (CanSeek)
            {
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        Position = offset;
                        break;
                    case SeekOrigin.Current:
                        Position += offset;
                        break;
                    case SeekOrigin.End:
                        Position = Length + offset;
                        break;
                }
            }
            return Position;
        }
    }
}
