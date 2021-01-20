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
            Assert.True(feats.SupportsStreamManagement);

            var feats2 = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.stream_features2.xml")).Cast<StreamFeatures>();
            Assert.False(feats2.SupportsStreamManagement);
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
            Assert.True(failed.Condition == Matrix.Xmpp.Base.ErrorCondition.UnexpectedRequest);
        }

        [Fact]
        public void FailedStanzaShouldIncludeItemNotFound()
        {
            var failed = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.failed3.xml")).Cast<Failed>();
            Assert.True(failed.Condition == Matrix.Xmpp.Base.ErrorCondition.ItemNotFound);
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
            Assert.Equal("abcd", enabled.Id);
            Assert.True(enabled.Resume);
         
            var enabled2 = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.enabled2.xml")).Cast<Enabled>();
            Assert.True(enabled2.Resume);
            
            var enabled3 = XmppXElement.LoadXml(Resource.Get("Xmpp.StreamManagement.enabled3.xml")).Cast<Enabled>();
            Assert.False(enabled3.Resume);
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
            Assert.Equal(0, a.LastHandledStanza);
        }

        [Fact]
        public void TestAnswerWithMaximumValue()
        {
            var a = XmppXElement.LoadXml("<a xmlns='urn:xmpp:sm:3' h='4294967295'/>").Cast<Answer>();
            Assert.Equal(4294967295, a.LastHandledStanza);
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
            Assert.Equal(1, r.LastHandledStanza);
            Assert.Equal("some-long-sm-id", r.PreviousId);
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
            Assert.Equal(3, r.LastHandledStanza);
            Assert.Equal("some-long-sm-id", r.PreviousId);
        }

        [Fact]
        public void TestBuildResumed()
        {
            new Resumed { LastHandledStanza = 3, PreviousId = "some-long-sm-id" }.ShouldBe(Resource.Get("Xmpp.StreamManagement.resumed1.xml"));
        }
    }
}
