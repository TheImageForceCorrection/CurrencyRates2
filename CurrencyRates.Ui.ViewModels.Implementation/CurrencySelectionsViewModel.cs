using System.Collections.Generic;
using System.Diagnostics;

namespace CurrencyRates.Ui.ViewModels.Implementation;

public sealed class CurrencySelectionsViewModel : ICurrencySelectionsViewModel
{
    public CurrencySelectionsViewModel(IRelativeCurrencyRateViewModel[,] currencyInfos,
        IReadOnlyList<string> currencyNames)
    {
        Debug.Assert(currencyInfos.GetLength(0) == currencyInfos.GetLength(1));
        Debug.Assert(currencyInfos.GetLength(0) == currencyNames.Count);

        CurrencyInfos = currencyInfos;
        CurrencyNames = currencyNames;
        FirstSelectedCurrency = new SelectedCurrencyViewModel(0);
        SecondSelectedCurrency = new SelectedCurrencyViewModel(1);
    }

    public IReadOnlyList<string> CurrencyNames { get; }

    public ISelectedCurrencyViewModel FirstSelectedCurrency { get; set; }
    public ISelectedCurrencyViewModel SecondSelectedCurrency { get; set; }
    public bool IsFirstIndexSelecting { get; set; }

    private IRelativeCurrencyRateViewModel[,] CurrencyInfos { get; }

    public bool AreEqualIndexes() =>
        FirstSelectedCurrency.SelectedCurrencyIndex == SecondSelectedCurrency.SelectedCurrencyIndex;

    public IRelativeCurrencyRateViewModel? GetSelectedRelativeCurrencyRateViewModel()
    {
        var currencyNamesCount = CurrencyNames.Count;
        var firstCurrencyIndex = FirstSelectedCurrency.SelectedCurrencyIndex;
        var secondCurrencyIndex = SecondSelectedCurrency.SelectedCurrencyIndex;

        if (firstCurrencyIndex == secondCurrencyIndex ||
            firstCurrencyIndex >= currencyNamesCount ||
            secondCurrencyIndex >= currencyNamesCount)
        {
            return null;
        }

        return CurrencyInfos[firstCurrencyIndex, secondCurrencyIndex];
    }
}