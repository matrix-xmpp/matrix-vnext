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

namespace Matrix.Xmpp.Muc
{
    /// <summary>
    /// There are four defined roles that an occupant may have
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// the absence of a role
        /// </summary>
        [Name("none")]
        None,
        
        /// <summary>
        /// A moderator is the most powerful occupant within the context of the room, 
        /// and can to some extent manage other occupants's roles in the room.
        /// </summary>
        [Name("moderator")]
        Moderator,
        
        /// <summary>
        /// A participant has fewer privileges than a moderator, although he or she always has the right to speak.
        /// </summary>
        [Name("participant")]
        Participant,
        
        /// <summary>
        /// A visitor is a more restricted role within the context of a moderated room, 
        /// since visitors are not allowed to send messages to all occupants.
        /// </summary>
        [Name("visitor")]
        Visitor
    }
}
