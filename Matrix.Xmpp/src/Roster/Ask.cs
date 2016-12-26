using Matrix.Core.Attributes;

namespace Matrix.Xmpp.Roster
{
    public enum Ask
    {
        None = -1,

        [Name("subscribe")]
        Subscribe,
        
        [Name("unsubscribe")]
        Unsubscribe
    }
}