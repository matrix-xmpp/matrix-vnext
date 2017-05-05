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

namespace Matrix.Xmpp.XData
{
    [XmppTag(Name = "option", Namespace = Namespaces.XData)]
    public class Option : XmppXElement
    {
        public Option() : base(Namespaces.XData, "option")
        {
        }

        public Option(string val) : this()
        {
            Value = val;
        }

        public Option(string label, string val) : this(val)
        {
            Label = label;
        }

        #region << Properties >>
        /// <summary>
        /// Label of the option
        /// </summary>
        public string Label
        {
            get { return GetAttribute("label"); }
            set { SetAttribute("label", value); }
        }
        #endregion
        
        public new string Value
        {
            /*
            get { return GetTag<XData.Value>(); }
            set 
            {
                XData.Value val = Element<XData.Value>();
                if (val == null)
                    val = new Value();
                
                val.Value = value;                 
            }
             */

            // much easier that way
            get { return GetTag("value"); }
            set { SetTag("value", value); }
        }
    }
}
