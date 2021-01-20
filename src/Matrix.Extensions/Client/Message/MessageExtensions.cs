namespace Matrix.Extensions.Client.Message
{  
    using System.Threading.Tasks;
    using Matrix.Xmpp;
    using Matrix.Xmpp.Client;

    public static class MessageExtensions
    {
        /// <summary>
        /// Send a message
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="msg">the <see cref="Message"/></param>
        /// <returns></returns>
        public static async Task SendMessageAsync(this IStanzaSender stanzaSender, Message msg)
        {
            await stanzaSender.SendAsync(msg).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a chat message
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="to"></param>
        /// <param name="text"></param>        
        /// <returns></returns>
        public static async Task SendChatMessageAsync(this IStanzaSender stanzaSender, Jid to, string text)
        {  
            await stanzaSender.SendAsync(new Message { Type = MessageType.Chat, To = to, Body = text }).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a chat message
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="to">The recipient of the message</param>
        /// <param name="text">The text of the message</param>
        /// <param name="thread">The message thread</param>
        /// <param name="autoGenerateMessageId">Should an id be automatically generated for the message?</param>
        /// <returns></returns>
        public static async Task SendChatMessageAsync(this IStanzaSender stanzaSender, Jid to, string text, string thread, bool autoGenerateMessageId)
        {
            var msg = new Message { Type = MessageType.Chat, To = to, Body = text, Thread = thread };
            if (autoGenerateMessageId)
            {
                msg.Id = Id.GenerateShortGuid();
            }

            await stanzaSender.SendAsync(msg).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a chat message
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="to">The recipient of the message</param>
        /// <param name="text">The text of the message</param>
        /// <param name="autoGenerateMessageId">Should an id be automatically generated for the message?</param>
        /// <returns></returns>
        public static async Task SendChatMessageAsync(this IStanzaSender stanzaSender, Jid to, string text, bool autoGenerateMessageId)
        {
            var msg = new Message { Type = MessageType.Chat, To = to, Body = text };
            if (autoGenerateMessageId)
            {
                msg.Id = Id.GenerateShortGuid();
            }

            await stanzaSender.SendAsync(msg).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a goup chat message
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="to">The chat room the message should get delivered to</param>
        /// <param name="text">The text of the message</param>
        /// <returns></returns>        
        public static async Task SendGroupChatMessageAsync(this IStanzaSender stanzaSender, Jid to, string text)
        {
            await stanzaSender.SendAsync(new Message { Type = MessageType.GroupChat, To = to, Body = text }).ConfigureAwait(false);
        }
                
        /// <summary>
        /// Send a group chat message
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="to">The chat room the message should get delivered to</param>
        /// <param name="text">The text of the message</param>
        /// <param name="autoGenerateMessageId">Should an id be automatically generated for the message?</param>
        /// <returns></returns>
        public static async Task SendGroupChatMessageAsync(this IStanzaSender stanzaSender, Jid to, string text, bool autoGenerateMessageId)
        {
            var msg = new Message { Type = MessageType.GroupChat, To = to, Body = text };
            if (autoGenerateMessageId)
            {
                msg.Id = Id.GenerateShortGuid();
            }

            await stanzaSender.SendAsync(msg).ConfigureAwait(false);
        }

        /// <summary>
        /// Send a group chat message
        /// </summary>
        /// <param name="stanzaSender"><see cref="IStanzaSender"/></param>
        /// <param name="to">The chat room the message should get delivered to</param>
        /// <param name="text">The text of the message</param>
        /// <param name="thread">The message thread</param>
        /// <param name="autoGenerateMessageId">Should an id be automatically generated for the message?</param>
        /// <returns></returns>
        public static async Task SendGroupChatMessageAsync(this IStanzaSender stanzaSender, Jid to, string text, string thread, bool autoGenerateMessageId)
        {
            var msg = new Message { Type = MessageType.GroupChat, To = to, Body = text, Thread = thread };
            if (autoGenerateMessageId)
            {
                msg.Id = Id.GenerateShortGuid();
            }

            await stanzaSender.SendAsync(msg).ConfigureAwait(false);
        }
    }
}
