namespace Matrix.Transport
{
    using System;
    using System.Threading.Tasks;

    public interface IResolver
    {
        Task<Uri> ResolveUriAsync(string xmppDomain);
    }
}
