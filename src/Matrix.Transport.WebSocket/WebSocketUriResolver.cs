namespace Matrix.Transport.WebSocket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Json;
    using Matrix.Xml;
    using Polly;
    using Xmpp;

    public class WebSocketUriResolver : IResolver
    {
        private readonly HttpClient client = new HttpClient();

        public WebSocketUriResolver()
        {
            Factory.RegisterElementsFromAssembly(typeof(Xml.HostMeta).GetTypeInfo().Assembly);
        }

        private async Task<string> GetData(string uri)
        {
            var response = await client.GetAsync(uri).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            throw new HttpRequestException();
        }

        private IEnumerable<HostMetaUri> GetUrls(string xmppDomain)
        {
            yield return new HostMetaUri { Uri = $"https://{xmppDomain}/.well-known/host-meta.json", Type = HostMetaType.Json};
            yield return new HostMetaUri { Uri = $"https://{xmppDomain}/.well-known/host-meta", Type = HostMetaType.Xml };

            yield return new HostMetaUri { Uri = $"http://{xmppDomain}/.well-known/host-meta.json", Type = HostMetaType.Json};
            yield return new HostMetaUri { Uri = $"http://{xmppDomain}/.well-known/host-meta", Type = HostMetaType.Xml};
        }

        public async Task<Uri> ResolveUriAsync(string xmppDomain)
        {
            var addressIterator = GetUrls(xmppDomain).GetEnumerator();
            addressIterator.MoveNext();

            var retryPolicy = Policy<string>
                .Handle<HttpRequestException>()
                .RetryAsync(3, onRetry: (exception, retryCount) => { addressIterator.MoveNext(); });

            var fallbackPolicy = Policy<string>
                .Handle<Exception>()
                .FallbackAsync<string>(fallbackAction: (ct) => Task.FromResult(string.Empty));

            var data = await fallbackPolicy
                .WrapAsync(retryPolicy)
                .ExecuteAsync(async () => await GetData(addressIterator.Current.Uri).ConfigureAwait(false))
                .ConfigureAwait(false);

            if (!string.IsNullOrEmpty(data))
            {
                return addressIterator.Current.Type == HostMetaType.Json
                    ? GetUriFromJsonMetadata(data)
                    : GetUriFromXmlMetadata(data);
            }

            return null;
        }

        public Uri GetUriFromJsonMetadata(string meta)
        {
            try
            {
                var hosts = JsonSerializer.Deserialize<HostMeta>(meta,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    });

                var link = hosts?.Links?.FirstOrDefault(l => l.Rel == Namespaces.AlternativeConnectionsWebSocket);
                if (link != null)
                {
                    return new Uri(link.Href);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public Uri GetUriFromXmlMetadata(string meta)
        {
            try
            {
                var hosts = XmppXElement
                    .LoadXml(meta)
                    .Cast<Xml.HostMeta>();

                var link = hosts?.Links?.FirstOrDefault(l => l.Rel == Namespaces.AlternativeConnectionsWebSocket);
                return link != null ? new Uri(link.Href) : null;
            }
            catch
            {
                return null;
            }
        }
    }
}
