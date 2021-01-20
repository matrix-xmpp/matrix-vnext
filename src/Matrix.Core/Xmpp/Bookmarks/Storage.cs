using System.Collections.Generic;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Bookmarks
{
    [XmppTag(Name = "storage", Namespace = Namespaces.StorageBookmarks)]
    public class Storage : XmppXElement
    {
        public Storage()
            : base(Namespaces.StorageBookmarks, "storage")
        {
        }

        #region << Item properties >>
        /// <summary>
        /// Adds a conference.
        /// </summary>
        /// <returns></returns>
        public Conference AddConference()
        {
            var conf = new Conference();
            Add(conf);

            return conf;
        }

        /// <summary>
        /// Adds a conference.
        /// </summary>
        /// <param name="conference">The conference.</param>
        public void AddConference(Conference conference)
        {
            Add(conference);
        }

        /// <summary>
        /// Gets all conferences.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Conference> GetConferences()
        {
            return Elements<Conference>();
        }
        
        /// <summary>
        /// Sets the conferences.
        /// </summary>
        /// <param name="conferences">The conferences.</param>
        public void SetConferences(IEnumerable<Conference> conferences)
        {
            RemoveAllConferences();
            foreach (Conference conf in conferences)
                AddConference(conf);
        }

        /// <summary>
        /// Removes all Items.
        /// </summary>
        public void RemoveAllConferences()
        {
            RemoveAll<Conference>();
        }
        #endregion
    }
}
