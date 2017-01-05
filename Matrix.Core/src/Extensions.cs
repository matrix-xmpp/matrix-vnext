using System;
using System.Collections.Generic;
using System.Reflection;
using Matrix.Core.Attributes;
using Matrix.Core.Crypt;

namespace Matrix.Core
{
    public static class Extensions
    {
        /// <summary>
        /// Converts all bytes in the Array to a string representation.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>string representation</returns>
        public static string ToHex(this byte[] data)
        {
            return BitConverter.ToString(data).Replace("-", string.Empty).ToLower();
        }

        //public static IEnumerable<TResult> ToEnumerale<TResult>(this System.Collections.IEnumerable source)
        //    where TResult : struct
        //{
        //    IEnumerable<TResult> enumerable = source as IEnumerable<TResult>;
        //    if (enumerable != null)
        //        return enumerable;

        //    return CastIterator<TResult>(source);
        //}

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

        private static string GetFullyQualifiedEnumName<T>(this T @this) where T : struct
        {
            var type = typeof(T);
            return string.Format("{0}.{1}.{2}", type.Namespace, type.Name, System.Enum.GetName(type, @this));
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
