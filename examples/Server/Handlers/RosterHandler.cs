using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matrix.Network.Handlers;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;
using Matrix.Xmpp.Ping;

namespace Server.Handlers
{
    public class RosterHandler<T> : XmppStanzaHandler where T : Iq
    {
        public RosterHandler()
        {
            //Handle(
            //    el =>
            //        el.OfType<T>()
            //        && el.Cast<T>().Type == IqType.Get
            //        && el.Cast<T>().Query.OfType<Ping>(),

            //    async (context, xmppXElement) =>
            //    {
            //        var iq = xmppXElement.Cast<T>();

            //        var resIq = Factory.GetElement<T>();
            //        resIq.Id = iq.Id;
            //        resIq.To = iq.From;
            //        resIq.Type = IqType.Result;

            //        await SendAsync(resIq);
            //    });
        }
    }
}
