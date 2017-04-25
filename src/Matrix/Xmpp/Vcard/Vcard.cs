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

using System;
using System.Collections.Generic;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Vcard
{
    [XmppTag(Name = "vCard", Namespace = Namespaces.Vcard)]
    public class Vcard : XmppXElement
    {
        #region Xml samples
        /*
            Example 2. Receiving One's Own vCard
    
	        <iq 
		        to='stpeter@jabber.org/Gabber'
		        type='result'
		        id='v1'>
	        <vCard xmlns='vcard-temp'>
		        <FN>Peter Saint-Andre</FN>
		        <N>
			        <FAMILY>Saint-Andre<FAMILY>
			        <GIVEN>Peter</GIVEN>
			        <MIDDLE/>
		        </N>
		        <NICKNAME>stpeter</NICKNAME>
		        <URL>http://www.jabber.org/people/stpeter.php</URL>
		        <BDAY>1966-08-06</BDAY>
		        <ORG>
		        <ORGNAME>Jabber Software Foundation</ORGNAME>
		        <ORGUNIT/>
		        </ORG>
		        <TITLE>Executive Director</TITLE>
		        <ROLE>Patron Saint</ROLE>
		        <TEL><VOICE/><WORK/><NUMBER>303-308-3282</NUMBER></TEL>
		        <TEL><FAX/><WORK/><NUMBER/></TEL>
		        <TEL><MSG/><WORK/><NUMBER/></TEL>
		        <ADR>
			        <WORK/>
			        <EXTADD>Suite 600</EXTADD>
			        <STREET>1899 Wynkoop Street</STREET>
			        <LOCALITY>Denver</LOCALITY>
			        <REGION>CO</REGION>
			        <PCODE>80202</PCODE>
			        <CTRY>USA</CTRY>
		        </ADR>
		        <TEL><VOICE/><HOME/><NUMBER>303-555-1212</NUMBER></TEL>
		        <TEL><FAX/><HOME/><NUMBER/></TEL>
		        <TEL><MSG/><HOME/><NUMBER/></TEL>
		        <ADR>
			        <HOME/>
			        <EXTADD/>
			        <STREET/>
			        <LOCALITY>Denver</LOCALITY>
			        <REGION>CO</REGION>
			        <PCODE>80209</PCODE>
			        <CTRY>USA</CTRY>
		        </ADR>
		        <EMAIL><INTERNET/><PREF/><USERID>stpeter@jabber.org</USERID></EMAIL>
		        <JABBERID>stpeter@jabber.org</JABBERID>
		        <DESC>
			        More information about me is located on my 
			        personal website: http://www.saint-andre.com/
		        </DESC>		
		        </vCard>
            </iq>
         
            <FN>Peter Saint-Andre</FN>

		    <NICKNAME>stpeter</NICKNAME>
		    <URL>http://www.jabber.org/people/stpeter.php</URL>
		    <BDAY>1966-08-06</BDAY>

		    <TITLE>Executive Director</TITLE>
		    <ROLE>Patron Saint</ROLE>
		    <TEL><VOICE/><WORK/><NUMBER>303-308-3282</NUMBER></TEL>
		    <TEL><FAX/><WORK/><NUMBER/></TEL>
		    <TEL><MSG/><WORK/><NUMBER/></TEL>

		    <TEL><VOICE/><HOME/><NUMBER>303-555-1212</NUMBER></TEL>
		    <TEL><FAX/><HOME/><NUMBER/></TEL>
		    <TEL><MSG/><HOME/><NUMBER/></TEL>

		    <EMAIL><INTERNET/><PREF/><USERID>stpeter@jabber.org</USERID></EMAIL>
		    <JABBERID>stpeter@jabber.org</JABBERID>
		    <DESC>
			    More information about me is located on my 
			    personal website: http://www.saint-andre.com/
		    </DESC>		
		    </vCard>
        */
        #endregion

        public Vcard()
            : base(Namespaces.Vcard, "vCard")
        {
            // TODO, add more stuff as needed to vcard
        }

        /// <summary>
        /// Gets or sets the fullname.
        /// </summary>
        /// <value>The fullname.</value>
        public string Fullname
        {
            get { return GetTag("FN"); }
            set { SetTag("FN", value); }
        }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>The nickname.</value>
        public string Nickname
        {
            get { return GetTag("NICKNAME"); }
            set { SetTag("NICKNAME", value); }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public string Url
        {
            get { return GetTag("URL"); }
            set { SetTag("URL", value); }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get { return GetTag("TITLE"); }
            set { SetTag("TITLE", value); }
        }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public string Role
        {
            get { return GetTag("ROLE"); }
            set { SetTag("ROLE", value); }
        }

        /// <summary>
        /// Gets or sets the birthday.
        /// </summary>
        /// <value>The birthday.</value>
        public DateTime Birthday
        {
            get
            {
                try
                {
                    string sDate = GetTag("BDAY");
                    if (sDate != null)
                        return DateTime.Parse(sDate);
                    
                    return DateTime.MinValue;
                }
                catch
                {
                    return DateTime.MinValue;
                }
            }
            set { SetTag("BDAY", value.ToString("yyyy-MM-dd")); }
        }

        /// <summary>
        /// Gets or sets the jid.
        /// </summary>
        /// <value>The jid.</value>
        public Jid Jid
        {
            get { return GetTagJid("JABBERID"); }
            set { SetTag("JABBERID", value.Bare); }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description
        {
            get { return GetTag("DESC"); }
            set { SetTag("DESC", value); }
        }
        
        /// <summary>
        /// Gets  a collection of <see cref="Telephone"/> numbers.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Telephone> Telephones()
        {
            return Elements<Telephone>();
        }

        /// <summary>
        /// Adds a <see cref="Telephone"/> number.
        /// </summary>
        /// <param name="telephone">The telephone.</param>
        public void AddTelephone(Telephone telephone)
        {
            Add(telephone);
        }

        /// <summary>
        /// Gets a collection of <see cref="Address"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Address> GetAddresses()
        {
            return Elements<Address>();
        }

        /// <summary>
        /// Adds a <see cref="Address"/>.
        /// </summary>
        /// <param name="address">The address.</param>
        public void AddAddress(Address address)
        {
            Add(address);
        }

        /// <summary>
        /// Gets a colection of <see cref="Email"/> addresses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Email> GetEmails()
        {
            return Elements<Email>();            
        }

        /// <summary>
        /// Adds a <see cref="Email"/> address.
        /// </summary>
        /// <param name="email">The email.</param>
        public void AddEmail(Email email)
        {
            Add(email);
        }

        /// <summary>
        /// The <see cref="Photo"/>.
        /// </summary>
        /// <value>The photo.</value>
        public Photo Photo
        {
            get { return Element<Photo>(); }
            set { Replace(value); }
        }
    }
}
