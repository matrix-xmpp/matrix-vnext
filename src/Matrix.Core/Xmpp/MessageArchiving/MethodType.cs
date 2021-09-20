namespace Matrix.Xmpp.MessageArchiving
{
    #region Schema
    /*
      <xs:element name='method'>
        <xs:complexType>
          <xs:simpleContent>
            <xs:extension base='empty'>
              <xs:attribute name='type' type='xs:string' use='required'/>
              <xs:attribute name='use' use='required'>
                <xs:simpleType>
                  <xs:restriction base='xs:NCName'>
                    <xs:enumeration value='concede'/>
                    <xs:enumeration value='forbid'/>
                    <xs:enumeration value='prefer'/>
                  </xs:restriction>
                </xs:simpleType>
              </xs:attribute>
            </xs:extension>
          </xs:simpleContent>
        </xs:complexType>
      </xs:element>
    */
    #endregion

    public enum MethodType
    {        
        /// <summary>
        ///  preferences for use of automatic archiving on the user's server.
        /// </summary>
        Auto,
              
        /// <summary>
        ///  Preferences for use of local archiving to a file or database on the user's machine or device.
        /// </summary>
        Local,

        /// <summary>
        /// Preferences for use of manual archiving by the user's client to the user's server.
        /// </summary>
        Manual
    }
}
