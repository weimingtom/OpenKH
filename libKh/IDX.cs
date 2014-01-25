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
        /// Used to get a file entry from IDX
        /// </summary>
        public struct FileEntry
        {
            /// <summary>
            /// Filename of file entry
            /// </summary>
            public string filename;
            /// <summary>
            /// 32-bit hash of filename
            /// </summary>
            public int hash1;
            /// <summary>
            /// 16-bit hash of filename
            /// </summary>
            public int hash2;
            /// <summary>
            /// IMG stream
            /// </summary>
            public Stream stream;
            /// <summary>
            /// Index of file entry on IDX
            /// </summary>
            public int index;
            /// <summary>
            /// Position of the file inside IMG stream
            /// </summary>
            public long position;
            /// <summary>
            /// Real length of the file inside
            /// </summary>
            public long length;
            /// <summary>
            /// Length of the file inside IMG stream
            /// </summary>
            public long clength;
            /// <summary>
            /// true if the file is compressed
            /// </summary>
            public bool compressed;
            /// <summary>
            /// true if the file is loaded in stream-mode and not directly
            /// </summary>
            public bool streamed;
        }

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
            public int position;
            /// <summary>
            /// Length of uncompressed file
            /// </summary>
            public int length;
            /// <summary>
            /// Length of file inside the IMG, 2048 bytes aligned
            /// </summary>
            public int clength;
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

        /// <summary>
        /// Annoying method that converts a portion of bytes to an integer value
        /// </summary>
        /// <param name="array">array where to extract the integer value</param>
        /// <param name="offset">start index</param>
        /// <param name="length">number of elements to process</param>
        /// <returns>integer value</returns>
        private static int ByteToInt(byte[] array, int offset, int length)
        {
            int n = 0;
            for (int i = 0; i < length; i++)
                n |= array[offset + i] << (8 * i);
            return n;
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
            // First 4 bytes are the entries count
            byte[] data = new byte[4];
            streamIdx.Read(data, 0, data.Length);
            filesCount = ByteToInt(data, 0, 4);
            idx = new FileIdx[filesCount];

            // Parse IDX file
            byte[] dataIdx = new byte[0x10];
            for (int i = 0; i < filesCount; i++)
            {
                streamIdx.Read(dataIdx, 0, dataIdx.Length);
                idx[i].hash32 = ByteToInt(dataIdx, 0, 4);
                idx[i].hash16 = ByteToInt(dataIdx, 4, 2);
                idx[i].clength = ByteToInt(dataIdx, 6, 2);
                idx[i].position = ByteToInt(dataIdx, 8, 4);
                idx[i].length = ByteToInt(dataIdx, 12, 4);

                idx[i].streamed = (idx[i].clength & 0x4000) != 0;
                idx[i].compressed = (idx[i].clength & 0x8000) != 0;
                idx[i].clength = (idx[i].clength & 0x3FFF) * 0x800;
                idx[i].position *= 0x800;
            }

            // Get the stream of IMG
            this.streamImg = streamImg;
        }

        /// <summary>
        /// Open a file inside IDX / IMG
        /// </summary>
        /// <param name="filename">name of the file to open</param>
        /// <returns>file entry, compatible with KhStream</returns>
        public FileEntry OpenFile(string filename)
        {
            FileEntry entry = new FileEntry();
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
