using Matrix.Attributes;

namespace Matrix.Xmpp.Privacy
{
    /// <summary>
    /// privacy list action
    /// </summary>
    public enum Action
    {
        /// <summary>
        /// 
        /// </summary>
        [Name("allow")]
        Allow,
        
        /// <summary>
        /// 
        /// </summary>
        [Name("deny")]
        Deny
    }
}