using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.SecurityLabels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.Xmpp.Tests.SecurityLabels
{
    [TestClass]
    public class SecurityLabelsTests
    {
        private const string xml1 = @"<message xmlns='jabber:client'>
    <body>This content is classified.</body>
    <securitylabel xmlns='urn:xmpp:sec-label:0'>
        <displaymarking fgcolor='black' bgcolor='red'>SECRET</displaymarking>
        <label><esssecuritylabel xmlns='urn:xmpp:sec-label:ess:0'>MQYCAQQGASk=</esssecuritylabel></label>
    </securitylabel>
</message>";

        private const string xml2 = @"<catalog xmlns='urn:xmpp:sec-label:catalog:2'
      to='example.com' name='Default'
      desc='an example set of labels'
          restrict='false'>
        <item selector='Classified|SECRET'>
            <securitylabel xmlns='urn:xmpp:sec-label:0'>
                <displaymarking fgcolor='black' bgcolor='red'>SECRET</displaymarking>
                <label>
                    <esssecuritylabel xmlns='urn:xmpp:sec-label:ess:0'
                        >MQYCAQQGASk=</esssecuritylabel>
                </label>
            </securitylabel>
        </item>
        <item selector='Classified|CONFIDENTIAL'>
            <securitylabel xmlns='urn:xmpp:sec-label:0'>
                <displaymarking fgcolor='black' bgcolor='navy'>CONFIDENTIAL</displaymarking>
                <label>
                    <esssecuritylabel xmlns='urn:xmpp:sec-label:ess:0'
                        >MQYCAQMGASk</esssecuritylabel>
                </label>
            </securitylabel>
        </item>
        <item selector='Classified|RESTRICTED'>
            <securitylabel xmlns='urn:xmpp:sec-label:0'>
                <displaymarking fgcolor='black' bgcolor='aqua'>RESTRICTED</displaymarking>
                <label>
                    <esssecuritylabel xmlns='urn:xmpp:sec-label:ess:0'
                        >MQYCAQIGASk=</esssecuritylabel>
                </label>
            </securitylabel>
        </item>
        <item selector='UNCLASSIFIED' default='true'/>
  </catalog>";

        private const string xml3 = @"<securitylabel xmlns='urn:xmpp:sec-label:0'>
                <displaymarking fgcolor='blackxxx' bgcolor='red'>SECRET</displaymarking>
                <label>
                    <esssecuritylabel xmlns='urn:xmpp:sec-label:ess:0'
                        >MQYCAQQGASk=</esssecuritylabel>
                </label>
            </securitylabel>";

        [TestMethod]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);

            var msg = xmpp1 as Message;
            var secLabel = msg.Element<SecurityLabel>();

            Assert.AreEqual(secLabel != null, true);

            var display = secLabel.DisplayMarking;
            Assert.AreEqual(display.ForegroundColor == Color.Black, true);
            Assert.AreEqual(display.BackgroundColor == Color.Red, true);
            Assert.AreEqual(display.Value, "SECRET");

            var label = secLabel.Label;
            var ess = label.EssSecurityLabel;
            Assert.AreEqual(ess.Value.Trim(), "MQYCAQQGASk=");
        }

        [TestMethod]
        public void Test2()
        {
            var msg = new Message
                {
                    Body = "This content is classified.",
                    SecurityLabel = new SecurityLabel
                    {
                        DisplayMarking = new DisplayMarking
                            {
                                ForegroundColor = Color.Black,
                                BackgroundColor = Color.Red,
                                Value = "SECRET"
                            },
                        Label = new Label
                            {
                                EssSecurityLabel = new EssSecurityLabel {Value = "MQYCAQQGASk="}
                            }
                    }
                };

            msg.ShouldBe(xml1);
        }

        [TestMethod]
        public void TestCatalog()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml2);

            var catalog = xmpp1 as Catalog;

            Assert.AreEqual(catalog != null, true);
            Assert.AreEqual(catalog.To.Bare, "example.com");
            Assert.AreEqual(catalog.CatalogName, "Default");
            Assert.AreEqual(catalog.Description, "an example set of labels");
            Assert.AreEqual(catalog.Restrictive, false);

            var items = catalog.GetItems();
            Assert.AreEqual(items.Count(), 4);
            var item1 = items.ElementAt(0);
            Assert.AreEqual(item1.Selector, "Classified|SECRET");
            var seclabel1 = item1.SecurityLabel;
            Assert.AreEqual(seclabel1.DisplayMarking.ForegroundColor == Color.Black, true);
            Assert.AreEqual(seclabel1.DisplayMarking.BackgroundColor == Color.Red, true);
            Assert.AreEqual(seclabel1.DisplayMarking.Value, "SECRET");

            var item2 = items.ElementAt(1);
            Assert.AreEqual(item2.Selector, "Classified|CONFIDENTIAL");
            var seclabel2 = item2.SecurityLabel;
            Assert.AreEqual(seclabel2.DisplayMarking.ForegroundColor == Color.Black, true);
            Assert.AreEqual(seclabel2.DisplayMarking.BackgroundColor == Color.Navy, true);
            Assert.AreEqual(seclabel2.DisplayMarking.Value, "CONFIDENTIAL");

            var item3 = items.ElementAt(2);
            Assert.AreEqual(item3.Selector, "Classified|RESTRICTED");
            var seclabel3 = item3.SecurityLabel;
            Assert.AreEqual(seclabel3.DisplayMarking.ForegroundColor == Color.Black, true);
            Assert.AreEqual(seclabel3.DisplayMarking.BackgroundColor == Color.Aqua, true);
            Assert.AreEqual(seclabel3.DisplayMarking.Value, "RESTRICTED");


            var item4 = items.ElementAt(3);
            Assert.AreEqual(item4.Selector, "UNCLASSIFIED");
        }

        [TestMethod]
        public void TestSecurityLabelWithUnknownColor()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml3);

            var label = xmpp1 as SecurityLabel;
            var display = label.DisplayMarking;
            Assert.AreEqual(display.ForegroundColor == Color.UnknownColor, true);
        }
    }
}
