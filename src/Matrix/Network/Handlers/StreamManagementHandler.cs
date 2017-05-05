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
using System.Threading;
using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Attributes;
using Matrix.Xmpp.StreamManagement.Ack;
using Matrix.Xmpp.StreamManagement;
using System.Reactive.Linq;

namespace Matrix.Network.Handlers
{
    [Name("StreamManagement-Handler")]
    public class StreamManagementHandler : XmppStanzaHandler
    {
        private readonly StanzaCounter incomingStanzaCounter = new StanzaCounter();
        private readonly StanzaCounter outgoingStanzaCounter = new StanzaCounter();

        /// <summary>
        /// Observable for the incoming stanza counter
        /// </summary>
        public IObservable<int> IncomingStanzasCountObservable => incomingStanzaCounter.ValueChanged;

        /// <summary>
        /// Observable for the ougoing stanza counter
        /// </summary>
        public IObservable<int> OutgoingStanzasCountObservable => outgoingStanzaCounter.ValueChanged;


        public bool IsEnabled { get; set; } = false;
        public bool Resume { get; set; } = false;

        Func<XmppXElement, bool> predicateIncomingStanzas = el => el.OfType<Iq>() || el.OfType<Presence>() || el.OfType<Message>();
        Func<XmppXElement, bool> predicateRequest = el => el.OfType<Request>();

        #region << EnableAsync >>
        /// <summary>
        /// Enables Streammanagement on the current XMPP stream
        /// </summary>
        /// <param name="streamResumption"></param>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>        
        /// <exception cref="StreamManagementException">Throws a StreamManagementException on failure.</exception>
        /// <returns></returns>
        public async Task<Enabled> EnableAsync(bool streamResumption, int timeout, CancellationToken cancellationToken)
        {
            var res = await SendAsync<Enabled, Failed>(new Enable() { Resume = streamResumption }, timeout, cancellationToken);

            if (res.OfType<Enabled>())
            {
                var enabled = res.Cast<Enabled>();
                Resume = enabled.Resume;
                await DoEnable();
                return enabled;
            }
            else
            {
                throw new StreamManagementException(res);
            }
        }

        /// <summary>
        /// Enables Streammanagement on the current XMPP stream
        /// </summary>
        /// <param name="streamResumption"></param>
        /// <returns></returns>
        public async Task EnableAsync(bool streamResumption = true)
        {
            await EnableAsync(streamResumption, DefaultTimeout, CancellationToken.None);
        }

        /// <summary>
        /// Enables Streammanagement on the current XMPP stream
        /// </summary>
        /// <param name="streamResumption"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task EnableAsync(bool streamResumption, int timeout)
        {
            await EnableAsync(streamResumption, timeout, CancellationToken.None);
        }

        /// <summary>
        /// Enables Streammanagement on the current XMPP stream
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task EnableAsync(int timeout)
        {
            await EnableAsync(true, timeout, CancellationToken.None);
        }

        /// <summary>
        /// Enables Streammanagement on the current XMPP stream
        /// </summary>
        /// <param name="streamResumption"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task EnableAsync(bool streamResumption, CancellationToken cancellationToken)
        {
            await EnableAsync(streamResumption, DefaultTimeout, cancellationToken);
        }

        /// <summary>
        /// Enables Streammanagement on the current XMPP stream
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task EnableAsync(CancellationToken cancellationToken)
        {
            await EnableAsync(true, DefaultTimeout, cancellationToken);
        }
        #endregion

        #region << ResumeAsync >>
        public async Task<Resumed> ResumeAsync(string previousStreamId, int sequenceNumber)
        {
            return await ResumeAsync(previousStreamId, sequenceNumber, DefaultTimeout, CancellationToken.None);
        }

        public async Task<Resumed> ResumeAsync(string previousStreamId, int sequenceNumber, CancellationToken cancellationToken)
        {
            return await ResumeAsync(previousStreamId, sequenceNumber, DefaultTimeout, cancellationToken);
        }

        public async Task<Resumed> ResumeAsync(string previousStreamId, int sequenceNumber, int timeout)
        {
            return await ResumeAsync(previousStreamId, sequenceNumber, timeout, CancellationToken.None);
        }

        public async Task<Resumed> ResumeAsync(string previousStreamId, int sequenceNumber, int timeout, CancellationToken cancellationToken)
        {
            var res = await SendAsync<Resumed, Failed>(
                        new Resume() { PreviousId = previousStreamId, LastHandledStanza = sequenceNumber },
                        timeout,
                        cancellationToken);

            if (res.OfType<Resumed>())
            {
                var resumed = res.Cast<Resumed>();
                return resumed;
            }
            else
            {
                throw new StreamManagementException(res);
            }
        }
        #endregion

        #region << RequestAckAsync >>
        /// <summary>
        /// Requests a StreamManagement ack answer from the server
        /// </summary>        
        /// <exception cref="StreamManagementAckRequestException">
        /// Throws an StreamManagementAckRequestException when the replay does not match the expected value.
        /// </exception>
        /// <returns></returns>
        public async Task<Answer> RequestAckAsync()
        {
            return await RequestAckAsync(DefaultTimeout, CancellationToken.None);
        }

        /// <summary>
        /// Requests a StreamManagement ack answer from the server
        /// </summary>        
        /// <param name="cancellationToken"></param>
        /// <exception cref="StreamManagementAckRequestException">
        /// Throws an StreamManagementAckRequestException when the replay does not match the expected value.
        /// </exception>
        /// <returns></returns>
        public async Task<Answer> RequestAckAsync(CancellationToken cancellationToken)
        {
            return await RequestAckAsync(DefaultTimeout, cancellationToken);
        }

        /// <summary>
        /// Requests a StreamManagement ack answer from the server
        /// </summary>
        /// <param name="timeout"></param>        
        /// <exception cref="StreamManagementAckRequestException">
        /// Throws an StreamManagementAckRequestException when the replay does not match the expected value.
        /// </exception>
        /// <returns></returns>
        public async Task<Answer> RequestAckAsync(int timeout)
        {
            return await RequestAckAsync(timeout, CancellationToken.None);
        }

        /// <summary>
        /// Requests a StreamManagement ack answer from the server
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="StreamManagementAckRequestException">
        /// Throws an StreamManagementAckRequestException when the replay does not match the expected value.
        /// </exception>
        /// <returns></returns>
        public async Task<Answer> RequestAckAsync(int timeout, CancellationToken cancellationToken)
        {
            Func<XmppXElement, bool> predicate = pel => pel.OfType<Answer>();
            var answer = await SendAsync<Answer>(() => SendAsync(new Request()), predicate, timeout, cancellationToken);
            if (answer.LastHandledStanza != outgoingStanzaCounter.Value)
                throw new StreamManagementAckRequestException(answer, $"Expected count {outgoingStanzaCounter.Value}, actual count is {answer.LastHandledStanza}");

            return answer;
        }
        #endregion

        private async Task DoEnable()
        {
            await Task.Run(() =>
            {
                Handle(
                   predicateIncomingStanzas,
                   (context, xmppXElement) =>
                   {
                       // increase incoming stanza counter
                       incomingStanzaCounter.Value++;
                   });


                // automatically reply to StreamManagement requests
                Handle(
                  predicateRequest,
                  async (context, xmppXElement) =>
                  {
                      await SendAsync(new Answer { LastHandledStanza = incomingStanzaCounter.Value });
                  });

                IsEnabled = true;
            });
        }

        private async Task DoDisable()
        {
            await Task.Run(() =>
            {
                UnHandle(predicateRequest);
                UnHandle(predicateIncomingStanzas);
                IsEnabled = false;
                Resume = false;
            });
        }

        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            if (IsEnabled && message is XmppXElement)
            {
                var el = message as XmppXElement;
                if (
                    el.OfType<Iq>()
                    || el.OfType<Presence>()
                    || el.OfType<Message>()
                    )
                {
                    // increase outgoing stanza counter
                    outgoingStanzaCounter.Value++;
                }
            }
            return base.WriteAsync(context, message);
        }
    }
}
