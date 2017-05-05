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
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Time
{
    /// <summary>
    /// XEP-0202: Entity Time
    /// </summary>
    [XmppTag(Name = "time", Namespace = Namespaces.Time)]
    public class Time : XmppXElement
    {
        public Time()
            : base(Namespaces.Time, "time")
        {
        }

        // TODO
        /// <summary>
        /// Gets or sets the UTC offset.
        /// </summary>
        /// <value>The UTC offset.</value>
        //public TimeSpan UtcOffset
        //{
        //    get
        //    {
        //        var tzo = GetTag("tzo");
        //        if (tzo == null)
        //            return TimeSpan.Zero;

        //        /* .NET is not able to parse the following format: "-03:30"
        //            so we append the minutes and .NET is happy.
                    
        //            +00:00 .NET does not like the plus
        //            -10:00 len:6
        //             10:00 len:5                    
        //        */

        //        if (tzo.StartsWith("+"))
        //            tzo = tzo.Substring(1);

        //        if (tzo.Length == 5 || tzo.Length == 6)
        //            tzo += ":00";
                
        //        return TimeSpan.Parse(tzo);
        //    }
        //    set { SetTag("tzo", Util.Time.FormatOffset(value)); }
        //}

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>The date time.</value>
        public DateTime DateTime
        {
            get { return Matrix.Time.Iso8601Date(GetTag("utc")); }
            set { SetTag("utc", Matrix.Time.Iso8601Date(value)); }
        }

        // TODO
        ///// <summary>
        ///// Sets the utc offset and time automatically.
        ///// </summary>
        //public void SetDateTimeNow()
        //{
        //    UtcOffset = Util.Time.UtcOffset();
        //    DateTime = DateTime.Now;
        //}

    }
}
