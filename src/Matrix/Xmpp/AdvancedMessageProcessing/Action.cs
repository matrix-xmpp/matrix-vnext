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

namespace Matrix.Xmpp.AdvancedMessageProcessing
{
    public enum Action
    {
        None = -1,

        /// <summary>
        /// Namespace: http://jabber.org/protocol/amp?action=drop
        /// Behavior: The message is silently discarded but an alert is returned to the sender.
        /// Defined in XEP-0079: Advanced Message Processing.
        /// </summary>
        Alert,
        
        /// <summary>
        /// Namespace: http://jabber.org/protocol/amp?action=drop
        /// Behavior: The message is silently discarded.
        /// Defined in XEP-0079: Advanced Message Processing.
        /// </summary>
        Drop,

        /// <summary>
        /// Namespace: http://jabber.org/protocol/amp?action=error
        /// Behavior: The message is not processed and an error is returned to the sender, specifying which rule resulted in failed processing.
        /// Defined in XEP-0079: Advanced Message Processing.
        /// </summary>
        Error,
        
        /// <summary>
        /// Namespace: http://jabber.org/protocol/amp?action=notify
        /// Behavior: The message is processed and a notification message is returned to the sender, specifying which rule was processed.
        /// Defined in XEP-0079: Advanced Message Processing.
        /// </summary>
        Notify
    }
}
