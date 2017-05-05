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
using Matrix.Xml;

namespace Matrix.Xmpp.Privacy
{
    [XmppTag(Name = Tag.Item, Namespace = Namespaces.IqPrivacy)]
    public class Item : XmppXElement
    {
        public Item() : base(Namespaces.IqPrivacy, "item")
        {
        }
        
        public Action Action
		{
			get { return GetAttributeEnum<Action>("action"); }
			set { SetAttribute("action", value.ToString().ToLower()); }
		}

        public Type Type
        {
            get { return GetAttributeEnum<Type>("type"); }
            set
            {
                if (value != Type.None)
                    SetAttribute("type", value.ToString().ToLower());
                else
                    RemoveAttribute("type");
            }
        }

        /// <summary>
        /// The order of this rule
        /// </summary>
        public int Order
        {
            get { return GetAttributeInt("order"); }
            set { SetAttribute("order", value); }
        }

        /// <summary>
        /// The value to match of this rule
        /// </summary>
        public string Val
        {
            get { return GetAttribute("value"); }
            set { SetAttribute("value", value); }
        }

        /// <summary>
        /// Block Iq stanzas
        /// </summary>
        public bool BlockIq
        {
            get { return HasTag(Tag.Iq); }
            set
            {
                if (value)
                    SetTag(Tag.Iq);
                else
                    RemoveTag(Tag.Iq);
            }
        }

        /// <summary>
        /// Block messages
        /// </summary>
        public bool BlockMessage
        {
            get { return HasTag(Tag.Message); }
            set
            {
                if (value)
                    SetTag(Tag.Message);
                else
                    RemoveTag(Tag.Message);
            }
        }

        /// <summary>
        /// Block incoming presence
        /// </summary>
        public bool BlockIncomingPresence
        {
            get { return HasTag(Tag.PresenceIn); }
            set
            {
                if (value)
                    SetTag(Tag.PresenceIn);
                else
                    RemoveTag(Tag.PresenceIn);
            }
        }

        /// <summary>
        /// Block outgoing presence
        /// </summary>
        public bool BlockOutgoingPresence
        {
            get
            {

                return HasTag(Tag.PresenceOut);
            }
            set
            {
                if (value)
                    SetTag(Tag.PresenceOut);
                else
                    RemoveTag(Tag.PresenceOut);
            }
        }
        
        /// <summary>
        /// which stanzas should be blocked?
        /// the enum values can be combined.
        /// </summary>
        /// <example>
        /// item.Stanza = Stanza.Message | Stanza.IncomingPresence;
        /// </example>
        public Stanza Stanza
        {
            get
            {
                var result = Stanza.All;

                if (BlockIq)
                    result |= Stanza.Iq;
                if (BlockMessage) 
                    result |= Stanza.Message;
                if (BlockIncomingPresence)
                    result |= Stanza.IncomingPresence;
                if (BlockOutgoingPresence)
                    result |= Stanza.OutgoingPresence;
                
                return result;
            }
            set
            {
                if (value == Stanza.All)
                {
                    // Block All Communications
                    BlockIq                 = false;
                    BlockMessage            = false;
                    BlockIncomingPresence   = false;
                    BlockOutgoingPresence   = false;
                }
                else
                {
                    BlockIq                 = ((value & Stanza.Iq) == Stanza.Iq);
                    BlockMessage            = ((value & Stanza.Message) == Stanza.Message);
                    BlockIncomingPresence   = ((value & Stanza.IncomingPresence) == Stanza.IncomingPresence);
                    BlockOutgoingPresence   = ((value & Stanza.OutgoingPresence) == Stanza.OutgoingPresence);
                }
            }
        }
    }
}
