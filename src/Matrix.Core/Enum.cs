using System;
using System.Collections.Generic;

namespace Matrix.Core
{
    public static class Enum
    {
        static readonly Dictionary<Type, Array> EnumValuesCache = new Dictionary<Type, Array>();

        /// <summary>
        /// Parses a sring to an enum member by using the NameAttribute on the Enum
        /// member when present
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns>The enum member</returns>
        public static T ParseUsingNameAttrib<T>(string val) where T : struct
        {
            var values = GetValues<T>().ToEnum<T>();
            foreach (var eVal in values)
            {
                if (val.Equals(eVal.GetName()))
                    return eVal;
            }
            return (T)((object)-1);
        }
        
        /// <summary>
        /// Returns all values of an Enum as Array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Array of enum values</returns>
        public static Array GetValues<T>() where T : struct
        {
            lock (EnumValuesCache)
            {
                var type = typeof(T);

                if (EnumValuesCache.ContainsKey(type))
                    return EnumValuesCache[type];
            
                var enumValues = new List<int>();

                foreach (var eName in System.Enum.GetNames(type))
                    enumValues.Add((int)System.Enum.Parse(type, eName, false));
            
                var ret = enumValues.ToArray();
            
                EnumValuesCache.Add(type, ret);

                return ret;
            }
        }

    }
}