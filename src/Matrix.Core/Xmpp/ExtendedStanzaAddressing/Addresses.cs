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
