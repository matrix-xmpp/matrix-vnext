using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "subscribe-options", Namespace = Namespaces.Pubsub)]
    public class SubscribeOptions : XmppXElement
    {
        #region Schema
        /*
            <xs:element name='subscribe-options'>
                <xs:complexType>
                    <xs:sequence>
                        <xs:element name='required' type='empty' minOccurs='0'/>
                    </xs:sequence>
                </xs:complexType>
            </xs:element>
        */
        #endregion

        #region << Constructors >>
        public SubscribeOptions()
            : base(Namespaces.Pubsub, "subscribe-options")
        {
        }
        #endregion

        public bool Required
        {
            get { return HasTag("required"); }
            set
            {
                if (value)
                    SetTag("required");
                else
                    RemoveTag("required");
            }
        }
    }
}