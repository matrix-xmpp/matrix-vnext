namespace Matrix
{
    public interface IXmppClient : IClientIqSender, IStanzaSender
    {
        string XmppDomain { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Resource { get; set; }
    }
}
