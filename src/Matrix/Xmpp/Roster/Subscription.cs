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

namespace Matrix.Xmpp.Roster
{
    public enum Subscription
    {
        /// <summary>
        /// the user does not have a subscription to the contact's presence information, 
        /// and the contact does not have a subscription to the user's presence information
        /// </summary>
        [Name("none")]
        None,
        
        /// <summary>
        /// the user has a subscription to the contact's presence information, but the contact does 
        /// not have a subscription to the user's presence information
        /// </summary>
        [Name("to")]
        To,
        
        /// <summary>
        /// the contact has a subscription to the user's presence information, but the user does not have a subscription 
        /// to the contact's presence information
        /// </summary>
        [Name("from")]
        From,
        
        /// <summary>
        /// both the user and the contact have subscriptions to each other's presence information
        /// </summary>
        [Name("both")]
        Both,
        
        /// <summary>
        /// for requests to remove the contact from the roster
        /// </summary>
        [Name("remove")]
        Remove
    }
}
