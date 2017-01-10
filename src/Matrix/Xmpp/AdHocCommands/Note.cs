using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.AdHocCommands
{
    [XmppTag(Name = "note", Namespace = Namespaces.AdHocCommands)]
    public class Note : XmppXElement
    {
        public Note() : base(Namespaces.AdHocCommands, "note")
        {
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public NoteType Type
        {
            get { return GetAttributeEnum<NoteType>("type"); }
            set { SetAttribute("type", value.ToString().ToLower()); }
        }
    }
}