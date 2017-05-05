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

using Matrix.Attributes;

namespace Matrix.Xmpp.Oob
{
    /*
        <message from='stpeter@jabber.org/work'
             to='MaineBoy@jabber.org/home'>
          <body>Yeah, but do you have a license to Jabber?</body>
          <x xmlns='jabber:x:oob'>
            <url>http://www.jabber.org/images/psa-license.jpg</url>
          </x>
        </message>
     */

    /// <summary>
    /// XEP-0066: Out of Band Data
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.XOob)]
    public class XOob : Oob
    {
        public XOob()
            : base(Namespaces.XOob, "x")
        {
        }
    }
}
