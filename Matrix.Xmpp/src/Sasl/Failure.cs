using Matrix.Core;
using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Sasl
{
    /// <summary>
    /// Sasl failure object.
    /// </summary>
    [XmppTag(Name = "failure", Namespace = Namespaces.Sasl)]
    public class Failure : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Failure"/> class.
        /// </summary>
        public Failure() : base(Namespaces.Sasl, "failure")
		{			
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Failure"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        public Failure(FailureCondition condition) : this()
        {
            Condition = condition;
        }
        #endregion
        
        /// <summary>
        /// The failure condition
        /// </summary>
        public FailureCondition Condition
        {
            get
            {
                foreach (var failureCondition in Util.Enum.GetValues<FailureCondition>().ToEnum<FailureCondition>())
                {
                     if (HasTag(failureCondition.GetName()))
                        return failureCondition;
                }
                return FailureCondition.UnknownCondition;
            }
            set
            {
                if (value != FailureCondition.UnknownCondition)
                    SetTag(value.GetName());
            }
        }

        /// <summary>
        /// An optional text description for the authentication failure.
        /// </summary>
        public string Text
        {
            get { return GetTag("text"); }
            set { SetTag("text", value); }
        }
	} 
}