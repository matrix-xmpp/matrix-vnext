using Matrix.Attributes;
using Matrix.Xml;

namespace Matrix.Xmpp.PubSub.Event
{
    [XmppTag(Name = "event", Namespace = Namespaces.PubsubEvent)]
    public class Event : XmppXElement
    {
        #region Schema
        /*
           <xs:element name='event'>
            <xs:complexType>
              <xs:choice minOccurs='0'>
                <xs:element ref='collection'/>
                <xs:element ref='configuration'/>
                <xs:element ref='delete'/>
                <xs:element ref='items'/>
                <xs:element ref='purge'/>
                <xs:element ref='subscription'/>
              </xs:choice>
            </xs:complexType>
          </xs:element>
        */
        #endregion

        #region << Constructors >>
        public Event() : base(Namespaces.PubsubEvent, "event")
        {
        }
        #endregion

        #region << Properties >>
        public Collection Collection
        {
            get { return Element<Collection>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }

        public Configuration Configuration
        {
            get { return Element<Configuration>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }

        public Delete Delete
        {
            get { return Element<Delete>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }

        public Items Items
        {
            get { return Element<Items>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }

        public Purge Purge
        {
            get { return Element<Purge>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }

        public Subscription Subscription
        {
            get { return Element<Subscription>(); }
            set
            {
                RemoveNodes();
                Add(value);
            }
        }
        #endregion
       
        public PubSubEventType Type
        {
            get
            {
                var firstEl = FirstElement;
                
                if (firstEl is Collection)
                    return PubSubEventType.Collection;

                if (firstEl is Configuration)
                    return PubSubEventType.Configuration;

                if (firstEl is Delete)
                    return PubSubEventType.Delete;

                if (firstEl is Items)
                    return PubSubEventType.Items;

                if (firstEl is Purge)
                    return PubSubEventType.Purge;

                if (firstEl is Subscription)
                    return PubSubEventType.Subscription;

                return PubSubEventType.None;
            }
            set
            {
                RemoveNodes();
                
                switch (value)
                {
                    case PubSubEventType.Collection:
                        Add(new Collection());
                        break;
                    case PubSubEventType.Configuration:
                        Add(new Configuration());
                        break;
                    case PubSubEventType.Delete:
                        Add(new Delete());
                        break;
                    case PubSubEventType.Items:
                        Add(new Items());
                        break;
                    case PubSubEventType.Purge:
                        Add(new Purge());
                        break;
                    case PubSubEventType.Subscription:
                        Add(new Subscription());
                        break;
                }
            }
        }
    }
}