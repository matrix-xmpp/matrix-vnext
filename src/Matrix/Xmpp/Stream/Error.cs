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
using Matrix.Xml;

namespace Matrix.Xmpp.Stream
{
    /// <summary>
	/// Stream Errors &lt;stream:error&gt;
	/// </summary>
    [XmppTag(Name = Tag.Error, Namespace = Namespaces.Stream)]
	public class Error : XmppXElement
	{
        #region Xml sample
        /*
            <stream:error>
              <defined-condition xmlns='urn:ietf:params:xml:ns:xmpp-streams'/>
              <text xmlns='urn:ietf:params:xml:ns:xmpp-streams'
                    xml:lang='langcode'>
                OPTIONAL descriptive text
              </text>
              [OPTIONAL application-specific condition element]
            </stream:error>
     
            <stream:error>Invalid handshake</stream:error>
            <stream:error>Socket override by another connection.</stream:error>
        */
        #endregion
        public Error()
            : base(Namespaces.Stream, "stream", Tag.Error)
		{		
		}

        public Error(ErrorCondition condition) : this()
        {
            Condition = condition;
        }        
        
        public ErrorCondition Condition
        {
            get
            {
                foreach (var errorCondition in Enum.GetValues<ErrorCondition>().ToEnum<ErrorCondition>())
                {
                    if (HasTag(Namespaces.Streams, errorCondition.GetName()))
                        return errorCondition;
                }
                return ErrorCondition.UndefinedCondition;
            }
            set
            {
                if (value != ErrorCondition.UndefinedCondition)
                    SetTag(Namespaces.Streams, value.GetName(), null);
            }
        }

        /// <summary>
        /// <para>
        /// The &lt;text/&gt; element is OPTIONAL. If included, it SHOULD be used only to provide descriptive or diagnostic information
        /// that supplements the meaning of a defined condition or application-specific condition. 
        /// </para>
        /// <para>
        /// It SHOULD NOT be interpreted programmatically by an application.
        /// It SHOULD NOT be used as the error message presented to a user, but MAY be shown in addition to the error message 
        /// associated with the included condition element (or elements).
        /// </para>
        /// </summary>
        public new string Text
        {
            get { return GetTag(Namespaces.Streams, "text"); }
            set
            {
                SetTag(Namespaces.Streams, "text", value);
            }
        }    
    }
}
