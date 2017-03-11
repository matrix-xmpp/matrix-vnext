using System;
using System.Collections.Generic;
using System.Reflection;
using Matrix.Attributes;
using Matrix.Crypt;
using System.Threading;
using System.Threading.Tasks;

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
    }
}
