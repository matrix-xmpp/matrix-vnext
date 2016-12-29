using System;
using System.Security.Cryptography;
using System.Text;
using Matrix.Core.Crypt;

namespace Matrix.Sasl.Digest
{
    /// <summary>
    /// Summary description for Step2.
    /// </summary>
    internal class Step2
    {
        Step1 _step1;
        XmppClient _xmppClient;

        /// <summary>
        /// builds a step2 message reply to the given step1 message
        /// </summary>
        /// <param name="step1">The step1.</param>
        /// <param name="xmppClient">The xmppClient.</param>
        internal Step2(Step1 step1, XmppClient xmppClient)
        {
            _step1 = step1;
            _xmppClient = xmppClient;

            _step1.Nonce = step1.Nonce;

            // fixed for SASL in amessage servers (jabberd 1.x)
            if (SupportsAuth(step1.Qop))
                _step1.Qop = "auth";

            GenerateCnonce();
            GenerateNc();
            GenerateDigestUri();
            GenerateResponse();
        }

        /// <summary>
        /// Does the server support Auth?
        /// </summary>
        /// <param name="qop"></param>
        /// <returns></returns>
        private bool SupportsAuth(string qop)
        {
            string[] auth = qop.Split(',');
            // This overload was not available in the CF, so updated this to the following
            //bool ret = Array.IndexOf(auth, "auth") < 0 ? false : true;
            return Array.IndexOf(auth, "auth", auth.GetLowerBound(0), auth.Length) < 0 ? false : true;
        }

        #region << Properties and member variables >>
        private string m_Cnonce;
        private string m_Nc;
        private string m_DigestUri;
        private string m_Response;
        private string m_Authzid;

        internal string Cnonce
        {
            get { return m_Cnonce; }
            set { m_Cnonce = value; }
        }

        internal string Nc
        {
            get { return m_Nc; }
            set { m_Nc = value; }
        }

        internal string DigestUri
        {
            get { return m_DigestUri; }
            set { m_DigestUri = value; }
        }

        internal string Response
        {
            get { return m_Response; }
            set { m_Response = value; }
        }

        internal string Authzid
        {
            get { return m_Authzid; }
            set { m_Authzid = value; }
        }
        #endregion


        internal string GetMessage()
        {
            return GenerateMessage();
        }

        private void GenerateCnonce()
        {
            // Lenght of the Session ID on bytes,
            // 32 bytes equaly 64 chars
            // 16^64 possibilites for the session IDs (4.294.967.296)
            // This should be unique enough
            const int lenght = 32;


            RandomNumberGenerator rng = RandomNumberGenerator.Create();

            var buf = new byte[lenght];
            rng.GetBytes(buf);

            m_Cnonce = Hash.HexToString(buf).ToLower();
#if TEST
            m_Cnonce = "28f47432f9606887d9b727e65db225eb7cb4b78073d8b6f32399400e01438f1e";
#endif
        }

        private void GenerateNc()
        {
            const int nc = 1;
            m_Nc = nc.ToString().PadLeft(8, '0');
        }

        private void GenerateDigestUri()
        {
            m_DigestUri = "xmpp/" + _xmppClient.XmppDomain;
        }

        /*
			HEX( KD ( HEX(H(A1)),
			{
				nonce-value, ":" nc-value, ":",
				cnonce-value, ":", qop-value, ":", HEX(H(A2)) }))
		
			If authzid is specified, then A1 is
		
			A1 = { H( { username-value, ":", realm-value, ":", passwd } ),
			":", nonce-value, ":", cnonce-value, ":", authzid-value }
		
			If authzid is not specified, then A1 is
		
			A1 = { H( { username-value, ":", realm-value, ":", passwd } ),
			":", nonce-value, ":", cnonce-value }
		
			where
		
			passwd   = *OCTET
        */

        internal void GenerateResponse()
        {
            byte[] H1;
            byte[] H2;
            byte[] H3;
            //byte[] temp;
            string A1;
            string A2;
            string A3;
            string p1;
            string p2;

            var sb = new StringBuilder();
            sb.Append(_xmppClient.Username);
            sb.Append(":");
            sb.Append(_step1.Realm);
            sb.Append(":");
            sb.Append(_xmppClient.Password);

            //H1 = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
            H1 = Hash.Md5HashBytes(Encoding.UTF8.GetBytes(sb.ToString()));
#if TEST
            var H1hex = Util.Hash.HexToString(H1);
#endif

            sb.Remove(0, sb.Length);
            sb.Append(":");
            sb.Append(_step1.Nonce);
            sb.Append(":");
            sb.Append(Cnonce);

            if (m_Authzid != null)
            {
                sb.Append(":");
                sb.Append(m_Authzid);
            }
            A1 = sb.ToString();

#if (SILVERLIGHT || WINRT)
            byte[] bA1 = Encoding.UTF8.GetBytes(A1);
#else
            byte[] bA1 = Encoding.ASCII.GetBytes(A1);
#endif
            byte[] bH1A1 = new byte[H1.Length + bA1.Length];

            Array.Copy(H1, 0, bH1A1, 0, H1.Length);
            Array.Copy(bA1, 0, bH1A1, H1.Length, bA1.Length);

#if TEST
            var bH1A1hex = Util.Hash.HexToString(bH1A1);
#endif

            //H1 = new MD5CryptoServiceProvider().ComputeHash(bH1A1);	
            H1 = Hash.Md5HashBytes(bH1A1);

#if TEST
            H1hex = Util.Hash.HexToString(H1);
#endif

            sb.Remove(0, sb.Length);
            sb.Append("AUTHENTICATE:");
            sb.Append(m_DigestUri);
            if (_step1.Qop.CompareTo("auth") != 0)
            {
                sb.Append(":00000000000000000000000000000000");
            }
            A2 = sb.ToString();
#if (SILVERLIGHT || WINRT)
            H2 = Encoding.UTF8.GetBytes(A2);
#else
            H2 = Encoding.ASCII.GetBytes(A2);
#endif

            //H2 = new MD5CryptoServiceProvider().ComputeHash(H2);
            H2 = Hash.Md5HashBytes(H2);
#if TEST
            var H2hex = Util.Hash.HexToString(H2);
#endif
            // create p1 and p2 as the hex representation of H1 and H2
            p1 = Hash.HexToString(H1).ToLower();
            p2 = Hash.HexToString(H2).ToLower();

            sb.Remove(0, sb.Length);
            sb.Append(p1);
            sb.Append(":");
            sb.Append(_step1.Nonce);
            sb.Append(":");
            sb.Append(m_Nc);
            sb.Append(":");
            sb.Append(m_Cnonce);
            sb.Append(":");
            sb.Append(_step1.Qop);
            sb.Append(":");
            sb.Append(p2);

            A3 = sb.ToString();

            H3 = Hash.Md5HashBytes(Encoding.ASCII.GetBytes(A3));
#if TEST
            var H3hex = Util.Hash.HexToString(H3);
#endif
            m_Response = Hash.HexToString(H3).ToLower();
        }

        private string GenerateMessage()
        {
            var sb = new StringBuilder();
            sb.Append("username=");
            sb.Append(AddQuotes(_xmppClient.Username));
            sb.Append(",");
            sb.Append("realm=");
            sb.Append(AddQuotes(_step1.Realm));
            sb.Append(",");
            sb.Append("nonce=");
            sb.Append(AddQuotes(_step1.Nonce));
            sb.Append(",");
            sb.Append("cnonce=");
            sb.Append(AddQuotes(Cnonce));
            sb.Append(",");
            sb.Append("nc=");
            sb.Append(Nc);
            sb.Append(",");
            sb.Append("qop=");
            sb.Append(_step1.Qop);
            sb.Append(",");
            sb.Append("digest-uri=");
            sb.Append(AddQuotes(DigestUri));
            sb.Append(",");
            sb.Append("charset=");
            sb.Append(_step1.Charset);
            sb.Append(",");
            sb.Append("response=");
            sb.Append(Response);

            return sb.ToString();
        }

        /// <summary>
        /// return the given string with quotes
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string AddQuotes(string s)
        {
            // fixed, s can be null (eg. for realm in ejabberd)
            if (!string.IsNullOrEmpty(s))
                s = s.Replace(@"\", @"\\");

            const string quote = "\"";
            return quote + s + quote;
        }
    }
}