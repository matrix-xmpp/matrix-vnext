namespace Matrix.Xmpp.AdvancedMessageProcessing
{
    public enum Action
    {
        None = -1,

        /// <summary>
        /// Namespace: http://jabber.org/protocol/amp?action=drop
        /// Behavior: The message is silently discarded but an alert is returned to the sender.
        /// Defined in XEP-0079: Advanced Message Processing.
        /// </summary>
        Alert,
        
        /// <summary>
        /// Namespace: http://jabber.org/protocol/amp?action=drop
        /// Behavior: The message is silently discarded.
        /// Defined in XEP-0079: Advanced Message Processing.
        /// </summary>
        Drop,

        /// <summary>
        /// Namespace: http://jabber.org/protocol/amp?action=error
        /// Behavior: The message is not processed and an error is returned to the sender, specifying which rule resulted in failed processing.
        /// Defined in XEP-0079: Advanced Message Processing.
        /// </summary>
        Error,
        
        /// <summary>
        /// Namespace: http://jabber.org/protocol/amp?action=notify
        /// Behavior: The message is processed and a notification message is returned to the sender, specifying which rule was processed.
        /// Defined in XEP-0079: Advanced Message Processing.
        /// </summary>
        Notify
    }
}
