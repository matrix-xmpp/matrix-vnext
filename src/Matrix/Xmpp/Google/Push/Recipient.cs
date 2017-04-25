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
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Google.Push
{
    #region Xml sample
    /*
    <message from="cloudprint.google.com" to="{FULL_JID">
     <push xmlns="google:push" channel="cloudprint.google.com">
       <recipient to="{BARE_JID}">{raw data, ignore}</recipient>
       <data>{base-64 encoded printer id}</data>
     </push>
    </message>
    */
    #endregion

    [XmppTag(Name = "recipient", Namespace = Namespaces.GooglePush)]
    public class Recipient : XmppXElementWithAddress
    {
        public Recipient(): base(Namespaces.GooglePush, "recipient")
        {}
    }
}
