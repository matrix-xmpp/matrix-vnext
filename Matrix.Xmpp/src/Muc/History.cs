using System;
using Matrix.Core;
using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Muc
{
    /*
        Example 29. User Requests Limit on Number of Messages in History

        <presence
            from='hag66@shakespeare.lit/pda'
            to='darkcave@macbeth.shakespeare.lit/thirdwitch'>
          <x xmlns='http://jabber.org/protocol/muc'>
            <history maxstanzas='20'/>
          </x>
        </presence>
              

        Example 30. User Requests History in Last 3 Minutes

        <presence
            from='hag66@shakespeare.lit/pda'
            to='darkcave@macbeth.shakespeare.lit/thirdwitch'>
          <x xmlns='http://jabber.org/protocol/muc'>
            <history seconds='180'/>
          </x>
        </presence>
              

        Example 31. User Requests All History Since the Beginning of the Unix Era

        <presence
            from='hag66@shakespeare.lit/pda'
            to='darkcave@macbeth.shakespeare.lit/thirdwitch'>
          <x xmlns='http://jabber.org/protocol/muc'>
            <history since='1970-01-01T00:00Z'/>
          </x>
        </presence>
    
        <xs:element name='history'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='maxchars' type='xs:int' use='optional'/>
                  <xs:attribute name='maxstanzas' type='xs:int' use='optional'/>
                  <xs:attribute name='seconds' type='xs:int' use='optional'/>
                  <xs:attribute name='since' type='xs:dateTime' use='optional'/>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
        </xs:element>

    */

    /// <summary>
    /// Class for managing Muc history
    /// </summary>
    [XmppTag(Name = "history", Namespace = Namespaces.Muc)]
    public class History : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Muc"/> class.
        /// </summary>
        public History()
            : base(Namespaces.Muc, "history")
        {
        }

        /// <summary>
        /// get the history starting from a given date when available
        /// </summary>
        /// <param name="date"></param>
        public History(DateTime date)
            : this()
        {
            Since = date;
        }

        /// <summary>
        /// Specify the maximum nunber of messages to retrieve from the history
        /// </summary>
        /// <param name="max"></param>
        public History(int max)
            : this()
        {
            MaxStanzas = max;
        }
        #endregion

        /// <summary>
        /// request the last xxx seconds of history when available
        /// </summary>
        public int Seconds
        {
            get { return GetAttributeInt("seconds"); }
            set { SetAttribute("seconds", value); }
        }

        /// <summary>
        /// Request maximum stanzas of history when available
        /// </summary>
        public int MaxStanzas
        {
            get { return GetAttributeInt("maxstanzas"); }
            set { SetAttribute("maxstanzas", value); }
        }

        /// <summary>
        /// Request history from a given date when available
        /// </summary>
        public DateTime Since
        {
            get { return Core.Time.Iso8601Date(GetAttribute("since")); }
            set { SetAttribute("since", Core.Time.Iso8601Date(value)); }
        }

        /// <summary>
        /// Limit the total number of characters in the history to "X" 
        /// (where the character count is the characters of the complete XML stanzas, 
        /// not only their XML character data).
        /// </summary>
        public int MaxCharacters
        {
            get { return GetAttributeInt("maxchars"); }
            set { SetAttribute("maxchars", value); }
        }

        #region << Operators >>
        /// <summary>
        /// Performs an implicit conversion from <see cref="System.Int32"/> to <see cref="Matrix.Xmpp.Muc.History"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        static public implicit operator History(int value)
        {
            return new History(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="System.DateTime"/> to <see cref="Matrix.Xmpp.Muc.History"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the conversion.</returns>
        static public implicit operator History(DateTime value)
        {
            return new History(value);
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Matrix.Xmpp.Muc.History"/> to <see cref="System.Int32"/>.
        /// </summary>
        /// <param name="history">The history.</param>
        /// <returns>The result of the conversion.</returns>
        static public implicit operator int(History history)
        {
            return history.MaxStanzas;
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="Matrix.Xmpp.Muc.History"/> to <see cref="System.DateTime"/>.
        /// </summary>
        /// <param name="history">The history.</param>
        /// <returns>The result of the conversion.</returns>
        static public implicit operator DateTime(History history)
        {
            return history.Since;
        }
        #endregion
    }
}