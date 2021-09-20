using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub
{
    [XmppTag(Name = "options", Namespace = Namespaces.Pubsub)]
    public class Options : Base.XmppXElementWithXData
    {
        #region Schema
        /*
            <pubsub xmlns='http://jabber.org/protocol/pubsub'>
                <options node='princely_musings' jid='francisco@denmark.lit'>
                  <x xmlns='jabber:x:data' type='form'>
                    <field var='FORM_TYPE' type='hidden'>
                      <value>http://jabber.org/protocol/pubsub#subscribe_options</value>
                    </field>
                    <field var='pubsub#deliver' type='boolean'
                           label='Enable delivery?'>
                      <value>1</value>
                    </field>
                    <field var='pubsub#digest' type='boolean'
                           label='Receive digest notifications (approx. one per day)?'>
                      <value>0</value>
                    </field>
                    <field var='pubsub#include_body' type='boolean'
                           label='Receive message body in addition to payload?'>
                      <value>false</value>
                    </field>
                    <field
                        var='pubsub#show-values'
                        type='list-multi'
                        label='Select the presence types which are
                               allowed to receive event notifications'>
                      <option label='Want to Chat'><value>chat</value></option>
                      <option label='Available'><value>online</value></option>
                      <option label='Away'><value>away</value></option>
                      <option label='Extended Away'><value>xa</value></option>
                      <option label='Do Not Disturb'><value>dnd</value></option>
                      <value>chat</value>
                      <value>online</value>
                    </field>
                  </x>
                </options>
            </pubsub>
         * 
        */
        #endregion

        #region << Constructors >>
        public Options()
            : base(Namespaces.Pubsub, "options")
        {
        }
        #endregion
    }
}
