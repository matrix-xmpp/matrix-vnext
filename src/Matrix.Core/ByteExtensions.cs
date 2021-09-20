namespace Matrix
{
    using System;

    public static class ByteExtensions
    {
        /// <summary>
        /// Converts all bytes in the array to a HEX string representation.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string representation</returns>
        public static string ToHex(this byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty).ToLower();
        }
    }
}
