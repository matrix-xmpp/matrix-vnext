/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DotNetty.Transport.Channels;

using Matrix.Xml;
using Matrix.Xmpp;
using Matrix.Xmpp.Base;
using System.Threading;
using System.Net;
using Matrix.Attributes;

namespace Matrix.Network.Handlers
{
    [Name("XmppStanza-Handler")]
    public class XmppStanzaHandler : SimpleChannelInboundHandler<XmppXElement>
    {
        public override bool IsSharable => true;
        
        public static int DefaultTimeout { get; set; } = TimeConstants.TwoMinutes;

        private readonly Dictionary<Func<XmppXElement, bool>, Action<IChannelHandlerContext, XmppXElement>> handleTypes = new Dictionary<Func<XmppXElement, bool>, Action<IChannelHandlerContext, XmppXElement>>();
      
        private IChannelHandlerContext channelHandlerContext;

        protected XmppStanzaHandler Handle<T>(Action<IChannelHandlerContext, XmppXElement> action) where T : XmppXElement
        {
            return Handle(el => el.OfType<T>(), action);
        }

        protected XmppStanzaHandler Handle(Func<XmppXElement, bool> predicate, Action<IChannelHandlerContext, XmppXElement> action)
        {
            if (predicate == null) return this;
            Contract.Requires<ArgumentNullException>(action != null, nameof(action));

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

        public override void ChannelRegistered(IChannelHandlerContext context)
        {
            base.ChannelRegistered(context);
            channelHandlerContext = context;
        }

        public override void ChannelUnregistered(IChannelHandlerContext context)
        {
            base.ChannelUnregistered(context);
            //channelHandlerContext = null;
        }

        public override Task ConnectAsync(IChannelHandlerContext context, EndPoint remoteAddress, EndPoint localAddress)
        {
            return base.ConnectAsync(context, remoteAddress, localAddress);
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

        #region << SendAsync string members >>
        private static readonly ManualResetEvent s_sleepEvent = new ManualResetEvent(false);
        protected async Task SendAsync(string s)
        {
            while (channelHandlerContext == null)
                s_sleepEvent.WaitOne(100);

            await channelHandlerContext.WriteAndFlushAsync(s);
        }

        public async Task<T> SendAsync<T>(string s)
            where T : XmppXElement
        {
            return await SendAsync<T>(s, DefaultTimeout);
        }

        public async Task<T> SendAsync<T>(string s, int timeout)
            where T : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T>();
            return await SendAsync<T>(() => SendAsync(s), predicate, timeout);
        }
        public async Task<T> SendAsync<T>(string s, CancellationToken cancellationToken)
            where T : XmppXElement
        {
            return await SendAsync<T>(s, DefaultTimeout, cancellationToken);
        }

        public async Task<T> SendAsync<T>(string s, int timeout, CancellationToken cancellationToken)
            where T : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T>();
            return await SendAsync<T>(() => SendAsync(s), predicate, timeout, cancellationToken);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(string s)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(s, DefaultTimeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(string s, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(string s, CancellationToken cancellationToken)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(s, DefaultTimeout, cancellationToken);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(string s, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, timeout, cancellationToken);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(string s)
           where T1 : XmppXElement
           where T2 : XmppXElement
           where T3 : XmppXElement
        {
            return await SendAsync<T1, T2, T3>(s, DefaultTimeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(string s, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(string s, CancellationToken cancellationToken)
           where T1 : XmppXElement
           where T2 : XmppXElement
           where T3 : XmppXElement
        {
            return await SendAsync<T1, T2, T3>(s, DefaultTimeout, cancellationToken);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(string s, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, timeout, cancellationToken);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4>(string s, CancellationToken cancellationToken)
           where T1 : XmppXElement
           where T2 : XmppXElement
           where T3 : XmppXElement
           where T4 : XmppXElement
        {
            return await SendAsync<T1, T2, T3, T4>(s, DefaultTimeout, cancellationToken);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4>(string s, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>() || e.OfType<T4>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, timeout, cancellationToken);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4, T5>(string s, CancellationToken cancellationToken)
           where T1 : XmppXElement
           where T2 : XmppXElement
           where T3 : XmppXElement
           where T4 : XmppXElement
           where T5 : XmppXElement
        {
            return await SendAsync<T1, T2, T3, T4, T5>(s, DefaultTimeout, cancellationToken);
        }
        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4, T5>(string s, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
            where T5 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>() || e.OfType<T4>() || e.OfType<T5>();
            return await SendAsync<XmppXElement>(() => SendAsync(s), predicate, timeout, cancellationToken);
        }
        #endregion

        #region << SendAsync XmppXElement members >>
        protected async Task SendAsync(XmppXElement el)
        {
            await channelHandlerContext.WriteAndFlushAsync(el);
        }       

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return await SendAsync<T1, T2, T3>(el, DefaultTimeout, cancellationToken);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout, CancellationToken cancellationToken)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>();
            return await SendAsync<XmppXElement>(() => SendAsync(el), predicate, timeout, cancellationToken);
        }        

        public async Task<T> SendAsync<T>(XmppXElement el)
            where T : XmppXElement
        {
            return await SendAsync<T>(el, DefaultTimeout);
        }

        public async Task<T> SendAsync<T>(XmppXElement el, int timeout)
              where T : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T>();
            return await SendAsync<T>(() => SendAsync(el), predicate, timeout);
        }

        public async Task<T> SendAsync<T>(XmppXElement el, CancellationToken cancellationToken)
             where T : XmppXElement
        {
            return await SendAsync<T>(el, DefaultTimeout, cancellationToken);
        }

        public async Task<T> SendAsync<T>(XmppXElement el, int timeout, CancellationToken cancellationToken)
             where T : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T>();
            return await SendAsync<T>(() => SendAsync(el), predicate, timeout, cancellationToken);
        }

        public async Task<T> SendAsync<T>(XmppXElement el, Func<XmppXElement, bool> predicate, int timeout, CancellationToken cancellationToken)
          where T : XmppXElement
        {            
            return await SendAsync<T>(() => SendAsync(el), predicate, timeout, cancellationToken);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(el, DefaultTimeout);
        }
        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(el, timeout, CancellationToken.None);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, CancellationToken cancellationToken)
          where T1 : XmppXElement
          where T2 : XmppXElement
        {
            return await SendAsync<T1, T2>(el, DefaultTimeout, cancellationToken);
        }

        public async Task<XmppXElement> SendAsync<T1, T2>(XmppXElement el, int timeout, CancellationToken cancellationToken)
           where T1 : XmppXElement
           where T2 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>();
            return await SendAsync<XmppXElement>(() => SendAsync(el), predicate, timeout, cancellationToken);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {            
            return await SendAsync<T1, T2, T3>(el, DefaultTimeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>();
            return await SendAsync<XmppXElement>(() => SendAsync(el.ToString(false)), predicate, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4>(XmppXElement el)
           where T1 : XmppXElement
           where T2 : XmppXElement
           where T3 : XmppXElement
           where T4 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>() || e.OfType<T4>();
            return await SendAsync<XmppXElement>(() => SendAsync(el.ToString(false)), predicate, DefaultTimeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>() || e.OfType<T4>();
            return await SendAsync<XmppXElement>(() => SendAsync(el.ToString(false)), predicate, timeout);
        }

        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4, T5>(XmppXElement el)
           where T1 : XmppXElement
           where T2 : XmppXElement
           where T3 : XmppXElement
           where T4 : XmppXElement
           where T5 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>() || e.OfType<T4>() || e.OfType<T5>();
            return await SendAsync<XmppXElement>(() => SendAsync(el.ToString(false)), predicate, DefaultTimeout);
        }
        public async Task<XmppXElement> SendAsync<T1, T2, T3, T4, T5>(XmppXElement el, int timeout)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
            where T5 : XmppXElement
        {
            Func<XmppXElement, bool> predicate = e => e.OfType<T1>() || e.OfType<T2>() || e.OfType<T3>() || e.OfType<T4>() || e.OfType<T5>();
            return await SendAsync<XmppXElement>( ()=>SendAsync(el.ToString(false)), predicate, timeout);
        }        
        #endregion
       
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
            return await SendAsync<T>(sendTask, predicate, timeout, CancellationToken.None);
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
        public async Task<T> SendAsync<T>(
            Func<Task> sendTask,
            Func<XmppXElement, bool> predicate,
            int timeout,
            CancellationToken cancellationToken            
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


                cancellationToken.ThrowIfCancellationRequested();               
            }
            catch (TaskCanceledException ex)
            {
                exception = ex;
            }
            catch (OperationCanceledException ex)
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
        public async Task<T> SendIqAsync<T>(Iq iq)
            where T : Iq
        {
            return await SendIqAsync<T>(iq, DefaultTimeout);
        }

        public async Task<T> SendIqAsync<T>(Iq iq, int timeout)
            where T : Iq
        {
            return await SendIqAsync<T>(iq, DefaultTimeout, CancellationToken.None);
        }

        public async Task<T> SendIqAsync<T>(Iq iq, int timeout, CancellationToken cancellationToken)
            where T : Iq
        {
            Func<XmppXElement, bool> predicate = e =>
                e.OfType<T>()
                && e.Cast<T>().Id == iq.Id
                && (e.Cast<T>().Type == IqType.Error || e.Cast<T>().Type == IqType.Result);

            return await SendAsync<T>(() => SendAsync(iq), predicate, timeout, cancellationToken);
        }
        #endregion
    }
}
