using System.Collections.Generic;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.RosterItemExchange
{
    /// <summary>
    /// XEP-0144 Roster Item exchange
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.XRosterX)]
    public class Exchange : XmppXElement
    {
        #region Xml sample
        /*
            <message from='horatio@denmark.lit' to='hamlet@denmark.lit'>
              <body>Some visitors, m'lord!</body>
              <x xmlns='http://jabber.org/protocol/rosterx'> 
                <item action='add'
                      jid='rosencrantz@denmark.lit'
                      name='Rosencrantz'>
                  <group>Visitors</group>
                </item>
                <item action='add'
                      jid='guildenstern@denmark.lit'
                      name='Guildenstern'>
                  <group>Visitors</group>
                </item>
              </x>
            </message>
        */
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Exchange" /> class.
        /// </summary>
        public Exchange() : base(Namespaces.XRosterX, "x")
        {}
        
        /// <summary>
        /// Gets the roster exchange items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RosterExchangeItem> GetRosterExchangeItems()
        {
            return Elements<RosterExchangeItem>();
        }
        
        /// <summary>
        /// Adds the roster exchange item.
        /// </summary>
        /// <param name="ri">The ri.</param>
        public void AddRosterExchangeItem(RosterExchangeItem ri)
        {
            Add(ri);
        }
    }
}
