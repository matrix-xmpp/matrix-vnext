namespace Matrix.Network
{
    public class ChannelHandlerNames
    {
        #region << handlers >>
        public static string DisconnetHandler           => "Disconnect-Handler";
        public static string KeepAliveHandler           => "KeepAlive-Handler";
        public static string XmppStreamEventHandler     => "XmppStreamEvent-Handler";
        public static string XmppPingHandler            => "XmppPing-Handler";
        public static string StreamFooterHandler        => "StreamFooter-Handler";
        public static string XmppStanzaHandler          => "XmppStanza-Handler";
        public static string CatchAllXmppStanzaHandler  => "CatchAllXmppStanza-Handler";
        #endregion

        #region << decoderd and encoders >>
        public static string ZlibDecoder                => "Zlib-Decoder";
        public static string ZlibEncoder                => "Zlib-Encoder";
        public static string XmlStreamDecoder           => "XmlStream-Decoder";
        public static string XmppXElementEncoder        => "XmppXElement-Encoder";
        public static string UTF8StringEncoder          => "UTF8String-Encoder";
        #endregion
    }
}
