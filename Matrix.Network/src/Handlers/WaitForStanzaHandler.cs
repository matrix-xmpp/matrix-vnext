using System;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix.Xml;

namespace Matrix.Network.Handlers
{
    public class WaitForStanzaHandler : XmppStanzaHandler
    {
        const int DefaultTimeout = 500000;

        public async Task<T> SendAsync<T>(XmppXElement el, int timeout = DefaultTimeout)
           where T : XmppXElement
        {
            return await SendAsync<T>(el.ToString(false), timeout);
        }

        public async Task<T> SendAsync<T>(string s, int timeout = DefaultTimeout)
           where T : XmppXElement
        {
            Func<XmppXElement, bool> predicate1 = e => e.OfType<T>();
            return await SendAsync<T>(s, predicate1, null, null, timeout);

        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout = DefaultTimeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            Func<XmppXElement, bool> predicate1 = e => e.OfType<T1>();
            Func<XmppXElement, bool> predicate2 = e => e.OfType<T2>();

            return await SendAsync<XmppXElement>(el.ToString(false), predicate1, predicate2, null, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout = DefaultTimeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
           where T3 : XmppXElement
        {
            Func<XmppXElement, bool> predicate1 = e => e.OfType<T1>();
            Func<XmppXElement, bool> predicate2 = e => e.OfType<T2>();
            Func<XmppXElement, bool> predicate3 = e => e.OfType<T3>();

            return await SendAsync<XmppXElement>(el.ToString(false), predicate1, predicate2, predicate3, timeout);
        }

        internal async Task<XmppXElement> SendAsync<T1, T2>(string s, int timeout = DefaultTimeout)
          where T1 : XmppXElement
          where T2 : XmppXElement
        {
            Func<XmppXElement, bool> predicate1 = e => e.OfType<T1>();
            Func<XmppXElement, bool> predicate2 = e => e.OfType<T2>();
            
            return await SendAsync<XmppXElement>(s, predicate1, predicate2, null, timeout);
        }
        
        private async Task<T> SendAsync<T>(
            string s, 
            Func<XmppXElement, bool> predicate1, 
            Func<XmppXElement, bool> predicate2 = null, 
            Func<XmppXElement, bool> predicate3 = null, 
            int timeout = DefaultTimeout)
           where T : XmppXElement

        {
            var resultCompletionSource = new TaskCompletionSource<T>();

            var action = new Action<IChannelHandlerContext, XmppXElement>(
                (ctx, xel) =>
                {
                    UnSubscribe(predicate1, predicate2, predicate3);
                    resultCompletionSource.SetResult(xel as T);
                });

            Subscribe(predicate1, action);
            Subscribe(predicate2, action);
            Subscribe(predicate3, action);

            await SendAsync(s);

            if (resultCompletionSource.Task ==
                await Task.WhenAny(resultCompletionSource.Task, Task.Delay(timeout)))
                return await resultCompletionSource.Task;

            // timed out, remove this iq from our dictionary
            UnSubscribe(predicate1, predicate2, predicate3);

            resultCompletionSource.SetException(new TimeoutException());

            return await resultCompletionSource.Task;
        }
    }
}
