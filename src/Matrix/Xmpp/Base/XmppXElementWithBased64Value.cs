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

namespace Matrix.Xmpp.Base
{
    using Matrix.Xml;
    using System;

    public abstract class XmppXElementWithBased64Value : XmppXElement
    {
        protected XmppXElementWithBased64Value(string ns, string tag) : base(ns, tag)
        {
        }
        
        /// <summary>
        /// sets the value of a SASL step element (challenge, response, auth).
        /// The value gets is converted to Base64.
        /// </summary>
        public byte[] Bytes
        {
            get
            {
                if (Value == "")
                    return null;
                if (Value == "=")
                    return new byte[0];
                return Convert.FromBase64String(Value);
            }
            set
            {
                if (value == null)
                    if (Value.Length > 0)
                        Value = "";
                    else
                        return;
                else if (value.Length == 0)
                    Value = "=";
                else
                    Value = Convert.ToBase64String(value);
            }
        }        
    }
}
