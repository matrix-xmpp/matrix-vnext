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
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.GeoLoc
{
    [XmppTag(Name = "geoloc", Namespace = Namespaces.Geoloc)]
    public class GeoLoc : XmppXElement
    {
        #region  Schema
        /*
            *<xs:element name='accuracy' minOccurs='0' type='xs:decimal'/>
            *<xs:element name='alt' minOccurs='0' type='xs:decimal'/>
            *<xs:element name='area' minOccurs='0' type='xs:string'/>
            *<xs:element name='bearing' minOccurs='0' type='xs:decimal'/>
            *<xs:element name='building' minOccurs='0' type='xs:string'/>
            *<xs:element name='country' minOccurs='0' type='xs:string'/>
            *<xs:element name='countrycode' minOccurs='0' type='xs:string'/>
            *<xs:element name='datum' minOccurs='0' type='xs:string'/>
            *<xs:element name='description' minOccurs='0' type='xs:string'/>
            *<xs:element name='error' minOccurs='0' type='xs:decimal'/>
            *<xs:element name='floor' minOccurs='0' type='xs:string'/>
            *<xs:element name='lat' minOccurs='0' type='xs:decimal'/>
            *<xs:element name='locality' minOccurs='0' type='xs:string'/>
            *<xs:element name='lon' minOccurs='0' type='xs:decimal'/>
            *<xs:element name='postalcode' minOccurs='0' type='xs:string'/>
            *<xs:element name='region' minOccurs='0' type='xs:string'/>
            *<xs:element name='room' minOccurs='0' type='xs:string'/>
            *<xs:element name='speed' minOccurs='0' type='xs:decimal'/>
            *<xs:element name='street' minOccurs='0' type='xs:string'/>
            *<xs:element name='text' minOccurs='0' type='xs:string'/>
            <xs:element name='timestamp' minOccurs='0' type='xs:dateTime'/>
            *<xs:element name='uri' minOccurs='0' type='xs:anyURI'/>
         */
        #endregion
       
        /// <summary>
        /// XEP-0080 Geographical Location (GeoLoc)
        /// Defines a format for capturing data about an entity's geographical location (geoloc).
        /// The format defined herein is intended to provide a semi-structured format for 
        /// describing a geographical location that may change fairly frequently, 
        /// where the geoloc information is provided as Global Positioning System (GPS) coordinates.
        /// </summary>
        public GeoLoc() : base(Namespaces.Geoloc, "geoloc")
        {
        }

        #region << string members >>
        /// <summary>
        /// A natural-language name for or description of the location.
        /// </summary>
        public string Description
        {
            get { return GetTag("description"); }
            set { SetTag("description", value); }
        }

        /// <summary>
        /// A code used for postal delivery (eg. 74080).
        /// </summary>
        public string PostalCode
        {
            get { return GetTag("postalcode"); }
            set { SetTag("postalcode", value); }
        }

        /// <summary>
        /// An administrative region of the nation, such as a state or province.
        /// </summary>
        public string Region
        {
            get { return GetTag("region"); }
            set { SetTag("region", value); }
        }

        /// <summary>
        /// A particular room in a building.
        /// </summary>
        public string Room
        {
            get { return GetTag("room"); }
            set { SetTag("room", value); }
        }

        /// <summary>
        /// A thoroughfare within the locality, or a crossing of two thoroughfares.
        /// </summary>
        public string Street
        {
            get { return GetTag("street"); }
            set { SetTag("street", value); }
        }

        /// <summary>
        /// A catch-all element that captures any other information about the location.
        /// </summary>
        public new string Text
        {
            get { return GetTag("text"); }
            set { SetTag("text", value); }
        }

        /// <summary>
        /// The GPS datum.
        /// </summary>
        public string Datum
        {
            get { return GetTag("datum"); }
            set { SetTag("datum", value); }
        }

        /// <summary>
        /// A specific building on a street or in an area.
        /// </summary>
        public string Building
        {
            get { return GetTag("building"); }
            set { SetTag("building", value); }
        }

        /// <summary>
        /// The nation where the user is located.
        /// </summary>
        public string Country
        {
            get { return GetTag("country"); }
            set { SetTag("country", value); }
        }

        /// <summary>
        /// The ISO 3166 two-letter country code (eg. "US").
        /// </summary>
        public string CountryCode
        {
            get { return GetTag("countrycode"); }
            set { SetTag("countrycode", value); }
        }

        /// <summary>
        /// A named area such as a campus or neighborhood.
        /// </summary>
        public string Area
        {
            get { return GetTag("area"); }
            set { SetTag("area", value); }
        }

        /// <summary>
        /// A locality within the administrative region, such as a town or city.
        /// </summary>
        public string Locality
        {
            get { return GetTag("locality"); }
            set { SetTag("locality", value); }
        }

        /// <summary>
        /// A particular floor in a building.
        /// </summary>
        public string Floor
        {
            get { return GetTag("floor"); }
            set { SetTag("floor", value); }
        }
        #endregion
        
        #region << double members >>
        /// <summary>
        /// Altitude in meters above or below sea level
        /// </summary>
        public double Altitude
        {
            get { return GetTagDouble("alt"); }
            set { SetTag("alt", value); }
        }
        
        /// <summary>
        /// GPS bearing (direction in which the entity is heading to reach its next waypoint),
        /// measured in decimal degrees relative to true north.
        /// </summary>
        public double Bearing
        {
            get { return GetTagDouble("bearing"); }
            set { SetTag("bearing", value); }
        }

        /// <summary>
        /// Horizontal GPS error in meters. This property obsoletes the Error property.
        /// </summary>
        /// <value>
        /// The accuracy.
        /// </value>
        public double Accuracy
        {
            get { return GetTagDouble("accuracy"); }
            set { SetTag("accuracy", value); }
        }

        /// <summary>
        /// The speed at which the entity is moving, in meters per second.
        /// </summary>
        public double Speed
        {
            get { return GetTagDouble("speed"); }
            set { SetTag("speed", value); }
        }

        /// <summary>
        /// Latitude in decimal degrees North.
        /// </summary>
        public double Latitude
        {
            get { return GetTagDouble("lat"); }
            set { SetTag("lat", value); }
        }

        /// <summary>
        /// Longitude in decimal degrees East.
        /// </summary>
        public double Longitude
        {
            get { return GetTagDouble("lon"); }
            set { SetTag("lon", value); }
        }

        /// <summary>
        /// Horizontal GPS error in arc minutes.
        /// This property is deprecated in favor of Accuracy.
        /// </summary>
        public double Error
        {
            get { return GetTagDouble("error"); }
            set { SetTag("error", value); }
        }
        #endregion

        /// <summary>
        /// UTC timestamp specifying the moment when the reading was taken.
        /// </summary>
        public DateTime Timestamp
        {
            get { return Matrix.Time.Iso8601Date(GetTag("timestamp")); }
            set { SetTag("timestamp", Matrix.Time.Iso8601Date(value)); }
        }

        /// <summary>
        /// A URI or URL pointing to information about the location.
        /// </summary>
        public System.Uri Uri
        {
            get { return new System.Uri(GetTag("uri")); }
            set { SetTag("uri", value.ToString()); }
        }
    }
}
