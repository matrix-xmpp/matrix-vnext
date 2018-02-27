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

namespace Matrix
{
    using System.Reflection;

    public static class ObjectExtensions
    {
        /// <summary>
        /// Check if an interface is implemented on a <see cref="Type"/>
        /// </summary>
        /// <typeparam name="I">The interface</typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool Implements<I>(this object source) where I : class
        {
            return typeof(I).IsAssignableFrom(source.GetType());
        }
    }
}
