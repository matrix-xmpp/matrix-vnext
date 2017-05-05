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
    /// <summary>
    /// Telephone number object in <see cref="Vcard"/>
    /// </summary>
    [XmppTag(Name = "TEL", Namespace = Namespaces.Vcard)]
    public class Telephone : XmppXElement
    {
        #region Schema
        /*
            <TEL><VOICE/><WORK/><NUMBER>303-308-3282</NUMBER></TEL>
		    <TEL><FAX/><WORK/><NUMBER/></TEL>
		    <TEL><MSG/><WORK/><NUMBER/></TEL>
      
            HOME?,
            WORK?,
            VOICE?, 
            FAX?, 
            PAGER?, 
            MSG?, 
            CELL?, 
            VIDEO?, 
            BBS?, 
            MODEM?, 
            ISDN?, 
            PCS?, 
            PREF?,         
        */
        #endregion

        #region << Constructors >>
        public Telephone()
            : base(Namespaces.Vcard, "TEL")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Telephone"/> class.
        /// </summary>
        /// <param name="number">The number.</param>
        public Telephone(string number)
            : this()
        {
            Number = number;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Telephone"/> class.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="preferred">if set to <c>true</c> this is a preferred number.</param>
        public Telephone(string number, bool preferred)
            : this(number)
        {
            this.IsPreferred    = preferred;            
        }
        #endregion

        /// <summary>
        /// Gets or sets the telephone number.
        /// </summary>
        /// <value>The number.</value>
        public string Number
        {
            get { return GetTag("NUMBER"); }
            set { SetTag("NUMBER", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is a home numer.
        /// </summary>
        /// <value><c>true</c> if this instance is a home number; otherwise, <c>false</c>.</value>
        public bool IsHome
        {
            get { return HasTag("HOME"); }
            set { AddFirstOrRemoveTag("HOME", value);}
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is a work numer.
        /// </summary>
        /// <value><c>true</c> if this instance is a work number; otherwise, <c>false</c>.</value>
        public bool IsWork
        {
            get { return HasTag("WORK"); }
            set { AddFirstOrRemoveTag("WORK", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a voice numer.
        /// </summary>
        /// <value><c>true</c> if this instance is a voice number; otherwise, <c>false</c>.</value>
        public bool IsVoice
        {
            get { return HasTag("VOICE"); }
            set { AddFirstOrRemoveTag("VOICE", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a fax numer.
        /// </summary>
        /// <value><c>true</c> if this instance is a fax number; otherwise, <c>false</c>.</value>
        public bool IsFax
        {
            get { return HasTag("FAX"); }
            set { AddFirstOrRemoveTag("FAX", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a pager number.
        /// </summary>
        /// <value><c>true</c> if this instance is a pager number; otherwise, <c>false</c>.</value>
        public bool IsPager
        {
            get { return HasTag("PAGER"); }
            set { AddFirstOrRemoveTag("PAGER", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a messaging service number.
        /// </summary>
        /// <value><c>true</c> if this instance is a messaging service number; otherwise, <c>false</c>.</value>
        public bool IsMessagingService
        {
            get { return HasTag("MSG"); }
            set { AddFirstOrRemoveTag("MSG", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a cellular number.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is a cellular number; otherwise, <c>false</c>.
        /// </value>
        public bool IsCellular
        {
            get { return HasTag("CELL"); }
            set { AddFirstOrRemoveTag("CELL", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a video number.
        /// </summary>
        /// <value><c>true</c> if this instance is video number; otherwise, <c>false</c>.</value>
        public bool IsVideo
        {
            get { return HasTag("VIDEO"); }
            set { AddFirstOrRemoveTag("VIDEO", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a BBS number.
        /// </summary>
        /// <value><c>true</c> if this instance is a BBSnumber ; otherwise, <c>false</c>.</value>
        public bool IsBBS
        {
            get { return HasTag("BBS"); }
            set { AddFirstOrRemoveTag("BBS", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a modem number.
        /// </summary>
        /// <value><c>true</c> if this instance is a modem number; otherwise, <c>false</c>.</value>
        public bool IsModem
        {
            get { return HasTag("MODEM"); }
            set { AddFirstOrRemoveTag("MODEM", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a ISDN number.
        /// </summary>
        /// <value><c>true</c> if this instance is a ISDN number; otherwise, <c>false</c>.</value>
        public bool IsISDN
        {
            get { return HasTag("ISDN"); }
            set { AddFirstOrRemoveTag("ISDN", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a personal communication services number.
        /// </summary>
        /// <value><c>true</c> if this instance is a personal communication services number; otherwise, <c>false</c>.</value>
        public bool IsPCS
        {
            get { return HasTag("PCS"); }
            set { AddFirstOrRemoveTag("PCS", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is preferred umber.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is preferreda preferred number; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreferred
        {
            get { return HasTag("PREF"); }
            set { AddFirstOrRemoveTag("PREF", value); }
        }       
    }
}
