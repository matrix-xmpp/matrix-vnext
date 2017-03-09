using System;
using System.Collections.Generic;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Matrix.Xml;
using System.Linq;
using Xunit;

namespace Matrix.Tests
{
    public class FooTests
    {
        [Fact]
        public void Test1()
        {
            var xml = Resource.Get("stream1.xml");
            var el = XmppXElement.LoadXml(xml);
            var stanzas = el.Descendants("stanza");
            System.Diagnostics.Debug.WriteLine(stanzas.Count());
        }



        /*
         * 
         * <!-- C1 -->
<stream:stream xmlns:stream="http://etherx.jabber.org/streams" version="1.0" xmlns="jabber:client" to="localhost" xml:lang="en" xmlns:xml="http://www.w3.org/XML/1998/namespace">


<!-- S1 -->
<stream:stream xmlns:stream='http://etherx.jabber.org/streams' version='1.0' from='localhost' id='71a4bc43-3e09-44f3-9430-81ee7bf6b392' xml:lang='en' xmlns='jabber:client'>
<stream:features>
    <mechanisms xmlns="urn:ietf:params:xml:ns:xmpp-sasl">
        <mechanism>PLAIN</mechanism>
    </mechanisms>
</stream:features>

<!-- C2 -->
<auth xmlns="urn:ietf:params:xml:ns:xmpp-sasl" mechanism="PLAIN">biwsbj1hbGV4LHI9enpCVUkyekJ2S2RxMndoMWZZNE5kU0JEZmovK0llRFkwaUJ4bk05UGJnaz0=</auth>

<!-- S2 -->
<success xmlns="urn:ietf:params:xml:ns:xmpp-sasl"/>


<!-- C3 -->
<stream:stream xmlns:stream="http://etherx.jabber.org/streams" version="1.0" xmlns="jabber:client" to="localhost" xml:lang="en" xmlns:xml="http://www.w3.org/XML/1998/namespace">

<!-- S3 -->
<stream:stream xmlns:stream='http://etherx.jabber.org/streams' version='1.0' from='localhost' id='cf76e2b8-ac4e-4305-98bc-8bb116d94f25' xmlns='jabber:client'>
<stream:features>
    <bind xmlns="urn:ietf:params:xml:ns:xmpp-bind">
        <required/>
    </bind>
    <session xmlns="urn:ietf:params:xml:ns:xmpp-session">
        <optional/>
    </session>
    <c xmlns="http://jabber.org/protocol/caps" node="http://prosody.im" ver="oxBiiFkjfIU4zAle9Gk8+Qn8Fhk=" hash="sha-1"/>
</stream:features>

<!-- C4 -->
<iq type="set" id="bind_1">
    <bind xmlns="urn:ietf:params:xml:ns:xmpp-bind">
        <resource>Psi+</resource>
    </bind>
</iq>

<!-- S5 -->
<iq xmlns="jabber:client" type="result" id="bind_1">
    <bind xmlns="urn:ietf:params:xml:ns:xmpp-bind">
        <jid>alex@localhost/MatriX</jid>
    </bind>
</iq>

<!-- C6 -->
<iq type="set" id="ab49a">
    <session xmlns="urn:ietf:params:xml:ns:xmpp-session"/>
</iq>

<!-- S6 -->
<iq type="result" id="ab49a" to="alex@localhost/Psi+"/>


         */
        /// <summary>
        /// Tries to parse the given xml string and returns the value of the id attribute.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private static string GetId(string xml)
        {
            try
            {
                return XmppXElement.LoadXml(xml).GetAttribute("id");
            }
            catch
            {
                return null;
            }            
        }

        public async Task Test()
        {
            int packetCounter = 0;
            TcpListener server = null;
            try
            {
                
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, 5222);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[4096];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = await server.AcceptTcpClientAsync();// .AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int recv;

                    // Loop to receive all the data sent by the client.
                    while ((recv = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        packetCounter++;
                        // Translate data bytes to a ASCII string.
                        data = Encoding.UTF8.GetString(bytes, 0, recv);
                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client.
                        data = data.ToUpper();

                        byte[] msg = Encoding.UTF8.GetBytes(data);

                        // Send back a response.
                        await stream.WriteAsync(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                    // Shutdown and end connection
                    client.Dispose();// .Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    
    }
}
