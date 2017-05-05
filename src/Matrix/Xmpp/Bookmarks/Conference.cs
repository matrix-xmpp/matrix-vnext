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

namespace Matrix.Xmpp.Bookmarks
{
    /// <summary>
    /// represents a conference bookmark.
    /// </summary>
    [XmppTag(Name = "conference", Namespace = Namespaces.StorageBookmarks)]
    public class Conference : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Conference"/> class.
        /// </summary>
        public Conference() : base(Namespaces.StorageBookmarks, "conference")
        {
        }

        /// <summary>
        /// A name/description for this bookmarked room
        /// </summary>
        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Should the client join this room automatically after successfuil login?
        /// </summary>
        public bool AutoJoin
        {
            get { return GetAttributeBool("autojoin"); }
            set { SetAttribute("autojoin", value); }
        }

        /// <summary>
        /// The Jid of the bookmarked room
        /// </summary>
        public Jid Jid
        {
            get { return GetAttributeJid("jid"); }
            set { SetAttribute("jid", value); }
        }

        /// <summary>
        /// The Nickname for this room
        /// </summary>
        public string Nickname
        {
            get { return GetTag("nick"); }
            set { SetTag("nick", value); }
        }

        /// <summary>
        /// The password for password protected rooms
        /// </summary>
        public string Password
        {
            get { return GetTag("password"); }
            set { SetTag("password", value); }
        }
    }
}
