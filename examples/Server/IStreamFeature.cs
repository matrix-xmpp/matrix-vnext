using Matrix.Xmpp.Stream;
using Server.Handlers;

namespace Server
{
    public interface IStreamFeature
    {
        void AddStreamFeatures(ServerConnectionHandler serverSession, StreamFeatures features);
    }
}
