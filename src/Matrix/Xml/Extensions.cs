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
