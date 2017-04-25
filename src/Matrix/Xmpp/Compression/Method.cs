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
    [XmppTag(Name = "method", Namespace = Namespaces.Compress)]
    public class Method : XmppXElement
    {
        #region << Constructors >>
        internal Method(string ns) : base(ns, "method")
        {
        }

        public Method() : this(Namespaces.Compress)
        {            
        }

        public Method(Methods method) : this()
        {
            Value = method.GetName();
        }
        #endregion

        public Methods CompressionMethod
        {
            get { return Enum.ParseUsingNameAttrib<Methods>(Value); }
            set { Value = value.GetName(); }
        }
    }
}
