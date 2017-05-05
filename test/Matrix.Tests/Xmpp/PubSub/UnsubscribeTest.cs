/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
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
using Matrix.Xmpp.PubSub;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    public class UnSubscribeTest
    {
        [Fact]
        public void ShoudBeOfTypeUnsubscribe()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.unsubscribe1.xml")).ShouldBeOfType<Unsubscribe>();
        }

        [Fact]
        public void TestUnsubscribe()
        {
            var unsub = XmppXElement.LoadXml(Resource.Get("Xmpp.PubSub.unsubscribe1.xml")).Cast<Unsubscribe>();
            Assert.Equal(unsub.Node, "princely_musings");
            Assert.Equal(unsub.Jid.ToString(), "francisco@denmark.lit");
            Assert.Equal(unsub.SubId, "abcd");
        }

        [Fact]
        public void TestBuildUnsubscribe()
        {
            var unsub = new Unsubscribe { Node = "princely_musings", Jid = "francisco@denmark.lit", SubId = "abcd" };
            unsub.ShouldBe(Resource.Get("Xmpp.PubSub.unsubscribe1.xml"));
        }
    }
}
