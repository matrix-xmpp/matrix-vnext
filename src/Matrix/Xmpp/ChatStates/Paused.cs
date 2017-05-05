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
    /// User had been composing but now has stopped.
    /// User was composing but has not interacted with the message input interface for a short period of time (e.g., 5 seconds).
    /// </summary>
    [XmppTag(Name = "paused", Namespace = Namespaces.Chatstates)]
    public class Paused : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Paused"/> class.
        /// </summary>
        public Paused()
            : base(Namespaces.Chatstates, Chatstate.Paused.ToString().ToLower())
        {
        }
    }
}
