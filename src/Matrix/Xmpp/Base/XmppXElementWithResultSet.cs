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

using Matrix.Xml;
using Matrix.Xmpp.ResultSetManagement;

namespace Matrix.Xmpp.Base
{
    public abstract class XmppXElementWithResultSet : XmppXElement
    {
        protected XmppXElementWithResultSet(string ns, string tagname) : base(ns, tagname)
        {
        }
        
        /// <summary>
        /// Gets or sets the result set.
        /// </summary>
        /// <value>
        /// The result set.
        /// </value>
        public Set ResultSet
        {
            get { return Element<Set>(); }
            set { Replace(value); }
        }
    }
}
