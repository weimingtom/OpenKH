#include "IDX.h"
#include "Data.h"


namespace Kh
{

	int IDX::CalculateHash32(const std::wstring &str)
	{
		int c = -1;

		int strIndex = 0;
		do
		{
			c ^= static_cast<int>(str[strIndex]) << 24;
			for (int i = 8; i > 0; i--)
			{
				if (c < 0)
				{
					c = (c << 1) ^ 0x4C11DB7;
				}
				else
				{
					c <<= 1;
				}
			}
			strIndex++;
		} while (strIndex < str.length());

		return ~c;
	}

	int IDX::CalculateHash16(const std::wstring &str)
	{
		int s1 = -1;
		int length = str.length();
		while (--length >= 0)
		{
			s1 = (s1 ^ (str[length] << 8)) & 0xFFFF;
			for (int i = 8; i > 0; i--)
			{
				if ((s1 & 0x8000) != 0)
				{
					s1 = ((s1 << 1) ^ 0x1021) & 0xFFFF;
				}
				else
				{
					s1 <<= 1;
				}
			}
		}
		return (~s1) & 0xFFFF;
	}

	IDX::IDX(System::IO::Stream *streamIdx, System::IO::Stream *streamImg)
	{
		// First 4 bytes are the entries count
		InitializeInstanceFields();
		unsigned char data[4];
		streamIdx->Read(data, 0, sizeof(data) / sizeof(data[0]));
		filesCount = Data::ByteToInt(data, 0, 4);
		idx = new FileIdx[filesCount];

		// Parse IDX file
		unsigned char dataIdx[0x10];
		for (int i = 0; i < filesCount; i++)
		{
			streamIdx->Read(dataIdx, 0, sizeof(dataIdx) / sizeof(dataIdx[0]));
			idx[i].hash32 = Data::ByteToInt(dataIdx, 0, 4);
			idx[i].hash16 = Data::ByteToInt(dataIdx, 4, 2);
			idx[i].clength = Data::ByteToInt(dataIdx, 6, 2);
			idx[i].position = Data::ByteToInt(dataIdx, 8, 4);
			idx[i].length = Data::ByteToInt(dataIdx, 12, 4);

			idx[i].streamed = (idx[i].clength & 0x4000) != 0;
			idx[i].compressed = (idx[i].clength & 0x8000) != 0;
			idx[i].clength = (idx[i].clength & 0x3FFF) * 0x800 + 0x800;
			idx[i].position *= 0x800;
		}

		// Get the stream of IMG
		this->streamImg = streamImg;
	}

	Kh::IDX::FileEntry IDX::OpenFile(const std::wstring &filename)
	{
		FileEntry entry = FileEntry();
		entry.filename = filename;
		entry.hash1 = CalculateHash32(filename);
		entry.hash2 = CalculateHash16(filename);
		entry.stream = streamImg;
		entry.index = SearchHashes(entry.hash1, entry.hash2);
		if (entry.index >= 0)
		{
			entry.position = idx[entry.index].position;
			entry.length = idx[entry.index].length;
			entry.clength = idx[entry.index].clength;
			entry.compressed = idx[entry.index].compressed;
			entry.streamed = idx[entry.index].streamed;
		}
		return entry;
	}

	int IDX::SearchHashes(int hash32, int hash16)
	{
		for (int i = 0; i < filesCount; i++)
		{
			if (idx[i].hash32 == hash32 && idx[i].hash16 == hash16)
			{
				return i;
			}
		}
		return -1;
	}

	void IDX::InitializeInstanceFields()
	{
		filesCount = 0;
	}
}
