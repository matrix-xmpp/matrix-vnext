using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DotNetty.Transport.Channels;

using Matrix.Xml;

namespace Matrix.Network.Handlers
{
    public class XmppStanzaHandler : SimpleChannelInboundHandler<XmppXElement>
    {
        static readonly Dictionary<Func<XmppXElement, bool>, Action<IChannelHandlerContext, XmppXElement>> HandleTypes = new Dictionary<Func<XmppXElement, bool>, Action<IChannelHandlerContext, XmppXElement>>();
      
        private IChannelHandlerContext channelHandlerContext;

        protected XmppStanzaHandler Subscribe<T>(Action<IChannelHandlerContext, XmppXElement> action) where T : XmppXElement
        {
            return Subscribe(el => el.OfType<T>(), action);
        }

        protected XmppStanzaHandler Subscribe(Func<XmppXElement, bool> predicate, Action<IChannelHandlerContext, XmppXElement> action)
        {
            if (predicate == null) return this;
            if (action == null) throw new ArgumentNullException(nameof(action));

            HandleTypes.Add(predicate, action);
            return this;
        }

        protected XmppStanzaHandler UnSubscribe(Func<XmppXElement, bool> predicate)
        {
            if (predicate == null) return this;

            if (HandleTypes.ContainsKey(predicate))
                HandleTypes.Remove(predicate);

            return this;
        }

        protected XmppStanzaHandler UnSubscribe(Func<XmppXElement, bool> predicate1, Func<XmppXElement, bool> predicate2)
        {
            return 
                UnSubscribe(predicate1)
                .UnSubscribe(predicate2);
        }

        protected XmppStanzaHandler UnSubscribe(Func<XmppXElement, bool> predicate1, Func<XmppXElement, bool> predicate2, Func<XmppXElement, bool> predicate3)
        {
            return 
                UnSubscribe(predicate1)
                .UnSubscribe(predicate2)
                .UnSubscribe(predicate3);
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
    }
}
