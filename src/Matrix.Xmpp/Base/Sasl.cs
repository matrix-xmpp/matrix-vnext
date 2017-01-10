using System;
using Matrix.Xml;

namespace Matrix.Xmpp.Base
{
    public abstract class Sasl : XmppXElement
    {
        #region << Constructor >>
        protected Sasl(string tag) : base(Namespaces.Sasl, tag)
        {
        }
        #endregion

        #region << Properties >>
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
        #endregion
    }
}