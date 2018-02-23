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
    using System.Threading;
    using System.Threading.Tasks;

    using Matrix.Xmpp.Client;

    public interface IClientIqSender
    {
        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <returns>The server response Iq</returns>
        Task<Iq> SendIqAsync(Iq iq);

        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="timeout"></param>
        /// <returns>The server response Iq</returns>
        Task<Iq> SendIqAsync(Iq iq, int timeout);

        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The server response Iq</returns>
        Task<Iq> SendIqAsync(Iq iq, CancellationToken cancellationToken);
        
        /// <summary>
        /// Send an Iq asynchronous to the server
        /// </summary>
        /// <param name="iq"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>The server response Iq</returns>
        Task<Iq> SendIqAsync(Iq iq, int timeout, CancellationToken cancellationToken);        
    }
}
