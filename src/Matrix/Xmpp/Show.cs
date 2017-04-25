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

using Matrix.Attributes;

namespace Matrix.Xmpp
{
    /// <summary>
    /// Enumeration that represents the online state.
    /// </summary>
    public enum Show
    {
        /// <summary>
        /// 
        /// </summary>
        None = -1,

        /// <summary>
        /// The entity or resource is temporarily away.
        /// </summary>
        [Name("away")]
        Away,

        /// <summary>
        /// The entity or resource is actively interested in chatting.
        /// </summary>
        [Name("chat")]
        Chat,

        /// <summary>
        /// The entity or resource is busy (dnd = "Do Not Disturb").
        /// </summary>
        [Name("dnd")]
        DoNotDisturb,

        /// <summary>
        /// The entity or resource is away for an extended period (xa = "eXtended Away").
        /// </summary>
        [Name("xa")]
        ExtendedAway,
    }
}
