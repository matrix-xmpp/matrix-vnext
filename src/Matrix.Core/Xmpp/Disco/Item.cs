using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Disco
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.DiscoItems)]
    public class Item : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
            : base(Namespaces.DiscoItems, "item")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        public Item(Jid jid)
            : this()
        {
            Jid = jid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <param name="node">The node.</param>
        public Item(Jid jid, string node)
            : this(jid)
        {
            Node = node;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="jid">The jid.</param>
        /// <param name="node">The node.</param>
        /// <param name="name">The name.</param>
        public Item(Jid jid, string node, string name)
            : this(jid, node)
        {
            Name = name;
        }
        #endregion

        /// <summary>
        /// Gets or sets the Jid.
        /// </summary>
        /// <value>The Jid.</value>
        public Jid Jid
        {
            get { return new Jid(GetAttribute("jid")); }
            set { SetAttribute("jid", value.ToString()); }
        }

        /// <summary>
        /// Gets the name of this element.
        /// </summary>
        /// <value>The name</value>        
        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }
    }
}
