namespace Matrix.Xmpp.Stream.Features
{
    public class MessageArchiving : StreamFeature
    {
        public MessageArchiving() : base(Namespaces.Archiving, "feature")
        {
        }
    }
}