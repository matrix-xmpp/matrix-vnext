using Matrix.Attributes;

namespace Matrix.Xmpp.Sasl
{
    [XmppTag(Name = "response", Namespace = Namespaces.Sasl)]
    public class Response : Base.Sasl
    {
        #region xml sample
        /*
            <response xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
            dXNlcm5hbWU9InNvbWVub2RlIixyZWFsbT0ic29tZXJlYWxtIixub25jZT0i
            T0E2TUc5dEVRR20yaGgiLGNub25jZT0iT0E2TUhYaDZWcVRyUmsiLG5jPTAw
            MDAwMDAxLHFvcD1hdXRoLGRpZ2VzdC11cmk9InhtcHAvZXhhbXBsZS5jb20i
            LHJlc3BvbnNlPWQzODhkYWQ5MGQ0YmJkNzYwYTE1MjMyMWYyMTQzYWY3LGNo
            YXJzZXQ9dXRmLTgK
            </response>
        */
        #endregion

        #region << Constructors >>
        public Response() : base("response")
        {            
        }

        public Response(string content) : this()
        {            
            Value = content;
        }
        #endregion
    }
}
