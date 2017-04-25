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

namespace Matrix.Xmpp.Vcard
{
    /// <summary>
    /// 
    /// </summary>
    [XmppTag(Name = "ADR", Namespace = Namespaces.Vcard)]
    public class Address : XmppXElement
    {
        #region Schema
        /*
          <!-- Structured address property. Address components with
            multiple values must be specified as a comma separated list
            of values. -->
          <!ELEMENT ADR (
            HOME?, 
            WORK?, 
            POSTAL?, 
            PARCEL?, 
            (DOM | INTL)?, 
            PREF?, 
            POBOX?, 
            EXTADD?, 
            STREET?, 
            LOCALITY?, 
            REGION?, 
            PCODE?, 
            CTRY?
          )>
          
            <!ELEMENT POBOX (#PCDATA)>
            <!ELEMENT EXTADD (#PCDATA)>
            <!ELEMENT STREET (#PCDATA)>
            <!ELEMENT LOCALITY (#PCDATA)>
            <!ELEMENT REGION (#PCDATA)>
            <!ELEMENT PCODE (#PCDATA)>
            <!ELEMENT CTRY (#PCDATA)>

        */
        #endregion

        #region << Constructors >>
        public Address()
            : base(Namespaces.Vcard, "ADR")
        {
        }
        #endregion

        /// <summary>
        /// Gets or sets a value indicating whether this instance is an international address.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is an international address; otherwise, <c>false</c>.
        /// </value>
        public bool IsInternational
        {
            get { return HasTag("INTL"); }
            set 
            { 
                AddOrRemoveTag("INTL", value);
                if (value)
                    IsDomestic = false;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a domestic delivery address.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is a domestic delivery address; otherwise, <c>false</c>.
        /// </value>
        public bool IsDomestic
        {
            get { return HasTag("DOM"); }
            set 
            {
                AddOrRemoveTag("DOM", value);
                if (value)
                    IsInternational = false;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a home address.
        /// </summary>
        /// <value><c>true</c> if this instance is a home address; otherwise, <c>false</c>.</value>
        public bool IsHome
        {
            get { return HasTag("HOME"); }
            set { AddFirstOrRemoveTag("HOME", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a work address.
        /// </summary>
        /// <value><c>true</c> if this instance is a work address; otherwise, <c>false</c>.</value>
        public bool IsWork
        {
            get { return HasTag("WORK"); }
            set { AddFirstOrRemoveTag("WORK", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a postal address.
        /// </summary>
        /// <value><c>true</c> if this instance is a postal address; otherwise, <c>false</c>.</value>
        public bool IsPostal
        {
            get { return HasTag("POSTAL"); }
            set { AddOrRemoveTag("POSTAL", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a parcel delivery address.
        /// </summary>
        /// <value><c>true</c> if this instance is parcel delivery address; otherwise, <c>false</c>.</value>
        public bool IsParcel
        {
            // TODO
            get { return HasTag("PARCEL"); }
            set { AddOrRemoveTag("PARCEL", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is a preferred address.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is a preferred address; otherwise, <c>false</c>.
        /// </value>
        public bool IsPreferred
        {
            get { return HasTag("PREF"); }
            set { AddOrRemoveTag("PREF", value); }         
        }

        /// <summary>
        /// Gets or sets the po box.
        /// </summary>
        /// <value>The po box.</value>
        public string PoBox
        {
            get { return GetTag("POBOX"); }
            set { SetTag("POBOX", value); }
        }

        /// <summary>
        /// Gets or sets the extra address.
        /// </summary>
        /// <value>The extra address.</value>
        public string ExtraAddress
        {
            get { return GetTag("EXTADD"); }
            set { SetTag("EXTADD", value); }
        }

        /// <summary>
        /// Gets or sets the street.
        /// </summary>
        /// <value>The street.</value>
        public string Street
        {
            get { return GetTag("STREET"); }
            set { SetTag("STREET", value); }
        }

        /// <summary>
        /// Gets or sets the locality.
        /// </summary>
        /// <value>The locality.</value>
        public string Locality
        {
            get { return GetTag("LOCALITY"); }
            set { SetTag("LOCALITY", value); }
        }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>The region.</value>
        public string Region
        {
            get { return GetTag("REGION"); }
            set { SetTag("REGION", value); }
        }

        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        /// <value>The post code.</value>
        public string PostCode
        {
            get { return GetTag("PCODE"); }
            set { SetTag("PCODE", value); }
        }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>The country.</value>
        public string Country
        {
            get { return GetTag("CTRY"); }
            set { SetTag("CTRY", value); }
        }
    }
}
