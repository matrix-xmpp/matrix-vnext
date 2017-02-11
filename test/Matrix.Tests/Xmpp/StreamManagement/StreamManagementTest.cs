using Matrix.Xml;
using Matrix.Xmpp.Stream;
using Matrix.Xmpp.StreamManagement;
using Matrix.Xmpp.StreamManagement.Ack;
using Shouldly;
using Xunit;

namespace Matrix.Tests.Xmpp.StreamManagement
{
    public class StreamManagementTest
    {
        [Fact]
        public void ShouldBeOfTypeStreamFetaures()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.stream_features1.xml")).ShouldBeOfType<StreamFeatures>();
            XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.stream_features2.xml")).ShouldBeOfType<StreamFeatures>();
        }

        [Fact]
        public void TestStreamFeatures()
        {
            var feats = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.stream_features1.xml")).Cast<StreamFeatures>();
            Assert.Equal(feats.SupportsStreamManagement, true);

            var feats2 = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.stream_features2.xml")).Cast<StreamFeatures>();
            Assert.Equal(feats2.SupportsStreamManagement, false);
        }

        [Fact]
        public void TestBuildSm()
        {
            new Matrix.Xmpp.Stream.Features.StreamManagement { Optional = true }.ShouldBe(Resource.Get("Xmpp.StreamManagement.sm1.xml"));
        }

        [Fact]
        public void ShouldBeOfTypeStreamFailed()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.failed1.xml")).ShouldBeOfType<Failed>();
        }

        [Fact]
        public void TestFailed()
        {
            var failed = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.failed1.xml")).Cast<Failed>();
            Assert.Equal(failed.Condition == Matrix.Xmpp.Base.ErrorCondition.UnexpectedRequest, true);
        }
        
        [Fact]
        public void TestBuildFailed()
        {
            new Failed().ShouldBe(Resource.Get("Xmpp.StreamManagement.failed2.xml"));
        }

        [Fact]
        public void ShouldBeOfTypeEnable()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.enable1.xml")).ShouldBeOfType<Enable>();
        }
        
        [Fact]
        public void ShouldBeOfTypeEnabled()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.enabled1.xml")).ShouldBeOfType<Enabled>();
        }

        [Fact]
        public void TestBuildEnable()
        {
            new Enable().ShouldBe(Resource.Get("Xmpp.StreamManagement.enable1.xml"));
        }

        [Fact]
        public void TestEnabled()
        {
            var enabled = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.enabled1.xml")).Cast<Enabled>();
            Assert.Equal(enabled.Id, "abcd");
            Assert.Equal(enabled.Resume, true);
         
            var enabled2 = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.enabled2.xml")).Cast<Enabled>();
            Assert.Equal(enabled2.Resume, true);
            
            var enabled3 = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.enabled3.xml")).Cast<Enabled>();
            Assert.Equal(enabled3.Resume, false);
        }
        
        [Fact]
        public void TestBuildEnabled()
        {
            new Enabled { Resume = true, Id = "abcd" }.ShouldBe(Resource.Get("Xmpp.StreamManagement.enabled1.xml"));
        }

        [Fact]
        public void ShouldBeOfTypeRequest()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.r1.xml")).ShouldBeOfType<Request>();
        }

        [Fact]
        public void ShouldBeOfTypeAnswer()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.a1.xml")).ShouldBeOfType<Answer>();
        }

        [Fact]
        public void TestBuildRequest()
        {
            new Request().ShouldBe(Resource.Get("Xmpp.StreamManagement.r1.xml"));
        }

        [Fact]
        public void TestAnswer()
        {
            var a = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.a1.xml")).Cast<Answer>();
            Assert.Equal(a.LastHandledStanza, 0);
        }

        [Fact]
        public void TestBuildAnswer()
        {
            new Answer { LastHandledStanza = 0 }.ShouldBe(Resource.Get("Xmpp.StreamManagement.a1.xml"));
        }

        [Fact]
        public void ShouldBeOfTypeResume()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.resume1.xml")).ShouldBeOfType<Resume>();
        }

        [Fact]
        public void ShouldBeOfTypeResumed()
        {
            XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.resumed1.xml")).ShouldBeOfType<Resumed>();
        }

        [Fact]
        public void TestResume()
        {
            var r = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.resume1.xml")).Cast<Resume>();
            Assert.Equal(r.LastHandledStanza, 1);
            Assert.Equal(r.PreviousId, "some-long-sm-id");
        }

        [Fact]
        public void TestBuildResume()
        {
            new Resume { LastHandledStanza = 1, PreviousId = "some-long-sm-id" }.ShouldBe(Resource.Get("Xmpp.StreamManagement.resume1.xml"));
        }

        [Fact]
        public void TestResumed()
        {
            var r = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.resumed1.xml")).ShouldBeOfType<Resumed>();
            Assert.Equal(r.LastHandledStanza, 3);
            Assert.Equal(r.PreviousId, "some-long-sm-id");
        }

        [Fact]
        public void TestBuildResumed()
        {
            new Resumed { LastHandledStanza = 3, PreviousId = "some-long-sm-id" }.ShouldBe(Resource.Get("Xmpp.StreamManagement.resumed1.xml"));
        }
    }
}
