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
using Matrix.Xml;

namespace Matrix.Xmpp.Privacy
{
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.IqPrivacy)]
    public class Privacy : XmppXElement
    {
        public Privacy() : base(Namespaces.IqPrivacy, Tag.Query)
        {
        }
        
        /// <summary>
        /// Add a privacy list
        /// </summary>
        /// <param name="list"></param>
        public void AddList(List list)
        {
            Add(list);
        }
        
        /// <summary>
        /// get all Lists
        /// </summary>
        /// <returns></returns>
          
        public IEnumerable<List> GetLists()
        {
            return Elements<List>();
        }

        /// <summary>
        /// The active list
        /// </summary>
        public Active Active
        {
            get { return Element<Active>(); }
            set { Replace(value); }
        }
        
        /// <summary>
        /// The default list
        /// </summary>
        public Default Default
        {
            get { return Element<Default>(); }
            set { Replace(value); }
        }
    }
}
