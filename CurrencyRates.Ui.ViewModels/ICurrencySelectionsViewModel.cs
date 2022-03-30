using System.Collections.Generic;

namespace CurrencyRates.Ui.ViewModels;

public interface ICurrencySelectionsViewModel
{
    ISelectedCurrencyViewModel FirstSelectedCurrency { get; set; }
    ISelectedCurrencyViewModel SecondSelectedCurrency { get; set; }

    bool IsFirstIndexSelecting { get; set; }
    IReadOnlyList<string> CurrencyNames { get; }

    IRelativeCurrencyRateViewModel? GetSelectedRelativeCurrencyRateViewModel();
    bool AreEqualIndexes();
}