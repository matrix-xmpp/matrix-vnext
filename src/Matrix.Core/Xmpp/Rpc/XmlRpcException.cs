using System;

namespace Matrix.Xmpp.Rpc
{
    public class XmlRpcException : Exception
    {
        public XmlRpcException()
        {
        }

        public XmlRpcException(string msg)
            : base(msg)
        {
        }

        public XmlRpcException(Int16 code, string msg)
            : base(msg)
        {
            Code = code;
        }

        public Int16 Code { get; set; }
    }
}
