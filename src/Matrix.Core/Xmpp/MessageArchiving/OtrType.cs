namespace Matrix.Xmpp.MessageArchiving
{
    #region
    /*
        <xs:attribute name='otr' use='optional'>
            <xs:simpleType>
                <xs:restriction base='xs:NCName'>
                <xs:enumeration value='approve'/>
                <xs:enumeration value='concede'/>
                <xs:enumeration value='forbid'/>
                <xs:enumeration value='oppose'/>
                <xs:enumeration value='prefer'/>
                <xs:enumeration value='require'/>
                </xs:restriction>
            </xs:simpleType>
        </xs:attribute>
     */
    #endregion

    public enum OtrType
    {
        /// <summary>
        /// approve -- the user MUST explicitly approve off-the-record communication.
        /// </summary>
        Approve,        
        /// <summary>
        /// concede -- communications MAY be off the record if requested by another user.
        /// </summary>
        Concede,
        
        /// <summary>
        /// communications MUST NOT be off the record.
        /// </summary>
        Forbid,
        
        /// <summary>
        /// communications SHOULD NOT be off the record even if requested.
        /// </summary>
        Oppose,
        
        /// <summary>
        /// communications SHOULD be off the record if possible.
        /// </summary>
        Prefer,
                
        /// <summary>
        ///  communications MUST be off the record.
        /// </summary>
        Require
    }
}
