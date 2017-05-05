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
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Matrix.Attributes;
using Matrix.Crypt;
using Matrix.Xml;
using Matrix.Xmpp.Disco;

namespace Matrix.Xmpp.Capabilities
{
    [XmppTag(Name = "c", Namespace = Namespaces.Caps)]
    public class Caps : XmppXElement
    {
        public Caps()
            : base(Namespaces.Caps, "c")
        {
        }

        /// <summary>
        /// Required node attribute
        /// </summary>
        public string Node
        {
            get { return GetAttribute("node"); }
            set { SetAttribute("node", value); }
        }

        /// <summary>
        /// Required version attribute
        /// </summary>
        public string Version
        {
            get { return GetAttribute("ver"); }
            set { SetAttribute("ver", value); }
        }

        /// <summary>
        /// Required Hash-Type.
        /// </summary>
        public string Hash
        {
            get { return GetAttribute("hash"); }
            set { SetAttribute("hash", value); }
        }

        /// <summary>
        /// Sets the hash algorithm. It does the same as the Hash member, only using a enum type instead of a string
        /// as parameter.
        /// </summary>
        /// <value>
        /// The hash algorithm.
        /// </value>
        public HashAlgorithms HashAlgorithm
        {
            set { Hash = value.ToName(); }
            get { return Crypt.Hash.HashAlgorithmsFromName(Hash); }
        }

        /// <summary>
        /// Builds and sets the caps ver attribute from a disco info object
        /// </summary>
        /// <param name="info"></param>
        public void SetVersion(Info info)
        {
            Hash = HashAlgorithms.Sha1.ToName(); // "sha-1";
            Version = BuildHash(info);
        }
        
        /// <summary>
        /// Build the caps hash from the given disco info object with the default hash algorithmn SHA1
        /// </summary>
        /// <param name="di"></param>
        /// <returns></returns>
        public static string BuildHash(Info di)
        {
            return BuildHash(di, HashAlgorithms.Sha1);
        }

        public static string BuildHash(Info di, HashAlgorithms hashAlgorithm)
        {
            var ids = di.GetIdentities().Select(did => did.Key).ToList();
            var features = di.GetFeatures().Select(df => df.Var).ToList();
            var forms = di.GetDataForms().Select(form => form.GetField("FORM_TYPE").GetValue()).ToList();

            // sort everything now
            ids.Sort();
            features.Sort();
            forms.Sort();

            var sb = new StringBuilder();

            foreach (string s in ids)
                sb.Append(s + "<");

            foreach (string s in features)
                sb.Append(s + "<");

            foreach (string s in forms)
                sb.Append(s + "<");

            foreach (string formType in forms)
            {
                string ftype = formType;
                var data = di.GetDataForms().FirstOrDefault(f => f.GetField("FORM_TYPE").GetValue() == ftype);
                var fields = (
                                 from field in data.GetFields()
                                 where field.Var != "FORM_TYPE"
                                 orderby field.Var
                                 select field.Var
                             ).ToList();

                foreach (string field in fields)
                {
                    sb.Append(field + "<");
                    foreach (string val in data.GetField(field).GetValues())
                        sb.Append(val + "<");
                }
            }

            string ret = HashBytes(Encoding.UTF8.GetBytes(sb.ToString()), hashAlgorithm);
            return ret;
        }


        internal static string HashBytes(byte[] val, HashAlgorithms hashAlgorithm)
        {
            using (HashAlgorithm hasher = Crypt.Hash.GetHashAlgorithm(hashAlgorithm))
            {
                byte[] hash = hasher.ComputeHash(val, 0, val.Length);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
