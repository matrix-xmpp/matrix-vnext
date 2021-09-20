using Matrix.Xml;
using Matrix.Xmpp.PubSub;

namespace Matrix.Xmpp.Base
{
    public abstract class Affiliation : XmppXElement
    {
        #region << Constructors >>
        protected Affiliation(string ns) : base(ns, "affiliation")
        {
        }
        #endregion

        #region << Properties >>
        
        /// <summary>
        /// the message type (chat, groupchat, normal, headline or error).
        /// </summary>
        public AffiliationType AffiliationType
        {
            /*
                <xs:restriction base='xs:NCName'>
                    <xs:enumeration value='member'/>
                    <xs:enumeration value='none'/>
                    <xs:enumeration value='outcast'/>
                    <xs:enumeration value='owner'/>
                    <xs:enumeration value='publisher'/>
                    <xs:enumeration value='publish-only'/>
                </xs:restriction>
             
                we cannot use the Enum functions here because of the
                dash in 'publish-only which is not a valid enum
            */
            get { return GetAttributeEnumUsingNameAttrib<AffiliationType>("affiliation"); }
            set { SetAttribute("affiliation", value.GetName());
            }
        }
        #endregion
    }
}
