using System.Security.Cryptography.X509Certificates;

namespace Server
{
    public interface ICertificateProvider
    {
        X509Certificate2 RequestCertificate(string xmppDomain);
    }
}
