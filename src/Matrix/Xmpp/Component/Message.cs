/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using Matrix.Attributes;

namespace Matrix.Xmpp.Component
{
    [XmppTag(Name = Tag.Message, Namespace = Namespaces.Accept)]
    public class Message : Base.Message
    {        
        #region << Constructors >>
        public Message() : base(Namespaces.Accept)
        {            
        }      

        public Message(Jid to)
            : this()
        {
            To = to;
        }
        public Message(Jid to, string body)
            : this(to)
        {
            Body = body;
        }

        public Message(Jid to, Jid from)
            : this()
        {
            To = to;
            From = from;
        }

        public Message(string to, string body)
            : this()
        {
            To = new Jid(to);
            Body = body;
        }

        public Message(Jid to, string body, string subject)
            : this()
        {
            To = to;
            Body = body;
            Subject = subject;
        }

        public Message(string to, string body, string subject)
            : this()
        {
            To = new Jid(to);
            Body = body;
            Subject = subject;
        }

        public Message(string to, string body, string subject, string thread)
            : this()
        {
            To = new Jid(to);
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        public Message(Jid to, string body, string subject, string thread)
            : this()
        {
            To = to;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        public Message(string to, MessageType type, string body)
            : this()
        {
            To = new Jid(to);
            Type = type;
            Body = body;
        }

        public Message(Jid to, MessageType type, string body)
            : this()
        {
            To = to;
            Type = type;
            Body = body;
        }

        public Message(string to, MessageType type, string body, string subject)
            : this()
        {
            To = new Jid(to);
            Type = type;
            Body = body;
            Subject = subject;
        }

        public Message(Jid to, MessageType type, string body, string subject)
            : this()
        {
            To = to;
            Type = type;
            Body = body;
            Subject = subject;
        }

        public Message(string to, MessageType type, string body, string subject, string thread)
            : this()
        {
            To = new Jid(to);
            Type = type;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        public Message(Jid to, MessageType type, string body, string subject, string thread)
            : this()
        {
            To = to;
            Type = type;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        public Message(Jid to, Jid from, string body)
            : this()
        {
            To = to;
            From = from;
            Body = body;
        }

        public Message(Jid to, Jid from, string body, string subject)
            : this()
        {
            To = to;
            From = from;
            Body = body;
            Subject = subject;
        }

        public Message(Jid to, Jid from, string body, string subject, string thread)
            : this()
        {
            To = to;
            From = from;
            Body = body;
            Subject = subject;
            Thread = thread;
        }

        public Message(Jid to, Jid from, MessageType type, string body)
            : this()
        {
            To = to;
            From = from;
            Type = type;
            Body = body;
        }

        public Message(Jid to, Jid from, MessageType type, string body, string subject)
            : this()
        {
            To = to;
            From = from;
            Type = type;
            Body = body;
            Subject = subject;
        }

        public Message(Jid to, Jid from, MessageType type, string body, string subject, string thread)
            : this()
        {
            To = to;
            From = from;
            Type = type;
            Body = body;
            Subject = subject;
            Thread = thread;
        }
        #endregion

        /// <summary>
        /// Error object
        /// </summary>
        public new Error Error
        {
            get { return Element<Error>(); }
            set { Replace<Error>(value); }
        }
    }
}
