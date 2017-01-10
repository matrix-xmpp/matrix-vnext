using Matrix.Attributes;

namespace Matrix.Xmpp.Muc
{
    /// <summary>
    /// There are five defined affiliations that a user may have in relation to a room
    /// </summary>
    public enum Affiliation
    {
        /// <summary>
        /// the absence of an affiliation
        /// </summary>
        [Name("none")]
        None,

        /// <summary>
        /// 
        /// </summary>
        [Name("owner")]
        Owner,

        /// <summary>
        /// 
        /// </summary>
        [Name("admin")]
        Admin,

        /// <summary>
        /// 
        /// </summary>
        [Name("member")]
        Member,

        /// <summary>
        /// 
        /// </summary>
        [Name("outcast")]
        Outcast
    }
}