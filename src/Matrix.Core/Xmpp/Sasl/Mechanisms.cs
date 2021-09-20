using System.Collections.Generic;
using System.Linq;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Sasl
{
    /// <summary>
    /// SASL Mechanisms
    /// </summary>
    [XmppTag(Name = "mechanisms", Namespace = Namespaces.Sasl)]
    public class Mechanisms : XmppXElement
    {
        public Mechanisms()
            : base(Namespaces.Sasl, "mechanisms")
        {            
        }

        public IEnumerable<Mechanism> GetMechanisms()
        {
            return Elements<Mechanism>();
        }

        public bool SupportsMechanism(SaslMechanism mechanism)
        {
            return GetMechanisms().Any(mech => mech.SaslMechanism == mechanism);
        }

        public Mechanism GetMechanism(SaslMechanism mechanism)
        {
            return GetMechanisms().FirstOrDefault(mech => mech.SaslMechanism == mechanism);
        }

        public void AddMechanism(SaslMechanism mechanism)
        {
            Add(new Mechanism(mechanism));
        }

        /// <summary>
        /// XEP-0233 Principal Hostname for Sasl authentication.
        /// </summary>
        public string PrincipalHostname
        {
            get
            {
                var host = Element<Hostname>();
                return host != null ? host.Value : null;
            }
        }
    }
}
