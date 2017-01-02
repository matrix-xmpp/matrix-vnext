using System.Linq;
using System.Net;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Jingle;
using Matrix.Xmpp.Jingle.Apps.Rtp;
using Matrix.Xmpp.Jingle.Candidates;
using Matrix.Xmpp.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Test.Xmpp.Jingle
{
    [TestClass]
    public class JingleTest
    {
        const string XML1 = @"<jingle xmlns='urn:xmpp:jingle:1' action='transport-info' sid='cTnaKddYxmHK76S3'>
                <content creator='initiator' name='A'>
                    <transport xmlns='urn:xmpp:jingle:transports:ice-udp:1' pwd='04Bsdmhg2N7J46VsdPPn33' ufrag='mWEp'>
                        <candidate generation='0' network='0' port='8010' protocol='udp' component='1' foundation='0' ip='FE80::DDA6:3488:E417:D4D7' priority='2130706431' type='host' id='dqMSbhh4Yc'/>
                    </transport>
                </content>
            </jingle>";

        private const string XML2 = @"<iq xmlns='jabber:client' from='from@address.com/fromSystem' id='xxxxxxxx' to='to@address.com/toSystem' type='set'>
    <jingle xmlns='urn:xmpp:jingle:1' action='session-initiate' initiator='from@address.com/fromSystem' sid='sidxxxxx'>
        <content creator='initiator' name='voice'>
            <description xmlns='urn:xmpp:jingle:apps:rtp:1' media='audio'>
                <payload-type id='96' name='speex' clockrate='16000' />
                <payload-type id='97' name='speex' clockrate='8000' />
            </description>
            <transport xmlns='urn:xmpp:jingle:transports:raw-udp:1'>
                <candidate generation='0' id='xxxxxxxx' ip='10.1.1.104' port='13540' />
            </transport>
        </content>
    </jingle>
</iq>";
        //<candidate candidate='1' generation='0' id='xxxxxxxx' ip='10.1.1.104' port='13540' />
        [TestMethod]
        public void CreateJingleIq()
        {
            var iq = new JingleIq
                {
                    From = "from@address.com/fromSystem",
                    To = "to@address.com/toSystem",
                    Type = Matrix.Xmpp.IqType.Set,
                    Id = "xxxxxxxx",
                    Jingle =
                        {
                            Action = Matrix.Xmpp.Jingle.Action.SessionInitiate,
                            Initiator = "from@address.com/fromSystem",
                            Sid = "sidxxxxx"
                        }
                };
            //iq.Jingle.GenerateSid();  //for random sid
            var content = new Content
                {
                    Creator = Creator.Initiator,
                    Name = "voice"
                };

            var description = new Description
            {
                Media = Media.Audio
            };
            description.AddPayloadType(new PayloadType{ Id=96, Name = "speex", Clockrate = 16000 });
            description.AddPayloadType(new PayloadType { Id = 97, Name = "speex", Clockrate = 8000 });

            content.Description = description;

            // Transport
            var transport = new Matrix.Xmpp.Jingle.Transports.TransportRawUdp();
            //transport.GeneratePwd(); for random Pwd
            transport.AddCandidate(
                new CandidateRawUdp
                    {
                        // there is GenerateId method to create a random id
                        Id = "xxxxxxxx",
                        Generation = 0,
                        IPAddress = IPAddress.Parse("10.1.1.104"),
                        Port = 13540
                    });
            
            content.TransportRawUdp = transport;
            iq.Jingle.Content = content;
            
            iq.ShouldBe(XML2);
        }

        [TestMethod]
        public void Test1()
        {
            var xmpp1 = XmppXElement.LoadXml(XML1);
            Assert.AreEqual(true, xmpp1 is Matrix.Xmpp.Jingle.Jingle);

            var jingle = xmpp1 as Matrix.Xmpp.Jingle.Jingle;
            if (jingle != null)
            {
                Assert.AreEqual(jingle.Action == Matrix.Xmpp.Jingle.Action.TransportInfo, true);
                Assert.AreEqual(jingle.Sid, "cTnaKddYxmHK76S3");

                var content = jingle.Content;
                if (content != null)
                {
                    Assert.AreEqual(content.Creator == Creator.Initiator, true);
                    Assert.AreEqual(content.Name, "A");

                    var transp = content.TransportIceUdp;
                    if (transp != null)
                    {
                        Assert.AreEqual(transp.Pwd, "04Bsdmhg2N7J46VsdPPn33");
                        Assert.AreEqual(transp.Ufrag, "mWEp");

                        var cands = transp.GetCandidates();
                        if (cands != null)
                        {
                            Assert.AreEqual(cands.Count(), 1);

                            var cand = cands.First();
                            Assert.AreEqual(cand.Generation, 0);
                            Assert.AreEqual(cand.Network, 0);
                            Assert.AreEqual(cand.Port, 8010);
                            Assert.AreEqual(cand.Component, 1);
                            Assert.AreEqual(cand.Foundation, 0);
                            Assert.AreEqual(cand.Protocol == Protocol.Udp, true);
                            Assert.AreEqual(cand.Priority, 2130706431);
                            Assert.AreEqual(cand.Type == CandidateType.Host, true);
                            Assert.AreEqual(cand.Id, "dqMSbhh4Yc");
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void DescriptionTest()
        {
            const string XML = @"<description xmlns='urn:xmpp:jingle:apps:rtp:1' media='audio'>
                <payload-type id='110' name='SPEEX' clockrate='16000'/>
                <payload-type id='97' name='SPEEX' clockrate='8000'/>
                </description>";

            var xmpp1 = XmppXElement.LoadXml(XML);
            Assert.AreEqual(true, xmpp1 is Description);

            var desc = xmpp1 as Description;
            Assert.AreEqual(desc.Media == Media.Audio, true);
            var payloads = desc.GetPayloadTypes();
            Assert.AreEqual(payloads.Count(), 2);

            var p1 = payloads.ToArray()[0];
            var p2 = payloads.ToArray()[1];

            Assert.AreEqual(p1.Id, 110);
            Assert.AreEqual(p2.Id, 97);

            Assert.AreEqual(p1.Name, "SPEEX");
            Assert.AreEqual(p2.Name, "SPEEX");

            Assert.AreEqual(p1.Clockrate, 16000);
            Assert.AreEqual(p2.Clockrate, 8000);
        }

        [TestMethod]
        public void ReasonTest()
        {
            string XML = "<reason xmlns='urn:xmpp:jingle:1'><busy/></reason>";
            var xmpp1 = XmppXElement.LoadXml(XML);
            
            Assert.AreEqual(true, xmpp1 is Reason);

            var reason = xmpp1 as Reason;
            Assert.AreEqual(reason.Condition == Condition.Busy, true);
        }


        [TestMethod]
        public void ReasonTest2()
        {
            string XML = "<reason xmlns='urn:xmpp:jingle:1'><alternative-session><sid>b84tkkwlmb48kgfb</sid></alternative-session></reason>";
            var xmpp1 = XmppXElement.LoadXml(XML);

            Assert.AreEqual(true, xmpp1 is Reason);

            var reason = xmpp1 as Reason;
            Assert.AreEqual(reason.Condition == Condition.AlternativeSession, true);
            var altSession = reason.AlternativeSession;
            Assert.AreEqual(altSession.Sid, "b84tkkwlmb48kgfb");
        }

        [TestMethod]
        public void CreateReasonWithAlternativeSessionTest()
        {
            //string a = Matrix.Util.Crypt.GenerateRandomB64(2);

            //string XML = "<reason xmlns='urn:xmpp:jingle:1'><alternative-session><sid>b84tkkwlmb48kgfb</sid></alternative-session></reason>";
            //var reason = new Reason
            //                 {
            //                     Condition = Condition.AlternativeSession,
            //                     AlternativeSession = {Sid = "b84tkkwlmb48kgfb"},
            //                 };


            //XmlAssertion.AssertXmlEquals(Util.GetXmlDiff(XML, reason));
        }

        [TestMethod]
        public void CreatePayloadTypeTest()
        {
            const string XML = @"<payload-type xmlns='urn:xmpp:jingle:apps:rtp:1' id='110' name='SPEEX' clockrate='16000' ptime='1000' maxptime='2000'/>";
            var payload = new PayloadType
                           {
                               Id = 110,
                               Name = "SPEEX",
                               Clockrate = 16000,
                               PTime = 1000,
                               MaxPTime = 2000
                           };
            payload.ShouldBe(XML);
        }

        [TestMethod]
        public void CreateDescritpionTest()
        {
            const string XML = @"<description xmlns='urn:xmpp:jingle:apps:rtp:1' media='audio'/>";

            var desc = new Description
            {
                Media = Media.Audio
            };
            desc.ShouldBe(XML);
        }

        [TestMethod]
        public void CreateContentTest()
        {
            const string XML1 = "<content xmlns='urn:xmpp:jingle:1' creator='initiator' name='A'/>";
            const string XML2 = "<content xmlns='urn:xmpp:jingle:1' creator='responder' name='A'/>";
            var ct = new Content
                         {
                             Creator = Creator.Initiator,
                             Name = "A"
                         };

            var ct2 = new Content
            {
                Creator = Creator.Responder,
                Name = "A"
            };
            ct.ShouldBe(XML1);
            ct2.ShouldBe(XML2);
        }

        [TestMethod]
        public void CreateCandidateTest()
        {
            const string XML1 = "<candidate xmlns='urn:xmpp:jingle:transports:ice-udp:1' generation='0' network='0' port='8011' protocol='udp' component='2' foundation='0' ip='127.0.0.1' priority='2130704126' type='host' id='LstSUALnbI'/>";
            
            var cand = new CandidateIceUdp()
            {
                Generation = 0,
                Network = 0,
                Port = 8011,
                Protocol = Protocol.Udp,
                Component = 2,
                Foundation = 0,
                IPAddress = System.Net.IPAddress.Parse("127.0.0.1"),
                Priority = 2130704126,
                Type = CandidateType.Host,
                Id = "LstSUALnbI"
            };
            cand.ShouldBe(XML1);
        }
    }
}
