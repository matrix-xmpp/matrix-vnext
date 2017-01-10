using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "session", Namespace = Namespaces.Archiving)]
    public class Session : XmppXElement
    {
        public Session() : base(Namespaces.Archiving, "session")
        {
        }

        public string Thread
        {
            get { return GetAttribute("thread"); }
            set { SetAttribute("thread", value); }
        }
       
        /// <summary>
        /// Specifies the user's default setting for Save Mode.
        /// </summary>
        public SaveType Save
        {
            get { return GetAttributeEnum<SaveType>("save"); }
            set { SetAttribute("save", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Specifies the user's default setting for OTR Mode.
        /// </summary>
        public OtrType Otr
        {
            get { return GetAttributeEnum<OtrType>("otr"); }
            set { SetAttribute("otr", value.ToString().ToLower()); }
        }

        /// <summary>
        /// The 'timeout' indicates how long this rule will stay in server after the latest message in this thread is exchanged. 
        /// Server MUST NOT forget this rule before 'timeout' seconds after latest message in this thread is exchanged but MAY keep this rule
        ///  longer than 'timeout' value specifies.
        /// Client MUST NOT set this , but wait for server's answer to know this value.
        /// If the client wants to keep this rule longer, it must send a new <session/> element to the server before this timeour expires.
        /// </summary>
        public int Timeout
        {
            get { return GetAttributeInt("timeout"); }
            set { SetAttribute("timeout", value); }
        }
    }
}