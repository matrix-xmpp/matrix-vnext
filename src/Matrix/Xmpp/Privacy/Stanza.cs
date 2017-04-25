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

namespace Matrix.Xmpp.Privacy
{
    /// <summary>
    /// enum for block or allow communications.
    /// This flags could be combined under the following conditions.
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    ///     <item>All must stand alone, its not allowed to combine thsi flag</item>
    ///     <item>Message, Iq, IncomingPresence and Outgoing Presence could be combined, 
    ///         <b>but</b> its not allowed to combine more than 3 of this flag.
    ///         If you need all of them you have to use the All flag</item>
    /// </list>
    /// </remarks>    
    [Flags]
    public enum Stanza
    {
        /// <summary>
        /// Block all stanzas
        /// !!! Don't combine this flag with others!!!
        /// </summary>
        All = 0,

        /// <summary>
        /// Block messages
        /// </summary>
        Message = 1,

        /// <summary>
        /// Block IQs
        /// </summary>
        Iq = 2,

        /// <summary>
        /// Block Incoming Presences
        /// </summary>
        IncomingPresence = 4,

        /// <summary>
        /// Block Outgoing Presences
        /// </summary>
        OutgoingPresence = 8,
    }
}
