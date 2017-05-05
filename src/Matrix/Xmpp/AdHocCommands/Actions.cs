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

namespace Matrix.Xmpp.AdHocCommands
{
    [XmppTag(Name = "actions", Namespace = Namespaces.AdHocCommands)]
    public class Actions : XmppXElement
    {
        public Actions() : base(Namespaces.AdHocCommands, "actions")
        {
        }

        /// <summary>
        /// Gets or sets the execute action, only complete, next and previous is allowed
        /// </summary>
        public Action Execute
        {
            get
            {
                return GetAttributeEnum<Action>("execute");
            }
            set
            {
                if ((int) value > 7)
                    throw new NotSupportedException();
                
                if (value == Action.None)
                    RemoveAttribute("execute");
                else
                    SetAttribute("execute", value.ToString().ToLower());
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Actions"/> is "complete".
        /// </summary>
        /// <value><c>true</c> if complete; otherwise, <c>false</c>.</value>
        public bool Complete
        {
            get { return HasTag("complete"); }
            set
            {
                if (value)
                    SetTag("complete");
                else
                    RemoveTag("complete");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Actions"/> is "next".
        /// </summary>
        /// <value><c>true</c> if next; otherwise, <c>false</c>.</value>
        public bool Next
        {
            get { return HasTag("next"); }
            set
            {
                if (value)
                    SetTag("next");
                else
                    RemoveTag("next");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Actions"/> is "previous".
        /// </summary>
        /// <value><c>true</c> if previous; otherwise, <c>false</c>.</value>
        public bool Previous
        {
            get { return HasTag("prev"); }
            set
            {
                if (value)
                    SetTag("prev");
                else
                    RemoveTag("prev");
            }
        }

        /// <summary>
        /// Actions, only complete, prev and next are allowed here and can be combined
        /// </summary>
        public Action Action
        {
            get
            {
                Action res = 0;

                if (Complete)
                    res |= Action.Complete;
                
                if (Previous)
                    res |= Action.Prev;
                
                if (Next)
                    res |= Action.Next;

                if (res == 0)
                    return Action.None;
                
                return res;
            }
            set
            {
                // only complete, previous and next are supported.
                if ((int) value > 7)
                    throw new NotSupportedException();

                if (value == Action.None)
                {
                    Complete = false;
                    Previous = false;
                    Next = false;
                }
                else
                {
                    Complete = ((value & Action.Complete) == Action.Complete);
                    Previous = ((value & Action.Prev) == Action.Prev);
                    Next = ((value & Action.Next) == Action.Next);
                }
            }
        }
    }
}
