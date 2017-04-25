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

namespace Matrix.Xmpp.Muc.User
{
    /// <summary>
    /// Decline
    /// </summary>
    [XmppTag(Name = "decline", Namespace = Namespaces.MucUser)]
    public class Decline : XmppXElementWithAddress
    {
        #region Xml sample
        /*
          <message
                from='hecate@shakespeare.lit/broom'
                to='darkcave@macbeth.shakespeare.lit'>
              <x xmlns='http://jabber.org/protocol/muc#user'>
                <decline to='crone1@shakespeare.lit'>
                  <reason>
                    Sorry, I'm too busy right now.
                  </reason>
                </decline>
              </x>
          </message>
          */
        #endregion

        #region Schema
        /*
          <xs:element name='decline'>
            <xs:complexType>
              <xs:sequence>
                <xs:element ref='reason' minOccurs='0'/>
              </xs:sequence>
              <xs:attribute name='from' type='xs:string' use='optional'/>
              <xs:attribute name='to' type='xs:string' use='optional'/>
            </xs:complexType>
          </xs:element>
        */
        #endregion

        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Decline"/> class.
        /// </summary>
        /// <param name="tagname">The tagname.</param>
        internal Decline(string tagname)
            : base(Namespaces.MucUser, tagname)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Decline"/> class.
        /// </summary>
        public Decline()
            : this("decline")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Decline"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        public Decline(Jid to)
            : this()
        {
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Decline"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        public Decline(Jid to, Jid from)
            : this(to)
        {
            From = from;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Decline"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="reason">The reason.</param>
        public Decline(Jid to, string reason)
            : this(to)
        {
            Reason = reason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Decline"/> class.
        /// </summary>
        /// <param name="to">To.</param>
        /// <param name="from">From.</param>
        /// <param name="reason">The reason.</param>
        public Decline(Jid to, Jid from, string reason)
            : this(to, from)
        {
            Reason = reason;
        }
        #endregion

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public string Reason
        {
            set { SetTag("reason", value); }
            get { return GetTag("reason"); }
        }
    }
}
