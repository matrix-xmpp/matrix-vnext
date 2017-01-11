using System;
using Matrix.Xml;
using Matrix.Xmpp.AdHocCommands;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.AdHocCommands
{
    public class ActionsTests
    {
        private const string XML1
          = @"<actions execute='complete'
                xmlns='http://jabber.org/protocol/commands'>
              <prev/>
              <complete/>
            </actions>";

        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);
            xmpp1.ShouldBeOfType<Actions>();

            var actions = xmpp1 as Actions;
            if (actions != null)
            {
                actions.Execute.ShouldBe(Matrix.Xmpp.AdHocCommands.Action.Complete);
                actions.Execute.ShouldNotBe(Matrix.Xmpp.AdHocCommands.Action.Execute);
                actions.Execute.ShouldNotBe(Matrix.Xmpp.AdHocCommands.Action.None);

                actions.Previous.ShouldBeTrue();
                actions.Complete.ShouldBeTrue();
                actions.Next.ShouldBeFalse();

                actions.Action.ShouldBe(Matrix.Xmpp.AdHocCommands.Action.Complete | Matrix.Xmpp.AdHocCommands.Action.Prev);
            }
        }

        [Fact]
        public void TestException()
        {
            Should.Throw<NotSupportedException>(() =>
                new Actions {Execute = Matrix.Xmpp.AdHocCommands.Action.Execute});
        }

        [Fact]
        public void TestException2()
        {
            Should.Throw<NotSupportedException>(
                () =>
                    new Actions {Execute = Matrix.Xmpp.AdHocCommands.Action.Cancel});
        }

        [Fact]
        public void TestException3()
        {
            Should.Throw<NotSupportedException>(() =>
                new Actions
                {
                    Action = Matrix.Xmpp.AdHocCommands.Action.Complete | Matrix.Xmpp.AdHocCommands.Action.Execute
                });
        }


    }
}