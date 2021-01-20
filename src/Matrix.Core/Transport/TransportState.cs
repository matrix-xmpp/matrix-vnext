namespace Matrix.Transport
{
    using Matrix;

    public class TransportState : DistinctBehaviorSubject<State>
    {
        public TransportState() : base(State.Disconnected)
        {
        }
    }
}
