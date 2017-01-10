using Matrix.Core;

namespace Matrix.Xmpp.Client
{
    public class JingleIq : Iq
    {
        public JingleIq()
        {
            GenerateId();
            Jingle = new Jingle.Jingle();
        }

        public Jingle.Jingle Jingle
        {
            get { return Element<Jingle.Jingle>(); }
            set { Replace(value); }
        }
    }
}