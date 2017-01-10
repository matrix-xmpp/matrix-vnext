using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Vcard.Update
{
    /// <summary>
    /// Vcard update
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.VcardUpdate)]
    public class X : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="X"/> class.
        /// </summary>
        public X() 
            : base(Namespaces.VcardUpdate, "x")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X"/> class.
        /// </summary>
        /// <param name="photo">The photo.</param>
        public X(string photo) : this()
        {
            Photo = photo;
        }
        #endregion

        /// <summary>
        /// Gets or sets the photo hash.
        /// </summary>
        /// <value>The photo.</value>
        public string Photo
        {
            get { return GetTag("photo"); }
            set
            {
                if (value == null)
                    RemoveTag("photo");
                else
                    SetTag("photo", value);
            }
        }
    }
}