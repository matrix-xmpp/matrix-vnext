/*
 * Copyright (c) 2003-2017 by AG-Software <info@ag-software.de>
 *
 * All Rights Reserved.
 * See the COPYING file for more information.
 *
 * This file is part of the MatriX project.
 *
 * NOTICE: All information contained herein is, and remains the property
 * of AG-Software and its suppliers, if any.
 * The intellectual and technical concepts contained herein are proprietary
 * to AG-Software and its suppliers and may be covered by German and Foreign Patents,
 * patents in process, and are protected by trade secret or copyright law.
 *
 * Dissemination of this information or reproduction of this material
 * is strictly forbidden unless prior written permission is obtained
 * from AG-Software.
 *
 * Contact information for AG-Software is available at http://www.ag-software.de
 */

using System;
using System.Security.Cryptography;
using System.Text;
using Matrix.Crypt;

namespace Matrix.Sasl.Digest
{
    /// <summary>
    /// Implementation od Digest-Md5 step 2
    /// </summary>
    public class Step2
    {
        readonly Step1 step1;
        readonly XmppClient xmppClient;

        /// <summary>
        /// builds a step2 message reply to the given step1 message
        /// </summary>
        /// <param name="step1">The step1.</param>
        /// <param name="xmppClient">The xmppClient.</param>
        public Step2(Step1 step1, XmppClient xmppClient)
        {
            this.step1 = step1;
            this.xmppClient = xmppClient;

            this.step1.Nonce = step1.Nonce;
         
            GenerateCnonce();
            GenerateNc();
            GenerateDigestUri();
            GenerateResponse();
        }

        #region << Properties and member variables >>
        internal string Cnonce { get; set; }

        internal string Nc { get; set; }

        internal string DigestUri { get; set; }

        internal string Response { get; set; }

        internal string Authzid { get; set; }
        #endregion


        public string GetMessage()
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

            Cnonce = buf.ToHex();
#if TEST
            m_Cnonce = "28f47432f9606887d9b727e65db225eb7cb4b78073d8b6f32399400e01438f1e";
#endif
        }

        private void GenerateNc()
        {
            const int nc = 1;
            Nc = nc.ToString().PadLeft(8, '0');
        }

        private void GenerateDigestUri()
        {
            DigestUri = "xmpp/" + xmppClient.XmppDomain;
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
            sb.Append(xmppClient.Username);
            sb.Append(":");
            sb.Append(step1.Realm ?? xmppClient.XmppDomain);
            sb.Append(":");
            sb.Append(xmppClient.Password);

            //H1 = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
            H1 = Hash.Md5HashBytes(Encoding.UTF8.GetBytes(sb.ToString()));
#if TEST
            var H1hex = Util.Hash.HexToString(H1);
#endif

            sb.Remove(0, sb.Length);
            sb.Append(":");
            sb.Append(step1.Nonce);
            sb.Append(":");
            sb.Append(Cnonce);

            if (Authzid != null)
            {
                sb.Append(":");
                sb.Append(Authzid);
            }
            A1 = sb.ToString();

            byte[] bA1 = Encoding.ASCII.GetBytes(A1);

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
            /*
                from rfc2831
                If the "qop" directive's value is "auth", then A2 is:

                  A2       = { "AUTHENTICATE:", digest-uri-value }

               If the "qop" value is "auth-int" or "auth-conf" then A2 is:

                  A2       = { "AUTHENTICATE:", digest-uri-value,
                           ":00000000000000000000000000000000" }
            */
            sb.Append("AUTHENTICATE:");
            sb.Append(DigestUri);

            if (step1.Qop != "auth")
            { 
                sb.Append(":00000000000000000000000000000000");
            }

            A2 = sb.ToString();
            H2 = Encoding.ASCII.GetBytes(A2);


            //H2 = new MD5CryptoServiceProvider().ComputeHash(H2);
            H2 = Hash.Md5HashBytes(H2);
#if TEST
            var H2hex = Util.Hash.HexToString(H2);
#endif
            // create p1 and p2 as the hex representation of H1 and H2
            p1 = H1.ToHex();
            p2 = H2.ToHex();

            sb.Remove(0, sb.Length);
            sb.Append(p1);
            sb.Append(":");
            sb.Append(step1.Nonce);
            sb.Append(":");
            sb.Append(Nc);
            sb.Append(":");
            sb.Append(Cnonce);
            sb.Append(":");
            sb.Append(step1.Qop);
            sb.Append(":");
            sb.Append(p2);

            A3 = sb.ToString();

            H3 = Hash.Md5HashBytes(Encoding.ASCII.GetBytes(A3));
#if TEST
            var H3hex = Util.Hash.HexToString(H3);
#endif
            Response = H3.ToHex().ToLower();
        }

        private string GenerateMessage()
        {
            var sb = new StringBuilder();
            sb.Append("username=");
            sb.Append(AddQuotes(xmppClient.Username));
            sb.Append(",");
            sb.Append("realm=");
            sb.Append(AddQuotes(step1.Realm ?? xmppClient.XmppDomain));
            sb.Append(",");
            sb.Append("nonce=");
            sb.Append(AddQuotes(step1.Nonce));
            sb.Append(",");
            sb.Append("cnonce=");
            sb.Append(AddQuotes(Cnonce));
            sb.Append(",");
            sb.Append("nc=");
            sb.Append(Nc);
            sb.Append(",");
            sb.Append("qop=");
            sb.Append(step1.Qop);
            sb.Append(",");
            sb.Append("digest-uri=");
            sb.Append(AddQuotes(DigestUri));
            sb.Append(",");
            sb.Append("charset=");
            sb.Append(step1.Charset);
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
