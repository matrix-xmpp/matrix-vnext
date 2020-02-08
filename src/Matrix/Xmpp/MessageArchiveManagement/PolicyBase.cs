/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

namespace Matrix.Xmpp.MessageArchiveManagement
{
    using System.Collections.Generic;
    using Xml;
    
    public abstract class PolicyBase : XmppXElement
    {
        protected PolicyBase(string tagName)
            : base(Namespaces.MessageArchiveManagement, tagName)
        {
        }

        /// <summary>
        /// Adds an Item.
        /// </summary>
        /// <param name="jid">The item.</param>
        /// <returns></returns>
        public Jid AddJid(Matrix.Jid jid)
        {
            var j = new Jid(jid);
            Add(j);

            return j;
        }

        /// <summary>
        /// Adds an Item.
        /// </summary>
        /// <param name="jid">The item.</param>
        public void AddJid(Jid jid)
        {
            Add(jid);
        }

        /// <summary>
        /// Gets the jids.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Jid> GetJids()
        {
            return Elements<Jid>();
        }
    }
}