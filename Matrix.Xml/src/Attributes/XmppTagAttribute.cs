using System;

namespace Matrix.Xml.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class XmppTagAttribute : Attribute
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
    }
}