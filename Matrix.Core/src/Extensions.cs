using System;
using System.Collections.Generic;
using System.Text;

namespace Matrix.Core
{
    public static class Extensions
    {
        public static IEnumerable<TResult> ToEnumerale<TResult>(this System.Collections.IEnumerable source) where TResult : struct
        {
            IEnumerable<TResult> enumerable = source as IEnumerable<TResult>;
            if (enumerable != null)
                return enumerable;

            return CastIterator<TResult>(source);
        }

        public static IEnumerable<TResult> CastIterator<TResult>(System.Collections.IEnumerable source)
        {
            foreach (object obj in source) yield return (TResult)obj;
        }
    }
}
