namespace Matrix.Transport.Socket
{
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    
    public interface ICertificateValidator
    {
        bool RemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors);
    }
}
