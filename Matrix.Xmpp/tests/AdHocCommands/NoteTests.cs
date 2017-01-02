using Matrix.Xml;
using Matrix.Xmpp.AdHocCommands;

using Matrix.Xmpp.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Matrix.Xmpp.Tests.AdHocCommands
{
    [TestClass]
    public class NoteTests
    {
        private const string VALUE = "Service 'httpd' has been configured.";
        private const string XML1 = @"<note xmlns='http://jabber.org/protocol/commands' type='info'>" + VALUE + "</note>";
        
        [TestMethod]
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

        [TestMethod]
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
