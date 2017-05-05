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

namespace Matrix.Xmpp.Base
{
    /// <summary>
    /// stanza error condition as defined in RFC 3920 9.3
    /// </summary>
    public enum ErrorCondition
    {
        /// <summary>
        /// The sender has sent a stanza containing XML that does not conform to the appropriate schema or that 
        /// cannot be processed (e.g., an IQ stanza that includes an unrecognized value of the 'type' attribute);
        /// the associated error type SHOULD be "modify".
        /// </summary>
        [Name("bad-request")]
        BadRequest,
        
        /// <summary>
        /// Access cannot be granted because an existing resource exists with the same name or address; 
        /// the associated error type SHOULD be "cancel". 
        /// </summary>
        [Name("conflict")]
        Conflict,
        
        /// <summary>
        /// The feature represented in the XML stanza is not implemented by the intended recipient or 
        /// an intermediate server and therefore the stanza cannot be processed; the associated error type SHOULD 
        /// be "cancel" or "modify".
        /// </summary>
        [Name("feature-not-implemented")]
        FeatureNotImplemented,
        
        /// <summary>
        /// The requesting entity does not possess the required permissions to perform the action; 
        /// the associated error type SHOULD be "auth".
        /// </summary>
        [Name("forbidden")]
        Forbidden,
        
        /// <summary>
        /// The recipient or server can no longer be contacted at this address 
        /// (the error stanza MAY contain a new address in the XML character data of the &lt;gone/&gt; element); 
        /// the associated error type SHOULD be "cancel" or "modify".
        /// </summary>
        [Name("gone")]
        Gone,
        
        /// <summary>
        /// The server could not process the stanza because of a misconfiguration or an otherwise-undefined 
        /// internal server error; the associated error type SHOULD be "wait" or "cancel".
        /// </summary>
        [Name("internal-server-error")]
        InternalServerError,
        
        /// <summary>
        /// The addressed JID or item requested cannot be found; the associated error type SHOULD be "cancel" or "modify".
        /// </summary>
        /// <remarks>        
        /// An application MUST NOT return this error if doing so would provide information about the intended 
        /// recipient's network availability to an entity that is not authorized to know such information; 
        /// instead it SHOULD return a &lt;service-unavailable/&gt; error.
        /// </remarks> 
        [Name("item-not-found")]       
        ItemNotFound,
        
        /// <summary>
        /// The sending entity has provided or communicated an XMPP address 
        /// (e.g., a value of the 'to' attribute) or aspect thereof (e.g., an XMPP resource identifier) 
        /// that does not adhere to the syntax defined under RFC3920 Section 3 (Addresses); 
        /// the associated error type SHOULD be "modify".
        /// </summary>
        [Name("jid-malformed")]
        JidMalformed,
        
        /// <summary>
        /// The recipient or server understands the request but is refusing to process it because it does not
        /// meet criteria defined by the recipient or server (e.g., a local policy regarding stanza size 
        /// limits or acceptable words in messages); the associated error type SHOULD be "modify".
        /// </summary>
        [Name("not-acceptable")]
        NotAcceptable,
        
        /// <summary>
        /// The recipient or server does not allow any entity to perform the action (e.g., sending to entities at 
        /// a blacklisted domain); the associated error type SHOULD be "cancel".
        /// </summary>
        [Name("not-allowed")]
        NotAllowed,
        
        /// <summary>
        /// The sender must provide proper credentials before being allowed to perform the action, or has provided 
        /// improper credentials; the associated error type SHOULD be "auth".
        /// </summary>
        [Name("not-authorized")]
        NotAuthorized,
        
        /// <summary>
        /// The item requested has not changed since it was last requested; the associated error type SHOULD be "continue".
        /// </summary>
        [Name("not-modified")]
        NotModified,

        /// <summary>
        /// The requesting entity is not authorized to access the requested service because payment is required; 
        /// the associated error type SHOULD be "auth".
        /// </summary>   
        [Name("payment-required")]     
        PaymentRequired,
        
        /// <summary>
        /// The intended recipient is temporarily unavailable; the associated error type SHOULD be "wait".
        /// </summary>
        /// <remarks>
        /// An application MUST NOT return this error if doing so would provide information about the 
        /// intended recipient's network availability to an entity that is not authorized to know such 
        /// information; instead it SHOULD return a &lt;service-unavailable/&gt; error.
        /// </remarks>
        [Name("recipient-unavailable")]
        RecipientUnavailable,
        
        /// <summary>
        /// The recipient or server is redirecting requests for this information to another entity, 
        /// typically in a temporary fashion; the associated error type SHOULD be "modify" and the error stanza
        /// SHOULD contain the alternate address (which SHOULD be a valid JID) in the XML character data 
        /// of the &lt;redirect/&gt; element.
        /// </summary>
        [Name("redirect")]
        Redirect,
        
        /// <summary>
        /// The requesting entity is not authorized to access the requested service because prior 
        /// registration is required; the associated error type SHOULD be &quot;auth&quot;.
        /// </summary>
        [Name("registration-required")]
        RegistrationRequired,
        
        /// <summary>
        /// A remote server or service specified as part or all of the JID of the intended recipient 
        /// does not exist; the associated error type SHOULD be &quot;cancel&quot;.
        /// </summary>
        [Name("remote-server-not-found")]
        RemoteServerNotFound,
        
        /// <summary>
        /// A remote server or service specified as part or all of the JID of the intended recipient 
        /// (or required to fulfill a request) could not be contacted within a reasonable amount 
        /// of time; the associated error type SHOULD be &quot;wait&quot;.
        /// </summary>
        [Name("remote-server-timeout")]
        RemoteServerTimeout,
        
        /// <summary>
        /// The server or recipient lacks the system resources necessary to service the request; 
        /// the associated error type SHOULD be "wait".
        /// </summary>
        [Name("resource-constraint")]
        ResourceConstraint,
        
        /// <summary>
        /// The server or recipient does not currently provide the requested service; 
        /// the associated error type SHOULD be &quot;cancel&quot;.
        /// </summary>
        /// <remarks>
        /// An application SHOULD return a &lt;service-unavailable/&gt; error instead of 
        /// &lt;item-not-found/&gt; or &lt;recipient-unavailable/&gt; if sending one of the latter 
        /// errors would provide information about the intended recipient&#39;s network 
        /// availability to an entity that is not authorized to know such information.
        /// </remarks>
        [Name("service-unavailable")]
        ServiceUnavailable,
        
        /// <summary>
        /// The requesting entity is not authorized to access the requested service 
        /// because a prior subscription is required; the associated error type SHOULD be &quot;auth&quot;.
        /// </summary>
        [Name("subscription-required")]
        SubscriptionRequired,
        
        /// <summary>
        /// The error condition is not one of those defined by the other conditions in this list; 
        /// any error type may be associated with this condition, and it SHOULD be used only in conjunction 
        /// with an application-specific condition.
        /// </summary>
        [Name("undefined-condition")]
        UndefinedCondition,
        
        /// <summary>
        /// The recipient or server understood the request but was not expecting it at this time 
        /// (e.g., the request was out of order); the associated error type SHOULD be "wait" or "modify".
        /// </summary>
        [Name("unexpected-request")]
        UnexpectedRequest,


        /// <summary>
        /// the entity has violated some local service policy; the server MAY choose to specify the policy in 
        /// the &lt;text/&gt; element or an application-specific condition element.
        /// </summary>
        [Name("policy-violation")]
        PolicyViolation,
    }
}
