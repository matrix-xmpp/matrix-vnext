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

namespace Matrix.Xmpp.ExtendedStanzaAddressing
{
    [XmppTag(Name = "addresses", Namespace = Namespaces.ExtendedStanzaAdressing)]
    public class Addresses : XmppXElement
    {
        public Addresses() : base(Namespaces.ExtendedStanzaAdressing, "addresses")
        {
        }

        #region << Address properties >>
        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <returns></returns>
        public Address AddAddress()
        {
            var address = new Address();
            Add(address);

            return address;
        }
        
        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="address">The address.</param>
        public void AddAddress(Address address)
        {
            Add(address);
        }
        
        /// <summary>
        /// Gets the addresses.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Address> GetAddresses()
        {
            return Elements<Address>();
        }

        /// <summary>
        /// Sets the addresses.
        /// </summary>
        /// <param name="addresses">The items.</param>
        public void SetAddresses(IEnumerable<Address> addresses)
        {
            RemoveAllAddresses();
            foreach (Address address in addresses)
                AddAddress(address);
        }

        /// <summary>
        /// Removes all addresses.
        /// </summary>
        public void RemoveAllAddresses()
        {
            RemoveAll<Address>();
        }
        #endregion
    }
}
