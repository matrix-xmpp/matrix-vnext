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
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.SecurityLabels
{
    [XmppTag(Name = "catalog", Namespace = Namespaces.SecurityLabelCatalog)]
    public class Catalog : XmppXElementWithAddressAndIdAttributeAndItemCollection<Item>
    {
        public Catalog() : base(Namespaces.SecurityLabelCatalog, "catalog")
        {
        }

        public string CatalogName
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        public string Description
        {
            get { return GetAttribute("desc"); }
            set { SetAttribute("desc", value); }
        }

        public bool Restrictive
        {
            get { return GetAttributeBool("restrict"); }
            set { SetAttribute("restrict", value); }
        }

        public int Size
        {
            get { return GetAttributeInt("size"); }
            set { SetAttribute("size", value); }
        }
    }
}
