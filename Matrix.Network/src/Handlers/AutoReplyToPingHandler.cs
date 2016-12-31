// Copyright (c)  AG-Software. All Rights Reserved.
// by Alexander Gnauck (alex@ag-software.net)

using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;
using Matrix.Xmpp.Ping;

namespace Matrix.Network.Handlers
{
    public class AutoReplyToPingHandler<T> : XmppStanzaHandler where T : Iq
    {
        public AutoReplyToPingHandler()
        {
            Subscribe(
                el =>
                    el.OfType<T>()
                    && el.Cast<T>().Type == IqType.Get
                    && el.Cast<T>().Query.OfType<Ping>(),

                async (context, xmppXElement) =>
                {
                    var iq = xmppXElement.Cast<T>();

                    var resIq = Factory.GetElement<T>();
                    resIq.Id    = iq.Id;
                    resIq.To    = iq.From;
                    resIq.Type  = IqType.Result;

                    await SendAsync(resIq);
                });
        }
    }
}
