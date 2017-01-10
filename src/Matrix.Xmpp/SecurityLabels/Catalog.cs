using Matrix.Core.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.SecurityLabels
{
    [XmppTag(Name = "catalog", Namespace = Namespaces.SecurityLabelCatalog)]
    public class Catalog : XmppXElementWithAddressAndIdAttributeAndItemCollection<Item>
    {
        public Catalog() : base(Namespaces.SecurityLabelCatalog, "catalog")
        {
        }

        public string CatalogName
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        public string Description
        {
            get { return GetAttribute("desc"); }
            set { SetAttribute("desc", value); }
        }

        public bool Restrictive
        {
            get { return GetAttributeBool("restrict"); }
            set { SetAttribute("restrict", value); }
        }

        public int Size
        {
            get { return GetAttributeInt("size"); }
            set { SetAttribute("size", value); }
        }
    }
}