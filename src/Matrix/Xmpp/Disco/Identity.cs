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

namespace Matrix.Xmpp.Disco
{
    [XmppTag(Name = "identity", Namespace = Namespaces.DiscoInfo)]
    public class Identity : XmppXElement, IEquatable<Identity>
    {
        #region << Constructors >>
        public Identity() : base(Namespaces.DiscoInfo, "identity")
        {
        }

        public Identity(string type, string name, string category) : this()
        {
            Type        = type;
            Name        = name;
            Category    = category;
        }

        public Identity(string type, string category) : this()
        {
            Type = type;
            Category = category;
        }
        #endregion

        #region << Properties >>
        /// <summary>
        /// type category name for the entity
        /// </summary>
        public string Type
        {
            get { return GetAttribute("type"); }
            set { SetAttribute("type", value); }
        }

        /// <summary>
        /// natural-language name for the entity
        /// </summary>
        public new string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// category name for the entity
        /// </summary>
        public string Category
        {
            get { return GetAttribute("category"); }
            set { SetAttribute("category", value); }
        }
        #endregion

        public string Key
        {
            get { return string.Format("{0}/{1}/{2}/{3}", Category, Type, XmlLanguage, Name); }
        }
        
        public bool Equals(Identity other)
        {
            return String.CompareOrdinal(Key, other.Key) == 0;
        }
    }
}
