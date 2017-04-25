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

namespace Matrix.Xmpp.Auth
{
    /// <summary>
    /// Non SASL authentication (XEP-0078)
    /// </summary>
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqAuth)]
    public class Auth : XmppXElement
    {
        public Auth():base(Namespaces.IqAuth, Tag.Query)
        {}

        #region << Properties >>
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
        public string Username
        {
            get { return GetTag("username"); }
            set { SetTag("username", value); }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password
        {
            get { return GetTag("password"); }
            set { SetTag("password", value); }
        }

        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        /// <value>The resource.</value>
        public string Resource
        {
            get { return GetTag("resource"); }
            set { SetTag("resource", value); }
        }

        public string Digest
        {
            get { return GetTag("digest"); }
            set { SetTag("digest", value); }
        }
        #endregion
    }
}
