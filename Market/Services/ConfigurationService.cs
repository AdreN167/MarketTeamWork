using System.Data.SqlClient;
using System.Data.Common;
using Microsoft.Extensions.Configuration;

namespace Market.Services
{
    public static class ConfigurationService
    {
        public static IConfigurationRoot Configuration { get; private set; }

        public static void Init()
        {
            //if (DbProviderFactories.GetFactory("MarketProvider") == null)
            //{
                DbProviderFactories.RegisterFactory("MarketProvider", SqlClientFactory.Instance);
            //}

            if (Configuration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                Configuration = configurationBuilder.AddJsonFile("appSettings.json").Build();
            }
        }
    }
}

