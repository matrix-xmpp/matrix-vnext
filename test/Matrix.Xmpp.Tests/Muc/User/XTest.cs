using Matrix.Xmpp.Muc.User;
using Xunit;
using X = Matrix.Xmpp.Muc.User.X;

namespace Matrix.Xmpp.Tests.Muc.User
{
    [Collection("Factory collection")]
    public class XTest
    {
        string xml1 = @"<x xmlns='http://jabber.org/protocol/muc#user'>
                <invite to='hecate@shakespeare.lit' />
                <password>cauldronburn</password>               
              </x>";
        
        
        [Fact]
        public void Test1()
        {
            var x = new X();
            x.AddInvite(new Invite { To = "hecate@shakespeare.lit"});
            x.Password = "cauldronburn";
            
            x.ShouldBe(xml1);
        }
    }
}