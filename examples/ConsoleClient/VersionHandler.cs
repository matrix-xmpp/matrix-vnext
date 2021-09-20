namespace ConsoleClient
{
    using Matrix;
    using Matrix.Xml;
    using Matrix.Xmpp;
    using Matrix.Xmpp.Client;
    using System;
    using System.Reactive.Linq;

    /// <summary>
    /// This handler automatically replies to incoming XMPP software version requests (XEP-0092: Software Version)
    /// </summary>    
    public class VersionHandler : XmppHandler
    {
        public VersionHandler(XmppClient xmppClient)
            : base(xmppClient)
        {
            xmppClient
                .XmppXElementReceived
                .Where(
                    el =>
                        el.OfType<Iq>()
                        && el.Cast<Iq>().Type == IqType.Get
                        && el.Cast<Iq>().Query.OfType<Matrix.Xmpp.Version.Version>()
                )
                .Subscribe(async el =>
                {
                    var iq = el.Cast<Iq>();

                    var resIq = new VersionIq();
                    resIq.Id = iq.Id;
                    resIq.To = iq.From;
                    resIq.Type = IqType.Result;

                    resIq.Version.Name = "Matrix-Client";
                    resIq.Version.Os = "Windows";
                    resIq.Version.Ver = "1.2.0";

                    await xmppClient.SendAsync(resIq).ConfigureAwait(false);
                });
        }
    }
}
