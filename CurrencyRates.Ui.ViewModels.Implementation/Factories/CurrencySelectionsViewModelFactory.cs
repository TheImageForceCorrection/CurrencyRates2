using System;
using System.Linq;
using System.Threading.Tasks;
using CurrencyRates.BusinessLogic;
using CurrencyRates.Ui.ViewModels.Factories;

namespace CurrencyRates.Ui.ViewModels.Implementation.Factories;

public sealed class CurrencySelectionsViewModelFactory : ICurrencySelectionsViewModelFactory
{
    private readonly IRelativeCurrencyRatesProvider _relativeCurrencyRatesProvider;
    private readonly Func<RelativeCurrencyRate, IRelativeCurrencyRateViewModel> _relativeCurrencyRateViewModelFactory;

    public CurrencySelectionsViewModelFactory(IRelativeCurrencyRatesProvider relativeCurrencyRatesProvider,
        Func<RelativeCurrencyRate, IRelativeCurrencyRateViewModel> relativeCurrencyRateViewModelFactory)
    {
        _relativeCurrencyRatesProvider = relativeCurrencyRatesProvider;
        _relativeCurrencyRateViewModelFactory = relativeCurrencyRateViewModelFactory;
    }

    public async Task<ICurrencySelectionsViewModel> Create()
    {
        var relativeCurrencyRates = await _relativeCurrencyRatesProvider.ProvideAll();
        var currencyNames = relativeCurrencyRates.Select(r => r.FirstCurrencyName)
            .Distinct(StringComparer.Ordinal).ToList();

        var relativeCurrencyRateViewModels =
            new IRelativeCurrencyRateViewModel[currencyNames.Count, currencyNames.Count];

        foreach (var relativeCurrencyRate in relativeCurrencyRates)
        {
            var firstCurrencyRateIndex = currencyNames.IndexOf(relativeCurrencyRate.FirstCurrencyName);
            var secondCurrencyRateIndex = currencyNames.IndexOf(relativeCurrencyRate.SecondCurrencyName);

            var relativeCurrencyRateViewModel = _relativeCurrencyRateViewModelFactory.Invoke(relativeCurrencyRate);

            relativeCurrencyRateViewModels[firstCurrencyRateIndex, secondCurrencyRateIndex] =
                relativeCurrencyRateViewModel;
        }

        return new CurrencySelectionsViewModel(relativeCurrencyRateViewModels, currencyNames);
    }
}