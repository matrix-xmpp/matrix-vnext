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

using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    public abstract class Error : XmppXElement
    {
        #region << Constructor >>
        protected Error(string ns) : base(ns, "error")
        {
        }
        internal Error(string ns, string tagname) : base(ns, tagname)
        {
        }
        #endregion
                       
        public ErrorType Type
        {
            get { return GetAttributeEnum<ErrorType>("type"); }
            set { SetAttribute("type", value.ToString().ToLower()); }
        }

        /// <summary>
        /// the error condition
        /// </summary>
        public ErrorCondition Condition
		{
			get
			{
                var values = Enum.GetValues<ErrorCondition>().ToEnum<ErrorCondition>();
                foreach (var errorCondition in values)
                {
                    if (HasTag(Namespaces.Stanzas, errorCondition.GetName()))
                        return errorCondition;
                }
                return ErrorCondition.UndefinedCondition;
			}
            set
			{
				switch (value)
				{
					case ErrorCondition.BadRequest:
						SetTag(Namespaces.Stanzas, "bad-request", null);
						Type = ErrorType.Modify;
						break;
					case ErrorCondition.Conflict:
						SetTag(Namespaces.Stanzas, "conflict", null);
						Type = ErrorType.Cancel;
						break;
					case ErrorCondition.FeatureNotImplemented:
						SetTag(Namespaces.Stanzas, "feature-not-implemented", null);
						Type = ErrorType.Cancel;
						break;
					case ErrorCondition.Forbidden:
						SetTag(Namespaces.Stanzas, "forbidden", null);
						Type = ErrorType.Auth;
						break;
					case ErrorCondition.Gone:
						SetTag(Namespaces.Stanzas, "gone", null);
						Type = ErrorType.Modify;
						break;
					case ErrorCondition.InternalServerError:
						SetTag(Namespaces.Stanzas, "internal-server-error", null);
						Type = ErrorType.Wait;
						break;
					case ErrorCondition.ItemNotFound:
						SetTag(Namespaces.Stanzas, "item-not-found", null);
						Type = ErrorType.Cancel;
						break;
					case ErrorCondition.JidMalformed:
						SetTag(Namespaces.Stanzas, "jid-malformed", null);
						Type = ErrorType.Modify;
						break;
					case ErrorCondition.NotAcceptable:
						SetTag(Namespaces.Stanzas, "not-acceptable", null);
						Type = ErrorType.Modify;
						break;
					case ErrorCondition.NotAllowed:
						SetTag(Namespaces.Stanzas, "not-allowed", null);
						Type = ErrorType.Cancel;
						break;
					case ErrorCondition.NotAuthorized:
						SetTag(Namespaces.Stanzas, "not-authorized", null);
						Type = ErrorType.Auth;
						break;
                    case ErrorCondition.NotModified:
						SetTag(Namespaces.Stanzas, "not-modified", null);
						Type = ErrorType.Continue;
						break;                        
					case ErrorCondition.PaymentRequired:
						SetTag(Namespaces.Stanzas, "payment-required", null);
						Type = ErrorType.Auth;
						break;
					case ErrorCondition.RecipientUnavailable:
						SetTag(Namespaces.Stanzas, "recipient-unavailable", null);
						Type = ErrorType.Wait;
						break;
					case ErrorCondition.Redirect:
						SetTag(Namespaces.Stanzas, "redirect", null);
						Type = ErrorType.Modify;
						break;
					case ErrorCondition.RegistrationRequired:
						SetTag(Namespaces.Stanzas, "registration-required", null);
						Type = ErrorType.Auth;
						break;
					case ErrorCondition.RemoteServerNotFound:
						SetTag(Namespaces.Stanzas, "remote-server-not-found", null);
						Type = ErrorType.Cancel;
						break;
					case ErrorCondition.RemoteServerTimeout:
						SetTag(Namespaces.Stanzas, "remote-server-timeout", null);
						Type = ErrorType.Wait;
						break;
					case ErrorCondition.ResourceConstraint:	
						SetTag(Namespaces.Stanzas, "resource-constraint", null);
						Type = ErrorType.Wait;
						break;
					case ErrorCondition.ServiceUnavailable:
						SetTag(Namespaces.Stanzas, "service-unavailable", null);
						Type = ErrorType.Cancel;
						break;
					case ErrorCondition.SubscriptionRequired:
						SetTag(Namespaces.Stanzas, "subscription-required", null);
						Type = ErrorType.Auth;
						break;
					case ErrorCondition.UndefinedCondition:
						SetTag(Namespaces.Stanzas, "undefined-condition", null);
						// could be any
						break;
					case ErrorCondition.UnexpectedRequest:
						SetTag(Namespaces.Stanzas, "unexpected-request", null);
						Type = ErrorType.Wait;
						break;

				}
			}
		}

        /// <summary>
        /// The optional error text
        /// </summary>
        public new string Text
        {
            get
            {
                return GetTag(Namespaces.Stanzas, "text");
            }
            set
            {
                if (value == null)
                    RemoveTag(Namespaces.Stanzas, "text");
                else
                    SetTag(Namespaces.Stanzas, "text", value);
            }
        }    
    }           
}
