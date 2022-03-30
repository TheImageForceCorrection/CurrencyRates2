using System;
using System.IO;
using Newtonsoft.Json;

namespace CurrencyRates.Configuration.Implementation
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private const string ConfigurationFileName = "Config.json";

        public ConfigurationProvider()
        {
            var loadedApplicationConfiguration = Load();

            if (loadedApplicationConfiguration is not null)
            {
                ApplicationConfiguration = loadedApplicationConfiguration;
                return;
            }

            ApplicationConfiguration =
                new ApplicationConfiguration { CurrencyDataUrl = ApplicationConfiguration.CurrencyDataUrlFallback };
        }

        public ApplicationConfiguration ApplicationConfiguration { get; }

        private ApplicationConfiguration? Load()
        {
            if (!File.Exists(ConfigurationFileName))
            {
                return null;
            }

            try
            {
                using var file = File.OpenText(ConfigurationFileName);
                using var jsonReader = new JsonTextReader(file);
                var serializer = new JsonSerializer();
                return serializer.Deserialize<ApplicationConfiguration>(jsonReader);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}