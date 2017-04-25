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

namespace Matrix.Xmpp.Bosh
{
    public enum Condition
    {
        None = -1,

        /// <summary>
        /// The format of an HTTP header or binding element received from the client is unacceptable (e.g., syntax error).
        /// </summary>
        [Name("bad-request")]
        BadRequest,
    
        /// <summary>
        /// The target domain specified in the 'to' attribute or the target host or port specified in the 'route' attribute is no longer 
        /// serviced by the connection manager.
        /// </summary>
        [Name("host-gone")]
        HostGone,

        /// <summary>
        /// The target domain specified in the 'to' attribute or the target host or port specified in the 'route' attribute is unknown 
        /// to the connection manager.
        /// </summary>
        [Name("host-unknown")]
        HostUnknown, 
        
        /// <summary>
        /// The initialization element lacks a 'to' or 'route' attribute (or the attribute has no value) but the connection manager requires one.
        /// </summary>
        [Name("improper-addressing")]
        ImproperAddressing,
        
        /// <summary>
        /// The connection manager has experienced an internal error that prevents it from servicing the request.
        /// </summary>
        [Name("internal-server-error")]
        InternalServerError,

        /// <summary>
        /// (1) 'sid' is not valid, 
        /// (2) 'stream' is not valid, 
        /// (3) 'rid' is larger than the upper limit of the expected window, 
        /// (4) connection manager is unable to resend response, 
        /// (5) 'key' sequence is invalid.
        /// </summary>
        [Name("item-not-found")]
        ItemNotFound,

        /// <summary>
        /// Another request being processed at the same time as this request caused the session to terminate.
        /// </summary>
        [Name("other-request")]
        OtherRequest,

        /// <summary>
        /// The client has broken the session rules (polling too frequently, requesting too frequently, sending too many simultaneous requests).
        /// </summary>
        [Name("policy-violation")]
        PolicyViolation,

        /// <summary>
        /// The connection manager was unable to connect to, or unable to connect securely to, or has lost its connection to, the server.
        /// </summary>
        [Name("remote-connection-failed")]
        RemoteConnectionFailed,
        
        /// <summary>
        /// Encapsulates an error in the protocol being transported.
        /// </summary>
        [Name("remote-stream-error")]
        RemoteStreamError,
        
        /// <summary>
        /// The connection manager does not operate at this URI (e.g., the connection manager accepts only SSL or TLS connections at some
        /// https: URI rather than the http: URI requested by the client). The client can try POSTing to the URI in the content of the
        /// &lt;uri/&gt; child element.
        /// </summary>
        [Name("see-other-uri")]
        SeeOtherUri,
        
        /// <summary>
        /// The connection manager is being shut down. All active HTTP sessions are being terminated. No new sessions can be created.
        /// </summary>
        [Name("system-shutdown")]
        SystemShutdown,
        
        /// <summary>
        /// The error is not one of those defined herein; the connection manager SHOULD include application-specific information in the
        /// content of the <body/> wrapper.
        /// </summary>
        [Name("undefined-condition")]
        UndefinedCondition
    }
}
