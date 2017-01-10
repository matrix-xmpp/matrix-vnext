using System;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Disco
{
    [XmppTag(Name = "identity", Namespace = Namespaces.DiscoInfo)]
    public class Identity : XmppXElement, IEquatable<Identity>
    {
        #region << Constructors >>
        public Identity() : base(Namespaces.DiscoInfo, "identity")
        {
        }

        public Identity(string type, string name, string category) : this()
        {
            Type        = type;
            Name        = name;
            Category    = category;
        }

        public Identity(string type, string category) : this()
        {
            Type = type;
            Category = category;
        }
        #endregion

        #region << Properties >>
        /// <summary>
        /// type category name for the entity
        /// </summary>
        public string Type
        {
            get { return GetAttribute("type"); }
            set { SetAttribute("type", value); }
        }

        /// <summary>
        /// natural-language name for the entity
        /// </summary>
        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// category name for the entity
        /// </summary>
        public string Category
        {
            get { return GetAttribute("category"); }
            set { SetAttribute("category", value); }
        }
        #endregion

        public string Key
        {
            get { return string.Format("{0}/{1}/{2}/{3}", Category, Type, XmlLanguage, Name); }
        }
        
        public bool Equals(Identity other)
        {
            return String.CompareOrdinal(Key, other.Key) == 0;
        }
    }
}