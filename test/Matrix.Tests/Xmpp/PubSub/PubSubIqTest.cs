using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using NUnit.Framework;
using Xunit;

namespace Matrix.Tests.Xmpp.PubSub
{
    
    public class PubSubIqTest
    {
        private const string XML1
            = @"<iq xmlns='jabber:client' type='result'
            from='pubsub.shakespeare.lit'
            to='hamlet@denmark.lit/elsinore'
            id='create2'>
            <pubsub xmlns='http://jabber.org/protocol/pubsub'>
              <create node='25e3d37dabbab9541f7523321421edc5bfeb2dae'/>
            </pubsub>
        </iq>";

        private const string XML2
            = @"<iq xmlns='jabber:client' type='set'
            from='hamlet@denmark.lit/elsinore'
            to='pubsub.shakespeare.lit'
            id='create2'>
            <pubsub xmlns='http://jabber.org/protocol/pubsub'>
              <create/>
              <configure/>
            </pubsub>
        </iq>";
        
        [Fact]
        public void Test1()
        {
            PubSubIq pIq = new PubSubIq
                               {
                                   Type = Matrix.Xmpp.IqType.Result,
                                   From = "pubsub.shakespeare.lit",
                                   To = "hamlet@denmark.lit/elsinore",
                                   Id = "create2"

                               };

            pIq.PubSub.Create = new Matrix.Xmpp.PubSub.Create { Node = "25e3d37dabbab9541f7523321421edc5bfeb2dae" };

            pIq.ShouldBe(XML1);
        }

        [Fact]
        public void Test2()
        {
            var pIq = new PubSubIq
            {
                Type = Matrix.Xmpp.IqType.Set,
                From = "hamlet@denmark.lit/elsinore",
                To = "pubsub.shakespeare.lit",
                Id = "create2"

            };

            pIq.PubSub.Create       = new Matrix.Xmpp.PubSub.Create();
            pIq.PubSub.Configure    = new Matrix.Xmpp.PubSub.Configure();
            
            pIq.ShouldBe(XML2);
        }
    }
}
