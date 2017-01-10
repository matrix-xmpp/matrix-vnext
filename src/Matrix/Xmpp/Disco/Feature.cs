using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Disco 
{
    [XmppTag(Name = "feature", Namespace = Namespaces.DiscoInfo)]
    public class Feature : XmppXElement
    {
        #region Xml sample
        /*
		<iq type='result'
			from='plays.shakespeare.lit'
			to='romeo@montague.net/orchard'
			id='info1'>
		<query xmlns='http://jabber.org/protocol/disco#info'>
			<identity
				category='conference'
				type='text'
				name='Play-Specific Chatrooms'/>
			<identity
				category='directory'
				type='chatroom'
				name='Play-Specific Chatrooms'/>
			<feature var='http://jabber.org/protocol/disco#info'/>
			<feature var='http://jabber.org/protocol/disco#items'/>
			<feature var='http://jabber.org/protocol/muc'/>
			<feature var='jabber:iq:register'/>
			<feature var='jabber:iq:search'/>
			<feature var='jabber:iq:time'/>
			<feature var='jabber:iq:version'/>
		</query>
		</iq>
		*/
        #endregion

        #region << Constructors >>
        public Feature()
            : base(Namespaces.DiscoInfo, "feature")
        {
        }

        public Feature(string var) : this()
        {
            Var = var;
        }
        #endregion

        /// <summary>
        /// protocol namespace or other feature offered by the entity
        /// </summary>
        public string Var
        {
            get { return GetAttribute("var"); }
            set { SetAttribute("var", value); }
        }
    }
}