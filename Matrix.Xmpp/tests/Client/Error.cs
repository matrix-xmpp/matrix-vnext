using Microsoft.VisualStudio.TestTools.UnitTesting;

using Matrix.Xml;

namespace Matrix.Xmpp.Tests.Client
{
    /*
     <error type='modify'>
       <bad-request xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/>
     </error>
    */
    [TestClass]
    public class Error
    {
       
        [TestMethod]
        public void Test()
        {
            // with <text/> tag
            string xml1 = "<error type='modify' xmlns='jabber:client'><bad-request xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/><text xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'>dummy text</text></error>";
            
            // without <text/> tag
            string xml2 = "<error type='modify' xmlns='jabber:client'><bad-request xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/></error>";
            
            // more error test stanzas
            string xml3 = "<error type='modify' xmlns='jabber:client'><forbidden xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/></error>";
            string xml4 = "<error type='modify' xmlns='jabber:client'><gone xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/></error>";
            string xml5 = "<error type='modify' xmlns='jabber:client'><internal-server-error xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/></error>";
            string xml6 = "<error type='modify' xmlns='jabber:client'><item-not-found xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/></error>";
            string xml7 = "<error type='modify' xmlns='jabber:client'><jid-malformed xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/></error>";
            string xml8 = "<error type='modify' xmlns='jabber:client'><bad-request xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/></error>";
            string xml9 = "<error type='modify' xmlns='jabber:client'><not-acceptable xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/></error>";
            string xml10 = "<error type='modify' xmlns='jabber:client'><not-authorized xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/></error>";
            string xml11 = "<error type='modify' xmlns='jabber:client'><not-modified xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/></error>";
            string xml12 = "<error type='modify' xmlns='jabber:client'><payment-required xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/></error>";

            Assert.AreEqual(true, ((Matrix.Xmpp.Client.Error)(XmppXElement.LoadXml(xml3))).Condition == Matrix.Xmpp.Base.ErrorCondition.Forbidden);
            Assert.AreEqual(true, ((Matrix.Xmpp.Client.Error)(XmppXElement.LoadXml(xml4))).Condition == Matrix.Xmpp.Base.ErrorCondition.Gone);
            Assert.AreEqual(true, ((Matrix.Xmpp.Client.Error)(XmppXElement.LoadXml(xml5))).Condition == Matrix.Xmpp.Base.ErrorCondition.InternalServerError);
            Assert.AreEqual(true, ((Matrix.Xmpp.Client.Error)(XmppXElement.LoadXml(xml6))).Condition == Matrix.Xmpp.Base.ErrorCondition.ItemNotFound);
            Assert.AreEqual(true, ((Matrix.Xmpp.Client.Error)(XmppXElement.LoadXml(xml7))).Condition == Matrix.Xmpp.Base.ErrorCondition.JidMalformed);
            Assert.AreEqual(true, ((Matrix.Xmpp.Client.Error)(XmppXElement.LoadXml(xml8))).Condition == Matrix.Xmpp.Base.ErrorCondition.BadRequest);
            Assert.AreEqual(true, ((Matrix.Xmpp.Client.Error)(XmppXElement.LoadXml(xml9))).Condition == Matrix.Xmpp.Base.ErrorCondition.NotAcceptable);
            Assert.AreEqual(true, ((Matrix.Xmpp.Client.Error)(XmppXElement.LoadXml(xml10))).Condition == Matrix.Xmpp.Base.ErrorCondition.NotAuthorized);
            Assert.AreEqual(true, ((Matrix.Xmpp.Client.Error)(XmppXElement.LoadXml(xml11))).Condition == Matrix.Xmpp.Base.ErrorCondition.NotModified);
            Assert.AreEqual(true, ((Matrix.Xmpp.Client.Error)(XmppXElement.LoadXml(xml12))).Condition == Matrix.Xmpp.Base.ErrorCondition.PaymentRequired);
            
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);
           
            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Client.Error);
            
            Matrix.Xmpp.Client.Error err = xmpp1 as Matrix.Xmpp.Client.Error;
            Assert.AreEqual("dummy text", err.Text);


            Matrix.Xmpp.Client.Error err2 = new Matrix.Xmpp.Client.Error(Matrix.Xmpp.Base.ErrorCondition.BadRequest);
            err2.Text = "dummy text";         
            err2.ShouldBe(xml1);
            
            err2.Text = null;
            err2.ShouldBe(xml2);
        }
    }
}
