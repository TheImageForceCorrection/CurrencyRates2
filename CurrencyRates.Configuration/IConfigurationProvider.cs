namespace CurrencyRates.Configuration;

public interface IConfigurationProvider
{
    public ApplicationConfiguration ApplicationConfiguration { get; }
}