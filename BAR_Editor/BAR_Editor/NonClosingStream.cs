namespace BAR_Editor
{
    using System;
    using System.IO;

    internal class NonClosingStream : Stream, IDisposable
    {
        private Stream _baseStream;

        public NonClosingStream(Stream baseStream)
        {
            _baseStream = baseStream;
        }

        public override void Close()
        {
            _baseStream = null;
        }

        public override void Flush()
        {
            _baseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _baseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _baseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _baseStream.SetLength(value);
        }

        void IDisposable.Dispose()
        {
            _baseStream = null;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _baseStream.Write(buffer, offset, count);
        }

        public override bool CanRead
        {
            get
            {
                return _baseStream.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return _baseStream.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return _baseStream.CanWrite;
            }
        }

        public override long Length
        {
            get
            {
                return _baseStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return _baseStream.Position;
            }
            set
            {
                _baseStream.Position = value;
            }
        }
    }
}

