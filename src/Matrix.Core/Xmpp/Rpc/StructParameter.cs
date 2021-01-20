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
