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
using Matrix.Xml;

namespace Matrix.Xmpp.Compression
{
    public abstract class Compression : XmppXElement
    {
        internal Compression(string ns)
            : base(ns, "compression")
        {
        }

        //protected Compression() : this(Namespaces.FeatureCompress)
        //{            
        //}

        /// <summary>
        /// Add a compression method/algorithm
        /// </summary>
        /// <param name="method"></param>
        public void AddMethod(Methods method)
        {
            if (!Supports(method))
            {
                if (GetType() == typeof(Stream.Features.Compression))
                    Add(new Features.Method(method));
                else
                    Add(new Method(method));
            }
        }

        /// <summary>
        /// Is the given compression method/algrithm supported?
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public bool Supports(Methods method)
        {
            return GetMethods().Any(m => m.CompressionMethod == method);
        }

        internal IEnumerable<Method> GetMethods()
        {
            return Elements<Method>();
        }
    }
}
