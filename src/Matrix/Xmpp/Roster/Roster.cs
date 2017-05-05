/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using System.Linq;
using System.Collections.Generic;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Roster
{
    /// <summary>
    /// Roster class, this represents a contact list in XMPP aka roster.
    /// </summary>
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqRoster)]
    public class Roster : XmppXElement
    {
        #region Xml sample
        // <iq from="user@server.com/Office" id="doroster_1" type="result">
        //		<query xmlns="jabber:iq:roster">
        //			<item subscription="both" name="juiliet" jid="11111@server.com"><group>Group 1</group></item>
        //			<item subscription="both" name="roman" jid="22222@server.com"><group>Group 1</group></item>
        //			<item subscription="both" name="angie" jid="33333@server.com"><group>Group 1</group></item>
        //			<item subscription="both" name="bob" jid="44444@server.com"><group>Group 2</group></item>
        //		</query>
        // </iq> 
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Roster"/> class.
        /// </summary>
        public Roster() : base(Namespaces.IqRoster, Tag.Query)
        {
        }

        /// <summary>
        /// A string that identifies a particular version of the roster information.
        /// The value MUST be generated only by the server and MUST be treated by the client as opaque.
        /// The server can use any appropriate method for generating the version ID, such as a hash of the roster data 
        /// or a strictly-increasing sequence number.
        /// </summary>
        public string Version
        {
            get { return GetAttribute("ver"); }
            set { SetAttribute("ver", value); }
        }

        /// <summary>
        /// Get all groups
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RosterItem> GetRoster()
        {
            return Elements<RosterItem>();
        }

        /// <summary>
        /// Adds the roster item.
        /// </summary>
        /// <param name="ri">The ri.</param>
        public void AddRosterItem(RosterItem ri)
        {
            Add(ri);
        }
        
        /// <summary>
        /// Does the Roster contain an RosterItem with the given Jid?
        /// </summary>
        /// <param name="jid"></param>
        /// <returns></returns>
        public bool ContainsRosterItem(Jid jid)
        {
            var items = GetRoster();
            
            if (items == null)
                return false;
            
            if (!items.Any())
                return false;
            
            var bjc = new BareJidComparer();
            return items.Any(r => r.Jid.Equals(jid, bjc));
        }

        public RosterItem this [Jid jid]
        {
             get
             {
                 var items = GetRoster();

                 if (items != null)
                 {
                     var bjc = new BareJidComparer();
                     return items.FirstOrDefault(r => r.Jid.Equals(jid, bjc));    
                 }
                 return null;
             }
        }

        /// <summary>
        /// Removes the RosterItem from the roster with the given Jid
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <returns>true when removed, false when the item did not exists and could not be removed.</returns>
        public bool RemoveRosterItem(Jid jid)
        {
            var bjc = new BareJidComparer();
            var item = GetRoster().FirstOrDefault(r => r.Jid.Equals(jid, bjc));
            if (item != null)
            {
                item.Remove();
                return true;
            }
            return false;
        }
    }
}
