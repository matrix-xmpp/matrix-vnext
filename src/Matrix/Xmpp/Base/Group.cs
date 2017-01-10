using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    public class Group : XmppXElement
    {
        public Group() : base(Namespaces.Client, "group")
        {
        }

        public Group(string groupname)
            : this()
        {
            Name = groupname;
        }

        /// <summary>
        /// gets or sets the Name of the contact group
        /// </summary>
        public new string Name
        {
            set { Value = value; }
            get { return Value; }
        }
    }
}