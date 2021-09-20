namespace Matrix
{
    using System.Threading.Tasks;

    /// <summary>
    /// Implements exponential backoff strategy for reconnect logic.
    /// </summary>
    public class ExponentialBackoff
    {
        private readonly int startDelay;
        private readonly int maxDelay;
        private int delay;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExponentialBackoff"/> class.
        /// </summary>
        /// <param name="startDelay">delay to start with in seconds</param>
        /// <param name="maxDelay">maximum delay in seconds</param>
        public ExponentialBackoff(int startDelay = 1, int maxDelay = 128)
        {
            this.delay = startDelay;
            this.startDelay = startDelay;
            this.maxDelay = maxDelay;
        }

        public Task Delay()
        {
            // store current delay
            var curDelay = this.delay;

            // increase for next interation
            if (delay < 128)
            {
                this.delay = delay << 1;
            }

            return Task.Delay(curDelay * 1000);
        }
    }
}
