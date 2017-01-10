using System;
using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.Jingle
{
    #region Xml sample
    /*
        <jingle xmlns='urn:xmpp:jingle:1'>
          action='session-initiate'
          initiator='romeo@montague.lit/orchard'
          sid='a73sjjvkla37jfea'>
     */
    #endregion
    [XmppTag(Name = "jingle", Namespace = Namespaces.Jingle)]
    public class Jingle : XmppXElement
    {
        public Jingle() : base(Namespaces.Jingle, "jingle")
        {
        }

        public Action Action
        {
            get { return GetAttributeEnumUsingNameAttrib<Action>("action");}
            set { SetAttribute("action", value.GetName());}
        }

        public Jid Initiator
        {
            get { return GetAttributeJid("initiator");}
            set { SetAttribute("initiator", value.ToString());}
        }

        public Jid Responder
        {
            get { return GetAttributeJid("responder");}
            set { SetAttribute("responder", value);}
        }
        
        public string Sid
        {
            get { return GetAttribute("sid"); }
            set { SetAttribute("sid", value); }
        }

        /// <summary>
        /// generates a new unique Sid
        /// </summary>
        public void GenerateSid()
        {
            Sid = Guid.NewGuid().ToString();
        }

        public Content Content
        {
            get { return Element<Content>(); }
            set { Replace(value); }
        }
    }
}