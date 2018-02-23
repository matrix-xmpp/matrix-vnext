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
    using System.Collections.Generic;
    using System.Reflection;
    using Matrix.Attributes;
    using Matrix.Crypt;

    public static class EnumExtensions
    {        
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
    }
}
