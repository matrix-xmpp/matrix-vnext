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

namespace Matrix.Xmpp.Avatar
{
    using Matrix.Attributes;
    using Matrix.Xmpp.Base;
    using System;

    /// <summary>
    /// Represents the avatar info element
    /// </summary>
    [XmppTag(Name = "info", Namespace = Namespaces.AvatarMetadata)]
    public class Info : XmppXElementWithIdAttribute
    {
        /*
         <metadata xmlns='urn:xmpp:avatar:metadata'>
          <info bytes='12345'
                height='64'
                id='111f4b3c50d7b0df729d299bc6f8e9ef9066971f'
                type='image/png'
                width='64'/>
        </metadata>
        */

        public Info()
            : base(Namespaces.AvatarMetadata, "info")
        {            
        }

        /// <summary>
        /// Gets or sets the byte count
        /// </summary>
        public int CountBytes
        {
            get { return GetAttributeInt("bytes"); }
            set { SetAttribute("bytes", value); }
        }

        /// <summary>
        /// Gets or sets the image width in pixels
        /// </summary>
        public int Width
        {
            get { return GetAttributeInt("width"); }
            set { SetAttribute("width", value); }
        }

        /// <summary>
        /// Gets or sets the image height in pixels
        /// </summary>
        public int Height
        {
            get { return GetAttributeInt("height"); }
            set { SetAttribute("height", value); }
        }

        /// <summary>
        /// Gets or sets the IANA-registered content type of the image data. (eg: image/png)
        /// </summary>
        public string Type
        {
            get { return GetAttribute("type"); }
            set { SetAttribute("type", value); }
        }

        /// <summary>
        /// Gets or sets the http: or https: URL at which the image data file is hosted.
        /// This property MUST NOT be set unless the image data file can be retrieved via HTTP.
        /// </summary>
        public Uri Uri
        {
            get { return GetAttributeUri("url"); }
            set { SetAttribute("url", value); }
        }
    }
}
