using Matrix.Core.Attributes;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "retract", Namespace = Namespaces.Pubsub)]
    public class Retract : Publish
    {
        #region Schema
        /*
        <xs:element name='retract'>
            <xs:complexType>
              <xs:sequence>
                <xs:element ref='item' minOccurs='1' maxOccurs='unbounded'/>
              </xs:sequence>
              <xs:attribute name='node' type='xs:string' use='required'/>
              <xs:attribute name='notify' type='xs:boolean' use='optional'/>
            </xs:complexType>
         </xs:element>
        */
        #endregion

        #region << Constructors >>
        public Retract()
            : base("retract")
        {
        }
        #endregion

        public bool Notify
        {
            get { return GetAttributeBool("notify"); }
            set { SetAttribute("notify", value); }
        }
    }
}