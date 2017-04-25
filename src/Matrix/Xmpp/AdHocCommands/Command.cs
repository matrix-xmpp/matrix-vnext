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

namespace Matrix.Xmpp.AdHocCommands
{
    [XmppTag(Name = "command", Namespace = Namespaces.AdHocCommands)]
    public class Command : XmppXElement
    {
        public Command()
            : base(Namespaces.AdHocCommands, "command")
        {
        }
        
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
        /// Gets or sets the session id.
        /// </summary>
        /// <value>The session id.</value>
        public string SessionId
        {
            get { return GetAttribute("sessionid"); }
            set { SetAttribute("sessionid", value); }
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>The action.</value>
        public Action Action
        {
            get
            {
                return GetAttributeEnum<Action>("action");
            }
            set
            {
                if (value == Action.None)
                    RemoveAttribute("action");
                else
                    SetAttribute("action", value.ToString().ToLower());
            }
        }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public Status Status
        {
            get { return GetAttributeEnum<Status>("status"); }
            set
            {
                if (value == Status.None)
                    RemoveAttribute("status");
                else
                    SetAttribute("status", value.ToString().ToLower());
            }
        }
        
        public Actions Actions
        {
            get { return Element<Actions>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        /// <value>The note.</value>
        public Note Note
        {
            get { return Element<Note>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the X data.
        /// </summary>
        /// <value>The X data.</value>
        public XData.Data XData
        {
            get { return Element<XData.Data>(); }
            set { Replace(value); }
        }
    }
}
