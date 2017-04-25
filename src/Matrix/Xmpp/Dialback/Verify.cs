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
using Matrix.Crypt;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Dialback
{
    [XmppTag(Name = "verify", Namespace = Namespaces.ServerDialback)]
    public class Verify : XmppXElementWithAddress
    {
        public Verify() : base(Namespaces.ServerDialback, "verify")
        {
        }

        /// <summary>
        /// Generates a Dialback key as described in XEP-0185
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="receivingServer"></param>
        /// <param name="originatingServer"></param>
        /// <param name="streamId"></param>
        /// <returns></returns>
        public static string GenerateDialbackKey(
            string secret,
            string receivingServer,
            string originatingServer,
            string streamId)
        {
            return 
                Hash.HMACSHA256HashHex(
                    Hash.Sha256HashHex(secret),
                    String.Concat(receivingServer, " ", originatingServer, " ", streamId)
                );
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id
        {
            get { return GetAttribute("id"); }
            set { SetAttribute("id", value); }
        }

        /// <summary>
        /// The dialbackkey
        /// </summary>
        public string DialbackKey
        {
            get { return Value; }
            set { Value = value; }
        }


        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public VerifyType Type
        {
            get { return GetAttributeEnum<VerifyType>("type"); }
            set
            {
                if (value == VerifyType.None)
                    RemoveAttribute("type");
                else
                    SetAttribute("type", value.ToString().ToLower());
            }
        }
    }
}
