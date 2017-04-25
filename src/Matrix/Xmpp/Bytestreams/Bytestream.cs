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

namespace Matrix.Xmpp.Bytestreams
{
    [XmppTag(Name = Tag.Query, Namespace = Namespaces.Bytestreams)]
    public class Bytestream : XmppXElement
    {
        public Bytestream() : base(Namespaces.Bytestreams, Tag.Query)
        {
        }

        public string Sid
        {
            get { return GetAttribute("sid");}
            set { SetAttribute("sid", value);}
        }
        
        public Mode Mode
        {
            get { return GetAttributeEnum<Mode>("mode"); }
            set
            {
                if (value == Mode.None)
                    RemoveAttribute("mode");
                
                SetAttribute("mode", value.ToString().ToLower());
            }
        }

        #region << streamhost members >>
        /// <summary>
        /// Adds a atreamhost.
        /// </summary>
        /// <returns></returns>
        public Streamhost AddStreamhost()
        {
            var shost = new Streamhost();
            Add(shost);

            return shost;
        }

        /// <summary>
        /// Adds the streamhost.
        /// </summary>
        /// <param name="shost">The streamhost.</param>
        public void AddStreamhost(Streamhost shost)
        {
            Add(shost);
        }
        /// <summary>
        /// Gets all streamhosts.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Streamhost> GetStreamhosts()
        {
            return Elements<Streamhost>();
        }

        /// <summary>
        /// Sets the streamhosts.
        /// </summary>
        /// <param name="shosts">The streamhosts.</param>
        public void SetStreamhosts(IEnumerable<Streamhost> shosts)
        {
            RemoveAllStreamhosts();
            foreach (Streamhost host in shosts)
                AddStreamhost(host);
        }

        /// <summary>
        /// Removes all streamhosts.
        /// </summary>
        public void RemoveAllStreamhosts()
        {
            RemoveAll<Streamhost>();
        }
        #endregion
        
        public Activate Activate
        {
            get { return Element<Activate>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }

        public StreamhostUsed StreamhostUsed
        {
            get { return Element<StreamhostUsed>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }
    }
}
