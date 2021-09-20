using System;

namespace Matrix.Xml
{
    public static class Extensions
    {
        /// <summary>
        /// Validates if a XmppXElement matches the given prerdicate
        /// </summary>
        /// <param name="xmppXElement"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool IsMatch(this XmppXElement xmppXElement, Func<XmppXElement, bool> predicate)
        {
            return predicate(xmppXElement);
        }

        /// <summary>
        /// Check if a XmppXElement is of a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmppXElement"></param>
        /// <returns></returns>
        public static bool OfType<T>(this XmppXElement xmppXElement) where T : XmppXElement
        {
            return xmppXElement is T;
        }

        public static bool OfTypeAny<T1, T2>(this XmppXElement xmppXElement) 
            where T1 : XmppXElement
            where T2 : XmppXElement
        {
            return xmppXElement.OfType<T1>() || xmppXElement.OfType<T2>();
        }

        public static bool OfTypeAny<T1, T2, T3>(this XmppXElement xmppXElement)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
        {
            return xmppXElement.OfType<T1>() || xmppXElement.OfType<T2>() || xmppXElement.OfType<T3>();
        }

        public static bool OfTypeAny<T1, T2, T3, T4>(this XmppXElement xmppXElement)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
        {
            return xmppXElement.OfType<T1>() || xmppXElement.OfType<T2>() || xmppXElement.OfType<T3>() || xmppXElement.OfType<T4>();
        }

        public static bool OfTypeAny<T1, T2, T3, T4, T5>(this XmppXElement xmppXElement)
            where T1 : XmppXElement
            where T2 : XmppXElement
            where T3 : XmppXElement
            where T4 : XmppXElement
            where T5 : XmppXElement
        {
            return xmppXElement.OfType<T1>() || xmppXElement.OfType<T2>() || xmppXElement.OfType<T3>() || xmppXElement.OfType<T4>() || xmppXElement.OfType<T5>();
        }

        /// <summary>
        /// Cast a XmppXElement to the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmppXElement"></param>
        /// <returns></returns>
        public static T Cast<T>(this XmppXElement xmppXElement) where T: XmppXElement
        {
            return (T) xmppXElement;
        }
    }
}
