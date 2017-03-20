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
using Matrix.Xmpp.Sasl;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Sasl
{
    public class FailureTest
    {
        [Fact]
        public void ShouldBeOfTypeChallenge()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.failure1.xml")).ShouldBeOfType<Failure>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.failure2.xml")).ShouldBeOfType<Failure>();
        }

        [Fact]
        public void TestFailure1()
        {
            var fail = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.failure1.xml")).Cast<Failure>();
            Assert.Equal(fail.Condition == FailureCondition.InvalidAuthzId, true);
        }

        [Fact]
        public void TestFailure2()
        {
            var fail = XmppXElement.LoadXml(Resource.Get("Xmpp.Sasl.failure2.xml")).Cast<Failure>();
            Assert.Equal(fail.Condition == FailureCondition.UnknownCondition, true);
        }

        [Fact]
        public void TestBuildFailure()
        {
            new Failure
            {
                Condition = FailureCondition.InvalidAuthzId
            }.ShouldBe(Resource.Get("Xmpp.Sasl.failure1.xml"));
        }
    }
}
