namespace Matrix
{
    public abstract class XmppHandler
    {
        private XmppConnection xmppConnection;

        protected XmppHandler(XmppConnection xmppConnection)
        {
            this.xmppConnection = xmppConnection;
        }

        public XmppConnection XmppConnection => this.xmppConnection;
    }
}
