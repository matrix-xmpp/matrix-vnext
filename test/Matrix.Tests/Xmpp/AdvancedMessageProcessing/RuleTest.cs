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

using System;
using Matrix.Xml;
using Matrix.Xmpp.AdvancedMessageProcessing;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.AdvancedMessageProcessing
{
    public class RuleTest
    {
        [Fact]
        public void XmlShouldBeOfTypeRule()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.rule1.xml"))
                .ShouldBeOfType<Rule>();
        }

        [Fact]
        public void TestAction()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.rule1.xml"))
                .Cast<Rule>()
                .Action.ShouldBe(Matrix.Xmpp.AdvancedMessageProcessing.Action.Error);
        }
        

        [Fact]
        public void TestCondition()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.rule1.xml"))
                .Cast<Rule>()
                .Condition.ShouldBe(Condition.ExpireAt);
        }

        [Fact]
        public void TestCondition2()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.rule2.xml"))
                .Cast<Rule>()
                .Condition.ShouldBe(Condition.MatchResource);
        }


        [Fact]
        public void TestValueAsString()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.rule1.xml"))
                .Cast<Rule>()
                .ValueAsString.ShouldBe("foo");
        }

        [Fact]
        public void TestValueAsString2()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.rule2.xml"))
                .Cast<Rule>()
                .ValueAsString.ShouldBe("other");
        }

        [Fact]
        public void TestExpireAt()
        {
            var rule = XmppXElement.LoadXml(Resource.Get("Xmpp.AdvancedMessageProcessing.rule3.xml")).Cast<Rule>();
            Assert.Equal(rule.ValueAsDateTime.ToUniversalTime().Equals(new DateTime(2004, 1, 1, 0, 0, 0)), true);
        }

        [Fact]
        public void BuildRule()
        {
            var expectedXml = Resource.Get("Xmpp.AdvancedMessageProcessing.rule1.xml");
            var rule4 = new Rule
            {
                Action =
                      Matrix.Xmpp.AdvancedMessageProcessing.Action.Error,
                Condition = Condition.ExpireAt,
                ValueAsString = "foo"
            };

            rule4.ShouldBe(expectedXml);
        }

        [Fact]
        public void BuildRule2()
        {
            var expectedXml = Resource.Get("Xmpp.AdvancedMessageProcessing.rule2.xml");
            new Rule
            {
                Action = Matrix.Xmpp.AdvancedMessageProcessing.Action.Error,
                Condition = Condition.MatchResource,
                ValueAsString = "other"
            }
            .ShouldBe(expectedXml);
        }
    }
}
