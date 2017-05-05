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

namespace Matrix.Xmpp.AdvancedMessageProcessing
{
    [XmppTag(Name = "rule", Namespace = Namespaces.AMP)]
    public class Rule : XmppXElement
    {
        public Rule() : base(Namespaces.AMP, "rule")
        {
        }

        /// <summary>
        /// The 'value' attribute defines how the condition is matched. 
        /// This attribute MUST be present, and MUST NOT be an empty string (""). 
        /// The interpretation of this attribute's value is determined by the 'condition' attribute.
        /// </summary>
        public string ValueAsString
        {
            get { return GetAttribute("value"); }
            set { SetAttribute("value", value); }
        }

        public DateTime ValueAsDateTime
        {
            get { return Matrix.Time.Iso8601Date(ValueAsString); }
            set { ValueAsString = Matrix.Time.Iso8601DateString(value); }
        }
        
        /// <summary>
        /// The 'action' attribute defines the result for this rule. 
        /// This attribute MUST be present, and MUST be either a value defined in the Defined Actions section, 
        /// or one registered with the XMPP Registrar.
        /// </summary>
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
        /// The 'condition' attribute defines the overall condition this rule applies to. 
        /// This attribute MUST be present, and MUST be either a value defined in the Defined Conditions section, 
        /// or one registered with the XMPP Registrar.
        /// </summary>
        public Condition Condition
        {
            get { return GetAttributeEnumUsingNameAttrib<Condition>("condition"); }
            set { SetAttribute("condition", value.GetName()); }
        }
    }
}
