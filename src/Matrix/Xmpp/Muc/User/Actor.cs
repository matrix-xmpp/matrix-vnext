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
    #region Schema
    /*
      <xs:element name='actor'>
        <xs:complexType>
          <xs:simpleContent>
            <xs:extension base='empty'>
              <xs:attribute name='jid' type='xs:string' use='required'/>
            </xs:extension>
          </xs:simpleContent>
        </xs:complexType>
      </xs:element>
    */
    #endregion

    /// <summary>
    /// Actor
    /// </summary>
    [XmppTag(Name = "actor", Namespace = Namespaces.MucUser)]
    public class Actor : XmppXElementWithJidAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Actor"/> class.
        /// </summary>
        public Actor()
            : base(Namespaces.MucUser, "actor")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Actor"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        public Actor(Jid jid)
            : this()
        {
            Jid = jid;
        }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>The nickname.</value>
        public string Nickname
        {
            get { return GetAttribute("nick"); }
            set { SetAttribute("nick", value); }
        }
    }
}
