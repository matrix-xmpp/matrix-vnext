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

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Software Version IQ, XEP-0092
    /// </summary>
    public class VersionIq : Iq
    {
        #region << Constructors >>
        public VersionIq()
        {
            Add(new Version.Version());
            GenerateId();
        }

        public VersionIq(IqType type)
            : this()
        {
            Type = type;
        }

        public VersionIq(Jid to)
            : this()
        {
            To = to;
        }

        public VersionIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        public VersionIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        public VersionIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Version object
        /// </summary>
        public Version.Version Version
        {
            get { return Element<Version.Version>(); }
            set { Replace(value); }
        }
    }
}
