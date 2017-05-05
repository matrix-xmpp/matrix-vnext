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

namespace Matrix.Xmpp.Muc
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Item : Base.Item
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        /// <param name="ns">The namespace.</param>
        protected Item(string ns)
            : base(ns)
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public Role Role
        {
            get { return GetAttributeEnum<Role>("role"); }
            set { SetAttribute("role", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Gets or sets the affiliation.
        /// </summary>
        /// <value>The affiliation.</value>
        public Affiliation Affiliation
        {
            get { return GetAttributeEnum<Affiliation>("affiliation"); }
            set { SetAttribute("affiliation", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>The nickname.</value>
        public string Nickname
        {
            get { return GetAttribute("nick"); }
            set { SetAttribute("nick", value); }
        }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        /// <value>The reason.</value>
        public string Reason
        {
            set { SetTag("reason", value); }
            get { return GetTag("reason"); }
        }        
    }
}
