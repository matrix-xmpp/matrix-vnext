using System.IO;

namespace Matrix.IO.Compression 
{
    internal class CompressionHelper
    {
        const int BufferSize = 4096;

        public static byte[] Compress(Deflater deflater,  byte[] bIn)
        {
            lock (deflater)
            {
                // The Flush SHOULD be after each STANZA
                // The lib sends always one complete XML Element/stanza,
                // it doesn't cache stanza and send them in groups, and also doesnt send partial
                // stanzas. So everything should be ok here.
                deflater.SetInput(bIn);
                deflater.Flush();
                
                using (var ms = new MemoryStream())
                {
                    int ret;
                    do
                    {
                        byte[] buf = new byte[BufferSize];
                        ret = deflater.Deflate(buf);
                        if (ret > 0)
                            ms.Write(buf, 0, ret);

                    } while (ret > 0);
                    return ms.ToArray();
                }
            }
        }

        /// <summary>
        /// Compress bytes
        /// </summary>
        /// <param name="bIn"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] bIn)
        {
            return Compress(new Deflater(), bIn);
        }

        /// <summary>
        /// Decompress bytes
        /// </summary>
        /// <param name="bIn">The b in.</param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] bIn)
        {
            return Decompress(new Inflater(), bIn, bIn.Length);
        }

        public static byte[] Decompress(Inflater inflater, byte[] bIn)
        {
            return Decompress(inflater, bIn, bIn.Length);
        }

        public static byte[] Decompress(Inflater inflater, byte[] bIn, int length)
        {
            lock (inflater)
            {
                inflater.SetInput(bIn, 0, length);

                using (var ms = new MemoryStream())
                {
                    int ret;
                    do
                    {
                        byte[] buf = new byte[BufferSize];
                        ret = inflater.Inflate(buf);
                        if (ret > 0)
                            ms.Write(buf, 0, ret);

                    } while (ret > 0);

                    return ms.ToArray();
                }
            }
        }
    }
}