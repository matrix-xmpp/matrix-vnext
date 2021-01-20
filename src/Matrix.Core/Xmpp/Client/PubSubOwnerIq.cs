namespace Matrix.Xmpp.Client
{
    /// <summary>
    /// Pubsub owner iq
    /// </summary>
    public class PubSubOwnerIq : Iq
    {
        public PubSubOwnerIq()
        {
            GenerateId();
            PubSub = new PubSub.Owner.PubSub();
        }
    
        public PubSub.Owner.PubSub PubSub
        {
            get { return Element<PubSub.Owner.PubSub>(); }
            set { Replace(value); }
        }
    }
}
