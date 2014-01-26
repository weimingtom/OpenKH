using System;
using System.Collections.Generic;
using System.IO;

namespace Kh
{
    /// <summary>
    /// Process IDX files
    /// </summary>
    public class IDX
    {
        /// <summary>
        /// Readable and x86 aligned structure from IDX
        /// </summary>
        struct FileIdx
        {
            /// <summary>
            /// 32-bit hash of filename
            /// Use CalculateHash32 to get it
            /// </summary>
            public int hash32;
            /// <summary>
            /// 16-bit hash of filename
            /// Use CalculateHash16 to get it
            /// </summary>
            public int hash16;
            /// <summary>
            /// Real position of file inside the IMG, 2048 bytes aligned
            /// </summary>
            public long position;
            /// <summary>
            /// Length of uncompressed file
            /// </summary>
            public long length;
            /// <summary>
            /// Length of file inside the IMG, 2048 bytes aligned
            /// </summary>
            public long clength;
            /// <summary>
            /// Flag that explains if the current file is compressed
            /// </summary>
            public bool compressed;
            /// <summary>
            /// Flag that explains if the current file is able to stream data
            /// </summary>
            public bool streamed;
        }

        /// <summary>
        /// Calculate a 32-bit has from a string.
        /// SLPM_66675.ELF: where this code was extracted? I forgot it lol
        /// </summary>
        /// <param name="str">filename</param>
        /// <returns>32-bit hash</returns>
        static int CalculateHash32(string str)
        {
            int c = -1;

            int strIndex = 0;
            do
            {
                c ^= (int)str[strIndex] << 24;
                for (int i = 8; i > 0; i--)
                {
                    if (c < 0)
                        c = (c << 1) ^ 0x4C11DB7;
                    else
                        c <<= 1;
                }
                strIndex++;
            } while (strIndex < str.Length);

            return ~c;
        }

        /// <summary>
        /// Calculate a 32-bit has from a string.
        /// SLPM_66675.ELF: where this code was extracted? This was discovered from Crazycat
        /// </summary>
        /// <param name="str">filename</param>
        /// <returns>32-bit hash</returns>
        static int CalculateHash16(string str)
        {
            int s1 = -1;
            int length = str.Length;
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

        Stream streamImg;
        int filesCount;
        FileIdx[] idx;

        /// <summary>
        /// Parse an IDX file
        /// </summary>
        /// <param name="streamIdx">stream that contains IDX data</param>
        /// <param name="streamImg">stream that contains IMG data</param>
        public IDX(System.IO.Stream streamIdx, System.IO.Stream streamImg)
        {
            BinaryReader reader = new BinaryReader(streamIdx);

            // First 4 bytes are the entries count
            filesCount = reader.ReadInt32();
            idx = new FileIdx[filesCount];

            // Parse IDX file
            for (int i = 0; i < filesCount; i++)
            {
                idx[i].hash32 = reader.ReadInt32();
                idx[i].hash16 = reader.ReadInt16();
                idx[i].clength = reader.ReadInt16();
                idx[i].position = reader.ReadInt32();
                idx[i].length = reader.ReadInt32();

                idx[i].compressed = (idx[i].clength & 0x4000) != 0;
                idx[i].streamed = (idx[i].clength & 0x8000) != 0;
                idx[i].clength = (idx[i].clength & 0x3FFF) * 0x800 + 0x800;
                idx[i].position *= 0x800;
            }

            // Get the stream of IMG
            this.streamImg = streamImg;
        }

        /// <summary>
        /// Get stream from the specified filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Stream OpenFile(string filename)
        {
            int hash1 = CalculateHash32(filename);
            int hash2 = CalculateHash16(filename);
            int index = SearchHashes(hash1, hash2);

            if (index >= 0)
            {
                if (idx[index].compressed == true)
                {
                    return Uncompress(streamImg, idx[index].position,
                        idx[index].clength, idx[index].length);
                }
                else
                {
                    byte[] data = new byte[idx[index].length];
                    streamImg.Position = idx[index].position;
                    streamImg.Read(data, 0, data.Length);
                    return new MemoryStream(data);
                }
            }
            throw new System.IO.FileNotFoundException();
        }

        private Stream Uncompress(Stream streamIn, long offset, long srcSize, long dstSize)
        {
            byte[] dstData = new byte[dstSize];

            // srcSize needs to be 2048 bytes aligned in order to work
            if ((srcSize & 0x7FF) == 0)
            {
                byte[] srcData = new byte[srcSize];
                streamIn.Position = offset + srcSize - srcData.Length;
                streamIn.Read(srcData, 0, srcData.Length);

                long srcPos = srcSize;
                long dstPos = dstSize;
                byte key = 0;

                // Find decompression key from the end of file 
                while (srcPos > 0)
                {
                    // The key is never 0
                    byte data = srcData[--srcPos];
                    if (data != 0x00)
                    {
                        key = data;
                        break;
                    }
                }

                // Skip file length
                srcPos -= 4;

                while (srcPos > 0 && dstPos > 0)
                {
                    byte data = srcData[--srcPos];
                    if (data == key)
                    {
                        byte copyPos = srcData[--srcPos];
                        if (copyPos != 0)
                        {
                            for (int i = srcData[--srcPos] + 2; i >= 0; i--)
                            {
                                if (dstPos > 0)
                                    dstData[--dstPos] = dstData[dstPos + copyPos];
                                else
                                    break;
                            }
                        }
                        else
                        {
                            dstData[--dstPos] = data;
                        }
                    }
                    else
                    {
                        dstData[--dstPos] = data;
                    }
                }
                srcPos = srcPos;
            }
            return new MemoryStream(dstData);
        }

        /// <summary>
        /// Search the hashes inside the IDX structure
        /// </summary>
        /// <param name="hash32">32-bit hash to search</param>
        /// <param name="hash16">16-bit hash to search</param>
        /// <returns>IDX index that contains the file description</returns>
        private int SearchHashes(int hash32, int hash16)
        {
            for (int i = 0; i < filesCount; i++)
            {
                if (idx[i].hash32 == hash32 &&
                    idx[i].hash16 == hash16)
                    return i;
            }
            return -1;
        }
    }
}
