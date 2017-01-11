using System;
using Matrix.Xml;
using Matrix.Xmpp.AdvancedMessageProcessing;
using NUnit.Framework;
using Xunit;
using Shouldly;


namespace Matrix.Tests.Xmpp.AdvancedMessageProcessing
{
    
    public class RuleTest
    {
        public void Test1()
        {
            const string XML1 =
                "<rule xmlns='http://jabber.org/protocol/amp' action='error' condition='expire-at' value='foo'/>";
            
            const string XML2 =
                "<rule xmlns='http://jabber.org/protocol/amp' action='error' condition='match-resource' value='other'/>";

            const string XML3 = 
                " <rule xmlns='http://jabber.org/protocol/amp' condition='expire-at' action='drop' value='2004-01-01T00:00:00Z'/>";

            var rule1 = XmppXElement.LoadXml(XML1) as Rule;
            Assert.Equal(rule1.Action == Matrix.Xmpp.AdvancedMessageProcessing.Action.Error, true);
            Assert.Equal(rule1.Condition == Condition.ExpireAt, true);
            Assert.Equal(rule1.ValueAsString, "foo");

            var rule2 = XmppXElement.LoadXml(XML2) as Rule;
            Assert.Equal(rule2.Action == Matrix.Xmpp.AdvancedMessageProcessing.Action.Error, true);
            Assert.Equal(rule2.Condition == Condition.MatchResource, true);
            Assert.Equal(rule2.ValueAsString, "other");

            var rule3 = XmppXElement.LoadXml(XML3) as Rule;
            Assert.Equal(rule3.ValueAsDateTime.ToUniversalTime().Equals(new DateTime(2004, 1, 1, 0, 0, 0)), true);
            
            var rule4 = new Rule
                {
                    Action =
                        Matrix.Xmpp.AdvancedMessageProcessing.Action.Error,
                    Condition = Condition.ExpireAt,
                    ValueAsString = "foo"
                };
            
            rule4.ShouldBe(XML1);

            var rule5 = new Rule
                {
                    Action =
                        Matrix.Xmpp.AdvancedMessageProcessing.Action.Error,
                    Condition = Condition.MatchResource,
                    ValueAsString = "other"
                };
            
            rule5.ShouldBe(XML2);
        }
    }
}
