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
    public static class IntegerExtensions
    {
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
    }
}
