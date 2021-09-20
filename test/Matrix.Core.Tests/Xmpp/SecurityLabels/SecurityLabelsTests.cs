using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.SecurityLabels;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.SecurityLabels
{
    public class SecurityLabelsTests
    {
        [Fact]
        public void TestMessageWuthSecurityLabel()
        {
            var msg = XmppXElement.LoadXml(Resource.Get("Xmpp.SecurityLabels.message1.xml")).Cast<Message>();
            var secLabel = msg.Element<SecurityLabel>();

            Assert.True(secLabel != null);

            var display = secLabel.DisplayMarking;
            Assert.True(display.ForegroundColor == Color.Black);
            Assert.True(display.BackgroundColor == Color.Red);
            Assert.Equal("SECRET", display.Value);

            var label = secLabel.Label;
            var ess = label.EssSecurityLabel;
            Assert.Equal("MQYCAQQGASk=", ess.Value.Trim());
        }

        [Fact]
        public void TestBuildMessageWithSecurityLabel()
        {
            new Message
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
            }
            .ShouldBe(Resource.Get("Xmpp.SecurityLabels.message1.xml"));
        }

        [Fact]
        public void TestShouldbeOfTypeCatalog()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.SecurityLabels.catalog1.xml")).ShouldBeOfType<Catalog>();
        }

        [Fact]
        public void TestCatalog()
        {
            var catalog = XmppXElement.LoadXml(Resource.Get("Xmpp.SecurityLabels.catalog1.xml")).Cast<Catalog>();

            Assert.True(catalog != null);
            Assert.Equal("example.com", catalog.To.Bare);
            Assert.Equal("Default", catalog.CatalogName);
            Assert.Equal("an example set of labels", catalog.Description);
            Assert.False(catalog.Restrictive);

            var items = catalog.GetItems();
            Assert.Equal(4, items.Count());
            var item1 = items.ElementAt(0);
            Assert.Equal("Classified|SECRET", item1.Selector);
            var seclabel1 = item1.SecurityLabel;
            Assert.True(seclabel1.DisplayMarking.ForegroundColor == Color.Black);
            Assert.True(seclabel1.DisplayMarking.BackgroundColor == Color.Red);
            Assert.Equal("SECRET", seclabel1.DisplayMarking.Value);

            var item2 = items.ElementAt(1);
            Assert.Equal("Classified|CONFIDENTIAL", item2.Selector);
            var seclabel2 = item2.SecurityLabel;
            Assert.True(seclabel2.DisplayMarking.ForegroundColor == Color.Black);
            Assert.True(seclabel2.DisplayMarking.BackgroundColor == Color.Navy);
            Assert.Equal("CONFIDENTIAL", seclabel2.DisplayMarking.Value);

            var item3 = items.ElementAt(2);
            Assert.Equal("Classified|RESTRICTED", item3.Selector);
            var seclabel3 = item3.SecurityLabel;
            Assert.True(seclabel3.DisplayMarking.ForegroundColor == Color.Black);
            Assert.True(seclabel3.DisplayMarking.BackgroundColor == Color.Aqua);
            Assert.Equal("RESTRICTED", seclabel3.DisplayMarking.Value);
            
            var item4 = items.ElementAt(3);
            Assert.Equal("UNCLASSIFIED", item4.Selector);
        }

        [Fact]
        public void TestSecurityLabelWithUnknownColor()
        {
            var label = XmppXElement.LoadXml(Resource.Get("Xmpp.SecurityLabels.securitylabel1.xml")).Cast<SecurityLabel>();
            var display = label.DisplayMarking;
            Assert.True(display.ForegroundColor == Color.UnknownColor);
        }
    }
}
