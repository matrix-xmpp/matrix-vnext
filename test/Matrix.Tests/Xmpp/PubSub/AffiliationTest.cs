/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

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
            Assert.Equal(affs.Node, "princely_musings");

            Assert.Equal(affiliations.Count(), 2);
            Assert.Equal(affiliations.ToArray()[0].AffiliationType, AffiliationType.Owner);
            Assert.Equal(affiliations.ToArray()[1].AffiliationType, AffiliationType.Outcast);
            
            Assert.Equal(affiliations.ToArray()[0].Jid.Equals("hamlet@denmark.lit"), true);
            Assert.Equal(affiliations.ToArray()[1].Jid.Equals("polonius@denmark.lit"), true);
                
            Assert.NotEqual(affiliations.ToArray()[0].Jid.ToString(), "12345");
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
