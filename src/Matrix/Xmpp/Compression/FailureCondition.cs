using Matrix.Attributes;

namespace Matrix.Xmpp.Compression
{
    public enum FailureCondition
    {
        /// <summary>
        /// unknown error condition
        /// </summary>
        UnknownCondition = -1,
        
        /// <summary>
        /// 
        /// </summary>
        [Name("setup-failed")]
        SetupFailed,
        
        /// <summary>
        /// 
        /// </summary>
        [Name("processing-failed")]
        ProcessingFailed,
        
        /// <summary>
        /// 
        /// </summary>
        [Name("unsupported-method")]
        UnsupportedMethod
    }
}