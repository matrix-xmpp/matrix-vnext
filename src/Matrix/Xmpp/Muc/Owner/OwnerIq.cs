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

using Matrix.Xmpp.Client;

namespace Matrix.Xmpp.Muc.Owner
{
    /// <summary>
    /// Represents a Muc Owner Iq.
    /// </summary>
    public class OwnerIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        public OwnerIq()
        {
            GenerateId();
            OwnerQuery = new OwnerQuery();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public OwnerIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        public OwnerIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        public OwnerIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        public OwnerIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerIq"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="type">The type.</param>
        /// <param name="id">The id.</param>
        public OwnerIq(Jid to, Jid from, IqType type, string id)
            : this(to, from, type)
        {
            Id = id;
        }
        #endregion

        /// <summary>
        /// Gets or sets the owner query.
        /// </summary>
        /// <value>The admin.</value>
        public OwnerQuery OwnerQuery
        {
            get { return Element<OwnerQuery>(); }
            set { Replace(value); }
        }
    }
}
