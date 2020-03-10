/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

using System.Threading;
using System.Threading.Tasks;
using Matrix.Network.Handlers;
using Matrix.Xmpp.StreamManagement.Ack;

namespace Matrix.Extensions.Client.StreamManagement
{
    public static class StreamManagementExtensions
    {
        /// <summary>
        /// Requests a StreamManagement ack answer from the server
        /// </summary>
        /// <param name="xmppClient">The XmppClient</param>
        /// <exception cref="StreamManagementAckRequestException">
        /// Throws an StreamManagementAckRequestException when the replay does not match the expected value.
        /// </exception>
        /// <returns></returns>
        public static async Task<Answer> RequestStreamManagementAckAsync(this XmppClient xmppClient)
        {
            return await xmppClient.Pipeline.Get<StreamManagementHandler>().RequestAckAsync() ;
        }

        /// <summary>
        /// Requests a StreamManagement ack answer from the server
        /// </summary>
        /// <param name="xmppClient">The XmppClient</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="StreamManagementAckRequestException">
        /// Throws an StreamManagementAckRequestException when the replay does not match the expected value.
        /// </exception>
        /// <returns></returns>
        public static async Task<Answer> RequestStreamManagementAckAsync(this XmppClient xmppClient, CancellationToken cancellationToken)
        {
            return await xmppClient.Pipeline.Get<StreamManagementHandler>().RequestAckAsync(cancellationToken) ;
        }

        /// <summary>
        /// Requests a StreamManagement ack answer from the server
        /// </summary>
        /// <param name="xmppClient">The XmppClient</param>
        /// <param name="timeout"></param>
        /// <exception cref="StreamManagementAckRequestException">
        /// Throws an StreamManagementAckRequestException when the replay does not match the expected value.
        /// </exception>
        /// <returns></returns>
        public static async Task<Answer> RequestStreamManagementAckAsync(this XmppClient xmppClient, int timeout)
        {
            return await xmppClient.Pipeline.Get<StreamManagementHandler>().RequestAckAsync(timeout);
        }


        /// <summary>
        /// Requests a StreamManagement ack answer from the server
        /// </summary>
        /// <param name="xmppClient">The XmppClient</param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="StreamManagementAckRequestException">
        /// Throws an StreamManagementAckRequestException when the replay does not match the expected value.
        /// </exception>
        /// <returns></returns>
        public static async Task<Answer> RequestStreamManagementAckAsync(this XmppClient xmppClient, int timeout, CancellationToken cancellationToken)
        {
            return await xmppClient.Pipeline.Get<StreamManagementHandler>().RequestAckAsync(timeout, cancellationToken);
        }
    }
}
