namespace Matrix.Xmpp.MessageCarbons
{
    public abstract class ForwardContainer : CarbonBase
    {
        protected ForwardContainer(string tagname) : base(tagname) { }
        
        /// <summary>
        /// Gets or sets the forwarded.
        /// </summary>
        /// <value>
        /// The forwarded.
        /// </value>
        public Forwarded Forwarded
        {
            get { return Element<Forwarded>(); }
            set { Replace(value); }
        }
    }
}
