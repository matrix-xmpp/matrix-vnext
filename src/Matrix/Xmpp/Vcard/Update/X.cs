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

using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Vcard.Update
{
    /// <summary>
    /// Vcard update
    /// </summary>
    [XmppTag(Name = "x", Namespace = Namespaces.VcardUpdate)]
    public class X : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="X"/> class.
        /// </summary>
        public X() 
            : base(Namespaces.VcardUpdate, "x")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="X"/> class.
        /// </summary>
        /// <param name="photo">The photo.</param>
        public X(string photo) : this()
        {
            Photo = photo;
        }
        #endregion

        /// <summary>
        /// Gets or sets the photo hash.
        /// </summary>
        /// <value>The photo.</value>
        public string Photo
        {
            get { return GetTag("photo"); }
            set
            {
                if (value == null)
                    RemoveTag("photo");
                else
                    SetTag("photo", value);
            }
        }
    }
}
