//using Matrix;
//using Matrix.Xml;
//using Matrix.Xmpp.Client;
//using Matrix.Xmpp.PubSub;
//using Matrix.Xmpp.XData;
//using NUnit.Framework;
//using Xunit;
//using Item=Matrix.Xmpp.PubSub.Item;

//namespace Matrix.Tests.Xmpp.PubSub
//{
    
//    public class PubSubManagerTest
//    {

//        public class Entry : XmppXElement
//        {
//            public Entry() : base("http://www.ag-software.de/foo/bar", "entry")
//            {
//            }
        
//            public Presence Presence
//            {
//                get { return Element<Presence>(); }
//                set { Replace(value); }
//            }
//        }

//        public class Presence : XmppXElement
//        {
//            public Presence()
//                : base("http://www.ag-software.de/foo/bar", "presence")
//            {
//            }

//            public string Status
//            {
//                get { return GetTag("status"); }
//                set { SetTag("status", value); }
//            }

//        }
//        [Fact]
//        public void TestCreateInstantNode()
//        {
//            const string XML =
//                @"<iq type='set'            
//                    to='pubsub.shakespeare.lit'
//                    id='instant2'
//                    xmlns='jabber:client'>
//                    <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//                      <create/>
//                      <configure/>
//                    </pubsub>
//                </iq>";
//            var pm = new PubSubManager();
//            Iq iq = pm.CreateInstantNodeStanza("pubsub.shakespeare.lit");
//            iq.Id = "instant2";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestCreateNode()
//        {
//            const string XML = @"<iq type='set'
//                    to='pubsub.shakespeare.lit'
//                    id='create1'
//                    xmlns='jabber:client'>
//                    <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//                      <create node='princely_musings'/>
//                      <configure/>
//                    </pubsub>
//                </iq>";
            
//            var pm = new PubSubManager();
//            Iq iq = pm.CreateNodeStanza("pubsub.shakespeare.lit", "princely_musings", null);
//            iq.Id = "create1";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestCreateNode2()
//        {
//            const string XML =
//                @"<iq type='set'
//        to='pubsub.shakespeare.lit'
//        id='create2'
//        xmlns='jabber:client'>
//        <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//          <create node='princely_musings'/>
//          <configure>
//            <x xmlns='jabber:x:data' type='submit'>
//              <field var='FORM_TYPE' type='hidden'>
//                <value>http://jabber.org/protocol/pubsub#node_config</value>
//              </field>
//              <field var='pubsub#access_model'><value>whitelist</value></field>
//            </x>
//          </configure>
//        </pubsub>
//    </iq>";

           
//            var form = new Data {Type = FormType.Submit};

//            var field1 = form.AddField(new Field {Type = FieldType.Hidden, Var = "FORM_TYPE"});
//            field1.SetValue("http://jabber.org/protocol/pubsub#node_config");

//            var field2 = form.AddField(new Field {Var = "pubsub#access_model"});
//            field2.SetValue("whitelist");

//            var conf = new Configure {XData = form};

//            var pm = new PubSubManager();
//            Iq iq = pm.CreateNodeStanza("pubsub.shakespeare.lit", "princely_musings", conf);
//            iq.Id = "create2";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestDeleteNode()
//        {
//            const string XML =
//                @" <iq type='set'                
//                to='pubsub.shakespeare.lit'
//                id='delete1'
//                xmlns='jabber:client'>
//              <pubsub xmlns='http://jabber.org/protocol/pubsub#owner'>
//                <delete node='princely_musings'/>
//              </pubsub>
//            </iq>";

//            var pm = new PubSubManager();
//            Iq iq = pm.DeleteNodeStanza("pubsub.shakespeare.lit", "princely_musings");
//            iq.Id = "delete1";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestUnsubscribe()
//        {
//            const string XML =
//                @"<iq type='set'            
//            to='pubsub.shakespeare.lit'
//            id='unsub1'
//            xmlns='jabber:client'>
//          <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//             <unsubscribe
//                 node='princely_musings'
//                 jid='francisco@denmark.lit'/>
//          </pubsub>
//        </iq>";
//            var pm = new PubSubManager();
//            Iq iq = pm.UnsubscribeStanza("pubsub.shakespeare.lit", "princely_musings", "francisco@denmark.lit");
//            iq.Id = "unsub1";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestUnsubscribeWithSubId()
//        {
//            const string XML =
//                @"<iq type='set'            
//            to='pubsub.shakespeare.lit'
//            id='unsub1'
//            xmlns='jabber:client'>
//          <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//             <unsubscribe
//                 node='princely_musings'
//                 jid='francisco@denmark.lit'
//                subid='n1gbwYqLQyH4ILW71fVrk19MJATXnnqna0Wl0PpY'/>
//          </pubsub>
//        </iq>";
//            var pm = new PubSubManager();
//            Iq iq = pm.UnsubscribeStanza("pubsub.shakespeare.lit", "princely_musings", "n1gbwYqLQyH4ILW71fVrk19MJATXnnqna0Wl0PpY", "francisco@denmark.lit");
//            iq.Id = "unsub1";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestSubscribe()
//        {
//            const string XML =
//                @" <iq type='set'            
//            to='pubsub.shakespeare.lit'
//            id='sub1'
//            xmlns='jabber:client'>
//          <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//            <subscribe
//                node='princely_musings'
//                jid='francisco@denmark.lit'/>
//          </pubsub>
//        </iq>";

//            var pm = new PubSubManager();
//            Iq iq = pm.SubscribeStanza("pubsub.shakespeare.lit", "princely_musings", "francisco@denmark.lit");
//            iq.Id = "sub1";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestPublishItem()
//        {
//            const string XML = @"<iq type='set'            
//            to='pubsub.shakespeare.lit'
//            id='publish1'
//            xmlns='jabber:client'>
//          <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//            <publish node='princely_musings'>
//              <item>
//                <entry xmlns='http://www.w3.org/2005/Atom'>
//                  <title>Soliloquy</title>
//                  <summary>
//        To be, or not to be: that is the question:
//        Whether 'tis nobler in the mind to suffer
//        The slings and arrows of outrageous fortune,
//        Or to take arms against a sea of troubles,
//        And by opposing end them?
//                  </summary>
//                  <link rel='alternate' type='text/html'
//                        href='http://denmark.lit/2003/12/13/atom03'/>
//                  <id>tag:denmark.lit,2003:entry-32397</id>
//                  <published>2003-12-13T18:30:02Z</published>
//                  <updated>2003-12-13T18:30:02Z</updated>
//                </entry>
//              </item>
//            </publish>
//          </pubsub>
//        </iq>";

//            const string PAYLOAD1 =
//                @"<entry xmlns='http://www.w3.org/2005/Atom'>
//                  <title>Soliloquy</title>
//                  <summary>
//        To be, or not to be: that is the question:
//        Whether 'tis nobler in the mind to suffer
//        The slings and arrows of outrageous fortune,
//        Or to take arms against a sea of troubles,
//        And by opposing end them?
//                  </summary>
//                  <link rel='alternate' type='text/html'
//                        href='http://denmark.lit/2003/12/13/atom03'/>
//                  <id>tag:denmark.lit,2003:entry-32397</id>
//                  <published>2003-12-13T18:30:02Z</published>
//                  <updated>2003-12-13T18:30:02Z</updated>
//                </entry>";

//            var payload = XmppXElement.LoadXml(PAYLOAD1);

//            var item = new Item();
//            item.Add(payload);

//            var pm = new PubSubManager();
//            Iq iq = pm.PublishItemsStanza("pubsub.shakespeare.lit", "princely_musings", new[]{item});
//            iq.Id = "publish1";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestPublishItems()
//        {
//            const string XML = @"<iq type='set'            
//            to='pubsub.shakespeare.lit'
//            id='publish1'
//            xmlns='jabber:client'>
//          <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//            <publish node='princely_musings'>
//              <item>
//                <entry xmlns='http://www.w3.org/2005/Atom'>
//                  <title>Soliloquy</title>
//                  <summary>
//        To be, or not to be: that is the question:
//        Whether 'tis nobler in the mind to suffer
//        The slings and arrows of outrageous fortune,
//        Or to take arms against a sea of troubles,
//        And by opposing end them?
//                  </summary>
//                  <link rel='alternate' type='text/html'
//                        href='http://denmark.lit/2003/12/13/atom03'/>
//                  <id>tag:denmark.lit,2003:entry-32397</id>
//                  <published>2003-12-13T18:30:02Z</published>
//                  <updated>2003-12-13T18:30:02Z</updated>
//                </entry>
//              </item>
//              <item>
//                <entry xmlns='http://www.w3.org/2005/Atom'>
//                  <title>Soliloquy 2</title>
//                  <summary>
//        To be, or not to be: that is the question:
//        Whether 'tis nobler in the mind to suffer
//        The slings and arrows of outrageous fortune,
//        Or to take arms against a sea of troubles,
//        And by opposing end them?
//                  </summary>
//                  <link rel='alternate' type='text/html'
//                        href='http://denmark.lit/2003/12/13/atom03'/>
//                  <id>tag:denmark.lit,2003:entry-32397</id>
//                  <published>2003-12-13T18:30:02Z</published>
//                  <updated>2003-12-13T18:30:02Z</updated>
//                </entry>
//              </item>
//            </publish>
//          </pubsub>
//        </iq>";

//            const string PAYLOAD1 =
//                @"<entry xmlns='http://www.w3.org/2005/Atom'>
//                  <title>Soliloquy</title>
//                  <summary>
//        To be, or not to be: that is the question:
//        Whether 'tis nobler in the mind to suffer
//        The slings and arrows of outrageous fortune,
//        Or to take arms against a sea of troubles,
//        And by opposing end them?
//                  </summary>
//                  <link rel='alternate' type='text/html'
//                        href='http://denmark.lit/2003/12/13/atom03'/>
//                  <id>tag:denmark.lit,2003:entry-32397</id>
//                  <published>2003-12-13T18:30:02Z</published>
//                  <updated>2003-12-13T18:30:02Z</updated>
//                </entry>";

//            const string PAYLOAD2 =
//                @"<entry xmlns='http://www.w3.org/2005/Atom'>
//                  <title>Soliloquy 2</title>
//                  <summary>
//        To be, or not to be: that is the question:
//        Whether 'tis nobler in the mind to suffer
//        The slings and arrows of outrageous fortune,
//        Or to take arms against a sea of troubles,
//        And by opposing end them?
//                  </summary>
//                  <link rel='alternate' type='text/html'
//                        href='http://denmark.lit/2003/12/13/atom03'/>
//                  <id>tag:denmark.lit,2003:entry-32397</id>
//                  <published>2003-12-13T18:30:02Z</published>
//                  <updated>2003-12-13T18:30:02Z</updated>
//                </entry>";

           

//            var item1 = new Item();
//            item1.Add(XmppXElement.LoadXml(PAYLOAD1));

//            var item2 = new Item();
//            item2.Add(XmppXElement.LoadXml(PAYLOAD2));

//            var pm = new PubSubManager();
//            Iq iq = pm.PublishItemsStanza("pubsub.shakespeare.lit", "princely_musings", new[] { item1, item2 });
//            iq.Id = "publish1";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestRetractItem()
//        {
//            const string XML =
//                @"<iq type='set'                
//                to='pubsub.shakespeare.lit'
//                id='retract1'
//                xmlns='jabber:client'>
//              <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//                <retract node='princely_musings'>
//                  <item id='ae890ac52d0df67ed7cfdf51b644e901'/>
//                </retract>
//              </pubsub>
//            </iq>";
            

//            var pm = new PubSubManager();
//            var iq = pm.RetractItemsStanza("pubsub.shakespeare.lit", "princely_musings", new []{"ae890ac52d0df67ed7cfdf51b644e901"});
//            iq.Id = "retract1";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

      

//        [Fact]
//        public void TestRetractItems()
//        {
//            const string XML =
//                @"<iq type='set'                
//                to='pubsub.shakespeare.lit'
//                id='retract1'
//                xmlns='jabber:client'>
//              <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//                <retract node='princely_musings'>
//                  <item id='ae890ac52d0df67ed7cfdf51b644e901'/>
//                  <item id='abc'/>
//                  <item id='def'/>
//                </retract>
//              </pubsub>
//            </iq>";
            

//            var pm = new PubSubManager();
//            var iq = pm.RetractItemsStanza("pubsub.shakespeare.lit", "princely_musings", new []{"ae890ac52d0df67ed7cfdf51b644e901", "abc", "def"});
//            iq.Id = "retract1";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestPurgeNode()
//        {
//            const string XML = @"<iq type='set'                
//                to='pubsub.shakespeare.lit'
//                id='purge1'
//                xmlns='jabber:client'>
//              <pubsub xmlns='http://jabber.org/protocol/pubsub#owner'>
//                <purge node='blogs/princely_musings'/>
//              </pubsub>
//            </iq>";

//            var pm = new PubSubManager();
//            var iq = pm.PurgeNodeStanza("pubsub.shakespeare.lit", "blogs/princely_musings");
//            iq.Id = "purge1";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }
    
//        [Fact]
//        public void TestCreateNodeConfigurationStanza()
//        {
//            const string XML =
//                @"<iq type='get'
//                from='hamlet@denmark.lit/elsinore'
//                to='pubsub.shakespeare.lit'
//                id='config1'
//                xmlns='jabber:client'>
//              <pubsub xmlns='http://jabber.org/protocol/pubsub#owner'>
//                <configure node='princely_musings'/>
//              </pubsub>
//            </iq>";

//            var pm = new PubSubManager();
//            var iq = pm.RequestNodeConfigurationStanza("pubsub.shakespeare.lit", "princely_musings");
//            iq.Id = "config1";
//            iq.From = "hamlet@denmark.lit/elsinore";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestSubmitNodeConfigurationStanza()
//        {
//            const string XML1 = @"<iq type='set'
//            from='hamlet@denmark.lit/elsinore'
//            to='pubsub.shakespeare.lit'
//            id='config2'
//            xmlns='jabber:client'>
//              <pubsub xmlns='http://jabber.org/protocol/pubsub#owner'>
//                <configure node='princely_musings'>
//                  <x xmlns='jabber:x:data' type='submit'>
//                    <field var='FORM_TYPE' type='hidden'>
//                      <value>http://jabber.org/protocol/pubsub#node_config</value>
//                    </field>
//                    <field var='pubsub#title'><value>Princely Musings (Atom)</value></field>
//                    <field var='pubsub#deliver_notifications'><value>1</value></field>
//                    <field var='pubsub#deliver_payloads'><value>1</value></field>
//                    <field var='pubsub#persist_items'><value>1</value></field>
//                    <field var='pubsub#max_items'><value>10</value></field>
//                    <field var='pubsub#access_model'><value>open</value></field>
//                    <field var='pubsub#publish_model'><value>publishers</value></field>
//                    <field var='pubsub#send_last_published_item'><value>never</value></field>
//                    <field var='pubsub#presence_based_delivery'><value>false</value></field>
//                    <field var='pubsub#notify_config'><value>0</value></field>
//                    <field var='pubsub#notify_delete'><value>0</value></field>
//                    <field var='pubsub#notify_retract'><value>0</value></field>
//                    <field var='pubsub#notify_sub'><value>0</value></field>
//                    <field var='pubsub#max_payload_size'><value>1028</value></field>
//                    <field var='pubsub#type'><value>http://www.w3.org/2005/Atom</value></field>
//                    <field var='pubsub#body_xslt'>
//                      <value>http://jabxslt.jabberstudio.org/atom_body.xslt</value>
//                    </field>
//                  </x>
//                </configure>
//              </pubsub>
//            </iq>";

//            const string XML2 = @"<x xmlns='jabber:x:data' type='submit'>
//                <field var='FORM_TYPE' type='hidden'>
//                  <value>http://jabber.org/protocol/pubsub#node_config</value>
//                </field>
//                <field var='pubsub#title'><value>Princely Musings (Atom)</value></field>
//                <field var='pubsub#deliver_notifications'><value>1</value></field>
//                <field var='pubsub#deliver_payloads'><value>1</value></field>
//                <field var='pubsub#persist_items'><value>1</value></field>
//                <field var='pubsub#max_items'><value>10</value></field>
//                <field var='pubsub#access_model'><value>open</value></field>
//                <field var='pubsub#publish_model'><value>publishers</value></field>
//                <field var='pubsub#send_last_published_item'><value>never</value></field>
//                <field var='pubsub#presence_based_delivery'><value>false</value></field>
//                <field var='pubsub#notify_config'><value>0</value></field>
//                <field var='pubsub#notify_delete'><value>0</value></field>
//                <field var='pubsub#notify_retract'><value>0</value></field>
//                <field var='pubsub#notify_sub'><value>0</value></field>
//                <field var='pubsub#max_payload_size'><value>1028</value></field>
//                <field var='pubsub#type'><value>http://www.w3.org/2005/Atom</value></field>
//                <field var='pubsub#body_xslt'>
//                  <value>http://jabxslt.jabberstudio.org/atom_body.xslt</value>
//                </field>
//              </x>";

//            var pm = new PubSubManager();
            
//            var form = XmppXElement.LoadXml(XML2) as Data;
            
//            var iq = pm.SubmitNodeConfigurationStanza("pubsub.shakespeare.lit", "princely_musings", form);
//            iq.Id = "config2";
//            iq.From = "hamlet@denmark.lit/elsinore";
            
//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML1, iq));
//        }

//        [Fact]
//        public void TestRetrieveAffiliationsListStanza()
//        {
//            const string XML =
//                @"<iq type='get'                
//                to='pubsub.shakespeare.lit'
//                id='ent1'
//                xmlns='jabber:client'>
//              <pubsub xmlns='http://jabber.org/protocol/pubsub#owner'>
//                <affiliations node='princely_musings'/>
//              </pubsub>
//            </iq>";

//            var pm = new PubSubManager();
//            var iq = pm.RequestAffiliationsListStanza("pubsub.shakespeare.lit", "princely_musings");
//            iq.Id = "ent1";
            
//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void TestRequestSubscriptionsList()
//        {
//            const string XML =
//                @"<iq type='get'                   
//                    to='pubsub.shakespeare.lit'
//                    id='subman1'
//                    xmlns='jabber:client'>
//                  <pubsub xmlns='http://jabber.org/protocol/pubsub#owner'>
//                    <subscriptions node='princely_musings'/>
//                  </pubsub>
//                </iq>";

//            var pm = new PubSubManager();
//            var iq = pm.RequestSubscriptionsListStanza("pubsub.shakespeare.lit", "princely_musings");
//            iq.Id = "subman1";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void ModifyAffiliations()
//        {
//            const string XML =
//                @"<iq type='set'                
//                to='pubsub.shakespeare.lit'
//                id='ent3'
//                xmlns='jabber:client'>
//              <pubsub xmlns='http://jabber.org/protocol/pubsub#owner'>
//                <affiliations node='princely_musings'>
//                  <affiliation jid='hamlet@denmark.lit' affiliation='none'/>
//                  <affiliation jid='polonius@denmark.lit' affiliation='none'/>
//                  <affiliation jid='bard@shakespeare.lit' affiliation='publisher'/>
//                </affiliations>
//              </pubsub>
//            </iq>";

//            var affs = new Matrix.Xmpp.PubSub.Owner.Affiliation[3];
//            affs[0] = new Matrix.Xmpp.PubSub.Owner.Affiliation { Jid = "hamlet@denmark.lit", AffiliationType = AffiliationType.None };
//            affs[1] = new Matrix.Xmpp.PubSub.Owner.Affiliation { Jid = "polonius@denmark.lit", AffiliationType = AffiliationType.None };
//            affs[2] = new Matrix.Xmpp.PubSub.Owner.Affiliation { Jid = "bard@shakespeare.lit", AffiliationType = AffiliationType.Publisher };
           
//            var pm = new PubSubManager();
//            var iq = pm.ModifyAffiliationsStanza("pubsub.shakespeare.lit", "princely_musings", affs);
//            iq.Id = "ent3";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        [Fact]
//        public void RequestSubscriptionsStanzaTest()
//        {
//            const string XML =
//                @"<iq type='get'                
//                to='pubsub.shakespeare.lit'
//                id='ent3'
//                xmlns='jabber:client'>
//               <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//                <subscriptions node='somenode'/>
//              </pubsub>
//            </iq>";

//            var pm = new PubSubManager();
//            var iq = pm.RequestSubscriptionsStanza("pubsub.shakespeare.lit", "somenode");
//            iq.Id = "ent3";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }


//        [Fact]
//        public void RequestAllSubscriptionsStanzaTest()
//        {
//            const string XML =
//                @"<iq type='get'                
//                to='pubsub.shakespeare.lit'
//                id='ent3'
//                xmlns='jabber:client'>
//               <pubsub xmlns='http://jabber.org/protocol/pubsub'>
//                <subscriptions/>
//              </pubsub>
//            </iq>";

//            var pm = new PubSubManager();
//            var iq = pm.RequestAllSubscriptionsStanza("pubsub.shakespeare.lit");
//            iq.Id = "ent3";

//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }

//        /*
//        [Fact]
//        public void TestCustom()
//        {
            
//            var entry = new Entry {Presence = new Presence() {Status = "Foo"}};

//            var item = new Item();
//            item.Add(entry);

//            var pm = new PubSubManager();
//            pm.PublishItem("pubsub.shakespeare.lit", "princely_musings", item);
            

//            //XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, iq));
//        }
//        */
//    }
//}