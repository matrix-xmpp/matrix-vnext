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

namespace Matrix.Xmpp.Client
{
    public class PrivacyIq : Iq
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="PrivacyIq"/> class.
        /// </summary>
        public PrivacyIq()
        {
            GenerateId();
            Privacy = new Privacy.Privacy();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivacyIq"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public PrivacyIq(IqType type)
            : this()
        {
            Type = type;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivacyIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        public PrivacyIq(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivacyIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        public PrivacyIq(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrivacyIq"/> class.
        /// </summary>
        /// <param name="to">To Jid.</param>
        /// <param name="from">From Jid.</param>
        /// <param name="type">The type.</param>
        public PrivacyIq(Jid to, Jid from, IqType type)
            : this(to, from)
        {
            Type = type;
        }
        #endregion

        /// <summary>
        /// Gets or sets the privacy object.
        /// </summary>
        /// <value>The vcard.</value>
        public Privacy.Privacy Privacy
        {
            get { return Element<Privacy.Privacy>(); }
            set { Replace(value); }
        }
    }
}
