using Matrix.Attributes;
using Matrix.Xml;
using Matrix.Xmpp.Capabilities;
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
        public bool SupportsStartTls
        {
            get { return StartTls != null; }
        }

        /// <summary>
        /// Gets a value indicating whether TLS is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if TLS is required] otherwise, <c>false</c>.
        /// </value>
        public bool TlsIsRequired
        {
            get { return StartTls != null && StartTls.Required; }
        }

        /// <summary>
        /// Are sessions supported? This is old Jabebr stuff which should not be used anymore in XMPP.
        /// </summary>
        public bool SupportsSession
        {
            get { return Session != null; }
        }

        /// <summary>
        /// Is Stream Compression supported?
        /// </summary>
        public bool SupportsCompression
        {
            get { return Compression != null; }
        }

        /// <summary>
        /// Is old jabber style authentication (XEP-0078: Non-SASL Authentication) supported?
        /// </summary>
        public bool SupportsAuth
        {
            get { return Auth != null; }
        }

        /// <summary>
        /// Is Registration supported?
        /// </summary>
        public bool SupportsRegistration
        {
            get { return Register != null; }
        }

        /// <summary>
        /// Is roster versioning supported?
        /// </summary>
        public bool SupportsRosterVersioning
        {
            get { return RosterVersioning != null; }
        }

        /// <summary>
        /// Is stream management supported?
        /// </summary>
        public bool SupportsStreamManagement
        {
            get { return StreamManagement != null; }
        }
        
        /// <summary>
        /// Are bidirectional Server-to-Server Connections supported?
        /// </summary>
        public bool SupportsBidi
        {
            get { return Bidi != null; }
        }

        /// <summary>
        /// Is message archiving supported? (XEP-0136)
        /// </summary>
        public bool SupportsMessageArchiving
        {
            get { return MessageArchiving != null; }
        }
    }
}