using Matrix.Core;
using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Compression
{
    [XmppTag(Name = "compress", Namespace = Namespaces.Compress)]
    public class Compress : XmppXElement
    {        
        #region << Constructors >>
        public Compress() : base(Namespaces.Compress, "compress")
        {
        }

        /// <summary>
        /// Constructor with a given method/algorithm for Stream compression
        /// </summary>
        /// <param name="method">method/algorithm used to compressing the stream</param>
        public Compress(Methods method) : this()
        {
            Method = method;
        }
        #endregion

        /// <summary>
        /// method/algorithm used to compressing the stream
        /// </summary>
        public Methods Method
        {
            set 
            {
                if (value != Methods.Unknown)
					SetTag("method", value.GetName());  
            }
            get 
            {
                return GetTagEnumUsingNameAttrib<Methods>("method");
            }
        }
    }    
}