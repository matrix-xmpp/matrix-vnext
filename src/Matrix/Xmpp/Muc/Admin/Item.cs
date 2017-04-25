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

namespace Matrix.Xmpp.Muc.Admin
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.MucAdmin)]
    public class Item : Muc.Item
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
            : base(Namespaces.MucAdmin)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="affiliation">The affiliation.</param>
        public Item(Affiliation affiliation)
            : this()
        {
            Affiliation = affiliation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="role">The role.</param>
        public Item(Role role)
            : this()
        {
            Role = role;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="nick">The nick.</param>
        public Item(Role role, string nick)
            : this(role)
        {
            Nickname = nick;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="nick">The nick.</param>
        /// <param name="reason">The reason.</param>
        public Item(Role role, string nick, string reason)
            : this(role, nick)
        {
            Reason = reason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="affiliation">The affiliation.</param>
        /// <param name="role">The role.</param>
        public Item(Affiliation affiliation, Role role)
            : this(affiliation)
        {
            Role = role;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="affiliation">The affiliation.</param>
        /// <param name="role">The role.</param>
        /// <param name="jid">The jjid.</param>
        public Item(Affiliation affiliation, Role role, Jid jid)
            : this(affiliation, role)
        {
            Jid = jid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="affiliation">The affiliation.</param>
        /// <param name="role">The role.</param>
        /// <param name="jid">The jid.</param>
        /// <param name="nick">The nick.</param>
        public Item(Affiliation affiliation, Role role, Jid jid, string nick)
            : this(affiliation, role, jid)
        {
            Nickname = nick;
        }
        #endregion
    }
}
