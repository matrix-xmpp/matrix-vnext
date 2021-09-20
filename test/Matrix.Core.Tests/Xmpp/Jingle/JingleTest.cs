using System.Linq;
using System.Net;
using Matrix.Xml;
using Matrix.Xmpp.Client;
using Matrix.Xmpp.Jingle;
using Matrix.Xmpp.Jingle.Apps.Rtp;
using Matrix.Xmpp.Jingle.Candidates;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.Jingle
{
    public class JingleTest
    {
        [Fact]
        public void TestCreateJingleIq()
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
            transport.AddCandidate(
                new CandidateRawUdp
                    {
                        Id = "xxxxxxxx",
                        Generation = 0,
                        IPAddress = IPAddress.Parse("10.1.1.104"),
                        Port = 13540
                    });
            
            content.TransportRawUdp = transport;
            iq.Jingle.Content = content;
            
            iq.ShouldBe(Resource.Get("Xmpp.Jingle.jingle_iq1.xml"));
        }

        [Fact]
        public void ElementShouldBeOfTypeJingle()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Jingle.jingle1.xml")).ShouldBeOfType<Matrix.Xmpp.Jingle.Jingle>();
        }

        [Fact]
        public void TestJingleProperties()
        {
            var jingle = XmppXElement.LoadXml(Resource.Get("Xmpp.Jingle.jingle1.xml")).Cast<Matrix.Xmpp.Jingle.Jingle>();

            Assert.True(jingle.Action == Action.TransportInfo);
            Assert.Equal("cTnaKddYxmHK76S3", jingle.Sid);

            var content = jingle.Content;
            if (content != null)
            {
                Assert.True(content.Creator == Creator.Initiator);
                Assert.Equal("A", content.Name);

                var transp = content.TransportIceUdp;

                Assert.Equal("04Bsdmhg2N7J46VsdPPn33", transp.Pwd);
                Assert.Equal("mWEp", transp.Ufrag);

                var cands = transp.GetCandidates();
                if (cands != null)
                {
                    Assert.Single(cands);

                    var cand = cands.First();
                    Assert.Equal(0, cand.Generation);
                    Assert.Equal(0, cand.Network);
                    Assert.Equal(8010, cand.Port);
                    Assert.Equal(1, cand.Component);
                    Assert.Equal(0, cand.Foundation);
                    Assert.True(cand.Protocol == Protocol.Udp);
                    Assert.Equal(2130706431, cand.Priority);
                    Assert.True(cand.Type == CandidateType.Host);
                    Assert.Equal("dqMSbhh4Yc", cand.Id);
                }
            }
        }

        [Fact]
        public void ElementShouldBeOfTypeDescription()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Jingle.description1.xml")).ShouldBeOfType<Description>();
        }

        [Fact]
        public void TestDescription()
        {
            var desc = XmppXElement.LoadXml(Resource.Get("Xmpp.Jingle.description1.xml")).Cast<Description>();
            Assert.True(desc.Media == Media.Audio);
            var payloads = desc.GetPayloadTypes();
            Assert.Equal(2, payloads.Count());

            var p1 = payloads.ToArray()[0];
            var p2 = payloads.ToArray()[1];

            Assert.Equal(110, p1.Id);
            Assert.Equal(97, p2.Id);

            Assert.Equal("SPEEX", p1.Name);
            Assert.Equal("SPEEX", p2.Name);

            Assert.Equal(16000, p1.Clockrate);
            Assert.Equal(8000, p2.Clockrate);
        }

        [Fact]
        public void ElementShouldBeOfTypeReason()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.Jingle.reason1.xml")).ShouldBeOfType<Reason>();
        }

        [Fact]
        public void ReasonTest()
        {
            var reason = XmppXElement.LoadXml(Resource.Get("Xmpp.Jingle.reason1.xml")).Cast<Reason>();
            Assert.True(reason.Condition == Condition.Busy);

            reason = XmppXElement.LoadXml(Resource.Get("Xmpp.Jingle.reason2.xml")).Cast<Reason>();
            Assert.True(reason.Condition == Condition.AlternativeSession);
            var altSession = reason.AlternativeSession;
            Assert.Equal("b84tkkwlmb48kgfb", altSession.Sid);
        }
     
        [Fact]
        public void TestCreatePayloadType()
        {
            var payload = new PayloadType
                           {
                               Id = 110,
                               Name = "SPEEX",
                               Clockrate = 16000,
                               PTime = 1000,
                               MaxPTime = 2000
                           };

            payload.ShouldBe(Resource.Get("Xmpp.Jingle.payload-type1.xml"));
        }

        [Fact]
        public void TestCreateDescritpion()
        {
            var desc = new Description
            {
                Media = Media.Audio
            };

            desc.ShouldBe(Resource.Get("Xmpp.Jingle.description2.xml"));
        }

        [Fact]
        public void TestCreateContent()
        {
            var ct = new Content
            {
                Creator = Creator.Initiator,
                Name = "A"
            };
            ct.ShouldBe(Resource.Get("Xmpp.Jingle.content1.xml"));

            var ct2 = new Content
            {
                Creator = Creator.Responder,
                Name = "A"
            };
            ct2.ShouldBe(Resource.Get("Xmpp.Jingle.content2.xml"));
        }

        [Fact]
        public void CreateCandidateTest()
        {
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
            cand.ShouldBe(Resource.Get("Xmpp.Jingle.candidate1.xml"));
        }
    }
}
