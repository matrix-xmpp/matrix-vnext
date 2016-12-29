namespace Matrix.Xmpp.MessageArchiving
{
    /// <summary>
    /// Base class with "Start", "With" and "Version" attributes
    /// </summary>
    public abstract class ArchiveEvent : Link
    {
        #region
        /*
          <xs:element name='changed'>
            <xs:complexType>
              <xs:simpleContent>
                <xs:extension base='empty'>
                  <xs:attribute name='start' type='xs:dateTime' use='required'/>
                  <xs:attribute name='with' type='xs:string' use='required'/>
                  <xs:attribute name='version' type='xs:nonNegativeInteger' use='required'/>
                </xs:extension>
              </xs:simpleContent>
            </xs:complexType>
          </xs:element>
         */
        #endregion
        protected ArchiveEvent(string tagName)
            : base(tagName)
        {
        }

        public int Version
        {
            get { return GetAttributeInt("version"); }
            set { SetAttribute("version", value); }
        }
    }
}