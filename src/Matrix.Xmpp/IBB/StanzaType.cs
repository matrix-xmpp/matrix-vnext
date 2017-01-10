using Matrix.Core.Attributes;

namespace Matrix.Xmpp.IBB
{
    public enum StanzaType
    {
        [Name("iq")]
        Iq = -1,

        [Name("message")]
        Message,
    }
}