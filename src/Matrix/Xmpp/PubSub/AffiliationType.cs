using Matrix.Attributes;

namespace Matrix.Xmpp.PubSub
{
    public enum AffiliationType
    {

        #region Schema
        /*
            <xs:restriction base='xs:NCName'>
                <xs:enumeration value='member'/>
                <xs:enumeration value='none'/>
                <xs:enumeration value='outcast'/>
                <xs:enumeration value='owner'/>
                <xs:enumeration value='publisher'/>
                <xs:enumeration value='publish-only'/>
            </xs:restriction>
        */
        #endregion

        [Name("member")]
        Member,

        [Name("none")]
        None,

        [Name("outcast")]
        Outcast,

        [Name("owner")]
        Owner,

        [Name("publisher")]
        Publisher,

        [Name("publish-only")]
        PublishOnly
    }
}