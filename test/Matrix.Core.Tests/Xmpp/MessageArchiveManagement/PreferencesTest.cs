namespace Matrix.Tests.Xmpp.MessageArchiveManagement
{
    using Matrix.Xml;
    using Matrix.Xmpp.MessageArchiveManagement;
    using Shouldly;
    using Xunit;
    using System.Xml.Linq;
    using System.Linq;

    public class PreferencesTest
    {
        string PREFS1 = @"<prefs xmlns='urn:xmpp:mam:2' default='roster' />";
        string PREFS2 = @"<prefs xmlns='urn:xmpp:mam:2' default='always' />";
        string PREFS3 = @"<prefs xmlns='urn:xmpp:mam:2' default='never' />";

        string PREFS4 = @"<prefs xmlns='urn:xmpp:mam:2' default='roster'>
                        <always>
                          <jid>romeo@montague.lit</jid>
                        </always>                      
                      </prefs>";
        
        string PREFS5 = @"<prefs xmlns='urn:xmpp:mam:2' default='roster'>                        
                        <never>
                          <jid>montague@montague.lit</jid>
                        </never>
                      </prefs>";

        string PREFS6 = @"<prefs xmlns='urn:xmpp:mam:2' default='roster'>
                        <always>
                            <jid>romeo@montague.lit</jid>
                            <jid>romeo2@montague.lit</jid>
                        </always>
                        <never>
                            <jid>montague@montague.lit</jid>
                            <jid>montague2@montague.lit</jid>
                            <jid>montague3@montague.lit</jid>
                        </never>
                      </prefs>";

        [Fact]
        public void ShouldBeOfTypePreferences()
        {
            XmppXElement.LoadXml(PREFS1).ShouldBeOfType<Preferences>();
        }

        [Fact]
        public void ShouldContainAlwaysElement()
        {
            XmppXElement.LoadXml(PREFS4).Cast<Preferences>().Always.ShouldNotBeNull();
            XmppXElement.LoadXml(PREFS4).Cast<Preferences>().Never.ShouldBeNull();
        }

        [Fact]
        public void ShouldContainNeverElement()
        {
            XmppXElement.LoadXml(PREFS5).Cast<Preferences>().Always.ShouldBeNull();
            XmppXElement.LoadXml(PREFS5).Cast<Preferences>().Never.ShouldNotBeNull();
        }

        [Fact]
        public void TestDefaultProperty()
        {
            XmppXElement.LoadXml(PREFS1).Cast<Preferences>().Default.ShouldBe(DefaultPreference.Roster);
            XmppXElement.LoadXml(PREFS2).Cast<Preferences>().Default.ShouldBe(DefaultPreference.Always);
            XmppXElement.LoadXml(PREFS3).Cast<Preferences>().Default.ShouldBe(DefaultPreference.Never);
        }

        [Fact]
        public void PolicyCountsShouldMatch()
        {
            XmppXElement.LoadXml(PREFS6).Cast<Preferences>().Always.GetJids().Count().ShouldBe(2);
            XmppXElement.LoadXml(PREFS6).Cast<Preferences>().Never.GetJids().Count().ShouldBe(3);
        }

        [Fact]
        public void NeverPolicyShouldContainSpecificJid()
        {
            XmppXElement.LoadXml(PREFS5).Cast<Preferences>().Never.GetJids().Count().ShouldBe(1);
            XmppXElement.LoadXml(PREFS5).Cast<Preferences>().Never.GetJids().FirstOrDefault()?.Value.ShouldBe("montague@montague.lit");
            XmppXElement.LoadXml(PREFS5).Cast<Preferences>().Never.GetJids().FirstOrDefault()?.Value.ShouldNotBe("foo");
        }

        [Fact]
        public void TestBuildPreferences()
        {
            new Preferences()
            {
                Default = DefaultPreference.Roster
            }.ShouldBe(PREFS1);

            new Preferences()
            {
                Default = DefaultPreference.Always
            }.ShouldBe(PREFS2);


            new Preferences()
            {
                Default = DefaultPreference.Never
            }.ShouldBe(PREFS3);
        }

        [Fact]
        public void TestBuildPolicy()
        {
            var always = new Always();
            always.AddJid("romeo@montague.lit");
            always.AddJid("romeo2@montague.lit");

            var never = new Never();
            never.AddJid("montague@montague.lit");
            never.AddJid("montague2@montague.lit");
            never.AddJid("montague3@montague.lit");

            var prefs = new Preferences()
            {
                Default = DefaultPreference.Roster,
                Never = never,
                Always = always
            };

            prefs.ShouldBe(PREFS6);
        }
    }
}
