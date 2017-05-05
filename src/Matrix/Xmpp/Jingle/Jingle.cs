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

using System;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Jingle
{
    #region Xml sample
    /*
        <jingle xmlns='urn:xmpp:jingle:1'>
          action='session-initiate'
          initiator='romeo@montague.lit/orchard'
          sid='a73sjjvkla37jfea'>
     */
    #endregion
    [XmppTag(Name = "jingle", Namespace = Namespaces.Jingle)]
    public class Jingle : XmppXElement
    {
        public Jingle() : base(Namespaces.Jingle, "jingle")
        {
        }

        public Action Action
        {
            get { return GetAttributeEnumUsingNameAttrib<Action>("action");}
            set { SetAttribute("action", value.GetName());}
        }

        public Jid Initiator
        {
            get { return GetAttributeJid("initiator");}
            set { SetAttribute("initiator", value.ToString());}
        }

        public Jid Responder
        {
            get { return GetAttributeJid("responder");}
            set { SetAttribute("responder", value);}
        }
        
        public string Sid
        {
            get { return GetAttribute("sid"); }
            set { SetAttribute("sid", value); }
        }

        /// <summary>
        /// generates a new unique Sid
        /// </summary>
        public void GenerateSid()
        {
            Sid = Guid.NewGuid().ToString();
        }

        public Content Content
        {
            get { return Element<Content>(); }
            set { Replace(value); }
        }
    }
}
