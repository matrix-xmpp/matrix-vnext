using System;
using System.Threading;

namespace Matrix.Core
{
    /// <summary>
    /// Generates unique ids for XMPP stanzas
    /// </summary>
    public class Id
    {
        private static long     id;
        private static string	prefix	   = "MX_";
        private static IdType   idType     = IdType.ShortGuid;

        private static readonly object IdLock = new object();

        public static IdType Type
        {
            get { return idType; }
            set { idType = value; }
        }

		public static string GetNextId()
		{
		    var ret = String.Empty;
            switch (idType)
            {
                case IdType.Numeric:
                    ret += prefix;
                    ret += Interlocked.Increment(ref id);
                    break;
                case IdType.Guid:
                    ret += Guid.NewGuid().ToString();
                    break;
                case IdType.ShortGuid:
                    ret += Convert
                                .ToBase64String(Guid.NewGuid().ToByteArray())
                                .Substring(0, 22);
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

		/// <summary>
		/// to Save Bandwidth on Mobile devices you can change the prefix
		/// null is also possible to optimize Bandwidth usage
		/// </summary>
		public static string Prefix
		{
			get { return prefix; }
			set { prefix = value ?? string.Empty; }
		}
	}    
}