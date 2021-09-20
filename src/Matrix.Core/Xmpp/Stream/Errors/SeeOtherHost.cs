using System;
using System.Globalization;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Stream.Errors
{
    [XmppTag(Name = "see-other-host", Namespace = Namespaces.Streams)]
    public class SeeOtherHost : XmppXElement
    {
        // <see-other-host xmlns="urn:ietf:params:xml:ns:xmpp-streams">BAYMSG1020127.gateway.edge.messenger.live.com</see-other-host>
        public SeeOtherHost() : base(Namespaces.Streams, "see-other-host")
        {
        }

        public string Hostname
        {
            get
            {
                if (!String.IsNullOrEmpty(Value))
                {
                    var split = Value.Split(':');
                    if (split.Length > 0)
                    {
                        return split[0];
                    }
                }
                return null;
            }
            set { Value = value + ":" + Port.ToString(CultureInfo.InvariantCulture); }
        }

        public int Port
        {
            get
            {
                if (!String.IsNullOrEmpty(Value))
                {
                    var split = Value.Split(':');
                    if (split.Length > 1)
                    {
                        return int.Parse(split[1]);
                    }
                }
                return 5222;
            }
            set { Value = Hostname + ":" + value; }
        }
    }
}
