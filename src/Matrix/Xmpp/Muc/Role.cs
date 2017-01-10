using Matrix.Attributes;

namespace Matrix.Xmpp.Muc
{
    /// <summary>
    /// There are four defined roles that an occupant may have
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// the absence of a role
        /// </summary>
        [Name("none")]
        None,
        
        /// <summary>
        /// A moderator is the most powerful occupant within the context of the room, 
        /// and can to some extent manage other occupants's roles in the room.
        /// </summary>
        [Name("moderator")]
        Moderator,
        
        /// <summary>
        /// A participant has fewer privileges than a moderator, although he or she always has the right to speak.
        /// </summary>
        [Name("participant")]
        Participant,
        
        /// <summary>
        /// A visitor is a more restricted role within the context of a moderated room, 
        /// since visitors are not allowed to send messages to all occupants.
        /// </summary>
        [Name("visitor")]
        Visitor
    }
}