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

namespace Matrix.Xmpp.Roster
{
    /// <summary>
    /// RosterItem represents a contact object.
    /// </summary>
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.IqRoster)]
    public class RosterItem : Base.RosterItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RosterItem"/> class.
        /// </summary>
        public RosterItem() : base(Namespaces.IqRoster)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterItem"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        public RosterItem(Jid jid)
            : this()
        {
            Jid = jid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterItem"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <param name="name">The name.</param>
        public RosterItem(Jid jid, string name)
            : this(jid)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RosterItem"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <param name="name">The name.</param>
        /// <param name="group">The group.</param>
        public RosterItem(Jid jid, string name, string group)
            : this(jid, name)
        {
            AddGroup(group);
        }

        /// <summary>
        /// Gets or sets the subscription.
        /// </summary>
        /// <value>The subscription.</value>
        public Subscription Subscription
        {
            get { return GetAttributeEnum<Subscription>("subscription"); }
            set { SetAttribute("subscription", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Gets or sets the ask.
        /// </summary>
        /// <value>The ask.</value>
        public Ask Ask
        {
            get { return GetAttributeEnum<Ask>("ask"); }
            set
            {
                if (value == Ask.None)
                    RemoveAttribute("ask");
                else
                    SetAttribute("ask", value.ToString().ToLower());
            }
        }

        /// <summary>
        /// Approved is used to signal subscription pre-approval.
        /// </summary>
        public bool Approved
        {
            get { return GetAttributeBool("approved"); }
            set
            {
                if (value)
                    SetAttribute("approved", true);
                else
                    RemoveAttribute("approved");
            }
        }
    }
}
