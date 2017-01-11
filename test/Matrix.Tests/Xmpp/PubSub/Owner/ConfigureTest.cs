using Matrix.Xml;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    
    
    public class ConfigureTest
    {
        private const string XML1 = @"<configure xmlns='http://jabber.org/protocol/pubsub#owner'/>";
        
        private const string XML2 = @"<configure xmlns='http://jabber.org/protocol/pubsub#owner'>
              <x xmlns='jabber:x:data' type='submit'>
                <field var='FORM_TYPE' type='hidden'>
                  <value>http://jabber.org/protocol/pubsub#node_config</value>
                </field>
                <field var='pubsub#title'><value>Princely Musings (Atom)</value></field>
                <field var='pubsub#deliver_notifications'><value>1</value></field>
                <field var='pubsub#deliver_payloads'><value>1</value></field>
                <field var='pubsub#persist_items'><value>1</value></field>
                <field var='pubsub#max_items'><value>10</value></field>
                <field var='pubsub#access_model'><value>open</value></field>
                <field var='pubsub#publish_model'><value>publishers</value></field>
                <field var='pubsub#send_last_published_item'><value>never</value></field>
                <field var='pubsub#presence_based_delivery'><value>false</value></field>
                <field var='pubsub#notify_config'><value>0</value></field>
                <field var='pubsub#notify_delete'><value>0</value></field>
                <field var='pubsub#notify_retract'><value>0</value></field>
                <field var='pubsub#notify_sub'><value>0</value></field>
                <field var='pubsub#max_payload_size'><value>1028</value></field>
                <field var='pubsub#type'><value>http://www.w3.org/2005/Atom</value></field>
                <field var='pubsub#body_xslt'>
                  <value>http://jabxslt.jabberstudio.org/atom_body.xslt</value>
                </field>
              </x>
            </configure>";

        [Fact]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Owner.Configure);

            new Matrix.Xmpp.PubSub.Owner.Configure().ShouldBe(XML1);
        }

        [Fact]
        public void Test2()
        {
            var xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.PubSub.Owner.Configure);

            var conf = xmpp1 as Matrix.Xmpp.PubSub.Owner.Configure;
            Assert.Equal(conf.XData != null, true);
        }
    }

    
}
