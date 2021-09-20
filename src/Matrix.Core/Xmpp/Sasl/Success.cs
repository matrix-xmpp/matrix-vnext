using Matrix.Attributes;

namespace Matrix.Xmpp.Sasl
{
    /// <summary>
    /// Sasl success
    /// </summary>
    [XmppTag(Name = "success", Namespace = Namespaces.Sasl)]
    public class Success :  Base.Sasl
    {
        #region Xml sample
        /*
            <success xmlns="urn:ietf:params:xml:ns:xmpp-sasl">dj1QY21hNHJPQUdId0hLazBSWU9TRkRzL29SYTQ9</success>
            <success xmlns='urn:ietf:params:xml:ns:xmpp-sasl'/>
         */
        #endregion

        public Success() : base("success")
        {            
        }
    }
}
