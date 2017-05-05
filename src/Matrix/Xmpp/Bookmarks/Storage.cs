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

namespace Matrix.Xmpp.Bookmarks
{
    [XmppTag(Name = "storage", Namespace = Namespaces.StorageBookmarks)]
    public class Storage : XmppXElement
    {
        public Storage()
            : base(Namespaces.StorageBookmarks, "storage")
        {
        }

        #region << Item properties >>
        /// <summary>
        /// Adds a conference.
        /// </summary>
        /// <returns></returns>
        public Conference AddConference()
        {
            var conf = new Conference();
            Add(conf);

            return conf;
        }

        /// <summary>
        /// Adds a conference.
        /// </summary>
        /// <param name="conference">The conference.</param>
        public void AddConference(Conference conference)
        {
            Add(conference);
        }

        /// <summary>
        /// Gets all conferences.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Conference> GetConferences()
        {
            return Elements<Conference>();
        }
        
        /// <summary>
        /// Sets the conferences.
        /// </summary>
        /// <param name="conferences">The conferences.</param>
        public void SetConferences(IEnumerable<Conference> conferences)
        {
            RemoveAllConferences();
            foreach (Conference conf in conferences)
                AddConference(conf);
        }

        /// <summary>
        /// Removes all Items.
        /// </summary>
        public void RemoveAllConferences()
        {
            RemoveAll<Conference>();
        }
        #endregion
    }
}
