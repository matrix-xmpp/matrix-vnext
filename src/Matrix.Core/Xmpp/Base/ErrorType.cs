using Matrix.Attributes;

namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// 
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// 
        /// </summary>
        [Name("cancel")]
        Cancel,

        /// <summary>
        /// 
        /// </summary>
        [Name("continue")]
        Continue,

        /// <summary>
        /// 
        /// </summary>
        [Name("modify")]
        Modify,

        /// <summary>
        /// 
        /// </summary>
        [Name("auth")]
        Auth,

        /// <summary>
        /// 
        /// </summary>
        [Name("wait")]
        Wait
    }
}
