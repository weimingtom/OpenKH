#pragma once

#include <string>


namespace Kh
{
	/// <summary>
	/// Process IDX files
	/// </summary>
	class IDX
	{
		/// <summary>
		/// Used to get a file entry from IDX
		/// </summary>
	public:
		class FileEntry
		{
			/// <summary>
			/// Filename of file entry
			/// </summary>
		public:
			std::wstring filename;
			/// <summary>
			/// 32-bit hash of filename
			/// </summary>
			int hash1;
			/// <summary>
			/// 16-bit hash of filename
			/// </summary>
			int hash2;
			/// <summary>
			/// IMG stream
			/// </summary>
			Stream *stream;
			/// <summary>
			/// Index of file entry on IDX
			/// </summary>
			int index;
			/// <summary>
			/// Position of the file inside IMG stream
			/// </summary>
			long long position;
			/// <summary>
			/// Real length of the file inside
			/// </summary>
			long long length;
			/// <summary>
			/// Length of the file inside IMG stream
			/// </summary>
			long long clength;
			/// <summary>
			/// true if the file is compressed
			/// </summary>
			bool compressed;
			/// <summary>
			/// true if the file is loaded in stream-mode and not directly
			/// </summary>
			bool streamed;
		};

		/// <summary>
		/// Readable and x86 aligned structure from IDX
		/// </summary>
	private:
		class FileIdx
		{
			/// <summary>
			/// 32-bit hash of filename
			/// Use CalculateHash32 to get it
			/// </summary>
		public:
			int hash32;
			/// <summary>
			/// 16-bit hash of filename
			/// Use CalculateHash16 to get it
			/// </summary>
			int hash16;
			/// <summary>
			/// Real position of file inside the IMG, 2048 bytes aligned
			/// </summary>
			long long position;
			/// <summary>
			/// Length of uncompressed file
			/// </summary>
			long long length;
			/// <summary>
			/// Length of file inside the IMG, 2048 bytes aligned
			/// </summary>
			long long clength;
			/// <summary>
			/// Flag that explains if the current file is compressed
			/// </summary>
			bool compressed;
			/// <summary>
			/// Flag that explains if the current file is able to stream data
			/// </summary>
			bool streamed;
		};

		/// <summary>
		/// Calculate a 32-bit has from a string.
		/// SLPM_66675.ELF: where this code was extracted? I forgot it lol
		/// </summary>
		/// <param name="str">filename</param>
		/// <returns>32-bit hash</returns>
	private:
		static int CalculateHash32(const std::wstring &str);

		/// <summary>
		/// Calculate a 32-bit has from a string.
		/// SLPM_66675.ELF: where this code was extracted? This was discovered from Crazycat
		/// </summary>
		/// <param name="str">filename</param>
		/// <returns>32-bit hash</returns>
		static int CalculateHash16(const std::wstring &str);

		Stream *streamImg;
		int filesCount;
//C# TO C++ CONVERTER WARNING: Since the array size is not known in this declaration, C# to C++ Converter has converted this array to a pointer.  You will need to call 'delete[]' where appropriate:
//ORIGINAL LINE: FileIdx[] idx;
		FileIdx *idx;

		/// <summary>
		/// Parse an IDX file
		/// </summary>
		/// <param name="streamIdx">stream that contains IDX data</param>
		/// <param name="streamImg">stream that contains IMG data</param>
	public:
		IDX(System::IO::Stream *streamIdx, System::IO::Stream *streamImg);

		/// <summary>
		/// Open a file inside IDX / IMG
		/// </summary>
		/// <param name="filename">name of the file to open</param>
		/// <returns>file entry, compatible with KhStream</returns>
		FileEntry OpenFile(const std::wstring &filename);

		/// <summary>
		/// Search the hashes inside the IDX structure
		/// </summary>
		/// <param name="hash32">32-bit hash to search</param>
		/// <param name="hash16">16-bit hash to search</param>
		/// <returns>IDX index that contains the file description</returns>
	private:
		int SearchHashes(int hash32, int hash16);

	private:
		void InitializeInstanceFields();
	};
}
