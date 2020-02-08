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

using System;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

using DotNetty.Transport.Channels;

using Matrix.Attributes;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.StreamManagement.Ack;
using Matrix.Xmpp.StreamManagement;
using Matrix.Xmpp.Stream;

namespace Matrix.Network.Handlers
{
    [Name("StreamManagement-Handler")]
    public class StreamManagementHandler : XmppStanzaHandler
    {
        public readonly StanzaCounter IncomingStanzaCounter = new StanzaCounter();
        public readonly StanzaCounter OutgoingStanzaCounter = new StanzaCounter();

        /// <summary>
        /// Observable for the incoming stanza counter
        /// </summary>
        public IObservable<long> IncomingStanzasCountObservable => IncomingStanzaCounter.ValueChanged;

        /// <summary>
        /// Observable for the ougoing stanza counter
        /// </summary>
        public IObservable<long> OutgoingStanzasCountObservable => OutgoingStanzaCounter.ValueChanged;

        /// <summary>
        /// Gets or sets a value indicating whether stream manaegment was enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the stream can be resumed or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if resume; otherwise, <c>false</c>.
        /// </value>
        public bool CanResume { get; set; } = false;

        /// <summary>
        /// Gets a value indicating whether Stream Management is supported.
        /// </summary>
        /// <value>
        ///   <c>true</c> if supported; otherwise, <c>false</c>.
        /// </value>
        public bool Supported { get; private set; } = false;

        public string StreamId { get; set; } = null;

        private XmppClient xmppClient;

        public StreamManagementHandler(XmppClient xmppClient)
        {
            this.xmppClient = xmppClient;

            this.xmppClient
                    .XmppSessionState
                    .ValueChanged
                    .Where(s => s == SessionState.Binded)
                    .Subscribe(async s =>
                    {
                        if (Supported)
                        {
                            await EnableAsync();
                        }
                    });

            // wen we see a stream footer then the stream was closed and cannot be resumed
            this.xmppClient
                   .XmppSessionEvent
                   .ValueChanged
                   .Where(s => s == SessionEvent.StreamFooterSent || s == SessionEvent.StreamFooterReceived)
                   .Subscribe(s =>
                   {
                       IsEnabled = true;
                       StreamId = null;
                       CanResume = false;
                   });

            // look in stream features for support of stream management
            Handle(
               el =>
                   el.OfType<StreamFeatures>()
                   && el.Cast<StreamFeatures>().SupportsStreamManagement,
                (context, xmppXElement) =>
                {
                    Supported = true;
                });

            //Handle(
            //    el =>
            //        el.OfType<StreamFeatures>()
            //        && xmppClient.XmppSessionState.Value == SessionState.Authenticated
            //        && CanResume
            //        && StreamId != null,
            //      async (context, xmppXElement) =>
            //      {
            //          await ResumeAsync(xmppXElement as StreamFeatures);
            //      });

            Handle(
                el => el.OfType<Iq>()
                    || el.OfType<Presence>()
                    || el.OfType<Message>(),
                   (context, xmppXElement) =>
                   {
                       // increase incoming stanza counter
                       if (IsEnabled)
                       {
                           IncomingStanzaCounter.Increment();
                       }
                   });

            // automatically reply to StreamManagement requests
            Handle(
                el => el.OfType<Request>(),
                  async (context, xmppXElement) =>
                  {
                      if (IsEnabled)
                      {
                          await SendAsync(new Answer { LastHandledStanza = IncomingStanzaCounter.Value });
                      }
                  });
        }

        /// <summary>
        /// Enables Streammanagement on the current XMPP stream
        /// </summary>        
        /// <returns></returns>
        private async Task EnableAsync()
        {
            var res = await SendAsync<Enabled, Failed>(new Enable() { Resume = true });

            if (res.OfType<Enabled>())
            {
                var enabled = res.Cast<Enabled>();
                this.CanResume = enabled.Resume;
                this.StreamId = enabled.Id;

                IsEnabled = true;
            }
        }

        public async Task ResumeAsync(CancellationToken cancellationToken)
        {
            this.xmppClient.XmppSessionState.Value = SessionState.Resuming;

            var res = await SendAsync<Resumed, Failed>(
                        new Resume()
                        {
                            PreviousId = this.StreamId,
                            LastHandledStanza = IncomingStanzaCounter.Value
                        }
                        ,cancellationToken
                      );

            if (res.OfType<Resumed>())
            {
                var resumed = res.Cast<Resumed>();

                OutgoingStanzaCounter.Value = resumed.LastHandledStanza;
                StreamId = resumed.PreviousId;

                this.xmppClient.XmppSessionState.Value = SessionState.Resumed;
            }
            else if (res.OfType<Failed>())
            {
                // reset all values to default
                IsEnabled = false;
                StreamId = null;
                CanResume = false;
                IncomingStanzaCounter.Reset();
                OutgoingStanzaCounter.Reset();

                // set state, so we can continue automatically with resource binding
                this.xmppClient.XmppSessionState.Value = SessionState.ResumeFailed;
            }
        }

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
            if (answer.LastHandledStanza != OutgoingStanzaCounter.Value)
                throw new StreamManagementAckRequestException(answer, $"Expected count {OutgoingStanzaCounter.Value}, actual count is {answer.LastHandledStanza}");

            return answer;
        }
        #endregion

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
                    OutgoingStanzaCounter.Increment();
                }
            }
            return base.WriteAsync(context, message);
        }
    }
}
