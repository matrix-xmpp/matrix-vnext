namespace Matrix.Xmpp.Client
{
    public class PubSubIq : Iq
    {
        public PubSubIq()
        {
            GenerateId();
            PubSub = new PubSub.PubSub();
        }
    
        public PubSub.PubSub PubSub
        {
            get { return Element<PubSub.PubSub>(); }
            set { Replace(value); }
        }
    }
}
