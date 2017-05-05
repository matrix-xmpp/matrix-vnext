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

namespace Matrix.Xmpp.MessageArchiving
{
    public abstract class MessageItem : ArchiveItem
    {
        #region Schema
        /*
          <xs:complexType name='messageType'>
            <xs:sequence>
              <xs:element name='body' type='xs:string' minOccurs='0' maxOccurs='unbounded'/>
              <xs:any processContents='lax' namespace='##other' minOccurs='0' maxOccurs='unbounded'/>
            </xs:sequence>
            <xs:attribute name='jid' type='xs:string' use='optional'/>
            <xs:attribute name='name' type='xs:string' use='optional'/>
            <xs:attribute name='secs' type='xs:nonNegativeInteger' use='optional'/>
            <xs:attribute name='utc' type='xs:dateTime' use='optional'/>
          </xs:complexType>
        */
        #endregion

        protected MessageItem(string tagName) : base(tagName)
        {}

        /// <summary>
        /// Gets or sets the content of a message.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body
        {
            get { return GetTag("body"); }
            set { SetTag("body", value); }
        }
        
        /// <summary>
        /// Gets or sets the seconds of the message relative to the previous message in the collection 
        /// (or, for the first message, relative to the start of the collection)
        /// </summary>
        /// <value>
        /// The seconds.
        /// </value>
        public int Seconds
        {
            get { return GetAttributeInt("secs"); }
            set { SetAttribute("secs", value); }
        }

        public Jid Jid
        {
            get { return GetAttributeJid("jid"); }
            set { SetAttribute("jid", value); }
        }

        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }
    }
}
