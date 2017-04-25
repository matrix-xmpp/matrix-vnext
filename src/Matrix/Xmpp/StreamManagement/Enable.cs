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
    [XmppTag(Name = "enable", Namespace = Namespaces.FeatureStreamManagement)]
    public class Enable : XmppXElement
    {
        internal Enable(string tagname)
            : base(Namespaces.FeatureStreamManagement, tagname)
        {
        }

        public Enable() : this("enable")
        {
        }

        public bool Resume
        {
            get { return GetAttributeBool("resume"); }
            set
            {
                if (value)
                    SetAttribute("resume", true);
                else
                    RemoveAttribute("resume");
            }
        }

        /// <summary>
        /// The initiating entity's preferred maximum resumption time in seconds.
        /// </summary>
        public int MaxResumptionTime
        {
            get { return GetAttributeInt("max"); }
            set { SetAttribute("max", value); }
        }

        /// <summary>
        /// The initiating entity's preferred number of stanzas between acks.
        /// </summary>
        public int StanzasBetweenAcks
        {
            get { return GetAttributeInt("stanzas"); }
            set { SetAttribute("stanzas", value); }
        }
    }
}
