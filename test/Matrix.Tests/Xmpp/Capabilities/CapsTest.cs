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
using Matrix.Xmpp.Capabilities;
using Matrix.Xmpp.Disco;
using Matrix.Xmpp.Stream;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Capabilities
{
    public class CapsTest
    {
        [Fact]
        public void TestBuildHash()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Capabilities.discoinfo1.xml")).Cast<Info>();
            var hash = Caps.BuildHash(info);
            hash.ShouldBe("q07IKJEyjvHSyhy//CH0CxmKi8w=");
        }

        [Fact]
        public void TestBuildHash2()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Capabilities.discoinfo3.xml")).Cast<Info>();
            string hash = Caps.BuildHash(info);
            hash.ShouldBe("XH3meI11JZj00/DhOlop7p7jKBE=");
        }

        [Fact]
        public void TestBuildHash5()
        {
            var info = XmppXElement.LoadXml(Resource.Get("Xmpp.Capabilities.discoinfo4.xml")).Cast<Info>();
            string hash = Caps.BuildHash(info);
            hash.ShouldBe("8ungGR8ouA8Bi9LIUp8r3+1tgbY=");
        }

        [Fact]
        public void TestCapsInStreamFeatures()
        {
            var features = XmppXElement.LoadXml(Resource.Get("Xmpp.Capabilities.streamfeatures.xml")).Cast<StreamFeatures>();
            var caps = features.Caps;
            caps.Node.ShouldBe("http://jabberd.org");
            caps.Version.ShouldBe("ItBTI0XLDFvVxZ72NQElAzKS9sU=");
            caps.Hash.ShouldBe("sha-1");
        }
    }
}
