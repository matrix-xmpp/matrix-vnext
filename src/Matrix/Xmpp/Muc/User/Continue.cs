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

namespace Matrix.Xmpp.Muc.User
{
    /// <summary>
    /// Continue
    /// </summary>
    [XmppTag(Name = "continue", Namespace = Namespaces.MucUser)]
    public class Continue : XmppXElement
    {
        #region Schema
        /*
          <xs:element name='continue'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='thread' type='xs:string' use='optional'/>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
          </xs:element>
         
         <continue thread='e0ffe42b28561960c6b12b944a092794b9683a38'/>
        */
        #endregion

        #region << Constructors >>
        /// <summary>
        /// Initializes a new instance of the <see cref="Continue"/> class.
        /// </summary>
        public Continue()
            : base(Namespaces.MucUser, "continue")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Continue"/> class.
        /// </summary>
        /// <param name="thread">The thread.</param>
        public Continue(string thread) : this()
        {
            Thread = thread;
        }
        #endregion

        /// <summary>
        /// Gets or sets the thread.
        /// </summary>
        /// <value>The thread.</value>
        public string Thread
        {
            get { return GetAttribute("thread"); }
            set { SetAttribute("thread", value); }
        }
    }
}
