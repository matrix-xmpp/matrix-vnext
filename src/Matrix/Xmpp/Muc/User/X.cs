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

using System.Collections.Generic;
using System.Linq;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Muc.User
{
    /// <summary>
    /// X
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.MucUser)]
    public class X : XmppXElement
    {
        /*
          <x xmlns='http://jabber.org/protocol/muc#user'>
            <item affiliation='member' role='participant'/>
            <status code='100'/>
            <status code='110'/>
            <status code='170'/>
            <status code='210'/>
          </x>
         
          <xs:element name='x'>
            <xs:complexType>
              <xs:choice minOccurs='0' maxOccurs='unbounded'>
                <xs:element ref='decline' minOccurs='0'/>
                <xs:element ref='destroy' minOccurs='0'/>
                <xs:element ref='invite' minOccurs='0' maxOccurs='unbounded'/>
                <xs:element ref='item' minOccurs='0'/>
                <xs:element name='password' type='xs:string' minOccurs='0'/>
                <xs:element ref='status' minOccurs='0' maxOccurs='unbounded'/>
              </xs:choice>
            </xs:complexType>
          </xs:element>         
        */
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="X"/> class.
        /// </summary>
        public X()
            : base(Namespaces.MucUser, "x")
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets the room password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            set { SetTag("password", value); }
            get { return GetTag("password"); }
        }
        
        /// <summary>
        /// Gets or sets the <see cref="Item"/>.
        /// </summary>
        /// <value>The item.</value>
        public Item Item
        {
            get { return Element<Item>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Decline"/>.
        /// </summary>
        /// <value>The declice.</value>
        public Decline Decline
        {
            get { return Element<Decline>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="Destroy"/>.
        /// </summary>
        /// <value>The destroy.</value>
        public Destroy Destroy
        {
            get { return Element<Destroy>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Adds a status.
        /// </summary>
        /// <param name="status">The status.</param>
        public void AddStatus(Status status)
        {
            if (status != null) Add(status);
        }

        /// <summary>
        /// Gets the statuses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Status> GetStatuses()
        {
            return Elements<Status>();
        }

        /// <summary>
        /// Determines whether the specified status is included.
        /// </summary>
        /// <param name="scode">The scode.</param>
        /// <returns>
        /// 	<c>true</c> if the specified scode has status; otherwise, <c>false</c>.
        /// </returns>
        public bool HasStatus(StatusCode scode)
        {
            return GetStatuses().Any(s => s.Code == scode);
        }

        /// <summary>
        /// Determines whether the specified code is included.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>
        /// 	<c>true</c> if the specified code has status; otherwise, <c>false</c>.
        /// </returns>
        public bool HasStatus(int code)
        {
            return GetStatuses().Any(s => s.CodeInt == code);
        }

        /// <summary>
        /// Adds the invite.
        /// </summary>
        /// <param name="invite">The invite.</param>
        public void AddInvite(Invite invite)
        {
            if (invite != null) Add(invite);
        }
        
        /// <summary>
        /// Gets all <see cref="Invite"/>s.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Invite> GetInvites()
        {
            return Elements<Invite>();
        }
    }
}
