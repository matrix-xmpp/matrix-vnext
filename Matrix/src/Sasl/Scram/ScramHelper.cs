using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Matrix.Core.Crypt;

namespace Matrix.Sasl.Scram
{
    internal class ScramHelper
    {
        private const int LenghtClientNonce     = 24;
        private const int LenghtServerNonce     = 24;
        private const int LenghtSalt            = 20;
        private const int DefaultIterationCount = 4096;

        private string _firstClientMessage;
        private string _firstServerMessage;

        private string _clientNonceB64;
        private string _serverNonceB64;

        private byte[] _salt;
        private byte[] _storedKey;
        private byte[] _serverKey;

        #region << client messages >>
        public string GenerateFirstClientMessage(string user)
        {
            _clientNonceB64 = GenerateClientNonce();

            var sb = new StringBuilder();

            // no channel bindings supported
            sb.Append("n,,");

            // username
            sb.Append("n=");
            sb.Append(EscapeUsername(user));
            sb.Append(",");

            // client nonce
            sb.Append("r=");
            sb.Append(_clientNonceB64);

            _firstClientMessage = sb.ToString();
            return _firstClientMessage;
        }

        public string GenerateFinalClientMessage(string sMessage, string password)
        {
            var pairs = ParseMessage(sMessage);

            //string clientServerNonce = pairs["r"];
            string serverNonce = pairs["r"].Substring(_clientNonceB64.Length);

            var salt = pairs["s"];   // the user's salt - (base64 encoded)
            var iteration = pairs["i"];  // iteation count

            // the bare of our first message
            var clientFirstMessageBare = _firstClientMessage.Substring(3);

            var sb = new StringBuilder();
            sb.Append("c=biws,");
            // Client/Server nonce
            sb.Append("r=");
            sb.Append(_clientNonceB64);
            sb.Append(serverNonce);

            string clientFinalMessageWithoutProof = sb.ToString();

            string authMessage = clientFirstMessageBare + "," + sMessage + "," + clientFinalMessageWithoutProof;

            var saltedPassword = Hi(password, Convert.FromBase64String(salt), Convert.ToInt32(iteration));

            var clientKey = Hash.HMAC(saltedPassword, "Client Key");
            var storedKey = Hash.Sha1HashBytes(clientKey);

            var clientSignature = Hash.HMAC(storedKey, authMessage);

            var clientProof = BinaryXor(clientKey, clientSignature);

            string clientFinalMessage = clientFinalMessageWithoutProof;
            clientFinalMessage += ",p=";
            clientFinalMessage += Convert.ToBase64String(clientProof);

            return clientFinalMessage;
        }

        public string GetUserFromFirstClientMessage(string msg)
        {
            if (String.IsNullOrEmpty(msg))
                return null;

            var pairs = ParseMessage(msg);
            if (pairs.Count == 0)
                return null;

            return pairs["n"];
        }
        #endregion

        #region << server messages >>
        /// <summary>
        /// Generates the first Server message bases on the first client message and the plain password
        /// </summary>
        /// <param name="msg">first client message</param>
        /// <param name="pass">plain password</param>
        /// <returns></returns>
        public string GenerateFirstServerMessage(string msg, string pass)
        {
            var pairs = ParseMessage(msg);

            string clientNonce = pairs["r"];
            string user = pairs["n"];

            _serverNonceB64 = GenerateServerNonce();
            _salt = GenerateSalt();

            var saltedPassword = Hi(pass, _salt, DefaultIterationCount);

            var clientKey = Hash.HMAC(saltedPassword, "Client Key");
            _storedKey = Hash.Sha1HashBytes(clientKey);
            _serverKey = Hash.HMAC(saltedPassword, "Server Key");

            var sb = new StringBuilder();

            sb.Append("r=");
            sb.Append(clientNonce + _serverNonceB64);
            sb.Append(",");

            sb.Append("s=");
            sb.Append(Convert.ToBase64String(_salt));
            sb.Append(",");

            sb.Append("i=");
            sb.Append(DefaultIterationCount);

            _firstClientMessage = msg;
            _firstServerMessage = sb.ToString();

            return _firstServerMessage;
        }

        /// <summary>
        /// Creates the final server message based on the final client message
        /// </summary>
        /// <param name="finalClient">The final client message</param>
        /// <returns>return null on failure and teh server final message on success</returns>
        public string GenerateFinalServerMessage(string finalClient)
        {
            var pairs = ParseMessage(finalClient);

            string channelbinding = pairs["c"];
            string nonce = pairs["r"];
            string proof = pairs["p"];

            byte[] bProof = Convert.FromBase64String(proof);

            string authMessage = BuildClientFirstMessageBare(_firstClientMessage)
                                 + "," + _firstServerMessage
                                 + "," + BuildClientFinalMessageWithoutProof(finalClient);

            var clientSignature = Hash.HMAC(_storedKey, authMessage);
            var serverSignature = Hash.HMAC(_serverKey, authMessage);

            var clientKey = BinaryXor(clientSignature, bProof);

            bool match = _storedKey.SequenceEqual((Hash.Sha1HashBytes(clientKey)));
            if (match)
            {
                // the server final message
                return "v=" + Convert.ToBase64String(serverSignature);
            }

            return null;
        }
        #endregion

        #region << generate randoms >>
        private byte[] GenerateSalt()
        {
            return Randoms.GenerateRandom(LenghtSalt);
        }

        /// <summary>
        /// Generate a random client nonce
        /// </summary>
        private string GenerateClientNonce()
        {
            var random = Randoms.GenerateRandom(LenghtClientNonce);
            return Convert.ToBase64String(random);
        }

        private string GenerateServerNonce()
        {
            var random = Randoms.GenerateRandom(LenghtServerNonce);
            return Convert.ToBase64String(random);
        }

        #endregion

        private static Dictionary<string, string> ParseMessage(string msg)
        {
            var str = msg.Split(',');

            var dict = new Dictionary<string, string>();
            foreach (string s in str)
            {
                int equalPos = s.IndexOf('=');
                if (equalPos == -1)
                    continue;

                var key = s.Substring(0, equalPos - 0);
                var val = s.Substring(equalPos + 1);

                if (!dict.ContainsKey(key))
                    dict.Add(key, val);
            }
            return dict;
        }

        public byte[] Hi(string pass, byte[] saltbytes, int iterations)
        {
            var pdb = new Rfc2898DeriveBytes(pass, saltbytes, iterations);
            return pdb.GetBytes(20);
        }

        /// <summary>
        /// Binary XOR
        /// </summary>
        /// <param name="b1"></param>
        /// <param name="b2"></param>
        /// <returns></returns>
        private byte[] BinaryXor(byte[] b1, byte[] b2)
        {
            var result = new byte[b1.Length];
            for (var i = 0; i < result.Length; ++i)
                result[i] = (byte)(b1[i] ^ b2[i]);

            return result;
        }

        private static string EscapeUsername(string user)
        {
            /*
            The characters ',' or '=' in usernames are sent as '=2C' and
            '=3D' respectively.  If the server receives a username that
            contains '=' not followed by either '2C' or '3D', then the
            server MUST fail the authentication.
            */

            var ret = user.Replace(",", "=2C");
            ret = ret.Replace("=", "=3D");

            return ret;
        }

        public string BuildClientFinalMessageWithoutProof(string finalClient)
        {
            var idx = finalClient.LastIndexOf(",p=", StringComparison.Ordinal);
            return finalClient.Substring(0, idx);
        }

        public string BuildClientFirstMessageBare(string clientFirst)
        {
            var idx = clientFirst.IndexOf("n=", StringComparison.Ordinal);
            return clientFirst.Substring(idx);
        }
    }
}