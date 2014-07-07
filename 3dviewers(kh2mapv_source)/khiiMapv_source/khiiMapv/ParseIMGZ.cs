using System.Collections.Generic;
using System.IO;

namespace khiiMapv
{
    public class ParseIMGZ
    {
        /// <summary>
        /// Function that will parse IMGZ files
        /// </summary>
        /// <param name="bin">Byte array of the IMGZ to process</param>
        /// <returns>List of IMGD files into a byte array</returns>
        public static PicIMGD[] TakeIMGZ(byte[] bin)
        {
            var memoryStream = new MemoryStream(bin, false);
            var binaryReader = new BinaryReader(memoryStream);
            var list = new List<PicIMGD>();
            memoryStream.Position = 12L;
            int num = binaryReader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                int index = binaryReader.ReadInt32();
                int count = binaryReader.ReadInt32();
                var si = new MemoryStream(bin, index, count, false);
                PicIMGD item = ParseIMGD.TakeIMGD(si);
                list.Add(item);
            }
            return list.ToArray();
        }
    }
}