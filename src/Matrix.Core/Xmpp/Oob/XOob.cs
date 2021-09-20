using Matrix.Attributes;

namespace Matrix.Xmpp.Oob
{
    /*
        <message from='stpeter@jabber.org/work'
             to='MaineBoy@jabber.org/home'>
          <body>Yeah, but do you have a license to Jabber?</body>
          <x xmlns='jabber:x:oob'>
            <url>http://www.jabber.org/images/psa-license.jpg</url>
          </x>
        </message>
     */

    /// <summary>
    /// XEP-0066: Out of Band Data
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.XOob)]
    public class XOob : Oob
    {
        public XOob()
            : base(Namespaces.XOob, "x")
        {
        }
    }
}
