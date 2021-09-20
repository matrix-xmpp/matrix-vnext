using System;

namespace Matrix.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class XmppTagAttribute : Attribute
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
    }
}
