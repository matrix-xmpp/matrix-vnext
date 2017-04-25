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

namespace Matrix.Xmpp.Muc
{
    /// <summary>
    /// XEP-0249: Direct MUC Invitations
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.XConference)]
    public class Conference : XmppXElementWithJidAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Conference"/> class.
        /// </summary>
        public Conference()
            : base(Namespaces.XConference, "x")
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Conference"/> class.
        /// </summary>
        /// <param name="jid">The jid of the conference room.</param>
        public Conference(Jid jid) : this()
        {
            Jid = jid;
        }

        /// <summary>
        /// specifies a password needed for entry into a password-protected room.
        /// </summary>
        public string Password
        {
            get { return GetAttribute("password"); }
            set { SetAttribute("password", value); }
        }

        /// <summary>
        /// specifies a human-readable purpose for the invitation.
        /// </summary>
        public string Reason
        {
            get { return GetAttribute("reason"); }
            set { SetAttribute("reason", value); }
        }
    }
}
