using System;
using System.Text;
using System.Threading.Tasks;
using Matrix.Idn;
using Matrix.Xml;
using Matrix.Xmpp.Sasl;

namespace Matrix.Sasl.Scram
{
    public class ScramSha1Processor : ISaslProcessor
    {
        public async Task<XmppXElement> AuthenticateClientAsync(XmppClient xmppClient)
        {
            var scramHelper = new ScramHelper();

            var username = xmppClient.Username;
#if STRINGPREP
           var  password = StringPrep.SaslPrep(xmppClient.Password);
#else
            var password = xmppClient.Password;
#endif

            string msg = ToB64String(scramHelper.GenerateFirstClientMessage(username));
            var authMessage = new Auth(SaslMechanism.ScramSha1, msg);

            var ret1 = await xmppClient.SendAsync<Failure, Challenge>(authMessage);

            if (ret1 is Challenge)
            {
                var resp = GenerateFinalMessage(ret1 as Challenge, scramHelper, password);
                var ret2 = await xmppClient.SendAsync<Failure, Success>(resp);

                return ret2;
            }

            return ret1;
        }

        private Response GenerateFinalMessage(Challenge ch, ScramHelper scramHelper, string password)
        {
            byte[] b = ch.Bytes;
            string firstServerMessage = Encoding.UTF8.GetString(b, 0, b.Length);
            string clientFinalMessage = scramHelper.GenerateFinalClientMessage(firstServerMessage, password);
            return new Response(ToB64String(clientFinalMessage));
        }

        private static string ToB64String(string sin)
        {
            byte[] msg = Encoding.UTF8.GetBytes(sin);
            return Convert.ToBase64String(msg, 0, msg.Length);
        }

        private string FromB64String(string sin)
        {
            var b = Convert.FromBase64String(sin);
            return Encoding.UTF8.GetString(b, 0, b.Length);
        }
    }
}
