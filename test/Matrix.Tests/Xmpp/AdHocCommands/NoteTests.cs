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
using Matrix.Xmpp.AdHocCommands;
using Xunit;
using Shouldly;

namespace Matrix.Tests.Xmpp.AdHocCommands
{
    
    public class NoteTests
    {
        private const string VALUE = "Service 'httpd' has been configured.";
        private const string XML1 = @"<note xmlns='http://jabber.org/protocol/commands' type='info'>" + VALUE + "</note>";
        
        [Fact]
        public void Test1()
        {
            XmppXElement xmpp1 = XmppXElement.LoadXml(XML1);

            xmpp1.ShouldBeOfType<Note>();

            var note = xmpp1 as Note;
            if (note != null)
            {
                note.Value.ShouldBe(VALUE);
                note.Value.ShouldNotBe("dummy");
                note.Type.ShouldBe(NoteType.Info);
                note.Type.ShouldNotBe(NoteType.Error);
                note.Type.ShouldNotBe(NoteType.Warn);
            }
        }

        [Fact]
        public void Test2()
        {
            var note = new Note()
            {
                Type = NoteType.Info,
                Value = VALUE
            };

            note.ShouldBe(XML1);
        }
    }
}
