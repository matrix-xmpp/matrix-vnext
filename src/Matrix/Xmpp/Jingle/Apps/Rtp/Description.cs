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

namespace Matrix.Xmpp.Jingle.Apps.Rtp
{
    [XmppTag(Name = "description", Namespace = Namespaces.JingleAppsRtp)]
    public class Description : XmppXElement
    {
        public Description()
            : base(Namespaces.JingleAppsRtp, "description")
        {
        }

        /// <summary>
        /// specifies the media type, such as "audio" or "video".
        /// </summary>
        public Media Media
        {
            get { return GetAttributeEnum<Media>("media"); }
            set { SetAttribute("media", value.ToString().ToLower()); }
        }

        /// <summary>
        /// The 32-bit synchronization source for this media stream, as defined in RFC 3550.
        /// </summary>
        public string Ssrc
        {
            get { return GetAttribute("ssrc"); }
            set { SetAttribute("ssrc", value); }
        }


        #region << PayloadType properties >>
        /// <summary>
        /// Adds the type of the payload.
        /// </summary>
        /// <returns></returns>
        public PayloadType AddPayloadType()
        {
            var payload = new PayloadType();
            Add(payload);

            return payload;
        }
        
        /// <summary>
        /// Adds the payload.
        /// </summary>
        /// <param name="payload">The payload.</param>
        public void AddPayloadType(PayloadType payload)
        {
            Add(payload);
        }
        
        /// <summary>
        /// Addpayloads the types.
        /// </summary>
        /// <param name="payloadTypes">The payload types.</param>
        public void AddPayloadTypes(PayloadType[] payloadTypes)
        {
            foreach (var payload in payloadTypes)
                Add(payload);
        }

        /// <summary>
        /// Gets the payload types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PayloadType> GetPayloadTypes()
        {
            return Elements<PayloadType>();
        }

        /// <summary>
        /// Sets the payloadType.
        /// </summary>
        /// <param name="payloadTypes">The payload types.</param>
        public void SetPayloadTypes(IEnumerable<PayloadType> payloadTypes)
        {
            RemoveAllPayloadTypes();
            foreach (PayloadType cand in payloadTypes)
                AddPayloadType(cand);
        }

        /// <summary>
        /// Removes all payload types.
        /// </summary>
        public void RemoveAllPayloadTypes()
        {
            RemoveAll<PayloadType>();
        }
        #endregion
    }
}
