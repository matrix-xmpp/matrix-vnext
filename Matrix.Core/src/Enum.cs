using System;
using System.Collections.Generic;
using System.Reflection;

namespace Matrix.Core
{
    public static class Enum
    {
        static readonly Dictionary<Type, Array> EnumValuesCache = new Dictionary<Type, Array>();

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