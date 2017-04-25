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

namespace Matrix.Xmpp.Stream
{
    public enum ErrorCondition
    {
        #region Docs
        /*
            RFC 4.7.3.  Defined Conditions

            The following stream-level error conditions are defined:

            * <bad-format/> -- the entity has sent XML that cannot be processed; this error MAY be used instead of the more 
            specific XML-related errors, such as <bad-namespace-prefix/>, <invalid-xml/>, <restricted-xml/>, 
            <unsupported-encoding/>, and <xml-not-well-formed/>, although the more specific errors are preferred.
        
            * <bad-namespace-prefix/> -- the entity has sent a namespace prefix that is unsupported, or has sent no 
            namespace prefix on an element that requires such a prefix (see XML Namespace Names and Prefixes 
            (XML Namespace Names and Prefixes)).
        
            * <conflict/> -- the server is closing the active stream for this entity because a new stream has been 
            initiated that conflicts with the existing stream.
        
            * <connection-timeout/> -- the entity has not generated any traffic over the stream for some period of 
            time (configurable according to a local service policy).
        
            * <host-gone/> -- the value of the 'to' attribute provided by the initiating entity in the stream header 
            corresponds to a hostname that is no longer hosted by the server.
     
            * <host-unknown/> -- the value of the 'to' attribute provided by the initiating entity in the 
            stream header does not correspond to a hostname that is hosted by the server.
     
            * <improper-addressing/> -- a stanza sent between two servers lacks a 'to' or 'from' attribute 
            (or the attribute has no value).
        
            * <internal-server-error/> -- the server has experienced a misconfiguration or an otherwise-undefined 
            internal error that prevents it from servicing the stream.
        
            * <invalid-from/> -- the JID or hostname provided in a 'from' address does not match an authorized JID 
            or validated domain negotiated between servers via SASL or dialback, or between a client and a server 
            via authentication and resource binding.
     
            * <invalid-id/> -- the stream ID or dialback ID is invalid or does not match an ID previously provided.
        
            * <invalid-namespace/> -- the streams namespace name is something other than 
            "http://etherx.jabber.org/streams" or the dialback namespace name is something other than 
            "jabber:server:dialback" (see XML Namespace Names and Prefixes (XML Namespace Names and Prefixes)).
        
            * <invalid-xml/> -- the entity has sent invalid XML over the stream to a server that performs 
            validation (see Validation (Validation)).
     
            * <not-authorized/> -- the entity has attempted to send data before the stream has been authenticated,
            or otherwise is not authorized to perform an action related to stream negotiation; the receiving entity 
            MUST NOT process the offending stanza before sending the stream error.
     
            * <policy-violation/> -- the entity has violated some local service policy; the server MAY choose to 
            specify the policy in the <text/> element or an application-specific condition element.
        
            * <remote-connection-failed/> -- the server is unable to properly connect to a remote entity that is 
            required for authentication or authorization.
        
            * <resource-constraint/> -- the server lacks the system resources necessary to service the stream.
        
            * <restricted-xml/> -- the entity has attempted to send restricted XML features such as a comment, 
            processing instruction, DTD, entity reference, or unescaped character (see Restrictions (Restrictions)).
     
            * <see-other-host/> -- the server will not provide service to the initiating entity but is redirecting 
            traffic to another host; the server SHOULD specify the alternate hostname or IP address 
            (which MUST be a valid domain identifier) as the XML character data of the <see-other-host/> element.
     
            * <system-shutdown/> -- the server is being shut down and all active streams are being closed.
     
            * <undefined-condition/> -- the error condition is not one of those defined by the other conditions 
            in this list; this error condition SHOULD be used only in conjunction with an application-specific condition.
        
            * <unsupported-encoding/> -- the initiating entity has encoded the stream in an encoding that is not 
            supported by the server (see Character Encoding (Character Encoding)).
     
            * <unsupported-stanza-type/> -- the initiating entity has sent a first-level child of the stream 
            that is not supported by the server.
     
            * <unsupported-version/> -- the value of the 'version' attribute provided by the initiating entity 
            in the stream header specifies a version of XMPP that is not supported by the server; the server 
            MAY specify the version(s) it supports in the <text/> element.
     
            * <xml-not-well-formed/> -- the initiating entity has sent XML that is not well-formed
        */
        #endregion

        /// <summary>
        /// unknown error condition
        /// </summary>
        [Name("UnknownCondition")]
        UnknownCondition = -1,

        /// <summary>
        /// the entity has sent XML that cannot be processed; this error MAY be used instead of the more 
        /// specific XML-related errors, such as &lt;bad-namespace-prefix/&gt;, &lt;invalid-xml/&gt;, 
        /// &lt;restricted-xml/&gt;, &lt;unsupported-encoding/&gt;, and &lt;xml-not-well-formed/&gt;, 
        /// although the more specific errors are preferred.
        /// </summary>
        [Name("bad-format")]
        BadFormat,

        /// <summary>
        /// the entity has sent a namespace prefix that is unsupported, or has sent no namespace prefix on an
        /// element that requires such a prefix (see XML Namespace Names and Prefixes (XML Namespace Names and Prefixes)).
        /// </summary>
        [Name("bad-namespace-prefix")]
        BadNamespacePrefix,

        /// <summary>
        /// the server is closing the active stream for this entity because a new stream has been initiated 
        /// that conflicts with the existing stream.
        /// </summary>
        [Name("conflict")]
        Conflict,

        /// <summary>
        /// the entity has not generated any traffic over the stream for some period of time 
        /// (configurable according to a local service policy).
        /// </summary>
        [Name("connection-timeout")]
        ConnectionTimeout,

        /// <summary>
        /// the value of the 'to' attribute provided by the initiating entity in the stream header corresponds to a hostname that is no longer hosted by the server.
        /// </summary>
        [Name("host-gone")]
        HostGone,

        /// <summary>
        /// the value of the 'to' attribute provided by the initiating entity in the stream header does not
        /// correspond to a hostname that is hosted by the server.
        /// </summary>
        [Name("host-unknown")]
        HostUnknown,

        /// <summary>
        /// a stanza sent between two servers lacks a 'to' or 'from' attribute (or the attribute has no value).
        /// </summary>
        [Name("improper-addressing")]
        ImproperAddressing,

        /// <summary>
        /// the server has experienced a misconfiguration or an otherwise-undefined internal error 
        /// that prevents it from servicing the stream.
        /// </summary>
        [Name("internal-server-error")]
        InternalServerError,

        /// <summary>
        /// the JID or hostname provided in a 'from' address does not match an authorized JID or 
        /// validated domain negotiated between servers via SASL or dialback, or between a client 
        /// and a server via authentication and resource binding.
        /// </summary>
        [Name("invalid-from")]
        InvalidFrom,

        /// <summary>
        /// the stream ID or dialback ID is invalid or does not match an ID previously provided.
        /// </summary>
        [Name("invalid-id")]
        InvalidId,

        /// <summary>
        /// the streams namespace name is something other than "http://etherx.jabber.org/streams" or the 
        /// dialback namespace name is something other than "jabber:server:dialback" 
        /// (see XML Namespace Names and Prefixes (XML Namespace Names and Prefixes)).
        /// </summary>
        [Name("invalid-namespace")]
        InvalidNamespace,

        /// <summary>
        /// the entity has sent invalid XML over the stream to a server that performs validation.
        /// </summary>
        [Name("invalid-xml")]
        InvalidXml,

        /// <summary>
        /// the entity has attempted to send data before the stream has been authenticated, or otherwise is not
        /// authorized to perform an action related to stream negotiation; the receiving entity MUST NOT process 
        /// the offending stanza before sending the stream error.
        /// </summary>
        [Name("not-authorized")]
        NotAuthorized,

        /// <summary>
        /// the entity has violated some local service policy; the server MAY choose to specify the policy in the &lt;text/&gt; element or an application-specific condition element.
        /// </summary>
        [Name("policy-violation")]
        PolicyViolation,

        /// <summary>
        /// the server is unable to properly connect to a remote entity that is required for authentication or authorization.
        /// </summary>
        [Name("remote-connection-failed")]
        RemoteConnectionFailed,

        /// <summary>
        /// the server lacks the system resources necessary to service the stream.
        /// </summary>
        [Name("resource-constraint")]
        ResourceConstraint,

        /// <summary>
        /// the entity has attempted to send restricted XML features such as a comment, processing instruction, DTD, 
        /// entity reference, or unescaped character (see Restrictions (Restrictions)).
        /// </summary>
        [Name("restricted-xml")]
        RestrictedXml,

        /// <summary>
        /// the server will not provide service to the initiating entity but is redirecting traffic to another host; 
        /// the server SHOULD specify the alternate hostname or IP address (which MUST be a valid domain identifier)
        /// as the XML character data of the &lt;see-other-host/&gt; element.
        /// </summary>
        [Name("see-other-host")]
        SeeOtherHost,

        /// <summary>
        /// the server is being shut down and all active streams are being closed.
        /// </summary>
        [Name("system-shutdown")]
        SystemShutdown,

        /// <summary>
        /// the error condition is not one of those defined by the other conditions in this list; this error condition 
        /// SHOULD be used only in conjunction with an application-specific condition.
        /// </summary>
        [Name("undefined-condition")]
        UndefinedCondition,

        /// <summary>
        /// the initiating entity has encoded the stream in an encoding that is not supported by the server.
        /// </summary>
        [Name("unsupported-encoding")]
        UnsupportedEncoding,

        /// <summary>
        /// the initiating entity has sent a first-level child of the stream that is not supported by the server.
        /// </summary>
        [Name("unsupported-stanza-type")]
        UnsupportedStanzaType,

        /// <summary>
        /// the value of the 'version' attribute provided by the initiating entity in the stream header specifies a 
        /// version of XMPP that is not supported by the server; the server MAY specify the version(s) it supports in the 
        /// &lt;text/&gt; element.
        /// </summary>
        [Name("unsupported-version")]
        UnsupportedVersion,

        /// <summary>
        /// the initiating entity has sent XML that is not well-formed as defined by the XML specs.
        /// </summary>
        [Name("xml-not-well-formed")]
        XmlNotWellFormed
    }
}
