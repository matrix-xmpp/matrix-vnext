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

namespace Matrix.Xmpp.Sasl
{
    public enum SaslMechanism
    {
        [Name(null)]
        None,

        [Name("GSSAPI")]
        Gssapi,
        
        [Name("NTLM")]
        Ntlm,

        [Name("EXTERNAL")]
        External,

        [Name("SCRAM-SHA-1")]
        ScramSha1,
        
        [Name("ANONYMOUS")]
        Anonymous,

        [Name("PLAIN")]
        Plain,
        
        [Name("DIGEST-MD5")]
        DigestMd5,
        
        [Name("X-GOOGLE-TOKEN")]
        XGoogleToken,
        
        [Name("X-FACEBOOK-PLATFORM")]
        XFacebookPlatform,
        
        [Name("X-MESSENGER-OAUTH2")]
        XMessengerOauth2,
        
        [Name("X-OAUTH2")]
        XOauth2,
        
        [Name("CISCO-VTG-TOKEN")]
        CiscoVtgToken,
        
        [Name("WEBEX-TOKEN")]
        WebexToken
    }
}
