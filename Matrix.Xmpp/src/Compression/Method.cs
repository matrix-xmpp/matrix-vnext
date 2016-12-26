using Matrix.Core;
using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Compression
{
    [XmppTag(Name = "method", Namespace = Namespaces.Compress)]
    public class Method : XmppXElement
    {
        #region << Constructors >>
        internal Method(string ns) : base(ns, "method")
        {
        }

        public Method() : this(Namespaces.Compress)
        {            
        }

        public Method(Methods method) : this()
        {
            Value = method.GetName();
        }
        #endregion

        public Methods CompressionMethod
        {
            get { return Util.Enum.ParseUsingNameAttrib<Methods>(Value); }
            set { Value = value.GetName(); }
        }
    }
}