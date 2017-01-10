using System;
using Matrix.Xml;
using Xunit;

namespace Matrix.Tests.Xmpp.Privacy
{
    
    public class PrivacyTest
    {
        private const string XML1 = @"<query xmlns='jabber:iq:privacy'>
                                  <active name='private'/>
                                  <default name='public'/>
                                  <list name='public'/>
                                  <list name='private'/>
                                  <list name='special'/>
                                </query>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Matrix.Xmpp.Privacy.Privacy);

            var priv = xmpp1 as Matrix.Xmpp.Privacy.Privacy;
            Assert.Equal(priv.Default.Name, "public");
            Assert.Equal(priv.Active.Name, "private");

            priv.Active = null;
            priv.Default = null;
            
            //Assert.Equal(item.Action, Matrix.Xmpp.Privacy.Action.deny);
            //Assert.Equal(item.Order, 5);
            //Assert.Equal(item.Type, Matrix.Xmpp.Privacy.Type.subscription);
        }
    }
}
