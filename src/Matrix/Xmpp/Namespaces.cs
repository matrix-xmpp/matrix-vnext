/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

namespace Matrix.Xmpp
{
    /// <summary>
    /// XMPP namespaces
    /// </summary>
    public class Namespaces
    {
        /// <summary>http://etherx.jabber.org/streams</summary>
        public const string Stream = "http://etherx.jabber.org/streams";
        /// <summary>Client to Server default namespace</summary>
        public const string Client = "jabber:client";
        /// <summary>Server to Server default namespace</summary>
        public const string Server = "jabber:server";
        /// <summary>Server Dialback</summary>
        public const string ServerDialback = "jabber:server:dialback";

        /// <summary>
        /// jabber:iq:agents
        /// </summary>
        public const string IqAgents = "jabber:iq:agents";
        /// <summary>
        /// jabber:iq:roster
        /// </summary>
        public const string IqRoster = "jabber:iq:roster";
        /// <summary>
        /// jabber:iq:auth
        /// </summary>
        public const string IqAuth = "jabber:iq:auth";
        /// <summary>
        /// jabber:iq:register
        /// </summary>
        public const string IqRegister = "jabber:iq:register";
        /// <summary>jabber:iq:oob</summary>
        public const string IqOob = "jabber:iq:oob";
        /// <summary>jabber:iq:last</summary>
        public const string IqLast = "jabber:iq:last";
        /// <summary>jabber:iq:time</summary>
        public const string IqTime = "jabber:iq:time";
        /// <summary>jabber:iq:version</summary>
        public const string IqVersion = "jabber:iq:version";
        /// <summary>jabber:iq:browse</summary>
        public const string IqBrowse = "jabber:iq:browse";
        /// <summary>jabber:iq:search</summary>
        public const string IqSearch = "jabber:iq:search";
        /// <summary>jabber:iq:avatar</summary>
        public const string IqAvatar = "jabber:iq:avatar";
        /// <summary>jabber:iq:private</summary>
        public const string IqPrivate = "jabber:iq:private";
        /// <summary>jabber:iq:privacy</summary>
        public const string IqPrivacy = "jabber:iq:privacy";

        /// <summary>XEP-0009: Jabber-RPC (jabber:iq:rpc)</summary>
        public const string IqRpc = "jabber:iq:rpc";

        /// <summary>jabber:x:oob</summary>
        public const string XOob = "jabber:x:oob";
        /// <summary>XEP-0091: Delayed Delivery (jabber:x:delay)</summary>
        public const string XDelay = "jabber:x:delay";

        /// <summary>XEP-0203: Delayed Delivery (urn:xmpp:delay)</summary>
        public const string Delay = "urn:xmpp:delay";

        /// <summary>(jabber:x:event)</summary>
        public const string XEvent = "jabber:x:event";
        /// <summary>(jabber:x:avatar)</summary>
        public const string XAvatar = "jabber:x:avatar";

        /// <summary>(jabber:x:conference)</summary>
        public const string XConference = "jabber:x:conference";

        /// <summary>
        /// (jabber:x:data)
        /// </summary>
        public const string XData = "jabber:x:data";

        /// <summary>XEP-0144: Roster Item Exchange (http://jabber.org/protocol/rosterx)</summary>
        public const string XRosterX = "http://jabber.org/protocol/rosterx";


        /// <summary>XEP-0045: Multi User Chat (http://jabber.org/protocol/muc)</summary>
        public const string Muc = "http://jabber.org/protocol/muc";

        /// <summary>XEP-0045: Multi User Chat (http://jabber.org/protocol/muc#user)</summary>
        public const string MucUser = "http://jabber.org/protocol/muc#user";

        /// <summary>XEP-0045: Multi User Chat (http://jabber.org/protocol/muc#admin)</summary>
        public const string MucAdmin = "http://jabber.org/protocol/muc#admin";
        /// <summary>XEP-0045: Multi User Chat (http://jabber.org/protocol/muc#owner)</summary>
        public const string MucOwner = "http://jabber.org/protocol/muc#owner";


        /// <summary>XEP-0030: Service Disovery (http://jabber.org/protocol/disco#items)</summary>
        public const string DiscoItems = "http://jabber.org/protocol/disco#items";

        /// <summary>XEP-0030: Service Disovery (http://jabber.org/protocol/disco#info)</summary>
        public const string DiscoInfo = "http://jabber.org/protocol/disco#info";

        /// <summary>(storage:client:avatar)</summary>
        public const string StorageAvatar = "storage:client:avatar";

        /// <summary>(vcard-temp)</summary>
        public const string Vcard = "vcard-temp";


        /// <summary>(urn:ietf:params:xml:ns:xmpp-streams)</summary>
        public const string Streams = "urn:ietf:params:xml:ns:xmpp-streams";
        /// <summary>(urn:ietf:params:xml:ns:xmpp-stanzas)</summary>
        public const string Stanzas = "urn:ietf:params:xml:ns:xmpp-stanzas";

        /// <summary>Tls namespace (urn:ietf:params:xml:ns:xmpp-tls)</summary>
        public const string Tls = "urn:ietf:params:xml:ns:xmpp-tls";
        /// <summary>Sasl (urn:ietf:params:xml:ns:xmpp-sasl)</summary>
        public const string Sasl = "urn:ietf:params:xml:ns:xmpp-sasl";
        /// <summary>Session (urn:ietf:params:xml:ns:xmpp-session)</summary>
        public const string Session = "urn:ietf:params:xml:ns:xmpp-session";
        /// <summary>Bind (urn:ietf:params:xml:ns:xmpp-bind)</summary>
        public const string Bind = "urn:ietf:params:xml:ns:xmpp-bind";

        /// <summary>Component (jabber:component:accept)</summary>
        public const string Accept = "jabber:component:accept";

        /// <summary>urn:xmpp:domain-based-name:0</summary>
        public const string SaslHostname0 = "urn:xmpp:domain-based-name:0";
        /// <summary>urn:xmpp:domain-based-name:1</summary>
        public const string SaslHostname1 = "urn:xmpp:domain-based-name:1";

        /// <summary>stream feature for old jabebr style authentication (http://jabber.org/features/iq-auth)</summary>
        public const string FeatureAuth = "http://jabber.org/features/iq-auth";

        // Features
        //<register xmlns='http://jabber.org/features/iq-register'/>
        /// <summary>(http://jabber.org/features/iq-register)</summary>
        public const string FeatureIqRegister = "http://jabber.org/features/iq-register";

        /// <summary>Stream Compression (http://jabber.org/features/compress)</summary>
        public const string FeatureCompress = "http://jabber.org/features/compress";

        /// <summary>XEP-0237: Roster Versioning (urn:xmpp:features:rosterver)</summary>
        public const string FeatureRosterVersioning = "urn:xmpp:features:rosterver";

        /// <summary>XEP-0198: Stream Management (urn:xmpp:sm:3)</summary>
        public const string FeatureStreamManagement = "urn:xmpp:sm:3";

        /// <summary> Bidirectional Server-to-Server Connections</summary>
        public const string FeatureBidi = "xmlns='urn:xmpp:bidi";

        /// <summary>XEP-0131: Stanza Headers and Internet Metadata (http://jabber.org/protocol/shim)</summary>
        public const string Shim = "http://jabber.org/protocol/shim";

        /// <summary>(http://jabber.org/protocol/primary)</summary>
        public const string Primary = "http://jabber.org/protocol/primary";

        /// <summary>XEP-0172: User nickname (http://jabber.org/protocol/nick)</summary>
        public const string Nick = "http://jabber.org/protocol/nick";

        /// <summary>XEP-0085 Chat State Notifications (http://jabber.org/protocol/chatstates)</summary>
        public const string Chatstates = "http://jabber.org/protocol/chatstates";

        /// <summary>XEP-0138: Stream Compression (http://jabber.org/protocol/compress)</summary>
        public const string Compress = "http://jabber.org/protocol/compress";

        /// <summary>XEP-0020: Feature Negotiation (http://jabber.org/protocol/feature-neg)</summary>
        public const string FeatureNeg = "http://jabber.org/protocol/feature-neg";

        /// <summary>XEP-0095 (http://jabber.org/protocol/si)</summary>
        public const string SI = "http://jabber.org/protocol/si";

        /// <summary>XEP-0096 (http://jabber.org/protocol/si/profile/file-transfer)</summary>
        public const string SIProfileFileTransfer = "http://jabber.org/protocol/si/profile/file-transfer";

        /// <summary>XEP-0065: SOCKS5 bytestreams (http://jabber.org/protocol/bytestreams)</summary>
        public const string Bytestreams = "http://jabber.org/protocol/bytestreams";


        /// <summary>
        /// XEP-0083 (roster:delimiter)
        /// </summary>
        public const string RosterDelimiter = "roster:delimiter";

        /// <summary>XEP-0071: XHTML-IM (http://jivesoftware.com/xmlns/phone)</summary>
        public const string XhtmlIm = "http://jabber.org/protocol/xhtml-im";
        /// <summary>(http://www.w3.org/1999/xhtml)</summary>
        public const string Xhtml = "http://www.w3.org/1999/xhtml";


        /// <summary>XEP-0115: Entity Capabilities (http://jabber.org/protocol/caps)</summary>
        public const string Caps = "http://jabber.org/protocol/caps";

        /// <summary>GeoLoc (http://jabber.org/protocol/geoloc)</summary>
        public const string Geoloc = "http://jabber.org/protocol/geoloc";

        /// <summary>XMPP ping (urn:xmpp:ping)</summary>
        public const string Ping = "urn:xmpp:ping";

        /// <summary>Ad-Hoc Commands (http://jabber.org/protocol/commands)</summary>
        public const string AdHocCommands = "http://jabber.org/protocol/commands";

        /// <summary>XEP-0060: Publish-Subscribe (http://jabber.org/protocol/pubsub)</summary>
        public const string Pubsub = "http://jabber.org/protocol/pubsub";
        /// <summary>XEP-0060: Publish-Subscribe (http://jabber.org/protocol/pubsub#event)</summary>
        public const string PubsubEvent = "http://jabber.org/protocol/pubsub#event";
        /// <summary>XEP-0060: Publish-Subscribe (http://jabber.org/protocol/pubsub#owner)</summary>
        public const string PubsubOwner = "http://jabber.org/protocol/pubsub#owner";

        /// <summary>XEP-0124: Http-Binding  (http://jabber.org/protocol/httpbind)</summary>
        public const string HttpBind = "http://jabber.org/protocol/httpbind";

        /// <summary>(urn:xmpp:xbosh)</summary>
        public const string XmppXBosh = "urn:xmpp:xbosh";


        /// <summary>XEP-0184: Message Receipts (urn:xmpp:receipts)</summary>
        public const string MessageReceipts = "urn:xmpp:receipts";

        /// <summary>XEP-0048: Bookmark Storage (storage:bookmarks)</summary>
        public const string StorageBookmarks = "storage:bookmarks";

        /// <summary>XEP-0047: In-Band Bytestreams (http://jabber.org/protocol/ibb)</summary>
        public const string Ibb = "http://jabber.org/protocol/ibb";

        //public const string IQIBB = "http://jabber.org/protocol/iqibb";

        /// <summary>XEP-0079: Advanced Message Processing (http://jabber.org/protocol/amp)</summary>
        public const string AMP = "http://jabber.org/protocol/amp";

        /// <summary>XEP-0153: vCard-Based Avatars (vcard-temp:x:update)</summary>
        public const string VcardUpdate = "vcard-temp:x:update";       

        /// <summary>Jingle</summary>
        public const string Jingle = "urn:xmpp:jingle:1";
        // Jingle transports
        /// <summary>urn:xmpp:jingle:transports:ibb:1</summary>
        public const string JingleTransportIbb = "urn:xmpp:jingle:transports:ibb:1";
        /// <summary>urn:xmpp:jingle:transports:raw-udp:1</summary>
        public const string JingleTransportRawUdp = "urn:xmpp:jingle:transports:raw-udp:1";
        /// <summary>urn:xmpp:jingle:transports:ice-udp:1</summary>
        public const string JingleTransportIceUdp = "urn:xmpp:jingle:transports:ice-udp:1";
        /// <summary>urn:xmpp:jingle:apps:rtp:1</summary>
        public const string JingleAppsRtp = "urn:xmpp:jingle:apps:rtp:1";

        /// <summary>XEP-0202: Entity Time (urn:xmpp:time)</summary>
        public const string Time = "urn:xmpp:time";

        /// <summary>XEP-0033: Extended Stanza Addressing (http://jabber.org/protocol/address)</summary>
        public const string ExtendedStanzaAdressing = "http://jabber.org/protocol/address";

        /// <summary>XEP-0224: Attention (urn:xmpp:attention:0)</summary>
        public const string Attention = "urn:xmpp:attention:0";

        /// <summary>XEP-0059: Result Set Management (http://jabber.org/protocol/rsm)</summary>
        public const string Rsm = "http://jabber.org/protocol/rsm";

        // Windows Live Messenger stuff        
        /// <summary>http://messenger.live.com/xmpp/jidlookup</summary>
        public const string WlmJidLookup = "http://messenger.live.com/xmpp/jidlookup";

        /// <summary>google:push</summary>
        public const string GooglePush = "google:push";
        /// <summary>google:mobile:data</summary>
        public const string GoogleMobileData = "google:mobile:data";

        /// <summary>XEP-0191: Blocking Command (urn:xmpp:blocking)</summary>
        public const string Blocking = "urn:xmpp:blocking";

        /// <summary>XEP-0107: User Mood (http://jabber.org/protocol/mood)</summary>
        public const string Mood = "http://jabber.org/protocol/mood";

        /// <summary>
        /// XEP-0118: User Tune (http://jabber.org/protocol/tune)
        /// </summary>
        public const string Tune = "http://jabber.org/protocol/tune";

        // XEP-0258: Security Labels in XMPP        
        /// <summary>urn:xmpp:sec-label:0</summary>
        public const string SecurityLabel = "urn:xmpp:sec-label:0";
        /// <summary>urn:xmpp:sec-label:ess:0</summary>
        public const string SecurityLabelEss = "urn:xmpp:sec-label:ess:0";
        /// <summary>urn:xmpp:sec-label:catalog:2</summary>
        public const string SecurityLabelCatalog = "urn:xmpp:sec-label:catalog:2";

        /// <summary>urn:xmpp:archive</summary>
        public const string Archiving = "urn:xmpp:archive";

        /// <summary>XEP-0280: Message Carbons</summary>
        public const string MessageCarbons = "urn:xmpp:carbons:2";

        public const string Forward = "urn:xmpp:forward:0";

        /// <summary>XEP-0308: Last Message Correction</summary>
        public const string LastMessageCorrection = "urn:xmpp:message-correct:0";

         /* websocket framing urn:ietf:params:xml:ns:xmpp-framing */
        public const string Framing = "urn:ietf:params:xml:ns:xmpp-framing";

        /// <summary>
        /// urn:xmpp:avatar:metadata (XEP-0084: User Avatar)
        /// </summary>
        public const string AvatarMetadata = "urn:xmpp:avatar:metadata";
        /// <summary>
        /// urn:xmpp:avatar:data  (XEP-0084: User Avatar)
        /// </summary>
        public const string AvatarData = "urn:xmpp:avatar:data";
    }
}
