using System;
using System.Threading;

namespace Matrix
{
    /// <summary>
    /// Generates unique ids for XMPP stanzas
    /// </summary>
    public class Id
    {
        private static long id;
        
        public static IdType Type { get; set; } = IdType.ShortGuid;

        /// <summary>
        /// to Save Bandwidth on Mobile devices you can change the prefix
        /// null is also possible to optimize Bandwidth usage
        /// </summary>
        public static string Prefix { get; set; } = "MX_";

        public static string GetNextId()
		{
		    var ret = String.Empty;
            switch (Type)
            {
                case IdType.Numeric:
                    ret += Prefix;
                    ret += Interlocked.Increment(ref id);
                    break;
                case IdType.Guid:
                    ret += Guid.NewGuid().ToString();
                    break;
                case IdType.ShortGuid:
                    ret += GenerateShortGuid();
                    break;
            }
		    return ret;
		}

        /// <summary>
		/// Reset the id counter to MX_1 again
		/// </summary>
		public static void Reset()
		{
			id = 0;
		}

        public static string GenerateShortGuid()
        {
            return Convert
                .ToBase64String(Guid.NewGuid().ToByteArray())
                .Substring(0, 22);
        }
    }    
}
