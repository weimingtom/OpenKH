using System;
using System.Collections.Generic;
using System.IO;

namespace Kh
{
    /// <summary>
    /// Process IDX of Kingdom Hearts 1
    /// </summary>
    public class IDX1
    {
        /// <summary>
        /// Readable and x86 aligned structure from IDX
        /// </summary>
        struct FileIdx
        {
            /// <summary>
            /// 32-bit hash of filename
            /// Use CalculateHash to get it
            /// </summary>
            public int hash;
            /// <summary>
            /// flags that describes how the entry is managed
            /// </summary>
            public long flags;
            /// <summary>
            /// Real position of file inside the IMG, 2048 bytes aligned
            /// </summary>
            public long position;
            /// <summary>
            /// Length of uncompressed file
            /// </summary>
            public long length;
        }

        /// <summary>
        /// Calculate a 32-bit has from a string.
        /// </summary>
        /// <param name="str">filename</param>
        /// <returns>32-bit hash</returns>
        public static int CalculateHash(string str)
        {
            int hash = 0;
            for (int i = 0, ch; i < str.Length; i++)
            {
                ch = str[i];
                hash = (2 * hash) ^ ((ch << 16) % 69665);
            }
            return hash;
        }

        Stream streamImg;
        int filesCount;
        FileIdx[] idx;

        /// <summary>
        /// Parse an IDX file
        /// </summary>
        /// <param name="streamIdx">stream that contains IDX data</param>
        /// <param name="streamImg">stream that contains ISO data</param>
        public IDX1(System.IO.Stream streamIdx, System.IO.Stream streamImg)
        {
            BinaryReader reader = new BinaryReader(streamIdx);

            // First 4 bytes are the entries count
            filesCount = (int)streamIdx.Length / 0x10;
            idx = new FileIdx[filesCount];

            // Parse IDX file
            for (int i = 0; i < filesCount; i++)
            {
                idx[i].hash = reader.ReadInt32();
                idx[i].flags = reader.ReadInt32();
                idx[i].position = reader.ReadInt32();
                idx[i].length = reader.ReadInt32();
            }

            // Get the stream of IMG
            this.streamImg = streamImg;
        }

        /// <summary>
        /// Get stream from the specified filename
        /// </summary>
        /// <param name="filename">filename</param>
        /// <returns></returns>
        public Stream OpenFile(string filename)
        {
            int hash1 = CalculateHash(filename);
            int index = SearchHash(hash1);

            if (index >= 0)
            {
                if ((idx[index].flags & 1) != 0)
                {
                    return Uncompress(streamImg, idx[index].position, -1, idx[index].length);
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

        /// <summary>
        /// Check if the specified file exists
        /// </summary>
        /// <param name="filename">filename</param>
        /// <returns></returns>
        public bool ExistFile(string filename)
        {
            int hash1 = CalculateHash(filename);
            int index = SearchHash(hash1);
            return index != -1;
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
            }
            return new MemoryStream(dstData);
        }

        /// <summary>
        /// Search the hashes inside the IDX structure
        /// </summary>
        /// <param name="hash">32-bit hash to search</param>
        /// <returns>IDX index that contains the file description</returns>
        public int SearchHash(int hash)
        {
            int start = 0;
            int end = idx.Length - 1;
            while (start <= end)
            {
                int center = start + ((end - start) >> 1);
                int result = idx[center].hash - hash;

                if (result < 0)
                {
                    start = center + 1;
                }
                else if (result > 0)
                {
                    end = center - 1;
                }
                else
                    return center;
            }
            return -1;
        }
    }
}
