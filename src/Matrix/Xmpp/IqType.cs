/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

namespace Matrix.Xmpp
{
    /// <summary>
    /// The 'type' attribute is REQUIRED for IQ stanzas.
    /// </summary>
    public enum IqType
    {
        /// <summary>
        /// The stanza is a request for information or requirements.
        /// </summary>
        [Name("get")]
        Get,
        
        /// <summary>
        /// The stanza provides required data, sets new values, or replaces existing values.
        /// </summary>
        [Name("set")]
        Set,
        
        /// <summary>
        /// The stanza is a response to a successful get or set request.
        /// </summary>
        [Name("result")]
        Result,
        
        /// <summary>
        /// An error has occurred regarding processing or delivery of a previously-sent get or set (see Stanza Errors (Stanza Errors)).
        /// </summary>
        [Name("error")]
        Error
    }
}
