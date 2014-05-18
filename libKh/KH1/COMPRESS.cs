using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libKh.KH1
{
    /// <summary>
    ///     Process the compression and decompressions algorithm of KH1.
    /// </summary>
    class COMPRESS
    {
        /// <summary>How far back to search for matches</summary>
        private const byte bufferSize = 255;

        /// <summary>Maximum characters to match - 3</summary>
        private const byte maxMatch = 255;
        private static byte findLeastByte(IEnumerable<byte> data)
        {
            var cnt = new uint[256];
            foreach (var i in data) { ++cnt[i]; }
            uint fC = UInt32.MaxValue;
            byte f = 0x13;
            for (int i = 0; i < cnt.Length; ++i)
            {
                if (cnt[i] < fC)
                {
                    f = (byte)i;
                    fC = cnt[i];
                    if (fC == 0) { break; }
                }
            }
            return f;
        }
        /// <summary>
        ///     Compression method for KH1.
        /// </summary>
        /// <param name="input">Byte array of the file to compress</param>
        /// <returns>Compressed file as a byte array</returns>
        public static byte[] compress(byte[] input)
        {
            // Compressed format has max of 3 bytes for length
            if (input.Length > 0xFFFFFF)
            {
                throw new Exception("Source too big");
            }
            // 9 bytes is the absolute smallest that can be compressed. "000000000" -> "+++0LLLF".
            if (input.Length < 9)
            {
                throw new Exception("Source too small");
            }
            byte flag = findLeastByte(input);    // Get the least-used byte for a flag
            int i = input.Length,                // Input position
                o = i - 5;                       // Output position (-5 for the 4 bytes added at the end + 1 byte smaller then input minimum)
            var outbuf = new byte[o];         // Output buffer (since we can't predict how well the file will compress)
            while (--i >= 0 && --o >= 0)
            {
                if (o >= 2)
                {
                    /*Attempt compression*/
                    int buffEnd = input.Length <= i + bufferSize ? input.Length : i + bufferSize + 1;
                    int mLen = 3;   //minimum = 4, so init this to 3
                    byte mPos = 0;
                    for (int j = i + 1; j < buffEnd; ++j)
                    {
                        int cnt = 0;
                        while (i >= cnt && input[j - cnt] == input[i - cnt])
                        {
                            if (++cnt == maxMatch + 3)
                            {
                                mLen = maxMatch + 3;
                                mPos = (byte)(j - i);
                                j = buffEnd;    // Break out of for loop
                                break;          // Break out of while loop
                            }
                        }
                        if (cnt > mLen) { mLen = cnt; mPos = (byte)(j - i); }
                    }
                    if (mLen > 3)
                    {
                        outbuf[o] = flag;
                        outbuf[--o] = mPos;
                        outbuf[--o] = (byte)(mLen - 3);
                        i -= (mLen - 1);
                        continue;
                    }
                }

                if ((outbuf[o] = input[i]) == flag) // No match was made, so copy the byte
                {
                    if (--o < 0)
                    {
                        break;  // There's not enough room to store the literal
                    }
                    outbuf[o] = 0;  // Output 0 to mean the byte is literal, and not a flag
                }
            }
            if (o < 0)
            {
                throw new Exception("Compressed data is as big as original");
            }

            // get length of compressed data (-1 for minimum 1 byte smaller)
            i = input.Length - o - 1;
            var output = new byte[i];
            Array.Copy(outbuf, o, output, 0, i - 4);
            output[i - 4] = (byte)(input.Length >> 16);
            output[i - 3] = (byte)(input.Length >> 8);
            output[i - 2] = (byte)(input.Length);
            output[i - 1] = flag;
            Console.WriteLine("  Compressed to {0:0%} of the original size!", (double)i / input.Length);
            return output;
        }
        /// <summary>
        ///     Decompression method for KH1.
        /// </summary>
        /// <param name="input">Byte array of the file to decompress</param>
        /// <returns>Decompressed file as a byte array</returns>
        public static byte[] decompress(byte[] input)
        {
            double Length = input.Length;
            checked
            {
                int ucI = input[(int)(unchecked(Length - 2))] | (input[(int)(unchecked(Length - 3))] | input[(int)(unchecked(Length - 4))] << 8) << 8;
                double UncompressedLength = ucI;
                var output = new byte[(int)(UncompressedLength)];
                var num3 = (int)(unchecked(Length - 5));
                byte b = input[(int)(unchecked(Length - 1))];
                while (true)
                {
                    int num4;
                    num3 = (num4 = num3) - 1;
                    byte b2;
                    if ((b2 = input[num4]) != b)
                    {
                        goto IL_F2;
                    }
                    int num5;
                    num3 = (num5 = num3) - 1;
                    int num6;
                    if ((num6 = input[num5]) == 0)
                    {
                        goto IL_F2;
                    }
                    int num7;
                    num3 = (num7 = num3) - 1;
                    int num8 = input[num7] + 3;
                    num6 += ucI;
                    while (true)
                    {
                        int num9;
                        num8 = (num9 = num8) - 1;
                        if (num9 <= 0)
                        {
                            break;
                        }
                        if (ucI < 1)
                        {
                            goto Block_3;
                        }
                        output[--ucI] = output[--num6];
                    }
                IL_106:
                    if (ucI <= 0)
                    {
                        break;
                    }
                    continue;
                IL_F2:
                    if (ucI > 0)
                    {
                        output[--ucI] = b2;
                    }
                    goto IL_106;
                }
            Block_3:
                return output;
            }
        }
    }
}
