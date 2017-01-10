using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Muc.Owner
{
    /// <summary>
    /// Owner Query
    /// </summary>
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.MucOwner)]
    public class OwnerQuery : XmppXElementWithXData
    {
        /*
        <query xmlns='http://jabber.org/protocol/muc#owner'/>
        */

        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerQuery"/> class.
        /// </summary>
        public OwnerQuery()
            : base(Namespaces.MucOwner, Tag.Query)
        {
        }
        #endregion
    }
}