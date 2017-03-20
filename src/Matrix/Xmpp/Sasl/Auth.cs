/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
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
    /// <summary>
    /// Summary description for Auth.
    /// </summary>
    [XmppTag(Name = "auth", Namespace = Namespaces.Sasl)]
    public class Auth : Base.Sasl
    {
        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Auth"/> class.
        /// </summary>
        public Auth() : base("auth")
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Auth"/> class.
        /// </summary>
        /// <param name="mechanism">The mechanism.</param>
        public Auth(SaslMechanism mechanism)
            : this()
        {
            SaslMechanism = mechanism;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Auth"/> class.
        /// </summary>
        /// <param name="mechanism">The sasl mechanism name as string.</param>
        public Auth(string mechanism)
            : this()
        {
            SetAttribute("mechanism", mechanism); 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Auth"/> class.
        /// </summary>
        /// <param name="mechanism">The mechanism.</param>
        /// <param name="text">The value of the auth tag.</param>
        public Auth(string mechanism, string text)
            : this(mechanism)
        {
            Value = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Auth"/> class.
        /// </summary>
        /// <param name="mechanism">The mechanism.</param>
        /// <param name="text">The value of the auth tag.</param>
        public Auth(SaslMechanism mechanism, string text)
            : this(mechanism)
        {
            Value = text;
        }
        #endregion

        /// <summary>
        /// Gets or sets the sasl mechanism.
        /// </summary>
        /// <value>
        /// The sasl mechanism.
        /// </value>
        public SaslMechanism SaslMechanism
        {
            get { return Mechanism.GetSaslMechanism(GetAttribute("mechanism")); }
            set { SetAttribute("mechanism", Mechanism.GetSaslMechanismName(value)); }
        }
    }
}
