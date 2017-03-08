using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Server
{
    public static class ServerSettings
    {
        public static bool IsSsl
        {
            get
            {
                string ssl = ExampleHelper.Configuration["ssl"];
                return !string.IsNullOrEmpty(ssl) && bool.Parse(ssl);
            }
        }

        public static List<IConfigurationSection> Hosts => ExampleHelper.Configuration.GetSection("hosts").GetChildren().ToList();

        public static int Port => int.Parse(ExampleHelper.Configuration["port"]);

        public static bool HostExists(string host)
        {
            foreach (var h in Hosts)
            {
                if (h.Key == host)
                    return true;
            }
            return false;
        }

        public static IConfigurationSection Certificate(string host)
        {
            foreach (var h in Hosts)
            {
                if (h.Key == host)
                    return h.GetSection("certificate");
            }
            return null;
        }
    }
}
