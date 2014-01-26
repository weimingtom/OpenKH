#include "Bar.h"
#include "Data.h"


namespace Kh
{

	Bar::Bar(FileStream *stream)
	{
		unsigned char data[0x10];
		stream->Read(data, 0, sizeof(data) / sizeof(data[0]));

		header.magic = Data::ByteToInt(data, 0, 4);
		header.count = Data::ByteToInt(data, 4, 4);
		header.dunno1 = Data::ByteToInt(data, 8, 4);
		header.dunno2 = Data::ByteToInt(data, 12, 4);

		if (header.magic != MagicCode)
		{
			throw System::IO::InvalidDataException();
		}

		entries = new Entry[header.count];
		for (int i = 0; i < header.count; i++)
		{
			stream->Read(data, 0, sizeof(data) / sizeof(data[0]));
			entries[i].dunno = Data::ByteToInt(data, 0, 4);
			entries[i].name = Data::ByteToInt(data, 4, 4);
			entries[i].position = Data::ByteToInt(data, 8, 4);
			entries[i].size = Data::ByteToInt(data, 12, 4);
		}
	}

	const int &Bar::getCount() const
	{
		return header.count;
	}
}
