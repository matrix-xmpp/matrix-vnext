namespace Matrix.Xmpp.MessageArchiveManagement
{
    using Matrix.Attributes;
    using Matrix.Xml;
    using ResultSetManagement;

    [XmppTag(Name = "fin", Namespace = Namespaces.MessageArchiveManagement)]
    public class Final : XmppXElement
    {
        public Final() : base(Namespaces.MessageArchiveManagement, "fin")
        {
        }

        /// <summary>
        /// Gets or sets a value to indicate whether the result is complete or not
        /// </summary>
        public bool Complete
        {
            get => GetAttributeBool("complete");
            set => SetAttribute("complete", value);
        }

        /// <summary>
        /// Gets or sets the result set.
        /// </summary>
        /// <value>
        /// The result set.
        /// </value>
        public Set ResultSet
        {
            get { return Element<Set>(); }
            set { Replace(value); }
        }
    }
}