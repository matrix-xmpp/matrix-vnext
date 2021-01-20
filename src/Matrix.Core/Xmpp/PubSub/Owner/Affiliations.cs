using System.Collections.Generic;
using Matrix.Attributes;

namespace Matrix.Xmpp.PubSub.Owner
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = "affiliations", Namespace = Namespaces.PubsubOwner)]
    public class Affiliations : Base.Affiliations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Affiliations"/> class.
        /// </summary>
        public Affiliations() : base(Namespaces.PubsubOwner)
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
