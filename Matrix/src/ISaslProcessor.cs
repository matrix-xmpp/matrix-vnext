using System.Threading.Tasks;
using Matrix.Xml;

namespace Matrix
{
    public interface ISaslProcessor
    {
        Task<XmppXElement> AuthenticateClientAsync(XmppClient xmppClient);
    }
}
