using System;
using System.Threading;

namespace Matrix.Core
{
    /// <summary>
    /// Id
    /// </summary>
    public class Id
    {

        private static long     m_id;
        private static string	m_Prefix	= "MX_";
        private static IdType   m_IdType    = IdType.Numeric;

        private static object idLock = new object();

        public static IdType Type
        {
            get { return m_IdType; }
            set { m_IdType = value; }
        }

		public static string GetNextId()		
        {
            lock(idLock)
            { 
                if (m_IdType == IdType.Numeric)
                    return m_Prefix + Interlocked.Increment(ref m_id);
            
                return m_Prefix + Guid.NewGuid();
            }
		}

        /// <summary>
		/// Reset the id counter to MX_1 again
		/// </summary>
		public static void Reset()
		{
			m_id = 0;
		}

		/// <summary>
		/// to Save Bandwidth on Mobile devices you can change the prefix
		/// null is also possible to optimize Bandwidth usage
		/// </summary>
		public static string Prefix
		{
			get { return m_Prefix; }
			set { m_Prefix = value ?? ""; }
		}
	}    
}