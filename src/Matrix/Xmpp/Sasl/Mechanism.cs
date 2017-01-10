using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Sasl
{
    /// <summary>
    /// Represents a sasl mechanism
    /// </summary>
    [XmppTag(Name = "mechanism", Namespace = Namespaces.Sasl)]
    public class Mechanism : XmppXElement
    {
        // <mechanism>DIGEST-MD5</mechanism>
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Mechanism"/> class.
        /// </summary>
        public Mechanism()
            : base(Namespaces.Sasl, "mechanism")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mechanism"/> class.
        /// </summary>
        /// <param name="mechanism">The mechanism.</param>
        public Mechanism(SaslMechanism mechanism)
            : this()
        {
            SaslMechanism = mechanism;
        }
        #endregion

        /// <summary>
        /// get or set the SASL mechanis as enum
        /// </summary>
        /// <value>The sasl mechanism.</value>
        public SaslMechanism SaslMechanism
        {
            get { return GetSaslMechanism(Value); }
            set { Value = GetSaslMechanismName(value); }
        }

        internal static SaslMechanism GetSaslMechanism(string mechanism)
        {
            foreach (var saslMechanism in Enum.GetValues<SaslMechanism>().ToEnum<SaslMechanism>())
            {
                if (saslMechanism.GetName() == mechanism)
                    return saslMechanism;
            }
            return SaslMechanism.None;
        }        
        
        internal static string GetSaslMechanismName(SaslMechanism mechanism)
        {
            return mechanism.GetName();
        }

        internal string KerberosPrincipal
        {
            /*
             * Cisco XCP send the principal here
             * 
             * <mechanism 
             *  kerb:principal='xmpp/ssops.sso.avaya.com@SSO.AVAYA.COM'
             *  xmlns:kerb='http://jabber.com/protocol/kerberosinfo'>GSSAPI</mechanism>
             */
            get { return GetAttribute("http://jabber.com/protocol/kerberosinfo", "principal"); }
        }
    }
}