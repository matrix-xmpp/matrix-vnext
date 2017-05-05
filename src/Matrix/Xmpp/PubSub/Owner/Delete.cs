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

namespace Matrix.Xmpp.PubSub.Owner
{
    [XmppTag(Name = "delete", Namespace = Namespaces.PubsubOwner)]
    public class Delete : Xmpp.PubSub.Delete
    {
        #region Schema
        /*
          <xs:element name='pubsub'>
            <xs:complexType>
              <xs:choice>
                <xs:element ref='affiliations'/>
                <xs:element ref='configure'/>
                <xs:element ref='delete'/>
                <xs:element ref='purge'/>
                <xs:element ref='subscriptions'/>
              </xs:choice>
            </xs:complexType>
          </xs:element>
        */ 
        #endregion

        public Delete()
            : base(Namespaces.PubsubOwner)
        {
        }
    }
}
