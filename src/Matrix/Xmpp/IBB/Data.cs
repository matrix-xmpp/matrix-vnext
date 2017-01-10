using Matrix.Attributes;

namespace Matrix.Xmpp.IBB
{
    [XmppTag(Name = "data", Namespace = Namespaces.Ibb)]
    public class Data : Close
    {
        public Data() : base("data")
        {
        }

        /// <summary>
        /// the sequence
        /// </summary>
        public int Sequence
        {
            get { return GetAttributeInt("seq"); }
            set { SetAttribute("seq", value); }
        }
    }
}