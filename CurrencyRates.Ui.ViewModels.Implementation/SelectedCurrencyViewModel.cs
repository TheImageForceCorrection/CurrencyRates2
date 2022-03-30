namespace CurrencyRates.Ui.ViewModels.Implementation;

public sealed class SelectedCurrencyViewModel : ISelectedCurrencyViewModel
{
    public SelectedCurrencyViewModel(int selectedCurrencyIndex)
    {
        SelectedCurrencyIndex = selectedCurrencyIndex;
    }

    public int SelectedCurrencyIndex { get; set; }
}