using System.Collections.Generic;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Privacy
{
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqPrivacy)]
    public class Privacy : XmppXElement
    {
        public Privacy() : base(Namespaces.IqPrivacy, Tag.Query)
        {
        }
        
        /// <summary>
        /// Add a privacy list
        /// </summary>
        /// <param name="list"></param>
        public void AddList(List list)
        {
            Add(list);
        }
        
        /// <summary>
        /// get all Lists
        /// </summary>
        /// <returns></returns>
          
        public IEnumerable<List> GetLists()
        {
            return Elements<List>();
        }

        /// <summary>
        /// The active list
        /// </summary>
        public Active Active
        {
            get { return Element<Active>(); }
            set { Replace(value); }
        }
        
        /// <summary>
        /// The default list
        /// </summary>
        public Default Default
        {
            get { return Element<Default>(); }
            set { Replace(value); }
        }
    }
}
