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
using Matrix.Xmpp.Nickname;

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Presence object
    /// </summary>
    [XmppTag(Name = Tag.Presence, Namespace = Namespaces.Client)]
    public class Presence : Base.Presence
    {
        #region <<Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class.
        /// </summary>
        public Presence() : base(Namespaces.Client)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class.
        /// </summary>
        /// <param name="show">The <see cref="Show"/>.</param>
        public Presence(Show show) : this()
        {
            Show = show;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class.
        /// </summary>
        /// <param name="type">The <see cref="PresenceType"/>.</param>
        public Presence(PresenceType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class.
        /// </summary>
        /// <param name="show">The show.</param>
        /// <param name="status">The status.</param>
        public Presence(Show show, string status)
            : this(show)
        {
            Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Presence"/> class.
        /// </summary>
        /// <param name="show">The <see cref="Show"/>.</param>
        /// <param name="status">The status.</param>
        /// <param name="priority">The priority.</param>
        public Presence(Show show, string status, int priority)
            : this(show, status)
        {
            Priority = priority;
        }
        #endregion

        /// <summary>
        /// Error object
        /// </summary>
        public Error Error
        {
            get { return Element<Error>(); }
            set { Replace(value); }
        }
        
        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>The nick.</value>
        public Nick Nick
        {
            get { return Element<Nick>(); }
            set { Replace(value); }
        }
    }
}
