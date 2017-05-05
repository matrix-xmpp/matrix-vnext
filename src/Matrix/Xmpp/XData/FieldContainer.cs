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

using Matrix.Xml;

namespace Matrix.Xmpp.XData
{
    /// <summary>
    /// Bass class for all XData classes that contain XData fields
    /// </summary>
    public abstract class FieldContainer : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldContainer"/> class.
        /// </summary>
        /// <param name="tagname">The tagname.</param>
        protected FieldContainer(string tagname) : base(Namespaces.XData, tagname)
        {
        }

        #region << Public Methods >>
        /// <summary>
        /// Add a field
        /// </summary>
        /// <returns></returns>
        public Field AddField()
        {
            var f = new Field();
            Add(f);
            return f;
        }
        
        /// <summary>
        /// Add a fields
        /// </summary>
        /// <param name="field"></param>
        public Field AddField(Field field)
        {
            Add(field);
            return field;
        }

        /// <summary>
        /// Add a field with the given var and value.
        /// </summary>
        /// <param name="var">The var name.</param>
        /// <param name="val">The value of the field</param>
        /// <returns></returns>
        public Field AddField(string var, string val)
        {
            var f = new Field(var, val);
            Add(f);
            return f;
        }

        /// <summary>
        /// Add a field with the given var, value and tyoe.
        /// </summary>
        /// <param name="var">The var name.</param>
        /// <param name="val">The value of the field</param>
        /// <param name="fieldType">The type of the field.</param>
        /// <returns></returns>
        public Field AddField(string var, string val, FieldType fieldType)
        {
            var f = new Field(var, val, fieldType);
            Add(f);
            return f;
        }

        /// <summary>
        /// Determines whether the specified field exists.
        /// </summary>
        /// <param name="var">The var.</param>
        /// <returns>
        /// 	<c>true</c> if the specified var has field; otherwise, <c>false</c>.
        /// </returns>
        public bool HasField(string var)
        {
            return GetFields().Any(f => f.Var == var);            
        }

        /// <summary>
        /// Retrieve a field with the given "var"
        /// </summary>
        /// <param name="var"></param>
        /// <returns>the field or null when the field does not exist</returns>
        public Field GetField(string var)
        {
            return GetFields().FirstOrDefault(f => f.Var == var);            
        }

        /// <summary>
        /// Remove fields
        /// </summary>
        /// <param name="var">var of the fields </param>
        public void RemoveField(string var)
        {
            GetFields().Where(f => f.Var == var).Remove();
        }

        /// <summary>
        /// Remove all fields
        /// </summary>
        public void RemoveAllFields()
        {
            GetFields().Remove();
        }
        
        /// <summary>
        /// Gets a list of all form fields
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Field> GetFields()
        {
            return Elements<Field>();            
        }

        /// <summary>
        /// Gets an array of all form fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public Field[] Fields
        {
            get { return GetFields().ToArray(); }
            set
            {
                RemoveAllFields();
                foreach (var field in value)
                    AddField(field);
            }
        }
        #endregion
    }
}
