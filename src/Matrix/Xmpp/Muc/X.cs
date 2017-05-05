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

namespace Matrix.Xmpp.Muc
{
    /*     
       <x xmlns='http://jabber.org/protocol/muc'>
           <password>secret</password>
       </x>     
     
       <x xmlns='http://jabber.org/protocol/muc'>
            <history since='1970-01-01T00:00Z'/>
       </x>
    */

    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.Muc)]
    public class X : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Muc"/> class.
        /// </summary>
        public X()
            : base(Namespaces.Muc, "x")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Muc"/> class.
        /// </summary>
        /// <param name="pass">The room password.</param>
        public X(string pass)
            : this()
        {
            Password = pass;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X"/> class.
        /// </summary>
        /// <param name="history">The <see cref="History"/>.</param>
        public X(History history)
            : this()
        {
            History = history;
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
        /// Gets or sets the <see cref="History"/>
        /// </summary>
        /// <value>The history.</value>
        public History History
        {
            get { return Element<History>(); }
            set { Replace(value); }
        }
    }
}
