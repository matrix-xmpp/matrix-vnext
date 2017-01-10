using Matrix.Attributes;

namespace Matrix.Xmpp.PubSub.Owner
{
    [XmppTag(Name = "delete", Namespace = Namespaces.PubsubOwner)]
    public class Delete : Xmpp.PubSub.Delete
    {
        #region Schema
        /*
          <xs:element name='pubsub'>
            <xs:complexType>
              <xs:choice>
                <xs:element ref='affiliations'/>
                <xs:element ref='configure'/>
                <xs:element ref='delete'/>
                <xs:element ref='purge'/>
                <xs:element ref='subscriptions'/>
              </xs:choice>
            </xs:complexType>
          </xs:element>
        */ 
        #endregion

        public Delete()
            : base(Namespaces.PubsubOwner)
        {
        }
    }
}