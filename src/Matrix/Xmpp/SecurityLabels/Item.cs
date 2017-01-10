using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.SecurityLabels
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = "item", Namespace = Namespaces.SecurityLabelCatalog)]
    public class Item : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item() : base(Namespaces.SecurityLabelCatalog, "item")
        {
        }

        /// <summary>
        /// Gets or sets the selector.
        /// The selector represents the item's placement in a hierarchical organization of the items.
        /// If one item has a selecto, all items should have a selector.
        /// The value of the selector conforms to the selector-value ABNF production:
        /// </summary>
        /// <value>
        /// The selector.
        /// </value>
        public string Selector
        {
            get { return GetAttribute("selector"); }
            set { SetAttribute("selector", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Item"/> is the default.
        /// One and only one of the items may be the default item. The client should default the label selection to this item in cases where the user has not selected an item.
        /// </summary>
        /// <value>
        ///   <c>true</c> if default; otherwise, <c>false</c>.
        /// </value>
        public bool Default
        {
            get { return GetAttributeBool("default"); }
            set { SetAttribute("default", value); }
        }

        /// <summary>
        /// Gets or sets the security label.
        /// </summary>
        /// <value>
        /// The security label.
        /// </value>
        public SecurityLabel SecurityLabel
        {
            get { return Element<SecurityLabel>(); }
            set { Replace(value); }
        }
    }
}