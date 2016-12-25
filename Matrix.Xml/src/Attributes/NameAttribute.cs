using System;

namespace Matrix.Xml.Attributes
{
    /// <summary>
    /// Name attribute which is used to represent the string name of Enumerations in Xml
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class NameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NameAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
    }
}