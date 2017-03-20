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

using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Disco;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Disco
{
    
    public class DiscoTest
    {
        [Fact]
        public void TestDiscoQuery()
        {
            var iq = XmppXElement.LoadXml(Resource.Get("Xmpp.Disco.iq1.xml")).Cast<Iq>();

            iq.Query.ShouldBeOfType<Items>();
            Items items = iq.Query.Cast<Items>();
            items.GetItems().Count().ShouldBe(3);
        }

        [Fact]
        public void BuildDiscoItems()
        {
            var expectedXml = Resource.Get("Xmpp.Disco.discoitems1.xml");

            Items items = new Items {Node = "http://jabber.org/protocol/tune"};

            items.AddItem(new Item("user1@server.com", "node1", "name1"));
            items.AddItem(new Item("user2@server.com", "node2", "name2"));
            items.AddItem(new Item("user3@server.com", "node3", "name3"));

            items.ShouldBe(expectedXml);
        }

        [Fact]
        public void BuildDiscoIqWithFeatures()
        {
            var expectedXml = Resource.Get("Xmpp.Disco.iq2.xml");
            var dIq = new DiscoInfoIq
                          {
                              Type = IqType.Result,
                              To = "user@server.com/resource",
                              Id = "id_from_request"
                          };
            dIq.Info.AddFeature(new Feature("urn:xmpp:jingle:1"));
            dIq.Info.AddFeature(new Feature("urn:xmpp:jingle:apps:rtp:audio"));
            dIq.Info.AddFeature(new Feature("urn:xmpp:jingle:apps:rtp:video"));

            dIq.ShouldBe(expectedXml);
        }
    }
}
