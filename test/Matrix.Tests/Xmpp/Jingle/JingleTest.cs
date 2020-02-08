/*
 * Copyright (c) 2003-2020 by AG-Software <info@ag-software.de>
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

            Assert.Equal(jingle.Action == Action.TransportInfo, true);
            Assert.Equal(jingle.Sid, "cTnaKddYxmHK76S3");

            var content = jingle.Content;
            if (content != null)
            {
                Assert.Equal(content.Creator == Creator.Initiator, true);
                Assert.Equal(content.Name, "A");

                var transp = content.TransportIceUdp;

                Assert.Equal(transp.Pwd, "04Bsdmhg2N7J46VsdPPn33");
                Assert.Equal(transp.Ufrag, "mWEp");

                var cands = transp.GetCandidates();
                if (cands != null)
                {
                    Assert.Equal(cands.Count(), 1);

                    var cand = cands.First();
                    Assert.Equal(cand.Generation, 0);
                    Assert.Equal(cand.Network, 0);
                    Assert.Equal(cand.Port, 8010);
                    Assert.Equal(cand.Component, 1);
                    Assert.Equal(cand.Foundation, 0);
                    Assert.Equal(cand.Protocol == Protocol.Udp, true);
                    Assert.Equal(cand.Priority, 2130706431);
                    Assert.Equal(cand.Type == CandidateType.Host, true);
                    Assert.Equal(cand.Id, "dqMSbhh4Yc");
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
            Assert.Equal(desc.Media == Media.Audio, true);
            var payloads = desc.GetPayloadTypes();
            Assert.Equal(payloads.Count(), 2);

            var p1 = payloads.ToArray()[0];
            var p2 = payloads.ToArray()[1];

            Assert.Equal(p1.Id, 110);
            Assert.Equal(p2.Id, 97);

            Assert.Equal(p1.Name, "SPEEX");
            Assert.Equal(p2.Name, "SPEEX");

            Assert.Equal(p1.Clockrate, 16000);
            Assert.Equal(p2.Clockrate, 8000);
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
            Assert.Equal(reason.Condition == Condition.Busy, true);

            reason = XmppXElement.LoadXml(Resource.Get("Xmpp.Jingle.reason2.xml")).Cast<Reason>();
            Assert.Equal(reason.Condition == Condition.AlternativeSession, true);
            var altSession = reason.AlternativeSession;
            Assert.Equal(altSession.Sid, "b84tkkwlmb48kgfb");
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
