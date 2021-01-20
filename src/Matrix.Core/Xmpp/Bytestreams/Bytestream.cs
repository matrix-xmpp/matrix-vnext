using System.Collections.Generic;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Bytestreams
{
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.Bytestreams)]
    public class Bytestream : XmppXElement
    {
        public Bytestream() : base(Namespaces.Bytestreams, Tag.Query)
        {
        }

        public string Sid
        {
            get { return GetAttribute("sid");}
            set { SetAttribute("sid", value);}
        }
        
        public Mode Mode
        {
            get { return GetAttributeEnum<Mode>("mode"); }
            set
            {
                if (value == Mode.None)
                    RemoveAttribute("mode");
                
                SetAttribute("mode", value.ToString().ToLower());
            }
        }

        #region << streamhost members >>
        /// <summary>
        /// Adds a atreamhost.
        /// </summary>
        /// <returns></returns>
        public Streamhost AddStreamhost()
        {
            var shost = new Streamhost();
            Add(shost);

            return shost;
        }

        /// <summary>
        /// Adds the streamhost.
        /// </summary>
        /// <param name="shost">The streamhost.</param>
        public void AddStreamhost(Streamhost shost)
        {
            Add(shost);
        }
        /// <summary>
        /// Gets all streamhosts.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Streamhost> GetStreamhosts()
        {
            return Elements<Streamhost>();
        }

        /// <summary>
        /// Sets the streamhosts.
        /// </summary>
        /// <param name="shosts">The streamhosts.</param>
        public void SetStreamhosts(IEnumerable<Streamhost> shosts)
        {
            RemoveAllStreamhosts();
            foreach (Streamhost host in shosts)
                AddStreamhost(host);
        }

        /// <summary>
        /// Removes all streamhosts.
        /// </summary>
        public void RemoveAllStreamhosts()
        {
            RemoveAll<Streamhost>();
        }
        #endregion
        
        public Activate Activate
        {
            get { return Element<Activate>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }

        public StreamhostUsed StreamhostUsed
        {
            get { return Element<StreamhostUsed>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }
    }
}
