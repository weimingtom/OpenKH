using System;
using System.Collections.Generic;
using System.IO;

namespace Kh
{
    public class Bar
    {
        const int MagicCode = 0x01524142;

        struct Header
        {
            public int magic;
            public int count;
            public int dunno1;
            public int dunno2;
        }
        struct Entry
        {
            public int type;
            public int name;
            public int position;
            public int size;
        }

        /// <summary>
        /// Type of files
        /// TODO INCOMPLETE LIST
        /// Thanks to GovanifY for this
        /// </summary>
        public enum Type
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
            IMGZ = 0x1D,
            BAR_4  = 0x1E,
            SEB = 0x1F,
            WD = 0x20,
            VSB = 0x22,
            TBMP = 0x24,
            BAR_5 = 0x2E,
            VIBD = 0x2F,
            VAG = 0x30

        } 

        Header header;
        Entry[] entries;
        Stream stream;

        public Bar(Stream stream)
        {
            byte[] data = new byte[0x10];
            stream.Read(data, 0, data.Length);

            header.magic = Data.ByteToInt(data, 0, 4);
            header.count = Data.ByteToInt(data, 4, 4);
            header.dunno1 = Data.ByteToInt(data, 8, 4);
            header.dunno2 = Data.ByteToInt(data, 12, 4);

            if (header.magic != MagicCode)
                throw new System.IO.InvalidDataException();

            if (header.count != 0)
            {
                entries = new Entry[header.count];
                for (int i = 0; i < header.count; i++)
                {
                    stream.Read(data, 0, data.Length);
                    entries[i].type = Data.ByteToInt(data, 0, 4);
                    entries[i].name = Data.ByteToInt(data, 4, 4);
                    entries[i].position = Data.ByteToInt(data, 8, 4);
                    entries[i].size = Data.ByteToInt(data, 12, 4);
                }
            }
            this.stream = stream;
        }

        /// <summary>
        /// Number of files inside BAR
        /// </summary>
        public int Count
        { get { return header.count; } }

        /// <summary>
        /// Check file type of specified file index inside BAR
        /// </summary>
        /// <param name="index">index of file; from 0 to Count</param>
        /// <returns>File type</returns>
        public Type GetType(int index)
        {
            return (Type)entries[index].type;
        }

        /// <summary>
        /// Check file name of specified file index inside BAR
        /// </summary>
        /// <param name="index">index of file; from 0 to Count</param>
        /// <returns>File name</returns>
        public string GetName(int index)
        {
            char[] ch = new char[4];
            int nName = entries[index].name;
            int strLength = 0;
            for (strLength = 0; strLength < ch.Length; strLength++)
            {
                ch[strLength] = (char)((nName >> (strLength * 8)) & 0xFF);
                if (ch[strLength] == '\0')
                    break;
            }
            return new String(ch, 0, strLength);
        }

        /// <summary>
        /// Get data from specified file index inside BAR
        /// </summary>
        /// <param name="index">index of file; from 0 to Count</param>
        /// <returns>stream data from specified file</returns>
        public Stream GetData(int index)
        {
            byte[] data = new byte[entries[index].size];
            stream.Position = entries[index].position;
            stream.Read(data, 0, data.Length);
            return new MemoryStream(data);
        }
    }
}
