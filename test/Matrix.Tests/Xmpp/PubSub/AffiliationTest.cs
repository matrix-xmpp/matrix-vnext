using System.Collections.Generic;
using System.Linq;
using Xunit;

using Shouldly;
using Matrix;
using Matrix.Xml;
using Matrix.Xmpp.PubSub;
using Matrix.Xmpp.Client;

namespace Matrix.Tests.Xmpp.PubSub
{
    
    public class AffiliationTest
    {
        private const string XML1
            = @"<affiliations xmlns='http://jabber.org/protocol/pubsub' node='princely_musings'>
                            <affiliation node='node1' affiliation='owner'/>
                            <affiliation node='node2' affiliation='publisher'/>
                            <affiliation node='node5' affiliation='outcast'/>
                            <affiliation node='node6' affiliation='owner'/>
                            <affiliation node='node7' affiliation='publish-only'/>
                </affiliations>";

        private const string XML2
            = @"<affiliations xmlns='http://jabber.org/protocol/pubsub#owner' node='princely_musings'>
                    <affiliation jid='hamlet@denmark.lit' affiliation='owner'/>
                    <affiliation jid='polonius@denmark.lit' affiliation='outcast'/>
                </affiliations>";

        private const string XML3
            = @"<iq type='result'
    from='pubsub.shakespeare.lit'
    to='francisco@denmark.lit'
    id='affil1' xmlns='jabber:client'>
  <pubsub xmlns='http://jabber.org/protocol/pubsub'>
    <affiliations>
      <affiliation node='node1' affiliation='owner'/>
      <affiliation node='node2' affiliation='publisher'/>
      <affiliation node='node5' affiliation='outcast'/>
      <affiliation node='node6' affiliation='owner'/>
    </affiliations>
  </pubsub>
</iq>";

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

                Assert.Equal(affiliations.Count(), 5);
                Assert.Equal(affiliations.ToArray()[0].AffiliationType, AffiliationType.Owner);
                Assert.Equal(affiliations.ToArray()[1].AffiliationType, AffiliationType.Publisher);
                Assert.Equal(affiliations.ToArray()[2].AffiliationType, AffiliationType.Outcast);
                Assert.Equal(affiliations.ToArray()[3].AffiliationType, AffiliationType.Owner);
                Assert.Equal(affiliations.ToArray()[4].AffiliationType, AffiliationType.PublishOnly);

                Assert.Equal(affiliations.ToArray()[0].Node, "node1");
                Assert.Equal(affiliations.ToArray()[1].Node, "node2");
                Assert.Equal(affiliations.ToArray()[2].Node, "node5");
                Assert.Equal(affiliations.ToArray()[3].Node, "node6");
                Assert.NotEqual(affiliations.ToArray()[3].Node, "node7");
            }
        }

        [Fact]
        public void Test2()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML2);
            Assert.Equal(true, xmpp1 is  Matrix.Xmpp.PubSub.Owner.Affiliations);

            var affs = xmpp1 as Matrix.Xmpp.PubSub.Owner.Affiliations;
            if (affs != null)
            {
                IEnumerable<Matrix.Xmpp.PubSub.Owner.Affiliation> affiliations = affs.GetAffiliations();
                Assert.Equal(affs.Node, "princely_musings");

                Assert.Equal(affiliations.Count(), 2);
                Assert.Equal(affiliations.ToArray()[0].AffiliationType, AffiliationType.Owner);
                Assert.Equal(affiliations.ToArray()[1].AffiliationType, AffiliationType.Outcast);
            
                Assert.Equal(affiliations.ToArray()[0].Jid.Equals("hamlet@denmark.lit"), true);
                Assert.Equal(affiliations.ToArray()[1].Jid.Equals("polonius@denmark.lit"), true);
                
                Assert.NotEqual(affiliations.ToArray()[0].Jid.ToString(), "12345");
            }
        }

        [Fact]
        public void Test3()
        {
            var affs = new Affiliations { Node = "princely_musings" };

            affs.AddAffiliation(new Affiliation { Node = "node1", AffiliationType = AffiliationType.Owner });
            affs.AddAffiliation(new Affiliation { Node = "node2", AffiliationType = AffiliationType.Publisher });
            affs.AddAffiliation(new Affiliation { Node = "node5", AffiliationType = AffiliationType.Outcast });
            affs.AddAffiliation(new Affiliation { Node = "node6", AffiliationType = AffiliationType.Owner });
            affs.AddAffiliation(new Affiliation { Node = "node7", AffiliationType = AffiliationType.PublishOnly });

            affs.ShouldBe(XML1);
        }

        [Fact]
        public void Test4()
        {
            var affs = new Matrix.Xmpp.PubSub.Owner.Affiliations { Node = "princely_musings" };

            affs.AddAffiliation(new Matrix.Xmpp.PubSub.Owner.Affiliation { Jid = "hamlet@denmark.lit",   AffiliationType = AffiliationType.Owner });
            affs.AddAffiliation(new Matrix.Xmpp.PubSub.Owner.Affiliation { Jid = "polonius@denmark.lit", AffiliationType = AffiliationType.Outcast });

            affs.ShouldBe(XML2);
        }

        [Fact]
        public void Test5()
        {
            var iq = XmppXElement.LoadXml(XML3) as Iq;

            var pubsub = iq.Element<Matrix.Xmpp.PubSub.PubSub>();
            var affs = pubsub.Element<Affiliations>();

            foreach (var affiliation in affs.GetAffiliations())
            {
                System.Diagnostics.Debug.WriteLine(affiliation.Node);
            }
        }
    }
}