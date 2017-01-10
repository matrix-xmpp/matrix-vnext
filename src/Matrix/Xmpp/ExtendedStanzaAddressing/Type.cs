using Matrix.Attributes;

namespace Matrix.Xmpp.ExtendedStanzaAddressing
{
    public enum Type
    {
        [Name("bcc")]
        Bcc,

        [Name("cc")]
        Cc,

        [Name("noreply")]
        Noreply,

        [Name("replyroom")]
        Replyroom,

        [Name("replyto")]
        Replyto,

        [Name("to")]
        To
    }
}