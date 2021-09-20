using System;
using Matrix.Xml;

namespace Matrix.Xmpp.MessageArchiving
{
    public abstract class ArchiveItem : XmppXElement
    {
        protected ArchiveItem(string tagName): base(Namespaces.Archiving, tagName)
        {}

        /// <summary>
        /// Gets or sets the UTC time stamp of the absolute time the note was created. 
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime TimeStamp
        {
            get { return GetAttributeIso8601Date("utc"); }
            set { SetAttributeIso8601Date("utc", value); }
        }
    }
}
