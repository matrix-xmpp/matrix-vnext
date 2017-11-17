namespace ConsoleClient
{
    using System.Text;
    using System.Threading.Tasks;
    using DotNetty.Buffers;
    using DotNetty.Transport.Channels;
    using Microsoft.Extensions.Logging;

    /// <summary>
    ///     A <see cref="IChannelHandler" /> that logs all events using a logging framework.
    ///     By default, all events are logged at <tt>DEBUG</tt> level.
    /// </summary>
    public class MyLoggingHandler : ChannelHandlerAdapter
    {
        private ILogger logger;

        /// <summary>
        ///     Creates a new instance whose logger name is the fully qualified class
        ///     name of the instance with hex dump enabled.
        /// </summary>
        public MyLoggingHandler(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        ///     Returns the <see cref="LogLevel" /> that this handler uses to log
        /// </summary>
        public LogLevel Level { get; }


        public override void ChannelRead(IChannelHandlerContext ctx, object message)
        {
            this.logger.LogInformation(this.Format(ctx, "RECV", message));
            ctx.FireChannelRead(message);
        }

        public override Task WriteAsync(IChannelHandlerContext ctx, object msg)
        {
            this.logger.LogInformation(this.Format(ctx, "SEND", msg));
            return ctx.WriteAsync(msg);
        }

        /// <summary>
        ///     Formats an event and returns the formatted message
        /// </summary>
        /// <param name="eventName">the name of the event</param>
        protected virtual string Format(IChannelHandlerContext ctx, string eventName)
        {
            string chStr = ctx.Channel.ToString();
            return new StringBuilder(chStr.Length + 1 + eventName.Length)
                .Append(chStr)
                .Append(' ')
                .Append(eventName)
                .ToString();
        }

        /// <summary>
        ///     Formats an event and returns the formatted message.
        /// </summary>
        /// <param name="eventName">the name of the event</param>
        /// <param name="arg">the argument of the event</param>
        protected virtual string Format(IChannelHandlerContext ctx, string eventName, object arg)
        {
            if (arg is IByteBuffer)
            {
                return this.FormatByteBuffer(ctx, eventName, (IByteBuffer)arg);
            }
            else
            {
                return this.FormatSimple(ctx, eventName, arg);
            }
        }

        /// <summary>
        ///     Generates the default log message of the specified event whose argument is a  <see cref="IByteBuffer" />.
        /// </summary>
        string FormatByteBuffer(IChannelHandlerContext ctx, string eventName, IByteBuffer msg)
        {
            string chStr = ctx.Channel.ToString();
            int length = msg.ReadableBytes;
            if (length == 0)
            {
                var buf = new StringBuilder(eventName.Length + 2);
                buf.Append(eventName).Append(": ");
                return buf.ToString();
            }
            else
            {
                var buf = new StringBuilder(eventName.Length + 2 + msg.ReadableBytes);

                byte[] resBuf = new byte[msg.ReadableBytes];
                msg.GetBytes(0, resBuf, 0, msg.ReadableBytes);
                var utf8String = Encoding.UTF8.GetString(resBuf);                
                buf.Append(eventName).Append(": ").Append(utf8String);

                return buf.ToString();
            }
        }

        /// <summary>
        ///     Generates the default log message of the specified event whose argument is an arbitrary object.
        /// </summary>
        string FormatSimple(IChannelHandlerContext ctx, string eventName, object msg)
        {
            string chStr = ctx.Channel.ToString();
            string msgStr = msg.ToString();
            var buf = new StringBuilder(chStr.Length + 1 + eventName.Length + 2 + msgStr.Length);
            return buf.Append(chStr).Append(' ').Append(eventName).Append(": ").Append(msgStr).ToString();
        }
    }
}
