using System;
using System.Threading;

namespace Matrix.Core
{
    /// <summary>
    /// Id
    /// </summary>
    public class Id
    {
        private static long     _id;
        private static string	_prefix	    = "MX_";
        private static IdType   _idType     = IdType.Numeric;

        private static readonly object IdLock = new object();

        public static IdType Type
        {
            get { return _idType; }
            set { _idType = value; }
        }

		public static string GetNextId()		
        {
            lock(IdLock)
            { 
                if (_idType == IdType.Numeric)
                    return _prefix + Interlocked.Increment(ref _id);
            
                return _prefix + Guid.NewGuid();
            }
		}

        /// <summary>
		/// Reset the id counter to MX_1 again
		/// </summary>
		public static void Reset()
		{
			_id = 0;
		}

		/// <summary>
		/// to Save Bandwidth on Mobile devices you can change the prefix
		/// null is also possible to optimize Bandwidth usage
		/// </summary>
		public static string Prefix
		{
			get { return _prefix; }
			set { _prefix = value ?? string.Empty; }
		}
	}    
}