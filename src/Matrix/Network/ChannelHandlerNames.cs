namespace Matrix.Network
{
    public class ChannelHandlerNames
    {
        public static string DisconnetHandler           => "DisconnectHandler";
        public static string ZlibDecoder                => "ZlibDecoder";
        public static string ZlibEncoder                => "ZlibEncoder";
        public static string KeepAliveHandler           => "KeepAliveHandler";
        public static string XmlStreamDecoder           => "XmlStreamDecoder";
        public static string XmppXElementEncoder        => "XmppXElementEncoder";
        public static string UTF8StringEncoder          => "UTF8StringEncoder";              
        public static string XmppPingHandler            => "XmppPingHandler";
        public static string XmppStreamEventHandler     => "XmppStreamEventHandler";
        public static string StreamFooterHandler        => "StreamFooterHandler";
        public static string XmppStanzaHandler          => "XmppStanzaHandler";
        public static string CatchAllXmppStanzaHandler  => "CatchAllXmppStanzaHandler";
    }
}
