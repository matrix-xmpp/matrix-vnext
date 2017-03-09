//using System;
//using System.Text;

//using System.Net;
//using System.Net.Sockets;
//using System.Threading.Tasks;
//using Matrix.Xml;
//using System.Linq;
//using System.Xml.Linq;
//using Xunit;
//using Matrix.Network.Resolver;
//using System.Threading;

//namespace Matrix.Tests
//{
//    public class FooTests
//    {
//        public static XmppXElement GetStanza(string type, int number)
//        {
//            var xml = Resource.Get("stream1.xml");
//            var el = XmppXElement.LoadXml(xml);

//            var stanza = el
//                            .Elements("stanza")
//                            .FirstOrDefault(s => s.Attribute("type").Value == type && s.Attribute("id").Value == number.ToString());

//            var sStanza = stanza
//                    .DescendantNodes()
//                    .Where(d => d.NodeType == System.Xml.XmlNodeType.CDATA)
//                    .Cast<XCData>()
//                    .FirstOrDefault()
//                    .Value
//                    .Trim(' ', '\r', '\n');

//            return XmppXElement.LoadXml(sStanza);
//        }

//        public static XmppXElement GetStanza(XmppXElement xel, string type, int number, string request)
//        {
//            var sStanza = GetStanzaAsString(xel, type, number);

//            if (sStanza == null)
//                return null;

//            var xEl = XmppXElement.LoadXml(sStanza);

//            if (request != null)
//            {
//                if (request.StartsWith("<iq") && (xEl.Name == "{jabber:client}iq" || xEl.Name == "iq"))
//                {
//                    var requestIq = XmppXElement.LoadXml(request);
//                    xEl.SetAttribute("id", requestIq.GetAttribute("id"));
//                }
//            }
//            return xEl;
//        }

//        public static string GetStanzaAsString(XmppXElement xel, string type, int number)
//        {
//            var stanza = xel
//                            .Elements("stanza")
//                            .FirstOrDefault(s => s.Attribute("type").Value == type && s.Attribute("id").Value == number.ToString());

//            return stanza?
//                    .DescendantNodes()
//                    .Where(d => d.NodeType == System.Xml.XmlNodeType.CDATA)
//                    .Cast<XCData>()
//                    .FirstOrDefault()
//                    .Value
//                    .Trim(' ', '\r', '\n');
//        }

//        //public static CopyIdFromRequest()

//        public static XmppXElement GetTestData(string res)
//        {
//            var xml = Resource.Get(res);
//            return XmppXElement.LoadXml(xml);
//        }

//        /// <summary>
//        /// Tries to parse the given xml string and returns the value of the id attribute.
//        /// </summary>
//        /// <param name="xml"></param>
//        /// <returns></returns>
//        private static string GetId(string xml)
//        {
//            try
//            {
//                return XmppXElement.LoadXml(xml).GetAttribute("id");
//            }
//            catch
//            {
//                return null;
//            }
//        }

//        [Fact]
//        public void Test()
//        {
//            var serverTask = Task.Run(() => ServerTask());
//            Thread.Sleep(2000);
//            //var server = Task.Factory.StartNew(ServerTask);
//            //server.Start();
//            var xmppClient = new XmppClient()
//            {
//                // jabber.org
//                Username = "gnauck",
//                Password = "***REMOVED***",
//                XmppDomain = "localhost",
//                HostnameResolver = new StaticNameResolver(IPAddress.Parse("127.0.0.1"), 5222)
//            };

//            xmppClient
//                .XmppXElementStream
//                .Subscribe(
//                    el => { },
//                    ex => System.Diagnostics.Debug.WriteLine($"OnError: {ex.Message}"),
//                    () => System.Diagnostics.Debug.WriteLine($"OnCompleted")
//            );

//            xmppClient
//                .ConnectAsync()
//                .GetAwaiter()
//                .GetResult();

//            Thread.Sleep(5000);

//            xmppClient
//                .CloseAsync()
//                .GetAwaiter()
//                .GetResult();

//            Thread.Sleep(5000);

//            System.Diagnostics.Debug.WriteLine("Foo");
//        }

//        [Fact]
//        public async Task ServerTask()
//        {
//            var testData = GetTestData("stream2.xml");

//            int packetCounter = 0;
//            TcpListener server = null;
//            try
//            {
//                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

//                // TcpListener server = new TcpListener(port);
//                server = new TcpListener(localAddr, 5222);

//                // Start listening for client requests.
//                server.Start();

//                // Buffer for reading data
//                var bytes = new Byte[4096];
//                string data = null;

//                // Enter the listening loop.
//                while (true)
//                {
//                    Console.Write("Waiting for a connection... ");

//                    // Perform a blocking call to accept requests.
//                    // You could also user server.AcceptSocket() here.
//                    TcpClient client = await server.AcceptTcpClientAsync();
//                    System.Diagnostics.Debug.WriteLine("Connected!");

//                    data = null;

//                    // Get a stream object for reading and writing
//                    NetworkStream stream = client.GetStream();

//                    int recv;
//                    bool exit = false;

//                    // Loop to receive all the data sent by the client.
//                    while ((recv = stream.Read(bytes, 0, bytes.Length)) != 0)
//                    {
//                        packetCounter++;
//                        // Translate data bytes to a ASCII string.
//                        data = Encoding.UTF8.GetString(bytes, 0, recv);
//                        Console.WriteLine("Received: {0}", data);

//                        // Process the data sent by the client.
//                        byte[] msg = Encoding.UTF8.GetBytes(data);
//                        System.Diagnostics.Debug.WriteLine($"RECV: {data}");

//                        if (data.Contains("<stream:stream "))
//                        {
//                            var res = GetStanzaAsString(testData, "server", packetCounter);
//                            if (res != null)
//                            {
//                                var resBytes = Encoding.UTF8.GetBytes(res);
//                                // Send back a response.
//                                await stream.WriteAsync(resBytes, 0, resBytes.Length);
//                                System.Diagnostics.Debug.WriteLine($"SEND: {res}");
//                            }
//                            else
//                            {
//                                exit = true;
//                            }
//                        }
//                        else if (data.Contains("</stream:stream"))
//                        {
//                            var res = GetStanzaAsString(testData, "server", packetCounter);
//                            if (res != null)
//                            {
//                                var resBytes = Encoding.UTF8.GetBytes(res);
//                                // Send back a response.
//                                await stream.WriteAsync(resBytes, 0, resBytes.Length);
//                                System.Diagnostics.Debug.WriteLine($"SEND: {res}");
//                                exit = true;
//                            }
//                        }
//                        else
//                        {
//                            var res = GetStanza(testData, "server", packetCounter, data);
//                            if (res != null)
//                            {
//                                var resBytes = Encoding.UTF8.GetBytes(res.ToString(false));
//                                // Send back a response.
//                                await stream.WriteAsync(resBytes, 0, resBytes.Length);
//                                System.Diagnostics.Debug.WriteLine($"SEND: {res}");
//                            }
//                            else
//                            {
//                                exit = true;
//                            }
//                        }

//                        if (exit)
//                            client.Dispose();
//                    }

//                    // Shutdown and end connection
//                    client.Dispose();// .Close();
//                }
//            }
//            catch (SocketException e)
//            {
//                System.Diagnostics.Debug.WriteLine($"SocketException: {e}");
//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Debug.WriteLine(ex.Message);
//            }
//            finally
//            {
//                // Stop listening for new clients.
//                server.Stop();
//            }

//            Console.WriteLine("\nHit enter to continue...");
//            Console.Read();
//        }

//    }
//}
