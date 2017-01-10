using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub.Event
{
    [XmppTag(Name = "collection", Namespace = Namespaces.PubsubEvent)]
    public class Collection : XmppXElement
    {
        public Collection()
            : base(Namespaces.PubsubEvent, "collection")
        {
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

        public Associate Associate
        {
            get { return Element<Associate>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }

        public Disassociate Disassociate
        {
            get { return Element<Disassociate>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }

        //public CollectionType Type
        //{
        //    get
        //    {
        //        // order important because of inheritance
        //        if (Disassociate != null)
        //            return CollectionType.Disassociate;

        //        if (Associate != null)
        //            return CollectionType.Associate;

        //        return CollectionType.None;
        //    }
        //    set
        //    {
        //        RemoveNodes();
        //        switch (value)
        //        {
        //            case CollectionType.Disassociate:
        //                Add(new Disassociate());
        //                break;
        //            case CollectionType.Associate:
        //                Add(new Associate());
        //                break;
        //        }
        //    }
        //}
    }
}