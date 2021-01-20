using Matrix.Xml;

namespace Matrix.Xmpp.Rpc
{
    public abstract class XmlRpcBase : XmppXElement
    {
        protected XmlRpcBase(string tagName) : base(Namespaces.IqRpc, tagName)
        {
        }

        public Parameters GetParameters()
        {
            return XmlRpcParser.ParseParams(this);
        }

        public void SetParameters(Parameters @params)
        {
            RemoveAll<Params>();
            var elParams = XmlRpcWriter.WriteParams(@params);
            Add(elParams);
        }
    }
}
