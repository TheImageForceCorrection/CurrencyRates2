using System.ComponentModel;
using System.Runtime.CompilerServices;
using CurrencyRates.BusinessLogic;
using Ui.ViewModels;

namespace CurrencyRates.Ui.ViewModels.Implementation
{
    public sealed class RelativeCurrencyRateViewModel : IRelativeCurrencyRateViewModel
    {
        // ReSharper disable once RedundantDefaultMemberInitializer
        private double _firstCurrencyAmount = 0;

        // ReSharper disable once RedundantDefaultMemberInitializer
        private double _secondCurrencyAmount = 0;

        public RelativeCurrencyRateViewModel(RelativeCurrencyRate relativeCurrencyRate) : this(
            relativeCurrencyRate.RelativeRate,
            relativeCurrencyRate.FirstCurrencyCharCode, relativeCurrencyRate.SecondCurrencyCharCode)
        {
        }

        private RelativeCurrencyRateViewModel(double relativeCurrencyRate,
            string firstCurrencyCharCode, string secondCurrencyCharCode)
        {
            RelativeCurrencyRate = relativeCurrencyRate;
            FirstCurrencyCharCode = firstCurrencyCharCode;
            SecondCurrencyCharCode = secondCurrencyCharCode;
        }

        public string FirstCurrencyCharCode { get; }
        public string SecondCurrencyCharCode { get; }

        public double FirstCurrencyAmount
        {
            get => _firstCurrencyAmount;
            set
            {
                _firstCurrencyAmount = value;
                _secondCurrencyAmount = value / RelativeCurrencyRate;
                OnPropertyChanged(nameof(FirstCurrencyAmount));
                OnPropertyChanged(nameof(SecondCurrencyAmount));
            }
        }

        public double SecondCurrencyAmount
        {
            get => _secondCurrencyAmount;
            set
            {
                _secondCurrencyAmount = value;
                _firstCurrencyAmount = value * RelativeCurrencyRate;
                OnPropertyChanged(nameof(FirstCurrencyAmount));
                OnPropertyChanged(nameof(SecondCurrencyAmount));
            }
        }

        public void ClearCurrencyAmounts()
        {
            FirstCurrencyAmount = 0;
            SecondCurrencyAmount = 0;
        }

        private double RelativeCurrencyRate { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}