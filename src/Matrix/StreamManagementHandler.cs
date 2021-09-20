namespace Matrix
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading;
    using System.Threading.Tasks;
    using Xml;
    using Xmpp.Client;
    using Xmpp.Stream;
    using Xmpp.StreamManagement;
    using Xmpp.StreamManagement.Ack;

    public class StreamManagementHandler : XmppHandler, IStreamManagementHandler
    {
        private ISubject<XmppXElement> xmppStanzaAckedSubject = new Subject<XmppXElement>();
        private readonly Dictionary<long, XmppXElement> unacked = new Dictionary<long, XmppXElement>();

        public IObservable<XmppXElement> XmppStanzaAckedObserver => xmppStanzaAckedSubject;
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

        public StreamManagementHandler(XmppConnection xmppConnection) : base(xmppConnection)
        {
            // when we see a stream footer then the stream was closed and cannot be resumed
            XmppConnection
                .Transport
                   .StateChanged
                   .Where(s => s == Transport.State.StreamFooterSent || s == Transport.State.StreamFooterReceived)
                   .Subscribe(s =>
                   {
                       IsEnabled = true;
                       StreamId = null;
                       CanResume = false;
                   });

            // look in stream features for support of stream management
            XmppConnection
                .XmppXElementReceived
                .Where
                (
                    el =>
                        el.OfType<StreamFeatures>()
                        && el.Cast<StreamFeatures>().SupportsStreamManagement)
                .Subscribe(el =>
                {
                    Supported = true;
                });

            XmppConnection
                .XmppXElementReceived
                .Where
                (el => el.OfTypeAny<Iq, Presence, Message>())
                .Subscribe(el =>
                {
                    // increase incoming stanza counter
                    if (IsEnabled)
                    {
                        IncomingStanzaCounter.Increment();
                    }
                });

            // automatically reply to StreamManagement requests
            XmppConnection
                .XmppXElementReceived
                .Where
                (
                    el => el.OfType<Request>()
                )
                .Subscribe(async el =>
                {
                    if (IsEnabled)
                    {
                        await XmppConnection.SendAsync(new Answer {LastHandledStanza = IncomingStanzaCounter.Value}).ConfigureAwait(false);
                    }
                });

            // handle ack replies from server
            XmppConnection
                .XmppXElementReceived
                .Where(
                    el => el.OfType<Answer>()
                )
                .Subscribe(el =>
                {
                    var stanzaId = el.Cast<Answer>().LastHandledStanza;
                    if (unacked.ContainsKey(stanzaId))
                    {
                        xmppStanzaAckedSubject.OnNext(unacked[stanzaId]);
                        unacked.Remove(stanzaId);
                    }
                });

            XmppConnection.Transport
                .BeforeXmlSent
                .Where(
                    el => IsEnabled && el.OfTypeAny<Xmpp.Base.Iq, Xmpp.Base.Presence, Xmpp.Base.Message>()
                )
                .Subscribe(el =>
                {
                    // increase outgoing stanza counter
                    OutgoingStanzaCounter.Increment();
                    unacked.Add(OutgoingStanzaCounter.Value, el);
                });

            XmppConnection.Transport
                .XmlSent
                .Where(
                    el => IsEnabled && el.OfTypeAny<Xmpp.Base.Iq, Xmpp.Base.Presence, Xmpp.Base.Message>()
                )
                .Subscribe(async el =>
                {
                    await XmppConnection.SendAsync(new Request()).ConfigureAwait(false);
                });
        }

        /// <summary>
        /// Enables stream management on the current XMPP stream
        /// </summary>        
        /// <returns></returns>
        public async Task EnableAsync()
        {
            var res = await XmppConnection.SendAsync<Enabled, Failed>(new Enable() { Resume = true }).ConfigureAwait(false);

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

        public async Task ResumeAsync(CancellationToken cancellationToken)
        {
            XmppConnection.XmppSessionStateSubject.Value = SessionState.Resuming;

            var res = await XmppConnection.SendAsync<Resumed, Failed>(
                        new Resume()
                        {
                            PreviousId = this.StreamId,
                            LastHandledStanza = IncomingStanzaCounter.Value
                        }
                        , cancellationToken
                      ).ConfigureAwait(false);

            if (res.OfType<Resumed>())
            {
                var resumed = res.Cast<Resumed>();

                OutgoingStanzaCounter.Value = resumed.LastHandledStanza;
                StreamId = resumed.PreviousId;

                XmppConnection.XmppSessionStateSubject.Value = SessionState.Resumed;

                // send stanzas
                var toResend = new XmppXElement[unacked.Count];
                unacked.Values.CopyTo(toResend, 0);
                unacked.Clear();
                foreach (var el in toResend)
                {
                    await XmppConnection.SendAsync(el).ConfigureAwait(false);
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
                XmppConnection.XmppSessionStateSubject.Value = SessionState.ResumeFailed;
            }
        }

    }
}

