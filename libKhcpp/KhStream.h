#pragma once

#include "IDX.h"
#include <string>


namespace Kh
{
	class FileStream : public System::IO::Stream
	{
	private:
		IDX::FileEntry entry;

		long long position;

		/// <summary>
		/// Open a Kingdom Hearts' file stream
		/// </summary>
		/// <param name="idx">IDX object</param>
		/// <param name="filename">name of the file to open</param>
	public:
		FileStream(IDX *idx, const std::wstring &filename);

		virtual const long long &getPosition() const override;
		virtual void setPosition(const long long &value) override;
		virtual const long long &getLength() const override;
		virtual const bool &getCanRead() const override;
		virtual const bool &getCanSeek() const override;
		virtual const bool &getCanWrite() const override;

		virtual int Read(unsigned char buffer[], int offset, int count) override;
		virtual void Write(unsigned char buffer[], int offset, int count) override;
		virtual void Flush() override;
		virtual void SetLength(long long value) override;

		virtual long long Seek(long long offset, SeekOrigin origin) override;

	private:
		void InitializeInstanceFields();
	};
}
