// Copyright (c)  AG-Software. All Rights Reserved.
// by Alexander Gnauck (alex@ag-software.net)


using System;
using System.Text;
using System.Threading.Tasks;

using DotNetty.Buffers;
using DotNetty.Common.Internal.Logging;
using DotNetty.Handlers.Logging;
using DotNetty.Transport.Channels;

namespace Matrix.Network.Handlers
{
    public class XmppLoggingHandler : ChannelHandlerAdapter
    {
        const LogLevel DefaultLevel = LogLevel.DEBUG;

        protected readonly InternalLogLevel InternalLevel;
        protected readonly IInternalLogger Logger;


        /// <summary>
        ///     Creates a new instance whose logger name is the fully qualified class
        ///     name of the instance with hex dump enabled.
        /// </summary>
        public XmppLoggingHandler()
            : this(DefaultLevel)
        {
        }

        /// <summary>
        ///     Creates a new instance whose logger name is the fully qualified class
        ///     name of the instance
        /// </summary>
        /// <param name="level">the log level</param>
        public XmppLoggingHandler(LogLevel level)
            : this(typeof(XmppLoggingHandler), level)
        {
        }

        /// <summary>
        ///     Creates a new instance with the specified logger name and with hex dump
        ///     enabled
        /// </summary>
        /// <param name="type">the class type to generate the logger for</param>
        public XmppLoggingHandler(Type type)
            : this(type, DefaultLevel)
        {
        }

        /// <summary>
        ///     Creates a new instance with the specified logger name.
        /// </summary>
        /// <param name="type">the class type to generate the logger for</param>
        /// <param name="level">the log level</param>
        public XmppLoggingHandler(Type type, LogLevel level)
        {
            if (type == null)
            {
                throw new NullReferenceException("type");
            }

            Logger = InternalLoggerFactory.GetInstance(type);
            Level = level;
            InternalLevel = level.ToInternalLevel();
        }

        /// <summary>
        ///     Creates a new instance with the specified logger name using the default log level.
        /// </summary>
        /// <param name="name">the name of the class to use for the logger</param>
        public XmppLoggingHandler(string name)
            : this(name, DefaultLevel)
        {
        }

        /// <summary>
        ///     Creates a new instance with the specified logger name.
        /// </summary>
        /// <param name="name">the name of the class to use for the logger</param>
        /// <param name="level">the log level</param>
        public XmppLoggingHandler(string name, LogLevel level)
        {
            if (name == null)
            {
                throw new NullReferenceException("name");
            }

            Logger = InternalLoggerFactory.GetInstance(name);
            Level = level;
            InternalLevel = level.ToInternalLevel();
        }

        /// <summary>
        ///     Returns the <see cref="LogLevel" /> that this handler uses to log
        /// </summary>
        public LogLevel Level { get; }


        public override void ChannelRead(IChannelHandlerContext ctx, object msg)
        {
            var buffer = msg as IByteBuffer;
            if (buffer != null)
            {
                if (Logger.IsEnabled(InternalLevel))
                {
                    Logger.Log(InternalLevel, FormatByteBuffer("RECV", buffer));
                }
            }
            ctx.FireChannelRead(msg);
        }

        public override Task WriteAsync(IChannelHandlerContext ctx, object msg)
        {
            var buffer = msg as IByteBuffer;
            if (buffer != null)
            {
                if (Logger.IsEnabled(InternalLevel))
                {
                    Logger.Log(InternalLevel, FormatByteBuffer("SEND" , buffer));
                }
            }
            return ctx.WriteAsync(msg);
        }

        string FormatByteBuffer(string eventName, IByteBuffer msg)
        {
            var sbuf = ByteBufferUtil.DecodeString(msg, msg.ReaderIndex, msg.ReadableBytes, Encoding.UTF8);
            return $"{eventName}: {sbuf}";
        }
    }
}