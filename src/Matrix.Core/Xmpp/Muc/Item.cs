namespace Matrix.Xmpp.Muc
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Item : Base.Item
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="ns">The namespace.</param>
        protected Item(string ns)
            : base(ns)
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public Role Role
        {
            get { return GetAttributeEnum<Role>("role"); }
            set { SetAttribute("role", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Gets or sets the affiliation.
        /// </summary>
        /// <value>The affiliation.</value>
        public Affiliation Affiliation
        {
            get { return GetAttributeEnum<Affiliation>("affiliation"); }
            set { SetAttribute("affiliation", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>The nickname.</value>
        public string Nickname
        {
            get { return GetAttribute("nick"); }
            set { SetAttribute("nick", value); }
        }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public string Reason
        {
            set { SetTag("reason", value); }
            get { return GetTag("reason"); }
        }        
    }
}
