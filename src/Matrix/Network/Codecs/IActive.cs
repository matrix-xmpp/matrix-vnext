namespace Matrix.Network.Codecs
{
    /// <summary>
    /// Interface for decoders which can be active or inactive in the pipeline.
    /// </summary>
    public interface IActive
    {
        bool Active { get; set; }
    }
}
