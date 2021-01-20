using System;
using Matrix.Attributes;
using Matrix.Xml;

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

    [XmppTag(Name = "data", Namespace = Namespaces.GooglePush)]
    public class Data : XmppXElement
    {
        public Data() : base(Namespaces.GooglePush, "data")
        {
        }

        /// <summary>
        /// Set the value of the data element, the date gets converted to and from base64 automatically.
        /// </summary>
        public new byte[] Value
        {
            get { return Convert.FromBase64String(base.Value); }
            set { base.Value = Convert.ToBase64String(value); }
        }
    }
}
