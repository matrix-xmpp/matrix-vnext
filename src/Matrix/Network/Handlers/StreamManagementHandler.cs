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
using System.Collections.Generic;
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
    using System.Reactive.Subjects;

    [Name("StreamManagement-Handler")]
    public class StreamManagementHandler : XmppStanzaHandler
    {
        private XmppConnection xmppConnection;
        private ISubject<XmppXElement> xmppStanzaAckedSubject = new Subject<XmppXElement>();
        public IObservable<XmppXElement> XmppStanzaAckedObserver => xmppStanzaAckedSubject;
        private readonly Dictionary<long, XmppXElement> unacked = new Dictionary<long, XmppXElement>();
        public readonly StanzaCounter IncomingStanzaCounter = new StanzaCounter();
        public readonly StanzaCounter OutgoingStanzaCounter = new StanzaCounter();

        /// <summary>
        /// Gets or sets a value indicating whether stream management was enabled.
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

        public StreamManagementHandler(XmppConnection xmppCon)
        {
            this.xmppConnection = xmppCon;

            // when we see a stream footer then the stream was closed and cannot be resumed
            xmppConnection
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

            // handle ack replies from server
            Handle(
                el => el.OfType<Answer>(),
                (context, xmppXElement) =>
                {
                    var stanzaId = xmppXElement.Cast<Answer>().LastHandledStanza;
                    if (unacked.ContainsKey(stanzaId))
                    {
                        xmppStanzaAckedSubject.OnNext(unacked[stanzaId]);
                        unacked.Remove(stanzaId);
                    }
                });
        }

        /// <summary>
        /// Enables stream management on the current XMPP stream
        /// </summary>        
        /// <returns></returns>
        internal async Task EnableAsync()
        {
            var res = await SendAsync<Enabled, Failed>(new Enable() { Resume = true });

            if (res.OfType<Enabled>())
            {
                var enabled = res.Cast<Enabled>();
                
                CanResume = enabled.Resume;
                StreamId = enabled.Id;
                IncomingStanzaCounter.Reset();
                OutgoingStanzaCounter.Reset();
                unacked.Clear(); // TODO, let users know about the lost packages

                IsEnabled = true;
            }
        }

        internal async Task ResumeAsync(CancellationToken cancellationToken)
        {
            this.xmppConnection.XmppSessionState.Value = SessionState.Resuming;

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

                xmppConnection.XmppSessionState.Value = SessionState.Resumed;

                // send stanzas
                var toResend = new XmppXElement[unacked.Count];
                unacked.Values.CopyTo(toResend, 0);
                unacked.Clear();
                foreach (var el in toResend)
                {
                    await SendAsync(el);
                }
            }
            else if (res.OfType<Failed>())
            {
                // reset all values to default
                IsEnabled = false;
                StreamId = null;
                CanResume = false;
                IncomingStanzaCounter.Reset();
                OutgoingStanzaCounter.Reset();
                unacked.Clear(); // TODO, let users know about the lost packages

                // set state, so we can continue automatically with resource binding
                this.xmppConnection.XmppSessionState.Value = SessionState.ResumeFailed;
            }
        }

        public override Task WriteAsync(IChannelHandlerContext context, object message)
        {
            if (IsEnabled && message is XmppXElement el)
            {
                if (el.OfTypeAny<Xmpp.Base.Iq, Xmpp.Base.Presence, Xmpp.Base.Message>())
                {
                    lock(OutgoingStanzaCounter)
                    { 
                        // increase outgoing stanza counter
                        OutgoingStanzaCounter.Increment();
                        unacked.Add(OutgoingStanzaCounter.Value, el);
                    }
                }
            }
            return base.WriteAsync(context, message);
        }
    }
}
