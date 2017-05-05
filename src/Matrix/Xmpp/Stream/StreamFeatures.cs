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
using Matrix.Xmpp.Capabilities;
using Matrix.Xmpp.Compression;
using Matrix.Xmpp.Stream.Features;
using Matrix.Xmpp.Tls;

namespace Matrix.Xmpp.Stream
{
    /// <summary>
    /// Summary description for Features.
    /// </summary>
    [XmppTag(Name = "features", Namespace = Namespaces.Stream)]
    public class StreamFeatures : XmppXElement
    {
        #region Xml samples
        /*
         <stream:features>
		        <mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
			        <mechanism>DIGEST-MD5</mechanism>
			        <mechanism>PLAIN</mechanism>
		        </mechanisms>
         </stream:features>

         <stream:features>
		        <starttls xmlns='urn:ietf:params:xml:ns:xmpp-tls'>
			        <required/>
		        </starttls>
		        <mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
			        <mechanism>DIGEST-MD5</mechanism>
			        <mechanism>PLAIN</mechanism>
		        </mechanisms>
         </stream:features>
         
         <stream:features>
             <c xmlns='http://jabber.org/protocol/caps'
                hash='sha-1'
                node='http://jabberd.org'
                ver='ItBTI0XLDFvVxZ72NQElAzKS9sU='/>
         </stream:features>
        */
        #endregion

        public StreamFeatures() : base(Namespaces.Stream, "stream", "features")
        {            
        }

        /// <summary>
        /// StartTls stream feature
        /// </summary>
        public StartTls StartTls
        {
            get { return Element<StartTls>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Sasl mechanisms stream feature
        /// </summary>
        public Sasl.Mechanisms Mechanisms
        {
            get { return Element<Sasl.Mechanisms>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Resource Bind stream feature
        /// </summary>
        public Bind.Bind Bind
        {
            get { return Element<Bind.Bind>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Session stream feature
        /// </summary>
        public Session.Session Session
        {
            get { return Element<Session.Session>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Cmmpression stream feature
        /// </summary>
        public Features.Compression Compression
        {
            get { return Element<Features.Compression>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// The old Jabber style auth stream feature
        /// </summary>
        public Features.Auth Auth
        {
            get { return Element<Features.Auth>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// The registration stream feature
        /// </summary>
        public Features.Register Register
        {
            get { return Element<Features.Register>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the roster versioning.
        /// </summary>
        /// <value>The roster versioning.</value>
        public RosterVersioning RosterVersioning
        {
            get { return Element<RosterVersioning>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the stream management.
        /// </summary>
        /// <value>The stream management.</value>
        public Features.StreamManagement StreamManagement
        {
            get { return Element<Features.StreamManagement>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// The server capabilities
        /// </summary>
        public Caps Caps
        {
            get { return Element<Caps>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Bidirectional Server-to-Server Connections
        /// </summary>
        public Bidi Bidi
        {
            get { return Element<Bidi>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the server supports message archiving (XEP-0136).
        /// </summary>
        /// <value>
        /// The message archiving.
        /// </value>
        public Features.MessageArchiving MessageArchiving
        {
            get { return Element<Features.MessageArchiving>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Are Caps supported?
        /// </summary>
        public bool SupportsCaps
        {
            get { return Caps != null; }
        }

        /// <summary>
        /// Is resource binding supported?
        /// </summary>
        public bool SupportsBind
        {
            get { return Bind != null; }
        }

        /// <summary>
        /// Is StartTls xupported?
        /// </summary>
        public bool SupportsStartTls => StartTls != null;

        /// <summary>
        /// Gets a value indicating whether TLS is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if TLS is required] otherwise, <c>false</c>.
        /// </value>
        public bool TlsIsRequired => StartTls != null && StartTls.Required;

        /// <summary>
        /// Are sessions supported? This is old Jabebr stuff which should not be used anymore in XMPP.
        /// </summary>
        public bool SupportsSession => Session != null;

        /// <summary>
        /// Is Stream Compression supported?
        /// </summary>
        public bool SupportsCompression => Compression != null;

        /// <summary>
        /// Is ZLib Compression supported?
        /// </summary>
        public bool SupportsZlibCompression => SupportsCompression && Compression.Supports(Methods.Zlib);

        /// <summary>
        /// Is old jabber style authentication (XEP-0078: Non-SASL Authentication) supported?
        /// </summary>
        public bool SupportsAuth => Auth != null;

        /// <summary>
        /// Is Registration supported?
        /// </summary>
        public bool SupportsRegistration => Register != null;

        /// <summary>
        /// Is roster versioning supported?
        /// </summary>
        public bool SupportsRosterVersioning => RosterVersioning != null;

        /// <summary>
        /// Is stream management supported?
        /// </summary>
        public bool SupportsStreamManagement => StreamManagement != null;

        /// <summary>
        /// Are bidirectional Server-to-Server Connections supported?
        /// </summary>
        public bool SupportsBidi => Bidi != null;

        /// <summary>
        /// Is message archiving supported? (XEP-0136)
        /// </summary>
        public bool SupportsMessageArchiving => MessageArchiving != null;
    }
}
