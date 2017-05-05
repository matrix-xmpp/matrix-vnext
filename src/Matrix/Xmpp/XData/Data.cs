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
using Matrix.Attributes;

namespace Matrix.Xmpp.XData
{
    [XmppTag(Name = "x", Namespace = Namespaces.XData)]
    public class Data  : FieldContainer
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Data"/> class.
        /// </summary>
        public Data() : base("x")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Data"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public Data(FormType type)
            : this()
        {
            Type = type;
        }
        #endregion

        #region << Properties >>
        public string Title
        {
            get { return GetTag("title"); }
            set { SetTag("title", value); }
        }

        public string Instructions
        {
            get { return GetTag("instructions"); }
            set { SetTag("instructions", value); }
        }

        /// <summary>
        /// Type of thie XDATA Form.
        /// </summary>
        public FormType Type
        {
            get { return GetAttributeEnum<FormType>("type"); }
            set { SetAttribute("type", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Gets or sets the reported.
        /// </summary>
        /// <value>The reported.</value>
        public Reported Reported
        {
            get { return Element<Reported>(); }
            set { Replace(value); }
        }

        #endregion

        #region << public Methods >>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Item AddItem()
        {
            var i = new Item();
            Add(i);
            return i;
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public Item AddItem(Item item)
        {
            Add(item);
            return item;
        }

        /// <summary>
        /// Gets a list of all form items
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Item> GetItems()
        {
            return Elements<Item>();
        }
        #endregion
    }
}
