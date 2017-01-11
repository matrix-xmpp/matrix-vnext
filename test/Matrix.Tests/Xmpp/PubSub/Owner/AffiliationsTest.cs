using System.Collections.Generic;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using NUnit.Framework;
using Xunit;
using Affiliation=Matrix.Xmpp.PubSub.Owner.Affiliation;
using Affiliations=Matrix.Xmpp.PubSub.Owner.Affiliations;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    
    public class AffiliationsTest
    {
        private const string XML1 = @"<affiliations node='princely_musings' xmlns='http://jabber.org/protocol/pubsub#owner'>
                                      <affiliation jid='hamlet@denmark.lit' affiliation='owner'/>
                                      <affiliation jid='polonius@denmark.lit' affiliation='outcast'/>
                                    </affiliations>";
        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.Equal(true, xmpp1 is Affiliations);

            var affs = xmpp1 as Affiliations;
            if (affs != null)
            {
                IEnumerable<Affiliation> affiliations = affs.GetAffiliations();
                Assert.Equal(affs.Node, "princely_musings");

                Assert.Equal(affiliations.Count(), 2);
                Assert.Equal(affiliations.ToArray()[0].AffiliationType, AffiliationType.Owner);
                Assert.Equal(affiliations.ToArray()[1].AffiliationType, AffiliationType.Outcast);

                Assert.Equal(affiliations.ToArray()[0].Jid.Equals("hamlet@denmark.lit"), true);
                Assert.Equal(affiliations.ToArray()[1].Jid.Equals("polonius@denmark.lit"), true);
            }
        }

        [Fact]
        public void Test2()
        {
            var affs = new Affiliations { Node = "princely_musings" };

            affs.AddAffiliation(new Affiliation { Jid = "hamlet@denmark.lit", AffiliationType = AffiliationType.Owner });
            affs.AddAffiliation(new Affiliation { Jid = "polonius@denmark.lit", AffiliationType = AffiliationType.Outcast });
            
            affs.ShouldBe(XML1);
        }
    }
}
