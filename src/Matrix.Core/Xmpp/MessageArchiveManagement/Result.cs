using Matrix.Attributes;

namespace Matrix.Xmpp.MessageArchiveManagement
{
    [XmppTag(Name = "result", Namespace = Namespaces.MessageArchiveManagement)]
    public class Result : MessageArchive
    {
        public Result() : base("result")
        {
        }

        /// <summary>
        /// Get or sets the id
        /// </summary>
        public string Id
        {
            get => GetAttribute("id");
            set => SetAttribute("id", value);
        }
    }
}