namespace LIBKH
{
        /// <summary>
        ///     Shared methods between OpenKH classes
        /// </summary>
        public class DATA
        {
            /// <summary>
            ///     Annoying method that converts a portion of bytes to an integer value
            /// </summary>
            /// <param name="array">array where to extract the integer value</param>
            /// <param name="offset">start index</param>
            /// <param name="length">number of elements to process</param>
            /// <returns>integer value</returns>
            public static int ByteToInt(byte[] array, int offset, int length)
            {
                int n = 0;
                for (int i = 0; i < length; i++)
                    n |= array[offset + i] << (8*i);
                return n;
            }
        }
    }