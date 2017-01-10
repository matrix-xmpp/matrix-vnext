using Matrix.Core;
using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Compression
{
    [XmppTag(Name = "failure", Namespace = Namespaces.Compress)]
    public class Failure : XmppXElement
    {
        public Failure()
            : base(Namespaces.Compress, "failure")
        {
        }

        /*
         * xs:element name='setup-failed' type='empty'/>
        <xs:element name='processing-failed' type='empty'/>
        <xs:element name='unsupported-method' type='empty'/>
         */

        public FailureCondition Condition
        {
            get
            {
                var values = Core.Enum.GetValues<FailureCondition>().ToEnum<FailureCondition>();
                foreach (var failureCondition in values)
                {
                     if (HasTag(failureCondition.GetName()))
                        return failureCondition;
                }

                return FailureCondition.UnknownCondition;
            }
            set
            {
                if (value != FailureCondition.UnknownCondition)
                    SetTag(Namespaces.Streams, value.GetName(), null);
            }
        }
    }
}