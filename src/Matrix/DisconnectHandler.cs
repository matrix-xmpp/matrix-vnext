namespace Matrix
{
    using System;
    using Transport;

    public class DisconnectHandler : XmppHandler
    {
        public DisconnectHandler(XmppConnection xmppConnection)
            : base(xmppConnection)
        {
            bool connected = false;

            xmppConnection
                .Transport
                .StateChanged
                .Subscribe(state =>
                {
                    switch (state)
                    {
                        case State.Connected:
                            connected = true;
                            break;
                        case State.Disconnected:
                            if (connected)
                            {                                
                                xmppConnection.XmppSessionStateSubject.Value = SessionState.Disconnected;
                                connected = false;
                            }                            
                            break;
                    }
                });
        }
    }
}
