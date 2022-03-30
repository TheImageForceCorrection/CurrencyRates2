namespace CurrencyRates.Configuration
{
    public sealed class ApplicationConfiguration
    {
        public string CurrencyDataUrl { get; set; } = string.Empty;

        public static string CurrencyDataUrlFallback = "https://www.cbr-xml-daily.ru/daily_json.js";
    }
}