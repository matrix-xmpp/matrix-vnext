namespace Matrix.Xmpp.Base
{
    using Matrix.Xml;
    using System;

    public abstract class XmppXElementWithBased64Value : XmppXElement
    {
        protected XmppXElementWithBased64Value(string ns, string tag) : base(ns, tag)
        {
        }
        
        /// <summary>
        /// sets the value of a SASL step element (challenge, response, auth).
        /// The value gets is converted to Base64.
        /// </summary>
        public byte[] Bytes
        {
            get
            {
                if (Value == "")
                    return null;
                if (Value == "=")
                    return new byte[0];
                return Convert.FromBase64String(Value);
            }
            set
            {
                if (value == null)
                    if (Value.Length > 0)
                        Value = "";
                    else
                        return;
                else if (value.Length == 0)
                    Value = "=";
                else
                    Value = Convert.ToBase64String(value);
            }
        }        
    }
}
