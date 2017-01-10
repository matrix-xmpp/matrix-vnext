using Matrix.Core.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.XData
{
    [XmppTag(Name = "option", Namespace = Namespaces.XData)]
    public class Option : XmppXElement
    {
        public Option() : base(Namespaces.XData, "option")
        {
        }

        public Option(string val) : this()
        {
            Value = val;
        }

        public Option(string label, string val) : this(val)
        {
            Label = label;
        }

        #region << Properties >>
        /// <summary>
        /// Label of the option
        /// </summary>
        public string Label
        {
            get { return GetAttribute("label"); }
            set { SetAttribute("label", value); }
        }
        #endregion
        
        public new string Value
        {
            /*
            get { return GetTag<XData.Value>(); }
            set 
            {
                XData.Value val = Element<XData.Value>();
                if (val == null)
                    val = new Value();
                
                val.Value = value;                 
            }
             */

            // much easier that way
            get { return GetTag("value"); }
            set { SetTag("value", value); }
        }
    }
}