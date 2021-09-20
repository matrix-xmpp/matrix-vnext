using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Google.Push
{
    #region Xml sample
    /*
        <subscribe xmlns="google:push">
		    <item channel="cloudprint.google.com" from="cloudprint.google.com"/>
	    </subscribe>
    */
    #endregion

    [XmppTag(Name = "subscribe", Namespace = Namespaces.GooglePush)]
    public class Subscribe : XmppXElement
    {
        public Subscribe() : base(Namespaces.GooglePush, "subscribe")
        {
        }
    }
}
