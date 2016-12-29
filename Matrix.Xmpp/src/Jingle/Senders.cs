namespace Matrix.Xmpp.Jingle
{
    #region Schema
    /*
        <xs:simpleType>
            <xs:restriction base='xs:NCName'>
                <xs:enumeration value='both'/>
                <xs:enumeration value='initiator'/>
                <xs:enumeration value='none'/>
                <xs:enumeration value='responder'/>
            </xs:restriction>
        </xs:simpleType>
     */
    #endregion

    public enum Senders
    {
        Both,
        Initiator,
        None,
        Responder
    }
}