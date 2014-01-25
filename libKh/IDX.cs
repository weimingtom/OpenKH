using System;
using System.Collections.Generic;
using System.IO;

namespace Kh
{
    public class IDX
    {
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
            public long vlength;
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
        struct FileIdx
        {
            public int hash32;
            public int hash16;
            public int position;
            public int vlength;
            public int length;
            public bool compressed;
            public bool streamed;
        }

        static int CalculateHash32(string str)
        {
            return 0;
        }
        static int CalculateHash16(string str)
        {
            return 0;
        }
        private static int ByteToInt(byte[] array)
        {
            int n = 0;
            for (int i = 0; i < array.Length; i++)
                n |= array[i] << (8 * i);
            return n;
        }

        Stream streamImg;
        int filesCount;
        FileIdx[] idx;

        public IDX(System.IO.Stream streamIdx, System.IO.Stream streamImg)
        {
            byte[] data = new byte[4];
            streamIdx.Read(data, 0, data.Length);
            filesCount = ByteToInt(data);
            idx = new FileIdx[filesCount];
            for (int i = 0; i < filesCount; i++)
            {
                // TODO read the idx structure inside FileIdx[]
            }
            this.streamImg = streamImg;
        }

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
                entry.vlength = idx[entry.index].vlength;
                entry.clength = idx[entry.index].length;
                entry.compressed = idx[entry.index].compressed;
                entry.streamed = idx[entry.index].streamed;
            }
            return entry;
        }

        private int SearchHashes(int hash32, int hash16)
        {
            for (int i = 0; i < filesCount; i++)
            {
                if (idx[i].hash32 == hash32 &&
                    idx[i].hash16 == hash16)
                    return 0;
            }
            return -1;
        }
    }
}
