using System.Collections.Generic;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using Shouldly;
using Xunit;
using Affiliation=Matrix.Xmpp.PubSub.Owner.Affiliation;
using Affiliations=Matrix.Xmpp.PubSub.Owner.Affiliations;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    
    public class AffiliationsTest
    {
        [Fact]
        public void ShoudBeOfTypeAffiliations()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.affiliations1.xml")).ShouldBeOfType<Affiliations>();
        }

        [Fact]
        public void TestAffiliations()
        {
            var affs = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.affiliations1.xml")).Cast<Affiliations>();
            
            IEnumerable<Affiliation> affiliations = affs.GetAffiliations();
            Assert.Equal("princely_musings", affs.Node);

            Assert.Equal(2, affiliations.Count());
            Assert.Equal(AffiliationType.Owner, affiliations.ToArray()[0].AffiliationType);
            Assert.Equal(AffiliationType.Outcast, affiliations.ToArray()[1].AffiliationType);

            Assert.True(affiliations.ToArray()[0].Jid.Equals("hamlet@denmark.lit"));
            Assert.True(affiliations.ToArray()[1].Jid.Equals("polonius@denmark.lit"));
        }

        [Fact]
        public void TestBuildAffiliations()
        {
            var affs = new Affiliations { Node = "princely_musings" };
            affs.AddAffiliation(new Affiliation { Jid = "hamlet@denmark.lit", AffiliationType = AffiliationType.Owner });
            affs.AddAffiliation(new Affiliation { Jid = "polonius@denmark.lit", AffiliationType = AffiliationType.Outcast });
            
            affs.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.affiliations1.xml"));
        }
    }
}
