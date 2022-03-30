using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrencyRates.Data;

namespace CurrencyRates.BusinessLogic.Implementation;

public sealed class RelativeCurrencyRatesProvider : IRelativeCurrencyRatesProvider
{
    private readonly ICurrenciesProvider _currenciesProvider;
    private readonly IRelativeCurrencyRateCalculator _relativeCurrencyRateCalculator;

    public RelativeCurrencyRatesProvider(ICurrenciesProvider currenciesProvider,
        IRelativeCurrencyRateCalculator relativeCurrencyRateCalculator)
    {
        _currenciesProvider = currenciesProvider;
        _relativeCurrencyRateCalculator = relativeCurrencyRateCalculator;
    }

    public async Task<IReadOnlyList<RelativeCurrencyRate>> ProvideAll()
    {
        var currencies = await _currenciesProvider.Provide();
        if (currencies is null)
        {
            return new List<RelativeCurrencyRate>();
        }

        var relativeCurrencyRates = new List<RelativeCurrencyRate>();

        foreach (var firstCurrency in currencies.CurrencyInfos)
        {
            foreach (var secondCurrency in currencies.CurrencyInfos)
            {
                if (firstCurrency.CharCode.Equals(secondCurrency.CharCode, StringComparison.Ordinal))
                {
                    continue;
                }

                var relativeCurrenciesRate =
                    _relativeCurrencyRateCalculator.Calculate(firstCurrency.CurrentRate,
                        secondCurrency.CurrentRate);

                var relativeCurrencyRate = new RelativeCurrencyRate(relativeCurrenciesRate, firstCurrency.Name,
                    firstCurrency.CharCode, secondCurrency.Name, secondCurrency.CharCode);

                relativeCurrencyRates.Add(relativeCurrencyRate);
            }
        }

        return relativeCurrencyRates;
    }
}