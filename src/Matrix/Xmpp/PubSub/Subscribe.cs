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

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "subscribe", Namespace = Namespaces.Pubsub)]
    public class Subscribe: XmppXElement
    {
        #region Xml sample
        /*
            <iq type='set'
                from='francisco@denmark.lit/barracks'
                to='pubsub.shakespeare.lit'
                id='sub1'>
              <pubsub xmlns='http://jabber.org/protocol/pubsub'>
                <subscribe
                    node='princely_musings'
                    jid='francisco@denmark.lit'/>
              </pubsub>
            </iq>
        */
        #endregion

        #region << Constructors >>
        public Subscribe()
            : this("subscribe")
		{
        }

        internal Subscribe(string tagName) : base(Namespaces.Pubsub, tagName)
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        /// <value>The node.</value>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }

        /// <summary>
        /// Gets or sets the jid.
        /// </summary>
        /// <value>The jid.</value>
        public Jid Jid
        {
            get { return GetAttributeJid("jid"); }
            set { SetAttribute("jid", value); }
        }
    }
}
