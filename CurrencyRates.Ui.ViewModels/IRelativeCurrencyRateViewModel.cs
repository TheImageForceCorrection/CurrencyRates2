using System.ComponentModel;

namespace CurrencyRates.Ui.ViewModels;

public interface IRelativeCurrencyRateViewModel : INotifyPropertyChanged
{
    string FirstCurrencyCharCode { get; }
    string SecondCurrencyCharCode { get; }
    double FirstCurrencyAmount { get; set; }
    double SecondCurrencyAmount { get; set; }

    void ClearCurrencyAmounts();
}