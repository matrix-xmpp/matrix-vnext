// Copyright (c)  AG-Software. All Rights Reserved.
// by Alexander Gnauck (alex@ag-software.net)

using System;

namespace Matrix.Xml
{
    public static class Extensions
    {
        public static bool IsMatch(this XmppXElement source, Func<XmppXElement, bool> predicate)
        {
            return predicate(source);
        }

        public static bool OfType<T>(this XmppXElement source) where T : XmppXElement
        {
            return source is T;
        }

        public static T Cast<T>(this XmppXElement source) where T: XmppXElement
        {
            return (T) source;
        }
    }
}