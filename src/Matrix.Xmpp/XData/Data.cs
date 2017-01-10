using System.Collections.Generic;
using Matrix.Core.Attributes;

namespace Matrix.Xmpp.XData
{
    [XmppTag(Name = "x", Namespace = Namespaces.XData)]
    public class Data  : FieldContainer
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Data"/> class.
        /// </summary>
        public Data() : base("x")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Data"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Data(FormType type)
            : this()
        {
            Type = type;
        }
        #endregion

        #region << Properties >>
        public string Title
        {
            get { return GetTag("title"); }
            set { SetTag("title", value); }
        }

        public string Instructions
        {
            get { return GetTag("instructions"); }
            set { SetTag("instructions", value); }
        }

        /// <summary>
        /// Type of thie XDATA Form.
        /// </summary>
        public FormType Type
        {
            get { return GetAttributeEnum<FormType>("type"); }
            set { SetAttribute("type", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Gets or sets the reported.
        /// </summary>
        /// <value>The reported.</value>
        public Reported Reported
        {
            get { return Element<Reported>(); }
            set { Replace(value); }
        }

        #endregion

        #region << public Methods >>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Item AddItem()
        {
            var i = new Item();
            Add(i);
            return i;
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public Item AddItem(Item item)
        {
            Add(item);
            return item;
        }

        /// <summary>
        /// Gets a list of all form items
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> GetItems()
        {
            return Elements<Item>();
        }
        #endregion
    }
}