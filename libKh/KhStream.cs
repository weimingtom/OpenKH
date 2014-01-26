using System;
using System.IO;

namespace Kh
{
<<<<<<< HEAD
    public class FileStream : System.IO.Stream
    {
        IDX.FileEntry entry;

        long position;

        /// <summary>
        /// Open a Kingdom Hearts' file stream
        /// </summary>
        /// <param name="idx">IDX object</param>
        /// <param name="filename">name of the file to open</param>
        public FileStream(IDX idx, string filename)
        {
            position = 0;
            entry = idx.OpenFile(filename);
            if (entry.index == -1)
                throw new System.IO.FileNotFoundException();
=======
    class KhFileStream : System.IO.Stream
    {
        Stream internalStream;
        long internalPosition;

        long virtualPosition;
        long virtualLength;

        public KhFileStream(IDX idx, string filename)
        {
            internalPosition = 0;
            virtualPosition = 0;
>>>>>>> parent of 906e419... BAR Editor
        }

        public override long Position
        {
<<<<<<< HEAD
            get { return position; }
            set { if (CanSeek) position = value; }
        }
        public override long Length
        {
            get { return entry.length; }
=======
            get { return virtualPosition; }
            set { virtualPosition = value; }
        }
        public override long Length
        {
            get { return virtualLength; }
>>>>>>> parent of 906e419... BAR Editor
        }
        public override bool CanRead
        {
            get { return true; }
        }
        public override bool CanSeek
        {
<<<<<<< HEAD
            get { return entry.streamed == false; }
=======
            get { return true; }
>>>>>>> parent of 906e419... BAR Editor
        }
        public override bool CanWrite
        {
            get { return false; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
<<<<<<< HEAD
            if (CanRead)
            {
                if (Position + count > Length)
                    count = (int)(Length - Position);
                entry.stream.Position = entry.position + Position;
                entry.stream.Read(buffer, offset, count);
                position += count;
                return count;
            }
=======
>>>>>>> parent of 906e419... BAR Editor
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
<<<<<<< HEAD
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
=======
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
>>>>>>> parent of 906e419... BAR Editor
            }
            return Position;
        }
    }
}
