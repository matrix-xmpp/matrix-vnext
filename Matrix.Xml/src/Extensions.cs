using System;
using System.Collections.Generic;
using System.Reflection;

using Matrix.Xml.Attributes;

namespace Matrix.Xml
{
    public static class Extensions
    {

        private static string GetFullyQualifiedEnumName<T>(this T @this) where T : struct
        {
            var type = typeof(T);
            return string.Format("{0}.{1}.{2}", type.Namespace, type.Name, Enum.GetName(type, @this));
        }

        static readonly Dictionary<string, string> EnumsNameAttributeCache = new Dictionary<string, string>();
        internal static string GetName<T>(this T enumerationValue) where T : struct
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
    }
}
