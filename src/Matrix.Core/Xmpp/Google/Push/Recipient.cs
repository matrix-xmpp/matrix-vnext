using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Google.Push
{
    #region Xml sample
    /*
    <message from="cloudprint.google.com" to="{FULL_JID">
     <push xmlns="google:push" channel="cloudprint.google.com">
       <recipient to="{BARE_JID}">{raw data, ignore}</recipient>
       <data>{base-64 encoded printer id}</data>
     </push>
    </message>
    */
    #endregion

    [XmppTag(Name = "recipient", Namespace = Namespaces.GooglePush)]
    public class Recipient : XmppXElementWithAddress
    {
        public Recipient(): base(Namespaces.GooglePush, "recipient")
        {}
    }
}
