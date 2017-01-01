using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DotNetty.Transport.Channels;

using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;

namespace Matrix.Network.Handlers
{
    public class XmppStanzaHandler : SimpleChannelInboundHandler<XmppXElement>
    {
        protected const int DefaultTimeout = 500000;
        static readonly Dictionary<Func<XmppXElement, bool>, Action<IChannelHandlerContext, XmppXElement>> HandleTypes = new Dictionary<Func<XmppXElement, bool>, Action<IChannelHandlerContext, XmppXElement>>();
      
        private IChannelHandlerContext channelHandlerContext;

        protected XmppStanzaHandler Handle<T>(Action<IChannelHandlerContext, XmppXElement> action) where T : XmppXElement
        {
            return Handle(el => el.OfType<T>(), action);
        }

        protected XmppStanzaHandler Handle(Func<XmppXElement, bool> predicate, Action<IChannelHandlerContext, XmppXElement> action)
        {
            if (predicate == null) return this;
            if (action == null) throw new ArgumentNullException(nameof(action));

            HandleTypes.Add(predicate, action);
            return this;
        }

        protected XmppStanzaHandler UnHandle(Func<XmppXElement, bool> predicate)
        {
            if (predicate == null) return this;

            if (HandleTypes.ContainsKey(predicate))
                HandleTypes.Remove(predicate);

            return this;
        }

        protected XmppStanzaHandler UnHandle(Func<XmppXElement, bool> predicate1, Func<XmppXElement, bool> predicate2)
        {
            return 
                UnHandle(predicate1)
                .UnHandle(predicate2);
        }

        protected XmppStanzaHandler UnHandle(Func<XmppXElement, bool> predicate1, Func<XmppXElement, bool> predicate2, Func<XmppXElement, bool> predicate3)
        {
            return 
                UnHandle(predicate1)
                .UnHandle(predicate2)
                .UnHandle(predicate3);
        }

        protected async Task SendAsync(XmppXElement el)
        {
            await channelHandlerContext.WriteAndFlushAsync(el);
        }

        protected async Task SendAsync(string s)
        {
            await channelHandlerContext.WriteAndFlushAsync(s);
        }

        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);
            channelHandlerContext = context;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, XmppXElement msg)
        {
            var it = HandleTypes.Keys.ToList();
            foreach (var predicate in it)
            {
                if (msg.IsMatch(predicate))
                {
                    if (HandleTypes.ContainsKey(predicate))
                        HandleTypes[predicate].Invoke(ctx, msg);
                }
            }
            //ctx.FireChannelRead(msg);
        }

        #region << SendAsync members >>
        public async Task<T> SendAsync<T>(string s, int timeout = DefaultTimeout)
           where T : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T>();
            return await SendAsync<T>(s, predicate, timeout);
        }

        public async Task<T> SendAsync<T>(XmppXElement el, int timeout = DefaultTimeout)
              where T : XmppXElement
        {
            return await SendAsync<T>(el.ToString(false), timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout = DefaultTimeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>();
            
            return await SendAsync<XmppXElement>(el.ToString(false), predicate, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout = DefaultTimeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
           where T3 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>();
            
            return await SendAsync<XmppXElement>(el.ToString(false), predicate, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(string s, int timeout = DefaultTimeout)
          where T1 : XmppXElement
          where T2 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>();
            
            return await SendAsync<XmppXElement>(s, predicate, timeout);
        }
      

        private async Task<T> SendAsync<T>(
            string s,
            Func<XmppXElement, bool> predicate,
            int timeout = DefaultTimeout)
           where T : XmppXElement

        {
            var resultCompletionSource = new TaskCompletionSource<T>();

            var action = new Action<IChannelHandlerContext, XmppXElement>(
                (ctx, xel) =>
                {
                    UnHandle(predicate);
                    resultCompletionSource.SetResult(xel as T);
                });

            Handle(predicate, action);
            
            await SendAsync(s);

            if (resultCompletionSource.Task ==
                await Task.WhenAny(resultCompletionSource.Task, Task.Delay(timeout)))
                return await resultCompletionSource.Task;

            // timed out, remove this iq from our dictionary
            UnHandle(predicate);

            resultCompletionSource.SetException(new TimeoutException());

            return await resultCompletionSource.Task;
        }
        #endregion

        #region << Send Iq >>
        public async Task<T> SendIqAsync<T>(Iq iq, int timeout = DefaultTimeout)
            where T : Iq
        {
            Func<XmppXElement, bool> predicate = e =>
                e.OfType<T>()
                && e.Cast<T>().Id == iq.Id
                && (e.Cast<T>().Type == IqType.Error || e.Cast<T>().Type == IqType.Result);

            return await SendAsync<T>(iq.ToString(false), predicate, timeout);
        }
        #endregion
    }
}
