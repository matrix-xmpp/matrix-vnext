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

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "affiliations", Namespace = Namespaces.Pubsub)]
    public class Affiliations : Base.Affiliations
    {
        public Affiliations() : base(Namespaces.Pubsub)
        {
        }
        
        /// <summary>
        /// Adds the affiliation.
        /// </summary>
        /// <returns></returns>
        public Affiliation AddAffiliation()
        {
            var affiliation = new Affiliation();
            Add(affiliation);

            return affiliation;
        }
        
        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="affiliation">The affiliation.</param>
        public void AddAffiliation(Affiliation affiliation)
        {
            Add(affiliation);
        }

        /// <summary>
        /// Gets the affiliations.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Affiliation> GetAffiliations()
        {
            return Elements<Affiliation>();
        }

        /// <summary>
        /// Sets the items.
        /// </summary>
        /// <param name="affiliations">The affiliations.</param>
        public void SetAffiliations(IEnumerable<Affiliation> affiliations)
        {
            RemoveAllAffiliations();
            foreach (Affiliation affiliation in affiliations)
                AddAffiliation(affiliation);
        }
        
        /// <summary>
        /// Removes all affiliations.
        /// </summary>
        public void RemoveAllAffiliations()
        {
            RemoveAll<Affiliation>();
        }
    }
}
