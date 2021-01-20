namespace Matrix.Transport.WebSocket.Tests.Server
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;

    public class TestServerApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override TestServer CreateServer(IWebHostBuilder builder) =>
            base.CreateServer(
                builder
                    //.UseConfiguration(Config)
                    .UseSolutionRelativeContentRoot(""));

        protected override IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
                .UseStartup<TStartup>();
    }
}