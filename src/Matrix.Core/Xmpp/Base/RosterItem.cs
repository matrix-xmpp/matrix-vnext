using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// Abstract RosterItem
    /// </summary>
	public abstract class RosterItem : Item
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="RosterItem"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        protected RosterItem(string ns) : base(ns)
        {
        }              

        /*
        <item subscription="both" name="Bob" jid="bob@server.org">
            <group>Work</group></item>
            <group>Jabber</group>
        </item>
        */
              
        /// <summary>
        /// Add a new group to the contact
        /// </summary>
        /// <param name="groupname"></param>
        public void AddGroup(string groupname)
        {
            if (!HasGroup(groupname))
            {
                var group = new XmppXElement(Namespace, "group") {Value = groupname};
                Add(group);
            }           
        }

        /// <summary>
        /// is this contact in any groups?
        /// </summary>
        public bool HasGroups
        {
            get { return GetGroupXElements().Any(); }
        }         
        
        /// <summary>
        /// check if the contact is i na specific group
        /// </summary>
        /// <param name="groupname"></param>
        /// <returns></returns>
        public bool HasGroup(string groupname)
        {
            if (HasGroups)
                return GetGroupXElements().Any(g => g.Value == groupname);
            
            return false;
        }

        /// <summary>
        /// remove the contact from a group
        /// </summary>
        /// <param name="groupname">group to remove</param>
        public void RemoveGroup(string groupname)
        {
            XElement group = GetGroupXElements().FirstOrDefault(g => g.Value == groupname);

            if (group != null)
                group.Remove();
        }

        /// <summary>
        /// Get all groups
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<XElement> GetGroupXElements()
        {
            return Elements("{" + Namespace + "}group");
        }

        /// <summary>
        /// Gets all the roster groups.
        /// </summary>
        /// <returns></returns>
        public List<string> GetGroups()
        {
            IEnumerable<XElement> eGroups = GetGroupXElements();
            return eGroups.Select(x => x.Value).ToList();            
        }
    }
}
