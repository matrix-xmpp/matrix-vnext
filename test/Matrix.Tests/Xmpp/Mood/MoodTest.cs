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
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Mood
{
    public class MoodTest
    {
        [Fact]
        public void ShouldBeOfTypeMood()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Mood.mood1.xml")).ShouldBeOfType<Matrix.Xmpp.Mood.Mood>();
        }

        [Fact]
        public void TestBuildMood()
        {
            var mood = new Matrix.Xmpp.Mood.Mood
            {
                UserMood = Matrix.Xmpp.Mood.Moods.Annoyed,
                MoodText = "curse my nurse!"
            };

            mood.ShouldBe(Resource.Get("Xmpp.Mood.mood1.xml"));
        }

        [Fact]
        public void TestBuildMood2()
        {
            var mood = new Matrix.Xmpp.Mood.Mood
            {
                UserMood = Matrix.Xmpp.Mood.Moods.InAwe,
            };

            Assert.Equal(mood.UserMood == Matrix.Xmpp.Mood.Moods.InAwe, true);
            Assert.Equal(mood.UserMood == Matrix.Xmpp.Mood.Moods.Hungry, false);
            Assert.Equal(mood.UserMood == Matrix.Xmpp.Mood.Moods.InLove, false);

            mood.ShouldBe(Resource.Get("Xmpp.Mood.mood2.xml"));
        }

        [Fact]
        public void TestMoodToPubsub()
        {
            var mood = new Matrix.Xmpp.Mood.Mood
            {
                UserMood = Matrix.Xmpp.Mood.Moods.Annoyed,
                MoodText = "curse my nurse!"
            };
            mood.ToPubSub().ShouldBe(Resource.Get("Xmpp.Mood.pubsub1.xml"));
        }
    }
}
