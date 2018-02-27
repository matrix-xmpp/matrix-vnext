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

namespace Matrix
{
    using DotNetty.Buffers;
    using DotNetty.Common.Utilities;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using Matrix.Attributes;
    using System.Reflection;

    public static class DotNettyExtensions
    {       
        /// <summary>
        /// Appends a ChannelHandler at the last position of this pipeline. 
        //  The name of the handler to append is taken from the NameAttribute of the handler class.
        /// </summary>
        /// <param name="channelPipeline"></param>
        /// <param name="name"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IChannelPipeline AddLast2(this IChannelPipeline channelPipeline, IChannelHandler handler)
        {
            var nameAttribute = handler.GetType().GetTypeInfo().GetCustomAttribute<NameAttribute>(false);
            return channelPipeline.AddLast(nameAttribute.Name, handler);
        }

        /// <summary>
        /// Inserts a  ChannelHandler after an existing handler of this pipeline.
        //  The name of the handlers is taken from the NameAttribute of the handler class.        
        /// </summary>
        /// <typeparam name="TAfter"></typeparam>
        /// <param name="channelPipeline"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IChannelPipeline AddAfter<TAfter>(this IChannelPipeline channelPipeline, IChannelHandler handler)
        {
            var name = handler.GetType().GetTypeInfo().GetCustomAttribute<NameAttribute>(false).Name;
            var baseName = typeof(TAfter).GetTypeInfo().GetCustomAttribute<NameAttribute>(false).Name;

            return channelPipeline.AddAfter(baseName, name, handler);
        }

        /// <summary>
        /// Inserts a  ChannelHandler before an existing handler of this pipeline.
        //  The name of the handlers is taken from the NameAttribute of the handler class.        
        /// </summary>
        /// <typeparam name="TBefore"></typeparam>
        /// <param name="channelPipeline"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static IChannelPipeline AddBefore<TBefore>(this IChannelPipeline channelPipeline, IChannelHandler handler)
        {
            var name = handler.GetType().GetTypeInfo().GetCustomAttribute<NameAttribute>(false).Name;
            var baseName = typeof(TBefore).GetTypeInfo().GetCustomAttribute<NameAttribute>(false).Name;

            return channelPipeline.AddBefore(baseName, name, handler);
        }

        public static bool Contains<T>(this IChannelPipeline channelPipeline) where T : class, IChannelHandler
        {
            return channelPipeline.Get<T>() != null;
        }

        /// <summary>
        /// Extensions method for the ToArray() method was removed in DotNetty 0.4.7
        /// </summary>
        /// <param name="buffer">The <see cref="IByteBuffer"/></param>
        /// <returns>The readable bytes of the buffer as array.</returns>
        public static byte[] ReadReadableBytes(this IByteBuffer buffer)
        {
            if (!buffer.IsReadable())
            {
                return ArrayExtensions.ZeroBytes;
            }
            byte[] result = new byte[buffer.ReadableBytes];
            buffer.ReadBytes(result, 0, result.Length);
            return result;
        }

        /// <summary>
        /// Cast a <seealso cref="INameResolver"/> to a given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameResolver"></param>
        /// <returns></returns>
        public static T Cast<T>(this INameResolver nameResolver)
        {
            return (T)nameResolver;
        }
    }
}
