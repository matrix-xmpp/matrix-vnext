/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

namespace Matrix.Xmpp.MessageArchiveManagement
{
    [XmppTag(Name = "prefs", Namespace = Namespaces.MessageArchiveManagement)]
    public class Preferences : XmppXElement
    {
        public Preferences() : base(Namespaces.MessageArchiveManagement, "prefs")
        {
        }

        /// <summary>
        /// Gets or sets the default behaviour
        /// </summary>
        public DefaultPreference Default
        {
            get { return GetAttributeEnumUsingNameAttrib<DefaultPreference>("default"); }
            set { SetAttribute("default", value.GetName()); }
        }

        /// <summary>
        /// Gets or Sets the <see cref="Always"/> Element
        /// </summary>
        public Always Always
        {
            get { return Element<Always>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// /// Gets or Sets the <see cref="Never"/> Element
        /// </summary>
        public Never Never
        {
            get { return Element<Never>(); }
            set { Replace(value); }
        }
    }
}
