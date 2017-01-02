using Matrix.Xml;
using Matrix.Xmpp.Stream;
using Matrix.Xmpp.StreamManagement;
using Matrix.Xmpp.StreamManagement.Ack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Matrix.Xmpp.Tests.StreamManagement
{
    [TestClass]
    public class StreamManagementTest
    {
        const string FEATURES = @"<stream:features xmlns:stream='http://etherx.jabber.org/streams'>
                 <bind xmlns='urn:ietf:params:xml:ns:xmpp-bind'>
                   <required/>
                 </bind>
                 <sm xmlns='urn:xmpp:sm:3'>
                   <optional/>
                 </sm>
               </stream:features>";

        const string FEATURES2 = @"<stream:features xmlns:stream='http://etherx.jabber.org/streams'>
                 <bind xmlns='urn:ietf:params:xml:ns:xmpp-bind'>
                   <required/>
                 </bind>                 
               </stream:features>";


        const string FEATURES3 = @"<sm xmlns='urn:xmpp:sm:3'>
                   <optional/>
                 </sm>";

        const string FAILED     = @"<failed xmlns='urn:xmpp:sm:3'>
            <unexpected-request xmlns='urn:ietf:params:xml:ns:xmpp-stanzas'/>
            </failed>";

        const string FAILED2 = @"<failed xmlns='urn:xmpp:sm:3'/>";

        const string ENABLE     = @"<enable xmlns='urn:xmpp:sm:3'/>";
        const string ENABLED    = @"<enabled xmlns='urn:xmpp:sm:3' id='abcd' resume='true'/>";
        const string ENABLED2   = @"<enabled xmlns='urn:xmpp:sm:3' id='abcd' resume='1'/>";
        const string ENABLED3   = @"<enabled xmlns='urn:xmpp:sm:3' id='abcd'/>";

        const string REQUEST = @"<r xmlns='urn:xmpp:sm:3'/>";
        const string ANSWER = @"<a xmlns='urn:xmpp:sm:3' h='0'/>";

        private const string RESUME = @"<resume xmlns='urn:xmpp:sm:3' h='1' previd='some-long-sm-id'/>";
        private const string RESUMED = @"<resumed xmlns='urn:xmpp:sm:3' h='3' previd='some-long-sm-id'/>";

        [TestMethod]
        public void FeaturesTest()
        {
            var el  = XmppXElement.LoadXml(FEATURES);
            var el2 = XmppXElement.LoadXml(FEATURES2);

            Assert.AreEqual(el  is StreamFeatures, true);
            Assert.AreEqual(el2 is StreamFeatures, true);

            var feats = el as StreamFeatures;
            if (feats != null)
            {
                Assert.AreEqual(feats.SupportsStreamManagement, true);
            }

            var feats2 = el2 as StreamFeatures;
            if (feats2 != null)
            {
                Assert.AreEqual(feats2.SupportsStreamManagement, false);
            }

            new Matrix.Xmpp.Stream.Features.StreamManagement { Optional = true }.ShouldBe(FEATURES3);
        }

        [TestMethod]
        public void FailedTest()
        {
            var el = XmppXElement.LoadXml(FAILED);

            Assert.AreEqual(el is Failed, true);

            var failed = el as Failed;
            if (failed != null)
            {
                Assert.AreEqual(failed.Condition == Matrix.Xmpp.Base.ErrorCondition.UnexpectedRequest, true);
            }
            
            new Failed().ShouldBe(FAILED2);
        }

        [TestMethod]
        public void EnableTest()
        {
            var el = XmppXElement.LoadXml(ENABLE);

            Assert.AreEqual(el is Enable, true);

            new Enable().ShouldBe(ENABLE);
        }

        [TestMethod]
        public void EnabledTest()
        {
            var el  = XmppXElement.LoadXml(ENABLED);
            var el2 = XmppXElement.LoadXml(ENABLED2);
            var el3 = XmppXElement.LoadXml(ENABLED3);

            Assert.AreEqual(el  is Enabled, true);
            Assert.AreEqual(el2 is Enabled, true);
            Assert.AreEqual(el3 is Enabled, true);

            var enabled = el as Enabled;
            if (enabled != null)
            {
                Assert.AreEqual(enabled.Id, "abcd");
                Assert.AreEqual(enabled.Resume, true);
            }

            var enabled2 = el2 as Enabled;
            if (enabled2 != null)
            {
                Assert.AreEqual(enabled2.Resume, true);
            }

            var enabled3 = el3 as Enabled;
            if (enabled3 != null)
            {
                Assert.AreEqual(enabled3.Resume, false);
            }

            new Enabled { Resume = true, Id = "abcd" }.ShouldBe(ENABLED);
        }

        [TestMethod]
        public void RequestTest()
        {
            var el = XmppXElement.LoadXml(REQUEST);
            
            Assert.AreEqual(el is Request, true);
            
            var req = el as Request;
            if (req != null)
            {
            }

            new Request().ShouldBe(REQUEST);
        }

        [TestMethod]
        public void AnswerTest()
        {
            var el = XmppXElement.LoadXml(ANSWER);

            Assert.AreEqual(el is Answer, true);

            var a = el as Answer;
            if (a != null)
            {
                Assert.AreEqual(a.LastHandledStanza, 0);
            }

            new Answer { LastHandledStanza = 0 }.ShouldBe(ANSWER);
        }

        [TestMethod]
        public void ResumeTest()
        {
            var el = XmppXElement.LoadXml(RESUME);

            Assert.AreEqual(el is Resume, true);

            var r = el as Resume;
            if (r != null)
            {
                Assert.AreEqual(r.LastHandledStanza, 1);
                Assert.AreEqual(r.PreviousId, "some-long-sm-id");
            }

            new Resume { LastHandledStanza = 1, PreviousId = "some-long-sm-id" }.ShouldBe(RESUME);
        }

        [TestMethod]
        public void ResumedTest()
        {
            var el = XmppXElement.LoadXml(RESUMED);

            Assert.AreEqual(el is Resumed, true);

            var r = el as Resumed;
            if (r != null)
            {
                Assert.AreEqual(r.LastHandledStanza, 3);
                Assert.AreEqual(r.PreviousId, "some-long-sm-id");
            }

            new Resumed { LastHandledStanza = 3, PreviousId = "some-long-sm-id" }.ShouldBe(RESUMED);
        }
    }
}
