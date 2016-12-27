using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Matrix.DotNetty
{
    public class DefaultCertificateValidator : ICertificateValidator
    {
        public bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return (sslPolicyErrors == SslPolicyErrors.None);
        }
    }
}
