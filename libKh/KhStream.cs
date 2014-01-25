using System;
using System.IO;

namespace Kh
{
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
        }

        public override long Position
        {
            get { return virtualPosition; }
            set { virtualPosition = value; }
        }
        public override long Length
        {
            get { return virtualLength; }
        }
        public override bool CanRead
        {
            get { return true; }
        }
        public override bool CanSeek
        {
            get { return true; }
        }
        public override bool CanWrite
        {
            get { return false; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
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
            return Position;
        }
    }
}
