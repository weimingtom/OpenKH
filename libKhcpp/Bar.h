#pragma once

#include "KhStream.h"


namespace Kh
{
	class Bar
	{
	private:
		class Header
		{
		public:
			int magic;
			int count;
			int dunno1;
			int dunno2;
		};
	private:
		class Entry
		{
		public:
			int dunno;
			int name;
			int position;
			int size;
		};

	public:
		enum class Type
		{
			Temp = 0x00,
			BAR = 0x01,
			String = 0x02,
			AI = 0x03,
			MDLX = 0x04,
			DOCT = 0x05,
			Hitbox = 0x06,
			RawTexture = 0x07,
			TIM2 = 0x0A,
			COCT_2 = 0x0B,
			SPWN = 0x0C,
			BIN = 0x0D,
			SKY = 0x0E,
			COCT_3 = 0x0F,
			BAR_2 = 0x11,
			PAX = 0x12,
			COCT_4 = 0x13,
			BAR_3 = 0x14,
			ANL = 0x16,
			IMGD = 0x18,
			SEQD = 0x19,
			LAYERD = 0x1C,
		};

	private:
		static const int MagicCode = 0x01524142;

		/// <summary>
		/// Type of files
		/// TODO INCOMPLETE LIST
		/// Thanks to GovanifY for this
		/// </summary>
		Header header;
//C# TO C++ CONVERTER WARNING: Since the array size is not known in this declaration, C# to C++ Converter has converted this array to a pointer.  You will need to call 'delete[]' where appropriate:
//ORIGINAL LINE: Entry[] entries;
		Entry *entries;

	public:
		Bar(FileStream *stream);

		const int &getCount() const;
	};
}
