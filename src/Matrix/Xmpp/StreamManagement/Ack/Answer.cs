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

namespace Matrix.Xmpp.StreamManagement.Ack
{
    [XmppTag(Name = "a", Namespace = Namespaces.FeatureStreamManagement)]
    public class Answer : XmppXElement
    {
        public Answer() : base(Namespaces.FeatureStreamManagement, "a")
        {
        }

        /// <summary>
        /// Identifies the last handled stanza (i.e., the last stanza that the receiver will acknowledge as having received).
        /// </summary>
        public int LastHandledStanza
        {
            get { return GetAttributeInt("h"); }
            set { SetAttribute("h", value); }
        }
    }
}
