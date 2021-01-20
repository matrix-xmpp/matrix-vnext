namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Item : XmppXElementWithJidAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="ns">The ns.</param>
        protected Item(string ns) : base(ns, Tag.Item)
        {            
        }

        /// <summary>
        /// Gets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        internal string Namespace
        {
            get { return base.Name.Namespace.NamespaceName; }
        }
        
        /// <summary>
        /// Gets the name of this element.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// An <see cref="T:System.Xml.Linq.XName"/> that contains the name of this element.
        /// </returns>
        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }
    }
}
