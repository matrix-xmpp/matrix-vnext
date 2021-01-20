namespace Matrix.Transport.WebSocket.Tests.Server
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.WebSockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Matrix.Xml;
    using Microsoft.AspNetCore.Builder;

    public class Startup
    {
        private XmppXElement testDocument;

        public static void ConfigureServices(/*IServiceCollection services*/)
        {
        }

        public void Configure(IApplicationBuilder app/*, IWebHostEnvironment env*/)
        {
            app.UseWebSockets();
            app.Use(async (context, next) =>
            {

                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    InitTestDocument(context.Request.Path.Value);
                    await HandleRequest(webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                }

            });
        }

        private async Task HandleRequest(WebSocket webSocket)
        {
            int packetCounter = 0;
            while (true)
            {
                var request = await ReadRequest(webSocket);
                packetCounter++;
                var result = request.result;
                if (result.CloseStatus.HasValue)
                {
                    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                    return;
                }

                await HandleTextRequest(webSocket, request.message, packetCounter);
            }
        }

        private async Task HandleTextRequest(WebSocket webSocket, string message,
            int packetCounter)
        {
            var chunks = GetReplyMessage(packetCounter);
            if (chunks.Count > 0)
            {
                foreach (var reply in chunks)
                {
                    var replyDoc = XmppXElement.LoadXml(reply);
                    // for IQs we need add the id of the incoming request
                    if (message.StartsWith("<iq") && (replyDoc.Name == "{jabber:client}iq"))
                    {
                        var requestIq = XmppXElement.LoadXml(message);
                        replyDoc.SetAttribute("id", requestIq.GetAttribute("id"));
                    }

                    await SendMessage(webSocket, replyDoc.ToString());
                }
            }
            else
            {
                try
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty,
                        CancellationToken.None);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
                
            }
        }

        private static async Task SendMessage(WebSocket webSocket, string msg)
        {
            var bytes = Encoding.UTF8.GetBytes(msg);
            await webSocket.SendAsync(
                new ArraySegment<byte>(bytes, 0, bytes.Length),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }

        private static async Task<(WebSocketReceiveResult result, string message)> ReadRequest(WebSocket webSocket)
        {
            var buffer = new ArraySegment<byte>(new byte[8192]);

            await using var ms = new MemoryStream();
            WebSocketReceiveResult result;
            do
            {
                result = await webSocket.ReceiveAsync(buffer, CancellationToken.None);
                if (result.CloseStatus.HasValue)
                    return (result, null);

                if (buffer.Array != null)
                    ms.Write(buffer.Array, buffer.Offset, result.Count);
            } while (!result.EndOfMessage);

            ms.Seek(0, SeekOrigin.Begin);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                var data = Encoding.UTF8.GetString(ms.ToArray());
                return (result, data);
            }
            else
            {
                throw new NotSupportedException($"Received message is not of type text.");
            }
        }

        private void InitTestDocument(string path)
        {
            var xml = Resource.Get($"Streams.WebSocket.{path[1..]}.xml");
            testDocument = XmppXElement.LoadXml(xml);
        }

        private List<string> GetReplyMessage(int number)
        {
            var stanza = testDocument
                .Elements("stanza")
                .FirstOrDefault(s => s.Attribute("type").Value == "server" && s.Attribute("id").Value == number.ToString());


            List<string> chunks = new List<string>();
            stanza?
                .DescendantNodes()
                .Where(d => d.NodeType == System.Xml.XmlNodeType.CDATA)
                .Cast<XCData>()
                .ToList()
                .ForEach(
                    v => chunks.Add(v.Value.Trim(' ', '\r', '\n'))
                );

            return chunks;
        }
    }
}