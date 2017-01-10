
namespace Matrix.Xmpp.Jingle
{
    public enum CandidateType
    {
        #region Schema
        /*
         * <xs:attribute name='type' use='required'>
          <xs:simpleType>
            <xs:restriction base='xs:NCName'>
              <xs:enumeration value='host'/>
              <xs:enumeration value='prflx'/>
              <xs:enumeration value='relay'/>
              <xs:enumeration value='srflx'/>
            </xs:restriction>
          </xs:simpleType>
         */
        #endregion

        Host,
        Prflx,
        Relay,
        Srflx
    }
}