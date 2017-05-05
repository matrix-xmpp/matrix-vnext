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

namespace Matrix.Xmpp.SecurityLabels
{
    /// <summary>
    /// Contains a display string for use by implementations which are unable to utilize the applicable security policy 
    /// to generate display markings.
    /// </summary>
    [XmppTag(Name = "displaymarking", Namespace = Namespaces.SecurityLabel)]
    public class DisplayMarking : XmppXElement
    {
        /*
         * The fgcolor= default is black. The bgcolor= default is white.
         */

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayMarking"/> class.
        /// </summary>
        public DisplayMarking() : base(Namespaces.SecurityLabel, "displaymarking")
        {
        }

        /// <summary>
        /// Gets or sets the color of the foreground. Default is Black
        /// </summary>
        /// <value>
        /// The color of the foreground.
        /// </value>
        public Color ForegroundColor
        {
            //<xs:attribute name="fgcolor" type="color" use="optional" default="black"/>
            get
            {
                if (!HasAttribute("fgcolor"))
                    return Color.Black;
                
                return GetAttributeEnum<Color>("fgcolor");
            }
            set { SetAttribute("fgcolor", value.ToString().ToLower()); }
        }

        /// <summary>
        /// Gets or sets the color of the background. Default is White.
        /// </summary>
        /// <value>
        /// The color of the background.
        /// </value>
        public Color BackgroundColor
        {
            //<xs:attribute name="bgcolor" type="color" use="optional" default="white"/>
            get
            {
                if (!HasAttribute("bgcolor"))
                    return Color.White;

                return GetAttributeEnum<Color>("bgcolor");
            }
            set { SetAttribute("bgcolor", value.ToString().ToLower()); }
        }

        public string ForegroundHexColor
        {
            //<xs:attribute name="fgcolor" type="color" use="optional" default="black"/>
            get { return GetAttribute("fgcolor"); }
            set { SetAttribute("fgcolor", value); }
        }

        public string BackgroundHexColor
        {
            //<xs:attribute name="bgcolor" type="color" use="optional" default="white"/>
            get
            {
                return GetAttribute("bgcolor");
            }
            set { SetAttribute("bgcolor", value); }
        }
    }
}
