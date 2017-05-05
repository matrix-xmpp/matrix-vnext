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

namespace Matrix.Xmpp.Jingle
{
    /// <summary>
    /// The defined conditions ofr the reason element.
    /// </summary>
    public enum Condition
    {
        /// <summary>
        /// unknown condition.
        /// </summary>
        UnknownCondition,
        
        /// <summary>
        /// The party prefers to use an existing session with the peer rather than initiate a new session. 
        /// </summary>
        [Name("alternative-session")]
        AlternativeSession,
        
        /// <summary>
        /// The party is busy and cannot accept a session.
        /// </summary>
        [Name("busy")]
        Busy,

        /// <summary>
        /// The initiator wishes to formally cancel the session initiation request.
        /// </summary>
        [Name("cancel")]
        Cancel,
        
        /// <summary>
        /// The action is related to connectivity problems.
        /// </summary>
        [Name("connectivity-error")]
        ConnectivityError,
        
        /// <summary>
        /// The party wishes to formally decline the session.
        /// </summary>
        [Name("decline")]
        Decline,
        
        /// <summary>
        /// The session length has exceeded a pre-defined time limit (e.g., a meeting hosted at a conference service).
        /// </summary>
        [Name("expired")]
        Expired,
        
        /// <summary>
        /// The party has been unable to initialize processing related to the application type.
        /// </summary>
        [Name("failed-application")]
        FailedApplication,
        
        /// <summary>
        /// The party has been unable to establish connectivity for the transport method.
        /// </summary>
        [Name("failed-transport")]
        FailedTransport,

        /// <summary>
        /// The action is related to a non-specific application error.
        /// </summary>
        [Name("general-error")]
        GeneralError,

        /// <summary>
        /// The entity is going offline or is no longer available.
        /// </summary>
        [Name("gone")]
        Gone,

        /// <summary>
        /// The party supports the offered application type but does not support the offered or negotiated parameters.
        /// </summary>
        [Name("incompatible-parameters")]
        IncompatibleParameters,
        
        /// <summary>
        /// The action is related to media processing problems.
        /// </summary>
        [Name("media-error")]
        MediaError,
        
        /// <summary>
        /// The action is related to a violation of local security policies.
        /// </summary>
        [Name("security-error")]
        SecurityError,
        
        /// <summary>
        /// The action is generated during the normal course of state management and does not reflect any error.
        /// </summary>
        [Name("success")]
        Success,
        
        /// <summary>
        /// A request has not been answered so the sender is timing out the request.
        /// </summary>
        [Name("timeout")]
        Timeout,
        
        /// <summary>
        /// The party supports none of the offered application types.
        /// </summary>
        [Name("unsupported-applications")]
        UnsupportedApplications,
        
        /// <summary>
        /// The party supports none of the offered transport methods.
        /// </summary>
        [Name("unsupported-transports")]
        UnsupportedTransports
    }
}
