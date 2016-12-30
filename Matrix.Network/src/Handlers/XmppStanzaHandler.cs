using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using DotNetty.Transport.Channels;

using Matrix.Xml;

namespace Matrix.Network.Handlers
{
    public class XmppStanzaHandler : SimpleChannelInboundHandler<XmppXElement>
    {
        static readonly Dictionary<Type, Action<IChannelHandlerContext, XmppXElement>> HandleTypes = new Dictionary<Type, Action<IChannelHandlerContext, XmppXElement>>();
      
        private IChannelHandlerContext channelHandlerContext;

        protected XmppStanzaHandler Subscribe<T>(Action<IChannelHandlerContext,XmppXElement> action) where T : XmppXElement
        {
            return Subscribe(typeof(T), action);
        }

        protected XmppStanzaHandler Subscribe(Type t, Action<IChannelHandlerContext, XmppXElement> action)
        {
            if (t == null) return this;

            HandleTypes.Add(t, action);
            return this;
        }

        protected XmppStanzaHandler UnSubscribe<T>() where T : XmppXElement
        {
            return UnSubscribe(typeof (T));
        }

        protected XmppStanzaHandler UnSubscribe<T1, T2>() where T1 : XmppXElement where T2 : XmppXElement
        {
            return UnSubscribe(typeof(T1), typeof(T2));
        }

        protected XmppStanzaHandler UnSubscribe(Type t)
        {
            if (t == null) return this;

            if (HandleTypes.ContainsKey(t))
                HandleTypes.Remove(t);

            return this;
        }

        protected XmppStanzaHandler UnSubscribe(Type t1, Type t2)
        {
            UnSubscribe(t1);
            return UnSubscribe(t2);
        }

        protected XmppStanzaHandler UnSubscribe(Type t1, Type t2, Type t3)
        {
            UnSubscribe(t1);
            UnSubscribe(t2);
            return UnSubscribe(t3);
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
            foreach (var key in it)
            {
                if (key.IsInstanceOfType(msg))
                {
                    if (HandleTypes.ContainsKey(key))
                        HandleTypes[key].Invoke(ctx, msg);
                }
            }

            //ctx.FireChannelRead(msg);
        }
    }
}
