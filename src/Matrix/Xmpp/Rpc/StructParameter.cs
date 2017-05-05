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

namespace Matrix.Xmpp.Rpc
{
    public class StructParameter : Dictionary<string, object>
    {
        public StructParameter() {}
        public StructParameter(int capacity) : base(capacity) { }
        public StructParameter(IEqualityComparer<string> comparer) : base(comparer) { }
        public StructParameter(IDictionary<string, object> dictionary) : base(dictionary) { }
        public StructParameter(int capacity, IEqualityComparer<string> comparer) : base(capacity, comparer) { }
        public StructParameter(IDictionary<string, object> dictionary, IEqualityComparer<string> comparer) : base(dictionary, comparer) { }
    }
}
