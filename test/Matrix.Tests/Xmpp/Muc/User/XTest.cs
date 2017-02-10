using Matrix.Xmpp.Muc.User;
using Xunit;
using X = Matrix.Xmpp.Muc.User.X;

namespace Matrix.Tests.Xmpp.Muc.User
{
    
    public class XTest
    {
        [Fact]
        public void TestBuildUserX()
        {
            var x = new X();
            x.AddInvite(new Invite { To = "hecate@shakespeare.lit"});
            x.Password = "cauldronburn";
            
            x.ShouldBe(Resource.Get("Xmpp.Muc.User.userx1.xml"));
        }
    }
}