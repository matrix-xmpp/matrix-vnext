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

namespace Matrix.Xmpp.Compression
{
    [XmppTag(Name = "failure", Namespace = Namespaces.Compress)]
    public class Failure : XmppXElement
    {
        public Failure()
            : base(Namespaces.Compress, "failure")
        {
        }

        /*
         * xs:element name='setup-failed' type='empty'/>
        <xs:element name='processing-failed' type='empty'/>
        <xs:element name='unsupported-method' type='empty'/>
         */

        public FailureCondition Condition
        {
            get
            {
                var values = Enum.GetValues<FailureCondition>().ToEnum<FailureCondition>();
                foreach (var failureCondition in values)
                {
                     if (HasTag(failureCondition.GetName()))
                        return failureCondition;
                }

                return FailureCondition.UnknownCondition;
            }
            set
            {
                if (value != FailureCondition.UnknownCondition)
                    SetTag(Namespaces.Streams, value.GetName(), null);
            }
        }
    }
}
