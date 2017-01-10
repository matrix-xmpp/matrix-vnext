using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.SecurityLabels;
using Xunit;

namespace Matrix.Tests.Xmpp.SecurityLabels
{
    
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

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml1);

            var msg = xmpp1 as Message;
            var secLabel = msg.Element<SecurityLabel>();

            Assert.Equal(secLabel != null, true);

            var display = secLabel.DisplayMarking;
            Assert.Equal(display.ForegroundColor == Color.Black, true);
            Assert.Equal(display.BackgroundColor == Color.Red, true);
            Assert.Equal(display.Value, "SECRET");

            var label = secLabel.Label;
            var ess = label.EssSecurityLabel;
            Assert.Equal(ess.Value.Trim(), "MQYCAQQGASk=");
        }

        [Fact]
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

        [Fact]
        public void TestCatalog()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml2);

            var catalog = xmpp1 as Catalog;

            Assert.Equal(catalog != null, true);
            Assert.Equal(catalog.To.Bare, "example.com");
            Assert.Equal(catalog.CatalogName, "Default");
            Assert.Equal(catalog.Description, "an example set of labels");
            Assert.Equal(catalog.Restrictive, false);

            var items = catalog.GetItems();
            Assert.Equal(items.Count(), 4);
            var item1 = items.ElementAt(0);
            Assert.Equal(item1.Selector, "Classified|SECRET");
            var seclabel1 = item1.SecurityLabel;
            Assert.Equal(seclabel1.DisplayMarking.ForegroundColor == Color.Black, true);
            Assert.Equal(seclabel1.DisplayMarking.BackgroundColor == Color.Red, true);
            Assert.Equal(seclabel1.DisplayMarking.Value, "SECRET");

            var item2 = items.ElementAt(1);
            Assert.Equal(item2.Selector, "Classified|CONFIDENTIAL");
            var seclabel2 = item2.SecurityLabel;
            Assert.Equal(seclabel2.DisplayMarking.ForegroundColor == Color.Black, true);
            Assert.Equal(seclabel2.DisplayMarking.BackgroundColor == Color.Navy, true);
            Assert.Equal(seclabel2.DisplayMarking.Value, "CONFIDENTIAL");

            var item3 = items.ElementAt(2);
            Assert.Equal(item3.Selector, "Classified|RESTRICTED");
            var seclabel3 = item3.SecurityLabel;
            Assert.Equal(seclabel3.DisplayMarking.ForegroundColor == Color.Black, true);
            Assert.Equal(seclabel3.DisplayMarking.BackgroundColor == Color.Aqua, true);
            Assert.Equal(seclabel3.DisplayMarking.Value, "RESTRICTED");


            var item4 = items.ElementAt(3);
            Assert.Equal(item4.Selector, "UNCLASSIFIED");
        }

        [Fact]
        public void TestSecurityLabelWithUnknownColor()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(xml3);

            var label = xmpp1 as SecurityLabel;
            var display = label.DisplayMarking;
            Assert.Equal(display.ForegroundColor == Color.UnknownColor, true);
        }
    }
}
