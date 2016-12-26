using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Matrix.Core.Attributes;

//using Matrix.Xmpp.Jingle.Apps.Rtp;
//using Matrix.Xmpp.Jingle.Candidates;
//using Matrix.Xmpp.Jingle.Transports;

namespace Matrix.Xml
{
    /// <summary>
    /// Factory for registering XmppXElement types
    /// </summary>
    public class Factory
    {
#if TEST
        public
#else
        private
#endif
        static readonly Dictionary<string, Type> FactoryTable = new Dictionary<string, Type>();
        
        static Factory()
        {
            InitFactory();
        }        

        public static void RegisterElement<T>(string localName) where T : XmppXElement
        {
            RegisterElement<T>("", localName);
        }

        /// <summary>
        /// Adds new Element Types to the Hashtable
        /// Use this function also to register your own created Elements.
        /// If a element is already registered it gets overwritten. This behaviour is also useful if you you want to overwrite
        /// classes and add your own derived classes to the factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ns"></param>
        /// <param name="localName"></param>
        public static void RegisterElement<T>(string ns, string localName) where T : XmppXElement
        {
            RegisterElement(typeof(T), ns, localName);           
        }

        private static void RegisterElement(Type type, string ns, string localName)
        {
            string key = BuildKey(ns, localName);

            // added thread safety on a user request
            lock (FactoryTable)
            {
                if (FactoryTable.ContainsKey(key))
                    FactoryTable[key] = type;
                else
                    FactoryTable.Add(key, type);
            }
        }

        /// <summary>
        /// Builds the key for looking up.
        /// </summary>
        /// <param name="ns">The ns.</param>
        /// <param name="localName">Name of the local.</param>
        /// <returns></returns>
        private static string BuildKey(string ns, string localName)
        {
            return "{" + ns + "}" + localName;
        }

        public static XmppXElement GetElement(string prefix, string localName, string ns)
        {
            Type t = null;
            string key = BuildKey(ns, localName);
            if (FactoryTable.ContainsKey(key))
                t = FactoryTable[key];

            XmppXElement ret;
            if (t != null)
            {
                /*
                 * unity webplayer does not support Activator.CreateInstance,
                 * but can create types with compiled lambdas instead.             
                 */
                ret = (XmppXElement)Activator.CreateInstance(t);
            }
            else
                ret = !string.IsNullOrEmpty(ns) ? new XmppXElement(ns, localName) : new XmppXElement(localName);
                             
            return ret;
        }

        #region << InitFactory >>
        /// <summary>
        /// Inits the factory.
        /// </summary>
        private static void InitFactory()
        {
            //RegisterFromAttributes();
            //return;
            
//            RegisterElement <License.License>                       (Namespaces.AgsLicense, "License");
//            RegisterElement <License.Customer>                      (Namespaces.AgsLicense, "Customer"); 

//            RegisterElement <Xmpp.Base.Stream>                      (Namespaces.Stream, "stream");

//            RegisterElement <Xmpp.Stream.StreamFeatures>            (Namespaces.Stream, "features");

//            // stream features
//            RegisterElement <Xmpp.Stream.Features.Auth>             (Namespaces.FeatureAuth,              "auth");
//            RegisterElement <Xmpp.Stream.Features.Register>         (Namespaces.FeatureIqRegister,       "register");
//            RegisterElement <Xmpp.Stream.Features.RosterVersioning> (Namespaces.FeatureRosterVersioning, "ver");
//            RegisterElement <Xmpp.Stream.Features.Compression>      (Namespaces.FeatureCompress,          "compression");
//            RegisterElement<Xmpp.Stream.Features.Bidi>              (Namespaces.FeatureBidi,              "bidi");
            

//            // SASL
//            RegisterElement <Xmpp.Sasl.Mechanisms>           (Namespaces.Sasl,              "mechanisms");
//            RegisterElement <Xmpp.Sasl.Mechanism>            (Namespaces.Sasl,              "mechanism");
//            RegisterElement <Xmpp.Sasl.Auth>                 (Namespaces.Sasl,              "auth");
//            RegisterElement <Xmpp.Sasl.Response>             (Namespaces.Sasl,              "response");
//            RegisterElement <Xmpp.Sasl.Success>              (Namespaces.Sasl,              "success");
//            RegisterElement <Xmpp.Sasl.Failure>              (Namespaces.Sasl,              "failure");
//            RegisterElement <Xmpp.Sasl.Challenge>            (Namespaces.Sasl,              "challenge");
//            RegisterElement <Xmpp.Sasl.Hostname>             (Namespaces.SaslHostname0,     "hostname");
//            RegisterElement <Xmpp.Sasl.Hostname>             (Namespaces.SaslHostname1,     "hostname");
            
//            // main stanzas
//            RegisterElement <Xmpp.Client.Iq>                 (Namespaces.Client, Tag.Iq);
//            RegisterElement <Xmpp.Client.Message>            (Namespaces.Client, Tag.Message);
//            RegisterElement <Xmpp.Client.Presence>           (Namespaces.Client, Tag.Presence);

//            // Error
//            RegisterElement <Xmpp.Client.Error>              (Namespaces.Client, "error");

//#if WIN || MONO
//            // component namespace
//            RegisterElement <Xmpp.Component.Error>           (Namespaces.Accept, "error");
//            RegisterElement <Xmpp.Component.Iq>              (Namespaces.Accept, Tag.Iq);
//            RegisterElement <Xmpp.Component.Message>         (Namespaces.Accept, Tag.Message);
//            RegisterElement <Xmpp.Component.Presence>        (Namespaces.Accept, Tag.Presence);
//            RegisterElement <Xmpp.Component.Handshake>       (Namespaces.Accept, "handshake");

//            RegisterElement<Xmpp.Server.Error>               (Namespaces.Server, "error");
//            RegisterElement<Xmpp.Server.Iq>                  (Namespaces.Server, Tag.Iq);
//            RegisterElement<Xmpp.Server.Message>             (Namespaces.Server, Tag.Message);
//            RegisterElement<Xmpp.Server.Presence>            (Namespaces.Server, Tag.Presence);
//#endif
//            // StartTls
//            RegisterElement<Xmpp.Tls.StartTls>(Namespaces.Tls, Tag.StartTls);
//            RegisterElement<Xmpp.Tls.Proceed>(Namespaces.Tls, Tag.Proceed);

//            // Bind
//            RegisterElement <Xmpp.Bind.Bind>                 (Namespaces.Bind, Tag.Bind);
//            // Session
//            RegisterElement <Xmpp.Session.Session>           (Namespaces.Session, Tag.Session);
//            // Roster
//            RegisterElement <Xmpp.Roster.Roster>             (Namespaces.IqRoster, Tag.Query);
//            RegisterElement <Xmpp.Roster.RosterItem>         (Namespaces.IqRoster, Tag.Item);
            
//            RegisterElement <Xmpp.Stream.Error>              (Namespaces.Stream, Tag.Error);
            
//            RegisterElement <Xmpp.Stream.Errors.SeeOtherHost>(Namespaces.Streams, "see-other-host");

//            // XData
//            RegisterElement <Xmpp.XData.Data>                (Namespaces.XData, "x");
//            RegisterElement <Xmpp.XData.Option>              (Namespaces.XData, "option");
//            RegisterElement <Xmpp.XData.Field>               (Namespaces.XData, "field");
//            RegisterElement <Xmpp.XData.Value>               (Namespaces.XData, "value");
//            RegisterElement <Xmpp.XData.Reported>            (Namespaces.XData, "reported");
//            RegisterElement <Xmpp.XData.Item>                (Namespaces.XData, "item");

//            // OOB
//            RegisterElement <Xmpp.Oob.Oob>                      (Namespaces.IqOob, "query");

//            // Stream Compression
//            RegisterElement <Xmpp.Compression.Compresed>        (Namespaces.Compress,          "compressed");
//            RegisterElement <Xmpp.Compression.Compress>         (Namespaces.Compress,          "compress");
//            RegisterElement <Xmpp.Compression.Failure>          (Namespaces.Compress,          "failure");
//            //RegisterElement <Xmpp.Compression.Compression>      (Namespaces.Compress,          "compression");
//            RegisterElement <Xmpp.Compression.Method>           (Namespaces.Compress,          "method");
//            RegisterElement <Xmpp.Compression.Features.Method>  (Namespaces.FeatureCompress,  "method");

//            // BOSH            
//            RegisterElement <Xmpp.Bosh.Body>                 (Namespaces.HttpBind, "body");
            
//            // Disco
//            RegisterElement <Xmpp.Disco.Items>               (Namespaces.DiscoItems,   "query");
//            RegisterElement <Xmpp.Disco.Item>                (Namespaces.DiscoItems,   "item");
//            RegisterElement <Xmpp.Disco.Info>                (Namespaces.DiscoInfo,    "query");
//            RegisterElement <Xmpp.Disco.Identity>            (Namespaces.DiscoInfo,    "identity");
//            RegisterElement <Xmpp.Disco.Feature>             (Namespaces.DiscoInfo,    "feature");
            
//            // Muc
//            RegisterElement <Xmpp.Muc.X>                     (Namespaces.Muc,           "x");
//            RegisterElement <Xmpp.Muc.User.X>                (Namespaces.MucUser,      "x");
//            RegisterElement <Xmpp.Muc.History>               (Namespaces.Muc,           "history");
//            RegisterElement <Xmpp.Muc.User.Status>           (Namespaces.MucUser,      Tag.Status);
//            RegisterElement <Xmpp.Muc.User.Continue>         (Namespaces.MucUser,      "continue");
//            RegisterElement <Xmpp.Muc.User.Item>             (Namespaces.MucUser,      "item");
//            RegisterElement <Xmpp.Muc.Admin.Item>            (Namespaces.MucAdmin,     "item");
//            RegisterElement <Xmpp.Muc.Admin.AdminQuery>      (Namespaces.MucAdmin,     "query");
//            RegisterElement <Xmpp.Muc.Owner.OwnerQuery>      (Namespaces.MucOwner,     "query");
//            RegisterElement <Xmpp.Muc.User.Actor>            (Namespaces.MucUser,      "actor");
//            RegisterElement <Xmpp.Muc.User.Decline>          (Namespaces.MucUser,      "decline");
//            RegisterElement <Xmpp.Muc.User.Invite>           (Namespaces.MucUser,      "invite");
//            RegisterElement <Xmpp.Muc.User.Destroy>          (Namespaces.MucUser,      "destroy");
//            RegisterElement <Xmpp.Muc.Owner.Destroy>         (Namespaces.MucOwner,     "destroy");

//            // Direct invites
//            RegisterElement<Xmpp.Muc.Conference>             (Namespaces.XConference,  "x");
            
//            // Register
//            RegisterElement <Xmpp.Register.Register>         (Namespaces.IqRegister,   Tag.Query);
//            // Auth
//            RegisterElement<Xmpp.Auth.Auth>                  (Namespaces.IqAuth, Tag.Query);
            
//            // Nickname
//            RegisterElement <Xmpp.Nickname.Nick>             (Namespaces.Nick,          "nick");

//            RegisterElement <Xmpp.Vcard.Vcard>               (Namespaces.Vcard,         "vCard");
//            RegisterElement <Xmpp.Vcard.Telephone>           (Namespaces.Vcard,         "TEL");
//            RegisterElement <Xmpp.Vcard.Email>               (Namespaces.Vcard,         "EMAIL");
//            RegisterElement <Xmpp.Vcard.Address>             (Namespaces.Vcard,         "ADR");

//            RegisterElement <Xmpp.Vcard.Photo>               (Namespaces.Vcard,         "PHOTO");

//            RegisterElement <Xmpp.Vcard.Update.X>            (Namespaces.VcardUpdate,  "x");

//            // Delay
//            RegisterElement <Xmpp.Delay.XDelay>              (Namespaces.XDelay,       "x");
//            RegisterElement <Xmpp.Delay.Delay>               (Namespaces.Delay,         "delay");
            
//            // PubSub
//            RegisterElement <Xmpp.PubSub.Event.Collection>   (Namespaces.PubsubEvent, "collection");
//            RegisterElement <Xmpp.PubSub.Event.Associate>    (Namespaces.PubsubEvent, "associate");
//            RegisterElement <Xmpp.PubSub.Event.Disassociate> (Namespaces.PubsubEvent, "disassociate");
//            RegisterElement <Xmpp.PubSub.Event.Event>        (Namespaces.PubsubEvent, "event");
//            RegisterElement <Xmpp.PubSub.Event.Configuration>(Namespaces.PubsubEvent, "configuration");
//            RegisterElement <Xmpp.PubSub.Event.Delete>       (Namespaces.PubsubEvent, "delete");
//            RegisterElement <Xmpp.PubSub.Event.Item>         (Namespaces.PubsubEvent, "item");
//            RegisterElement <Xmpp.PubSub.Event.Items>        (Namespaces.PubsubEvent, "items");
//            RegisterElement <Xmpp.PubSub.Event.Purge>        (Namespaces.PubsubEvent, "purge");
//            RegisterElement <Xmpp.PubSub.Event.Retract>      (Namespaces.PubsubEvent, "retract");
//            RegisterElement <Xmpp.PubSub.Event.Subscription> (Namespaces.PubsubEvent, "subscription");

//            RegisterElement <Xmpp.PubSub.Owner.PubSub>       (Namespaces.PubsubOwner, "pubsub");
//            RegisterElement <Xmpp.PubSub.Owner.Configure>    (Namespaces.PubsubOwner, "configure");
//            RegisterElement <Xmpp.PubSub.Owner.Delete>       (Namespaces.PubsubOwner, "delete");
//            RegisterElement <Xmpp.PubSub.Owner.Purge>        (Namespaces.PubsubOwner, "purge");
//            RegisterElement <Xmpp.PubSub.Owner.Subscriptions>(Namespaces.PubsubOwner, "subscriptions");
//            RegisterElement <Xmpp.PubSub.Owner.Subscription> (Namespaces.PubsubOwner, "subscription");
//            RegisterElement <Xmpp.PubSub.Owner.Affiliations> (Namespaces.PubsubOwner, "affiliations");
//            RegisterElement <Xmpp.PubSub.Owner.Affiliation>  (Namespaces.PubsubOwner, "affiliation");

//            RegisterElement <Xmpp.PubSub.PubSub>             (Namespaces.Pubsub,       "pubsub");
//            RegisterElement <Xmpp.PubSub.Configure>          (Namespaces.Pubsub,       "configure");
//            RegisterElement <Xmpp.PubSub.Create>             (Namespaces.Pubsub,       "create");
//            RegisterElement <Xmpp.PubSub.Subscribe>          (Namespaces.Pubsub,       "subscribe");
//            RegisterElement <Xmpp.PubSub.Unsubscribe>        (Namespaces.Pubsub,       "unsubscribe");
//            RegisterElement <Xmpp.PubSub.Publish>            (Namespaces.Pubsub,       "publish");
//            RegisterElement <Xmpp.PubSub.Options>            (Namespaces.Pubsub,       "options");
      

//            RegisterElement <Xmpp.PubSub.Items>              (Namespaces.Pubsub,       "items");
//            RegisterElement <Xmpp.PubSub.Item>               (Namespaces.Pubsub,       "item");
//            RegisterElement <Xmpp.PubSub.Retract>            (Namespaces.Pubsub,       "retract");
//            RegisterElement <Xmpp.PubSub.SubscribeOptions>   (Namespaces.Pubsub,       "subscribe-options");
//            RegisterElement <Xmpp.PubSub.Subscription>       (Namespaces.Pubsub,       "subscription");
//            RegisterElement <Xmpp.PubSub.Subscriptions>      (Namespaces.Pubsub,       "subscriptions");
//            RegisterElement <Xmpp.PubSub.Affiliation>        (Namespaces.Pubsub,       "affiliation");
//            RegisterElement <Xmpp.PubSub.Affiliations>       (Namespaces.Pubsub,       "affiliations");

//            //Bytestreams
//            RegisterElement <Xmpp.Bytestreams.Activate>      (Namespaces.Bytestreams,  "activate");
//            RegisterElement <Xmpp.Bytestreams.StreamhostUsed>(Namespaces.Bytestreams,  "streamhost-used");
//            RegisterElement <Xmpp.Bytestreams.Streamhost>    (Namespaces.Bytestreams,  "streamhost");
//            RegisterElement <Xmpp.Bytestreams.Bytestream>    (Namespaces.Bytestreams,  Tag.Query);

//            // Feature Negotiation
//            RegisterElement <Xmpp.FeatureNegotiation.Feature>(Namespaces.FeatureNeg,  "feature");

//            // SI
//            RegisterElement <Xmpp.StreamInitiation.SI>       (Namespaces.SI,           "si");
         
//            // SI -> Profile -> FileTranfer
//            RegisterElement <Xmpp.StreamInitiation.Profile.FileTansfer.File> (Namespaces.SIProfileFileTransfer, "file");
//            RegisterElement <Xmpp.StreamInitiation.Profile.FileTansfer.Range>(Namespaces.SIProfileFileTransfer, "range");
          
//            // Transport
//            RegisterElement<Xmpp.IBB.Open>  (Namespaces.Ibb, "open");
//            RegisterElement<Xmpp.IBB.Close> (Namespaces.Ibb, "close");
//            RegisterElement<Xmpp.IBB.Data>  (Namespaces.Ibb, "data");

//            // Bookmarks
//            RegisterElement<Xmpp.Bookmarks.Conference>  (Namespaces.StorageBookmarks, "conference");
//            RegisterElement<Xmpp.Bookmarks.Storage>     (Namespaces.StorageBookmarks, "storage");

//            // private
//            RegisterElement<Xmpp.Private.Private>       (Namespaces.IqPrivate, Tag.Query);

//            // ad hoc commadns
//            RegisterElement<Xmpp.AdHocCommands.Command> (Namespaces.AdHocCommands, "command");
//            RegisterElement<Xmpp.AdHocCommands.Actions> (Namespaces.AdHocCommands, "actions");
//            RegisterElement<Xmpp.AdHocCommands.Note>    (Namespaces.AdHocCommands, "note");

//            // Chatstates Jep-0085
//            RegisterElement<Xmpp.Chatstates.Active>     (Namespaces.Chatstates, Xmpp.Chatstates.Chatstate.Active.ToString().ToLower());
//            RegisterElement<Xmpp.Chatstates.Composing>  (Namespaces.Chatstates, Xmpp.Chatstates.Chatstate.Composing.ToString().ToLower());
//            RegisterElement<Xmpp.Chatstates.Gone>       (Namespaces.Chatstates, Xmpp.Chatstates.Chatstate.Gone.ToString().ToLower());
//            RegisterElement<Xmpp.Chatstates.Inactive>   (Namespaces.Chatstates, Xmpp.Chatstates.Chatstate.Inactive.ToString().ToLower());
//            RegisterElement<Xmpp.Chatstates.Paused>     (Namespaces.Chatstates, Xmpp.Chatstates.Chatstate.Paused.ToString().ToLower());
            
            
//            // Jingle
//            RegisterElement<Xmpp.Jingle.Jingle>             (Namespaces.Jingle,                "jingle");
//            RegisterElement<Xmpp.Jingle.Content>            (Namespaces.Jingle,                "content");
//            RegisterElement<Xmpp.Jingle.Reason>             (Namespaces.Jingle,                "reason");
//            RegisterElement<Xmpp.Jingle.AlternativeSession> (Namespaces.Jingle,                "alternative-session");
            
//            RegisterElement<TransportIbb>               (Namespaces.JingleTransportIbb,      "transport");
//            RegisterElement<TransportRawUdp>            (Namespaces.JingleTransportRawUdp,  "transport");
//            RegisterElement<TransportIceUdp>            (Namespaces.JingleTransportIceUdp,  "transport");
//            RegisterElement<CandidateIceUdp>            (Namespaces.JingleTransportIceUdp,  "candidate");
//            RegisterElement<CandidateRawUdp>            (Namespaces.JingleTransportRawUdp,  "candidate");
//            RegisterElement<Description>                (Namespaces.JingleAppsRtp,           "description");
//            RegisterElement<PayloadType>                (Namespaces.JingleAppsRtp,           "payload-type");
            

//            // XHtml-IM
//            RegisterElement<Xmpp.XHtmlIM.Html>          (Namespaces.XhtmlIm,  "html");
//            RegisterElement<Xmpp.XHtmlIM.Body>          (Namespaces.Xhtml,     "body");
//            RegisterElement<Xmpp.XHtmlIM.P>(Namespaces.Xhtml, "p");
//            RegisterElement<Xmpp.XHtmlIM.A>(Namespaces.Xhtml, "a");
//            RegisterElement<Xmpp.XHtmlIM.H1>(Namespaces.Xhtml, "h1");
//            RegisterElement<Xmpp.XHtmlIM.H2>(Namespaces.Xhtml, "h2");
//            RegisterElement<Xmpp.XHtmlIM.H3>(Namespaces.Xhtml, "h3");
//            RegisterElement<Xmpp.XHtmlIM.H4>(Namespaces.Xhtml, "h4");
//            RegisterElement<Xmpp.XHtmlIM.H5>(Namespaces.Xhtml, "h5");
//            RegisterElement<Xmpp.XHtmlIM.H6>(Namespaces.Xhtml, "h6");
//            RegisterElement<Xmpp.XHtmlIM.Img>(Namespaces.Xhtml, "img");
//            RegisterElement<Xmpp.XHtmlIM.Span>(Namespaces.Xhtml, "span");
//            RegisterElement<Xmpp.XHtmlIM.Strong>(Namespaces.Xhtml, "strong");
//            RegisterElement<Xmpp.XHtmlIM.Abbr>(Namespaces.Xhtml, "abbr");
//            RegisterElement<Xmpp.XHtmlIM.Acronym>(Namespaces.Xhtml, "acronym");
//            RegisterElement<Xmpp.XHtmlIM.Address>(Namespaces.Xhtml, "address");
//            RegisterElement<Xmpp.XHtmlIM.Blockquote>(Namespaces.Xhtml, "blockquote");
//            RegisterElement<Xmpp.XHtmlIM.Br>(Namespaces.Xhtml, "br");
//            RegisterElement<Xmpp.XHtmlIM.Cite>(Namespaces.Xhtml, "cite");
//            RegisterElement<Xmpp.XHtmlIM.Code>(Namespaces.Xhtml, "code");
//            RegisterElement<Xmpp.XHtmlIM.Dfn>(Namespaces.Xhtml, "dfn");
//            RegisterElement<Xmpp.XHtmlIM.Div>(Namespaces.Xhtml, "div");
//            RegisterElement<Xmpp.XHtmlIM.Em>(Namespaces.Xhtml, "em");
//            RegisterElement<Xmpp.XHtmlIM.Pre>(Namespaces.Xhtml, "pre");
//            RegisterElement<Xmpp.XHtmlIM.Q>(Namespaces.Xhtml, "q");
//            RegisterElement<Xmpp.XHtmlIM.Samp>(Namespaces.Xhtml, "samp");
//            RegisterElement<Xmpp.XHtmlIM.Var>(Namespaces.Xhtml, "var");
//            RegisterElement<Xmpp.XHtmlIM.Head>(Namespaces.Xhtml, "head");
//            RegisterElement<Xmpp.XHtmlIM.Title>(Namespaces.Xhtml, "title");
//            RegisterElement<Xmpp.XHtmlIM.Dl>(Namespaces.Xhtml, "dl");
//            RegisterElement<Xmpp.XHtmlIM.Dt>(Namespaces.Xhtml, "dt");
//            RegisterElement<Xmpp.XHtmlIM.Dd>(Namespaces.Xhtml, "dd");
//            RegisterElement<Xmpp.XHtmlIM.Ol>(Namespaces.Xhtml, "ol");
//            RegisterElement<Xmpp.XHtmlIM.Ul>(Namespaces.Xhtml, "ul");
//            RegisterElement<Xmpp.XHtmlIM.Li>(Namespaces.Xhtml, "li");
            
//            // Xmpp Ping
//            RegisterElement<Xmpp.Ping.Ping>             (Namespaces.Ping,      "ping");

//            // Caps
//            RegisterElement<Xmpp.Capabilities.Caps>     (Namespaces.Caps,      "c");

//            // urn time
//            RegisterElement<Xmpp.Time.Time>             (Namespaces.Time,      "time");

//            // stream managment
//            RegisterElement<Xmpp.Stream.Features.StreamManagement>  (Namespaces.FeatureStreamManagement, "sm");
//            RegisterElement<Xmpp.StreamManagement.Enable>           (Namespaces.FeatureStreamManagement, "enable");
//            RegisterElement<Xmpp.StreamManagement.Enabled>          (Namespaces.FeatureStreamManagement, "enabled");
//            RegisterElement<Xmpp.StreamManagement.Failed>           (Namespaces.FeatureStreamManagement, "failed");
//            RegisterElement<Xmpp.StreamManagement.Ack.Answer>       (Namespaces.FeatureStreamManagement, "a");
//            RegisterElement<Xmpp.StreamManagement.Ack.Request>      (Namespaces.FeatureStreamManagement, "r");
//            RegisterElement<Xmpp.StreamManagement.Resume>           (Namespaces.FeatureStreamManagement, "resume");
//            RegisterElement<Xmpp.StreamManagement.Resumed>          (Namespaces.FeatureStreamManagement, "resumed");

//            // extended Stanza addressing
//            RegisterElement<Xmpp.ExtendedStanzaAddressing.Address>  (Namespaces.ExtendedStanzaAdressing, "address");
//            RegisterElement<Xmpp.ExtendedStanzaAddressing.Addresses>(Namespaces.ExtendedStanzaAdressing, "addresses");

//            // privacy
//            RegisterElement<Xmpp.Privacy.Item>      (Namespaces.IqPrivacy, "item");
//            RegisterElement<Xmpp.Privacy.List>      (Namespaces.IqPrivacy, "list");
//            RegisterElement<Xmpp.Privacy.Active>    (Namespaces.IqPrivacy, "active");
//            RegisterElement<Xmpp.Privacy.Default>   (Namespaces.IqPrivacy, "default");
//            RegisterElement<Xmpp.Privacy.Privacy>   (Namespaces.IqPrivacy, Tag.Query);

//            //search
//            RegisterElement<Xmpp.Search.Search>     (Namespaces.IqSearch, Tag.Query);
//            RegisterElement<Xmpp.Search.SearchItem> (Namespaces.IqSearch, "item");

//            //XEP-0184: Message Delivery Receipts
//            RegisterElement<Xmpp.Receipts.Received> (Namespaces.MessageReceipts, "received");
//            RegisterElement<Xmpp.Receipts.Request>  (Namespaces.MessageReceipts, "request");

//            // XEP-0224 Attention
//            RegisterElement<Xmpp.Attention.Attention>  (Namespaces.Attention, "attention");

//            // Cache
//            RegisterElement<IO.Cache.Value>(Namespaces.AgsCache, "value");
//            RegisterElement<IO.Cache.Cache>(Namespaces.AgsCache, "cache");

//            // GeoLoc
//            RegisterElement<Xmpp.GeoLoc.GeoLoc>(Namespaces.Geoloc, "geoloc");

//            // RSM
//            RegisterElement<Xmpp.ResultSetManagement.Set>   (Namespaces.Rsm, "set");
//            RegisterElement<Xmpp.ResultSetManagement.First> (Namespaces.Rsm, "first");

//            // Windows Live Messenger
//            RegisterElement<Xmpp.WindowsLiveMessenger.GetJid>(Namespaces.WlmJidLookup, "getjid");

//            // jabber:iq:version
//            RegisterElement<Xmpp.Version.Version>           (Namespaces.IqVersion, Tag.Query);

//            RegisterElement<Xmpp.Last.Last>                 (Namespaces.IqLast, Tag.Query);
//            RegisterElement<Xmpp.Oob.XOob>                  (Namespaces.XOob, "x");

//            // Google Push
//            RegisterElement<Xmpp.Google.Push.Subscribe>     (Namespaces.GooglePush, "subscribe");
//            RegisterElement<Xmpp.Google.Push.Recipient>     (Namespaces.GooglePush, "recipient");
//            RegisterElement<Xmpp.Google.Push.Data>          (Namespaces.GooglePush, "data");
//            RegisterElement<Xmpp.Google.Push.Item>          (Namespaces.GooglePush, "item");

//            // Google GCM
//            RegisterElement<Xmpp.Google.Mobile.Gcm>         (Namespaces.GoogleMobileData, "gcm");

//            // XEP-0144 roster item exchange
//            RegisterElement<Xmpp.RosterItemExchange.Exchange>           (Namespaces.XRosterX, "x");
//            RegisterElement<Xmpp.RosterItemExchange.RosterExchangeItem> (Namespaces.XRosterX, "item");

//            // XEP-0191: Blocking Command
//            RegisterElement<Xmpp.Blocking.Block>        (Namespaces.Blocking, "block");
//            RegisterElement<Xmpp.Blocking.Unblock>      (Namespaces.Blocking, "unblock");
//            RegisterElement<Xmpp.Blocking.Blocklist>    (Namespaces.Blocking, "blocklist");
//            RegisterElement<Xmpp.Blocking.Item>         (Namespaces.Blocking, "item");
            
//            // XEP-0107: User Mood
//            RegisterElement<Xmpp.Mood.Mood>             (Namespaces.Mood, "mood");

//            // XEP-0118: User Tune
//            RegisterElement<Xmpp.Tune.Tune>             (Namespaces.Tune, "tune");

//            // XEP-0009: Xml_RPC
//            RegisterElement<Xmpp.Rpc.Rpc>               (Namespaces.IqRpc, Tag.Query);
//            RegisterElement<Xmpp.Rpc.MethodCall>        (Namespaces.IqRpc, "methodCall");
//            RegisterElement<Xmpp.Rpc.MethodResponse>    (Namespaces.IqRpc, "methodResponse");
//            RegisterElement<Xmpp.Rpc.Params>            (Namespaces.IqRpc, "params");
//            RegisterElement<Xmpp.Rpc.Param>             (Namespaces.IqRpc, "param");
//            RegisterElement<Xmpp.Rpc.Value>             (Namespaces.IqRpc, "value");
//            RegisterElement<Xmpp.Rpc.Fault>             (Namespaces.IqRpc, "fault");
//            RegisterElement<Xmpp.Rpc.Member>            (Namespaces.IqRpc, "member");
//            RegisterElement<Xmpp.Rpc.Name>              (Namespaces.IqRpc, "name");
//            RegisterElement<Xmpp.Rpc.Struct>            (Namespaces.IqRpc, "struct");
//            RegisterElement<Xmpp.Rpc.Data>              (Namespaces.IqRpc, "data");
//            RegisterElement<Xmpp.Rpc.Array>             (Namespaces.IqRpc, "array");

//            // XEP-0258: Security Labels in XMPP
//            RegisterElement<Xmpp.SecurityLabels.Catalog>            (Namespaces.SecurityLabelCatalog, "catalog");
//            RegisterElement<Xmpp.SecurityLabels.DisplayMarking>     (Namespaces.SecurityLabel, "displaymarking");
//            RegisterElement<Xmpp.SecurityLabels.EquivalentLabel>    (Namespaces.SecurityLabel, "equivalentlabel");
//            RegisterElement<Xmpp.SecurityLabels.EssSecurityLabel>   (Namespaces.SecurityLabelEss, "esssecuritylabel");
//            RegisterElement<Xmpp.SecurityLabels.Item>               (Namespaces.SecurityLabelCatalog, "item");
//            RegisterElement<Xmpp.SecurityLabels.Label>              (Namespaces.SecurityLabel, "label");
//            RegisterElement<Xmpp.SecurityLabels.SecurityLabel>      (Namespaces.SecurityLabel, "securitylabel");

//            // XEP-0079: Advanced Message Processing
//            RegisterElement<Xmpp.AdvancedMessageProcessing.Amp>                     (Namespaces.AMP, "amp");
//            RegisterElement<Xmpp.AdvancedMessageProcessing.Rule>                    (Namespaces.AMP, "rule");
//            RegisterElement<Xmpp.AdvancedMessageProcessing.InvalidRules>            (Namespaces.AMP, "invalid-rules");
//            RegisterElement<Xmpp.AdvancedMessageProcessing.UnsupportedActions>      (Namespaces.AMP, "unsupported-actions");
//            RegisterElement<Xmpp.AdvancedMessageProcessing.UnsupportedConditions>   (Namespaces.AMP, "unsupported-conditions");

//            // Dialback
//            RegisterElement<Xmpp.Dialback.Verify>   (Namespaces.ServerDialback, "verify");
            
//            // Archiving
//            RegisterElement<Xmpp.MessageArchiving.List>        (Namespaces.Archiving, "list");
//            RegisterElement<Xmpp.MessageArchiving.To>          (Namespaces.Archiving, "to");
//            RegisterElement<Xmpp.MessageArchiving.From>        (Namespaces.Archiving, "from");
//            RegisterElement<Xmpp.MessageArchiving.Chat>        (Namespaces.Archiving, "chat");
//            RegisterElement<Xmpp.MessageArchiving.Next>        (Namespaces.Archiving, "next");
//            RegisterElement<Xmpp.MessageArchiving.Previous>    (Namespaces.Archiving, "previous");
//            //RegisterElement<Xmpp.MessageArchiving.Next>        (Namespaces.Archiving, "previous");
//            RegisterElement<Xmpp.MessageArchiving.Save>        (Namespaces.Archiving, "save");
//            RegisterElement<Xmpp.MessageArchiving.Retrieve>    (Namespaces.Archiving, "retrieve");
//            RegisterElement<Xmpp.MessageArchiving.Removed>     (Namespaces.Archiving, "removed");
//            RegisterElement<Xmpp.MessageArchiving.Remove>      (Namespaces.Archiving, "remove");
//            RegisterElement<Xmpp.MessageArchiving.Changed>     (Namespaces.Archiving, "changed");
//            RegisterElement<Xmpp.MessageArchiving.Modified>    (Namespaces.Archiving, "modified");
//            //RegisterElement<Xmpp.MessageArchiving.Save>        (Namespaces.Archiving, "save");
//            RegisterElement<Xmpp.MessageArchiving.Default>     (Namespaces.Archiving, "default");
//            RegisterElement<Xmpp.MessageArchiving.Preferences> (Namespaces.Archiving, "pref");
//            RegisterElement<Xmpp.MessageArchiving.Auto>        (Namespaces.Archiving, "auto");
//            RegisterElement<Xmpp.MessageArchiving.Method>      (Namespaces.Archiving, "method");
//            RegisterElement<Xmpp.MessageArchiving.Item>        (Namespaces.Archiving, "item");
//            RegisterElement<Xmpp.MessageArchiving.ItemRemove>  (Namespaces.Archiving, "itemremove");
//            RegisterElement<Xmpp.MessageArchiving.Session>     (Namespaces.Archiving, "session");
//            RegisterElement<Xmpp.MessageArchiving.Note>         (Namespaces.Archiving, "note");

//            // Message Carbons
//            RegisterElement<Xmpp.MessageCarbons.Disable>    (Namespaces.MessageCarbons, "disable");
//            RegisterElement<Xmpp.MessageCarbons.Enable>     (Namespaces.MessageCarbons, "enable");
//            RegisterElement<Xmpp.MessageCarbons.Private>    (Namespaces.MessageCarbons, "private");
//            RegisterElement<Xmpp.MessageCarbons.Received>   (Namespaces.MessageCarbons, "received");
//            RegisterElement<Xmpp.MessageCarbons.Sent>       (Namespaces.MessageCarbons, "sent");
//            RegisterElement<Xmpp.MessageCarbons.Forwarded>  (Namespaces.MessageCarbons, "forwarded");

//            // XEP-0131: Stanza Headers and Internet Metadata
//            RegisterElement<Xmpp.Shim.Header>   (Namespaces.Shim, "header");
//            RegisterElement<Xmpp.Shim.Headers>  (Namespaces.Shim, "headers");

//            // XEP-0308: Last Message Correction
//            RegisterElement<Xmpp.LastMessageCorrection.Replace>(Namespaces.LastMessageCorrection, "replace");

//            RegisterElement<Xmpp.MessageEvents.Event>(Namespaces.XEvent, "x");

//            RegisterElement<Xmpp.Framing.Open>  (Namespaces.Framing, "open");
//            RegisterElement<Xmpp.Framing.Close> (Namespaces.Framing, "close");
        }
        #endregion

        #region register over attributes   
        private static void RegisterElement(Type type)
        {
            type
                .GetTypeInfo()
                .GetCustomAttributes<XmppTagAttribute>(false)
                .ToList()
                .ForEach(att =>
                    RegisterElement(type, att.Namespace, att.Name)
                );
        }

        /// <summary>
        /// Registers the element.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void RegisterElement<T>() where T : XmppXElement
        {
            var t = typeof(T).GetTypeInfo();
            var att = t.GetCustomAttributes(typeof(XmppTagAttribute), false).FirstOrDefault() as XmppTagAttribute;

            if (att != null)
                RegisterElement<T>(att.Namespace, att.Name);
        }

        /// <summary>
        /// Looks in a complete assembly for all XmppXElements and registered them using the XmppTag attribute.
        /// The XmppTag attribute must be present on the classes to register
        /// </summary>
        /// <param name="assembly"></param>
        public static void RegisterElementsFromAssembly(Assembly assembly)
        {
            var types = GetTypesWithAttribueFromAssembly<XmppTagAttribute>(assembly);

            foreach (var type in types)
                RegisterElement(type.GetType());

        }

        private static void RegisterFromAttributes()
        {
            RegisterElementsFromAssembly(typeof(Factory).GetTypeInfo().Assembly);
        }
        private static IEnumerable<TypeInfo> GetTypesWithAttribueFromAssembly<TAttribute>(Assembly assembly) where TAttribute : Attribute
        {
            return assembly
                .DefinedTypes
                .Where(t => t.IsSubclassOf(typeof(XmppXElement)))
                .Where(t => t.IsDefined(typeof(TAttribute), false));
        }
        #endregion
    }
}