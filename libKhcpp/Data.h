#pragma once

namespace Kh
{
	/// <summary>
	/// Shared methods between OpenKH classes
	/// </summary>
	class Data
	{
		/// <summary>
		/// Annoying method that converts a portion of bytes to an integer value
		/// </summary>
		/// <param name="array">array where to extract the integer value</param>
		/// <param name="offset">start index</param>
		/// <param name="length">number of elements to process</param>
		/// <returns>integer value</returns>
	public:
		static int ByteToInt(unsigned char array_Renamed[], int offset, int length);
	};
}
