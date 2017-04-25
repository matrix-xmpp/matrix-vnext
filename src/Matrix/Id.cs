/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

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
