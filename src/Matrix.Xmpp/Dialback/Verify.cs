using System;
using Matrix.Core.Crypt;
using Matrix.Core.Attributes;
using Matrix.Xmpp.Base;

namespace Matrix.Xmpp.Dialback
{
    [XmppTag(Name = "verify", Namespace = Namespaces.ServerDialback)]
    public class Verify : XmppXElementWithAddress
    {
        public Verify() : base(Namespaces.ServerDialback, "verify")
        {
        }

        /// <summary>
        /// Generates a Dialback key as described in XEP-0185
        /// </summary>
        /// <param name="secret"></param>
        /// <param name="receivingServer"></param>
        /// <param name="originatingServer"></param>
        /// <param name="streamId"></param>
        /// <returns></returns>
        public static string GenerateDialbackKey(
            string secret,
            string receivingServer,
            string originatingServer,
            string streamId)
        {
            return 
                Hash.HMACSHA256HashHex(
                    Hash.Sha256HashHex(secret),
                    String.Concat(receivingServer, " ", originatingServer, " ", streamId)
                );
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public string Id
        {
            get { return GetAttribute("id"); }
            set { SetAttribute("id", value); }
        }

        /// <summary>
        /// The dialbackkey
        /// </summary>
        public string DialbackKey
        {
            get { return Value; }
            set { Value = value; }
        }


        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public VerifyType Type
        {
            get { return GetAttributeEnum<VerifyType>("type"); }
            set
            {
                if (value == VerifyType.None)
                    RemoveAttribute("type");
                else
                    SetAttribute("type", value.ToString().ToLower());
            }
        }
    }
}