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

namespace Matrix.Xmpp.MessageArchiving
{
    /// <summary>
    /// Base class with "Start", "With" and "Version" attributes
    /// </summary>
    public abstract class ArchiveEvent : Link
    {
        #region
        /*
          <xs:element name='changed'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='start' type='xs:dateTime' use='required'/>
                  <xs:attribute name='with' type='xs:string' use='required'/>
                  <xs:attribute name='version' type='xs:nonNegativeInteger' use='required'/>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
          </xs:element>
         */
        #endregion
        protected ArchiveEvent(string tagName)
            : base(tagName)
        {
        }

        public int Version
        {
            get { return GetAttributeInt("version"); }
            set { SetAttribute("version", value); }
        }
    }
}
