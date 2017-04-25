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

using System.Xml.Linq;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Bosh
{
    /*
    POST /webclient HTTP/1.1
    Host: httpcm.jabber.org
    Accept-Encoding: gzip, deflate
    Content-Type: text/xml; charset=utf-8
    Content-Length: 153

    <body rid='1249243564'
          sid='SomeSID'
          type='terminate'
          xmlns='http://jabber.org/protocol/httpbind'>
      <presence type='unavailable'
                xmlns='jabber:client'/>
    </body>
    
    HTTP/1.1 200 OK
    Content-Type: text/xml; charset=utf-8
    Content-Length: 128

    <body authid='ServerStreamID'
          wait='60'
          inactivity='30'
          polling='5'
          requests='2'
          accept='deflate,gzip'
          sid='SomeSID'
          secure='true'
          stream='firstStreamName'
          charsets='ISO_8859-1 ISO-2022-JP'
          xmlns='http://jabber.org/protocol/httpbind'/>
    */
    [XmppTag(Name = "body", Namespace = Namespaces.HttpBind)]
    public class Body : XmppXElement
    {
        private readonly XNamespace nsBosh = Namespaces.XmppXBosh;

        #region << Constructors >>
        public Body() : base(Namespaces.HttpBind, "body")
        {            
        }
        #endregion
        
        public string Sid
        {
            get { return GetAttribute("sid"); }
            set { SetAttribute("sid", value); }
        }
         
        public long Rid
        {
            get { return GetAttributeLong("rid"); }
            set { SetAttribute("rid", value); }
        }

        public long Ack
        {
            get { return GetAttributeLong("ack"); }
            set { SetAttribute("ack", value); }
        }

        public bool Secure
        {
            get { return GetAttributeBool("secure"); }
            set { SetAttribute("secure", value); }
        }

        /// <summary>
        /// Specifies the longest time (in seconds) that the connection manager is allowed to wait before responding to any request 
        /// during the session. This enables the client to limit the delay before it discovers any network failure, 
        /// and to prevent its HTTP/TCP connection from expiring due to inactivity.
        /// </summary>
        public int Wait
        {
            get { return GetAttributeInt("wait"); }
            set { SetAttribute("wait", value); }
        }

        /// <summary>
        /// If the connection manager supports session pausing (inactivity) then it SHOULD advertise that to the client by including a 'maxpause'
        /// attribute in the session creation response element. The value of the attribute indicates the maximum length of a temporary 
        /// session pause (in seconds) that a client MAY request.
        /// </summary>
        public int MaxPause
        {
            get { return GetAttributeInt("maxpause"); }
            set { SetAttribute("maxpause", value); }
        }

        public int Inactivity
        {
            get { return GetAttributeInt("inactivity"); }
            set { SetAttribute("inactivity", value); }
        }

        public int Polling
        {
            get { return GetAttributeInt("polling"); }
            set { SetAttribute("polling", value); }
        }

        public int Requests
        {
            get { return GetAttributeInt("requests"); }
            set { SetAttribute("requests", value); }
        }

        /// <summary>
        /// Specifies the target domain of the first stream.
        /// </summary>
        public Jid To
        {
            get { return GetAttributeJid("to"); }
            set { SetAttribute("to", value); }
        }

        public Jid From
        {
            get { return GetAttributeJid("from"); }
            set { SetAttribute("from", value); }
        }

        /// <summary>
        /// specifies the maximum number of requests the connection manager is allowed to keep waiting at any one time during the session. 
        /// If the client is not able to use HTTP Pipelining then this SHOULD be set to "1".
        /// </summary>
        public int Hold
        {
            get { return GetAttributeInt("hold"); }
            set { SetAttribute("hold", value); }
        }

        /// <summary>
        /// Specifies the highest version of the BOSH protocol that the client supports.
        /// The numbering scheme is major.minor (where the minor number MAY be incremented higher than a single digit,
        /// so it MUST be treated as a separate integer).
        /// </summary>
        /// <value>The version.</value>
        /// <remarks>
        /// The version should not be confused with the version of any protocol being transported.
        /// </remarks>
        public string Version
        {
            get { return GetAttribute("ver"); }
            set { SetAttribute("ver", value); }
        }

        public string NewKey
        {
            get { return GetAttribute("newkey"); }
            set { SetAttribute("newkey", value); }
        }

        public string Key
        {
            get { return GetAttribute("key"); }
            set { SetAttribute("key", value); }
        }

        public Type Type
        {
            get { return GetAttributeEnum<Type>("type"); }
            set
            {
                if (value == Type.None)
                    RemoveAttribute("type");
                else
                    SetAttribute("type", value.ToString().ToLower());
            }
        }

        public Condition Condition
        {
            get { return GetAttributeEnumUsingNameAttrib<Condition>("condition"); }
            set
            {
                if (value == Condition.None)
                    RemoveAttribute("condition");
                else
                    SetAttribute("condition", value.GetName());
            }
        }
        
        public string XmppVersion
        {
            get { return GetAttribute(nsBosh + "version"); }
            set { SetAttributeValue(nsBosh + "version", value); }            
            
        }

        public bool XmppRestart
        {
            get { return GetAttributeBool(nsBosh + "restart"); }
            set { SetAttributeValue(nsBosh + "restart", value); }
        }

        internal void AddBoshNameSpace()
        {
            // xmlns:xmpp='urn:xmpp:xbosh'
            Add(new XAttribute(XNamespace.Xmlns + "xmpp", Namespaces.XmppXBosh));            
        }

        internal void AddStreamNameSpace()
        {
            // xmlns:stream='http://etherx.jabber.org/streams'>
            Add(new XAttribute(XNamespace.Xmlns + "stream", Namespaces.Stream));            
        }
    }
}
