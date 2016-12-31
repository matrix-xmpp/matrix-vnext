using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;

namespace Matrix.Network.Handlers
{
    public class IqHandler : XmppStanzaHandler
    {
        static readonly ConcurrentDictionary<string, Action<IChannelHandlerContext, XmppXElement>> DictFilter
            = new ConcurrentDictionary<string, Action<IChannelHandlerContext, XmppXElement>>();

        public IqHandler()
        {
            Handle<Iq>(HandleIq);
        }

        public async Task SendIq(Iq iq, Action<IChannelHandlerContext, XmppXElement> response)
        {
            var id = iq.Id;
            if (!string.IsNullOrEmpty(id))
            {
                if (DictFilter.TryAdd(id, response))
                    await SendAsync(iq); // await XmppClient.SendAsync(iq);
            }
        }

        public async Task<Iq> SendIqAsync(Iq iq, int timeout = 5000)
        {
            string iqId = iq.Id;
            var resultCompletionSource = new TaskCompletionSource<Iq>();

            await SendIq(iq, (chctx, el) =>
            {
                resultCompletionSource.SetResult(el as Iq);
            });


            if (resultCompletionSource.Task ==
                await Task.WhenAny(resultCompletionSource.Task, Task.Delay(timeout)))
                return await resultCompletionSource.Task;

            // timed out, remove this iq from our dictionary
            Action<IChannelHandlerContext, XmppXElement> action;
            DictFilter.TryRemove(iqId, out action);
            resultCompletionSource.SetException(new TimeoutException());

            return await resultCompletionSource.Task;
        }

        private void HandleIq(IChannelHandlerContext ctx, XmppXElement el)
        {
            var iq = el as Iq;
            if (iq == null)
                return;

            //iq response MUST be always either of type result or error
            if (iq.Type != IqType.Error && iq.Type != IqType.Result)
                return;

            string id = iq.Id;
            if (string.IsNullOrEmpty(id))
                return;

            if (DictFilter.ContainsKey(id))
            {
                Action<IChannelHandlerContext, XmppXElement> action;
                if (DictFilter.TryRemove(id, out action))
                    action.Invoke(ctx, iq);
            }
        }
    }
}
