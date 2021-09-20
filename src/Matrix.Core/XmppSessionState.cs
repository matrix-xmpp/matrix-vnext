namespace Matrix
{
    public class XmppSessionState : DistinctBehaviorSubject<SessionState>
    {
        public XmppSessionState(): base(SessionState.Disconnected)
        {
        }
    }
}
