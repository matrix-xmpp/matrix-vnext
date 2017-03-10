using DotNetty.Transport.Channels;
using Matrix.Xml;

using System.Linq;
using System.Xml.Linq;

namespace Matrix.Tests.ClientEnd2End
{
    public class TestServerHandler : SimpleChannelInboundHandler<string>
    {
        XmppXElement    testDocument;
        string          resource;
        int             packetCounter;

        public TestServerHandler(string res)
        {
            resource = res;
        }

        protected override void ChannelRead0(IChannelHandlerContext ctx, string data)
        {
            packetCounter++;                    
                        
            if (data.Contains("<stream:stream "))
            {
                var res = GetStanzaAsString(TestDocument, "server", packetCounter);
                if (res != null)                
                    ctx.WriteAndFlushAsync(res).GetAwaiter().GetResult();                                    
            }
            else if (data.Contains("</stream:stream"))
            {
                var res = GetStanzaAsString(TestDocument, "server", packetCounter);
                if (res != null)                
                    ctx.WriteAndFlushAsync(res).GetAwaiter().GetResult();                                   
            }
            else
            {
                var res = GetStanza(TestDocument, "server", packetCounter, data);
                if (res != null)                
                    ctx.WriteAndFlushAsync(res).GetAwaiter().GetResult();                
            }
        }

        private string GetStanza(XmppXElement xel, string type, int number, string request)
        {
            var sStanza = GetStanzaAsString(xel, type, number);

            if (sStanza == null)
                return null;

            var xEl = XmppXElement.LoadXml(sStanza);

            if (request != null)
            {
                if (request.StartsWith("<iq") && (xEl.Name == "{jabber:client}iq" || xEl.Name == "iq"))
                {
                    var requestIq = XmppXElement.LoadXml(request);
                    xEl.SetAttribute("id", requestIq.GetAttribute("id"));
                }
            }
            return xEl.ToString(false);
        }

        private string GetStanzaAsString(XmppXElement xel, string type, int number)
        {
            var stanza = xel
                            .Elements("stanza")
                            .FirstOrDefault(s => s.Attribute("type").Value == type && s.Attribute("id").Value == number.ToString());

            return stanza?
                    .DescendantNodes()
                    .Where(d => d.NodeType == System.Xml.XmlNodeType.CDATA)
                    .Cast<XCData>()
                    .FirstOrDefault()
                    .Value
                    .Trim(' ', '\r', '\n');
        }

        private XmppXElement TestDocument
        {
            get
            {
                if (testDocument == null)
                {
                    var xml = Resource.Get(resource);
                    testDocument = XmppXElement.LoadXml(xml);
                }               
                return testDocument;
            }            
        }
    }
}
