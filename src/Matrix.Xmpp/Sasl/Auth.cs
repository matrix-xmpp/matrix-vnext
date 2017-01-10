using Matrix.Core.Attributes;

namespace Matrix.Xmpp.Sasl
{
    /// <summary>
    /// Summary description for Auth.
    /// </summary>
    [XmppTag(Name = "auth", Namespace = Namespaces.Sasl)]
    public class Auth : Base.Sasl
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Auth"/> class.
        /// </summary>
        public Auth() : base("auth")
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Auth"/> class.
        /// </summary>
        /// <param name="mechanism">The mechanism.</param>
        public Auth(SaslMechanism mechanism)
            : this()
        {
            SaslMechanism = mechanism;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Auth"/> class.
        /// </summary>
        /// <param name="mechanism">The sasl mechanism name as string.</param>
        public Auth(string mechanism)
            : this()
        {
            SetAttribute("mechanism", mechanism); 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Auth"/> class.
        /// </summary>
        /// <param name="mechanism">The mechanism.</param>
        /// <param name="text">The value of the auth tag.</param>
        public Auth(string mechanism, string text)
            : this(mechanism)
        {
            Value = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Auth"/> class.
        /// </summary>
        /// <param name="mechanism">The mechanism.</param>
        /// <param name="text">The value of the auth tag.</param>
        public Auth(SaslMechanism mechanism, string text)
            : this(mechanism)
        {
            Value = text;
        }
        #endregion

        /// <summary>
        /// Gets or sets the sasl mechanism.
        /// </summary>
        /// <value>
        /// The sasl mechanism.
        /// </value>
        public SaslMechanism SaslMechanism
        {
            get { return Mechanism.GetSaslMechanism(GetAttribute("mechanism")); }
            set { SetAttribute("mechanism", Mechanism.GetSaslMechanismName(value)); }
        }
    }
}