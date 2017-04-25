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
