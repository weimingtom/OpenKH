#include "Data.h"


namespace Kh
{

	int Data::ByteToInt(unsigned char array_Renamed[], int offset, int length)
	{
		int n = 0;
		for (int i = 0; i < length; i++)
		{
			n |= array_Renamed[offset + i] << (8 * i);
		}
		return n;
	}
}
