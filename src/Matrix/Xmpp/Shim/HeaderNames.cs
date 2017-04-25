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

namespace Matrix.Xmpp.Shim
{
    /// <summary>
    /// Enum HeaderNames
    /// </summary>
    public enum HeaderNames
    {
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Accept")]        
        Accept,
 
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Accept-Charset")]
        AcceptCharset,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Accept-Encoding")]
        AcceptEncoding,

        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Accept-Language")]
        AcceptLanguage,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Accept-Ranges")]
        AcceptRanges,
        
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Age")]
        Age,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Alert-Info")]
        AlertInfo,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Allow")]
        Allow,
  
        /// <summary>
        /// see RFC 2617
        /// </summary>
        [Name("Authentication-Info")]
        AuthenticationInfo,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Cache-Control")]
        CacheControl,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Call-ID")]
        CallId,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Call-Info")]
        CallInfo,
  
        /// <summary>
        /// a level within a classification scheme
        /// </summary>
        [Name("Classification")]
        Classification,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Comments")]
        Comments,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Connection")]
        Connection,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Contact")]
        Contact,

        /// <summary>
        /// see RFC 2045
        /// </summary>
        [Name("Content-Description")]
        ContentDescription,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Content-Disposition")]
        ContentDisposition,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Content-Encoding")]
        ContentEncoding,
  
        /// <summary>
        /// see RFC 2045
        /// </summary>
        [Name("Content-ID")]
        ContentId,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Content-Language")]
        ContentLanguage,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Content-Length")]
        ContentLength,

        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Content-Location")]
        ContentLocation,
        
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Content-MD5")]
        ContentMd5,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Content-Range")]
        ContentRange,
  
        /// <summary>
        /// see RFC 2045
        /// </summary>
        [Name("Content-Transfer-Encoding")]
        ContentTransferEncoding,
  
        /// <summary>
        /// see RFC 2045 or RFC 2616
        /// </summary>
        [Name("Content-Type")]
        ContentType,

        /// <summary>
        /// an entity other than the primary Creator who helped to create a resource (RFC 2413)
        /// </summary>
        [Name("Contributor")]
        Contributor,

        /// <summary>
        /// the spatial or temporal characteristics of a resource (RFC 2413)
        /// </summary>
        [Name("Coverage")]
        Coverage,

        /// <summary>
        /// date and time of stanza creation in ISO 8601 format
        /// </summary>
        [Name("Created")]
        Created,

        /// <summary>
        /// the person or organization responsible for creating the content of a resource (RFC 2413)
        /// </summary>
        [Name("Creator")]
        Creator,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("CSeq")]
        CSeq,
  
        /// <summary>
        /// a string that conforms to the Date profile specified in XEP-0082
        /// </summary>
        [Name("Date")]
        Date,
  
        /// <summary>
        /// a string that conforms to the DateTime profile specified in XEP-0082
        /// </summary>
        [Name("DateTime")]
        DateTime,
  
        /// <summary>
        /// a textual description of the content of a resource (RFC 2413)
        /// </summary>
        [Name("Description")]
        Description,
  
        /// <summary>
        /// whether or not the stanza may be further distributed
        /// </summary>
        [Name("Distribute")]
        Distribute,

        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Error-Info")]
        ErrorInfo,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("ETag")]
        ETag,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Expect")]
        Expect,

        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Expires")]
        Expires,
  
        /// <summary>
        /// the data format of a resourcec (RFC 2413)
        /// </summary>
        [Name("Format")]
        Format,

        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Host")]
        Host,
  
        /// <summary>
        /// a string or number used to identity a resource (RFC 2413)
        /// </summary>
        [Name("Identifier")]
        Identifier,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("If-Match")]
        IfMatch,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("If-Modified-Since")]
        IfModifiedSince,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("If-None-Match")]
        IfNoneMatch,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("If-Range")]
        IfRange,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("If-Unmodified-Since")]
        IfUnmodifiedSince,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("In-Reply-To")]
        InReplyTo,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Keywords")]
        Keywords,
  
        /// <summary>
        /// the language in expressing the content of a resource (RFC 2413)
        /// </summary>
        [Name("Language")]
        Language,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Last-Modified")]
        LastModified,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Location")]
        Location,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Max-Forwards")]
        MaxForwards,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Message-ID")]
        MessageId,
  
        /// <summary>
        /// see RFC 2045
        /// </summary>
        [Name("MIME-Version")]
        MimeVersion,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Organization")]
        Organization,
    
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Pragma")]
        Pragma,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Priority")]
        Priority,
 
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Proxy-Authenticate")]
        ProxyAuthenticate,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Proxy-Authorization")]
        ProxyAuthorization,
  
        /// <summary>
        /// the entity responsible for making a resource available (RFC 2413)
        /// </summary>
        [Name("Publisher")]
        Publisher,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Range")]
        Range,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Received")]
        Received,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Record-Route")]
        RecordRoute,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("References")]
        References,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Referer")]
        Referer,
  
        /// <summary>
        /// the identifier of a second resource related to a primary resource (RFC 2413)
        /// </summary>
        [Name("Relation")]
        Relation,
  
        /// <summary>
        /// see RFC 3261 
        /// </summary>
        [Name("Reply-To")]
        ReplyTo,

        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Require")]
        Require,
 
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Resent-Bcc")]
        ResentBcc,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Resent-Cc")]
        ResentCc,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Resent-Date")]
        ResentDate,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Resent-From")]
        ResentFrom,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Resent-Message-Id")]
        ResentMessageId,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Resent-Sender")]
        ResentSender,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Resent-To")]
        ResentTo,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Retry-After")]
        RetryAfter,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Return-Path")]
        ReturnPath,
  
        /// <summary>
        /// the datetime associated with an email message, SIP exchange, or HTTP request (RFC 2822)
        /// </summary>
        [Name("RFC2822Date")]
        Rfc2822Date,
  
        /// <summary>
        /// a rights management statement, identifier, or link (RFC 2413)
        /// </summary>
        [Name("Rights")]
        Rights,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Route")]
        Route,
  
        /// <summary>
        /// see RFC 2822
        /// </summary>
        [Name("Sender")]
        Sender,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Server")]
        Server,
  
        /// <summary>
        /// information about the original source of the present resource (RFC 2413)
        /// </summary>
        [Name("Source")]
        Source,
  
        /// <summary>
        /// whether or not the stanza may be stored or archived (XEP-0131)
        /// </summary>
        [Name("Store")]
        Store,
  
        /// <summary>
        /// the human-readable topic of a message or resource (RFC 2822 or RFC 2413)
        /// </summary>
        [Name("Subject")]
        Subject,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Supported")]
        Supported,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("TE")]
        Te,
  
        /// <summary>
        /// a string that conforms to the Time profile specified in XEP-0082
        /// </summary>
        [Name("Time")]
        Time,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Timestamp")]
        Timestamp,
  
        /// <summary>
        /// the name given to a resource (RFC 2413)
        /// </summary>
        [Name("Title")]
        Title,

        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Trailer")]
        Trailer,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Transfer-Encoding")]
        TransferEncoding,
  
        /// <summary>
        /// a time to live for the stanza, in seconds
        /// </summary>
        [Name("TTL")]
        Ttl,
  
        /// <summary>
        /// the category of a resource (RFC 2413)
        /// </summary>
        [Name("Type")]
        Type,
  
        /// <summary>
        /// see RFC 3261
        /// </summary>
        [Name("Unsupported")]
        Unsupported,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Upgrade")]
        Upgrade,
        
        /// <summary>
        /// the time sensitivity of a stanza ("high", "medium", or "low")
        /// </summary>
        [Name("Urgency")]
        Urgency,
  
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("User-Agent")]
        UserAgent,
        
        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Vary")]
        Vary,

        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("Via")]
        Via,
  
        /// <summary>
        /// Warning
        /// </summary>
        [Name("Warning")]
        Warning,

        /// <summary>
        /// see RFC 2616
        /// </summary>
        [Name("WWW-Authenticate")]
        WwwAuthenticate,
    }
}
