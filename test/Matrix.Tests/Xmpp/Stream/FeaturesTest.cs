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
using Matrix.Xmpp.Compression;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Stream
{
    public class FeaturesTest
    {
        [Fact]
        public void TestShouldbeOfTypeStreamFeatures()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.stream_features1.xml")).ShouldBeOfType<Matrix.Xmpp.Stream.StreamFeatures>();
        }

        [Fact]
        public void TestStreamFeatures()
        {
            var feats = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.stream_features1.xml")).Cast<Matrix.Xmpp.Stream.StreamFeatures>();

            Assert.Equal(feats.SupportsCompression, true);
            Assert.Equal(feats.SupportsAuth, true);
            Assert.Equal(feats.Compression.Supports(Methods.Zlib), true);
        }

        [Fact]
        public void TestCompression()
        {
            var comp = XmppXElement.LoadXml(Resource.Get("Xmpp.Stream.compression2.xml")).Cast<Matrix.Xmpp.Stream.Features.Compression>();

            Assert.Equal(comp.Supports(Methods.Zlib), false);

            var method = comp.Element<Method>();
            Assert.Equal(method.CompressionMethod == Methods.Unknown, true);
        }
    }
}
