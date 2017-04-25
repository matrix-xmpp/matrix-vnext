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
using System.Reflection;
using Matrix.Attributes;
using Matrix.Crypt;
using System.Threading;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;

namespace Matrix
{
    public static class Extensions
    {
        /// <summary>
        /// Converts all bytes in the array to a HEX string representation.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string representation</returns>
        public static string ToHex(this byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty).ToLower();
        }

        #region << Enum extensions >>
        static readonly Dictionary<string, string> EnumsNameAttributeCache = new Dictionary<string, string>();

        /// <summary>
        /// Gets the value of the <see cref="T:Matrix.Attributes.NameAttribute"/> on an struct, including enums.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerationValue">The enumeration value.</param>
        /// <returns></returns>
        public static string GetName<T>(this T enumerationValue) where T : struct
        {
            lock (EnumsNameAttributeCache)
            {
                var fqn = enumerationValue.GetFullyQualifiedEnumName();
                if (EnumsNameAttributeCache.ContainsKey(fqn))
                    return EnumsNameAttributeCache[fqn];


                var nameAttribute = enumerationValue
                        .GetType()
                        .GetTypeInfo()
                        .GetDeclaredField(enumerationValue.ToString())
                        .GetCustomAttribute<NameAttribute>();

                var ret = nameAttribute == null ? enumerationValue.ToString() : nameAttribute.Name;

                // add to the cache
                if (!EnumsNameAttributeCache.ContainsKey(fqn))
                    EnumsNameAttributeCache.Add(fqn, ret);

                return ret;
            }
        }
           
        public static IEnumerable<TResult> CastIterator<TResult>(System.Collections.IEnumerable source)
        {
            foreach (object obj in source) yield return (TResult) obj;
        }

        private static string GetFullyQualifiedEnumName<T>(this T enumeration) where T : struct
        {
            var type = typeof(T);
            return $"{type.Namespace}.{type.Name}.{System.Enum.GetName(type, enumeration)}";
        }

        public static string ToName(this HashAlgorithms hashs)
        {
            return hashs.GetName();
        }

        // LinQ Cast in CF throws an exception when trying to cast from int32 to an enum
        // no idea why, but this fixes it.
        public static IEnumerable<TResult> ToEnum<TResult>(this System.Collections.IEnumerable source)
            where TResult : struct
        {
            IEnumerable<TResult> enumerable = source as IEnumerable<TResult>;
            if (enumerable != null)
                return enumerable;

            return CastIterator<TResult>(source);
        }
        #endregion

        #region << Number extensions >>
        /// <summary>
        /// Determines whether an integer value is in the given range between min and max values.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The allowed minimum.</param>
        /// <param name="max">The allowed maximum.</param>
        /// <returns></returns>
        public static bool IsInRange(this int value, int min, int max)
        {
            return (value >= min && value <= max);
        }
        #endregion

        #region << TaskExtensions >>
        public static Task WhenCanceled(this CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            return tcs.Task;
        }
        #endregion

        #region << DotNetty extensions >>
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
        #endregion
    }
}
