namespace Matrix.Xmpp.MessageArchiving
{
    #region
    /*     
      <xs:attribute name='save' use='optional'>
        <xs:simpleType>
            <xs:restriction base='xs:NCName'>
            <xs:enumeration value='body'/>
            <xs:enumeration value='false'/>
            <xs:enumeration value='message'/>
            <xs:enumeration value='stream'/>
            </xs:restriction>
        </xs:simpleType>
      </xs:attribute>      
     */
    #endregion

    public enum SaveType
    {
        /// <summary>
        /// the saving entity SHOULD save only <body/> elements. *
        /// </summary>
        Body,

        /// <summary>
        /// the saving entity MUST save nothing.
        /// </summary>
        False,

        /// <summary>
        /// the saving entity SHOULD save the full XML content of each <message/> element.
        /// </summary>
        Message,

        /// <summary>
        ///  the saving entity SHOULD save every byte that passes over the stream in either direction.
        /// </summary>
        Stream
    }
}