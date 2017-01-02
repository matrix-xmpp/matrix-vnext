//using Matrix.Xml;
//using Matrix.Xmpp.Client;
//using Matrix.Xmpp.XData;
//using Microsoft.VisualStudio.TestTools.UnitTesting;


//namespace Matrix.Xmpp.Tests.Muc
//{
//    [TestClass]
//    public class MucManagerTest
//    {
//        private string XML1 = @"<iq xmlns='jabber:client' type='set' to='dev@conference.ag-software.de' id='foo'>
//                <query xmlns='http://jabber.org/protocol/muc#owner'>
//                    <x xmlns='jabber:x:data' type='submit'>
//                        <field type='hidden' var='FORM_TYPE'>
//                            <value>http://jabber.org/protocol/muc#roomconfig</value>
//                        </field>               
//                    </x>
//                </query>
//               </iq>";


//        [TestMethod]
//        public void Test()
//        {
//            var data = new Data
//                           {
//                               Type = FormType.Submit
//                           };

//            var field = new Field
//                            {
//                                Type = FieldType.Hidden,
//                                Var = "FORM_TYPE"
//                            };

//            field.AddValue("http://jabber.org/protocol/muc#roomconfig");

//            data.AddField(field);

//            var mm = new MucManager();
//            var iq = mm.CreateSubmitRoomConfigurationStanza("dev@conference.ag-software.de", data);
//            iq.Id = "foo";
//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML1, iq));

//        }

//        [TestMethod]
//        public void GetXDataFromConfig()
//        {
//            string XML = @"<iq from='coven@chat.shakespeare.lit'
//                xmlns='jabber:client'
//                id='config1'
//                to='crone1@shakespeare.lit/desktop'
//                type='result'>
//              <query xmlns='http://jabber.org/protocol/muc#owner'>
//                <x xmlns='jabber:x:data' type='form'>
//                  <title>Configuration for coven Room</title>
//                  <instructions>
//                    Complete this form to modify the
//                    configuration of your room.
//                  </instructions>
//                  <field
//                      type='hidden'
//                      var='FORM_TYPE'>
//                    <value>http://jabber.org/protocol/muc#roomconfig</value>
//                  </field>      
//                </x>
//              </query>
//            </iq>";

//            var xmpp1 = XmppXElement.LoadXml(XML);


//            var iq = xmpp1 as Iq;

//            if (iq != null)
//            {
//                var query = iq.Element<Matrix.Xmpp.Muc.Owner.OwnerQuery>();
//                if (query != null)
//                {
//                    var xdata = query.XData;
//                }

//            }
//        }

//        [TestMethod]
//        public void TestDestroy()
//        {
//            string XML1 =
//                @"<iq xmlns='jabber:client' id='foo'
//                to='heath@chat.shakespeare.lit'
//                type='set'>
//              <query xmlns='http://jabber.org/protocol/muc#owner'>
//                <destroy jid='coven@chat.shakespeare.lit'>
//                  <reason>Macbeth doth come.</reason>
//                </destroy>
//              </query>
//            </iq>";

//            var mm = new MucManager();
//            var iq = mm.CreateDestroyRoomStanza("heath@chat.shakespeare.lit", "coven@chat.shakespeare.lit", "Macbeth doth come.");
//            iq.Id = "foo";
//            XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML1, iq));

//            var xmpp1 = XmppXElement.LoadXml(XML1);
            

//            var iq1 = xmpp1 as Iq;

//            if (iq1 != null)
//            {
//                var query = iq1.Element<Matrix.Xmpp.Muc.Owner.OwnerQuery>();
//                if (query != null)
//                {
//                    var destroy = query.Element<Matrix.Xmpp.Muc.Owner.Destroy>();
//                    Assert.AreEqual(destroy.Reason, "Macbeth doth come.");
//                    Assert.AreEqual(destroy.Jid.Equals("coven@chat.shakespeare.lit"), true);
//                }
                
//            }
//        }
//    }
//}
