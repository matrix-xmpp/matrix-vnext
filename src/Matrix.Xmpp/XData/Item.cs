using Matrix.Core.Attributes;

namespace Matrix.Xmpp.XData
{
    /// <summary>
    /// Used in XData seach.
    /// includes the headers of the search results
    /// </summary>
    [XmppTag(Name = "item", Namespace = Namespaces.XData)]
    public class Item : FieldContainer
    {
        #region << Constructors >>
        public Item() : base("item")
        {            
        }
        #endregion
    }
}