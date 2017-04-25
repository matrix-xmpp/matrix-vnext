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

using System.Collections.Generic;
using System.Linq;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Sasl
{
    /// <summary>
    /// SASL Mechanisms
    /// </summary>
    [XmppTag(Name = "mechanisms", Namespace = Namespaces.Sasl)]
    public class Mechanisms : XmppXElement
    {
        public Mechanisms()
            : base(Namespaces.Sasl, "mechanisms")
        {            
        }

        public IEnumerable<Mechanism> GetMechanisms()
        {
            return Elements<Mechanism>();
        }

        public bool SupportsMechanism(SaslMechanism mechanism)
        {
            return GetMechanisms().Any(mech => mech.SaslMechanism == mechanism);
        }

        public Mechanism GetMechanism(SaslMechanism mechanism)
        {
            return GetMechanisms().FirstOrDefault(mech => mech.SaslMechanism == mechanism);
        }

        public void AddMechanism(SaslMechanism mechanism)
        {
            Add(new Mechanism(mechanism));
        }

        /// <summary>
        /// XEP-0233 Principal Hostname for Sasl authentication.
        /// </summary>
        public string PrincipalHostname
        {
            get
            {
                var host = Element<Hostname>();
                return host != null ? host.Value : null;
            }
        }
    }
}
