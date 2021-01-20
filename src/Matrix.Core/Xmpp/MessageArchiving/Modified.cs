using System;
using System.Collections.Generic;
using Matrix.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.MessageArchiving
{
    [XmppTag(Name = "modified", Namespace = Namespaces.Archiving)]
    public class Modified : XmppXElementWithResultSet
    {
        #region Schema
        /*
          <xs:element name='modified'>
            <xs:complexType>
              <xs:sequence>
                <xs:element ref='changed' minOccurs='0' maxOccurs='unbounded'/>
                <xs:element ref='removed' minOccurs='0' maxOccurs='unbounded'/>
                <xs:any processContents='lax' namespace='##other' minOccurs='0' maxOccurs='unbounded'/>
              </xs:sequence>
              <xs:attribute name='start' type='xs:dateTime' use='required'/>
            </xs:complexType>
          </xs:element>
         */
        #endregion
        public Modified() : base(Namespaces.Archiving, "modified")
        {
        }

        #region removed
        public IEnumerable<Removed> GetRemoved()
        {
            return Elements<Removed>();
        }

        public Removed AddRemoved()
        {
            var removed = new Removed();
            Add(removed);

            return removed;
        }

        public Modified AddRemoved(Removed changed)
        {
            Add(changed);
            return this;
        }
        #endregion

        #region changed
        public IEnumerable<Changed> GetChanged()
        {
            return Elements<Changed>();
        }
        
        public Changed AddChanged()
        {
            var changed = new Changed();
            Add(changed);

            return changed;
        }
        
        public void AddChanged(Changed changed)
        {
            Add(changed);
        }
        #endregion

        public DateTime Start
        {
            get { return GetAttributeIso8601Date("start"); }
            set { SetAttributeIso8601Date("start", value); }
        }
    }
}
