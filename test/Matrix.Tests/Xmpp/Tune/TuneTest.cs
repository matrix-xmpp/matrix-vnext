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
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Tune
{
    public class TuneTest
    {
        [Fact]
        public void XmlShouldBeOfTypeTune()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .ShouldBeOfType<Matrix.Xmpp.Tune.Tune>();
        }

        [Fact]
        public void TestArtist()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Artist.ShouldBe("Yes");
        }

        [Fact]
        public void TestLength()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Length.ShouldBe(686);
        }

        [Fact]
        public void TestRating()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Rating.ShouldBe(8);
        }

        [Fact]
        public void TestSource()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Source.ShouldBe("Yessongs");
        }

        [Fact]
        public void TestTrack()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Track.ShouldBe("3");
        }

        [Fact]
        public void TestUri()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Tune.tune1.xml"))
                .Cast<Matrix.Xmpp.Tune.Tune>()
                .Uri.ToString().ShouldBe("http://www.yesworld.com/lyrics/Fragile.html#9");
        }

        [Fact]
        public void BuildTune()
        {
            var expectedXml = Resource.Get("Xmpp.Tune.tune1.xml");
            new Matrix.Xmpp.Tune.Tune
            {
                Artist = "Yes",
                Length = 686,
                Rating = 8,
                Source = "Yessongs",
                Title = "Heart of the Sunrise",
                Track = "3",
                Uri = new System.Uri("http://www.yesworld.com/lyrics/Fragile.html#9")
            }
            .ShouldBe(expectedXml);
        }
    }
}
