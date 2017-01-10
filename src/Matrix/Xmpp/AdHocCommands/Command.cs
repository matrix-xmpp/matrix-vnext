using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.AdHocCommands
{
    [XmppTag(Name = "command", Namespace = Namespaces.AdHocCommands)]
    public class Command : XmppXElement
    {
        public Command()
            : base(Namespaces.AdHocCommands, "command")
        {
        }
        
        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }

        /// <summary>
        /// Gets or sets the session id.
        /// </summary>
        /// <value>The session id.</value>
        public string SessionId
        {
            get { return GetAttribute("sessionid"); }
            set { SetAttribute("sessionid", value); }
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        public Action Action
        {
            get
            {
                return GetAttributeEnum<Action>("action");
            }
            set
            {
                if (value == Action.None)
                    RemoveAttribute("action");
                else
                    SetAttribute("action", value.ToString().ToLower());
            }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public Status Status
        {
            get { return GetAttributeEnum<Status>("status"); }
            set
            {
                if (value == Status.None)
                    RemoveAttribute("status");
                else
                    SetAttribute("status", value.ToString().ToLower());
            }
        }
        
        public Actions Actions
        {
            get { return Element<Actions>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>The note.</value>
        public Note Note
        {
            get { return Element<Note>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the X data.
        /// </summary>
        /// <value>The X data.</value>
        public XData.Data XData
        {
            get { return Element<XData.Data>(); }
            set { Replace(value); }
        }
    }
}