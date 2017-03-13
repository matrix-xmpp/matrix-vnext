using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DotNetty.Transport.Channels;

using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;
using System.Threading;

namespace Matrix.Network.Handlers
{
    public class XmppStanzaHandler : SimpleChannelInboundHandler<XmppXElement>
    {
        public const int DefaultTimeout = TimeConstants.TwoMinutes;

        private readonly Dictionary<Func<XmppXElement, bool>, Action<IChannelHandlerContext, XmppXElement>> handleTypes = new Dictionary<Func<XmppXElement, bool>, Action<IChannelHandlerContext, XmppXElement>>();
      
        private IChannelHandlerContext channelHandlerContext;

        protected XmppStanzaHandler Handle<T>(Action<IChannelHandlerContext, XmppXElement> action) where T : XmppXElement
        {
            return Handle(el => el.OfType<T>(), action);
        }

        protected XmppStanzaHandler Handle(Func<XmppXElement, bool> predicate, Action<IChannelHandlerContext, XmppXElement> action)
        {
            if (predicate == null) return this;
            if (action == null) throw new ArgumentNullException(nameof(action));

            handleTypes.Add(predicate, action);
            return this;
        }

        protected XmppStanzaHandler UnHandle(Func<XmppXElement, bool> predicate)
        {
            if (predicate == null) return this;

            if (handleTypes.ContainsKey(predicate))
                handleTypes.Remove(predicate);

            return this;
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
            var it = handleTypes.Keys.ToList();
            foreach (var predicate in it)
            {
                if (msg.IsMatch(predicate))
                {
                    if (handleTypes.ContainsKey(predicate))
                        handleTypes[predicate].Invoke(ctx, msg);
                }
            }

            ctx.FireChannelRead(msg);
        }

        #region << SendAsync members >>
        public async Task<T> SendAsync<T>(string s, int timeout = DefaultTimeout)
           where T : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T>();
            return await SendAsync<T>(() => SendAsync(s), predicate, timeout);
        }

        public async Task<T> SendAsync<T>(string s, CancellationToken cancellationToken, int timeout = DefaultTimeout)
           where T : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T>();
            return await SendAsync<T>(() => SendAsync(s), predicate, cancellationToken, timeout);
        }
               

        public async Task<XmppXElement> SendAsync<T1, T2>(string s, int timeout = DefaultTimeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(string s, CancellationToken cancellationToken, int timeout = DefaultTimeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, cancellationToken, timeout);
        }       

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(string s, int timeout = DefaultTimeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, CancellationToken cancellationToken, int timeout = DefaultTimeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>();
            return await SendAsync<XmppXElement>(() => SendAsync(el), predicate, cancellationToken, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(string s, CancellationToken cancellationToken, int timeout = DefaultTimeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, cancellationToken, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4>(string s, CancellationToken cancellationToken, int timeout = DefaultTimeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>() || e.OfType<T4>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, cancellationToken, timeout);
        }


        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4, T5>(string s, CancellationToken cancellationToken, int timeout = DefaultTimeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
            where T5 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>() || e.OfType<T4>() || e.OfType<T5>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, cancellationToken, timeout);
        }

        public async Task<T> SendAsync<T>(XmppXElement el, int timeout = DefaultTimeout)
              where T : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T>();
            return await SendAsync<T>(() => SendAsync(el), predicate, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout = DefaultTimeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(el, CancellationToken.None, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, CancellationToken cancellationToken, int timeout = DefaultTimeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>();
            return await SendAsync<XmppXElement>(() => SendAsync(el), predicate, cancellationToken, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout = DefaultTimeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>();
            return await SendAsync<XmppXElement>(() => SendAsync(el.ToString(false)), predicate, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4>(XmppXElement el, int timeout = DefaultTimeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>() || e.OfType<T4>();
            return await SendAsync<XmppXElement>(() => SendAsync(el.ToString(false)), predicate, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4, T5>(XmppXElement el, int timeout = DefaultTimeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
            where T5 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>() || e.OfType<T4>() || e.OfType<T5>();
            return await SendAsync<XmppXElement>( ()=>SendAsync(el.ToString(false)), predicate, timeout);
        }

        //private async Task<T> SendAsync<T>(
        //      Func<Task> sendTask,
        //      Func<XmppXElement, bool> predicate,
        //      int timeout = DefaultTimeout)
        //     where T : XmppXElement
        //{
        //    return await SendAsync<T>(sendTask, predicate, timeout, CancellationToken.None);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sendTask"></param>
        /// <param name="predicate"></param>
        /// <param name="timeout"></param>
        /// <exception cref="TimeoutException">Throws a TimeoutException when no response gets received within the given timeout.</exception>
        /// <returns></returns>
        private async Task<T> SendAsync<T>(
            Func<Task> sendTask,
            Func<XmppXElement, bool> predicate,
            int timeout)
            where T : XmppXElement

        {
            return await SendAsync<T>(sendTask, predicate, CancellationToken.None, timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sendTask"></param>
        /// <param name="predicate"></param>
        /// <param name="timeout"></param>
        /// <exception cref="TimeoutException">Throws a TimeoutException when no response gets received within the given timeout.</exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <returns></returns>
        private async Task<T> SendAsync<T>(
            Func<Task> sendTask,
            Func<XmppXElement, bool> predicate,
            CancellationToken cancellationToken,
            int timeout
        )
           where T : XmppXElement

        {
            Exception exception = null;
            var resultCompletionSource = new TaskCompletionSource<T>();

            var action = new Action<IChannelHandlerContext, XmppXElement>(
                (ctx, xel) =>
                {
                    UnHandle(predicate);
                    resultCompletionSource.SetResult(xel as T);
                });

            Handle(predicate, action);

            await sendTask();

            try
            {
                if (resultCompletionSource.Task ==
                await Task.WhenAny(resultCompletionSource.Task, Task.Delay(timeout, cancellationToken)))
                    return await resultCompletionSource.Task;
            }
            catch (TaskCanceledException ex)
            {
                exception = ex;
            }

            // timed out, remove this iq from our dictionary
            if (exception == null)
                exception = new TimeoutException();
            
            UnHandle(predicate);
            resultCompletionSource.SetException(exception);

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

            return await SendAsync<T>(() => SendAsync(iq), predicate, timeout);
        }

        #endregion
    }
}
