using System;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix.Xml;

namespace Matrix.Network.Handlers
{
    public class WaitForStanzaHandler : XmppStanzaHandler
    {
        const int DefaultTimeout = 500000;

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout = DefaultTimeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
           where T3 : XmppXElement
        {
            return await SendAsync<XmppXElement>(el, typeof(T1), typeof(T2), typeof(T3), timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout = DefaultTimeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            return await SendAsync<XmppXElement>(el, typeof(T1), typeof(T2), timeout);
        }

        internal async Task<XmppXElement> SendAsync<T1, T2>(string s, int timeout = DefaultTimeout)
          where T1 : XmppXElement
          where T2 : XmppXElement
        {
            return await SendAsync<XmppXElement>(s, typeof(T1), typeof(T2), null, timeout);
        }

        public async Task<T> SendAsync<T>(XmppXElement el, int timeout = DefaultTimeout)
            where T : XmppXElement
        {
            return await SendAsync<T>(el, typeof(T), null, timeout);
        }

        public async Task<T> SendAsync<T>(string s, int timeout = DefaultTimeout)
           where T : XmppXElement
        {
            return await SendAsync<T>(s, typeof(T), null, null, timeout);
        }

        private async Task<T> SendAsync<T>(XmppXElement el, Type sub1, Type sub2 = null, int timeout = DefaultTimeout)
            where T : XmppXElement

        {
            return await SendAsync<T>(el.ToString(false), sub1, sub2, null, timeout);
        }

        private async Task<T> SendAsync<T>(XmppXElement el, Type sub1, Type sub2 = null, Type sub3 = null, int timeout = DefaultTimeout)
           where T : XmppXElement

        {
            return await SendAsync<T>(el.ToString(false), sub1, sub2, sub3, timeout);
        }

        private async Task<T> SendAsync<T>(string s, Type sub1, Type sub2 = null, Type sub3 = null, int timeout = DefaultTimeout)
           where T : XmppXElement

        {
            var resultCompletionSource = new TaskCompletionSource<T>();

            var action = new Action<IChannelHandlerContext, XmppXElement>(
                (ctx, xel) =>
                {
                    UnSubscribe(sub1, sub2);
                    resultCompletionSource.SetResult(xel as T);
                });

            Subscribe(sub1, action);
            Subscribe(sub2, action);
            Subscribe(sub3, action);

            await SendAsync(s);

            if (resultCompletionSource.Task ==
                await Task.WhenAny(resultCompletionSource.Task, Task.Delay(timeout)))
                return await resultCompletionSource.Task;

            // timed out, remove this iq from our dictionary
            UnSubscribe(sub1, sub2, sub3);

            resultCompletionSource.SetException(new TimeoutException());

            return await resultCompletionSource.Task;
        }
    }
}
