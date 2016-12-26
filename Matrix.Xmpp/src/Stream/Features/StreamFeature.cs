using Matrix.Xml;

namespace Matrix.Xmpp.Stream.Features
{
    public abstract class StreamFeature : XmppXElement
    {
        protected StreamFeature(string ns, string tagname)  : base(ns, tagname)
        {
        }

        /// <summary>
        /// Is this feature optional?
        /// </summary>
        public bool Optional
        {
            get { return HasTag("optional"); }
            set
            {
                if (value == false)
                    RemoveTag("optional");
                else
                    SetTag("optional");
            }
        }

        /// <summary>
        /// Is this feature required?
        /// </summary>
        public bool Required
        {
            get { return HasTag("required"); }
            set
            {
                if (value == false)
                    RemoveTag("required");
                else
                    SetTag("required");
            }
        }
    }
}