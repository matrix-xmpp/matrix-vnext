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
            Assert.Equal(affs.Node, "princely_musings");

            Assert.Equal(affiliations.Count(), 2);
            Assert.Equal(affiliations.ToArray()[0].AffiliationType, AffiliationType.Owner);
            Assert.Equal(affiliations.ToArray()[1].AffiliationType, AffiliationType.Outcast);

            Assert.Equal(affiliations.ToArray()[0].Jid.Equals("hamlet@denmark.lit"), true);
            Assert.Equal(affiliations.ToArray()[1].Jid.Equals("polonius@denmark.lit"), true);
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
