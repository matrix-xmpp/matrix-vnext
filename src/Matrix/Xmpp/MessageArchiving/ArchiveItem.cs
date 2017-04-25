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
using Matrix.Xml;

namespace Matrix.Xmpp.MessageArchiving
{
    public abstract class ArchiveItem : XmppXElement
    {
        protected ArchiveItem(string tagName): base(Namespaces.Archiving, tagName)
        {}

        /// <summary>
        /// Gets or sets the UTC time stamp of the absolute time the note was created. 
        /// </summary>
        /// <value>
        /// The time stamp.
        /// </value>
        public DateTime TimeStamp
        {
            get { return GetAttributeIso8601Date("utc"); }
            set { SetAttributeIso8601Date("utc", value); }
        }
    }
}
