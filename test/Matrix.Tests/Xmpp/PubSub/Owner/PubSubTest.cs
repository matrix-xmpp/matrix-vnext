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

using Matrix.Xml;
using Matrix.Xmpp.PubSub.Owner;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub.Owner
{
    public class PubSubTest
    {
        [Fact]
        public void TestBuildPubsubSubscriptions()
        {
            var ps = new Matrix.Xmpp.PubSub.Owner.PubSub { Type = PubSubOwnerType.Subscriptions, Subscriptions = { Node = "princely_musings" } };
            ps.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.pubsub1.xml"));

            var ps2 = new Matrix.Xmpp.PubSub.Owner.PubSub
            {
                Type = PubSubOwnerType.Delete,
                Delete = { Node = "princely_musings" }
            };
            ps2.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.pubsub2.xml"));


            var ps3 = new Matrix.Xmpp.PubSub.Owner.PubSub
            {
                Type = PubSubOwnerType.Purge,
                Purge = { Node = "princely_musings" }
            };

            ps3.ShouldBe(Resource.Get("Xmpp.PubSub.Owner.pubsub3.xml"));
        }

        [Fact]
        public void TestPubsubSubscriptions()
        {
            var ps = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.pubsub1.xml")).Cast<Matrix.Xmpp.PubSub.Owner.PubSub>(); 
            Assert.Equal(ps.Type, PubSubOwnerType.Subscriptions);
            Assert.NotEqual(ps.Type, PubSubOwnerType.Purge);

            ps = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.pubsub2.xml")).Cast<Matrix.Xmpp.PubSub.Owner.PubSub>();
            Assert.Equal(ps.Type, PubSubOwnerType.Delete);
            Assert.Equal(ps.Delete.Node, "princely_musings");

            ps = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.Owner.pubsub3.xml")).Cast<Matrix.Xmpp.PubSub.Owner.PubSub>();
            Assert.Equal(ps.Type, PubSubOwnerType.Purge);
            Assert.Equal(ps.Purge.Node, "princely_musings");
        }
    }
}
