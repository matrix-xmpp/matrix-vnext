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

namespace Matrix.Xmpp.Server
{
    /// <summary>
    /// Represents a XMPP Server to server stream header
    /// </summary>
    public class Stream : Base.Stream
    {
        /// <summary>
        /// Initializes a Server Stream header.
        /// </summary>
        public Stream()
        {
            SetAttribute("xmlns", Namespaces.Server);
        }

        /// <summary>
        /// Initializes a Server Stream header.
        /// </summary>
        /// <param name="includeDialbackNameSpaceDeclaration">if set to <c>true</c> includes dialback name space declaration.</param>
        public Stream(bool includeDialbackNameSpaceDeclaration) : this()
        {
            if (includeDialbackNameSpaceDeclaration)
                AddDialbackNameSpaceDeclaration();
        }

        /// <summary>
        /// Adds the Dialback Namespace declaration to the stream element (xmlns:db='jabber:server:dialback')
        /// </summary>
        public void AddDialbackNameSpaceDeclaration()
        {
            AddNameSpaceDeclaration("db", Namespaces.ServerDialback);
        }
    }
}
