namespace Matrix
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IStreamManagementHandler
    {
        /// <summary>
        /// Gets or sets a value indicating whether stream management was enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the stream can be resumed or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if resume; otherwise, <c>false</c>.
        /// </value>
        bool CanResume { get; set; }

        /// <summary>
        /// Gets a value indicating whether Stream Management is supported.
        /// </summary>
        /// <value>
        ///   <c>true</c> if supported; otherwise, <c>false</c>.
        /// </value>
        bool Supported { get; }

        string StreamId { get; set; }

        /// <summary>
        /// Enables stream management on the current XMPP stream
        /// </summary>        
        /// <returns></returns>
        Task EnableAsync();

        Task ResumeAsync(CancellationToken cancellationToken);
    }

}
