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

namespace Matrix.Sasl.Digest
{
    /// <summary>
    /// Implementation od Digest-Md5 step 1
    /// </summary>
    public class Step1
    {
        #region Xml sample
        /*
        encoded challenge to client: 
        
        <challenge xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
        cmVhbG09InNvbWVyZWFsbSIsbm9uY2U9Ik9BNk1HOXRFUUdtMmhoIixxb3A9ImF1dGgi
        LGNoYXJzZXQ9dXRmLTgsYWxnb3JpdGhtPW1kNS1zZXNzCg==
        </challenge>
          
        The decoded challenge is: 
        
        realm="somerealm",nonce="OA6MG9tEQGm2hh",qop="auth",charset=utf-8,algorithm=md5-sess
        */
        #endregion

        #region << Constructors >>
        public Step1(string message)
        {
            Parse(message);
        }
        #endregion

        #region << Properties >>

        public string Realm { get; set; }

        public string Nonce { get; set; }

        /// <summary>
        /// default Qop to "auth" when not present as documented in rfc2831
        /// </summary>
        public string Qop { get; set; }       = "auth";

        public string Charset { get; set; }   = "utf-8";

        public string Algorithm { get; set; }

        public string Rspauth { get; set; }

        #endregion

        /*
            nonce="deqOGux/N6hDPtf9vkGMU5Vzae+zfrqpBIvh6LovbBM=",
            realm="amessage.de",
            qop="auth,auth-int,auth-conf",
            cipher="rc4-40,rc4-56,rc4,des,3des",
            maxbuf=1024,
            charset=utf-8,
            algorithm=md5-sess
        */
        private void Parse(string message)
        {
            try
            {
                int start = 0;
                while (start < message.Length)
                {
                    int equalPos = message.IndexOf('=', start);
                    if (equalPos > 0)
                    {
                        // look if the next char is a quote
                        int end;
                        if (message.Substring(equalPos + 1, 1) == "\"")
                        {
                            // quoted value, find the end now
                            end = message.IndexOf('"', equalPos + 2);
                            ParsePair(message.Substring(start, end - start + 1));
                            start = end + 2;
                        }
                        else
                        {
                            // value is not quoted, ends at the next comma or end of string   
                            end = message.IndexOf(',', equalPos + 1);
                            if (end == -1)
                                end = message.Length;

                            ParsePair(message.Substring(start, end - start));

                            start = end + 1;
                        }
                    }
                }
            }
            catch
            {
                throw new SaslException("Unable to parse challenge");
            }
        }

        private void ParsePair(string pair)
        {
            // have seen servers which put spaces before the pairs
            pair = pair.Trim();

            int equalPos = pair.IndexOf("=", StringComparison.Ordinal);
            if (equalPos > 0)
            {
                string key = pair.Substring(0, equalPos);
                // is the value quoted?

                string data = pair.Substring(equalPos + 1, 1) == "\""
                                  ? pair.Substring(equalPos + 2, pair.Length - equalPos - 3)
                                  : pair.Substring(equalPos + 1);

                switch (key)
                {
                    case "realm":
                        Realm = data;
                        break;
                    case "nonce":
                        Nonce = data;
                        break;
                    case "qop":
                        Qop = data;
                        break;
                    case "charset":
                        Charset = data;
                        break;
                    case "algorithm":
                        Algorithm = data;
                        break;
                    case "rspauth":
                        Rspauth = data;
                        break;
                }
            }
        }
    }
}
