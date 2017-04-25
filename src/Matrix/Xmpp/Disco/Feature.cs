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

namespace Matrix.Xmpp.Disco 
{
    [XmppTag(Name = "feature", Namespace = Namespaces.DiscoInfo)]
    public class Feature : XmppXElement
    {
        #region Xml sample
        /*
		<iq type='result'
			from='plays.shakespeare.lit'
			to='romeo@montague.net/orchard'
			id='info1'>
		<query xmlns='http://jabber.org/protocol/disco#info'>
			<identity
				category='conference'
				type='text'
				name='Play-Specific Chatrooms'/>
			<identity
				category='directory'
				type='chatroom'
				name='Play-Specific Chatrooms'/>
			<feature var='http://jabber.org/protocol/disco#info'/>
			<feature var='http://jabber.org/protocol/disco#items'/>
			<feature var='http://jabber.org/protocol/muc'/>
			<feature var='jabber:iq:register'/>
			<feature var='jabber:iq:search'/>
			<feature var='jabber:iq:time'/>
			<feature var='jabber:iq:version'/>
		</query>
		</iq>
		*/
        #endregion

        #region << Constructors >>
        public Feature()
            : base(Namespaces.DiscoInfo, "feature")
        {
        }

        public Feature(string var) : this()
        {
            Var = var;
        }
        #endregion

        /// <summary>
        /// protocol namespace or other feature offered by the entity
        /// </summary>
        public string Var
        {
            get { return GetAttribute("var"); }
            set { SetAttribute("var", value); }
        }
    }
}
