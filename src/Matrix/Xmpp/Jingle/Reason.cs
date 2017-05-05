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
using Matrix.Xml;

namespace Matrix.Xmpp.Jingle
{
    #region << XML schema >>
    /*
       <xs:complexType name='reasonElementType'>
        <xs:sequence>
          <xs:choice>
            <xs:element name='alternative-session' 
                        type='alternativeSessionElementType'/>
            <xs:element name='busy' type='empty'/>
            <xs:element name='cancel' type='empty'/>
            <xs:element name='connectivity-error' type='empty'/>
            <xs:element name='decline' type='empty'/>
            <xs:element name='expired' type='empty'/>
            <xs:element name='failed-application' type='empty'/>
            <xs:element name='failed-transport' type='empty'/>
            <xs:element name='general-error' type='empty'/>
            <xs:element name='gone' type='empty'/>
            <xs:element name='incompatible-parameters' type='empty'/>
            <xs:element name='media-error' type='empty'/>
            <xs:element name='security-error' type='empty'/>
            <xs:element name='success' type='empty'/>
            <xs:element name='timeout' type='empty'/>
            <xs:element name='unsupported-applications' type='empty'/>
            <xs:element name='unsupported-transports' type='empty'/>
          </xs:choice>
          <xs:element name='text' type='xs:string' minOccurs='0' maxOccurs='1'/>
          <xs:any namespace='##other' minOccurs='0' maxOccurs='1'/>
        </xs:sequence>
      </xs:complexType>
    */
    #endregion

    [XmppTag(Name = "reason", Namespace = Namespaces.Jingle)]
    public class Reason : XmppXElement
    {
        public Reason() : base(Namespaces.Jingle, "reason")
        {
        }

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>The condition.</value>
        public Condition Condition
        {
            get
            {
                if (Element<AlternativeSession>() != null)
                    return Condition.AlternativeSession;     

                foreach (var cond in Enum.GetValues<Condition>().ToEnum<Condition>())
                {
                    if (HasTag(cond.GetName()))
                        return cond;
                }
                return Condition.UnknownCondition;
            }
            set
            {
                if (value == Condition.UnknownCondition)
                    throw new NotSupportedException();

                if (value == Condition.AlternativeSession)
                    Add(new AlternativeSession());
                else
                {
                    string tag = value.GetName();
                    SetTag(tag);
                }
            }
        }

        /// <summary>
        /// Provides a human-readable information about the reason for this action.
        /// </summary>
        /// <value>The human-readable text.</value>
        public new string Text
        {
            get { return GetTag("text");}
            set { SetTag("text", value);}
        }

        public AlternativeSession AlternativeSession
        {
            get { return Element<AlternativeSession>(); }
            set { Replace(value); }
        }
    }
}
