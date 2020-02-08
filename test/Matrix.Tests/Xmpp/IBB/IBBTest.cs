/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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
using Matrix.Xmpp.IBB;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.IBB
{
    
    public class IBBTest
    {
        [Fact]
        public void ElementShouldBeOfTypeOpen()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.open1.xml")).ShouldBeOfType<Open>();
        }

        [Fact]
        public void ElementShouldBeOfTypeClose()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.close1.xml")).ShouldBeOfType<Close>();
        }

        [Fact]
        public void ElementShouldBeOfTypeData()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.data1.xml")).ShouldBeOfType<Data>();
        }

        [Fact]
        public void TestOpenProperties()
        {
            var open = XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.open1.xml")).Cast<Open>();
            Assert.Equal(open.BlockSize, 4096);
            Assert.Equal(open.Sid, "i781hf64");
            Assert.Equal(open.Stanza, StanzaType.Iq);

            open = XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.open2.xml")).Cast<Open>();
            Assert.Equal(open.BlockSize, 4096);
            Assert.Equal(open.Sid, "i781hf64");
            Assert.Equal(open.Stanza, StanzaType.Iq);
        }
        
        [Fact]
        public void TestBuildOpen()
        {
            var open = new Open {BlockSize = 4096, Sid = "i781hf64", Stanza = StanzaType.Iq};
            open.ShouldBe(Resource.Get("Xmpp.IBB.open1.xml"));
            
            var open2 = new Open { BlockSize = 4096, Sid = "i781hf64" };
            open2.ShouldBe(Resource.Get("Xmpp.IBB.open2.xml"));
        }

        [Fact]
        public void TestCloseId()
        {
            var close = XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.close1.xml")).Cast<Close>();
            Assert.Equal(close.Sid, "i781hf64");
        }

        [Fact]
        public void TestBuildClose()
        {
            var open = new Close {Sid = "i781hf64"};
            open.ShouldBe(Resource.Get("Xmpp.IBB.close1.xml"));
        }

        [Fact]
        public void TestData()
        {
            var data = XmppXElement.LoadXml(Resource.Get("Xmpp.IBB.data1.xml")).Cast<Data>();
            Assert.Equal(data.Sid, "i781hf64");
            Assert.Equal(data.Sequence, 99);
        }

        [Fact]
        public void TestBuildData()
        {
            var data = new Data { Sid = "i781hf64", Sequence = 99 };
            data.ShouldBe(Resource.Get("Xmpp.IBB.data1.xml"));
        }
    }
}
