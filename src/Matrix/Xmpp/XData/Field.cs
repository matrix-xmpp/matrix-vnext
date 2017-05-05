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

using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.XData
{
    [XmppTag(Name = "field", Namespace = Namespaces.XData)]
    public class Field : XmppXElement
    {
        #region << Constructors >>
        public Field() : base(Namespaces.XData, "field")
        {
        }

        public Field(string var, string val) : this()
        {
            Var = var;
            SetValue(val);
        }

        public Field(FieldType type)
            : this()
        {
            Type = type;
        }

        public Field(string var, string label, FieldType type)
            : this()
        {
            Type = type;
            Var = var;
            Label = label;
        }
        #endregion
        
        #region << Properties >>
        public string Var
        {
            get { return GetAttribute("var"); }
            set { SetAttribute("var", value); }
        }

        public string Label
        {
            get { return GetAttribute("label"); }
            set { SetAttribute("label", value); }
        }

        public FieldType Type
        {
            get
            {
                var fType = GetAttribute("type");
                foreach (var eType in Enum.GetValues<FieldType>().ToEnum<FieldType>())
                {
                     if (fType == eType.GetName())
                        return eType;
                }
                return FieldType.Unknown;
            }
            set
            {
                if (value != FieldType.Unknown)
                    SetAttribute("type", value.GetName());
                else 
                    RemoveAttribute("type");
            }
        }

        /// <summary>
        /// The data of this element provides a natural-language 
        /// description of the field, intended for presentation in a user-agent (e.g., as a 
        /// &quot;tool-tip&quot;, help button, or explanatory text provided near the field). The &lt;desc/&gt; 
        /// element SHOULD NOT contain newlines (the \n and \r characters), since layout is 
        /// the responsibility of a user agent, and any handling of newlines (e.g., 
        /// presentation in a user interface) is unspecified herein. (Note: To provide a 
        /// description of a field, it is RECOMMENDED to use a &lt;desc/&gt; element rather than a 
        /// separate &lt;field/&gt; element of type &quot;fixed&quot;.)
        /// </summary>
        public string Description
        {
            get { return GetTag("desc"); }
            set { SetTag("desc", value); }
        }
        
        /// <summary>
        /// Is this field a required field?
        /// </summary>
        public bool Required
        {
            get { return HasTag("required"); }
            set
            {
                if (value == false)
                    RemoveTag("required");
                else
                    SetTag("required");
            }
        }
        #endregion
        
        /// <summary>
        /// The value of the field.
        /// </summary>
        public string GetValue()
        {
            return GetTag<Value>();            
        }

        public bool HasValue(string val)
        {
            return GetValues().Any(s => s.Equals(val));            
        }

        /// <summary>
        /// Set a value
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public void SetValue(string val)
        {
            var el = Element<Value>();
            if (el != null)
                el.Value = val;
            else
                Add(new Value(val));            
        }

        /// <summary>
        /// Set the value of boolean fields
        /// </summary>
        /// <param name="val"></param>
        public void SetValueBool(bool val)
        {
            SetValue(val ? "1" : "0");
        }

        /// <summary>
        /// Get the value of boolean fields
        /// </summary>
        /// <returns></returns>
        public bool GetValueBool()
        {
            // only "0" and "1" are valid. We dont care about other buggy implementations
            string val = GetValue();
            if (val == null || val == "0")
                return false;
            
            return true;
        }

        /// <summary>
        /// Returns the value as Jif for the Jid fields. 
        /// Or null when the value is not a valid Jid.
        /// </summary>
        /// <returns></returns>
        public Jid GetValueJid()
        {
            try
            {
                return new Jid(GetValue());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a value
        /// </summary>
        /// <remarks>
        /// you can call this function multiple times to add values to "multi" fields
        /// </remarks> 
        /// <param name="val"></param>
        public Field AddValue(string val)
        {
            Add(new Value(val));
            return this;
        }

        /// <summary>
        /// Adds a boolean value (1 = true, 0 = false)
        /// </summary>
        /// <param name="val"></param>
        public Field AddValue(bool val)
        {
            AddValue(val ? "1" : "0");
            return this;
        }

        /// <summary>
        /// Adds multiple values to the already existing values from a string array
        /// </summary>
        /// <param name="vals"></param>
        public Field AddValues(string[] vals)
        {
            if (vals.Length > 0)
            {
                foreach (string s in vals)
                    AddValue(s);
            }
            return this;
        }

        /// <summary>
        /// Adds multiple values. All already existing values will be removed
        /// </summary>
        /// <param name="vals"></param>
        public Field SetValues(string[] vals)
        {
            Nodes().Remove();
            AddValues(vals);
            return this;
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <returns></returns>
        public List<string> GetValues()
        {
            return Elements<Value>().Select(val => val.Value).ToList();            
        }
        
        /// <summary>
        /// Gets or sets any the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public string[] Values
        {
            get { return GetValues().ToArray(); }
            set { SetValues(value); }
        }
               
        /// <summary>
        /// Add a option
        /// </summary>
        /// <param name="label"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public Option AddOption(string label, string val)
        {
            Option opt = new Option(label, val);
            Add(opt);
            
            return opt;
        }

        /// <summary>
        /// Add a option
        /// </summary>
        /// <returns></returns>
        public Option AddOption()
        {
            Option opt = new Option();
            Add(opt);
            
            return opt;
        }

        /// <summary>
        /// Add a option
        /// </summary>
        /// <param name="opt"></param>
        public void AddOption(Option opt)
        {
            Add(opt);
        }

        /// <summary>
        /// Get all options
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Option> GetOptions()
        {           
            return Elements<Option>();
        }
        
    }
}
