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
    public enum SessionState
    {
        /// <summary>
        /// The socket is disconnected
        /// </summary>
        Disconnected = 0,

        /// <summary>
        /// the socket is Connected
        /// </summary>
        Connected,

        /// <summary>
        /// The socket gets upgraded to TLS
        /// </summary>
        Securing,

        /// <summary>
        /// The socket was updates to TLS successful
        /// </summary>
        Secure,

        /// <summary>
        /// Register new Account in progress
        /// </summary>
        Registering,

        /// <summary>
        /// New account ws greistered
        /// </summary>
        Registered,

        /// <summary>
        /// Authentication is in progress
        /// </summary>
        Authenticating,

        /// <summary>
        /// Session is authenticated
        /// </summary>
        Authenticated,

        /// <summary>
        /// Negotiating stream compression
        /// </summary>
        Compressing,

        /// <summary>
        /// Stream compression is enabled
        /// </summary>
        Compressed,

        /// <summary>
        /// Trying to resume the previous stream
        /// </summary>
        Resume,

        /// <summary>
        /// Stream was resumed
        /// </summary>
        Resumed,

        /// <summary>
        /// Resource binding is in progress
        /// </summary>
        Binding,

        /// <summary>
        /// Resource binding finsihed with success
        /// </summary>
        Binded        
    }
}
