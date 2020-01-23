namespace Matrix.Tls.Mono
{
    enum FlushMode : byte
    {
        /// <summary>
        /// Do nothing with Flush.
        /// </summary>
        NoFlush = 0,
        /// <summary>
        /// An Flush is or will be posted to IEventExecutor.
        /// </summary>
        PendingFlush = 1,
        /// <summary>
        /// Force FinishWrap to call Flush.
        /// </summary>
        ForceFlush = 2,
    }
}
