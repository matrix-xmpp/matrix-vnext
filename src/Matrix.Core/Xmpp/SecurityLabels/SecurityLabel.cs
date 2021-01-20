using System.Collections.Generic;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.SecurityLabels
{
    /// <summary>
    /// A XEP-0258 security label
    /// </summary>
    [XmppTag(Name = "securitylabel", Namespace = Namespaces.SecurityLabel)]
    public class SecurityLabel : XmppXElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityLabel"/> class.
        /// </summary>
        public SecurityLabel() : base(Namespaces.SecurityLabel, "securitylabel")
        {
        }

        /// <summary>
        /// Gets or sets the display marking.
        /// </summary>
        /// <value>
        /// The display marking.
        /// </value>
        public DisplayMarking DisplayMarking
        {
            get { return Element<DisplayMarking>(); }
            set { Replace(value); }
        }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public Label Label
        {
            get { return Element<Label>(); }
            set { Replace(value); }
        }

        #region << EquivalentLabel collection properties >>
        /// <summary>
        /// Adds the equivalent label.
        /// </summary>
        /// <returns></returns>
        public EquivalentLabel AddEquivalentLabel()
        {
            var label = new EquivalentLabel();
            Add(label);

            return label;
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="label">The label.</param>
        public void AddItem(EquivalentLabel label)
        {
            Add(label);
        }

        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="labels">The labels.</param>
        public void AddItems(EquivalentLabel[] labels)
        {
            foreach (var label in labels)
                Add(label);
        }

        /// <summary>
        /// Gets the equivalent labels.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EquivalentLabel> GetEquivalentLabels()
        {
            return Elements<EquivalentLabel>();
        }

        /// <summary>
        /// Sets the items.
        /// </summary>
        /// <param name="labels">The labels.</param>
        public void SetItems(IEnumerable<EquivalentLabel> labels)
        {
            RemoveAllEquivalentLabels();
            foreach (EquivalentLabel label in labels)
                AddItem(label);
        }

        /// <summary>
        /// Removes all equivalent labels.
        /// </summary>
        public void RemoveAllEquivalentLabels()
        {
            RemoveAll<EquivalentLabel>();
        }
        #endregion
    }
}
