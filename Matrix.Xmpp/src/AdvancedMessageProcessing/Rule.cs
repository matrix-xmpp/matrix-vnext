using System;
using Matrix.Core;
using Matrix.Core.Attributes;
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
            get { return Time.Iso8601Date(ValueAsString); }
            set { ValueAsString = Time.Iso8601DateString(value); }
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