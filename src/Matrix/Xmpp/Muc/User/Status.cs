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

namespace Matrix.Xmpp.Muc.User
{
    /// <summary>
    /// Status
    /// </summary>
    [XmppTag(Name = "status", Namespace = Namespaces.MucUser)]
    public class Status : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        public Status() : base(Namespaces.MucUser, "status")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public Status(StatusCode code)
            : this()
        {
            Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Status"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public Status(int code)
            : this()
        {
            CodeInt = code;
        }
        #endregion
        
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The code as integer.</value>
        public int CodeInt
        {
            get { return GetAttributeInt("code"); }
            set { SetAttribute("code", value); }
        }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The code.</value>
        public StatusCode Code
        {
            get { return GetAttributeEnum<StatusCode>("code"); }
            set { SetAttribute("code", value.ToString()); }
        }
    }
}
