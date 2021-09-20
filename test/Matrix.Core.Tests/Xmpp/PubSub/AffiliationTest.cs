using System.Collections.Generic;
using System.Linq;
using Xunit;

using Shouldly;
using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using Matrix.Xmpp.Client;

namespace Matrix.Tests.Xmpp.PubSub
{
    public class AffiliationTest
    {
        [Fact]
        public void ShoudBeOfTypeAffiliations()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.affiliations4.xml")).ShouldBeOfType<Affiliations>();
        }

        [Fact]
        public void ShoudBeOfTypeOwnerAffiliations()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.affiliations2.xml")).ShouldBeOfType<Matrix.Xmpp.PubSub.Owner.Affiliations>();
        }

        [Fact]
        public void TestAffiliations()
        {
            var affs = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.affiliations4.xml")).Cast<Affiliations>();
         
            IEnumerable<Affiliation> affiliations = affs.GetAffiliations();
            Assert.Equal("princely_musings", affs.Node);

            Assert.Equal(5, affiliations.Count());
            Assert.Equal(AffiliationType.Owner, affiliations.ToArray()[0].AffiliationType);
            Assert.Equal(AffiliationType.Publisher, affiliations.ToArray()[1].AffiliationType);
            Assert.Equal(AffiliationType.Outcast, affiliations.ToArray()[2].AffiliationType);
            Assert.Equal(AffiliationType.Owner, affiliations.ToArray()[3].AffiliationType);
            Assert.Equal(AffiliationType.PublishOnly, affiliations.ToArray()[4].AffiliationType);

            Assert.Equal("node1", affiliations.ToArray()[0].Node);
            Assert.Equal("node2", affiliations.ToArray()[1].Node);
            Assert.Equal("node5", affiliations.ToArray()[2].Node);
            Assert.Equal("node6", affiliations.ToArray()[3].Node);
            Assert.NotEqual("node7", affiliations.ToArray()[3].Node);
        }

        [Fact]
        public void TestBuildAffiliations()
        {
            var affs = new Affiliations { Node = "princely_musings" };

            affs.AddAffiliation(new Affiliation { Node = "node1", AffiliationType = AffiliationType.Owner });
            affs.AddAffiliation(new Affiliation { Node = "node2", AffiliationType = AffiliationType.Publisher });
            affs.AddAffiliation(new Affiliation { Node = "node5", AffiliationType = AffiliationType.Outcast });
            affs.AddAffiliation(new Affiliation { Node = "node6", AffiliationType = AffiliationType.Owner });
            affs.AddAffiliation(new Affiliation { Node = "node7", AffiliationType = AffiliationType.PublishOnly });

            affs.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.affiliations4.xml"));
        }

        [Fact]
        public void TestBuildOwnerAffiliations()
        {
            var affs = new Matrix.Xmpp.PubSub.Owner.Affiliations { Node = "princely_musings" };

            affs.AddAffiliation(new Matrix.Xmpp.PubSub.Owner.Affiliation { Jid = "hamlet@denmark.lit", AffiliationType = AffiliationType.Owner });
            affs.AddAffiliation(new Matrix.Xmpp.PubSub.Owner.Affiliation { Jid = "polonius@denmark.lit", AffiliationType = AffiliationType.Outcast });

            affs.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.affiliations2.xml"));
        }

        [Fact]
        public void TestOwnerAffiliations()
        {
            var affs = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.affiliations2.xml")).Cast<Matrix.Xmpp.PubSub.Owner.Affiliations>();
         
            IEnumerable<Matrix.Xmpp.PubSub.Owner.Affiliation> affiliations = affs.GetAffiliations();
            Assert.Equal("princely_musings", affs.Node);

            Assert.Equal(2, affiliations.Count());
            Assert.Equal(AffiliationType.Owner, affiliations.ToArray()[0].AffiliationType);
            Assert.Equal(AffiliationType.Outcast, affiliations.ToArray()[1].AffiliationType);
            
            Assert.True(affiliations.ToArray()[0].Jid.Equals("hamlet@denmark.lit"));
            Assert.True(affiliations.ToArray()[1].Jid.Equals("polonius@denmark.lit"));
                
            Assert.NotEqual("12345", affiliations.ToArray()[0].Jid.ToString());
        }
        
        [Fact]
        public void TestaffiliationsIq()
        {
            var iq = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.affiliations_iq1.xml")).Cast<Iq>();
            var pubsub = iq.Element<Matrix.Xmpp.PubSub.PubSub>();
            var affs = pubsub.Element<Affiliations>();

            affs.GetAffiliations().Count().ShouldBe(4);
        }
    }
}
