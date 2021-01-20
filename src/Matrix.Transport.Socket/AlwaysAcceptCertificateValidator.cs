namespace Matrix.Transport.Socket
{
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    
    /// <summary>
    /// Implementation of <see cref="ICertificateValidator"/> which considers all
    /// certificates as valid.
    /// This should be used for testing purposes only. Eg. for self signed certs.
    /// </summary>
    public class AlwaysAcceptCertificateValidator : ICertificateValidator
    {
        public bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
