namespace Matrix
{
    using System;

    public class StanzaCounter : DistinctBehaviorSubject<long>
    {
        private long maxSequence = (long)Math.Pow(2, 32);

        public StanzaCounter() : base(0)
        {
        }

        public void Increment()
        {
            this.Value = (this.Value + 1) % maxSequence;
        }

        public void Reset()
        {
            this.Value = 0;
        }
    }
}