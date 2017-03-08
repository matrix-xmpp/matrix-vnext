using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Server
{
    public class PfxCertificateProvider : ICertificateProvider
    {
       public X509Certificate2 RequestCertificate(string xmppDomain)
        {
            var certConfig = ServerSettings.Certificate(xmppDomain);
            return new X509Certificate2(Path.Combine(ExampleHelper.ProcessDirectory, certConfig["path"]), certConfig["secret"]);
        }
    }
}
