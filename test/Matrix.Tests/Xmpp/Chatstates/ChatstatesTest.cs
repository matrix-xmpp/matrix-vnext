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
using Matrix.Xmpp.Client;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.Chatstates
{
    public class ChatstatesTest
    {
        [Fact]
        public void TestMessageStates()
        {
            var expectedXml = XmppXElement.LoadXml(Resource.Get("Xmpp.Chatstates.message2.xml")).Cast<Message>();
            var msg = XmppXElement.LoadXml(Resource.Get("Xmpp.Chatstates.message1.xml")).Cast<Message>();

            msg.Chatstate.ShouldBe(Matrix.Xmpp.Chatstates.Chatstate.Active);
            msg.Chatstate.ShouldNotBe(Matrix.Xmpp.Chatstates.Chatstate.Composing);
            msg.Chatstate.ShouldNotBe(Matrix.Xmpp.Chatstates.Chatstate.Gone);

            msg.Chatstate = Matrix.Xmpp.Chatstates.Chatstate.None;
            msg.ShouldBe(expectedXml);
        }

        [Fact]
        public void TestMessageStates2()
        {
            var expectedXml = XmppXElement.LoadXml(Resource.Get("Xmpp.Chatstates.message1.xml")).Cast<Message>();
            var msg = XmppXElement.LoadXml(Resource.Get("Xmpp.Chatstates.message2.xml")).Cast<Message>();
            msg.Chatstate.ShouldBe(Matrix.Xmpp.Chatstates.Chatstate.None);
            msg.Chatstate = Matrix.Xmpp.Chatstates.Chatstate.Active;
            msg.ShouldBe(expectedXml);
        }

        [Fact]
        public void BuildChatStateMessage()
        {
            var expectedXml = XmppXElement.LoadXml(Resource.Get("Xmpp.Chatstates.message3.xml")).Cast<Message>();
            new Message
                {
                    Type = Matrix.Xmpp.MessageType.Chat,
                    Chatstate = Matrix.Xmpp.Chatstates.Chatstate.Gone
                }
                .ShouldBe(expectedXml);
        }
    }
}
