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

namespace Matrix
{
    /// <summary>
    /// helper class for XMPP and utc time formats
    /// </summary>
    public class Time
    {
        /*
            <x xmlns="jabber:x:delay" from="..." stamp="20060303T15:43:08" />         
        */
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime JabberDate(string date)
        {
            if (String.IsNullOrEmpty(date))
                return DateTime.MinValue;

            // check if this is not a Jabber Date, but an Iso8601 date
            if (date.ToUpper().EndsWith(("Z")))
                return Iso8601Date(date);
            
            return ParseJabberDate(date);
        }

        /// <summary>
        /// parses the old Jabber style date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ParseJabberDate(string date)
        {
            // better put here a try catch in case a client sends a wrong formatted date
            try
            {
                var dt = new DateTime(int.Parse(date.Substring(0, 4)),
                                            int.Parse(date.Substring(4, 2)),
                                            int.Parse(date.Substring(6, 2)),
                                            int.Parse(date.Substring(9, 2)),
                                            int.Parse(date.Substring(12, 2)),
                                            int.Parse(date.Substring(15, 2))
                                          );

                return dt.ToLocalTime();
            }
            catch
            {
                return DateTime.MinValue;
            }   
        }
        
        /// <summary>
        /// Get a XMPP string representation of a Date        
        /// </summary>
        /// <param name="date">DateTime</param>
        /// <returns>XMPP string representation of a DateTime value</returns>
        public static string JabberDate(DateTime date)
        {
            return date.ToString("yyyyMMddTHH:mm:ss");
        }        

        /// <summary>
        /// The new standard used by XMPP in JEP-82 (ISO-8601)
        /// <example>1970-01-01T00:00Z</example>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime Iso8601Date(string date)
        {
            // .NET does a great Job parsing this Date profile
            try
            {
                return DateTime.Parse(date);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// The new standard used by XMPP in JEP-82 (ISO-8601)
        /// converts a local DateTime to a ISO-8601 formatted date in UTC format.
        /// <example>1970-01-01T00:00Z</example>
        /// </summary>
        /// <param name="date">local Datetime</param>
        /// <returns></returns>
        public static string Iso8601Date(DateTime date)
        {
            return Iso8601DateString(date.ToUniversalTime());            	
        }

        public static string Iso8601DateString(DateTime date)
        {
            return date.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        }

        // TODO TimeZone comes with a later NetStandard (1.7)
        /*
        public static TimeSpan UtcOffset()
        {
            var localZone = TimeZone.CurrentTimeZone;
            var currentDate = DateTime.Now;
            return localZone.GetUtcOffset(currentDate);

            // DST already contained in the GetUtcOffset result.
            //var dls = localZone.IsDaylightSavingTime(localZone.ToLocalTime(currentDate));
            //return new TimeSpan(dls ? currentOffset.Hours - 1 : currentOffset.Hours, currentOffset.Minutes, 0);
        }
        */

        internal static string FormatOffset(TimeSpan ts)
        {
            return String.Format("{0:00}:{1:00}", ts.Hours, Math.Abs(ts.Minutes));
        }
    }
}
