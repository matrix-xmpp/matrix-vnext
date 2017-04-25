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

namespace Matrix.Xmpp.Vcard
{
    [XmppTag(Name = "EMAIL", Namespace = Namespaces.Vcard)]
    public class Email : XmppXElement
    {
        #region Schema
        /*
          <EMAIL><INTERNET/><PREF/><USERID>stpeter@jabber.org</USERID></EMAIL>
     
          <!-- Email address property. Default type is INTERNET. -->
          <!ELEMENT EMAIL (
            HOME?, 
            WORK?, 
            INTERNET?, 
            PREF?, 
            X400?, 
            USERID
          )>

        <!ELEMENT USERID (#PCDATA)>
        */
        #endregion

        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class.
        /// </summary>
        public Email()
            : base(Namespaces.Vcard, "EMAIL")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        public Email(string address)
            : this()
        {
            Address = address;
            IsInternet = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Email"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="preferred">if set to <c>true</c> this is a preferred address.</param>
        public Email(string address, bool preferred)
            : this(address)
        {            
            if (preferred)
                this.IsPreferred = true;
        }
        #endregion
  
        /// <summary>
        /// The email Adress
        /// </summary>
        public string Address
        {
            get { return GetTag("USERID"); }
            set { SetTag("USERID", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a home email address.
        /// </summary>
        /// <value><c>true</c> if this instance is a home email address; otherwise, <c>false</c>.</value>
        public bool IsHome
        {
            get { return HasTag("HOME"); }
            set { AddFirstOrRemoveTag("HOME", value);}
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a work email address.
        /// </summary>
        /// <value><c>true</c> if this instance is a work email address; otherwise, <c>false</c>.</value>
        public bool IsWork
        {
            get { return HasTag("WORK"); }
            set { AddFirstOrRemoveTag("WORK", value);}
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is an internet (SMTP) email address.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is an internet (SMTP) email address; otherwise, <c>false</c>.
        /// </value>
        public bool IsInternet
        {
            get { return HasTag("INTERNET"); }
            set { AddFirstOrRemoveTag("INTERNET", value);}
        }        

        /// <summary>
        /// Gets or sets a value indicating whether this instance an X.400 service email address.
        /// </summary>
        /// <value><c>true</c> if this instance is an X.400 service email address; otherwise, <c>false</c>.</value>
        public bool IsX400
        {
            get { return HasTag("X400"); }
            set { AddFirstOrRemoveTag("X400", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a preferred email address.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is a preferred email address; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreferred
        {
            get { return HasTag("PREF"); }
            set { AddFirstOrRemoveTag("PREF", value); }
        }
    }
}
