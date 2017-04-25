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

namespace Matrix.Xmpp.Sasl
{
    /// <summary>
    /// Sasl failure object.
    /// </summary>
    [XmppTag(Name = "failure", Namespace = Namespaces.Sasl)]
    public class Failure : XmppXElement
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Failure"/> class.
        /// </summary>
        public Failure() : base(Namespaces.Sasl, "failure")
		{			
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Failure"/> class.
        /// </summary>
        /// <param name="condition">The condition.</param>
        public Failure(FailureCondition condition) : this()
        {
            Condition = condition;
        }
        #endregion
        
        /// <summary>
        /// The failure condition
        /// </summary>
        public FailureCondition Condition
        {
            get
            {
                foreach (var failureCondition in Enum.GetValues<FailureCondition>().ToEnum<FailureCondition>())
                {
                     if (HasTag(failureCondition.GetName()))
                        return failureCondition;
                }
                return FailureCondition.UnknownCondition;
            }
            set
            {
                if (value != FailureCondition.UnknownCondition)
                    SetTag(value.GetName());
            }
        }

        /// <summary>
        /// An optional text description for the authentication failure.
        /// </summary>
        public new string Text
        {
            get { return GetTag("text"); }
            set { SetTag("text", value); }
        }
	} 
}
