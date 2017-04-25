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

using System;
using System.IO;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Vcard
{
    /// <summary>
    /// Photo in <see cref="Vcard"/>
    /// </summary>
    [XmppTag(Name = "PHOTO", Namespace = Namespaces.Vcard)]
    public class Photo : XmppXElement
    {
        #region Schema
        /*
            <!-- Photograph property. Value is either a BASE64 encoded
                binary value or a URI to the external content. -->
            <!ELEMENT PHOTO ((TYPE, BINVAL) | EXTVAL)>
        */
        #endregion

        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Photo"/> class.
        /// </summary>
        public Photo()
            : base(Namespaces.Vcard, "PHOTO")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Photo"/> class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        public Photo(System.Uri uri)
            : this()
        {
            Extval = uri.AbsoluteUri;
        }
        #endregion


        /// <summary>
        /// Gets or sets the image type.
        /// </summary>
        /// <value>The image type.</value>
        public string Type
        {
            get { return GetTag("TYPE"); }
            set { SetTag("TYPE", value); }
        }

        /// <summary>
        /// Gets or sets the binval.
        /// </summary>
        /// <value>The binval.</value>
        public byte[] Binval
        {
            get
            {
                if (HasTag("BINVAL"))
                    return Convert.FromBase64String(GetTag("BINVAL"));
                return
                    null;
            }
            // there is a bug in MLink which does not accept base64 without linebreaks

            set { SetTag("BINVAL", Convert.ToBase64String(value)); }
        }

        #region << internal members >>
        /// <summary>
        /// Gets or sets the extval.
        /// </summary>
        /// <value>The extval.</value>
        internal string Extval
        {
            get { return GetTag("EXTVAL"); }
            set { SetTag("EXTVAL", value); }
        }


        /// <summary>
        /// Sets the image bytes.
        /// </summary>
        /// <param name="image">The image.</param>
        internal void SetImageBytes(byte[] image)
        {
            Binval = image;
        }
        #endregion
    }
}
