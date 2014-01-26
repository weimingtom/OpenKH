#include "KhStream.h"


namespace Kh
{

	FileStream::FileStream(IDX *idx, const std::wstring &filename)
	{
		InitializeInstanceFields();
		position = 0;
		entry = idx->OpenFile(filename);
		if (entry.index == -1)
		{
			throw System::IO::FileNotFoundException();
		}
	}

	const long long &FileStream::getPosition() const
	{
		return position;
	}

	void FileStream::setPosition(const long long &value)
	{
		if (getCanSeek())
		{
			position = value;
		}
	}

	const long long &FileStream::getLength() const
	{
		return entry.length;
	}

	const bool &FileStream::getCanRead() const
	{
		return true;
	}

	const bool &FileStream::getCanSeek() const
	{
		return entry.streamed == false;
	}

	const bool &FileStream::getCanWrite() const
	{
		return false;
	}

	int FileStream::Read(unsigned char buffer[], int offset, int count)
	{
		if (getCanRead())
		{
			if (getPosition() + count > getLength())
			{
				count = static_cast<int>(getLength() - getPosition());
			}
			entry.stream->Position = entry.position + getPosition();
			entry.stream->Read(buffer, offset, count);
			position += count;
			return count;
		}
		return 0;
	}

	void FileStream::Write(unsigned char buffer[], int offset, int count)
	{

	}

	void FileStream::Flush()
	{

	}

	void FileStream::SetLength(long long value)
	{

	}

	long long FileStream::Seek(long long offset, SeekOrigin origin)
	{
		if (getCanSeek())
		{
			switch (origin)
			{
				case SeekOrigin::Begin:
					setPosition(offset);
					break;
				case SeekOrigin::Current:
					setPosition(getPosition() + offset);
					break;
				case SeekOrigin::End:
					setPosition(getLength() + offset);
					break;
			}
		}
		return getPosition();
	}

	void FileStream::InitializeInstanceFields()
	{
		position = 0;
	}
}
