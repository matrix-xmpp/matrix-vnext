using System.Xml.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Matrix.Xmpp.Tests.Sasl
{
    [TestClass]
    public class MechanismTest
    {
        
        //const  string XML1 = "<stream:features><mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'><mechanism kerb:principal='xmpp/ssops.sso.avaya.com@SSO.AVAYA.COM' xmlns:kerb='http://jabber.com/protocol/kerberosinfo'>GSSAPI</mechanism></mechanisms></stream:features>";//const  string XML1 = "<stream:features><mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'><mechanism kerb:principal='xmpp/ssops.sso.avaya.com@SSO.AVAYA.COM' xmlns:kerb='http://jabber.com/protocol/kerberosinfo'>GSSAPI</mechanism></mechanisms></stream:features>";
        const string XML1 = "<mechanism xmlns='urn:ietf:params:xml:ns:xmpp-sasl' kerb:principal='xmpp/ssops.sso.avaya.com@SSO.AVAYA.COM' xmlns:kerb='http://jabber.com/protocol/kerberosinfo'>GSSAPI</mechanism>";

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Mechanism);

            var mech = xmpp1 as Mechanism;

            if (mech != null)
            {
                string princ = mech.GetAttribute("http://jabber.com/protocol/kerberosinfo", "principal");
                Assert.AreEqual(princ, "xmpp/ssops.sso.avaya.com@SSO.AVAYA.COM");
            }
        }

        [TestMethod]
        public void Test2()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            xmpp1.ShouldBeOfType<Mechanism>();
            
            //var mech = xmpp1 as Mechanism;

            //if (mech != null) Assert.AreEqual(mech.KerberosPrincipal, "xmpp/ssops.sso.avaya.com@SSO.AVAYA.COM");
        }

        [TestMethod]
        public void Test3()
        {
            var mech = new Mechanism();
            
            mech.Add(new XAttribute("{http://www.w3.org/2000/xmlns/}kerb", "http://jabber.com/protocol/kerberosinfo"));
            mech.Add(new XAttribute("{http://jabber.com/protocol/kerberosinfo}principal", "xmpp/ssops.sso.avaya.com@SSO.AVAYA.COM"));
        }
    }
}
