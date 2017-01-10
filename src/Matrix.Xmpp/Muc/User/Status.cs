using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Muc.User
{
    /// <summary>
    /// Status
    /// </summary>
    [XmppTag(Name = "status", Namespace = Namespaces.MucUser)]
    public class Status : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status() : base(Namespaces.MucUser, "status")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public Status(StatusCode code)
            : this()
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public Status(int code)
            : this()
        {
            CodeInt = code;
        }
        #endregion
        
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The code as integer.</value>
        public int CodeInt
        {
            get { return GetAttributeInt("code"); }
            set { SetAttribute("code", value); }
        }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The code.</value>
        public StatusCode Code
        {
            get { return GetAttributeEnum<StatusCode>("code"); }
            set { SetAttribute("code", value.ToString()); }
        }
    }
}