using System;

namespace Matrix
{
    public static class Contract
    {
        public static void Requires<T>(bool result, string message) where T : Exception
        {
            if (!result) throw (T)Activator.CreateInstance(typeof(T), message);
        }
    }
}
