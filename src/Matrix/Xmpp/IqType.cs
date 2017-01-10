using Matrix.Attributes;

namespace Matrix.Xmpp
{
    /// <summary>
    /// The 'type' attribute is REQUIRED for IQ stanzas.
    /// </summary>
    public enum IqType
    {
        /// <summary>
        /// The stanza is a request for information or requirements.
        /// </summary>
        [Name("get")]
        Get,
        
        /// <summary>
        /// The stanza provides required data, sets new values, or replaces existing values.
        /// </summary>
        [Name("set")]
        Set,
        
        /// <summary>
        /// The stanza is a response to a successful get or set request.
        /// </summary>
        [Name("result")]
        Result,
        
        /// <summary>
        /// An error has occurred regarding processing or delivery of a previously-sent get or set (see Stanza Errors (Stanza Errors)).
        /// </summary>
        [Name("error")]
        Error
    }
}