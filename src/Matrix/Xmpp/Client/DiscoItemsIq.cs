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

using Matrix.Xmpp.Disco;

namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Creates a Disco Items iq request
    /// </summary>
    public class DiscoItemsIq : Iq
    {
         #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        public DiscoItemsIq()
        {            
            GenerateId();
            Items = new Items();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="node">The node.</param>
        public DiscoItemsIq(string node) : this()
        {
           Items.Node = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DiscoItemsIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="node">The node.</param>
        public DiscoItemsIq(IqType type, string node)
            : this(type)
        {
            Items.Node = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public DiscoItemsIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jis.</param>
        /// <param name="node">The node.</param>
        public DiscoItemsIq(Jid to, string node)
            : this(to)
        {
            Items.Node = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public DiscoItemsIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="node">The node.</param>
        public DiscoItemsIq(Jid to, Jid from, string node)
            : this(to, from)
        {
            Items.Node = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public DiscoItemsIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoItemsIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        /// <param name="node">The node.</param>
        public DiscoItemsIq(Jid to, Jid from, IqType type, string node)
            : this(to, from, type)
        {
            Items.Node = node;
        }
        #endregion


        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public Items Items
        {
            get { return Element<Items>(); }
            set { Replace(value); }
        }
    }
}
