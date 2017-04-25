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

namespace Matrix.Xmpp.StreamManagement
{
    [XmppTag(Name = "resume", Namespace = Namespaces.FeatureStreamManagement)]
    public class Resume : XmppXElement
    {
        internal Resume(string tagname) : base(Namespaces.FeatureStreamManagement, tagname)
        {
        }

        public Resume() : this("resume")
        {
        }

        /// <summary>
        /// The sequenze number of the last handled stanza of the previous connection.
        /// </summary>
        public int  LastHandledStanza
        {
            get { return GetAttributeInt("h"); }
            set { SetAttribute("h", value); }
        }

        /// <summary>
        /// The SM-ID of the former stream.
        /// </summary>
        public string PreviousId
        {
            get { return GetAttribute("previd"); }
            set { SetAttribute("previd", value); }
        }
    }
}
