using System;
using System.Collections.Generic;
using System.Text;
using Matrix.Network;
using Matrix.Sasl;

namespace Matrix
{
    public class XmppClient : XmppConnection
    {

        #region << Properties >>
        public string Username { get; set; }
        public string Password { get; set; }


        public IAuthenticate SalsHandler { get; set; } = new DefaultSaslHandler();

        public ICertificateValidator CertificateValidator { get; set; } = new DefaultCertificateValidator();

        #endregion
    }
}
