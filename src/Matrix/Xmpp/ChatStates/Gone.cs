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
using Matrix.Xml;

namespace Matrix.Xmpp.Chatstates
{
    /// <summary>
    /// User has effectively ended their participation in the chat session.
    /// User has not interacted with the chat interface, system, or device for a relatively long period of time 
    /// (e.g., 2 minutes), or has terminated the chat interface (e.g., by closing the chat window).
    /// </summary>
    [XmppTag(Name = "gone", Namespace = Namespaces.Chatstates)]
    public class Gone : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gone"/> class.
        /// </summary>
        public Gone()
            : base(Namespaces.Chatstates, Chatstate.Gone.ToString().ToLower())
        {
        }
    }
}
