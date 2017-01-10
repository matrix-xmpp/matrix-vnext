using System;
using Matrix.Attributes;

namespace Matrix.Xmpp.PubSub.Event
{
    [XmppTag(Name = "subscription", Namespace = Namespaces.PubsubEvent)]
    public class Subscription : Base.Subscription
    {
        #region Schema
        /*
          <xs:element name='subscription'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='expiry' type='xs:dateTime' use='optional'/>
                  <xs:attribute name='jid' type='xs:string' use='required'/>
                  <xs:attribute name='node' type='xs:string' use='optional'/>
                  <xs:attribute name='subid' type='xs:string' use='optional'/>
                  <xs:attribute name='subscription' use='optional'>
                    <xs:simpleType>
                      <xs:restriction base='xs:NCName'>
                        <xs:enumeration value='none'/>
                        <xs:enumeration value='pending'/>
                        <xs:enumeration value='subscribed'/>
                        <xs:enumeration value='unconfigured'/>
                      </xs:restriction>
                    </xs:simpleType>
                  </xs:attribute>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
          </xs:element>
        */
        #endregion
        
        public Subscription() : base(Namespaces.PubsubEvent)
        {
        }

        public DateTime Expiry
        {
            get { return Matrix.Time.Iso8601Date(GetAttribute("expiry")); }    
            set { SetAttribute("expiry", Matrix.Time.Iso8601Date(value)); }
        }

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }
    }
}