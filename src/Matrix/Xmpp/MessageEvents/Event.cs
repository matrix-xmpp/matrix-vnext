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

namespace Matrix.Xmpp.MessageEvents
{
    /// <summary>
    /// XEP-0022: Message Events
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.XEvent)]
    public class Event : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
         public Event() : base(Namespaces.XEvent, "x")
         {
         }

         /// <summary>
         /// Indicates that the message has been stored offline by the intended recipient's server.
         /// This event is triggered only if the intended recipient's server supports offline storage, 
         /// has that support enabled, and the recipient is offline when the server receives the message for delivery.
         /// </summary>
         public bool IsOffline
         {
             get { return HasTag("offline"); }
             set
             {
                 RemoveTag("offline");
                 if (value)
                     AddTag("offline");
             }
         }

         /// <summary>
         /// Indicates that the message has been delivered to the recipient. 
         /// This signifies that the message has reached the recipient's Jabber client, 
         /// but does not necessarily mean that the message has been displayed. 
         /// This event is to be raised by the Jabber client.
         /// </summary>
         public bool IsDelivered
         {
             get { return HasTag("delivered"); }
             set
             {
                 RemoveTag("delivered");
                 if (value)
                     AddTag("delivered");
             }
         }

         /// <summary>
         /// Once the message has been received by the recipient's Jabber client, 
         /// it may be displayed to the user. 
         /// This event indicates that the message has been displayed, and is to be raised by the Jabber client.
         /// Even if a message is displayed multiple times, this event should be raised only once.
         /// </summary>
         public bool IsDisplayed
         {
             get { return HasTag("displayed"); }
             set
             {
                 RemoveTag("displayed");
                 if (value)
                     AddTag("displayed");
             }
         }

         /// <summary>
         /// In threaded chat conversations, this indicates that the recipient is composing a reply to a message.
         /// The event is to be raised by the recipient's Jabber client. 
         /// A Jabber client is allowed to raise this event multiple times in response to the same request, 
         /// providing the original event is cancelled first.
         /// </summary>
         public bool IsComposing
         {
             get { return HasTag("composing"); }
             set
             {
                 RemoveTag("composing");
                 if (value)
                     AddTag("composing");
             }
         }

         /// <summary>
         /// The type of the event.
         /// </summary>
         public EventType Type
         {
             get
             {
                 var ret = EventType.None;

                 if (IsOffline)
                     ret |= EventType.Offline;
                
                 if (IsDelivered)
                     ret |= EventType.Delivered;
                 
                 if (IsDisplayed) 
                     ret |= EventType.Displayed;
                 
                 if (IsComposing)
                     ret |= EventType.Composing;

                 return ret;
             }
             set
             {
                 IsOffline      = (value & EventType.Offline) == EventType.Offline;
                 IsDelivered    = (value & EventType.Delivered) == EventType.Delivered;
                 IsDisplayed    = (value & EventType.Displayed) == EventType.Displayed;
                 IsComposing    = (value & EventType.Composing) == EventType.Composing;
             }
         }

         /// <summary>
         /// 'id' attribute of the original message to which this event notification pertains.
         /// (If no 'id' attribute was included in the original message, then the id tag must still be included with no 
         /// </summary>
         public string Id
         {
             get { return GetTag("id"); }
             set { SetTag("id", value); }
         }
    }
}
