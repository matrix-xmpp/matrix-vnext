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
    /// Enumeration that represents the type of a message
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// This in a normal message, much like an email. You don't expect a fast reply.
        /// </summary>
        [Name("normal")]
        Normal = -1,

        /// <summary>
        /// a error messages
        /// </summary>
        [Name("error")]
        Error,

        /// <summary>
        /// is for chat like messages, person to person. Send this if you expect a fast reply. reply or no reply at all.
        /// </summary>
        [Name("chat")]
        Chat,

        /// <summary>
        /// is used for sending/receiving messages from/to a chatroom (IRC style chats) 
        /// </summary>
        [Name("groupchat")]
        GroupChat,

        /// <summary>
        /// Think of this as a news broadcast, or RRS Feed, the message will normally have a URL and Description Associated with it.
        /// </summary>
        [Name("headline")]
        Headline
    }
}
