using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Web.Http;
using CurrencyRates.Configuration;
using CurrencyRates.Data.Dtos;
using CurrencyRates.Data.Implementation.JsonDtos;
using Newtonsoft.Json;

namespace CurrencyRates.Data.Implementation
{
    public class CurrenciesProvider : ICurrenciesProvider
    {
        private readonly IConfigurationProvider _configurationProvider;

        public CurrenciesProvider(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public async Task<CurrenciesDto?> Provide()
        {
            var currencyDataUrl = _configurationProvider.ApplicationConfiguration.CurrencyDataUrl;

            var jsonCurrency = await GetJsonCurrenciesFromUrl(currencyDataUrl);

            return DeserializeJsonCurrency(jsonCurrency);
        }

        private async Task<string> GetJsonCurrenciesFromUrl(string currencyDataUrl)
        {
            var httpClient = new HttpClient();
            var httpResponse = await httpClient.GetAsync(new Uri(currencyDataUrl));

            return await httpResponse.Content.ReadAsStringAsync();
        }

        private CurrenciesDto? DeserializeJsonCurrency(string jsonCurrency)
        {
            var jsonSerializer = new JsonSerializer();
            var currencies =
                jsonSerializer.Deserialize<Currencies>(new JsonTextReader(new StringReader(jsonCurrency)));
            return currencies?.ToCurrenciesDto();
        }
    }
}