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

using System.Collections.Generic;
using System.Linq;
using Matrix.Xml;
using Matrix.Xmpp.Bytestreams;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Bytestreams
{
    public class BytestreamTest
    {
        [Fact]
        public void XmppXElementShouldbeOfTypeBytestream()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bytestreams.query1.xml")).ShouldBeOfType<Bytestream>();
        }

        [Fact]
        public void XmppXElementShouldbeOfTypeStreamhostUsed()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bytestreams.streamhost-used.xml")).ShouldBeOfType<StreamhostUsed>();
        }


        [Fact]
        public void XmppXElementShouldbeOfTypeActivate()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Bytestreams.activate1.xml")).ShouldBeOfType<Activate>();
        }

        [Fact]
        public void TestStreamhostProperties()
        {
            var bs = XmppXElement.LoadXml(Resource.Get("Xmpp.Bytestreams.query1.xml")).Cast<Bytestream>();
            bs.Sid.ShouldBe("vxf9n471bn46");
            bs.Mode.ShouldBe(Mode.Tcp);

            IEnumerable<Streamhost> hosts = bs.GetStreamhosts();
            hosts.Count().ShouldBe(2);

            var host1 = bs.GetStreamhosts().First(h => h.Port == 5086);
            host1.Host.ShouldBe("192.168.4.1");
            host1.Jid.Equals("initiator@example.com/foo").ShouldBe(true);

            var host2 = bs.GetStreamhosts().First(h => h.Jid.Equals("streamhostproxy.example.net"));
            host2.Host.ShouldBe("24.24.24.1");
            host2.Zeroconf.ShouldBe("_jabber.bytestreams");
        }

        [Fact]
        public void BuildStreamhost()
        {
            var expectedXml1 = Resource.Get("Xmpp.Bytestreams.streamhost1.xml");
            new Streamhost { Jid = "initiator@example.com/foo", Port = 5086, Host = "192.168.4.1" }
                .ShouldBe(expectedXml1);

            var expectedXml2 = Resource.Get("Xmpp.Bytestreams.streamhost2.xml");
            new Streamhost { Jid = "streamhostproxy.example.net", Host = "24.24.24.1", Zeroconf = "_jabber.bytestreams" }
                .ShouldBe(expectedXml2);
        }
        
        [Fact]
        public void BuildStreamhostUsed()
        {
            var expectedXml = Resource.Get("Xmpp.Bytestreams.streamhost-used.xml");
            new StreamhostUsed {Jid = "streamhostproxy.example.net"}
                .ShouldBe(expectedXml);
        }

        [Fact]
        public void TestStreamhostUsedProperties()
        {
            var su = XmppXElement.LoadXml(Resource.Get("Xmpp.Bytestreams.streamhost-used.xml")).ShouldBeOfType<StreamhostUsed>();
            su.Jid.Equals("streamhostproxy.example.net").ShouldBe(true);
        }

        [Fact]
        public void TestActivateProperties()
        {
            var expectedXml = Resource.Get("Xmpp.Bytestreams.activate2.xml");
            var activate = XmppXElement.LoadXml(Resource.Get("Xmpp.Bytestreams.activate1.xml")).Cast<Activate>();
            activate.Jid.Equals("target@example.org/bar").ShouldBe(true);
        }

        [Fact]
        public void BuildActivate()
        {
            var expectedXml = Resource.Get("Xmpp.Bytestreams.activate2.xml");
            var activate = XmppXElement.LoadXml(Resource.Get("Xmpp.Bytestreams.activate1.xml")).Cast<Activate>();
            activate.Jid = null;
            activate.ShouldBe(expectedXml);
        }
    }
}
