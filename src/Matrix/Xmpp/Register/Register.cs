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
using Matrix.Xmpp.XData;

namespace Matrix.Xmpp.Register
{
    /// <summary>
    /// XEP-0077: In-Band Registration
    /// </summary>
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqRegister)]
    public class Register : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Register"/> class.
        /// </summary>
        public Register()
            : base(Namespaces.IqRegister, Tag.Query)
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Register"/> class.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public Register(string username, string password)
            : this()
        {
            Username = username;
            Password = password;
        }
        #endregion

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
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>The instructions.</value>
        public string Instructions
        {
            get { return GetTag("instructions"); }
            set { SetTag("instructions", value); }
        }

        /// <summary>
        /// Gets the name of this element.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An <see cref="T:System.Xml.Linq.XName"/> that contains the name of this element.
        /// </returns>
        public new string Name
        {
            get { return GetTag("name"); }
            set { SetTag("name", value); }
        }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first.</value>
        public string First
        {
            get { return GetTag("first"); }
            set { SetTag("first", value); }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last.</value>
        public string Last
        {
            get { return GetTag("last"); }
            set { SetTag("last", value); }
        }
        
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email
        {
            get { return GetTag("email"); }
            set { SetTag("email", value); }
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>The key.</value>
        public string Key
        {
            get { return GetTag("key"); }
            set { SetTag("key", value); }
        }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>The date.</value>
        public string Date
        {
            get { return GetTag("date"); }
            set { SetTag("date", value); }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url
        {
            get { return GetTag("url"); }
            set { SetTag("url", value); }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public new string Text
        {
            get { return GetTag("text"); }
            set { SetTag("text", value); }
        }

        /// <summary>
        /// Gets or sets the misc.
        /// </summary>
        /// <value>The misc.</value>
        public string Misc
        {
            get { return GetTag("misc"); }
            set { SetTag("misc", value); }
        }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>The zip.</value>
        public string Zip
        {
            get { return GetTag("zip"); }
            set { SetTag("zip", value); }
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>The state.</value>
        public string State
        {
            get { return GetTag("state"); }
            set { SetTag("state", value); }
        }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>The city.</value>
        public string City
        {
            get { return GetTag("city"); }
            set { SetTag("city", value); }
        }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address
        {
            get { return GetTag("address"); }
            set { SetTag("address", value); }
        }

        /// <summary>
        /// Gets or sets the nick.
        /// </summary>
        /// <value>The nick.</value>
        public string Nick
        {
            get { return GetTag("nick"); }
            set { SetTag("nick", value); }
        }

        /// <summary>
        /// Remove registration from the server
        /// </summary>
        public new bool Remove
        {
            get { return HasTag("remove"); }
            set
            {
                if (value)
                    SetTag("remove");
                else
                    RemoveTag("remove");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the entity is already registered with a sevice.
        /// </summary>
        /// <value><c>true</c> if registered; otherwise, <c>false</c>.</value>
        public bool Registered
        {
            get { return HasTag("registered"); }
            set { AddOrRemoveTag("registered", value); }
        }

        /// <summary>
        /// Gets or sets the xdata form.
        /// </summary>
        /// <value>The data.</value>
        public Data XData
        {
            get { return Element<Data>(); }
            set { Replace(value); }
        }
        #endregion
    }
}
