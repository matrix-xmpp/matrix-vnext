namespace ConsoleClient
{
    using Matrix.Attributes;
    using Matrix.Network.Handlers;
    using Matrix.Xml;
    using Matrix.Xmpp;
    using Matrix.Xmpp.Client;    
    using Matrix.Xmpp.Version;

    /// <summary>
    /// This handler automatically replies to incoming XMPP software version requests (XEP-0092: Software Version)
    /// </summary>    
    [Name("Version-Handler")]
    public class XmppVersionHandler : XmppStanzaHandler
    {
        public XmppVersionHandler()
        {
            Handle(
                el =>
                    el.OfType<Iq>()
                    && el.Cast<Iq>().Type == IqType.Get
                    && el.Cast<Iq>().Query.OfType<Version>(),

                async (context, xmppXElement) =>
                {
                    var iq = xmppXElement.Cast<Iq>();

                    var resIq = new VersionIq();
                    resIq.Id = iq.Id;
                    resIq.To = iq.From;
                    resIq.Type = IqType.Result;
                    
                    resIq.Version.Name = "Matrix-Client";
                    resIq.Version.Os = "Windows";
                    resIq.Version.Ver = "1.2.0";

                    await SendAsync(resIq);
                });
        }
    }
}
