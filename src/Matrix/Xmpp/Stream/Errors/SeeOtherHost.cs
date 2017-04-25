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
using System.Globalization;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Stream.Errors
{
    [XmppTag(Name = "see-other-host", Namespace = Namespaces.Streams)]
    public class SeeOtherHost : XmppXElement
    {
        // <see-other-host xmlns="urn:ietf:params:xml:ns:xmpp-streams">BAYMSG1020127.gateway.edge.messenger.live.com</see-other-host>
        public SeeOtherHost() : base(Namespaces.Streams, "see-other-host")
        {
        }

        public string Hostname
        {
            get
            {
                if (!String.IsNullOrEmpty(Value))
                {
                    var split = Value.Split(':');
                    if (split.Length > 0)
                    {
                        return split[0];
                    }
                }
                return null;
            }
            set { Value = value + ":" + Port.ToString(CultureInfo.InvariantCulture); }
        }

        public int Port
        {
            get
            {
                if (!String.IsNullOrEmpty(Value))
                {
                    var split = Value.Split(':');
                    if (split.Length > 1)
                    {
                        return int.Parse(split[1]);
                    }
                }
                return 5222;
            }
            set { Value = Hostname + ":" + value; }
        }
    }
}
