using Matrix.Xmpp.Jingle.Transports;

using Xunit;


namespace Matrix.Xmpp.Tests.Jingle.Transports
{
    [Collection("Factory collection")]
    public class IBBTest
    {
        const string XML1 = @"<transport xmlns='urn:xmpp:jingle:transports:ibb:1'
                 block-size='4096'
                 sid='ch3d9s71'/>";
        
        [Fact]
        public void Test1()
        {
            // TODO
            //var xmpp1 = XmppXElement.LoadXml(XML1);
            //Assert.Equal(true, xmpp1 is Matrix.Xmpp.Jingle.Transports.Transport);

            //var tp = xmpp1 as Matrix.Xmpp.Jingle.Transports.Transport;
            //if (tp != null)
            //{
            //    Assert.Equal(tp.BlockSize, 4096);
            //    Assert.Equal(tp.Sid, "ch3d9s71");
                
            //}
        }

        [Fact]
        public void Test2()
        {
            var tp = new TransportIbb { BlockSize = 4096, Sid = "ch3d9s71"};
            tp.ShouldBe(XML1);
        }
    }
}
